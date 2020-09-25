using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public Sprite[] spriteChar;
    SpriteRenderer imageChar;
    int i = 0;
    int current = 0;
     Canvas canvasSelect;
    public GameObject[] prefabPlayer;
    public Animator animator;
    void Start()
    {
        imageChar = GameObject.FindWithTag("CharacterSprite").GetComponent<SpriteRenderer>();
        canvasSelect = GameObject.FindWithTag("Select").GetComponent<Canvas>();
    }


    void SelectAnim(int current)
    {
        switch (current)
        {
            case 0:
                {
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Select/Pear");
                    break;
                }
            case 1:
                {
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Select/Apple");
                    break;
                }
            case 2:
                {
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Select/Mandarin");
                    break;
                }
            default:
                break;
        }
    }

    public void NextButton()
    {
        i++;
        i = i > (spriteChar.Length - 1) ? 0 : i;
        imageChar.sprite = spriteChar[i];
        current = i;
        SelectAnim(current);
    }

    public void PrevButton()
    {
        i--;
        i = i < 0 ? (spriteChar.Length - 1) : i;
        imageChar.sprite = spriteChar[i];
        current = i;
        SelectAnim(current);
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
            {       Vector3 pos = Vector2.zero;
                    PhotonNetwork.Instantiate(prefabPlayer[2].name, pos, Quaternion.identity);
                    break;
            }
            default:
                break;
        }
        canvasSelect.enabled = false;
        Destroy(this.gameObject);
        Destroy(imageChar);
    }
}
