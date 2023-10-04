using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Jangjieon : MonoBehaviour
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

    // ���� ��� ��� ����
    private int RandomNum;

    //���ʿ��� ��µǵ��� �ϴ� Ȯ�ο�
    public bool isNPC_Start = true;

    //��ȭ�� ���� ��� �Ǿ�����
    public bool isSentenceEnd = false;

    //���� ��ȭ�� �� �ִ���
    public bool remainSentence = false;

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

    //���� Ŭ��
    void Start()
    {
        CharacterName.text = "";
        ChatText.text = "";
    }

    void Awake()
    {
        for (int i = 4999; i < dialogdb.NPC_01.Count; ++i)
        {
            if (dialogdb.NPC_01[i].index_num == index)
            {
                dialogs[index].name = dialogdb.NPC_01[i].npc_name;
                dialogs[index].dialogue = dialogdb.NPC_01[i].comment;
                index++;
            }
        }
    }

    public void Update()
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
            Debug.Log("zŰ ����! ������!!!!");

            controller_scr.TalkStart();
            if (bool_isNPC == false && !remainSentence)
            {
                StartCoroutine(TextPractice());
                images_NPC.SetActive(true);
                bool_isNPC = true;
                Trigger_NPC.instance.isNPCTrigger = true;
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
            }
            else if (isSentenceEnd)
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
                //������ȭ ����
                remainSentence = false;
                //��ȭ ��
                isSentenceEnd = false;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        isNPCTrigger = true;
        if (other.CompareTag("Player"))
        {
            OnClickdown();
        }
    }
    public void OnClickdown()
    {
        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("�̰� Touch! ������!!!!");
            //StartCoroutine(TextPractice());
            //bool_isBotjim = true;
            if (bool_isNPC == true)
            {
                Controller.instance.TalkStart();
                images_NPC.SetActive(true);
                bool_isNPC = false;

                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
            }
            else
            {
                images_NPC.SetActive(false);
                // images_NPC_portrait.SetActive(false);
                bool_isNPC = true;
                //StopCoroutine(TextPractice());
                Controller.instance.TalkEnd();
            }
        }
    }

    IEnumerator NormalChat_4999(string narrator, string narration)
    {
        int a = 0;
        /*
        // ���ڻ� ���� ����
        bool t_white = false;
        bool t_red = false;

        // ���ڻ� ���� ���ڴ� ��� ��� ����
        bool t_ignore = false;
        */
        //CharacterName.text = narrator;
        narrator = characternameText = dialogdb.NPC_01[563].npc_name;
        //CharacterName.text = narrator;
        writerText = dialogdb.NPC_01[0].comment;
        Debug.Log(characternameText);
        //writerText = "";

        //narrator = CharacterName.text;
        //yield return null;
        //�ؽ�Ʈ Ÿ����
        for (a = 0; a < narration.Length; a++)
        //for (a = 0; a < 62; a++)
        {
            /*string t_letter = narration[a].ToString();
            //string t_letter;
            switch (narration[a])
            {
                case '��':
                    t_white = false;
                    t_red = true;
                    t_ignore = true;
                    break;
                //case '��':
                    //t_white = true;
                    //t_red = false;
                    //t_ignore = true;
                    break;
            }
            if (t_ignore==true)
            {
                if (t_white)
                {
                    t_letter = "<color=#ffffff>" + narration[a] + "</color>";    // HTML Tag
					Debug.Log(t_letter);
                    Debug.Log('1');

				}

				else if (t_red)
                {
                    t_letter = "<color=#B40404>" + narration[a] + "</color>";
                    Debug.Log(t_letter);
                    Debug.Log('2');
                }
                Debug.Log(writerText);
                //ChatText.text = writerText;
                //writerText += t_letter; // Ư�����ڰ� �ƴ϶�� ��� ���
                //writerText += narration[a];
                //ChatText.text = writerText;
                //t_ignore = false; // �� ���� ������� �ٽ� false
            }*/

            writerText += narration[a];
            ChatText.text = writerText;
            //t_ignore = false; // �� ���� ������� �ٽ� false
            //ChatText.text = t_letter;
            //writerText += t_letter; // Ư�����ڰ� �ƴ϶�� ��� ���
            //ChatText.text = writerText;
            //�ؽ�Ʈ Ÿ���� �ð� ����
            yield return new WaitForSeconds(0.07f);
        }
        yield return null;
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

    IEnumerator NormalChat()
    {
        int a = 0;
        string narrator = characternameText = CharacterName.text = dialogdb.NPC_01[563].npc_name;
        string narration = dialogdb.NPC_01[603].comment;
        isNPC_Start = false;
        //narrator = CharacterName.text;


        for (a = 0; a < narration.Length; a++)
        //for (a = 0; a < textSpeed; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            //�ؽ�Ʈ Ÿ���� �ð� ����
            //yield return null;

            if (Input.GetKeyDown(KeyCode.Z))
            {
                //������ȭ ����
                remainSentence = true;
                //��ȭ ��
                isSentenceEnd = true;
            }

            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
        Debug.Log(writerText);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //��ȭ ��
            isSentenceEnd = true;
        }
        Debug.Log(writerText);

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

    //�����ε�
    IEnumerator ItemClueChat(string narrator, string narration)
    {
        //���̾�α�â ����
        images_NPC.SetActive(true);

        //ù��� �÷��� false
        isNPC_Start = false;

        //������ȭ ����
        remainSentence = true;

        Debug.Log(narration);
        int a = 0;
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
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            if (Input.GetKeyDown(KeyCode.Z))
            {
                //������ȭ ����
                remainSentence = true;
                //��ȭ ��
                isSentenceEnd = true;
            }

            //�ؽ�Ʈ Ÿ���� �ð� ����
            //yield return null;

            yield return new WaitForSeconds(0.02f);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //��ȭ ��
            isSentenceEnd = true;
        }

        //ZŰ�� �ٽ� ���� ������ ������ ���
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //������ȭ ����
                remainSentence = true;
                //��ȭ ��
                isSentenceEnd = true;
                break;
            }
            yield return null;
        }
    }

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

            Debug.Log(narration);
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

                //�߰��� ZŰ�� ������
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    break;
                }
            }

            //ZŰ�� �ٽ� ���� ������ ������ ���
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
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
        //2001 : �⸮�� ���� �� 
        if (ObjectManager.instance.GetEquipObjectKey() == 2000)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[563].npc_name, dialogdb.NPC_01[563].comment,true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[564].npc_name, dialogdb.NPC_01[564].comment));
        }
        //2001 : û���� ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[565].npc_name, dialogdb.NPC_01[565].comment));
        }
        //2002 : û���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[566].npc_name, dialogdb.NPC_01[566].comment));
        }
        //2004 : û�̿� �系
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[567].npc_name, dialogdb.NPC_01[567].comment));
        }
        //2005 : �������� �Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[568].npc_name, dialogdb.NPC_01[568].comment));
        }
        //2006 : �۳��� ���ΰ� û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[569].npc_name, dialogdb.NPC_01[569].comment));
        }
        //2007 : �·��� û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[570].npc_name, dialogdb.NPC_01[570].comment));
        }
        //2008 : �·��� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[571].npc_name, dialogdb.NPC_01[571].comment));
        }
        //2009 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[572].npc_name, dialogdb.NPC_01[572].comment));
        }
        //2010 : ����� ��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[573].npc_name, dialogdb.NPC_01[573].comment));
        }
        //2011 : ������� ��ó
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[574].npc_name, dialogdb.NPC_01[574].comment,true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[574].npc_name, dialogdb.NPC_01[575].comment));
        }
        //2012 : û���� �ŷ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[576].npc_name, dialogdb.NPC_01[576].comment));
        }
        //2013 : �⸮�� ��° �Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[577].npc_name, dialogdb.NPC_01[577].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[578].npc_name, dialogdb.NPC_01[578].comment));
        }
        //2014 : ���������� �� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            // �⺻ ���� ����, ���� ����������
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[579].npc_name, dialogdb.NPC_01[579].comment));
        }
        //2015 : û�̰� �簣 ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[580].npc_name, dialogdb.NPC_01[580].comment));
        }
        //2016 : ¤���� �簣 û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[581].npc_name, dialogdb.NPC_01[581].comment,true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[582].npc_name, dialogdb.NPC_01[582].comment));
        }
        //2017 : ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[583].npc_name, dialogdb.NPC_01[583].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[584].npc_name, dialogdb.NPC_01[584].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[585].npc_name, dialogdb.NPC_01[585].comment));

        }
        //2018 : ������ �ܰ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[586].npc_name, dialogdb.NPC_01[586].comment));
        }
        //2019 : ���� �ʴ� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[587].npc_name, dialogdb.NPC_01[587].comment));
        }
        //2020 : ������� �־��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[588].npc_name, dialogdb.NPC_01[588].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[589].npc_name, dialogdb.NPC_01[589].comment));
        }
        //2021 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[590].npc_name, dialogdb.NPC_01[590].comment));
        }
        //2022 : ����� ����� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[591].npc_name, dialogdb.NPC_01[591].comment));
        }
        //2023 : 3�� ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[592].npc_name, dialogdb.NPC_01[592].comment));
        }
        #endregion

        #region ������
        
        #endregion

        #region ���� �ܼ�
        //4023 : ����̸� ���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[593].npc_name, dialogdb.NPC_01[593].comment));
        }
        //4015 : û�̰� ����� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[594].npc_name, dialogdb.NPC_01[594].comment));
        }
        //4017 : û�̿� ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[595].npc_name, dialogdb.NPC_01[595].comment));
        }
        //8032 : �Բ� ����� �� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[596].npc_name, dialogdb.NPC_01[596].comment));
        }
        //4033 : ������ �ߴ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[597].npc_name, dialogdb.NPC_01[597].comment));
        }
        //4018 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[598].npc_name, dialogdb.NPC_01[598].comment));
        }
        //6045 : �ٴ��� ������ ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 6045)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[599].npc_name, dialogdb.NPC_01[599].comment,true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[600].npc_name, dialogdb.NPC_01[600].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[601].npc_name, dialogdb.NPC_01[601].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[602].npc_name, dialogdb.NPC_01[602].comment));
        }
        #endregion

        #region �⺻ ���
        else
        {
            yield return StartCoroutine(NormalChat());
        }
        #endregion
        //yield return StartCoroutine(NormalChat(characternameText, writerText));
        //if(index == 4999)
        //{
        //yield return StartCoroutine(NormalChat_4999(characternameText, writerText)); 
        //yield return StartCoroutine(NormalChat_2(characternameText, writerText));
        //}
        //yield return StartCoroutine(NormalChat(characternameText, writerText));
        //yield return StartCoroutine(NormalChat("���º���", "?�ȳ��ϼ���, �ݰ����ϴ�. ��ȭ ��ȯ �׽�Ʈ�Դϴ� �̰��� �׽�Ʈ��? �׷� �׽�Ʈ�� �׽�Ʈ�� �׽�Ʈ�� �׽�Ʈ"));
    }
}