using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class ButtonQuiz : MonoBehaviour
{
    public Text text;
    public Tasks tasks;
    public Image ImageButton;
    int score = 0;
    public Text scoreText;
    public Text answCount;
    public Canvas canvasQuitQuiz;
    public Tasks value;
    public Button[] btns;
    PhotonView photonView;


    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    [PunRPC]
    void AddPoints()
    {
        PhotonNetwork.LocalPlayer.AddScore(2);
        Debug.Log("added + 2");
        UpdateText();
    }

    void UpdateText()
    {
        scoreText.text = "Счёт: " + PhotonNetwork.LocalPlayer.GetScore().ToString();
    }

    public void Check()
    {
        Debug.Log(text.text);
        string str1 = text.text.Trim();
        string str2 = tasks.TrueAns.Trim();

            if (str1 == str2)
            {
                ImageButton.color = Color.green;
            photonView.RPC("AddPoints", RpcTarget.AllBuffered);
            PlayerPrefs.SetInt("true_answer", PlayerPrefs.GetInt("true_answer") + 1);
                answCount.text = "Правильные ответы: " + PlayerPrefs.GetInt("true_answer");
            }
            else
                ImageButton.color = Color.red;
            for (int i = 0; i < btns.Length; i++)
                btns[i].enabled = false;
            canvasQuitQuiz.enabled = true;
            value.ischecked = false;
    }

}
