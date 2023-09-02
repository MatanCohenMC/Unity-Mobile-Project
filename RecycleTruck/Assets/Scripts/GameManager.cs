using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum GameState
{
    Idle,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    // Define a delegate type for the game reset event.
    public delegate void GameResetEventHandler();
    // Create a delegate instance for the game reset event.
    public GameResetEventHandler OnGameSetup;
    public static GameManager Instance { get; private set; }
    public GameState CurrentGameState { get; private set; }
    public string PlayerName { get; private set; }
    public GameObject m_GameOverCanvas;

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

    private void Update()
    {
        
    }


    
    public void StartGame()
    {
        setupGame(); 
        CountdownManager.Instance.StartCountdown();
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

