using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static Transform player;
    public float smoothTime = 0.2f;
    Vector3 currentVelosity = Vector3.zero;
    private void LateUpdate()
    {
        //Vector2 newPos = new Vector3(player.position.x, currentVelosity.y, transform.position.z);
        if (player)
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }

    public static Transform Player
    {
        get => player;
        set => player = value;
    }
}
