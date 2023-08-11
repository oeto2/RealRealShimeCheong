using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowNPC : MonoBehaviour
{
    //오브젝트의 SpriteRenderer
    //public SpriteRenderer objcetSpriteRender;

    //오브젝트의 기본 이미지
    //public Sprite sprite_Origin;

    //오브젝트의 빛나는 이미지
    //public Sprite sprite_Change;

    //Spot Light Object
    public GameObject lightObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lightObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lightObject.SetActive(false);
        }
    }
}
