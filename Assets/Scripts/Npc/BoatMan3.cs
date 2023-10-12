using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMan3 : MonoBehaviour
{
    //������ ��Ҵ���
    public bool isTouch;

    private void Update()
    {
        if(isTouch && Input.GetKeyDown(KeyCode.Z) && !EventManager.instance.gameObject_SelectUI.activeSelf)
        {
            //������ ����
            EventManager.instance.SelectStart(NPCName.boatman3, 7194);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }
}
