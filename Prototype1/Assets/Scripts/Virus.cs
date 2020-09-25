using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Virus :  MonoBehaviour
{
    public Vector3 pointB;
    Vector3 position;

    IEnumerator Start()
    {
        var pointA = transform.position;
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
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
            position = thisTransform.position;
            yield return null;
        }
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //this is where i tried to fix it with OnPhotonSerializeView but it doesn't work
        if (stream.IsWriting)
        {
            stream.SendNext(position);
        }
        else
        {
            position = (Vector3)stream.ReceiveNext();

        }
    }
}
