using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Songnara : Dialogue,ITalkable
{
    public IEnumerator TextPractice()
    {

        #region �ܼ�

        //2005 : �������� �Ƶ�
        if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            //������������ ���� �ܼ� ȹ��
            ObjectManager.instance.GetClue(2014);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[537].npc_name, dialogdb.NPC_01[537].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[64].npc_name, dialogdb.NPC_01[64].comment));
        }

        //2006 : �۳��� ���ΰ� û��
        if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            //û���� ���� �ܼ� ȹ��
            ObjectManager.instance.GetClue(2009);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[538].npc_name, dialogdb.NPC_01[538].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[72].npc_name, dialogdb.NPC_01[72].comment));
        }

        //2009 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[101].npc_name, dialogdb.NPC_01[101].comment));
        }

        //2011 : ������� ��ó
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[120].npc_name, dialogdb.NPC_01[120].comment));
        }

        //2012 : û���� �ŷ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[128].npc_name, dialogdb.NPC_01[128].comment));
        }

        //2014 : ������������ ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[144].npc_name, dialogdb.NPC_01[144].comment));
        }

        //2015 : û�̰� �簣 ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[152].npc_name, dialogdb.NPC_01[152].comment));
        }

        //2017 : ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[170].npc_name, dialogdb.NPC_01[170].comment));
        }

        //2018 : ������ �ܰ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[189].npc_name, dialogdb.NPC_01[189].comment));
        }

        //2020 : ������� �־��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[206].npc_name, dialogdb.NPC_01[206].comment));
        }

        //2021 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[214].npc_name, dialogdb.NPC_01[214].comment));
        }

        //2022 : �ٻ� ���ε�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[228].npc_name, dialogdb.NPC_01[228].comment));
        }

        //2022 : �ٻ� ���ε�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[228].npc_name, dialogdb.NPC_01[228].comment));
        }

        //2023 : 3�� ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {

            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[555].npc_name, dialogdb.NPC_01[555].comment, true));

            //���忣��4 �������
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[248].npc_name, dialogdb.NPC_01[248].comment, true));

            //���� ��� ON
            EndingManager.instance.ShowEndingBG();
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[425].npc_name, dialogdb.NPC_01[425].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[426].npc_name, dialogdb.NPC_01[426].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[427].npc_name, dialogdb.NPC_01[427].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[428].npc_name, dialogdb.NPC_01[428].comment, true));

            //���忣�� ��� ���̱�
            EndingManager.instance.ChangeToBadEndingBG();
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[429].npc_name, dialogdb.NPC_01[429].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[430].npc_name, dialogdb.NPC_01[430].comment, true));

            //Ÿ��Ʋ�� �̵�
            EndingManager.instance.LoadTitleScene();
        }
        #endregion

        #region ������
        //1006 : ��� 
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[306].npc_name, dialogdb.NPC_01[306].comment));
        }

        //1007 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[314].npc_name, dialogdb.NPC_01[314].comment));
        }

        //1011 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[347].npc_name, dialogdb.NPC_01[347].comment));
        }
        //1013 : ������1
        else if (ObjectManager.instance.GetEquipObjectKey() == 1013)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[860].npc_name, dialogdb.NPC_01[860].comment));
        }
        //1014 : ������2
        else if (ObjectManager.instance.GetEquipObjectKey() == 1014)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[868].npc_name, dialogdb.NPC_01[868].comment));
        }
        #endregion

        #region ���� �ܼ�
        //4023 : ����̸� ���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[356].npc_name, dialogdb.NPC_01[356].comment));
        }

        //8032 : �Բ� ����� �� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[379].npc_name, dialogdb.NPC_01[379].comment));
        }

        //4033 : ������ �ߴ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[388].npc_name, dialogdb.NPC_01[388].comment));
        }

        //4018 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[393].npc_name, dialogdb.NPC_01[393].comment));
        }
        #endregion

        //�⺻ ���
        else
        {
            yield return StartCoroutine(DialogManager.instance.NormalChat("�۳��� ����"));
        }
    }
}