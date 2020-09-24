using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Text LogText;
    public Text UserName;

    void Start()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(1, 1000);
        PhotonNetwork.AutomaticallySyncScene = true;
        Log("Player's name is set to " + PhotonNetwork.NickName);
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
        UserName.text = "HELLO, + " + PlayerPrefs.GetString("login") + "!";
    }

    public override void OnConnectedToMaster()
    {
        Log("Connected to master");
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Log("Joined the room");
        PhotonNetwork.LoadLevel("Game");
    }


    void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }
}
