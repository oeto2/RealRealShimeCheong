using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCtrl : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public UIManager uimangerScr;

    //���콺 Ŀ�� SpriteRender
    public SpriteRenderer spriteRen_Cursor;

    //���콺 �⺻�̹��� 
    public Sprite sprite_idle;
    //���콺 Ŭ�� �̹���
    public Sprite sprite_Click;
    //None �̹���
    public Sprite sprite_None;

    //Ŀ�� x�� ��ġ ����
    [Range(0, 30f)] public float xPos;
    //Ŀ�� y�� ��ġ ����
    [Range(-30f, 0)] public float yPos;

    private void Awake()
    {
        //���콺 �Ⱥ��̱�
        Cursor.visible = false;

        //���콺 Ŀ�� �̹��� ����
        ChangeCursor(sprite_None);
    }

    // Update is called once per frame
    void Update()
    {
        //���콺 Ŀ�� �̹��� ��ġ
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + xPos,Input.mousePosition.y + yPos,Input.mousePosition.z));
        transform.position = cursorPos;

        //���� UI�� �������̶��
        if (uimangerScr.GetUiVisible())
        {
            //Debug.Log("UI������");
            //�⺻ Ŀ�� �Ⱥ��̱�
            Cursor.visible = false;


            //���콺 ��Ŭ�� ��
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("��Ŭ������");
                //Ŀ�� �̹��� ����
                ChangeCursor(sprite_Click);
            }

            //���콺 ��Ŭ���� �����
            else if (Input.GetMouseButtonUp(0))
            {
                //Ŀ�� �̹��� ����
                ChangeCursor(sprite_idle);
            }
        }
        
        //���� UI�� ���������� �ʴٸ�
        else if(!uimangerScr.GetUiVisible())
        {
            //�⺻ Ŀ�� �Ⱥ��̱�
            Cursor.visible = false;
            //Ŀ�� �̹��� ����
            ChangeCursor(sprite_None);
        }
        
    }


    //Ŀ�� �̹��� ����
    public void ChangeCursor(Sprite _cursorImg)
    {
        spriteRen_Cursor.sprite = _cursorImg;
    }
}
