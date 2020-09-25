using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Quiz : MonoBehaviour
{
    public Canvas canvas;
    PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag == "MainCharacter" && photonView.IsMine)
        {
            canvas.enabled = true;
            collision.gameObject.SetActive(false);
        }*/
        canvas.enabled = true;
        collision.gameObject.SetActive(false);
    }
}
