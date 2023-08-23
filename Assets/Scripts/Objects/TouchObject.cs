using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchObject : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public TutorialManager tutorialManagerScr;

    //������Ʈ spriteRender
    public SpriteRenderer spriteRen_targetObject;

    //��ġ�� ����
    public Color32 color32_touch = new Color32(130, 130, 130, 255);

    //���콺 ���˽�
    private void OnMouseEnter()
    {
        if (tutorialManagerScr.events != Events.TurnOnLights)
        {
            spriteRen_targetObject.color = color32_touch;
        }
    }

    //���콺 �����
    private void OnMouseExit()
    {
        if (tutorialManagerScr.events != Events.TurnOnLights)
        {
            spriteRen_targetObject.color = new Color32(255, 255, 255, 255);
        }
    }
}
