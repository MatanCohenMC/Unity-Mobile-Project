using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LeaderboardTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> leaderboardEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("LeaderBoardEntryContainer");
        entryTemplate = transform.Find("LeaderBoardEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        //AddLeaderboardEntry("aaa", 10);
        string jsonString = PlayerPrefs.GetString("LeaderboardTable");
        Leaderboard leaderboard = JsonUtility.FromJson<Leaderboard>(jsonString);

        // Sort leaderboard

        leaderboardEntryTransformList = new List<Transform>();
        
    }
}
