using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapWindowExit : MonoBehaviour
{
    //지도 오브젝트
    public GameObject gameObject_MapWindow;
    
    //지도 종료 버튼
    public void MapWindowExitButton()
    {
        //지도 오브젝트 비활성화
        gameObject_MapWindow.SetActive(false);
    }
}
