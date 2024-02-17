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

    [SerializeField]
    public S_NPCdatabase_Yes dialogdb;

    // ���� ��� ��� ����
    private int RandomNum;

    //��ȭ�� ���� ��� �Ǿ�����
    public bool isSentenceEnd = false;

    //���� ��ȭ�� �� �ִ���
    public bool remainSentence = false;

    //�۳��� ���ΰ� û�� ������ ��
    public int int_Select2006Num = 0;

    //2006�� ������ �� ��¥
    public int int_select2006Day = 0;

    //����2 ������Ʈ
    public GameObject gameObject_BoatMan2;

    //����2 �̺�Ʈ ���࿩��
    public bool boatMan2_Show;

    //����� ������ ���� �ߴ���
    public bool boatManObject;

    //������ ��Ҵ���
    public bool isTouch;

    //���� Ŭ��
    void Start()
    {
        //StartCoroutine(TextPractice());
        //TextPractice();
        //StopCoroutine(TextPractice());
        CharacterName.text = "";
        ChatText.text = "";

        //Selection_Text_Name.text = "������ �߻�!";
        //Selection_Text1.text = "���� û�� �ƺ� �Ǵ� ����̿�. �����ϰ� �����ֽÿ�.";
        //Selection_Text2.text = "���� �� �̾߱��� �����. �� ������� �ʹ��ϴ��� ���̿�!";
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

        if (Input.GetKeyDown(KeyCode.Z) && isTouch && UIManager.instance.SentenceCondition()
             && TutorialManager.instance.SentenceCondition())
        {
            Debug.Log("zŰ ����! ����!!!!");
            //bool_isBotjim = true;
            //controller_scr.TalkStart();

            if (bool_isNPC == false && !DialogManager.instance.remainSentence)
            {
                Debug.Log("��ȭ ����");
                images_NPC.SetActive(true);
                StartCoroutine(TextPractice());
                //Trigger_NPC.instance.isNPCTrigger = true;
            }

            //��ȭ�� ������ ���
            else if (DialogManager.instance.isSentenceEnd)
            {
                //isSelection_5136 = false;

                images_NPC.SetActive(false);
                // images_NPC_portrait.SetActive(false);
                //��� ����
                StopAllCoroutines();
                //Trigger_NPC.instance.isNPCTrigger = false;
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

    IEnumerator TextPractice()
    {
        #region �ܼ�
        //2001 : û���� ������
        if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[28].npc_name, dialogdb.NPC_01[28].comment));
        }

        //2003 : û�̿� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[44].npc_name, dialogdb.NPC_01[44].comment));
        }

        //2004 : û�̿� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[52].npc_name, dialogdb.NPC_01[52].comment));
        }

        //2005 : �������� �Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[63].npc_name, dialogdb.NPC_01[63].comment));
        }

        //2006 : �۳��� ���ΰ� û�� (�߰� ��� ����)
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[538].npc_name, dialogdb.NPC_01[538].comment, true));

            //�������� ���� �ʾҴٸ�
            if (!EventManager.instance.selectEndCheck.select2006_End)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[71].npc_name, dialogdb.NPC_01[71].comment, true));
                EventManager.instance.SelectStart(NPCName.boatman, 2006);
            }
            else
            {
                //������ 1���� ����� ���
                if (int_Select2006Num == 1)
                {
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[73].npc_name, dialogdb.NPC_01[73].comment, true));
                    //û���� �ŷ� �ܼ� ȹ��
                    ObjectManager.instance.GetClue(2012);
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[74].npc_name, dialogdb.NPC_01[74].comment));
                }
                else
                {
                    //���� �Ϸ簡 �����ٸ�
                    if (int_select2006Day < TimeManager.instance.int_DayCount)
                    {
                        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[71].npc_name, dialogdb.NPC_01[71].comment, true));
                        EventManager.instance.SelectStart(NPCName.boatman, 2006);
                    }
                    else
                    {
                        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[77].npc_name, dialogdb.NPC_01[77].comment));
                    }
                }
            }
        }

        //2007 : �·��� û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[84].npc_name, dialogdb.NPC_01[84].comment));
        }

        //2008 : �·��� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[92].npc_name, dialogdb.NPC_01[92].comment));
        }

        //2009 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[100].npc_name, dialogdb.NPC_01[100].comment));
        }

        //2011 : ������� ��ó
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[119].npc_name, dialogdb.NPC_01[119].comment));
        }

        //2012 : û���� �ŷ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[127].npc_name, dialogdb.NPC_01[127].comment));
        }

        //2013 : �⸮�� ��°�Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[135].npc_name, dialogdb.NPC_01[135].comment));
        }

        //2014 : ������������ ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            //�����ʴ� �� �ܼ� ȹ��
            ObjectManager.instance.GetClue(2019);

            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[546].npc_name, dialogdb.NPC_01[546].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[143].npc_name, dialogdb.NPC_01[143].comment));
        }

        //2015 : û�̰� �簣 ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[151].npc_name, dialogdb.NPC_01[151].comment));
        }

        //2017 : ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[169].npc_name, dialogdb.NPC_01[169].comment));
        }

        //2018 : ������ �ܰ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[188].npc_name, dialogdb.NPC_01[188].comment));
        }

        //2019 : �����ʴ� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[196].npc_name, dialogdb.NPC_01[196].comment));
        }

        //2020 : ������� �־��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            //����� ���� �ܼ� ȹ��
            ObjectManager.instance.GetClue(2021);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[552].npc_name, dialogdb.NPC_01[552].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[205].npc_name, dialogdb.NPC_01[205].comment));
        }

        //2021 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[213].npc_name, dialogdb.NPC_01[213].comment));
        }

        //2022 : ����� ����� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[227].npc_name, dialogdb.NPC_01[227].comment));
        }

        //2023 : 3�� ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[555].npc_name, dialogdb.NPC_01[555].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[236].npc_name, dialogdb.NPC_01[236].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[237].npc_name, dialogdb.NPC_01[237].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[238].npc_name, dialogdb.NPC_01[238].comment, true));

            //���ﰡ ���� ���̱�
            gameObject_BoatMan2.SetActive(true);
            boatMan2_Show = true;

            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[239].npc_name, dialogdb.NPC_01[239].comment));
        }
        #endregion

        #region ������
        //1000 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[289].npc_name, dialogdb.NPC_01[289].comment));
        }

        //1005 : �ָԹ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[297].npc_name, dialogdb.NPC_01[297].comment));
        }

        //1006 : ��� 
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[305].npc_name, dialogdb.NPC_01[305].comment));
        }

        //1007 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[313].npc_name, dialogdb.NPC_01[313].comment));
        }

        //1009 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[328].npc_name, dialogdb.NPC_01[328].comment));
        }

        //1011 : ����� ���� (���� ��� �߰�)
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[531].npc_name, dialogdb.NPC_01[531].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[344].npc_name, dialogdb.NPC_01[344].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[345].npc_name, dialogdb.NPC_01[345].comment, true));
            boatManObject = true;
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[346].npc_name, dialogdb.NPC_01[346].comment));
        }

        //1013 : ������1
        else if (ObjectManager.instance.GetEquipObjectKey() == 1013)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[859].npc_name, dialogdb.NPC_01[859].comment));
        }
        //1014 : ������2
        else if (ObjectManager.instance.GetEquipObjectKey() == 1014)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[867].npc_name, dialogdb.NPC_01[867].comment));
        }
        #endregion

        #region ���� �ܼ�
        //4023 : ����̸� ���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[355].npc_name, dialogdb.NPC_01[355].comment));
        }

        //4015 : ��Ʋ���� �־��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[363].npc_name, dialogdb.NPC_01[363].comment));
        }

        //4017 : û�̿� ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[371].npc_name, dialogdb.NPC_01[371].comment));
        }

        //4033 : ������ �ߴ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[560].npc_name, dialogdb.NPC_01[560].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[387].npc_name, dialogdb.NPC_01[387].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[389].npc_name, dialogdb.NPC_01[389].comment, true));

            //����� ����� ��� �ܼ�ȹ��
            ObjectManager.instance.GetClue(2022);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[390].npc_name, dialogdb.NPC_01[390].comment));
        }

        //4018 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[397].npc_name, dialogdb.NPC_01[397].comment));
        }
        #endregion

        //�⺻ ���
        else
        {
            yield return StartCoroutine(DialogManager.instance.NormalChat("����"));
        }
    }

    //�۳��� ���ΰ� û�� 1�� ���
    IEnumerator Select2006_Sentence1()
    {
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[73].npc_name, dialogdb.NPC_01[73].comment, true));
        //û���� �ŷ� �ܼ� ȹ��
        ObjectManager.instance.GetClue(2012);
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[74].npc_name, dialogdb.NPC_01[74].comment));
    }

    //�۳��� ���ΰ� û�� 2�� ���
    IEnumerator Select2006_Sentence2()
    {
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[75].npc_name, dialogdb.NPC_01[75].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[76].npc_name, dialogdb.NPC_01[76].comment));

    }

    //�۳��� ���ΰ� û�� ������ 1�� ��� ���
    public void PrintSelect2006_Sentence1()
    {
        StartCoroutine(Select2006_Sentence1());
    }

    //�۳��� ���ΰ� û�� ������ 2�� ��� ���
    public void PrintSelect2006_Sentence2()
    {
        StartCoroutine(Select2006_Sentence2());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }

    #region PreviousCode

    //IEnumerator NormalChat()
    //{
    //    //��ȭ �ߺ����� ����
    //    remainSentence = true;

    //    string narrator = characternameText = CharacterName.text = npcDatabaseScr.NPC_01[7].npc_name;
    //    string narration = npcDatabaseScr.NPC_01[7].comment;
    //    string narration_2 = npcDatabaseScr.NPC_01[404].comment;

    //    RandomNum = Random.Range(0, 2);
    //    Debug.Log(RandomNum);

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

    //    Debug.Log(narration);
    //    CharacterName.text = narrator;
    //    //characternameText = narrator;
    //    //narrator = CharacterName.text;

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

    //IEnumerator ItemClueChat2(string narrator, string narration, bool _remainSentence)
    //{
    //    //���� ��ȭ�� �������
    //    if (_remainSentence == true)
    //    {
    //        //������ȭ ����
    //        remainSentence = true;

    //        Debug.Log(narration);
    //        int a = 0;
    //        CharacterName.text = narrator;
    //        //characternameText = narrator;


    //        //narrator = CharacterName.text;

    //        //�ؽ�Ʈ Ÿ����
    //        for (a = 0; a < narration.Length; a++)
    //        {
    //            writerText += narration[a];
    //            ChatText.text = writerText;

    //            //�ؽ�Ʈ Ÿ���� �ð� ����
    //            //yield return null;
    //            yield return new WaitForSeconds(0.02f);

    //            //�߰��� ZŰ�� ������
    //            if (Input.GetKeyDown(KeyCode.Z))
    //            {
    //                break;
    //            }
    //        }

    //        //ZŰ�� �ٽ� ���� ������ ������ ���
    //        while (true)
    //        {
    //            if (Input.GetKeyDown(KeyCode.Z))
    //            {
    //                //Text ����
    //                writerText = "";
    //                break;
    //            }
    //            yield return null;
    //        }
    //    }

    ////Ű(default : space)�� �ٽ� ���� ������ ������ ���
    //while (true)
    //{
    //    if (isButtonClicked)
    //    {
    //        isButtonClicked = false;
    //        break;
    //    }
    //    yield return null;
    //}

    //}

    //IEnumerator ItemClueChat_select()
    //{
    //    //2006 : �۳��� ���ΰ� û�� (�߰� ��� ����)
    //    if (ObjectManager.instance.GetEquipObjectKey() == 2006)
    //    {
    //        images_NPC.SetActive(false);
    //        Canvas_Selection_UI.SetActive(true);
    //    }
    //    yield return null;
    //}

    //public void onClick_Selet1()
    //{
    //    //StartCoroutine(TextPractice_2());
    //    //Canvas_Selection_UI.SetActive(false);
    //    //images_NPC.SetActive(true);
    //        Canvas_Selection_UI.SetActive(false);

    //        isSelection_yes = true;
    //        isSelection_no = false;
    //        isSelection_5136 = false;

    //        images_NPC.SetActive(true);
    //        bool_isNPC = true;
    //        Trigger_NPC.instance.isNPCTrigger = true;
    //    //GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
    //}

    //public void onClick_Selet2()
    //{
    //    Canvas_Selection_UI.SetActive(false);

    //    isSelection_yes = false;
    //    isSelection_no = true;

    //    images_NPC.SetActive(true);
    //    bool_isNPC = true;
    //    Trigger_NPC.instance.isNPCTrigger = true;

    //    //GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];

    //}
    #endregion
}