using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCursor : MonoBehaviour
{
    //마우스 커서 SpriteRender
    public SpriteRenderer spriteRen_Cursor;

    //마우스 기본이미지 
    public Sprite sprite_idle;
    //마우스 클릭 이미지
    public Sprite sprite_Click;
    //None 이미지
    public Sprite sprite_None;

    //커서 x축 위치 조절
    [Range(0, 30f)] public float xPos;
    //커서 y축 위치 조절
    [Range(-30f, 0)] public float yPos;

    //타이틀 메인 카메라
    public Camera mainCamera;

    private void Awake()
    {
        //마우스 안보이기
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        //마우스 커서 이미지 위치
        Vector2 cursorPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + xPos, Input.mousePosition.y + yPos, Input.mousePosition.z));
        this.transform.position = cursorPos;

        //Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + xPos, Input.mousePosition.y + yPos, Input.mousePosition.z)));
        //마우스 좌클릭 시
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("좌클릭했음");
            //커서 이미지 변경
            ChangeCursor(sprite_Click);
        }

        //마우스 좌클릭을 땔경우
        else if (Input.GetMouseButtonUp(0))
        {
            //커서 이미지 변경
            ChangeCursor(sprite_idle);
        }

    }


    //커서 이미지 변경
    public void ChangeCursor(Sprite _cursorImg)
    {
        spriteRen_Cursor.sprite = _cursorImg;
    }
}
