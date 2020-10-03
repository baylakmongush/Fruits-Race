using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class CreateNewChar : MonoBehaviour
{

    public GameObject[] prefabPlayer;
    public Text score;
    public Text answ;
    void Start()
    {
       PhotonNetwork.AutomaticallySyncScene = true;
        if (GetComponent<PhotonView>().IsMine)
        {
            Vector3 pos = Vector2.zero;
            answ.text = "Правильные ответы: " + PlayerPrefs.GetInt("true_answer");
            score.text = "Счёт: " + PhotonNetwork.LocalPlayer.GetScore().ToString();
            PhotonNetwork.Instantiate(prefabPlayer[PlayerPrefs.GetInt("NumberCharClient")].name, pos, Quaternion.identity);
        }
    }
}
