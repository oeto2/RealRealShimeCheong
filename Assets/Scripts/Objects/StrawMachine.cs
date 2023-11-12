using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawMachine : MonoBehaviour
{
    //��Ʋ�� �÷��̾ ��Ҵ���
    public bool isTouch = false;

    private void Update()
    {
        //�÷��̾ ��¤�� ������ ���·� ZŰ�� ������ ��� + �������� ������ ���� �ʾƾ���
        if(isTouch && ObjectManager.instance.GetItem_Check(1012) && !ObjectManager.instance.GetItem_Check(1013) && 
            !ObjectManager.instance.GetItem_Check(1014) && Input.GetKeyDown(KeyCode.Z))
        {
            //������ ���� ����
            GameManager.instance.StrawPuzzleStart();
        }
    }

    //�÷��̾�� �������� ���
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    //�÷��̾�� �������� ���
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    //�÷��̾ ����� ���
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }
}
