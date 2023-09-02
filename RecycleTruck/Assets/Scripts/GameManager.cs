using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System;
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
    public GameObject m_GameOverCanvas;
    private ScoreManager m_ScoreManager;
   
    //public HealthManager m_HealthManager;

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
        CurrentGameState = GameState.Idle;
        Debug.Log("Game state: Idle");
    }

    private void Start()
    {
        m_ScoreManager = this.GetComponent<ScoreManager>();
    }


    public void StartGame()
    {
        setupGame(); 
        //CountdownManager.Instance.StartCountdown();
        GameObject.Find("CountDownPanel").GetComponent<CountdownManager>().StartCountdown();
    }

    public void changeGameStateToPlaying()
    {
        CurrentGameState = GameState.Playing;
        Debug.Log("Game state: Playing");
    }

    public void EndGame()
    {
        GameObject.Find("HUBCanvas").SetActive(false);
        m_GameOverCanvas.SetActive(true);
        setEndGameComponents();
        CurrentGameState = GameState.GameOver;
        Debug.Log("Game state: GameOver");
        setupGame();
    }

    public void QuitGame()
    {
        CurrentGameState = GameState.GameOver;
        Debug.Log("Game state: GameOver");
        setupGame();
    }

    public void PauseAndUnPauseGame()
    {
        if (CurrentGameState == GameState.Idle)
        {
            CurrentGameState = GameState.Playing;
            Debug.Log("Game state: Playing");
        }
        else if (CurrentGameState == GameState.Playing)
        {
            CurrentGameState = GameState.Idle;
            Debug.Log("Game state: Idle");
        }
    }
    private void setEndGameComponents()
    {
        setEncouragePlayerText();
        setPlayerFinalScoreText();
    }

    private void setEncouragePlayerText()
    {
        TextMeshProUGUI EncouragePlayerObject = GameObject.Find("EncouragePlayerText").transform.GetComponent<TextMeshProUGUI>();
        int playerScore = getIntFieldInScoreManagerScript("m_PlayerScore");

        if (playerScore > 0 && playerScore == getIntFieldInScoreManagerScript("m_HighScore"))
        {
            EncouragePlayerObject.text = "Keep up the eco-efforts,\nyou're one of the champions of our green world!";
        }
        else
        {
            EncouragePlayerObject.text = "Stay green, your eco-dedication shines bright!";
        }
    }    

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

    private void setPlayerFinalScoreText()
    {
        TextMeshProUGUI PlayerFinalScoreObject = GameObject.Find("PlayerFinalScoreText").transform.GetComponent<TextMeshProUGUI>();
        PlayerFinalScoreObject.text = getIntFieldInScoreManagerScript("m_PlayerScore").ToString();
    }

    public void SetPlayerName(string name)
    {
        if(name != string.Empty)
        {
            PlayerName = name;
            Debug.Log($"Player name set to: {PlayerName}");
        }
    }

    //public void ResetGame()
    //{
    //    HealthManager.Instance.ResetLives();
    //    ScoreManager.Instance.ResetPlayerScore();

    //}

    public void setupGame()
    {
        // Invoke the game reset delegate to notify listeners that the game has been reset.
        OnGameSetup?.Invoke();
    }

}

