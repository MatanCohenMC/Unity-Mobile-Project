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
    
    public void StartGame()
    {
        CurrentGameState = GameState.Playing;
        Debug.Log("Game state: Playing");

    }

    public void EndGame()
    {
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

