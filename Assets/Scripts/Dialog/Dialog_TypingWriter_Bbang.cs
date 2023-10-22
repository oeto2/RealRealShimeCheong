using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Bbang : MonoBehaviour
{
    // 실제 채팅이 나오는 텍스트
    public Text ChatText;

    // 캐릭터 이름이 나오는 텍스트
    public Text CharacterName;

    // 대화를 빠르게 넘길 수 있는 키(default : space)
    public List<KeyCode> skipButton;

    public string writerText = "";

    public string characternameText = "";

    bool isButtonClicked = false;

    public bool bool_isNPC = false;

    //다이얼로그 UI
    public GameObject images_Bbang;

    public Sprite[] images_NPC_portrait;

    public Trigger_NPC trigger_npc;

    public Controller controller_scr;

    // 랜덤 대사 출력 변수
    private int RandomNum;

    //대화가 전부 출력 되었는지
    public bool isSentenceEnd = false;

    //남은 대화가 더 있는지
    public bool remainSentence = false;

    // 글자색 설정 변수
    bool t_white = false;
    bool t_red = false;

    // 글자색 설정 문자는 대사 출력 무시
    bool t_ignore = false;

    [System.Serializable]
    public struct DialogData
    {
        public int speakerIndex;              // 이름과 대사를 출력할 현재 DialogSystem의 speaker 배열 순번
        public string name;                   // NPC 이름
        [TextArea(3, 5)]
        public string dialogue;               // 대사
    }

    [SerializeField]
    public int index;
    [SerializeField]
    public S_NPCdatabase_Yes dialogdb;
    [SerializeField]
    private DialogData[] dialogs;

    //외부 스크립트 참조
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

        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger && UIManager.instance.SentenceCondition()
            && TutorialManager.instance.SentenceCondition())
        {
            Debug.Log("z키 누름! 뺑덕어멈!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false && !remainSentence)
            {
                Debug.Log("뺑덕 대사 실행");
                images_Bbang.SetActive(true);
                StartCoroutine(TextPractice());
                Trigger_NPC.instance.isNPCTrigger = true;
                //대사 출력
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
                //bool_isNPC = true;
            }

            //대화 종료
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
                
                //남은대화 없음
                remainSentence = false;
                //대화 끝
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
        //대화 중복실행 방지
        remainSentence = true;

        string narrator = characternameText = CharacterName.text = dialogdb.NPC_01[1].npc_name;
        string narration = dialogdb.NPC_01[1].comment;
        string narration_2 = dialogdb.NPC_01[399].comment;

        RandomNum = Random.Range(0, 2);
        Debug.Log(RandomNum);

        //텍스트 타이핑
        if (RandomNum == 0)
        {
            for (int a = 0; a < narration.Length; a++)
            {
                writerText += narration[a];
                ChatText.text = writerText;

                //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
                if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
                {
                    ChatText.text = narration;

                    //남은대화 없음
                    remainSentence = true;
                    //대화 끝
                    isSentenceEnd = true;

                    //for문 조건 충족
                    a = narration.Length;
                    ////대화 끝
                    //isSentenceEnd = true;
                }

                //대사가 전부 출력되지 않았을 경우
                if (a < narration.Length)
                {
                    //대사 타이핑 속도
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

                //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
                if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
                {
                    ChatText.text = narration_2;

                    //남은대화 없음
                    remainSentence = true;
                    //대화 끝
                    isSentenceEnd = true;

                    //for문 조건 충족
                    a = narration_2.Length;
                    ////대화 끝
                    //isSentenceEnd = true;
                }

                //대사가 전부 출력되지 않았을 경우
                if (a < narration_2.Length)
                {
                    yield return new WaitForSeconds(0.02f);
                }
            }
            yield return null;
        }
        Debug.Log(writerText);

        //대사 출력이 모두 완료 되었다면
        if (ChatText.text == narration || ChatText.text == narration_2)
        {
            //대화 종료 조건 충족
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
        //남은대화 있음
        remainSentence = true;

        CharacterName.text = narrator;

        Debug.Log(narration);
        int a = 0;

        string t_letter = "";

        //심학규의 대사일 경우
        if (narrator == "심학규")
        {
            //초상화 변경
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[1];
        }
        else
        {
            //초상화 변경
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
        }

        //텍스트 타이핑
        for (a = 0; a < narration.Length; a++)
        {
            //writerText += narration[a];
            //ChatText.text = writerText;
            switch (narration[a])
            {
                case 'ⓡ':
                    t_white = false;
                    t_red = true;
                    t_ignore = true;
                    break;
                case 'ⓦ':
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
                writerText += t_letter; // 특수문자가 아니라면 대사 출력
                ChatText.text = writerText;
                //writerText += narration[a];
                //ChatText.text = writerText;
            }
            t_ignore = false; // 한 글자 찍었으면 다시 false

            //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
            {
                ChatText.text = narration;

                //남은대화 없음
                remainSentence = true;
                //대화 끝
                isSentenceEnd = true;

                //for문 조건 충족
                a = narration.Length;
            }

            //대사 출력 중일 경우에만
            if (ChatText.text != narration)
            {
                //텍스트 타이핑 시간 조절
                yield return new WaitForSeconds(0.02f);
            }
        }

        //대사 출력이 모두 완료 되었다면
        if (ChatText.text == writerText)
        {
            //대화 종료 조건 충족
            remainSentence = true;
            isSentenceEnd = true;
        }
    }


    //오버로드
    IEnumerator ItemClueChat(string narrator, string narration, bool _remainSentence)
    {
        string t_letter = "";

        //심학규의 대사일 경우
        if (narrator == "심학규")
        {
            //초상화 변경
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[1];
        }
        else
        {
            //초상화 변경
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
        }

        //남은 대화가 있을경우
        if (_remainSentence == true)
        {
            //남은대화 있음
            remainSentence = true;

            CharacterName.text = narrator;

            //텍스트 타이핑
            for (int a = 0; a < narration.Length; a++)
            {
                //writerText += narration[a];
                //ChatText.text = writerText;

                switch (narration[a])
                {
                    case 'ⓡ':
                        t_white = false;
                        t_red = true;
                        t_ignore = true;
                        break;
                    case 'ⓦ':
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
                    writerText += t_letter; // 특수문자가 아니라면 대사 출력
                    //writerText += narration[a];
                    ChatText.text = writerText;
                    //ChatText.text = writerText;
                }
                t_ignore = false; // 한 글자 찍었으면 다시 false

                //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
                if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
                {
                    writerText = narration;
                    ChatText.text = narration;

                    //남은대화 없음
                    remainSentence = true;
                    ////대화 끝
                    //isSentenceEnd = true;

                    //for문 조건 충족
                    a = narration.Length;
                }

                //대사 출력 중일 경우에만
                if (ChatText.text != narration)
                {
                    //텍스트 타이핑 시간 조절
                    yield return new WaitForSeconds(0.02f);
                }

            }

            //대사 출력 후 잠깐 딜레이
            yield return new WaitForSeconds(0.1f);

            //Z키를 다시 누를 때까지 무한정 대기
            while (true)
            {
                if (ChatText.text == writerText && Input.GetKeyDown(KeyCode.Z))
                {
                    Debug.Log("Text 비우기");

                    //Text 비우기
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