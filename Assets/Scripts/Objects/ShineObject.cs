using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShineObject : MonoBehaviour
{
    //외부 스크립트 참조
    public TutorialManager tutorialManagerScr;

    //오브젝트의 SpriteRenderer
    public SpriteRenderer objcetSpriteRender;

    //오브젝트의 기본 이미지
    public Sprite sprite_Origin;

    //오브젝트의 빛나는 이미지
    public Sprite sprite_Change;

    //오브젝트에 마우스를 올리고 있을경우
    private void OnMouseEnter()
    {
        if(tutorialManagerScr.setence1End)
        {
            //빛나는 이미지로 변경
            objcetSpriteRender.sprite = sprite_Change;
        }
    }

    //오브젝트에 마우스가 벗어날 경우
    private void OnMouseExit()
    {
        if (tutorialManagerScr.setence1End)
        {
            //기본 이미지로 변경
            objcetSpriteRender.sprite = sprite_Origin;
        }
    }
}
