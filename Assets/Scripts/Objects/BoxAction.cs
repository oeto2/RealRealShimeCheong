using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAction : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public ObjectControll objCtrlScr;
    public TutorialManager tutorialManagerScr;
    public Controller playerCtrlScr;

    //���� �̹�����
    public Sprite[] sprite_Box;

    //������Ʈ�� �̹���
    public SpriteRenderer spriteRender;

    //���ڿ� ��Ҵ��� Ȯ���ϴ� flag
    public bool isTouch;

    //ù��° �������� Ȯ���ϴ� flag
    public bool isFirst = true;



    private void Update()
    {
        //TurnOnLights �̺�Ʈ ���ĸ� �����ϰ� ����
        if(tutorialManagerScr.events != Events.TurnOnLights)
        {
            //���ڿ� ������ ZŰ�� ������ ���
            if (isTouch && Input.GetKeyDown(KeyCode.Z) && isFirst && !objCtrlScr.getMap)
            {
                //������ �ִ� ���ڷ� ����
                spriteRender.sprite = sprite_Box[2];
                isFirst = false;
            }

            else if (isTouch && Input.GetKeyDown(KeyCode.Z) && !isFirst)
            {
                //���ھ��� ������ ��� �ִٸ�
                if (spriteRender.sprite == sprite_Box[2])
                {
                    //���� �̹����� ����
                    spriteRender.sprite = sprite_Box[1];

                    //���� ȹ��
                    objCtrlScr.GetMap();
                }

                //���ڰ� �����ִ� �̹������
                if (spriteRender.sprite == sprite_Box[0])
                {
                    //���� �̹����� ����
                    spriteRender.sprite = sprite_Box[1];
                }

                //���ڰ� �����ִ� �̹������
                else if (spriteRender.sprite == sprite_Box[1])
                {
                    //���� �̹����� ����
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
