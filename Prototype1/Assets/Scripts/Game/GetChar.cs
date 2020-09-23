using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChar : MonoBehaviour
{
	private static int number;
	Type randomType;

	public static int Number
	{
		get => number;
		set => number = value;
	}

	/*void GetClass()
    {
		Type[] scriptTypes = { typeof(Pear), typeof(Apple), typeof(Mandarin) };
		Debug.Log(Number);
		randomType = scriptTypes[PlayerPrefs.GetInt("NumberChar")];
	}*/

//    private void Awake()/
//	{
//		Type[] scriptTypes;// = { typeof(Pear), typeof(Apple), typeof(Mandarin) };
//		Debug.Log(Number);
//		randomType = scriptTypes[PlayerPrefs.GetInt("NumberChar")];
//		gameObject.AddComponent(randomType);
//		//CameraController.Player = gameObject.transform;
//	}
}
