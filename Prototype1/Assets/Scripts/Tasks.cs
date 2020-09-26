using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tasks : MonoBehaviour
{
    public TextAsset[] tasks;
    string[] GetAllInf;
    string Task;
    string[] Ans;
    public string TrueAns;
    public Text taskText;
    public List<Text> answers;
    List<Text> tmp;
    string task;
    public Canvas thisCan;
    public Slider timer;
    public float timeRemaning = 10f;
    public bool ischecked = true;
    public Canvas canvasQuitQuiz;
    public bool taskChange = true;
    

    private void FixedUpdate()
    {
        tmp = answers;
        if (taskChange)
        {
            taskChange = false;
            int randomNumber = Random.Range(0, 6);
            task = tasks[randomNumber].text;
            GetAllInf = task.Split('/');
            Task = GetAllInf[0];
            taskText.text = Task;
            TrueAns = GetAllInf[2];
            Ans = GetAllInf[1].Split(';');
            int i = 0;
            int index = 1;
            while (i < 4)
            {
                if (tmp.Count > 0)
                {
                    index = Random.Range(0, tmp.Count);
                    tmp[index].text = Ans[i];
                    tmp.RemoveAt(index);
                }
                i++;
            }
        }


        if (thisCan.enabled == true)
        {
            if (timeRemaning > 0 && ischecked)
            {
                timeRemaning -= Time.deltaTime;
                timer.value = timeRemaning;
            }
            else if (timeRemaning <= 0)
            {
                timeRemaning = 0;
                canvasQuitQuiz.enabled = true;
            }
        }
    }
}
