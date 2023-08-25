using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_BoatMan : MonoBehaviour
{
    // ���� ä���� ������ �ؽ�Ʈ
    public Text ChatText;

    // ĳ���� �̸��� ������ �ؽ�Ʈ
    public Text CharacterName;

    // ��ȭ�� ������ �ѱ� �� �ִ� Ű(default : space)
    public List<KeyCode> skipButton;

    public string writerText = "";

    public string characternameText = "";

    bool isButtonClicked = false;

    public bool bool_isNPC = false;

    public GameObject images_NPC;

    public Sprite[] images_NPC_portrait;

    public Trigger_NPC trigger_npc;

    public bool isNPCTrigger;

    public Controller controller_scr;

    public S_NPCdatabase_Yes npcDatabaseScr;

    //���� Ŭ��
    void Start()
    {
        //StartCoroutine(TextPractice());
        //TextPractice();
        //StopCoroutine(TextPractice());
        CharacterName.text = "";
        ChatText.text = "";
    }


    void Update()
    {
        foreach (var element in skipButton) // ��ư �˻�
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }
        //TextPractice();
        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("zŰ ����! ����!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();
            if (bool_isNPC == false)
            {
                StartCoroutine(TextPractice());
                images_NPC.SetActive(true);
                Trigger_NPC.instance.isNPCTrigger = true;
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
                bool_isNPC = true;
            }
            else
            {
                images_NPC.SetActive(false);
                // images_NPC_portrait.SetActive(false);
                writerText = "";
                StopAllCoroutines();
                Trigger_NPC.instance.isNPCTrigger = false;
                //Controller.instance.TalkEnd();
                controller_scr.TalkEnd();
                bool_isNPC = false;
            }
        }
      
    }

    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        CharacterName.text = narrator;
        //characternameText = narrator;
        writerText = "";

        //narrator = CharacterName.text;

        //�ؽ�Ʈ Ÿ����
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            //�ؽ�Ʈ Ÿ���� �ð� ����
            //yield return null;
            yield return new WaitForSeconds(0.02f);
        }

        //Ű(default : space)�� �ٽ� ���� ������ ������ ���
        while (true)
        {
            if (isButtonClicked)
            {
                isButtonClicked = false;
                break;
            }
            yield return null;
        }
    }

    IEnumerator TextPractice()
    {
        #region �ܼ�
        //2001 : û���� ������
        if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[28].npc_name, npcDatabaseScr.NPC_01[28].comment));
        }

        //2003 : û�̿� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[44].npc_name, npcDatabaseScr.NPC_01[44].comment));
        }

        //2004 : û�̿� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[52].npc_name, npcDatabaseScr.NPC_01[52].comment));
        }

        //2005 : �������� �Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[63].npc_name, npcDatabaseScr.NPC_01[63].comment));
        }

        //2006 : �۳��� ���ΰ� û�� (�߰� ��� ����)
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[71].npc_name, npcDatabaseScr.NPC_01[71].comment));
        }

        //2007 : �·��� û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[84].npc_name, npcDatabaseScr.NPC_01[84].comment));
        }

        //2008 : �·��� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[92].npc_name, npcDatabaseScr.NPC_01[92].comment));
        }

        //2009 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[100].npc_name, npcDatabaseScr.NPC_01[100].comment));
        }

        //2011 : ������� ��ó
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[119].npc_name, npcDatabaseScr.NPC_01[119].comment));
        }

        //2012 : û���� �ŷ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[127].npc_name, npcDatabaseScr.NPC_01[127].comment));
        }

        //2013 : �⸮�� ��°�Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[135].npc_name, npcDatabaseScr.NPC_01[135].comment));
        }

        //2014 : ������������ ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[143].npc_name, npcDatabaseScr.NPC_01[143].comment));
        }

        //2015 : û�̰� �簣 ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[151].npc_name, npcDatabaseScr.NPC_01[151].comment));
        }

        //2017 : ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[169].npc_name, npcDatabaseScr.NPC_01[169].comment));
        }

        //2018 : ������ �ܰ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[188].npc_name, npcDatabaseScr.NPC_01[188].comment));
        }

        //2019 : �����ʴ� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[196].npc_name, npcDatabaseScr.NPC_01[196].comment));
        }

        //2020 : ������� �־��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[205].npc_name, npcDatabaseScr.NPC_01[205].comment));
        }

        //2021 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[213].npc_name, npcDatabaseScr.NPC_01[213].comment));
        }

        //2022 : �ٻ� ���ε�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[227].npc_name, npcDatabaseScr.NPC_01[227].comment));
        }

        //2023 : ����� ����� ��� (���� ��� ����)
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[236].npc_name, npcDatabaseScr.NPC_01[236].comment));
        }
        #endregion

        #region ������
        //1000 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[289].npc_name, npcDatabaseScr.NPC_01[289].comment));
        }

        //1005 : �ָԹ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[297].npc_name, npcDatabaseScr.NPC_01[297].comment));
        }

        //1006 : ��� 
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[305].npc_name, npcDatabaseScr.NPC_01[305].comment));
        }

        //1007 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[313].npc_name, npcDatabaseScr.NPC_01[313].comment));
        }

        //1009 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[328].npc_name, npcDatabaseScr.NPC_01[328].comment));
        }

        //1011 : ����� ���� (���� ��� �߰�)
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[344].npc_name, npcDatabaseScr.NPC_01[344].comment));
        }

        #endregion

        #region ���� �ܼ�
        //3001 : ����̸� ���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 3001)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[355].npc_name, npcDatabaseScr.NPC_01[355].comment));
        }

        //3002 : ��Ʋ���� �־��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 3002)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[363].npc_name, npcDatabaseScr.NPC_01[363].comment));
        }

        //3003 : û�̿� ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 3003)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[371].npc_name, npcDatabaseScr.NPC_01[371].comment));
        }

        //3005 : ������ �ߴ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 3005)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[387].npc_name, npcDatabaseScr.NPC_01[387].comment));
        }

        //3006 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 3006)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[397].npc_name, npcDatabaseScr.NPC_01[397].comment));
        }
        #endregion

        //�⺻ ���
        else
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[7].npc_name, npcDatabaseScr.NPC_01[7].comment));
        }
        //yield return StartCoroutine(NormalChat(characternameText, writerText));
        //yield return StartCoroutine(NormalChat("���º���", "?�ȳ��ϼ���, �ݰ����ϴ�. ��ȭ ��ȯ �׽�Ʈ�Դϴ� �̰��� �׽�Ʈ��? �׷� �׽�Ʈ�� �׽�Ʈ�� �׽�Ʈ�� �׽�Ʈ"));
    }
}