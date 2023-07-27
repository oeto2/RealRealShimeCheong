using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShineObject : MonoBehaviour
{
    //������Ʈ�� SpriteRenderer
    public SpriteRenderer objcetSpriteRender;

    //������Ʈ�� �⺻ �̹���
    public Sprite sprite_Origin;

    //������Ʈ�� ������ �̹���
    public Sprite sprite_Change;

    //������Ʈ�� ���콺�� �ø��� �������
    private void OnMouseEnter()
    {

        //������ �̹����� ����
        objcetSpriteRender.sprite = sprite_Change;

    }

    //������Ʈ�� ���콺�� ��� ���
    private void OnMouseExit()
    {
        //�⺻ �̹����� ����
        objcetSpriteRender.sprite = sprite_Origin;
    }
}
