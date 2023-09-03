using System.Linq;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public ScoreManager m_ScoreManager;
    public RowUI m_RowUi;

    void Start()
    {
        PresentSortedLeaderBoard();
    }

    // this method sort highScore leaderBoard and present it in a table format
    private void PresentSortedLeaderBoard()
    {
        var scores = m_ScoreManager.SortHighScoreLeaderBoard().ToArray();
        for (int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(m_RowUi, transform).GetComponent<RowUI>();
            row.m_Rank.text = (i + 1).ToString();
            row.m_Name.text = scores[i].name;
            row.m_Score.text = scores[i].score.ToString();
        }
    }

    // this method resets the leaderboard.
    public void ResetLeaderboard()
    {
        m_ScoreManager?.ResetScoreLeaderBoard();

        // Clear the content object by destroying all its child objects
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }   
}

