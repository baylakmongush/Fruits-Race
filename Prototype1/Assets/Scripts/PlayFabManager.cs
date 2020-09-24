using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class PlayFabManager : MonoBehaviour
{
    LoginWithPlayFabRequest loginReq;
    public InputField username;
    public InputField password;
    public InputField email;
    public bool isAuth = false;
    public Canvas lobby;
    public Canvas auth;
    
    void Start()
    {
        email.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            //     PlayerPrefs.SetInt("rank", loginReq.Username);
            //     PlayerPrefs.SetInt("score", loginReq.Username);
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

    void OnGetLeaderboard(GetLeaderboardResult result)
    {
        Debug.Log(result.Leaderboard[0].StatValue);
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            Debug.Log(player.DisplayName + ": " + player.StatValue);
        }
    }

    void OnErrorBoard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
    #endregion LeaderBoard
}
