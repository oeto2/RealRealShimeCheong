using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_BoatMan2 : MonoBehaviour
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

    //��ȭ�� ���� ��� �Ǿ�����
    public bool isSentenceEnd = false;

    //���� ��ȭ�� �� �ִ���
    public bool remainSentence = false;

    //�۳��� ���ΰ� û�� ������ ��
    public int int_Select2006Num = 0;

    //2006�� ������ �� ��¥
    public int int_select2006Day = 0;

    //����2 Ʈ���� ��ũ��Ʈ
    public BoatMan2_Trigger boatman2_TriggerScr;

    public S_NPCdatabase_Yes dialogdb;

    //���� Ŭ��
    void Start()
    {
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
             && TutorialManager.instance.SentenceCondition() && boatman2_TriggerScr.isTouch)
        {
            Debug.Log("zŰ ����! ����!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false && !DialogManager.instance.remainSentence)
            {
                Debug.Log("��ȭ ����");
                images_NPC.SetActive(true);
                StartCoroutine(TextPractice2());
                Trigger_NPC.instance.isNPCTrigger = true;
            }

            //��ȭ�� ������ ���
            else if (DialogManager.instance.isSentenceEnd)
            {
                //isSelection_5136 = false;

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


    IEnumerator TextPractice2()
    {
        //�⺻ ��� ����
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[765].npc_name, dialogdb.NPC_01[765].comment, true));

        //������ ����
        EventManager.instance.SelectStart(NPCName.boatman2, 7355);
    }

    //������� ��忣�� �ڷ�ƾ
    IEnumerator BoatManEnding()
    {
        //ȭ�� ��Ӱ� �ϱ�
        EndingManager.instance.ShowEndingBG();

        //��� ����
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[766].npc_name, dialogdb.NPC_01[766].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[767].npc_name, dialogdb.NPC_01[767].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[768].npc_name, dialogdb.NPC_01[768].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[769].npc_name, dialogdb.NPC_01[769].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[770].npc_name, dialogdb.NPC_01[770].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[771].npc_name, dialogdb.NPC_01[771].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[772].npc_name, dialogdb.NPC_01[772].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[773].npc_name, dialogdb.NPC_01[773].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[774].npc_name, dialogdb.NPC_01[774].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[775].npc_name, dialogdb.NPC_01[775].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[776].npc_name, dialogdb.NPC_01[776].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[778].npc_name, dialogdb.NPC_01[778].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[779].npc_name, dialogdb.NPC_01[779].comment));

        //Ÿ��Ʋ ȭ�� �̵�
        EndingManager.instance.LoadTitleScene();
    }

    //��� ���� ��忣�� ����
    public void StartBoatManEnding_1()
    {
        StartCoroutine(BoatManEnding());
    }


    //�� ���� ���� �ڷ�ƾ
    IEnumerator GoodEndingRoot()
    {
        //�ð��� �� �̻� �帣�� ����
        TimeManager.instance.RealTimeStop();

        //ȭ�� ��Ӱ� �ϱ�
        EndingManager.instance.ShowEndingBG();

        //��� ����
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[240].npc_name, dialogdb.NPC_01[240].comment, true));

        //�ɺ���, ��ġ ����
        GameManager.instance.TransferPlayer(GameManager.instance.oceanSponPos.position, 6);

        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[241].npc_name, dialogdb.NPC_01[241].comment, true));

        //��� õõ�� ����ϱ�
        EndingManager.instance.BrightEndingBG();

        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[242].npc_name, dialogdb.NPC_01[242].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[243].npc_name, dialogdb.NPC_01[243].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[244].npc_name, dialogdb.NPC_01[244].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[245].npc_name, dialogdb.NPC_01[245].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[246].npc_name, dialogdb.NPC_01[246].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[247].npc_name, dialogdb.NPC_01[247].comment, true));

        //���̾�α�â ����
        DialogManager.instance.Dialouge_System.SetActive(false);
        remainSentence = true;
        isSentenceEnd = true;
        controller_scr.TalkEnd();
    }

    //��� ���� ��忣�� ����
    public void StartGoodEndingRoot()
    {
        StartCoroutine(GoodEndingRoot());
    }

    #region Previous Code

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