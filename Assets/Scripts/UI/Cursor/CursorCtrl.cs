using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCtrl : MonoBehaviour
{
    //싱글톤
    public static CursorCtrl instance = null;

    //외부 스크립트 참조
    public UIManager uimangerScr;

    //마우스 커서 SpriteRender
    public SpriteRenderer spriteRen_Cursor;

    //커서 불빛 오브젝트
    public GameObject obj_CursorLight;

    //마우스 기본이미지 
    public Sprite sprite_idle;
    //마우스 클릭 이미지
    public Sprite sprite_Click;
    //None 이미지
    public Sprite sprite_None;

    //커서 x축 위치 조절
    [Range(0, 100f)] public float xPos;
    //커서 y축 위치 조절
    [Range(-100f, 0)] public float yPos;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }    
        }

        //마우스 안보이기
        Cursor.visible = false;

        //마우스 커서 이미지 변경
        ChangeCursor(sprite_None);
    }

    // Update is called once per frame
    void Update()
    {
        //마우스 커서 이미지 위치
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + xPos,Input.mousePosition.y + yPos,Input.mousePosition.z));
        transform.position = cursorPos;

        //만약 UI가 실행중이거나 선택지를 진행중이라면
        if (uimangerScr.GetUiVisible() || GameManager.instance.isPlayerSelecting)
        {
            //Debug.Log("UI실행중");
            //기본 커서 안보이기
            Cursor.visible = false;

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
        
        //만약 UI가 실행중이지 않고 선택지를 진행하고 있지 않다면
        else if(!uimangerScr.GetUiVisible() && !GameManager.instance.isPlayerSelecting)
        {
            //기본 커서 안보이기
            Cursor.visible = false;
            //커서 이미지 변경
            ChangeCursor(sprite_None);
        }
    }

    //커서 이미지 변경
    public void ChangeCursor(Sprite _cursorImg)
    {
        spriteRen_Cursor.sprite = _cursorImg;
    }

    //커서 불빛 끄기
    public void OffCursorLight()
    {
        obj_CursorLight.SetActive(false);
    }    

    //커서 불빛 켜기
    public void OnCursorLight()
    {
        obj_CursorLight.SetActive(true);
    }
}
