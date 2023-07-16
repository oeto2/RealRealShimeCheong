using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLight : MonoBehaviour
{
    //UI Manager Scripts
    public UIManager script_UIManager;

    //마우스 커서의 포지션 값
    private Vector3 vector3_CursorPos;

    // Update is called once per frame
    void Update()
    {
        //커서 위치 값
        vector3_CursorPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        
        //아이템창과 지도가 켜지지 않은 상태에서만 광원을 마우스 커서로 옮김
        if(!script_UIManager.isItemWindowLaunch && !script_UIManager.isMapWindowLaunch)
        {
            transform.position = vector3_CursorPos;
        }
    }
}
