using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CloseButtonQuiz : MonoBehaviour
{
    public Canvas canvas;
    public Image[] ans;
    public Canvas quitCanvas;
    public Slider slider;
    public Tasks tasks;
    public List<Text> item;
    public Button[] btns;
    public void Close()
    {
        PlayerPrefs.SetInt("TaskChange", 1);
        for (int i = 0; i < ans.Length; i++)
            ans[i].color = Color.white;
        tasks.ischecked = true;
        tasks.taskChange = true;
        quitCanvas.enabled = false;
        canvas.enabled = false;
        slider.value = 10;
        tasks.timeRemaning = 10;
        tasks.answers.Add(item[0]);
        tasks.answers.Add(item[1]);
        tasks.answers.Add(item[2]);
        tasks.answers.Add(item[3]);
        for (int i = 0; i < btns.Length; i++)
            btns[i].enabled = true;
    }
}
