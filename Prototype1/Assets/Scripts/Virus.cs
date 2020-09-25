using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Virus :  MonoBehaviour
{
    public Vector3 pointB;
    Vector3 pointA;
    PhotonView photonView;

    [PunRPC]
    IEnumerator Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            pointA = transform.position;
            if (PhotonNetwork.CountOfPlayers == 2)
            {
                while (true)
                {
                    yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
                    yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
                }
            }
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(pointA);
            stream.SendNext(pointB);
            stream.SendNext(transform.position);
        }
        else
        {
            pointA = (Vector3)stream.ReceiveNext();
            pointB = (Vector3)stream.ReceiveNext();
            transform.position = (Vector3)stream.ReceiveNext();
        }
    }
}
