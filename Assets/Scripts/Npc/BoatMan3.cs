using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatMan3 : MonoBehaviour
{
    //������ ��Ҵ���
    public bool isTouch;

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

    public bool isNPCTrigger;

    public Controller controller_scr;

    public S_NPCdatabase_Yes npcDatabaseScr;
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

    //������ �Ϸ��ߴ���
    public bool isSelectDone;

    //���� Ŭ��
    void Start()
    {
        CharacterName.text = "";
        ChatText.text = "";
    }

    void Update()
    {
        //if(isTouch && Input.GetKeyDown(KeyCode.Z) && !EventManager.instance.gameObject_SelectUI.activeSelf)
        //{
        //    
        //}

        foreach (var element in skipButton) // ��ư �˻�
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && UIManager.instance.SentenceCondition()
             && TutorialManager.instance.SentenceCondition() && isTouch && !isSelectDone)
        {
            Debug.Log("zŰ ����! ����!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false && !DialogManager.instance.remainSentence)
            {
                Debug.Log("��ȭ ����");
                images_NPC.SetActive(true);
                StartCoroutine(TextPractice());
            }

            //��ȭ�� ������ ���
            else if (DialogManager.instance.isSentenceEnd)
            {
                //isSelection_5136 = false;

                images_NPC.SetActive(false);
                //��� ����
                StopAllCoroutines();
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
        //������ ����
       EventManager.instance.SelectStart(NPCName.boatman3, 7194);
        yield return null;
    }

    IEnumerator EndingSentence()
    {
        //��� ��Ӱ� ����
        EndingManager.instance.ShowEndingBG();
        EndingManager.instance.BrightEndingBG();

        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[681].npc_name, dialogdb.NPC_01[681].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[682].npc_name, dialogdb.NPC_01[682].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[683].npc_name, dialogdb.NPC_01[683].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[684].npc_name, dialogdb.NPC_01[684].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[685].npc_name, dialogdb.NPC_01[685].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[686].npc_name, dialogdb.NPC_01[686].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[687].npc_name, dialogdb.NPC_01[687].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[688].npc_name, dialogdb.NPC_01[688].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[689].npc_name, dialogdb.NPC_01[689].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[690].npc_name, dialogdb.NPC_01[690].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[691].npc_name, dialogdb.NPC_01[691].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[692].npc_name, dialogdb.NPC_01[692].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[693].npc_name, dialogdb.NPC_01[693].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[694].npc_name, dialogdb.NPC_01[694].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[695].npc_name, dialogdb.NPC_01[695].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[696].npc_name, dialogdb.NPC_01[696].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[697].npc_name, dialogdb.NPC_01[697].comment, true));

        //������ ���� (1.���Ϸ� �پ���, 2.������ �ִ´�)
        EventManager.instance.SelectStart(NPCName.Shimbongsa, 7287);
    }

    //���� ��� ����
    public void StartEndingSentence()
    {
        StartCoroutine(EndingSentence());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }

    #region Previous code
    //IEnumerator NormalChat()
    //{
    //    //��ȭ �ߺ����� ����
    //    remainSentence = true;

    //    string narrator = characternameText = CharacterName.text = npcDatabaseScr.NPC_01[765].npc_name;
    //    string narration = npcDatabaseScr.NPC_01[765].comment;

    //    Debug.Log(RandomNum);


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
    //            ////��ȭ ��
    //            //isSentenceEnd = true;
    //        }

    //        //��簡 ���� ��µ��� �ʾ��� ���
    //        if (a < narration.Length)
    //        {
    //            //��� Ÿ���� �ӵ�
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
    //    //��ȭâ ���̱�
    //    images_NPC.SetActive(true);

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
    //}
    #endregion
}
