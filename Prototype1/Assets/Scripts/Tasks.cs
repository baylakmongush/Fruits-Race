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
    string task;

    private void Start()
    {
        int randomNumber = Random.Range(0, 6);
        task = tasks[randomNumber].text;
        GetAllInf = task.Split('/');
        Task = GetAllInf[0];
        taskText.text = Task;
        TrueAns = GetAllInf[2];
        Ans = GetAllInf[1].Split(';');
        int i = 0;
        while (i < 4)
        {
            int index = Random.Range(0, answers.Count);
            answers[index].text = Ans[i];
            answers.RemoveAt(index);
            i++;
        }
    }
}
