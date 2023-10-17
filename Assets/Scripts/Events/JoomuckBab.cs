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

    //������ �⺻ ������Ʈ
    public GameObject gameObjcet_GamasotNomal;

    //������ ���� ������Ʈ
    public GameObject gameObjcet_GamasotUsing;

    //�����ܰ� �����ߴ��� Ȯ���ϴ� falg
    public bool isTouch;

    //�ָԹ� ���� ����
    public enum MakeJoomuckBab
    {
        //���� �ֱ�
        PushJangJack = 0,
        //�ν˵� ���
        UseFireStone,
        //���� �� ��� ����
        FillWater_OR_PushRice,
        //���� �־��µ� ���� �ȳ���
        FillWaterDone,
        //���� �־��µ� ���� �ȳ���
        PushRiceDone,
        //�ָԹ� �����
        MakeJoomuckBab,
        //�̺�Ʈ ����
        Done
    }

    //���� ���� ����
    public enum MakeHerbOrder
    {
        //�� ���̱�
        LightFire = 0,
        //���� �ֱ�
        PushHerb,
        //�� �ֱ�
        PushWater,
        //���� �� ���� �Ϸ�
        Done,
        //���� �� ���ñ�1,
        DrinkHerb,
        //���� �� ���ñ�2,
        DrinkHerb2,
    }

    //�ָԹ� ����� �̺�Ʈ ����
    public MakeJoomuckBab makeJoomuckBab = MakeJoomuckBab.PushJangJack;

    //���� �� ���� ����
    public MakeHerbOrder makeHerbOrder = MakeHerbOrder.LightFire;

    private void Update()
    {
        //�ָԹ� �̺�Ʈ ���� ����
        if (isTouch && Input.GetKeyDown(KeyCode.Z))
        {
            //�ָԹ� �̺�Ʈ�� Ȱ��ȭ ���̰� ������ �������̶��
            if (EventManager.instance.GetEventBool(Events.JoomuckBab))
            {
                switch (makeJoomuckBab)
                {
                    //���� �ֱ�
                    case MakeJoomuckBab.PushJangJack:
                        StartCoroutine(PushJangJack());
                        break;

                    //�ν˵� ���
                    case MakeJoomuckBab.UseFireStone:
                        StartCoroutine(UseFireStone());
                        break;

                    //�� �ֱ� Ȥ�� �� �ֱ�
                    case MakeJoomuckBab.FillWater_OR_PushRice:
                        StartCoroutine(FillWater_OR_PushRice());
                        break;

                    //���� �־��µ� ���� �ȳ���
                    case MakeJoomuckBab.FillWaterDone:
                        StartCoroutine(FillWaterDone());
                        break;

                    //���� �־��µ� ���� �ȳ���
                    case MakeJoomuckBab.PushRiceDone:
                        StartCoroutine(PushRiceDone());
                        break;

                    //�ָԹ� �����
                    case MakeJoomuckBab.MakeJoomuckBab:
                        StartCoroutine(TakeJoomuckBab());

                        break;
                }
            }

            // ��� �̺�Ʈ (�ָԹ� ����� �̺�Ʈ�� ���� ��쿡�� ����)
            else if (isTouch && Input.GetKeyDown(KeyCode.Z) && makeJoomuckBab == MakeJoomuckBab.Done)
            {
                //��� �̺�Ʈ ����
                switch (makeHerbOrder)
                {
                    //�� ���̱�
                    case MakeHerbOrder.LightFire:

                        //�ν˵��� �����ϰ� ���� ���
                        if (ObjectManager.instance.GetEquipObjectKey() == 1002)
                        {
                            //�� ���̱� ��� ����
                            DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(520), true);

                            //�� ������Ʈ Ȱ��ȭ
                            gameobjcet_Fire.SetActive(true);

                            //���� ������ ����
                            makeHerbOrder = MakeHerbOrder.PushHerb;
                        }
                        break;

                    //��� �ֱ�
                    case MakeHerbOrder.PushHerb:

                        //��긦 �����ϰ� ���� ���
                        if (ObjectManager.instance.GetEquipObjectKey() == 1010)
                        {
                            //��� �ֱ� ��� ����
                            DialogManager.instance.StartPushHerbSentence();

                            //���� ������ ����
                            makeHerbOrder = MakeHerbOrder.PushWater;
                        }
                        break;

                    //�� �ֱ�
                    case MakeHerbOrder.PushWater:

                        //�� �ٰ��� �����ϰ� ���� ���
                        if (ObjectManager.instance.GetEquipObjectKey() == 7112)
                        {
                            //�� �ֱ� ���̾�α� ���
                            DialogManager.instance.Start_WaterBageSentence_2();

                            //���� ������ ����
                            makeHerbOrder = MakeHerbOrder.Done;
                        }
                        break;
                }
            }
        }
    }
    #region ���� �̺�Ʈ




    #endregion

    #region �ָԹ� �̺�Ʈ
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

        //�ν˵��� ���� ���̶��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1002)
        {
            //�ý��� �޼��� ���
            DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(265), true);
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
            makeJoomuckBab = MakeJoomuckBab.FillWater_OR_PushRice;
        }
        yield return null;
    }

    //�� Ȥ�� �� �ֱ�
    private IEnumerator FillWater_OR_PushRice()
    {
        //���� �ٰ����� �������̶��
        if (ObjectManager.instance.GetEquipObjectKey() == 1004)
        {
            //�ý��� �޼��� ���
            DialogManager.instance.Start_WaterBageSentence();

            //DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(282), true);

            //���� �ٰ��� ������ ����
            ObjectManager.instance.RemoveItem(1004);

            //�ٰ��� ������ ȹ��
            ObjectManager.instance.GetItem(1003);

            //���� �̺�Ʈ�� �̵�
            makeJoomuckBab = MakeJoomuckBab.FillWaterDone;
        }

        //���� �������̶��
        if (objectManagerScr.GetEquipObjectKey() == 1000)
        {
            //�ý��� �޼��� ���
            DialogManager.instance.Start_RiceSentence();

            //�� ������ ����
            ObjectManager.instance.RemoveItem(1000);

            //���� �̺�Ʈ�� �̵�
            makeJoomuckBab = MakeJoomuckBab.PushRiceDone;
        }

        yield return null;
    }

    //���� �־��µ� ���� �� ����
    private IEnumerator FillWaterDone()
    {
        //���� ���� ���̶��
        if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            //�ý��� �޼��� ���
            DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(523), true);

            //�� ������ ����
            ObjectManager.instance.RemoveItem(1000);

            //������ ��� ����
            UsingGamasot();

            //�ý��� �޼����� ���������� ���Ѵ��
            while (true)
            {
                if (DialogManager.instance.IsSystemMessageEnd() == true)
                {
                    yield return new WaitForSeconds(0.2f);

                    break;
                }

                yield return null;
            }
        }
        yield return null;
    }

    //���� �־��µ� ���� �ȳ���
    private IEnumerator PushRiceDone()
    {
        //���̵� �ٰ����� ���� ���̶��
        if (objectManagerScr.GetEquipObjectKey() == 1004)
        {
            //�ý��� �޼��� ���
            DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(522), true);

            //���̵� �ٰ��� ������ ����
            ObjectManager.instance.RemoveItem(1004);

            //�ٰ��� ������ ȹ��
            ObjectManager.instance.GetItem(1003);

            //������ ��� ����
            UsingGamasot();

            //���� �̺�Ʈ�� �̵�
            makeJoomuckBab = MakeJoomuckBab.MakeJoomuckBab;

            //�ý��� �޼����� ���������� ���Ѵ��
            while (true)
            {
                if (DialogManager.instance.IsSystemMessageEnd() == true)
                {
                    Debug.Log("���� �̺�Ʈ �̵�");
                    yield return new WaitForSeconds(0.2f);
                    break;
                }

                yield return null;
            }
        }
        yield return null;
    }

    //�ָԹ� ì���
    private IEnumerator TakeJoomuckBab()
    {
        if (DialogManager.instance.IsSystemMessageEnd() == true)
        {
            //�ý��� �޼��� ���
            DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(524), true);

            //�ָԹ� ȹ��
            ObjectManager.instance.GetItem(1005);

            //������ ��� ����
            UsingGamasotEnd();

            //���� �̺�Ʈ �̵�
            makeJoomuckBab = MakeJoomuckBab.Done;
        }

        yield return null;
    }

    //������Ʈ ���˽�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    //������Ʈ ���˿��� ��������
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }

    //������ ��� ����
    public void UsingGamasot()
    {
        //�⺻ ������ ������Ʈ ��Ȱ��ȭ
        gameObjcet_GamasotNomal.SetActive(false);

        //���� ������ ������Ʈ Ȱ��ȭ
        gameObjcet_GamasotUsing.SetActive(true);
    }

    //������ ��� ��
    public void UsingGamasotEnd()
    {
        //�⺻ ������ ������Ʈ ��Ȱ��ȭ
        gameObjcet_GamasotNomal.SetActive(true);

        //�� ������Ʈ ��Ȱ��ȭ
        gameobjcet_Fire.SetActive(false);

        //���� ������ ������Ʈ Ȱ��ȭ
        gameObjcet_GamasotUsing.SetActive(false);
    }

    //�ָԹ� �̺�Ʈ ���� int �� ��ȯ
    public int GetEventState()
    {
        switch (makeJoomuckBab)
        {
            case MakeJoomuckBab.PushJangJack:
                return 0;

            case MakeJoomuckBab.UseFireStone:
                return 1;

            case MakeJoomuckBab.FillWater_OR_PushRice:
                return 2;

            case MakeJoomuckBab.FillWaterDone:
                return 3;

            case MakeJoomuckBab.PushRiceDone:
                return 4;

            case MakeJoomuckBab.MakeJoomuckBab:
                return 5;

            case MakeJoomuckBab.Done:
                return 6;

            default:
                return 0;

        }
    }

    //�ָԹ� �̺�Ʈ ����
    public void EventSetting(int _eventNum)
    {
        switch (_eventNum)
        {
            //�ƹ��͵� ���� ����
            case 0:

                //�̺�Ʈ �����Ȳ ����
                makeJoomuckBab = MakeJoomuckBab.PushJangJack;

                //���� ���ֱ�
                gameObject_JangJack.SetActive(false);

                //�� ���ֱ�
                gameobjcet_Fire.SetActive(false);

                //������ ��� �̹��� ����
                gameObjcet_GamasotUsing.SetActive(false);

                //������ �⺻ �̹��� ���̱�
                gameObjcet_GamasotNomal.SetActive(true);

                break;

            //���� ���� ����
            case 1:

                //�̺�Ʈ �����Ȳ ����
                makeJoomuckBab = MakeJoomuckBab.UseFireStone;

                //���� ���̱�
                gameObject_JangJack.SetActive(true);

                //�� ���ֱ�
                gameobjcet_Fire.SetActive(false);

                //������ ��� �̹��� ����
                gameObjcet_GamasotUsing.SetActive(false);

                //������ �⺻ �̹��� ���̱�
                gameObjcet_GamasotNomal.SetActive(true);

                break;

            //���� ���� ����
            case 2:

                //�̺�Ʈ �����Ȳ ����
                makeJoomuckBab = MakeJoomuckBab.FillWater_OR_PushRice;

                //���� ���̱�
                gameObject_JangJack.SetActive(true);

                //�� ���̱�
                gameobjcet_Fire.SetActive(true);

                //������ ��� �̹��� ����
                gameObjcet_GamasotUsing.SetActive(false);

                //������ �⺻ �̹��� ���̱�
                gameObjcet_GamasotNomal.SetActive(true);

                break;

            //���� ���� ����
            case 3:

                //�̺�Ʈ �����Ȳ ����
                makeJoomuckBab = MakeJoomuckBab.FillWaterDone;

                //���� ���̱�
                gameObject_JangJack.SetActive(true);

                //�� ���̱�
                gameobjcet_Fire.SetActive(true);

                //������ ��� �̹��� ����
                gameObjcet_GamasotUsing.SetActive(false);

                //������ �⺻ �̹��� ���̱�
                gameObjcet_GamasotNomal.SetActive(true);

                break;

            //���� ���� ����
            case 4:

                //�̺�Ʈ �����Ȳ ����
                makeJoomuckBab = MakeJoomuckBab.PushRiceDone;

                //���� ���̱�
                gameObject_JangJack.SetActive(true);

                //�� ���̱�
                gameobjcet_Fire.SetActive(true);

                //������ ��� �̹��� ����
                gameObjcet_GamasotUsing.SetActive(false);

                //������ �⺻ �̹��� ���̱�
                gameObjcet_GamasotNomal.SetActive(true);

                break;

            //�Ұ� ���� ���� ����
            case 5:

                //�̺�Ʈ �����Ȳ ����
                makeJoomuckBab = MakeJoomuckBab.MakeJoomuckBab;

                //���� ���̱�
                gameObject_JangJack.SetActive(true);

                //�� ���̱�
                gameobjcet_Fire.SetActive(true);

                //������ ��� �̹��� ���̱�
                gameObjcet_GamasotUsing.SetActive(true);

                //������ �⺻ �̹��� ���̱�
                gameObjcet_GamasotNomal.SetActive(true);

                break;

            //�̺�Ʈ ��
            case 6:

                //�̺�Ʈ �����Ȳ ����
                makeJoomuckBab = MakeJoomuckBab.Done;

                //���� ���̱�
                gameObject_JangJack.SetActive(true);

                //�� ����
                gameobjcet_Fire.SetActive(false);

                //������ ��� �̹��� ����
                gameObjcet_GamasotUsing.SetActive(false);

                //������ �⺻ �̹��� ���̱�
                gameObjcet_GamasotNomal.SetActive(true);

                break;
        }
    }
    #endregion
}
