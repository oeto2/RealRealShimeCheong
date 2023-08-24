using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Songnara : MonoBehaviour
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

    //���̾�α� UI
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
   
        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            
            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false)
            {
                //���̾�α� ���̱�
                images_NPC.SetActive(true);
                //��� ����
                writerText = "";
                //��� ���
                StartCoroutine(TextPractice());
                //�ʻ�ȭ ����
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
                bool_isNPC = true;
            }
            else
            {
                //���̾�α� ����
                images_NPC.SetActive(false);
                //��� ����
                writerText = "";
                //�ڷ�ƾ ����
                StopAllCoroutines();
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
        //2006 : �۳��� ���ΰ� û��
        if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[72].npc_name, npcDatabaseScr.NPC_01[72].comment));
        }

        //2009 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[101].npc_name, npcDatabaseScr.NPC_01[101].comment));
        }

        //2011 : ������� ��ó
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[120].npc_name, npcDatabaseScr.NPC_01[120].comment));
        }

        //2012 : û���� �ŷ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[128].npc_name, npcDatabaseScr.NPC_01[128].comment));
        }

        //2014 : ������������ ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[144].npc_name, npcDatabaseScr.NPC_01[144].comment));
        }

        //2015 : û�̰� �簣 ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[152].npc_name, npcDatabaseScr.NPC_01[152].comment));
        }

        //2017 : ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[170].npc_name, npcDatabaseScr.NPC_01[170].comment));
        }

        //2018 : ������ �ܰ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[189].npc_name, npcDatabaseScr.NPC_01[189].comment));
        }

        //2020 : ������� �־��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[206].npc_name, npcDatabaseScr.NPC_01[206].comment));
        }

        //2021 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[214].npc_name, npcDatabaseScr.NPC_01[214].comment));
        }

        //2022 : �ٻ� ���ε�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[228].npc_name, npcDatabaseScr.NPC_01[228].comment));
        }

        //2022 : �ٻ� ���ε�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[228].npc_name, npcDatabaseScr.NPC_01[228].comment));
        }

        //2023 : ����� ����� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[248].npc_name, npcDatabaseScr.NPC_01[248].comment));
        }
        #endregion

        #region ������
        //1006 : ��� 
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[306].npc_name, npcDatabaseScr.NPC_01[306].comment));
        }

        //1007 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[314].npc_name, npcDatabaseScr.NPC_01[314].comment));
        }

        //1011 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[347].npc_name, npcDatabaseScr.NPC_01[347].comment));
        }

        #endregion

        #region ���� �ܼ�
        //3001 : ����̸� ���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 3001)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[356].npc_name, npcDatabaseScr.NPC_01[356].comment));
        }

        //3004 : �Բ� ����� �� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 3004)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[379].npc_name, npcDatabaseScr.NPC_01[379].comment));
        }

        //3005 : ������ �ߴ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 3005)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[388].npc_name, npcDatabaseScr.NPC_01[388].comment));
        }

        //3006 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 3006)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[393].npc_name, npcDatabaseScr.NPC_01[393].comment));
        }
        #endregion

        //�⺻ ���
        else
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[8].npc_name, npcDatabaseScr.NPC_01[8].comment));
        }
    }
}