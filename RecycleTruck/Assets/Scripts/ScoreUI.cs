using System.Linq;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private const int k_MaximumPlayersOnLeaderBoard = 5;
    private ScoreManager m_ScoreManager;
    private GameObject m_LeaderBoardContent;
    [SerializeField] private RowUI m_RowUI;

    void Start()
    {
        getMembersComponents();
        PresentSortedLeaderBoard();
    }

    // this method gets components of members
    private void getMembersComponents()
    {
        m_ScoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        m_LeaderBoardContent = m_ScoreManager.m_LeaderBoardContent;
    }

    // this method sort highScore leaderBoard and present it in a table format
    public void PresentSortedLeaderBoard()
    {
        removeContentRows();
        var scores = m_ScoreManager.SortedHighScoreLeaderBoard().ToArray();
        addContentRows(scores);
    }

    // this method adds content object rows to leaderboard
    private void addContentRows(Score[] scores)
    {
        for (int i = 0; i < scores.Length && i < k_MaximumPlayersOnLeaderBoard; i++)
        {
            var row = Instantiate(m_RowUI, m_LeaderBoardContent.transform).GetComponent<RowUI>();
            row.Rank.text = (i + 1).ToString();
            row.Name.text = scores[i].name;
            row.Score.text = scores[i].score.ToString();
        }
    }

    // this method resets the leaderboard.
    public void ResetLeaderboard()
    {
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>()?.ResetScoreLeaderBoard();
        removeContentRows();
    }  
            
    // Clear the content object by destroying all its child objects
    private void removeContentRows()
    {
        foreach (Transform child in m_LeaderBoardContent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}