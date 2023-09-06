using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedding : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public TimeManager timeManagerScr;
    public TutorialManager tutorialManagerScr;

    //Sleep Ui ������Ʈ
    public GameObject gameObjcet_SleepUI;

    //Sleep BackGround Object
    public GameObject gameObject_SleepBG;

    //�÷��̾ ������Ʈ�� ��Ҵ��� Ȯ���ϴ� flag
    public bool isTouch;
    
    // Update is called once per frame
    void Update()
    {
        //���࿡ �÷��̾ ������Ʈ�� ������ zŰ�� ������ ��� (�̺�Ʈ�� ���� �� ���� ��쿡�� ����)
        if(isTouch && Input.GetKeyDown(KeyCode.Z) && tutorialManagerScr.events == TutorialEvents.Done)
        {
            //Sleep UI �����ֱ�
            gameObjcet_SleepUI.SetActive(true);
        }
    }

    //������Ʈ�� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject Obj = collision.gameObject;

        //�浹�� ������Ʈ�� Player�� ���
        if (Obj.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    //������Ʈ�� ������
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject Obj = collision.gameObject;

        //�浹�� ������Ʈ�� Player�� ���
        if (Obj.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    //������Ʈ�� �浹�������� ���
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }

    //�� ��ư�� ������ ���
    public void YesButton()
    {
        //�Ϸ� ������
        timeManagerScr.PassOneDay();
        //Sleep UI ����
        gameObjcet_SleepUI.SetActive(false);
        //Sleep BackRound Ani Start
        ShowSleepBG();
        //1�ʵڿ� ������Ʈ ��Ȱ��ȭ
        Invoke("CloseSleepBG", 2f);
    }

    //�ƴϿ� ��ư�� ������ ���
    public void NoButton()
    {
        //Sleep UI ����
        gameObjcet_SleepUI.SetActive(false);
    }

    public void ShowSleepBG()
    {
        gameObject_SleepBG.SetActive(true);
    }

    public void CloseSleepBG()
    {
        gameObject_SleepBG.SetActive(false);
    }
}
