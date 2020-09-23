using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public Sprite[] spriteChar;
    Image imageChar;
    int i = 0;
    int current = 0;
     Canvas canvasSelect;
    public GameObject[] prefabPlayer;
    void Start()
    {
        imageChar = GameObject.FindWithTag("CharacterSprite").GetComponent<Image>();
        canvasSelect = GameObject.FindWithTag("Select").GetComponent<Canvas>();
    }

    public void NextButton()
    {
        i++;
        i = i > (spriteChar.Length - 1) ? 0 : i;
        imageChar.sprite = spriteChar[i];
        current = i;
    }

    public void PrevButton()
    {
        i--;
        i = i < 0 ? (spriteChar.Length - 1) : i;
        imageChar.sprite = spriteChar[i];
        current = i;
    }

    public void Select()
    {
        Debug.Log(current);
        switch (current)
        {
            case 0:
            {
                    Vector3 pos = Vector2.zero;
                    PhotonNetwork.Instantiate(prefabPlayer[0].name, pos, Quaternion.identity);
                    break;
            }
            case 1:
            {
                    Vector3 pos = Vector2.zero;
                    PhotonNetwork.Instantiate(prefabPlayer[1].name, pos, Quaternion.identity);
                    break;
            }
            case 2:
            {                    Vector3 pos = Vector2.zero;
                    PhotonNetwork.Instantiate(prefabPlayer[2].name, pos, Quaternion.identity);
                    break;
            }
            default:
                break;
        }
        canvasSelect.enabled = false;
        Destroy(this.gameObject);
    }
}
