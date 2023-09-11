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

    // ���� ��� ��� ����
    private int RandomNum;

    //��ȭ�� ���� ��� �Ǿ�����
    public bool isSentenceEnd = false;

    //���� ��ȭ�� �� �ִ���
    public bool remainSentence = false;

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
            Debug.Log("zŰ ����! �۳���!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false && !remainSentence)
            {
                Debug.Log("��ȭ ����");
                images_NPC.SetActive(true);
                StartCoroutine(TextPractice());
                Trigger_NPC.instance.isNPCTrigger = true;
                //�ʻ�ȭ ����
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
                //bool_isNPC = true;
            }

            //��ȭ�� ������ ���
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


    IEnumerator NormalChat()
    {
        int a = 0;
        string narrator = characternameText = CharacterName.text = npcDatabaseScr.NPC_01[8].npc_name;
        string narration = npcDatabaseScr.NPC_01[8].comment;
        string narration_2 = npcDatabaseScr.NPC_01[405].comment;
        string narration_3 = npcDatabaseScr.NPC_01[406].comment;


        RandomNum = Random.Range(0, 3);
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

        else if (RandomNum == 2)
        {
            for (a = 0; a < narration_3.Length; a++)
            //for (a = 0; a < textSpeed; a++)
            {
                writerText += narration_3[a];
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //��ȭ ��
            isSentenceEnd = true;
        }

        Debug.Log(writerText);
        //writerText = "";

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


    IEnumerator TextPractice()
    {
        #region �ܼ�

        //2005 : �������� �Ƶ�
        if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            //������������ ���� �ܼ� ȹ��
            ObjectManager.instance.GetClue(2014);

            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[64].npc_name, npcDatabaseScr.NPC_01[64].comment));
        }

        //2006 : �۳��� ���ΰ� û��
        if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            //û���� ���� �ܼ� ȹ��
            ObjectManager.instance.GetClue(2009);

            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[72].npc_name, npcDatabaseScr.NPC_01[72].comment));
        }

        //2009 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[101].npc_name, npcDatabaseScr.NPC_01[101].comment));
        }

        //2011 : ������� ��ó
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[120].npc_name, npcDatabaseScr.NPC_01[120].comment));
        }

        //2012 : û���� �ŷ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[128].npc_name, npcDatabaseScr.NPC_01[128].comment));
        }

        //2014 : ������������ ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[144].npc_name, npcDatabaseScr.NPC_01[144].comment));
        }

        //2015 : û�̰� �簣 ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[152].npc_name, npcDatabaseScr.NPC_01[152].comment));
        }

        //2017 : ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[170].npc_name, npcDatabaseScr.NPC_01[170].comment));
        }

        //2018 : ������ �ܰ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[189].npc_name, npcDatabaseScr.NPC_01[189].comment));
        }

        //2020 : ������� �־��� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[206].npc_name, npcDatabaseScr.NPC_01[206].comment));
        }

        //2021 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[214].npc_name, npcDatabaseScr.NPC_01[214].comment));
        }

        //2022 : �ٻ� ���ε�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[228].npc_name, npcDatabaseScr.NPC_01[228].comment));
        }

        //2022 : �ٻ� ���ε�
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[228].npc_name, npcDatabaseScr.NPC_01[228].comment));
        }

        //2023 : 3�� ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[248].npc_name, npcDatabaseScr.NPC_01[248].comment));
        }
        #endregion

        #region ������
        //1006 : ��� 
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[306].npc_name, npcDatabaseScr.NPC_01[306].comment));
        }

        //1007 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[314].npc_name, npcDatabaseScr.NPC_01[314].comment));
        }

        //1011 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[347].npc_name, npcDatabaseScr.NPC_01[347].comment));
        }

        #endregion

        #region ���� �ܼ�
        //4023 : ����̸� ���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[356].npc_name, npcDatabaseScr.NPC_01[356].comment));
        }

        //8032 : �Բ� ����� �� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[379].npc_name, npcDatabaseScr.NPC_01[379].comment));
        }

        //4033 : ������ �ߴ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[388].npc_name, npcDatabaseScr.NPC_01[388].comment));
        }

        //4018 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[393].npc_name, npcDatabaseScr.NPC_01[393].comment));
        }
        #endregion

        //�⺻ ���
        else
        {
            yield return StartCoroutine(NormalChat());
        }
    }
}