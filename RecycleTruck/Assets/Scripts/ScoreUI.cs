using System.Linq;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private ScoreManager m_ScoreManager;
    [SerializeField] private RowUI m_RowUI;

    void Start()
    {
        m_ScoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        PresentSortedLeaderBoard();
    }

    // this method sort highScore leaderBoard and present it in a table format
    public void PresentSortedLeaderBoard()
    {
        foreach (Transform child in transform)
        {
            if(child.name.Contains("LeaderBoardRow"))
            {
                Destroy(child.gameObject);
            }
        }

        Debug.Log($"presenting");

        var scores = m_ScoreManager.SortedHighScoreLeaderBoard().ToArray();
        for (int i = 0; i < scores.Length; i++)
        {
            Debug.Log("length: " + scores.Length);

            var row = Instantiate(m_RowUI, transform).GetComponent<RowUI>();
            row.Rank.text = (i + 1).ToString();
            Debug.Log("name:" + scores[i].name);
            Debug.Log("score: " + scores[i].score.ToString());
            row.Name.text = scores[i].name;
            row.Score.text = scores[i].score.ToString();
        }
    }



    // this method resets the leaderboard.
    public void ResetLeaderboard()
    {
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>()?.ResetScoreLeaderBoard();

        // Clear the content object by destroying all its child objects
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }   
}

