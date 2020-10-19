using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Text UserName;
    public InputField roomNameCreate;
    public InputField roomNameJoin;

    void Start()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(1, 1000);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
        if (UserName)
            UserName.text = "HELLO, + " + PlayerPrefs.GetString("login") + "!";
    }


    public void CreateRoom()
    {
        if (roomNameCreate.text != "")
            PhotonNetwork.CreateRoom(roomName: roomNameCreate.text, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("true_answer1", 0);
    }
    public void CreateRandomRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("true_answer", 0);
    }

    public void JoinRoom()
    {
        if (roomNameJoin.text != "")
            PhotonNetwork.JoinRoom(roomName: roomNameJoin.text);
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("true_answer", 0);    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("true_answer", 0);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

}
