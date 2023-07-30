using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShineObject : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public TutorialManager tutorialManagerScr;

    //������Ʈ�� SpriteRenderer
    public SpriteRenderer objcetSpriteRender;

    //������Ʈ�� �⺻ �̹���
    public Sprite sprite_Origin;

    //������Ʈ�� ������ �̹���
    public Sprite sprite_Change;

    //������Ʈ�� ���콺�� �ø��� �������
    private void OnMouseEnter()
    {
        if(tutorialManagerScr.setence1End)
        {
            //������ �̹����� ����
            objcetSpriteRender.sprite = sprite_Change;
        }
    }

    //������Ʈ�� ���콺�� ��� ���
    private void OnMouseExit()
    {
        if (tutorialManagerScr.setence1End)
        {
            //�⺻ �̹����� ����
            objcetSpriteRender.sprite = sprite_Origin;
        }
    }
}
