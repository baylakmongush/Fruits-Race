using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CreateNewChar : MonoBehaviour
{

    public GameObject[] prefabPlayer;
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        Vector3 pos = Vector2.zero;
        if (PhotonNetwork.IsMasterClient)
             PhotonNetwork.Instantiate(prefabPlayer[PlayerPrefs.GetInt("NumberCharMaster")].name, pos, Quaternion.identity);
         else
             PhotonNetwork.Instantiate(prefabPlayer[PlayerPrefs.GetInt("NumberCharClient")].name, pos, Quaternion.identity);
    }
}
