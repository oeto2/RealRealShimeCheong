using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowNPC : MonoBehaviour
{
    //������Ʈ�� SpriteRenderer
    //public SpriteRenderer objcetSpriteRender;

    //������Ʈ�� �⺻ �̹���
    //public Sprite sprite_Origin;

    //������Ʈ�� ������ �̹���
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
