using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_BusinessMan : Dialogue, ITalkable
{
    //�ܺ� ��ũ��Ʈ ����
    public BeadMove beadmoveScr;

    //���� ���� ����
    IEnumerator BeadPuzzlePlay()
    {
        //���̾�α� â ����
        DialogueCanvas.SetActive(false);

        //���� ���� ����
        GameManager.instance.PlayBeadPuzzle();

        //���� Ŭ���� ������ ���� ���
        while (true)
        {
            if (beadmoveScr.isClear)
            {
                break;
            }
            yield return null;
        }
    }

    public IEnumerator TextPractice()
    {
        //���� �� ������ �Ϸ� �ߴٸ�
        if (EventManager.instance.eventEndCheck.muckEvent_End == true)
        {
            //�̺�Ʈ �Ϸ� ����
            EventManager.instance.eventEndCheck.muckEvent_End = false;
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[153].npc_name, dialogdb.NPC_01[153].comment, true));

            //¤���� �簣 û�� �ܼ� ȹ��
            ObjectManager.instance.GetClue(2016);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[154].npc_name, dialogdb.NPC_01[154].comment));
        }

        else
        {
            #region �ܼ�
            //2001 : û���� ������
            if (ObjectManager.instance.GetEquipObjectKey() == 2001)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[25].npc_name, dialogdb.NPC_01[25].comment));
            }

            //2002 : û���� ���
            else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[33].npc_name, dialogdb.NPC_01[33].comment));
            }

            //2003 : û�̿� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
            {
                //û�̰� �簣�� ȹ��
                ObjectManager.instance.GetClue(2015);

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[535].npc_name, dialogdb.NPC_01[535].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[41].npc_name, dialogdb.NPC_01[41].comment));
            }

            //2005 : �������� �Ƶ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[60].npc_name, dialogdb.NPC_01[60].comment));
            }

            //2006 : �۳��� ���ΰ� û��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[68].npc_name, dialogdb.NPC_01[68].comment));
            }

            //2007 : �·��� û��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[81].npc_name, dialogdb.NPC_01[81].comment));
            }

            //2010 : ����� ��鼮
            else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[107].npc_name, dialogdb.NPC_01[107].comment));
            }

            //2011 : ������� ��ó
            else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[116].npc_name, dialogdb.NPC_01[116].comment));
            }

            //2012 : û���� �ŷ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[124].npc_name, dialogdb.NPC_01[124].comment));
            }

            //2013 : �⸮�� ��°�Ƶ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
            {
                //������ �ܰ� �ܼ� ȹ��
                ObjectManager.instance.GetClue(2018);

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[545].npc_name, dialogdb.NPC_01[545].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[132].npc_name, dialogdb.NPC_01[132].comment));
            }

            //2014 : ������������ ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[140].npc_name, dialogdb.NPC_01[140].comment));
            }

            //2015 : û�̰� �簣 �� (���� ��� �߰�?)
            else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[547].npc_name, dialogdb.NPC_01[547].comment, true));

                //���� �� ������ ��������
                if (EventManager.instance.eventProgress.deliveryMuck != true)
                {
                    //�� ���� �̺�Ʈ ����
                    EventManager.instance.EventActive(Events.muck);

                    //�� ȹ��
                    ObjectManager.instance.GetItem(1007);

                }

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[148].npc_name, dialogdb.NPC_01[148].comment));
            }

            //2016 : ¤���� �簣 û��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[158].npc_name, dialogdb.NPC_01[158].comment));
            }

            //2017 : ���� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[168].npc_name, dialogdb.NPC_01[168].comment));
            }

            //2018 : ������ �ܰ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[185].npc_name, dialogdb.NPC_01[185].comment));
            }

            //2019 : �����ʴ� ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[193].npc_name, dialogdb.NPC_01[193].comment));
            }

            //2020 : ������� �־��� ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[202].npc_name, dialogdb.NPC_01[202].comment));
            }

            //2021 : ����� ���� (���� ����? ��� �߰�)
            else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[553].npc_name, dialogdb.NPC_01[553].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[210].npc_name, dialogdb.NPC_01[210].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[215].npc_name, dialogdb.NPC_01[215].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[216].npc_name, dialogdb.NPC_01[216].comment, true));

                //���� ����
                yield return StartCoroutine(BeadPuzzlePlay());
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[217].npc_name, dialogdb.NPC_01[217].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[218].npc_name, dialogdb.NPC_01[218].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[219].npc_name, dialogdb.NPC_01[219].comment, true));
                //�� ȹ��
                ObjectManager.instance.GetItem(1011);
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[220].npc_name, dialogdb.NPC_01[220].comment));

            }

            //2023 : 3��������
            else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[232].npc_name, dialogdb.NPC_01[232].comment));
            }
            #endregion

            #region ������
            //1000 : ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[286].npc_name, dialogdb.NPC_01[286].comment));
            }

            //1005 : �ָԹ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[295].npc_name, dialogdb.NPC_01[295].comment));
            }

            //1006 : ��� (�̺�Ʈ?)
            else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[526].npc_name, dialogdb.NPC_01[526].comment, true));

                //���忣��1 �����
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[302].npc_name, dialogdb.NPC_01[302].comment, true));

                //��� �̹��� ����
                EndingManager.instance.ShowEndingBG();
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[410].npc_name, dialogdb.NPC_01[410].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[411].npc_name, dialogdb.NPC_01[411].comment, true));

                //���忣�� �÷��� ����
                EndingManager.instance.ChangeToBadEndingBG();
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[412].npc_name, dialogdb.NPC_01[412].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[413].npc_name, dialogdb.NPC_01[413].comment, true));
                //Ÿ��Ʋ �̵�
                EndingManager.instance.LoadTitleScene();

            }

            //1007 : ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[310].npc_name, dialogdb.NPC_01[310].comment));
            }

            //1009 : ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[325].npc_name, dialogdb.NPC_01[325].comment));
            }

            //1011 : ����� ���� (���Ĵ�� �߰�)
            else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[341].npc_name, dialogdb.NPC_01[341].comment));
            }

            //1013 : ������1
            else if (ObjectManager.instance.GetEquipObjectKey() == 1013)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[854].npc_name, dialogdb.NPC_01[854].comment));
            }
            //1014 : ������2
            else if (ObjectManager.instance.GetEquipObjectKey() == 1014)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[864].npc_name, dialogdb.NPC_01[864].comment));
            }
            #endregion

            #region ���� �ܼ�
            //4023 : ����̸� ���� ���
            else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[352].npc_name, dialogdb.NPC_01[352].comment));
            }

            //8032 : ����� �λ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[376].npc_name, dialogdb.NPC_01[376].comment));
            }

            //4033 : ������ �ߴ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[560].npc_name, dialogdb.NPC_01[560].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[384].npc_name, dialogdb.NPC_01[384].comment));
            }

            //4018 : û���� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[394].npc_name, dialogdb.NPC_01[394].comment));
            }
            #endregion

            //�⺻ ���
            else
            {
                yield return StartCoroutine(DialogManager.instance.NormalChat("����"));
            }
        }
    }
}