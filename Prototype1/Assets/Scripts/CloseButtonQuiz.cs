using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CloseButtonQuiz : MonoBehaviour
{
    public Canvas canvas;
    public void Close()
    {
        canvas.enabled = false;
    }
}
