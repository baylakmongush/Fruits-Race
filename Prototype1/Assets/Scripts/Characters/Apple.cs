using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Apple : PlayerController
{
	public override void LoadAnim()
    {
		getAnimator.runtimeAnimatorController = Resources.Load("PlayerAnim/AppleStay") as RuntimeAnimatorController;
	}
}
