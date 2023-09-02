using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public ScoreManager _scoreManager;
    public RowUI _rowUi;

    void Start()
    {
        /*_scoreManager.AddScore(new Score("MATAN", 10));
        _scoreManager.AddScore(new Score("TAHEL", 11));*/

        var scores = _scoreManager.SortHighScoreLeaderBoard().ToArray();
        for(int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(_rowUi, transform).GetComponent<RowUI>();
            row.rank.text = (i + 1).ToString();
            row.name.text = scores[i].name;
            row.score.text = scores[i].score.ToString();
        }
    }

    public void ResetLeaderboard()
    {
        _scoreManager?.ResetScoreLeaderBoard();

        // Clear the content object by destroying all its child objects
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    
}

