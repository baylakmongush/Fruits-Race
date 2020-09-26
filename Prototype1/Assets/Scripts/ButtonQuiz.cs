using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ButtonQuiz : MonoBehaviour
{
    public Text text;
    public Tasks tasks;
    public Image ImageButton;
    int score = 0;
    public Text scoreText;
    public Text answCount;
    public Canvas canvasQuitQuiz;

    public void Check()
    {
        Debug.Log(text.text);
        string str1 = text.text.Trim();
        string str2 = tasks.TrueAns.Trim();
       // if (GetComponent<PhotonView>().IsMine)
       // {
            if (str1 == str2)
            {
                ImageButton.color = Color.green;
                PlayerPrefs.SetInt("score_temp", PlayerPrefs.GetInt("score_temp") + 2);
                PlayerPrefs.SetInt("true_answer", PlayerPrefs.GetInt("true_answer") + 1);
                answCount.text = "Правильные ответы: " + PlayerPrefs.GetInt("true_answer");
                scoreText.text = "Счёт: " + score;
                canvasQuitQuiz.enabled = true;
            }
            else
                ImageButton.color = Color.red;
      //  }
    }
}
