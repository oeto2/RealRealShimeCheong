using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrobArea : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    //���콺 �����Ͱ� ��Ҵ��� Ȯ���ϴ� flag
    public bool isPointerEnter;

    //���콺�� ������Ʈ�� �浹�ߴ���
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("���콺�� �����");

        isPointerEnter = true;
    }

    //���콺�� ������Ʈ���� ����� ���
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("���콺 ���");

        isPointerEnter = false;
    }
}
