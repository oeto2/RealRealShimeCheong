using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrobArea : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    //마우스 포인터가 닿았는지 확인하는 flag
    public bool isPointerEnter;

    //마우스가 오브젝트와 충돌했는지
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("마우스와 닿았음");

        isPointerEnter = true;
    }

    //마우스가 오브젝트에서 벗어났을 경우
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("마우스 벗어남");

        isPointerEnter = false;
    }
}
