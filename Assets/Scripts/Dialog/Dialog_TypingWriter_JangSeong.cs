using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_JangSeong : MonoBehaviour
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

    //��ȭ�� ���� ��� �Ǿ�����
    public bool isSentenceEnd = false;

    //���� ��ȭ�� �� �ִ���
    public bool remainSentence = false;

    // ���� ��� ��� ����
    private int RandomNum;

    // ������ UI ���
    public GameObject Canvas_Selection_UI;

    // ������ �߻�!
    public Text Selection_Text_Name;

    // ������ 1 ��� �ؽ�Ʈ
    public Text Selection_Text1;

    // ������ 2 ��� �ؽ�Ʈ
    public Text Selection_Text2;

    // ������ Ȯ�� ����
    public bool isSelection_yes = false;
    public bool isSelection_no = false;

    public bool isSelection_2023;

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
        /*
        Selection_Text_Name.text = "������ �߻�!";
        Selection_Text1.text = "��";
        Selection_Text2.text = "�ƴϿ�";
        */
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
            Debug.Log("zŰ ����! ��»��!!!!");
            //bool_isBotjim = true;
            //�÷��̾� �̵�����
            controller_scr.TalkStart();

            if (bool_isNPC == false && !remainSentence)
            {

                //������ UI
                Canvas_Selection_UI.SetActive(false);

                //���̾�α� UI
                images_NPC.SetActive(true);

                //��� ���
                StartCoroutine(TextPractice());

                //bool_isNPC = true;
                //Trigger_NPC.instance.isNPCTrigger = true;
                //GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];

                //������ ���̱�
                if (ObjectManager.instance.GetEquipObjectKey() == 2023 && isSelection_2023 == false)
                {
                    Debug.Log("�⸮�� ������ ON");

                    Canvas_Selection_UI.SetActive(true);
                    isSelection_2023 = true;

                    images_NPC.SetActive(false);
                    bool_isNPC = true;
                    trigger_npc.isNPCTrigger = false;
                }
            }
            
            //��ȭ ����
            else if (isSentenceEnd)
            {
                //�÷��̾� �̵����� ����
                controller_scr.TalkEnd();

                isSelection_2023 = false;

                images_NPC.SetActive(false);
                bool_isNPC = false;
                trigger_npc.isNPCTrigger = false;

                writerText = "";
                StopAllCoroutines();
                

                 //������ȭ ����
                remainSentence = false;
                //��ȭ ��
                isSentenceEnd = false;
            }
        }
    }

    IEnumerator NormalChat()
    {
        //�ʻ�ȭ ����
        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];

        int a = 0;
        string narrator = characternameText = CharacterName.text = dialogdb.NPC_01[3].npc_name;
        string narration = dialogdb.NPC_01[3].comment;
        string narration_2 = dialogdb.NPC_01[401].comment;
        RandomNum = Random.Range(0, 2);

        //narrator = CharacterName.text;

        //�ؽ�Ʈ Ÿ����
        if (RandomNum == 0)
        {
            for (a = 0; a < narration.Length; a++)
            //for (a = 0; a < textSpeed; a++)
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
                yield return new WaitForSeconds(0.05f);
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
                yield return new WaitForSeconds(0.05f);
            }
            yield return null;
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

    IEnumerator ItemClueChat(string narrator, string narration)
    {
        //������ȭ ����
        remainSentence = true;

        //�ʻ�ȭ ����
        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];


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

        //if (ObjectManager.instance.GetEquipObjectKey() == 2000) //test��

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //������ȭ ����
            remainSentence = true;
            //��ȭ ��
            isSentenceEnd = true;
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

    }

    public void onClick_yes()
	{
        if (isSelection_2023 == true)
        {
            Canvas_Selection_UI.SetActive(false);

            isSelection_yes = true;
            isSelection_no = false;
            isSelection_2023 = false;

            images_NPC.SetActive(true);
            bool_isNPC = true;
            Trigger_NPC.instance.isNPCTrigger = true;
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
        }
    }

    public void onClick_no()
    {
        Canvas_Selection_UI.SetActive(false);

        isSelection_yes = false;
        isSelection_no = true;

        images_NPC.SetActive(true);
        bool_isNPC = true;
        Trigger_NPC.instance.isNPCTrigger = true;
        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];

    }

    IEnumerator TextPractice()
    {
        

        #region �ܼ�
        //2000 : �»���� �����
        if (ObjectManager.instance.GetEquipObjectKey() == 2000)
        {
            //�ܼ�ȹ��
            ObjectManager.instance.GetClue(2001);
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[11].npc_name, dialogdb.NPC_01[11].comment));
        }
        //2001 : û���� ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[24].npc_name, dialogdb.NPC_01[24].comment));
        }
        //2002 : û���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[32].npc_name, dialogdb.NPC_01[32].comment));
        }
        //2003 : û�̿� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[40].npc_name, dialogdb.NPC_01[40].comment));
        }
        //2004 : û�̿� �系
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[48].npc_name, dialogdb.NPC_01[48].comment));
        }
        //2005 : �������� �Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            //�⸮�� ��° �Ƶ� �ܼ� ȹ��
            ObjectManager.instance.GetClue(2013);

            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[59].npc_name, dialogdb.NPC_01[59].comment));
        }
        //2006 : �۳��� ���ΰ� û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[67].npc_name, dialogdb.NPC_01[67].comment));
        }
        //2007 : �·��� û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[80].npc_name, dialogdb.NPC_01[80].comment));
        }
        //2008 : �·��� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[88].npc_name, dialogdb.NPC_01[88].comment));
        }
        //2009 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[96].npc_name, dialogdb.NPC_01[96].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[102].npc_name, dialogdb.NPC_01[102].comment, true));

            //���� ���� �ܼ�ȹ��
            ObjectManager.instance.GetClue(2017);

            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[103].npc_name, dialogdb.NPC_01[103].comment));
        }
        //2010 : ����� ��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[106].npc_name, dialogdb.NPC_01[106].comment));
        }
        //2011 : ������� ��ó
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[115].npc_name, dialogdb.NPC_01[115].comment));
        }
        //2012 : û���� �ŷ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[123].npc_name, dialogdb.NPC_01[123].comment));
        }
        //2013 : �⸮ �� ��° �Ƶ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[131].npc_name, dialogdb.NPC_01[131].comment));
        }
        //2014 : ���������� �� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[139].npc_name, dialogdb.NPC_01[139].comment));
        }
        //2015 : û�̰� �簣 ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[147].npc_name, dialogdb.NPC_01[147].comment));
        }
        //2016 : ¤���� �簣 û��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[157].npc_name, dialogdb.NPC_01[157].comment));
        }
        //2017 : ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[167].npc_name, dialogdb.NPC_01[167].comment));
        }
        //2018 : ������ �ܰ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[184].npc_name, dialogdb.NPC_01[184].comment));
        }
        //2019 : ���� �ʴ� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[192].npc_name, dialogdb.NPC_01[192].comment));
        }
        //2020 : ������� �־��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[201].npc_name, dialogdb.NPC_01[201].comment));
        }
        //2021 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[207].npc_name, dialogdb.NPC_01[207].comment));
        }
        //2022 : ����� ����� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[223].npc_name, dialogdb.NPC_01[223].comment));
        }
        //2023 : 3�� ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[231].npc_name, dialogdb.NPC_01[231].comment));
            isSelection_2023 = true;
        }
        #endregion

        #region ������
        //1000 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[285].npc_name, dialogdb.NPC_01[285].comment));
        }
        //1005 : �ָԹ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[294].npc_name, dialogdb.NPC_01[294].comment));
        }
        //1007 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            //�� ������ ����
            ObjectManager.instance.RemoveItem(1007);
            //�� ���� �Ϸ�
            EventManager.instance.eventProgress.deliveryMuck = true;
            //�� ���� �̺�Ʈ �Ϸ�
            EventManager.instance.eventEndCheck.muckEvent_End = true;

            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[309].npc_name, dialogdb.NPC_01[309].comment));
        }
        //1009 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[324].npc_name, dialogdb.NPC_01[324].comment));
        }
        //1011 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[340].npc_name, dialogdb.NPC_01[340].comment));
        }

        #endregion

        #region ���� �ܼ�
        //4023 : ����̸� ���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[351].npc_name, dialogdb.NPC_01[351].comment));
        }
        //4015 : û�̰� ����� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[359].npc_name, dialogdb.NPC_01[359].comment));
        }
        //4017 : û�̿� ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[367].npc_name, dialogdb.NPC_01[367].comment));
        }
        //8032 : �Բ� ����� �� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[375].npc_name, dialogdb.NPC_01[375].comment));
        }
        //4033 : ������ �ߴ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[383].npc_name, dialogdb.NPC_01[383].comment));
        }
        //4018 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[393].npc_name, dialogdb.NPC_01[393].comment));
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