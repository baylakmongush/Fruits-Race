using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;

public class Finish : MonoBehaviourPunCallbacks
{


    public Text text;
    public List<Player> playerList;
    public Canvas canvas;
    ExitGames.Client.Photon.Hashtable props;
    int score;
    public Text scoreText;
    PhotonView photonView;

    private void Start()
    {
        props = new ExitGames.Client.Photon.Hashtable();
        if (PhotonNetwork.LocalPlayer.CustomProperties["score"] != null)
            props.Add("score", (int)PhotonNetwork.LocalPlayer.CustomProperties["score"]);
        else
            props.Add("score", 0);
        photonView = GetComponent<PhotonView>();
    }
    private void FixedUpdate()
    {
        if (canvas.enabled == true)
            UpdateSortList();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateSortList();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateSortList();
    }
    public void UpdateSortList()
    {
        text.text = "";
        playerList = PhotonNetwork.PlayerList.ToList();
        playerList.Sort(sortByScore);
        for (int i = 0; i < playerList.Count; i++)
        {
            int rank = i + 1;
            text.text += rank + ". " + SaveLogin.username_Save + " счёт: "+playerList[i].CustomProperties["score"] + "\n";
        }
    }
    public static int sortByScore(Player a, Player b)
    {
        return b.GetScore().CompareTo(a.GetScore());
    }

    public void SendScore(int scoreReceived)
    {
        score = (int)PhotonNetwork.LocalPlayer.CustomProperties["score"];
        score += scoreReceived;
        scoreText.text = "Счёт: " + score.ToString();
        SetCustomProperties(score);
        SaveLogin.SetStats((int)PhotonNetwork.LocalPlayer.CustomProperties["score"]);
    }

    void SetCustomProperties(int score)
    {
        props["score"] = score;
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MainCharacter")
        {
            GameObject player = GameObject.FindGameObjectWithTag("MainCharacter");
            player.SetActive(false);
            SendScore(3);
            canvas.enabled = true;
        }
    }
}
