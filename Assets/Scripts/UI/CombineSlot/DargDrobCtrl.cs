using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DargDrobCtrl : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    //���� ���� 1�� ��� ���� ��ũ��Ʈ
    public DrobArea drobAreaScr1;

    //���� ���� 2�� ��� ���� ��ũ��Ʈ
    public DrobArea drobAreaScr2;

    //������Ʈ �Ŵ���
    public ObjectManager objectManagerScr;

    //������ �̹���
    public Image image_combineSlot;

    //������Ʈ�� �̹���
    public Image image_Object;

    //������ ������ ����
    private Color color_Change = new Color32(135, 135, 135, 255);

    //���� ������ ����
    private Color color_Origin = new Color32(255, 255, 255, 255);

    //������ ��ȣ
    public int int_SlotNum;

    //�巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        //������ ������ ������
        image_combineSlot.color = color_Change;
        //������Ʈ�� ������ ������
        image_Object.color = color_Change;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    //������Ʈ �ȿ��� �巡�� ��ӽ�
    public void OnDrop(PointerEventData eventData)
    {
        
    }

    //�巡�� ��ӽ�
    public void OnEndDrag(PointerEventData eventData)
    {
        //������ ���� ������ �ǵ���
        image_combineSlot.color = color_Origin;
        //������Ʈ�� ���� ������ �ǵ���
        image_Object.color = color_Origin;

        //���� ���� 1���� ���콺�� ����ִ� ���
        if(drobAreaScr1.isPointerEnter)
        {
            Debug.Log("1�����Կ� ������ ����");

            //���� ���� 1�� ������ �ֱ�
            objectManagerScr.InputCombinSlot1(int_SlotNum);

            ////���� ��Ȱ��ȭ
            //gameObject.SetActive(false);
        }

        //���� ���� 2���� ���콺�� ����ִ� ���
        else if(drobAreaScr2.isPointerEnter)
        {
            Debug.Log("2�����Կ� ������ ����");
            
            //���� ���� 1�� ������ �ֱ�
            objectManagerScr.InputCombinSlot2(int_SlotNum);
            
            ////���� ��Ȱ��ȭ
            //gameObject.SetActive(false);
        }
    }
}
