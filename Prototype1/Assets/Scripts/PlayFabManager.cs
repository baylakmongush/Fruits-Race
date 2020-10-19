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
	public InputField username;
	public InputField password;
	public InputField usernameReg;
	public InputField passwordReg;
	public InputField email;
	public bool isAuth = false;
	public Canvas lobby;
	public Canvas auth;
	public Canvas reg;
	public Text rankText;
	public Text usernameText;
	public Text rank;
	public Text score;
	public Text log1;
	public Text log2;
	public Button avtorize;

	#region Autorization
	public void Login()
	{
		LoginWithPlayFabRequest loginReq = new LoginWithPlayFabRequest();
		loginReq.Username = username.text;
		loginReq.Password = password.text;
		PlayFabClientAPI.LoginWithPlayFab(loginReq, result => {
			Debug.Log("You are login");
			PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = SaveLogin.username_Save }, OnDisplayName, OnLoginFailure);
			usernameText.text = "Привет, " + username.text + "!";
			GetLeaderBoard();
			auth.gameObject.SetActive(false);
			lobby.enabled = true;
			SaveLogin.Login(username.text);
		}, error => {
			log1.text = "Неправильный логин/пароль";
			email.gameObject.SetActive(true);
		}, null);
	}

	private void OnDisplayName(UpdateUserTitleDisplayNameResult result)
	{
		Debug.Log(result.DisplayName + " is your new display name");
	}


	void OnLoginFailure(PlayFabError error)
	{
		log1.text = error.GenerateErrorReport();
	}

	public void RegOpen()
    {
		reg.enabled = true;
		auth.enabled = false;
	}

	public void AutoOpen()
	{
		reg.enabled = false;
		auth.enabled = true;
	}

	public void Register()
	{
		RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
		request.Email = email.text;
		request.Username = usernameReg.text;
		request.Password = passwordReg.text;
		PlayFabClientAPI.RegisterPlayFabUser(request, result => {
			log1.text =  "Вы зарегистрированы!";
			auth.enabled = true;
			reg.enabled = false;
		}, error => {
			log2.text = error.GenerateErrorReport();
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
		int i = 1;
		foreach (PlayerLeaderboardEntry player in result.Leaderboard)
		{
			Debug.Log(player.DisplayName + ": " + player.StatValue);
			rankText.text += i + " " + player.DisplayName + " " + player.StatValue + "\n";
			if (username.text == player.DisplayName)
			{
				rank.text = i.ToString();
				score.text = player.StatValue.ToString();
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
