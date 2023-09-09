using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoomuckBab : MonoBehaviour
{

    //�ܺν�ũ��Ʈ ����
    public ObjectManager objectManagerScr;

    //���� ������Ʈ
    public GameObject gameObject_JangJack;

    //�� ������Ʈ
    public GameObject gameobjcet_Fire;

    //�����ܰ� �����ߴ��� Ȯ���ϴ� falg
    public bool isTouch;

    //�ָԹ� ���� ����
    public enum MakeJoomuckBab
    {
        //���� �ֱ�
        PushJangJack = 0,
        //�ν˵� ���
        UseFireStone,
        //�� �ֱ�
        FillWater,
        //�� �ֱ�
        PushRice,
        //�ָԹ� �����
        MakeJoomuckBab
    }

    //�ָԹ� ����� �̺�Ʈ ����
    public MakeJoomuckBab makeJoomuckBab = MakeJoomuckBab.PushJangJack;

    private void Update()
    {
        if (isTouch && Input.GetKeyDown(KeyCode.Z))
        {
            //�ָԹ� �̺�Ʈ�� Ȱ��ȭ ���̰� ������ �������̶��
            if(EventManager.instance.GetEventBool(Events.JoomuckBab))
            {
                switch(makeJoomuckBab)
                {
                    //���� �����
                    case MakeJoomuckBab.PushJangJack:
                        StartCoroutine(PushJangJack());
                        break;

                    //�ν˵� ���
                    case MakeJoomuckBab.UseFireStone:
                        StartCoroutine(UseFireStone());
                        break;
                }
            }
        }
    }   

    //���� �ֱ�
    private IEnumerator PushJangJack()
    {
        //���� �������� �������̶��
        if (ObjectManager.instance.GetEquipObjectKey() == 1001)
        {
            //�ý��� �޼��� ���
            DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(519), true);

            //���� ������Ʈ Ȱ��ȭ
            gameObject_JangJack.SetActive(true);

            //���� ������ ����
            objectManagerScr.RemoveItem(1001);

            //���� �̺�Ʈ�� �̵�
            makeJoomuckBab = MakeJoomuckBab.UseFireStone;
        }
        yield return null;
    }
    
    //�ν˵� ���
    private IEnumerator UseFireStone()
    {
        //�ν˵��� �������̶��
        if (ObjectManager.instance.GetEquipObjectKey() == 1002)
        {
            //�� ������Ʈ Ȱ��ȭ
            gameobjcet_Fire.SetActive(true);

            //�ý��� �޼��� ���
            DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(520), true);

            //���� �̺�Ʈ�� �̵�
            makeJoomuckBab = MakeJoomuckBab.FillWater;
        }
        yield return null;
    }


    //������Ʈ ���˽�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    //������Ʈ ���˿��� ��������
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }
}
