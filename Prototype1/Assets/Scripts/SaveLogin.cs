using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class SaveLogin
{
    public static string username_Save;
    public static bool res;
    static public void Login(string username)
    {
        username_Save = username;
    }
    static public void SetStats(int score)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = "Score", Value = score
    }, }
        },
        result => { Debug.Log("User Statistics update"); },
        error => {
            Debug.LogError(error.GenerateErrorReport());
        });
    }
}
