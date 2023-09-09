using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWater : MonoBehaviour
{
    //�ݶ��̴��� �÷��̾ �����ߴ���
    public bool isTouch;

    private void Update()
    {
        //�ٰ��� ������ ZŰ�� ������ ���
        if(isTouch && ObjectManager.instance.GetEquipObjectKey() == 1003 && Input.GetKeyDown(KeyCode.Z))
        {
            //�ڷ�ƾ ����
            StartCoroutine(DrawWaterStart());
        }
    }

    private IEnumerator DrawWaterStart()
    {
        //���̵� �ٰ��� ȹ��
        ObjectManager.instance.GetItem(1004);

        //�ٰ��� ������ ����
        ObjectManager.instance.RemoveItem(1003);

        //�ý��� �޼��� ���
        DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(521), true);

        yield return null;
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
