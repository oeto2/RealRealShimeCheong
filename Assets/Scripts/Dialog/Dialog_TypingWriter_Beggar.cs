using System.Collections;
using System.Collections.Generic;
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

    //1005�� �ָԹ� ó����
    public bool isJoomuckBab = false;

    // ���� ��� ��� ����
    private int RandomNum;

    //��ȭ�� ���� ��� �Ǿ�����
    public bool isSentenceEnd = false;

    //���� ��ȭ�� �� �ִ���
    public bool remainSentence = false;
    public S_NPCdatabase_Yes dialogdb;


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

        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger && UIManager.instance.SentenceCondition()
             && TutorialManager.instance.SentenceCondition())
        {
            Debug.Log("zŰ ����! �������!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();

			if (bool_isNPC == false && !DialogManager.instance.remainSentence)
			{
				Debug.Log("��ȭ ����");
				images_NPC.SetActive(true);
				StartCoroutine(TextPractice());
				Trigger_NPC.instance.isNPCTrigger = true;
            }

			//��ȭ�� ������ ���
			else if (DialogManager.instance.isSentenceEnd)
            {
                images_NPC.SetActive(false);
                // images_NPC_portrait.SetActive(false);
                //��� ����
                StopAllCoroutines();
                Trigger_NPC.instance.isNPCTrigger = false;
                bool_isNPC = false;
                //Controller.instance.TalkEnd();
                controller_scr.TalkEnd();

                //������ȭ ����
                DialogManager.instance.remainSentence = false;
                //��ȭ ��
                DialogManager.instance.isSentenceEnd = false;
                //�ؽ�Ʈ ����
                DialogManager.instance.writerText = "";
            }
        }
    }


    IEnumerator NormalChat()
    {
        //��ȭ �ߺ����� ����
        remainSentence = true;

        //�ʻ�ȭ ����
        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];

        string narrator = characternameText = CharacterName.text = npcDatabaseScr.NPC_01[6].npc_name;
        string narration = npcDatabaseScr.NPC_01[6].comment;
        string narration_2 = npcDatabaseScr.NPC_01[403].comment;

        RandomNum = Random.Range(0, 2);
        Debug.Log(RandomNum);

        //�ؽ�Ʈ Ÿ����
        if (RandomNum == 0)
        {
            for (int a = 0; a < narration.Length; a++)
            {
                writerText += narration[a];
                ChatText.text = writerText;

                //5���� �̻� ��ȭ�� ����ǰ� ZŰ�� ������ ���
                if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
                {
                    ChatText.text = narration;

                    //������ȭ ����
                    remainSentence = true;
                    //��ȭ ��
                    isSentenceEnd = true;

                    //for�� ���� ����
                    a = narration.Length;
                    ////��ȭ ��
                    //isSentenceEnd = true;
                }

                //��簡 ���� ��µ��� �ʾ��� ���
                if (a < narration.Length)
                {
                    //��� Ÿ���� �ӵ�
                    yield return new WaitForSeconds(0.02f);
                }

            }
            yield return null;
        }
        else if (RandomNum == 1)
        {
            for (int a = 0; a < narration_2.Length; a++)
            //for (a = 0; a < textSpeed; a++)
            {
                writerText += narration_2[a];
                ChatText.text = writerText;

                //5���� �̻� ��ȭ�� ����ǰ� ZŰ�� ������ ���
                if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
                {
                    ChatText.text = narration_2;

                    //������ȭ ����
                    remainSentence = true;
                    //��ȭ ��
                    isSentenceEnd = true;

                    //for�� ���� ����
                    a = narration_2.Length;
                    ////��ȭ ��
                    //isSentenceEnd = true;
                }

                //��簡 ���� ��µ��� �ʾ��� ���
                if (a < narration_2.Length)
                {
                    yield return new WaitForSeconds(0.02f);
                }
            }
            yield return null;
        }
        Debug.Log(writerText);

        //��� ����� ��� �Ϸ� �Ǿ��ٸ�
        if (ChatText.text == narration || ChatText.text == narration_2)
        {
            //��ȭ ���� ���� ����
            remainSentence = true;
            isSentenceEnd = true;
        }
    }

    IEnumerator ItemClueChat(string narrator, string narration)
    {
        //������ȭ ����
        remainSentence = true;

        Debug.Log(narration);
        CharacterName.text = narrator;
        //characternameText = narrator;
        //narrator = CharacterName.text;

        //���б��� ����ϰ��
        if (narrator == "���б�")
        {
            //�ʻ�ȭ ����
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[1];
        }
        else
        {
            //�ʻ�ȭ ����
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
        }

        //�ؽ�Ʈ Ÿ����
        for (int a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            //5���� �̻� ��ȭ�� ����ǰ� ZŰ�� ������ ���
            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
            {
                ChatText.text = narration;

                //������ȭ ����
                remainSentence = true;
                //��ȭ ��
                isSentenceEnd = true;

                //for�� ���� ����
                a = narration.Length;
            }

            //��� ��� ���� ��쿡��
            if (ChatText.text != narration)
            {
                //�ؽ�Ʈ Ÿ���� �ð� ����
                yield return new WaitForSeconds(0.02f);
            }
        }

        //��� ����� ��� �Ϸ� �Ǿ��ٸ�
        if (ChatText.text == narration)
        {
            //��ȭ ���� ���� ����
            remainSentence = true;
            isSentenceEnd = true;
        }
    }

    //�����ε�
    IEnumerator ItemClueChat(string narrator, string narration, bool _remainSentence)
    {
        images_NPC.SetActive(true);

        //���б��� ����ϰ��
        if (narrator == "���б�")
        {
            //�ʻ�ȭ ����
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[1];
        }
        else
        {
            Debug.Log("���� �ʻ�ȭ ����");
            //�ʻ�ȭ ����
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
        }

        //���� ��ȭ�� �������
        if (_remainSentence == true)
        {
            //������ȭ ����
            remainSentence = true;

            CharacterName.text = narrator;

            //�ؽ�Ʈ Ÿ����
            for (int a = 0; a < narration.Length; a++)
            {
                writerText += narration[a];
                ChatText.text = writerText;

                //5���� �̻� ��ȭ�� ����ǰ� ZŰ�� ������ ���
                if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
                {
                    writerText = narration;
                    ChatText.text = narration;

                    //������ȭ ����
                    remainSentence = true;
                    ////��ȭ ��
                    //isSentenceEnd = true;

                    //for�� ���� ����
                    a = narration.Length;
                }

                //��� ��� ���� ��쿡��
                if (ChatText.text != narration)
                {
                    //�ؽ�Ʈ Ÿ���� �ð� ����
                    yield return new WaitForSeconds(0.02f);
                }

            }

            //��� ��� �� ��� ������
            yield return new WaitForSeconds(0.1f);

            //ZŰ�� �ٽ� ���� ������ ������ ���
            while (true)
            {
                if (ChatText.text == narration && Input.GetKeyDown(KeyCode.Z))
                {
                    Debug.Log("Text ����");

                    //Text ����
                    writerText = "";

                    break;
                }
                yield return null;
            }
        }
    }

    IEnumerator ClearChat(string narrator, string narration, bool _remainSentence)
    {
        //images_NPC.SetActive(false);
        CharacterName.text = narrator;




        images_NPC.SetActive(false);
        // images_NPC_portrait.SetActive(false);
        //��� ����
        writerText = "";
        //StopAllCoroutines();
        Trigger_NPC.instance.isNPCTrigger = false;
        bool_isNPC = false;

        yield return null;
    }

        IEnumerator TextPractice()
    {
        #region �ܼ�

        //2000 : �»���� �����
        if (ObjectManager.instance.GetEquipObjectKey() == 2000)
        {
            //�ָԹ� �̺�Ʈ Ȱ��ȭ
            EventManager.instance.EventActive(Events.JoomuckBab);

            //��ȭ ����
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[532].npc_name, dialogdb.NPC_01[532].comment, true));

            //û���� ��� �ܼ� ȹ��
            ObjectManager.instance.GetClue(2002);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[14].npc_name, dialogdb.NPC_01[14].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[17].npc_name, dialogdb.NPC_01[17].comment));
        }

        //2001 : û���� ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[27].npc_name, dialogdb.NPC_01[27].comment));
        }

        //2002 : û���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[35].npc_name, dialogdb.NPC_01[35].comment));
        }

        //2003 : û�̿� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[43].npc_name, dialogdb.NPC_01[43].comment));
        }

        //2004 : û�̿� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            //�������� �Ƶ� �ܼ� ȹ��
            ObjectManager.instance.GetClue(2005);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[536].npc_name, dialogdb.NPC_01[536].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[51].npc_name, dialogdb.NPC_01[51].comment));
        }

        //2005 : �������� �Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[62].npc_name, dialogdb.NPC_01[62].comment));
        }

        //2006 : �۳��� ���ΰ� û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[70].npc_name, dialogdb.NPC_01[70].comment));
        }

        //2008 : �·��� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[70].npc_name, dialogdb.NPC_01[70].comment));
        }

        //2009 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[99].npc_name, dialogdb.NPC_01[99].comment));
        }

        //2010 : ����� ��鼮
        else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[109].npc_name, dialogdb.NPC_01[109].comment));
        }

        //2010 : ������� ��ó
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[118].npc_name, dialogdb.NPC_01[118].comment));
        }

        //2012 : û���� �ŷ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[126].npc_name, dialogdb.NPC_01[126].comment));
        }

        //2013 : �⸮�� ��°�Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[134].npc_name, dialogdb.NPC_01[134].comment));
        }

        //2014 : ������������ ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[142].npc_name, dialogdb.NPC_01[142].comment));
        }

        //2015 : û�̰� �簣 ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[150].npc_name, dialogdb.NPC_01[150].comment));
        }

        //2016 : ¤���� �簣 û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[160].npc_name, dialogdb.NPC_01[160].comment));
        }

        //2017 : ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[166].npc_name, dialogdb.NPC_01[166].comment));
        }

        //2020 : ������� �־��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[204].npc_name, dialogdb.NPC_01[204].comment));
        }

        //2021 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[212].npc_name, dialogdb.NPC_01[212].comment));
        }

        //2023 : 3���� ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[234].npc_name, dialogdb.NPC_01[234].comment));
        }
        #endregion

        #region ������
        //1000 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[288].npc_name, dialogdb.NPC_01[288].comment));
        }

        //1005 : �ָԹ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[525].npc_name, dialogdb.NPC_01[525].comment, true));

            //�ý��� �޼���
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[17].npc_name, dialogdb.NPC_01[17].comment, true));
            //�ָԹ� ������ ����
            ObjectManager.instance.RemoveItem(1005);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[18].npc_name, dialogdb.NPC_01[18].comment, true));

            //�ָԹ� �Դ� ��..
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[809].npc_name, dialogdb.NPC_01[809].comment, true));

            //StopAllCoroutines();
            //��ȭ
            //yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[19].npc_name, npcDatabaseScr.NPC_01[19].comment, true));
            //yield return StartCoroutine(ClearChat(npcDatabaseScr.NPC_01[18].npc_name, npcDatabaseScr.NPC_01[18].comment, true));

            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[19].npc_name, dialogdb.NPC_01[19].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[809].npc_name, dialogdb.NPC_01[809].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[20].npc_name, dialogdb.NPC_01[20].comment, true));
            //û�̿� �系 �ܼ� ȹ��
            ObjectManager.instance.GetClue(2004);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[21].npc_name, dialogdb.NPC_01[21].comment));
        }

        //1006 : ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[304].npc_name, dialogdb.NPC_01[304].comment));
        }

        //1007 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[312].npc_name, dialogdb.NPC_01[312].comment));
        }

        //1008 : ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 1008) 
        {
            //������ ����
            ObjectManager.instance.RemoveItem(1008);
            //������ ���� �Ϸ�
            EventManager.instance.eventEndCheck.giveBoridduck_End = true;
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[528].npc_name, dialogdb.NPC_01[528].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[173].npc_name, dialogdb.NPC_01[173].comment, true));
            //�� ȹ��
            ObjectManager.instance.GetItem(1009);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[174].npc_name, dialogdb.NPC_01[174].comment));
        }

        //1009 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[327].npc_name, dialogdb.NPC_01[327].comment));
        }

        //1011 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[343].npc_name, dialogdb.NPC_01[343].comment));
        }
        #endregion

        #region ���� �ܼ�

        //4023 : ����̸� ���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[354].npc_name, dialogdb.NPC_01[354].comment));
        }

        //4015 : ��Ʋ���� �־��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[362].npc_name, dialogdb.NPC_01[362].comment));
        }

        //4017 : û�̿� ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[370].npc_name, dialogdb.NPC_01[370].comment));
        }

        //8032 : �Բ� ����� �λ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[378].npc_name, dialogdb.NPC_01[378].comment));
        }

        //4033 : ������ �ߴ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[385].npc_name, dialogdb.NPC_01[385].comment));
        }

        //4018 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[395].npc_name, dialogdb.NPC_01[395].comment));
        }
        #endregion

        //�⺻ ���
        else
        {
            yield return StartCoroutine(DialogManager.instance.NormalChat("����"));
        }
    }
}