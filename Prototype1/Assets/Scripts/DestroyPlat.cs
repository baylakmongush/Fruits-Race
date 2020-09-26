using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyPlat : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MainCharacter")
        {
            if (GetComponent<PhotonView>().InstantiationId == 0)
            {
                Destroy(gameObject, 2f);
            }
            else
            {
                if (GetComponent<PhotonView>().IsMine)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }
    }
}
