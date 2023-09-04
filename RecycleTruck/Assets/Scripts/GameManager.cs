using DG.Tweening;
using UnityEngine;
using TMPro;
using System.Reflection;

public enum GameState
{
    Idle,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    // Define a delegate type for the game setup event.
    public delegate void GameResetEventHandler();
    // Create a delegate instance for the game setup event.
    public GameResetEventHandler OnGameSetup;
    public static GameManager Instance { get; private set; }
    public GameState CurrentGameState { get; private set; }
    public string PlayerName { get; private set; }
    private const int k_Invalid = -1;
    private const string k_DefaultPlayerName = "Player";
    public GameObject m_GameOverCanvas;
    private ScoreManager m_ScoreManager;
    private GameState m_PreviousGameStatus = GameState.Idle;
    private float m_CurrentTimeScale;
    [SerializeField] private GameObject m_QuitButton;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        DOTween.Init();

        // Set the initial game state
        changeGameStateToIdle();
    }

    private void Start()
    {
        m_ScoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();      
        SetPlayerName(k_DefaultPlayerName);
    }

    public void StartGame()
    {
        setupGame();
        GameObject.Find("CountDownPanel").GetComponent<CountdownManager>().StartCountdown(); // start the countDown
    }

    // This method is used when game is ending
    public void EndGame()
    {
        m_ScoreManager.AddScoreToLeaderBoard(new Score(PlayerName, m_ScoreManager.m_PlayerScore));
        // unactivate HUBCanvas
        GameObject.Find("HUBCanvas").SetActive(false);
        // activate GameOverCanvas
        m_GameOverCanvas.SetActive(true);
        setScoreAndFinalTextForTheEndGame();
        changeGameStateToGameOver();
        setupGame();
    }

    // This method is used when player quits the game, changing the game state to "Game Over" and resetting the game setup.
    public void QuitGame()
    {
        // Change the game state to "Game Over"
        changeGameStateToGameOver();
        // Reset the game setup
        setupGame();
    }

    private void changeGameStateToIdle()
    {
        CurrentGameState = GameState.Idle;
        Debug.Log("Game state: Idle");
    }

    public void changeGameStateToPlaying()
    {
        CurrentGameState = GameState.Playing;
        Debug.Log("Game state: Playing");
    }

    private void changeGameStateToGameOver()
    {
        CurrentGameState = GameState.GameOver;
        Debug.Log("Game state: GameOver");
    }

    // This method is used to pause and unpause the game when the player hit the hint button.
    public void PauseAndUnPauseGameWhenUsingHint()
    {
        GameObject hintWindowObject = GameObject.Find("HintWindow");

        if(hintWindowObject != null) // unpause the game
        {
            m_QuitButton.SetActive(true);
            Time.timeScale = m_CurrentTimeScale;
            CurrentGameState = m_PreviousGameStatus;
            Debug.Log($"Game state: {CurrentGameState}");
        }
        else // pause the game
        {
            m_QuitButton.SetActive(false);
            m_CurrentTimeScale = Time.timeScale;
            m_PreviousGameStatus = CurrentGameState;
            changeGameStateToIdle();
            Time.timeScale = 0;
        }       
    }

    // This method is responsible for setting the final score text and setting the encouraging text to player when the game is over.
    private void setScoreAndFinalTextForTheEndGame()
    {
        setEncouragePlayerTextWhenGameOver();
        setPlayerFinalScoreText();
    }

    // This method sets the text displayed to encourage the player when the game is over.
    private void setEncouragePlayerTextWhenGameOver()
    {
        TextMeshProUGUI EncouragePlayerObject = GameObject.Find("EncouragePlayerText").transform.GetComponent<TextMeshProUGUI>();
        int playerScore = getIntFieldInScoreManagerScript("m_PlayerScore");

        // if player is one of the leaders in the game and has more than 0 points
        if (playerScore > 0 && playerScore == getIntFieldInScoreManagerScript("m_HighScore"))
        {
            EncouragePlayerObject.text = "Keep up the eco-efforts,\nyou're one of the champions of our green world!";
        }
        else
        {
            EncouragePlayerObject.text = "Stay green, your eco-dedication shines bright!";
        }
    }

    // This method retrieves the integer value of a specified field ('i_FieldName') from the ScoreManager script
    // using reflection. If the field is found, its value is returned; otherwise, it returns 'k_Invalid' as an error indicator.
    private int getIntFieldInScoreManagerScript(string i_FieldName)
    {
        int fieldValue = k_Invalid;

        // Use reflection to access the 'i_FieldName' field.
        FieldInfo fieldInfo = m_ScoreManager.GetType().GetField(i_FieldName);

        if (fieldInfo != null)
        {
            // Get the value of 'i_FieldName' from the script.
            fieldValue = (int)fieldInfo.GetValue(m_ScoreManager);
            Debug.Log($"{i_FieldName} Value: {fieldValue}");
        }
        else
        {
            Debug.LogError($"Field {i_FieldName} not found in ScoreManager script.");
        }

        return fieldValue;
    }

    // This method updates the player's final score text.
    private void setPlayerFinalScoreText()
    {
        // Find the 'PlayerFinalScoreText' UI element and access its TextMeshProUGUI component.
        TextMeshProUGUI PlayerFinalScoreObject = GameObject.Find("PlayerFinalScoreText").transform.GetComponent<TextMeshProUGUI>();
        // Set the text of 'PlayerFinalScoreText' to the player's score.
        PlayerFinalScoreObject.text = getIntFieldInScoreManagerScript("m_PlayerScore").ToString();
    }

    // this method sets player's name
    public void SetPlayerName(string name)
    {
        if(name != string.Empty)
        {
            PlayerName = name;
            Debug.Log($"Player name set to: {PlayerName}");
        }
    }

    // This method is used to set up the game. It invokes the 'OnGameSetup' delegate to notify listeners
    // that the game should be initialized.
    public void setupGame()
    {
        // Invoke the game reset delegate to notify listeners that the game has been reset.
        OnGameSetup?.Invoke();
    }
}

