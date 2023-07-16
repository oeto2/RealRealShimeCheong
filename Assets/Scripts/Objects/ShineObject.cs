using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineObject : MonoBehaviour
{
    //���� ������Ʈ
    public GameObject gameObject_Origin;

    //���� ������Ʈ
    public GameObject gameObject_Shine;

    //������Ʈ�� ���콺�� �ø��� �������
    private void OnMouseEnter()
    {
        if (gameObject.CompareTag("Object"))
        {
            gameObject_Origin.SetActive(false);
            gameObject_Shine.SetActive(true);
        }

    }

    //������Ʈ�� ���콺�� ��� ���
    private void OnMouseExit()
    {
        Debug.Log(gameObject.name + "���");
        gameObject_Origin.SetActive(true);
        gameObject_Shine.SetActive(false);
    }
}
