using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayFabManager : MonoBehaviour
{
    LoginWithPlayFabRequest loginReq;
    public InputField username;
    public InputField password;
    public InputField email;
    public bool isAuth = false;
    public Canvas lobby;
    public Canvas auth;
    public Text rankText;
    public Text usernameText;
    public Text rank;
    public Text score;
    
    void Start()
    {
        email.gameObject.SetActive(false);
    }

    #region Autorization
    public void Login()
    {
        loginReq = new LoginWithPlayFabRequest();
        loginReq.Username = username.text;
        loginReq.Password = password.text;
        PlayFabClientAPI.LoginWithPlayFab(loginReq, result => {
            isAuth = true;
            Debug.Log("You are login");
            PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = loginReq.Username }, OnDisplayName, OnLoginFailure);
            PlayerPrefs.SetString("login", loginReq.Username);
            usernameText.text = "Привет, " + loginReq.Username + "!";
            SetStats();
            GetLeaderBoard();
            auth.gameObject.SetActive(false);
            lobby.enabled = true;
        }, error => {
            isAuth = false;
            Debug.Log(error.ErrorMessage);
            email.gameObject.SetActive(true);
        }, null);
    }

    private void OnDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result.DisplayName + " is your new display name");
    }


    void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    public void Register()
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
        request.Email = email.text;
        request.Username = username.text;
        request.Password = password.text;
        PlayFabClientAPI.RegisterPlayFabUser(request, result => {
            Debug.Log("Welcome");
        }, error => {
            Debug.Log("Please enter your email");
        });
    }
    #endregion Authorization

    #region LeaderBoard
    public void GetLeaderBoard()
    {
        GetLeaderboardRequest getLeaderboardRequest = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "Score", MaxResultsCount = 20 };
        PlayFabClientAPI.GetLeaderboard(getLeaderboardRequest, OnGetLeaderboard, OnErrorBoard);
    }

    public void SetStats()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = "Score", Value = GetHighScore()
    }, }
        },
        result => { Debug.Log("User Statistics update"); },
        error => { Debug.LogError(error.GenerateErrorReport());
        });
    }

    int GetHighScore()
    {
        int resultScore;
        resultScore = Math.Max(PlayerPrefs.GetInt("Score"), PlayerPrefs.GetInt("LastScore"));
        return resultScore;
    }



    void OnGetLeaderboard(GetLeaderboardResult result)
    {
        Debug.Log(result.Leaderboard[0].StatValue);
        int i = 1;
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            Debug.Log(player.DisplayName + ": " + player.StatValue);
            rankText.text += i + " " + player.DisplayName + " " + player.StatValue + "\n";
            if (loginReq.Username == player.DisplayName)
            {
                rank.text = i.ToString();
                score.text = player.StatValue.ToString();
                PlayerPrefs.SetInt("LastScore", player.StatValue);
            }
            i++;
        }
    }


    void OnErrorBoard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
    #endregion LeaderBoard
}
