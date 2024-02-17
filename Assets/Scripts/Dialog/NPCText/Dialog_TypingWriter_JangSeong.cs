using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_JangSeong : Dialogue, ITalkable
{
    //�Բ� ����� �� ��� ��ȭ�� �ߴ��� üũ (��ȭ�� True)
    private bool clue8032Talk;

    //��ȭ ���� ����.
    public IEnumerator TextPractice()
    {
        //���� �⸮�� ��ȭ Ʃ�丮�� ���̶��
        if (TutorialManager.instance.events == TutorialEvents.TalkToHyang)
        {
            Debug.Log("�⸮�� Ʃ�丮�� ���");
            yield return StartCoroutine(DialogManager.instance.ItemClueChat("�⸮ �� ����", "�㿩��� ��¾ ���̿�?", true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat("���б�", "��û�̰� �⸮ �쿡 ���� �ʾҴٰ� �Ѵ�.", true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat("���б�", "����� �� ������ �ֺ��� ���ҹ� �� ����.", true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat("���б�", "����ӿ����� �Ϸ�� ���� �ð��� 5���Դϴ�. �Ϸ簡 ������ ���б��� ������ ��ȯ �˴ϴ�."));
            TutorialManager.instance.HyangTalkEnd = true;
        }

        else
        {
            #region �ܼ�
            //2000 : �»���� �����
            if (ObjectManager.instance.GetEquipObjectKey() == 2000)
            {
                //�ܼ�ȹ��
                ObjectManager.instance.GetClue(2001);
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[532].npc_name, dialogdb.NPC_01[532].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[11].npc_name, dialogdb.NPC_01[11].comment));
            }
            //2001 : û���� ������
            else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[24].npc_name, dialogdb.NPC_01[24].comment));
            }
            //2002 : û���� ���
            else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[32].npc_name, dialogdb.NPC_01[32].comment));
            }
            //2003 : û�̿� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[40].npc_name, dialogdb.NPC_01[40].comment));
            }
            //2004 : û�̿� �系
            else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[48].npc_name, dialogdb.NPC_01[48].comment));
            }
            //2005 : �������� �Ƶ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
            {
                //������ �̺�Ʈ Ŭ�����
                if (EventManager.instance.giveStraw)
                {
                    //�⸮�� ��° �Ƶ� �ܼ� ȹ��
                    ObjectManager.instance.GetClue(2013);
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[537].npc_name, dialogdb.NPC_01[537].comment, true));
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[59].npc_name, dialogdb.NPC_01[59].comment));
                }

                //���� ������ �̺�Ʈ�� Ŭ�������� ������ ���
                else
                {
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[537].npc_name, dialogdb.NPC_01[537].comment, true));
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[836].npc_name, dialogdb.NPC_01[836].comment, true));
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[836].npc_name, dialogdb.NPC_01[837].comment));
                }
            }
            //2006 : �۳��� ���ΰ� û��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[67].npc_name, dialogdb.NPC_01[67].comment));
            }
            //2007 : �·��� û��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[80].npc_name, dialogdb.NPC_01[80].comment));
            }
            //2008 : �·��� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[88].npc_name, dialogdb.NPC_01[88].comment));
            }
            //2009 : û���� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[541].npc_name, dialogdb.NPC_01[541].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[96].npc_name, dialogdb.NPC_01[96].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[102].npc_name, dialogdb.NPC_01[102].comment, true));

                //���� ���� �ܼ�ȹ��
                ObjectManager.instance.GetClue(2017);
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[103].npc_name, dialogdb.NPC_01[103].comment));
            }
            //2010 : ����� ��� ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[106].npc_name, dialogdb.NPC_01[106].comment));
            }
            //2011 : ������� ��ó
            else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[115].npc_name, dialogdb.NPC_01[115].comment));
            }
            //2012 : û���� �ŷ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[123].npc_name, dialogdb.NPC_01[123].comment));
            }
            //2013 : �⸮ �� ��° �Ƶ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[131].npc_name, dialogdb.NPC_01[131].comment));
            }
            //2014 : ���������� �� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[139].npc_name, dialogdb.NPC_01[139].comment));
            }
            //2015 : û�̰� �簣 ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[147].npc_name, dialogdb.NPC_01[147].comment));
            }
            //2016 : ¤���� �簣 û��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[157].npc_name, dialogdb.NPC_01[157].comment));
            }
            //2017 : ���� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[167].npc_name, dialogdb.NPC_01[167].comment));
            }
            //2018 : ������ �ܰ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[184].npc_name, dialogdb.NPC_01[184].comment));
            }
            //2019 : ���� �ʴ� ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[192].npc_name, dialogdb.NPC_01[192].comment));
            }
            //2020 : ������� �־��� ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[201].npc_name, dialogdb.NPC_01[201].comment));
            }
            //2021 : ����� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[207].npc_name, dialogdb.NPC_01[207].comment));
            }
            //2022 : ����� ����� ���
            else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[223].npc_name, dialogdb.NPC_01[223].comment));
            }
            //2023 : 3�� ������
            else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[555].npc_name, dialogdb.NPC_01[555].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[231].npc_name, dialogdb.NPC_01[231].comment, true));

                //���� ȭ�� ���̱�
                EndingManager.instance.ShowEndingBG();
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[676].npc_name, dialogdb.NPC_01[676].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[677].npc_name, dialogdb.NPC_01[677].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[678].npc_name, dialogdb.NPC_01[678].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[679].npc_name, dialogdb.NPC_01[679].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[680].npc_name, dialogdb.NPC_01[680].comment, true));

                //Ÿ��Ʋ ȭ�� �̵�
                EndingManager.instance.LoadTitleScene();
            }
            #endregion

            #region ������
            //1000 : ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[285].npc_name, dialogdb.NPC_01[285].comment));
            }
            //1005 : �ָԹ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[294].npc_name, dialogdb.NPC_01[294].comment));
            }
            //1007 : ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[527].npc_name, dialogdb.NPC_01[527].comment, true));

                //�� ������ ����
                ObjectManager.instance.RemoveItem(1007);
                //�� ���� �Ϸ�
                EventManager.instance.eventProgress.deliveryMuck = true;
                //�� ���� �̺�Ʈ �Ϸ�
                EventManager.instance.eventEndCheck.muckEvent_End = true;

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[309].npc_name, dialogdb.NPC_01[309].comment));
            }
            //1009 : ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[324].npc_name, dialogdb.NPC_01[324].comment));
            }
            //1011 : ����� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[340].npc_name, dialogdb.NPC_01[340].comment));
            }
            //1012 : ��¤
            else if (ObjectManager.instance.GetEquipObjectKey() == 1012)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[844].npc_name, dialogdb.NPC_01[844].comment));
            }

            //1013 : ������ 1
            else if (ObjectManager.instance.GetEquipObjectKey() == 1013)
            {
                //������ ������ ����
                ObjectManager.instance.RemoveItem(1013);

                //������ ���޿Ϸ�
                EventManager.instance.giveStraw = true;
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[842].npc_name, dialogdb.NPC_01[842].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[838].npc_name, dialogdb.NPC_01[838].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[840].npc_name, dialogdb.NPC_01[840].comment));
            }

            //1014 : ������ 2
            else if (ObjectManager.instance.GetEquipObjectKey() == 1014)
            {
                //������ ������ ����
                ObjectManager.instance.RemoveItem(1014);

                //������ ���޿Ϸ�
                EventManager.instance.giveStraw = true;
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[842].npc_name, dialogdb.NPC_01[842].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[839].npc_name, dialogdb.NPC_01[839].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[840].npc_name, dialogdb.NPC_01[840].comment));
            }

            #endregion

            #region ���� �ܼ�
            //4023 : ����̸� ���� ���
            else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[351].npc_name, dialogdb.NPC_01[351].comment));
            }
            //4015 : û�̰� ����� ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[359].npc_name, dialogdb.NPC_01[359].comment));
            }
            //4017 : û�̿� ���� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[367].npc_name, dialogdb.NPC_01[367].comment));
            }
            //8032 : �Բ� ����� �� ���
            else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
            {

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[559].npc_name, dialogdb.NPC_01[559].comment, true));

                //ù��° ��ȭ ��
                if (!clue8032Talk)
                {
                    clue8032Talk = true;
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[375].npc_name, dialogdb.NPC_01[375].comment));
                }
                else
                {
                    //���忣��2 ��ȭ����

                    //���� ��� ���̱�
                    EndingManager.instance.ShowEndingBG();
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[414].npc_name, dialogdb.NPC_01[414].comment, true));
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[415].npc_name, dialogdb.NPC_01[415].comment, true));

                    //���忣�� �̹����� ����
                    EndingManager.instance.ChangeToBadEndingBG();
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[416].npc_name, dialogdb.NPC_01[416].comment, true));
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[417].npc_name, dialogdb.NPC_01[417].comment, true));
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[418].npc_name, dialogdb.NPC_01[418].comment, true));

                    //Ÿ��Ʋ�� �̵�
                    EndingManager.instance.LoadTitleScene();
                }

            }
            //4033 : ������ �ߴ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[383].npc_name, dialogdb.NPC_01[383].comment));
            }
            //4018 : û���� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[393].npc_name, dialogdb.NPC_01[393].comment));
            }
            #endregion

            #region �⺻ ���
            else
            {
                Debug.Log("�⺻��� ����");
                yield return StartCoroutine(DialogManager.instance.NormalChat("�⸮ �� ����"));
            }
            #endregion
        }
    }
}