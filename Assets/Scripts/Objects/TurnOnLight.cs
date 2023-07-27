using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnLight : MonoBehaviour
{
    //Spot Light Object
    public GameObject lightObject;

    //������Ʈ�� ��Ҵ���
    private bool isTouch;

    //���� ��������
    [Tooltip("���� ���� ������ True")]
    public bool isTrunOnLight;

    // Update is called once per frame
    void Update()
    {
        if(isTouch && Input.GetKeyDown(KeyCode.Z))
        {
            lightObject.SetActive(true);
            isTrunOnLight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTouch = false;
        }
    }
}
