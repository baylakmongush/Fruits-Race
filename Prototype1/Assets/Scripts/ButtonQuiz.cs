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
    public Tasks value;
    public Button[] btns;

    public void Check()
    {
        Debug.Log(text.text);
        string str1 = text.text.Trim();
        string str2 = tasks.TrueAns.Trim();

            if (str1 == str2)
            {
                ImageButton.color = Color.green;
                PlayerPrefs.SetInt("score_temp", PlayerPrefs.GetInt("score_temp") + 2);
                score = PlayerPrefs.GetInt("score_temp");
                PlayerPrefs.SetInt("true_answer", PlayerPrefs.GetInt("true_answer") + 1);
                answCount.text = "Правильные ответы: " + PlayerPrefs.GetInt("true_answer");
                scoreText.text = "Счёт: " + score;
            }
            else
                ImageButton.color = Color.red;
            for (int i = 0; i < btns.Length; i++)
                btns[i].enabled = false;
            canvasQuitQuiz.enabled = true;
            value.ischecked = false;
    }

}
