using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private const int k_ScoreToAdd = 10;
    private ScoreData m_ScoresData;
    public TextMeshProUGUI m_PlayerScoreText;
    public TextMeshProUGUI m_HighScoreText;
    public int m_PlayerScore;
    public int m_HighScore;
    public GameObject m_LeaderBoardContent;

    void Awake()
    {
        // for first run of the game
        m_ScoresData = new ScoreData();

        // get the highscore value from the data.
        if (m_ScoresData != null && m_ScoresData.scores.Count != 0)
        {
            m_HighScore = m_ScoresData.scores.First().score;
        }
    }

    private void Start()
    {
        // Subscribe the SetupPlayerScore method to the OnGameSetup event in the GameManager instance.
        GameManager.Instance.OnGameSetup += SetupPlayerScore;
        updatePlayerScoreText();
        updateHighScoreText();
    }

    // this method initializes the player's score and updates the displayed player score text.
    public void SetupPlayerScore()
    {
        m_PlayerScore = 0;
        updatePlayerScoreText();
    }

    // this method adds points to the player's score, updates the displayed player score text, and checks for a new high score.
    public void AddPointsToPlayerScore()
    {
        m_PlayerScore += k_ScoreToAdd;
        updatePlayerScoreText();

        if (m_PlayerScore > m_HighScore)
        {
            m_HighScore = m_PlayerScore;
            updateHighScoreText();
        }
    }

    // this method updates the displayed player score text with the current player score.
    private void updatePlayerScoreText()
    {
        m_PlayerScoreText.text = "Your Score: " + m_PlayerScore.ToString();
    }

    // this method updates the displayed high score text with the current high score.
    private void updateHighScoreText()
    {
        m_HighScoreText.text = "HighScore: " + m_HighScore.ToString();
    }

    // this method return a sorted leaderboard of the highscores
    public IEnumerable<Score> SortedHighScoreLeaderBoard()
    {
        return m_ScoresData.scores.OrderByDescending(x => x.score);
    }

    // this method adds a Score to leaderBoard
    public void AddScoreToLeaderBoard(Score score)
    {
        m_ScoresData.scores.Add(score);       
        this.GetComponent<ScoreUI>().PresentSortedLeaderBoard();
    }

    // this method resets LeaderBoard
    public void ResetScoreLeaderBoard()
    {
        m_ScoresData.scores?.Clear();
        Debug.Log("Score Data was cleared");
    }
}


[Serializable]
public class Score
{
    public string name;
    public int score;

    public Score(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

[Serializable]
public class ScoreData
{
    public List<Score> scores;

    public ScoreData()
    {
        scores = new List<Score>();
    }
}
