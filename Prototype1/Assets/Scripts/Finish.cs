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
            text.text += rank + ". " + playerList[i].NickName + "\n";
        }
    }
    public static int sortByScore(Player a, Player b)
    {
        return b.GetScore().CompareTo(a.GetScore());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MainCharacter")
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.LocalPlayer.SetScore(PlayerPrefs.GetInt("score_temp") + 3);
                PlayerPrefs.SetInt("score_temp", 3);
            }
            else
                PhotonNetwork.LocalPlayer.SetScore(PlayerPrefs.GetInt("score_temp"));
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("score_temp"));
            canvas.enabled = true;
        }
    }
}
