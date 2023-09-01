using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private ScoreData _scoresData;

    void Awake()
    {
        var json = PlayerPrefs.GetString("scores", "{}");
        _scoresData = JsonUtility.FromJson<ScoreData>(json);
        
        // for first run of the game
        // _scoresData = new ScoreData();
    }

    public IEnumerable<Score> GetHighScore()
    {
        return _scoresData.scores.OrderByDescending(x => x.score);
    }

    public void AddScore(Score score)
    {
        _scoresData.scores.Add(score);
    }

    private void OnDestroy()
    {
        SaveScore();
    }

    public void SaveScore()
    {
        var json = JsonUtility.ToJson(_scoresData);
        PlayerPrefs.SetString("scores", json);
    }

    public void ResetScore()
    {
        _scoresData.scores?.Clear();
        Debug.Log("Score Data was cleared");

    }
}



[Serializable]
public class Score
{
    public string name;
    public float score;

    public Score(string name, float score)
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
