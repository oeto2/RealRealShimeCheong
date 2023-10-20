using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Songnara : MonoBehaviour
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
    public GameObject images_NPC;

    public Sprite[] images_NPC_portrait;

    public Trigger_NPC trigger_npc;

    public bool isNPCTrigger;

    public Controller controller_scr;

    public S_NPCdatabase_Yes npcDatabaseScr;

    // 랜덤 대사 출력 변수
    private int RandomNum;

    //대화가 전부 출력 되었는지
    public bool isSentenceEnd = false;

    //남은 대화가 더 있는지
    public bool remainSentence = false;

    //최초 클릭
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
            Debug.Log("z키 누름! 송나라!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false && !remainSentence)
            {
                Debug.Log("대화 실행");
                images_NPC.SetActive(true);
                StartCoroutine(TextPractice());
                Trigger_NPC.instance.isNPCTrigger = true;
                //초상화 변경
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
                //bool_isNPC = true;
            }

            //대화가 끝났을 경우
            else if (isSentenceEnd)
            {
                images_NPC.SetActive(false);
                // images_NPC_portrait.SetActive(false);
                //대사 비우기
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
    }


    IEnumerator NormalChat()
    {
        //대화 중복실행 방지
        remainSentence = true;

        string narrator = characternameText = CharacterName.text = npcDatabaseScr.NPC_01[8].npc_name;
        string narration = npcDatabaseScr.NPC_01[8].comment;
        string narration_2 = npcDatabaseScr.NPC_01[405].comment;
        string narration_3 = npcDatabaseScr.NPC_01[406].comment;


        RandomNum = Random.Range(0, 3);
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

        else if (RandomNum == 2)
        {
            for (int a = 0; a < narration_3.Length; a++)
            //for (a = 0; a < textSpeed; a++)
            {
                writerText += narration_3[a];
                ChatText.text = writerText;

                //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
                if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
                {
                    ChatText.text = narration_3;

                    //남은대화 없음
                    remainSentence = true;
                    //대화 끝
                    isSentenceEnd = true;

                    //for문 조건 충족
                    a = narration_3.Length;
                    ////대화 끝
                    //isSentenceEnd = true;
                }

                //대사가 전부 출력되지 않았을 경우
                if (a < narration_3.Length)
                {
                    yield return new WaitForSeconds(0.02f);
                }
            }
            yield return null;
        }
        Debug.Log(writerText);

        //대사 출력이 모두 완료 되었다면
        if (ChatText.text == narration || ChatText.text == narration_2 || ChatText.text == narration_3)
        {
            //대화 종료 조건 충족
            remainSentence = true;
            isSentenceEnd = true;
        }
    }

    IEnumerator ItemClueChat(string narrator, string narration)
    {
        //남은대화 있음
        remainSentence = true;

        Debug.Log(narration);
        CharacterName.text = narrator;
        //characternameText = narrator;
        //narrator = CharacterName.text;

        //심학규의 대사일경우
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
            }

            //대사 출력 중일 경우에만
            if (ChatText.text != narration)
            {
                //텍스트 타이핑 시간 조절
                yield return new WaitForSeconds(0.02f);
            }
        }

        //대사 출력이 모두 완료 되었다면
        if (ChatText.text == narration)
        {
            //대화 종료 조건 충족
            remainSentence = true;
            isSentenceEnd = true;
        }
    }

    //오버로드
    IEnumerator ItemClueChat(string narrator, string narration, bool _remainSentence)
    {
        //심학규의 대사일경우
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
                writerText += narration[a];
                ChatText.text = writerText;

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
                if (ChatText.text == narration && Input.GetKeyDown(KeyCode.Z))
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

        #region 단서

        //2005 : 누군가의 아들
        if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            //잠잠해져야할 물살 단서 획득
            ObjectManager.instance.GetClue(2014);

            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[537].npc_name, npcDatabaseScr.NPC_01[537].comment, true));
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[64].npc_name, npcDatabaseScr.NPC_01[64].comment));
        }

        //2006 : 송나라 상인과 청이
        if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            //청이의 도움 단서 획득
            ObjectManager.instance.GetClue(2009);
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[538].npc_name, npcDatabaseScr.NPC_01[538].comment, true));
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[72].npc_name, npcDatabaseScr.NPC_01[72].comment));
        }

        //2009 : 청이의 도움
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[101].npc_name, npcDatabaseScr.NPC_01[101].comment));
        }

        //2011 : 공양미의 출처
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[120].npc_name, npcDatabaseScr.NPC_01[120].comment));
        }

        //2012 : 청이의 거래
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[128].npc_name, npcDatabaseScr.NPC_01[128].comment));
        }

        //2014 : 잠잠해져야할 물살
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[144].npc_name, npcDatabaseScr.NPC_01[144].comment));
        }

        //2015 : 청이가 사간 것
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[152].npc_name, npcDatabaseScr.NPC_01[152].comment));
        }

        //2017 : 배의 출항
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[170].npc_name, npcDatabaseScr.NPC_01[170].comment));
        }

        //2018 : 노점의 단골
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[189].npc_name, npcDatabaseScr.NPC_01[189].comment));
        }

        //2020 : 사공에게 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[206].npc_name, npcDatabaseScr.NPC_01[206].comment));
        }

        //2021 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[214].npc_name, npcDatabaseScr.NPC_01[214].comment));
        }

        //2022 : 바쁜 상인들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[228].npc_name, npcDatabaseScr.NPC_01[228].comment));
        }

        //2022 : 바쁜 상인들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[228].npc_name, npcDatabaseScr.NPC_01[228].comment));
        }

        //2023 : 3월 보름날
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {

            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[555].npc_name, npcDatabaseScr.NPC_01[555].comment, true));
            //베드엔딩4 취생몽사
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[248].npc_name, npcDatabaseScr.NPC_01[248].comment, true));

            //엔딩 배경 ON
            EndingManager.instance.ShowEndingBG();
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[425].npc_name, npcDatabaseScr.NPC_01[425].comment, true));
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[426].npc_name, npcDatabaseScr.NPC_01[426].comment, true));
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[427].npc_name, npcDatabaseScr.NPC_01[427].comment, true));
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[428].npc_name, npcDatabaseScr.NPC_01[428].comment, true));

            //베드엔딩 배경 보이기
            EndingManager.instance.ChangeToBadEndingBG();
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[429].npc_name, npcDatabaseScr.NPC_01[429].comment, true));
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[430].npc_name, npcDatabaseScr.NPC_01[430].comment, true));

            //타이틀로 이동
            EndingManager.instance.LoadTitleScene();
        }
        #endregion

        #region 아이템
        //1006 : 비녀 
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[306].npc_name, npcDatabaseScr.NPC_01[306].comment));
        }

        //1007 : 먹
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[314].npc_name, npcDatabaseScr.NPC_01[314].comment));
        }

        //1011 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[347].npc_name, npcDatabaseScr.NPC_01[347].comment));
        }

        #endregion

        #region 조합 단서
        //4023 : 공양미를 구한 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[356].npc_name, npcDatabaseScr.NPC_01[356].comment));
        }

        //8032 : 함께 사라진 두 사람
        else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[379].npc_name, npcDatabaseScr.NPC_01[379].comment));
        }

        //4033 : 무역의 중단
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[388].npc_name, npcDatabaseScr.NPC_01[388].comment));
        }

        //4018 : 청이의 가출
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[393].npc_name, npcDatabaseScr.NPC_01[393].comment));
        }
        #endregion

        //기본 대사
        else
        {
            yield return StartCoroutine(NormalChat());
        }
    }
}