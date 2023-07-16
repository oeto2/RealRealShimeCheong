using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWIndowExit : MonoBehaviour
{
    //아이템 창 오브젝트
    public GameObject gameObject_ItemWindow;

    //아이템창 종료
    public void ItemWindowExit()
    {
        //아이템 창 오브젝트 비활성화
        gameObject_ItemWindow.SetActive(false);
    }
}
