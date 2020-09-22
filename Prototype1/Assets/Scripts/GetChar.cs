using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChar : MonoBehaviour
{
	private int number;
	Type randomType;

	void GetClass()
    {
		Type[] scriptTypes = { typeof(Pear), typeof(Apple), typeof(Mandarin) };
		switch (Number)
		{
			case 1:
				randomType = scriptTypes[1];
				break;
			case 2:
				randomType = scriptTypes[2];
				break;
			case 3:
				randomType = scriptTypes[1];
				break;
			default:
				return;
		}
	}
	void Start()
	{
		GetClass();
		gameObject.AddComponent(randomType);
	}

	public int Number
	{
		get => this.number;
		set => this.number = value;
	}
}
