using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAction : MonoBehaviour
{
    //외부 스크립트 참조
    public ObjectControll objCtrlScr;
    public TutorialManager tutorialManagerScr;
    public Controller playerCtrlScr;

    //상자 이미지들
    public Sprite[] sprite_Box;

    //오브젝트의 이미지
    public SpriteRenderer spriteRender;

    //상자와 닿았는지 확인하는 flag
    public bool isTouch;

    //첫번째 오픈인지 확인하는 flag
    public bool isFirst = true;



    private void Update()
    {
        //TurnOnLights 이벤트 이후만 동작하게 설정
        if(tutorialManagerScr.events != Events.TurnOnLights)
        {
            //상자와 접촉후 Z키를 눌렀을 경우
            if (isTouch && Input.GetKeyDown(KeyCode.Z) && isFirst && !objCtrlScr.getMap)
            {
                //지도가 있는 상자로 변경
                spriteRender.sprite = sprite_Box[2];
                isFirst = false;
            }

            else if (isTouch && Input.GetKeyDown(KeyCode.Z) && !isFirst)
            {
                //상자안의 지도가 들어 있다면
                if (spriteRender.sprite == sprite_Box[2])
                {
                    //열린 이미지로 변경
                    spriteRender.sprite = sprite_Box[1];

                    //지도 획득
                    objCtrlScr.GetMap();
                }

                //상자가 닫혀있는 이미지라면
                if (spriteRender.sprite == sprite_Box[0])
                {
                    //열린 이미지로 변경
                    spriteRender.sprite = sprite_Box[1];
                }

                //상자가 열려있는 이미지라면
                else if (spriteRender.sprite == sprite_Box[1])
                {
                    //닫힌 이미지로 변경
                    spriteRender.sprite = sprite_Box[0];
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObj = collision.gameObject;

        if(gameObj.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject gameObj = collision.gameObject;

        if (gameObj.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }
}
