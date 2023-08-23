using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchObject : MonoBehaviour
{
    //외부 스크립트 참조
    public TutorialManager tutorialManagerScr;

    //오브젝트 spriteRender
    public SpriteRenderer spriteRen_targetObject;

    //터치후 색깔
    public Color32 color32_touch = new Color32(130, 130, 130, 255);

    //마우스 접촉시
    private void OnMouseEnter()
    {
        if (tutorialManagerScr.events != Events.TurnOnLights)
        {
            spriteRen_targetObject.color = color32_touch;
        }
    }

    //마우스 벗어난뒤
    private void OnMouseExit()
    {
        if (tutorialManagerScr.events != Events.TurnOnLights)
        {
            spriteRen_targetObject.color = new Color32(255, 255, 255, 255);
        }
    }
}
