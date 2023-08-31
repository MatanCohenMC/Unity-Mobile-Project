using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Idle,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentGameState { get; private set; }
    public string PlayerName { get; private set; }
    public HealthManager m_HealthManager;

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
        m_HealthManager = GetComponentInChildren<HealthManager>();
    }

    private void Update()
    {
        
    }


    public void StartGame()
    {
        CurrentGameState = GameState.Playing;
        Debug.Log("Game state: Playing");

    }

    public void EndGame()
    {
        //GameObject.Find("MainMenuCanvas").SetActive(true);
        CurrentGameState = GameState.GameOver;
        Debug.Log("Game state: GameOver");
    }

    public void SetPlayerName(string name)
    {
        if(name != string.Empty)
        {
            PlayerName = name;
            Debug.Log($"Player name set to: {PlayerName}");
        }
    }
}

