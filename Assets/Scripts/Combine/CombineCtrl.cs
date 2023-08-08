using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineCtrl : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public UIManager uimanagerScr;

    //������Ʈ�� �����ߴ��� Ȯ���ϴ� flag
    private bool isCloser;

    private void Update()
    {
        //������Ʈ�� �浹���̰� ZŰ�� ���������
        if(isCloser && Input.GetKeyDown(KeyCode.Z))
        {
            //����â ����
            uimanagerScr.CombineWindowLaunch();
        }
    }

    //�浹���� ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���� �浹 Collsion�� Tag�� Player���
        if(collision.CompareTag("Player"))
        {
            isCloser = true;
        }
    }
    //�浹���� ���
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isCloser = true;
        }
    }

    //�浹 ������ ����� ���
    private void OnTriggerExit2D(Collider2D collision)
    {
        isCloser = false;
    }
}