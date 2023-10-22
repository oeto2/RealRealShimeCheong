using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Bbang : MonoBehaviour
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
    public GameObject images_Bbang;

    public Sprite[] images_NPC_portrait;

    public Trigger_NPC trigger_npc;

    public Controller controller_scr;

    // ���� ��� ��� ����
    private int RandomNum;

    //��ȭ�� ���� ��� �Ǿ�����
    public bool isSentenceEnd = false;

    //���� ��ȭ�� �� �ִ��� (true�� ��� ���̾�α׸� �������� �ʰ� ������ ����� ���� ��縦 �����)
    public bool remainSentence = false;

    // ���ڻ� ���� ����
    bool t_white = false;
    bool t_red = false;

    // ���ڻ� ���� ���ڴ� ��� ��� ����
    bool t_ignore = false;

    [System.Serializable]
    public struct DialogData
    {
        public int speakerIndex;              // �̸��� ��縦 ����� ���� DialogSystem�� speaker �迭 ����
        public string name;                   // NPC �̸�
        [TextArea(3, 5)]
        public string dialogue;               // ���
    }

    [SerializeField]
    public int index;
    [SerializeField]
    public S_NPCdatabase_Yes dialogdb;
    [SerializeField]
    private DialogData[] dialogs;

    //�ܺ� ��ũ��Ʈ���� ����ϱ� ���� �뵵(�̱�������)
    public static Dialog_TypingWriter_Bbang instance;
    void Start()
    {
        CharacterName.text = "";
        ChatText.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ObjectManager.instance.GetItem(1006);
        }

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
            Debug.Log("zŰ ����! ����!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false && !remainSentence)
            {
                Debug.Log("��ȭ ����");
                images_Bbang.SetActive(true);
                StartCoroutine(TextPractice());
                Trigger_NPC.instance.isNPCTrigger = true;
                //�ʻ�ȭ ����
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
                //bool_isNPC = true;
            }

            //��ȭ�� ������ ���
            else if (isSentenceEnd)
            {
                images_Bbang.SetActive(false);
                // images_NPC_portrait.SetActive(false);
                //��� ����
                writerText = "";
                StopAllCoroutines();
                Trigger_NPC.instance.isNPCTrigger = false;
                bool_isNPC = false;
                //Controller.instance.TalkEnd();
                controller_scr.TalkEnd();
                //������ȭ ����
                remainSentence = false;
                //��ȭ ��
                isSentenceEnd = false;
            }
        }
        //dialogstart();
    }

    public void dialogstart()
    {
        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("zŰ ����! Bbang!!!!");

            controller_scr.TalkStart();
            //bool_isBotjim = true;
            if (bool_isNPC == false)
            {
                StartCoroutine(TextPractice());
                trigger_npc.isNPCTrigger = true;
                images_Bbang.SetActive(true);
                bool_isNPC = true;
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
            }
            else
            {
                //�÷��̾� �̵����� ����
                controller_scr.TalkEnd();

                images_Bbang.SetActive(false);
                bool_isNPC = false;
                trigger_npc.isNPCTrigger = false;

                writerText = "";
                StopAllCoroutines();
            }
        }
    }

    IEnumerator NormalChat()
    {
        //��ȭ �ߺ����� ����
        remainSentence = true;

        string narrator = characternameText = CharacterName.text = dialogdb.NPC_01[1].npc_name;
        string narration = dialogdb.NPC_01[1].comment;
        string narration_2 = dialogdb.NPC_01[399].comment;

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

    //IEnumerator ItemClueChat(string narrator, string narration)
    //{
    //    int a = 0;
    //    CharacterName.text = narrator;
    //    writerText = "";

    //    //�ؽ�Ʈ Ÿ����
    //    for (a = 0; a < narration.Length; a++)
    //    {
    //        writerText += narration[a];
    //        ChatText.text = writerText;

    //        //�ؽ�Ʈ Ÿ���� �ð� ����
    //        //yield return null;
    //        yield return new WaitForSeconds(0.02f);
    //    }

    //    //Ű(default : space)�� �ٽ� ���� ������ ������ ���
    //    while (true)
    //    {
    //        if (isButtonClicked)
    //        {
    //            isButtonClicked = false;
    //            break;
    //        }
    //        yield return null;
    //    }
    //}

    IEnumerator ItemClueChat(string narrator, string narration)
    {
        //������ȭ ����
        remainSentence = true;

        CharacterName.text = narrator;

        Debug.Log(narration);
        int a = 0;

        string t_letter = "";

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
            //writerText += narration[a];
            //ChatText.text = writerText;
            switch (narration[a])
            {
                case '��':
                    t_white = false;
                    t_red = true;
                    t_ignore = true;
                    break;
                case '��':
                    t_white = true;
                    t_red = false;
                    t_ignore = true;
                    break;
            }

            if (!t_ignore)
            {
                if (t_white)
                {
                    t_letter = "<color=#ffffff>" + narration[a] + "</color>";    // HTML Tag
                    Debug.Log("0_write");
                }

                else if (t_red)
                {
                    t_letter = "<color=#B40404>" + narration[a] + "</color>";
                    Debug.Log("1_red");
                }
                //Debug.Log(writerText);
                writerText += t_letter; // Ư�����ڰ� �ƴ϶�� ��� ���
                ChatText.text = writerText;
                //writerText += narration[a];
                //ChatText.text = writerText;
            }
            t_ignore = false; // �� ���� ������� �ٽ� false

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

    IEnumerator TextPractice()
    {
        #region �ܼ�
        //2000 : �»���� �����
        if (ObjectManager.instance.GetEquipObjectKey() == 2000)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[9].npc_name, dialogdb.NPC_01[9].comment));
        }
        //2001 : û���� ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[22].npc_name, dialogdb.NPC_01[22].comment));
        }
        //2002 : û���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[30].npc_name, dialogdb.NPC_01[30].comment));
        }
        //2003 : û�̿� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[38].npc_name, dialogdb.NPC_01[38].comment));
        }
        //2004 : û�̿� �系
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            //��� �̺�Ʈ Ȱ��ȭ
            EventManager.instance.EventActive(Events.binyeo);
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[536].npc_name, dialogdb.NPC_01[536].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[46].npc_name, dialogdb.NPC_01[46].comment));
        }
        //2005 : �������� �Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[57].npc_name, dialogdb.NPC_01[57].comment));
        }
        //2006 : �۳��� ���ΰ� û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[65].npc_name, dialogdb.NPC_01[65].comment));
        }
        //2007 : �·��� û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
        {
            //�·��� ���� �ܼ� ȹ��
            ObjectManager.instance.GetClue(2008);

            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[539].npc_name, dialogdb.NPC_01[539].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[78].npc_name, dialogdb.NPC_01[78].comment));
        }
        //2008 : �·��� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[86].npc_name, dialogdb.NPC_01[86].comment));
        }
        //2009 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[94].npc_name, dialogdb.NPC_01[94].comment));
        }
        //2010 : ����� ��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[104].npc_name, dialogdb.NPC_01[104].comment));
        }
        //2011 : ������� ��ó
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[113].npc_name, dialogdb.NPC_01[113].comment));
        }
        //2012 : û���� �ŷ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[121].npc_name, dialogdb.NPC_01[121].comment));
        }
        //2013 : �⸮ �� ��° �Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[129].npc_name, dialogdb.NPC_01[129].comment));
        }
        /*//2014 : ���������� �� ����, 2020 : ������� �־��� ��, 2022 : ����� ����� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            // �⺻ ���� ����, ���� ����������
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[137].npc_name, dialogdb.NPC_01[137].comment));
        }*/
        //2015 : û�̰� �簣 ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[145].npc_name, dialogdb.NPC_01[145].comment));
        }
        //2016 : ¤���� �簣 û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[155].npc_name, dialogdb.NPC_01[155].comment));
        }
        //2017 : ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[163].npc_name, dialogdb.NPC_01[163].comment));
        }
        //2018 : ������ �ܰ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[182].npc_name, dialogdb.NPC_01[182].comment));
        }
        //2019 : ���� �ʴ� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[190].npc_name, dialogdb.NPC_01[190].comment));
        }
        //2021 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[207].npc_name, dialogdb.NPC_01[207].comment));
        }
        //2023 : 3�� ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[229].npc_name, dialogdb.NPC_01[229].comment));
        }

        #endregion

        #region ������
        //1000 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[283].npc_name, dialogdb.NPC_01[283].comment));
        }
        //1001 : ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 1001)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[249].npc_name, dialogdb.NPC_01[78].comment));
        }
        //1005 : �ָԹ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[292].npc_name, dialogdb.NPC_01[292].comment));
        }
        //1006 : ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            //��� ������Ʈ ����
            ObjectManager.instance.RemoveItem(1006);

            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[526].npc_name, dialogdb.NPC_01[526].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[54].npc_name, dialogdb.NPC_01[54].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[55].npc_name, dialogdb.NPC_01[55].comment, true));

            //�۳��� ���ΰ� û�� �ܼ� ȹ��
            ObjectManager.instance.GetClue(2006);
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[56].npc_name, dialogdb.NPC_01[56].comment));
        }
        //1007 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[307].npc_name, dialogdb.NPC_01[307].comment));
        }
        //1009 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[322].npc_name, dialogdb.NPC_01[322].comment));
        }
        //1011 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[338].npc_name, dialogdb.NPC_01[338].comment));
        }

        #endregion

        #region ���� �ܼ�
        //4023 : ����̸� ���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[349].npc_name, dialogdb.NPC_01[349].comment));
        }
        //4015 : û�̰� ����� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[357].npc_name, dialogdb.NPC_01[357].comment));
        }
        //4017 : û�̿� ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[365].npc_name, dialogdb.NPC_01[365].comment));
        }
        //8032 : �Բ� ����� �� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[373].npc_name, dialogdb.NPC_01[373].comment));
        }
        //4033 : ������ �ߴ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[381].npc_name, dialogdb.NPC_01[381].comment));
        }
        //4018 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[391].npc_name, dialogdb.NPC_01[391].comment));
        }
        #endregion

        #region �⺻ ���
        else
        {
            yield return StartCoroutine(NormalChat());
        }
        #endregion
    }

}