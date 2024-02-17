using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Guiduck : Dialogue, ITalkable
{
    public IEnumerator TextPractice()
    {
        //���� 3��15�� ��� �̺�Ʈ ���̶��
        if (EventManager.instance.eventProgress.day15ClueStart && !EventManager.instance.eventEndCheck.day15ClueGet)

        {
            //�ָ� ���� �Ϸ� �� �� ������ �Ϸ� ���� ��
            if (EventManager.instance.eventProgress.joomackPuzzle_Clear && EventManager.instance.eventProgress.giveFlowerEnd &&
                EventManager.instance.eventEndCheck.giveBoridduck_End)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[180].npc_name, dialogdb.NPC_01[180].comment, true));

                //3�� ������ �ܼ� ȹ��
                ObjectManager.instance.GetClue(2023);
                //3�� ������ �ܼ� ȹ�� �̺�Ʈ ����
                EventManager.instance.eventEndCheck.day15ClueGet = true;
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[181].npc_name, dialogdb.NPC_01[181].comment));
            }

            //���� ���� �ܼ��� ��ȭ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
            {
                ////�ָ����� ����
                //GameManager.instance.JoomackPuzzleStart();
                Debug.Log("���� ���� ��� ����");

                //�ָ� ������ Ŭ���� �ߴٸ�
                if (EventManager.instance.eventProgress.joomackPuzzle_Clear)
                {
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[171].npc_name, dialogdb.NPC_01[171].comment, true));

                    //������ ȹ��
                    ObjectManager.instance.GetItem(1008);
                    //������ ���� �̺�Ʈ ����
                    EventManager.instance.EventActive(Events.boridduck);
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[172].npc_name, dialogdb.NPC_01[172].comment));
                }
                else
                {
                    //�ָ� ������ Ŭ�������� ���ߴٸ�
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[164].npc_name, dialogdb.NPC_01[164].comment, true));

                    //�ָ� ���� ����
                    GameManager.instance.JoomackPuzzleStart();
                }
            }

            //�ָ� ���� �̿Ϸ� �� �� ������ �Ϸ� ���� ���
            else
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[178].npc_name, dialogdb.NPC_01[178].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[179].npc_name, dialogdb.NPC_01[179].comment));
            }
        }

        //�̺�Ʈ ���� �ƴ϶��
        else
        {
            #region �ܼ�
            //2000 : �»���� �����
            if (ObjectManager.instance.GetEquipObjectKey() == 2000)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[10].npc_name, dialogdb.NPC_01[10].comment));
            }
            //2001 : û���� ������
            else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[23].npc_name, dialogdb.NPC_01[23].comment));
            }
            //2002 : û���� ���
            else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
            {
                //û�̿� ���� ȹ��
                ObjectManager.instance.GetClue(2003);
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[534].npc_name, dialogdb.NPC_01[534].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[31].npc_name, dialogdb.NPC_01[31].comment));
            }
            //2003 : û�̿� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[39].npc_name, dialogdb.NPC_01[39].comment));
            }
            //2004 : û�̿� �系
            else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
            {
                //�·��� û��
                ObjectManager.instance.GetClue(2007);
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[536].npc_name, dialogdb.NPC_01[536].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[47].npc_name, dialogdb.NPC_01[47].comment));
            }
            //2005 : �������� �Ƶ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[58].npc_name, dialogdb.NPC_01[58].comment));
            }
            //2006 : �۳��� ���ΰ� û��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[66].npc_name, dialogdb.NPC_01[66].comment));
            }
            //2007 : �·��� û��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[79].npc_name, dialogdb.NPC_01[79].comment));
            }
            //2008 : �·��� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[87].npc_name, dialogdb.NPC_01[87].comment));
            }
            //2009 : û���� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[95].npc_name, dialogdb.NPC_01[95].comment));
            }
            //2010 : ����� ��� ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
            {
                //������� ��ó �ܼ� ȹ��
                ObjectManager.instance.GetClue(2011);

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[533].npc_name, dialogdb.NPC_01[533].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[105].npc_name, dialogdb.NPC_01[105].comment));
            }
            //2011 : ������� ��ó
            else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[114].npc_name, dialogdb.NPC_01[114].comment));
            }
            //2012 : û���� �ŷ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[122].npc_name, dialogdb.NPC_01[122].comment));
            }
            //2014 : ���������� �� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[138].npc_name, dialogdb.NPC_01[138].comment));
            }
            //2015 : û�̰� �簣 ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[146].npc_name, dialogdb.NPC_01[146].comment));
            }
            //2016 : ¤���� �簣 û��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[156].npc_name, dialogdb.NPC_01[156].comment));
            }
            //2017 : ���� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
            {
                ////�ָ����� ����
                //GameManager.instance.JoomackPuzzleStart();
                Debug.Log("���� ���� ��� ����");

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[549].npc_name, dialogdb.NPC_01[549].comment, true));

                //�ָ� ������ Ŭ���� �ߴٸ�
                if (EventManager.instance.eventProgress.joomackPuzzle_Clear)
                {
                    //yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[549].npc_name, dialogdb.NPC_01[549].comment, true));
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[171].npc_name, dialogdb.NPC_01[171].comment, true));

                    //������ ȹ��
                    ObjectManager.instance.GetItem(1008);
                    //������ ���� �̺�Ʈ ����
                    EventManager.instance.EventActive(Events.boridduck);
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[172].npc_name, dialogdb.NPC_01[172].comment));
                }

                else
                {
                    //�ָ� ������ Ŭ�������� ���ߴٸ�
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[164].npc_name, dialogdb.NPC_01[164].comment, true));

                    //�ָ� ���� ����
                    GameManager.instance.JoomackPuzzleStart();
                }
            }
            //2018 : ������ �ܰ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[183].npc_name, dialogdb.NPC_01[183].comment));
            }
            //2019 : ���� �ʴ� ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[191].npc_name, dialogdb.NPC_01[191].comment));
            }
            //2021 : ����� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[208].npc_name, dialogdb.NPC_01[208].comment));
            }
            //2023 : 3�� ������
            else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[230].npc_name, dialogdb.NPC_01[230].comment));
            }
            #endregion

            #region ������
            //1000 : ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[284].npc_name, dialogdb.NPC_01[284].comment));
            }
            //1001 : ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 1001)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[250].npc_name, dialogdb.NPC_01[250].comment));
            }
            //1002 : �νÿ� �ν˵�
            else if (ObjectManager.instance.GetEquipObjectKey() == 1002)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[258].npc_name, dialogdb.NPC_01[258].comment));
            }
            //1003 : �ٰ���
            else if (ObjectManager.instance.GetEquipObjectKey() == 1003)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[267].npc_name, dialogdb.NPC_01[267].comment));
            }
            //1005 : �ָԹ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[293].npc_name, dialogdb.NPC_01[293].comment));
            }
            //1006 : ���
            else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[300].npc_name, dialogdb.NPC_01[300].comment));
            }
            //1007 : ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[308].npc_name, dialogdb.NPC_01[308].comment));
            }
            //1008 : ������
            else if (ObjectManager.instance.GetEquipObjectKey() == 1008)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[316].npc_name, dialogdb.NPC_01[316].comment));
            }
            //1009 : ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
            {
                //�� ����������
                ObjectManager.instance.RemoveItem(1009);

                //�� ���� �Ϸ�
                EventManager.instance.eventProgress.giveFlowerEnd = true;

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[529].npc_name, dialogdb.NPC_01[529].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[175].npc_name, dialogdb.NPC_01[175].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[176].npc_name, dialogdb.NPC_01[176].comment, true));

                //���� ������ ȹ��
                ObjectManager.instance.GetItem(1010);
                //3�� ������ ��ȭ �̺�Ʈ ����
                EventManager.instance.eventProgress.day15ClueStart = true;
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[177].npc_name, dialogdb.NPC_01[177].comment));
            }
            //1011 : ����� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[338].npc_name, dialogdb.NPC_01[338].comment));
            }
            //1012 : ��¤
            else if (ObjectManager.instance.GetEquipObjectKey() == 1012)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[846].npc_name, dialogdb.NPC_01[846].comment));
            }
            //1013 : ������1
            else if (ObjectManager.instance.GetEquipObjectKey() == 1013)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[854].npc_name, dialogdb.NPC_01[854].comment));
            }
            //1014 : ������2
            else if (ObjectManager.instance.GetEquipObjectKey() == 1014)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[862].npc_name, dialogdb.NPC_01[862].comment));
            }
            #endregion

            #region ���� �ܼ�
            //4023 : ����̸� ���� ���
            else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[350].npc_name, dialogdb.NPC_01[350].comment));
            }
            //4015 : û�̰� ����� ��
            else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
            {
                //������� �־����� �ܼ� ȹ��
                ObjectManager.instance.GetClue(2020);
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[557].npc_name, dialogdb.NPC_01[557].comment));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[358].npc_name, dialogdb.NPC_01[358].comment));
            }
            //4017 : û�̿� ���� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[366].npc_name, dialogdb.NPC_01[366].comment));
            }
            //8032 : �Բ� ����� �� ���
            else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[374].npc_name, dialogdb.NPC_01[374].comment));
            }
            //4033 : ������ �ߴ�
            else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[382].npc_name, dialogdb.NPC_01[382].comment));
            }
            //4018 : û���� ����
            else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[392].npc_name, dialogdb.NPC_01[392].comment));
            }
            #endregion

            #region �⺻ ���
            else
            {
                yield return StartCoroutine(DialogManager.instance.NormalChat("�ʹ� ���"));
            }
            #endregion
        }
    }

    //���̾�α� ��ȭ ����
    public void StartDialogSentence()
    {
        StartCoroutine(TextPractice());
    }

    #region PreviousCode
    //IEnumerator NormalChat()
    //{
    //    //��ȭ �ߺ����� ����
    //    remainSentence = true;

    //    //�ʻ�ȭ ����
    //    GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];

    //    string narrator = characternameText = CharacterName.text = dialogdb.NPC_01[2].npc_name;
    //    string narration = dialogdb.NPC_01[2].comment;
    //    string narration_2 = dialogdb.NPC_01[400].comment;
    //    RandomNum = Random.Range(0, 2);

    //    //�ؽ�Ʈ Ÿ����
    //    if (RandomNum == 0)
    //    {
    //        for (int a = 0; a < narration.Length; a++)
    //        {
    //            writerText += narration[a];
    //            ChatText.text = writerText;

    //            //5���� �̻� ��ȭ�� ����ǰ� ZŰ�� ������ ���
    //            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
    //            {
    //                ChatText.text = narration;

    //                //������ȭ ����
    //                remainSentence = true;
    //                //��ȭ ��
    //                isSentenceEnd = true;

    //                //for�� ���� ����
    //                a = narration.Length;
    //                ////��ȭ ��
    //                //isSentenceEnd = true;
    //            }

    //            //��簡 ���� ��µ��� �ʾ��� ���
    //            if (a < narration.Length)
    //            {
    //                //��� Ÿ���� �ӵ�
    //                yield return new WaitForSeconds(0.02f);
    //            }

    //        }
    //        yield return null;
    //    }
    //    else if (RandomNum == 1)
    //    {
    //        for (int a = 0; a < narration_2.Length; a++)
    //        //for (a = 0; a < textSpeed; a++)
    //        {
    //            writerText += narration_2[a];
    //            ChatText.text = writerText;

    //            //5���� �̻� ��ȭ�� ����ǰ� ZŰ�� ������ ���
    //            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
    //            {
    //                ChatText.text = narration_2;

    //                //������ȭ ����
    //                remainSentence = true;
    //                //��ȭ ��
    //                isSentenceEnd = true;

    //                //for�� ���� ����
    //                a = narration_2.Length;
    //                ////��ȭ ��
    //                //isSentenceEnd = true;
    //            }

    //            //��簡 ���� ��µ��� �ʾ��� ���
    //            if (a < narration_2.Length)
    //            {
    //                yield return new WaitForSeconds(0.02f);
    //            }
    //        }
    //        yield return null;
    //    }
    //    Debug.Log(writerText);

    //    //��� ����� ��� �Ϸ� �Ǿ��ٸ�
    //    if (ChatText.text == narration || ChatText.text == narration_2)
    //    {
    //        //��ȭ ���� ���� ����
    //        remainSentence = true;
    //        isSentenceEnd = true;
    //    }
    //}

    //IEnumerator ItemClueChat(string narrator, string narration)
    //{
    //    //������ȭ ����
    //    remainSentence = true;

    //    //�ʻ�ȭ ����
    //    GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];

    //    CharacterName.text = narrator;
    //    //characternameText = narrator;
    //    writerText = "";

    //    //�ؽ�Ʈ Ÿ����
    //    for (int a = 0; a < narration.Length; a++)
    //    {
    //        writerText += narration[a];
    //        ChatText.text = writerText;

    //        //5���� �̻� ��ȭ�� ����ǰ� ZŰ�� ������ ���
    //        if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
    //        {
    //            ChatText.text = narration;

    //            //������ȭ ����
    //            remainSentence = true;
    //            //��ȭ ��
    //            isSentenceEnd = true;

    //            //for�� ���� ����
    //            a = narration.Length;
    //        }

    //        //��� ��� ���� ��쿡��
    //        if (ChatText.text != narration)
    //        {
    //            //�ؽ�Ʈ Ÿ���� �ð� ����
    //            yield return new WaitForSeconds(0.02f);
    //        }
    //    }

    //    //��� ����� ��� �Ϸ� �Ǿ��ٸ�
    //    if (ChatText.text == narration)
    //    {
    //        //��ȭ ���� ���� ����
    //        remainSentence = true;
    //        isSentenceEnd = true;
    //    }
    //}

    ////�����ε�
    //IEnumerator ItemClueChat(string narrator, string narration, bool _remainSentence)
    //{

    //    //���б��� ����ϰ��
    //    if (narrator == "���б�")
    //    {
    //        //�ʻ�ȭ ����
    //        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[1];
    //    }
    //    else
    //    {
    //        //�ʻ�ȭ ����
    //        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
    //    }

    //    //���� ��ȭ�� �������
    //    if (_remainSentence == true)
    //    {
    //        //������ȭ ����
    //        remainSentence = true;

    //        CharacterName.text = narrator;

    //        //�ؽ�Ʈ Ÿ����
    //        for (int a = 0; a < narration.Length; a++)
    //        {
    //            writerText += narration[a];
    //            ChatText.text = writerText;

    //            //5���� �̻� ��ȭ�� ����ǰ� ZŰ�� ������ ���
    //            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
    //            {
    //                writerText = narration;
    //                ChatText.text = narration;

    //                //������ȭ ����
    //                remainSentence = true;
    //                ////��ȭ ��
    //                //isSentenceEnd = true;

    //                //for�� ���� ����
    //                a = narration.Length;
    //            }

    //            //��� ��� ���� ��쿡��
    //            if (ChatText.text != narration)
    //            {
    //                //�ؽ�Ʈ Ÿ���� �ð� ����
    //                yield return new WaitForSeconds(0.02f);
    //            }

    //        }

    //        //��� ��� �� ��� ������
    //        yield return new WaitForSeconds(0.1f);

    //        //ZŰ�� �ٽ� ���� ������ ������ ���
    //        while (true)
    //        {
    //            if (ChatText.text == narration && Input.GetKeyDown(KeyCode.Z))
    //            {
    //                Debug.Log("Text ����");

    //                //Text ����
    //                writerText = "";

    //                break;
    //            }
    //            yield return null;
    //        }
    //    }
    //}
    #endregion
}