using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DargDrobCtrl : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    //조합 슬롯 1의 드롭 영역 스크립트
    public DrobArea drobAreaScr1;

    //조합 슬롯 2의 드롭 영역 스크립트
    public DrobArea drobAreaScr2;

    //오브젝트 매니저
    public ObjectManager objectManagerScr;

    //슬롯의 이미지
    public Image image_combineSlot;

    //오브젝트의 이미지
    public Image image_Object;

    //변경할 슬롯의 색깔
    private Color color_Change = new Color32(135, 135, 135, 255);

    //원래 슬롯의 색깔
    private Color color_Origin = new Color32(255, 255, 255, 255);

    //슬롯의 번호
    public int int_SlotNum;

    //드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        //슬롯의 색깔을 변경함
        image_combineSlot.color = color_Change;
        //오브젝트의 색깔을 변경함
        image_Object.color = color_Change;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    //오브젝트 안에서 드래그 드롭시
    public void OnDrop(PointerEventData eventData)
    {
        
    }

    //드래그 드롭시
    public void OnEndDrag(PointerEventData eventData)
    {
        //슬롯을 원래 색으로 되돌림
        image_combineSlot.color = color_Origin;
        //오브젝트를 원래 색으로 되돌림
        image_Object.color = color_Origin;

        //조합 슬롯 1번에 마우스가 닿아있는 경우
        if(drobAreaScr1.isPointerEnter)
        {
            Debug.Log("1번슬롯에 아이템 넣음");

            //조합 슬롯 1에 아이템 넣기
            objectManagerScr.InputCombinSlot1(int_SlotNum);

            ////슬롯 비활성화
            //gameObject.SetActive(false);
        }

        //조합 슬롯 2번에 마우스가 닿아있는 경우
        else if(drobAreaScr2.isPointerEnter)
        {
            Debug.Log("2번슬롯에 아이템 넣음");
            
            //조합 슬롯 1에 아이템 넣기
            objectManagerScr.InputCombinSlot2(int_SlotNum);
            
            ////슬롯 비활성화
            //gameObject.SetActive(false);
        }
    }
}
