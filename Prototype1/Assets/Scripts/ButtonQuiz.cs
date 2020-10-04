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
    int score;
    int answer;
    public Text scoreText;
    public Text answCount;
    public Canvas canvasQuitQuiz;
    public Tasks value;
    public Button[] btns;
    PhotonView photonView;
    ExitGames.Client.Photon.Hashtable props;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        props = new ExitGames.Client.Photon.Hashtable();
        if (PhotonNetwork.LocalPlayer.CustomProperties["answer"] != null)
        {
            props.Add("answer", (int)PhotonNetwork.LocalPlayer.CustomProperties["answer"]);
        }
        else
        {
            props.Add("answer", 0);
        }
        if (PhotonNetwork.LocalPlayer.CustomProperties["score"] != null)
        {
            props.Add("score", (int)PhotonNetwork.LocalPlayer.CustomProperties["score"]);
        }
        else
        {
            props.Add("score", 0);
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }
    public void SendScore(int scoreReceived, int answerReceived)
    {
        score = (int)PhotonNetwork.LocalPlayer.CustomProperties["score"];
        answer = (int)PhotonNetwork.LocalPlayer.CustomProperties["answer"];
        score += scoreReceived;
        answer += answerReceived;
        scoreText.text = "Счёт: " + score.ToString();
        answCount.text = "Правильные ответы: " + answer.ToString();
        SetCustomProperties(score, answer);
    }

    void SetCustomProperties(int score, int answer)
    {
        props["score"] = score;
        props["answer"] = answer;
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }
    public void Check()
    {
        Debug.Log(text.text);
        string str1 = text.text.Trim();
        string str2 = tasks.TrueAns.Trim();

            if (str1 == str2 && photonView.IsMine)
            {
                ImageButton.color = Color.green;
                PlayerController.isTrue = true;
                SendScore(2, 1);
            }
            else if (str1 == str2 && !photonView.IsMine)
            {
                ImageButton.color = Color.green;
                PlayerController.isTrue = true;
                SendScore(2, 1);
            }
        else
                ImageButton.color = Color.red;
            for (int i = 0; i < btns.Length; i++)
                btns[i].enabled = false;
            canvasQuitQuiz.enabled = true;
            value.ischecked = false;
    }

}
