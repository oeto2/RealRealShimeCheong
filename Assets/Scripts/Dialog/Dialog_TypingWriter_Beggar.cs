using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Beggar : MonoBehaviour
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

    // ���� ��� ��� ����
    private int RandomNum;

    //���� Ŭ��
    void Start()
    {
        //StartCoroutine(TextPractice());
        //TextPractice();
        //StopCoroutine(TextPractice());
        CharacterName.text="";
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

        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("zŰ ����! �������!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false)
            {
                images_NPC.SetActive(true);
                StartCoroutine(TextPractice());
                Trigger_NPC.instance.isNPCTrigger = true;
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
                bool_isNPC = true;
            }
            else
            {
                images_NPC.SetActive(false);
                // images_NPC_portrait.SetActive(false);
                //��� ����
                writerText = "";
                StopAllCoroutines();
                Trigger_NPC.instance.isNPCTrigger = false;
                bool_isNPC = false;
                //Controller.instance.TalkEnd();
                controller_scr.TalkEnd();
            }
        }
    }


    IEnumerator NormalChat()
    {
        int a = 0;
        string narrator = characternameText = CharacterName.text = npcDatabaseScr.NPC_01[6].npc_name;
        string narration = npcDatabaseScr.NPC_01[6].comment;
        string narration_2 = npcDatabaseScr.NPC_01[403].comment;

        RandomNum = Random.Range(0, 2);
        Debug.Log(RandomNum);

        if (RandomNum == 0)
        {
            for (a = 0; a < narration.Length; a++)
            //for (a = 0; a < textSpeed; a++)
            {
                writerText += narration[a];
                ChatText.text = writerText;

                //�ؽ�Ʈ Ÿ���� �ð� ����
                //yield return null;
                yield return new WaitForSeconds(0.02f);
            }
            yield return null;
        }
        else if (RandomNum == 1)
        {
            for (a = 0; a < narration_2.Length; a++)
            //for (a = 0; a < textSpeed; a++)
            {
                writerText += narration_2[a];
                ChatText.text = writerText;

                //�ؽ�Ʈ Ÿ���� �ð� ����
                //yield return null;
                yield return new WaitForSeconds(0.02f);
            }
            yield return null;
        }
        
        Debug.Log(writerText);
        //writerText = "";

        /*
        //�ؽ�Ʈ Ÿ����
        for (a = 0; a < narration.Length; a++)
        //for (a = 0; a < textSpeed; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            //�ؽ�Ʈ Ÿ���� �ð� ����
            //yield return null;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
        */

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

    IEnumerator ItemClueChat(string narrator, string narration)
    {
        int a = 0;
        CharacterName.text = narrator;
        //characternameText = narrator;
        

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

        //2000 : �»���� �����
        if (ObjectManager.instance.GetEquipObjectKey() == 2000)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[14].npc_name, npcDatabaseScr.NPC_01[14].comment));
        }

        //2001 : û���� ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[27].npc_name, npcDatabaseScr.NPC_01[27].comment));
        }

        //2002 : û���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[35].npc_name, npcDatabaseScr.NPC_01[35].comment));
        }

        //2003 : û�̿� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[43].npc_name, npcDatabaseScr.NPC_01[43].comment));
        }

        //2004 : û�̿� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[51].npc_name, npcDatabaseScr.NPC_01[51].comment));
        }

        //2004 : û�̿� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[51].npc_name, npcDatabaseScr.NPC_01[51].comment));
        }

        //2005 : �������� �Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[62].npc_name, npcDatabaseScr.NPC_01[62].comment));
        }

        //2006 : �۳��� ���ΰ� û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[70].npc_name, npcDatabaseScr.NPC_01[70].comment));
        }

        //2008 : �·��� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[70].npc_name, npcDatabaseScr.NPC_01[70].comment));
        }

        //2009 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[99].npc_name, npcDatabaseScr.NPC_01[99].comment));
        }

        //2010 : ����� ��鼮
        else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[109].npc_name, npcDatabaseScr.NPC_01[109].comment));
        }

        //2010 : ������� ��ó
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[118].npc_name, npcDatabaseScr.NPC_01[118].comment));
        }

        //2012 : û���� �ŷ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[126].npc_name, npcDatabaseScr.NPC_01[126].comment));
        }

        //2013 : �⸮�� ��°�Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[134].npc_name, npcDatabaseScr.NPC_01[134].comment));
        }

        //2014 : ������������ ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[142].npc_name, npcDatabaseScr.NPC_01[142].comment));
        }

        //2015 : û�̰� �簣 ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[150].npc_name, npcDatabaseScr.NPC_01[150].comment));
        }

        //2016 : ¤���� �簣 û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[160].npc_name, npcDatabaseScr.NPC_01[160].comment));
        }

        //2017 : ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[166].npc_name, npcDatabaseScr.NPC_01[166].comment));
        }

        //2020 : ������� �־��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[204].npc_name, npcDatabaseScr.NPC_01[204].comment));
        }

        //2021 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[212].npc_name, npcDatabaseScr.NPC_01[212].comment));
        }

        //2023 : ����� ����� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[234].npc_name, npcDatabaseScr.NPC_01[234].comment));
        }
        #endregion

        #region ������
        //1000 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[288].npc_name, npcDatabaseScr.NPC_01[288].comment));
        }

        //1006 : ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[304].npc_name, npcDatabaseScr.NPC_01[304].comment));
        }

        //1008 : ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 1008)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[312].npc_name, npcDatabaseScr.NPC_01[312].comment));
        }

        //1009 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[327].npc_name, npcDatabaseScr.NPC_01[327].comment));
        }

        //1011 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[343].npc_name, npcDatabaseScr.NPC_01[343].comment));
        }
        #endregion

        #region ���� �ܼ�

        //3001 : ����̸� ���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 3001)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[354].npc_name, npcDatabaseScr.NPC_01[354].comment));
        }

        //3002 : ��Ʋ���� �־��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 3002)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[362].npc_name, npcDatabaseScr.NPC_01[362].comment));
        }

        //3003 : û�̿� ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 3002)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[370].npc_name, npcDatabaseScr.NPC_01[370].comment));
        }

        //3004 : �Բ� ����� �λ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 3004)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[378].npc_name, npcDatabaseScr.NPC_01[378].comment));
        }

        //3005 : ������ �ߴ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 3005)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[385].npc_name, npcDatabaseScr.NPC_01[385].comment));
        }

        //3006 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 3006)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[395].npc_name, npcDatabaseScr.NPC_01[395].comment));
        }
        #endregion

        //�⺻ ���
        else
        {
            yield return StartCoroutine(NormalChat());
        }

        //yield return StartCoroutine(NormalChat(characternameText, writerText));
        //yield return StartCoroutine(NormalChat("���º���", "?�ȳ��ϼ���, �ݰ����ϴ�. ��ȭ ��ȯ �׽�Ʈ�Դϴ� �̰��� �׽�Ʈ��? �׷� �׽�Ʈ�� �׽�Ʈ�� �׽�Ʈ�� �׽�Ʈ"));
    }
}