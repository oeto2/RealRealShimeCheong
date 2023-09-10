using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Budhist : MonoBehaviour
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

        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("zŰ ����! �·�!!!!");

            controller_scr.TalkStart();
            if (bool_isNPC == false && !remainSentence)
            {
                StartCoroutine(TextPractice());
                images_NPC.SetActive(true);
                bool_isNPC = true;
                Trigger_NPC.instance.isNPCTrigger = true;
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
            }
            else if(isSentenceEnd)
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
            Debug.Log("�̰� Touch! �·�!!!!");
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
        narrator = characternameText = dialogdb.NPC_01[0].npc_name;
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
        string narrator = characternameText = CharacterName.text = dialogdb.NPC_01[5].npc_name;
        string narration = dialogdb.NPC_01[5].comment;
        string narration_2 = dialogdb.NPC_01[407].comment;
        RandomNum = Random.Range(0, 2);
        isNPC_Start = false;
        //narrator = CharacterName.text;

        //�ؽ�Ʈ Ÿ����
        if (RandomNum == 0)
        {
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
        }
        else if (RandomNum == 1)
        {
            for (a = 0; a < narration_2.Length; a++)
            //for (a = 0; a < textSpeed; a++)
            {
                writerText += narration_2[a];
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
            yield return null;
        }
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
        // ���� 1ȸ ���
        if (isNPC_Start==true)
        {
            Debug.Log("�·� 1ȸ ��� ���");
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[0].npc_name, dialogdb.NPC_01[0].comment));
            //yield return StartCoroutine(ItemClueChat("11","2222"));
        }

        #region �ܼ�
        //2001 : û���� ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[26].npc_name, dialogdb.NPC_01[26].comment));
        }
        //2002 : û���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[34].npc_name, dialogdb.NPC_01[34].comment));
        }
        //2003 : û�̿� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[42].npc_name, dialogdb.NPC_01[42].comment));
        }
        //2004 : û�̿� �系
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[50].npc_name, dialogdb.NPC_01[50].comment));
        }
        //2005 : �������� �Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[61].npc_name, dialogdb.NPC_01[61].comment));
        }
        //2006 : �۳��� ���ΰ� û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[69].npc_name, dialogdb.NPC_01[69].comment));
        }
        //2007 : �·��� û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[82].npc_name, dialogdb.NPC_01[82].comment));
        }
        //2008 : �·��� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[90].npc_name, dialogdb.NPC_01[90].comment));
        }
        //2009 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[98].npc_name, dialogdb.NPC_01[98].comment));
        }
        //2010 : ����� ��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[108].npc_name, dialogdb.NPC_01[108].comment));
        }
        //2011 : ������� ��ó
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[117].npc_name, dialogdb.NPC_01[117].comment));
        }
        //2014 : ���������� �� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            // �⺻ ���� ����, ���� ����������
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[141].npc_name, dialogdb.NPC_01[141].comment));
        }
        //2015 : û�̰� �簣 ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[149].npc_name, dialogdb.NPC_01[149].comment));
        }
        //2017 : ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[165].npc_name, dialogdb.NPC_01[165].comment));
        }
        //2021 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[211].npc_name, dialogdb.NPC_01[211].comment));
        }
        //2023 : 3�� ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[233].npc_name, dialogdb.NPC_01[233].comment));
        }
        #endregion

        #region ������
        //1000 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[253].npc_name, dialogdb.NPC_01[253].comment));
        }
        //1005 : �ָԹ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[296].npc_name, dialogdb.NPC_01[296].comment));
        }
        //1007 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[311].npc_name, dialogdb.NPC_01[311].comment));
        }
        //1009 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[326].npc_name, dialogdb.NPC_01[326].comment));
        }
        //1011 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[342].npc_name, dialogdb.NPC_01[342].comment));
        }
        #endregion

        #region ���� �ܼ�
        //4023 : ����̸� ���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[353].npc_name, dialogdb.NPC_01[353].comment));
        }
        //4033 : ������ �ߴ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[386].npc_name, dialogdb.NPC_01[386].comment));
        }
        //4018 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[396].npc_name, dialogdb.NPC_01[396].comment));
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