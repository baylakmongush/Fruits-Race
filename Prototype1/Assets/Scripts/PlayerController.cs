using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = 4;
    private PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        float horiz = Input.GetAxis("Horizontal");
        Vector3 pos = transform.position;
        if (horiz != 0)
        {
            pos.x += speed * horiz * Time.deltaTime;
        }
        transform.position = pos;
    }
}
