using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mandarin : PlayerController
{
    public override void LoadAnim()
    {
        getAnimator.runtimeAnimatorController = Resources.Load("PlayerAnim/MandarinStay") as RuntimeAnimatorController;
    }
}
