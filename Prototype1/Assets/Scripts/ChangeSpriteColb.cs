using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteColb : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] sprite;
    SpriteRenderer thisSprite;
    bool isChange = false;
    float time = 0;
    BoxCollider2D boxCollider;
    EdgeCollider2D edgeCollider;
    PhotonView photonView;
    bool change = false;
    void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        edgeCollider.enabled = false;
        photonView = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isChange = true;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (isChange)
        {
            if (photonView.IsMine)
            {
                change = true;
                photonView.RPC("changeSprite", RpcTarget.All);
                thisSprite.sprite = sprite[0];
            }
            if (time > 1)
            {
                boxCollider.enabled = false;
                edgeCollider.enabled = true;
                if (photonView.IsMine)
                {
                    change = false;
                    photonView.RPC("changeSprite", RpcTarget.All);
                    thisSprite.sprite = sprite[1];
                }
                isChange = false;
            }
        }
    }
    [PunRPC]
    void changeSprite()
    {
        if (change)
        {
            thisSprite.sprite = sprite[0];
            change = false;
        }
        else if (!change)
            thisSprite.sprite = sprite[1];
    }
}
