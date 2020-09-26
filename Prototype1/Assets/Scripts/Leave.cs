using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Leave : MonoBehaviourPunCallbacks
{
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.LoadLevel(0);
    }

    public void LeaveBt()
    {
        PhotonNetwork.LeaveRoom();
    }
}
