using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private const int k_ScoreToAdd = 10;
    private ScoreData _scoresData;
    public TextMeshProUGUI m_PlayerScoreText;
    public TextMeshProUGUI m_HighScoreText;
    public int m_PlayerScore;
    public int m_HighScore;


    void Awake()
    {
        var json = PlayerPrefs.GetString("scores", "{}");
        _scoresData = JsonUtility.FromJson<ScoreData>(json);

        if(_scoresData != null && _scoresData.scores.Count != 0)
        {
            m_HighScore = _scoresData.scores.First().score;
        }

        GameManager.Instance.OnGameSetup += SetupPlayerScore;
        // for first run of the game
        // _scoresData = new ScoreData();
    }

    private void Start()
    {
        updatePlayerScoreText();
        updateHighScoreText();
    }

    public void SetupPlayerScore()
    {
        m_PlayerScore = 0;
        updatePlayerScoreText();
    }

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

    private void updatePlayerScoreText()
    {
        m_PlayerScoreText.text = "Your Score: " + m_PlayerScore.ToString();
    }

    private void updateHighScoreText()
    {
        m_HighScoreText.text = "HighScore: " + m_HighScore.ToString();
    }

    public IEnumerable<Score> SortHighScoreLeaderBoard()
    {
        return _scoresData.scores.OrderByDescending(x => x.score);
    }

    public void AddScoreToLeaderBoard(Score score)
    {
        _scoresData.scores.Add(score);
    }

    private void OnDestroy()
    {
        SaveScoreLeaderBoard();
    }

    public void SaveScoreLeaderBoard()
    {
        var json = JsonUtility.ToJson(_scoresData);
        PlayerPrefs.SetString("scores", json);
    }

    public void ResetScoreLeaderBoard()
    {
        _scoresData.scores?.Clear();
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

public class ScoreData
{
    public List<Score> scores;

    public ScoreData()
    {
        scores = new List<Score>();
    }
}
