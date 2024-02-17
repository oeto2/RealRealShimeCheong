using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_BoatMan : MonoBehaviour
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

    public GameObject images_NPC;

    public Sprite[] images_NPC_portrait;

    public Trigger_NPC trigger_npc;

    public bool isNPCTrigger;

    public Controller controller_scr;

    public S_NPCdatabase_Yes npcDatabaseScr;

    [SerializeField]
    public S_NPCdatabase_Yes dialogdb;

    // 랜덤 대사 출력 변수
    private int RandomNum;

    //대화가 전부 출력 되었는지
    public bool isSentenceEnd = false;

    //남은 대화가 더 있는지
    public bool remainSentence = false;

    //송나라 상인과 청이 선택지 값
    public int int_Select2006Num = 0;

    //2006번 선택지 고른 날짜
    public int int_select2006Day = 0;

    //뱃사공2 오브젝트
    public GameObject gameObject_BoatMan2;

    //뱃사공2 이벤트 진행여부
    public bool boatMan2_Show;

    //사공의 물건을 전달 했는지
    public bool boatManObject;

    //뱃사공과 닿았는지
    public bool isTouch;

    //최초 클릭
    void Start()
    {
        //StartCoroutine(TextPractice());
        //TextPractice();
        //StopCoroutine(TextPractice());
        CharacterName.text = "";
        ChatText.text = "";

        //Selection_Text_Name.text = "선택지 발생!";
        //Selection_Text1.text = "내가 청이 아비 되는 사람이오. 솔직하게 말해주시오.";
        //Selection_Text2.text = "나도 그 이야기라면 들었소. 송 사람들이 너무하던데 말이오!";
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

        if (Input.GetKeyDown(KeyCode.Z) && isTouch && UIManager.instance.SentenceCondition()
             && TutorialManager.instance.SentenceCondition())
        {
            Debug.Log("z키 누름! 뱃사공!!!!");
            //bool_isBotjim = true;
            //controller_scr.TalkStart();

            if (bool_isNPC == false && !DialogManager.instance.remainSentence)
            {
                Debug.Log("대화 실행");
                images_NPC.SetActive(true);
                StartCoroutine(TextPractice());
                //Trigger_NPC.instance.isNPCTrigger = true;
            }

            //대화가 끝났을 경우
            else if (DialogManager.instance.isSentenceEnd)
            {
                //isSelection_5136 = false;

                images_NPC.SetActive(false);
                // images_NPC_portrait.SetActive(false);
                //대사 비우기
                StopAllCoroutines();
                //Trigger_NPC.instance.isNPCTrigger = false;
                bool_isNPC = false;
                //Controller.instance.TalkEnd();
                controller_scr.TalkEnd();

                //남은대화 없음
                DialogManager.instance.remainSentence = false;
                //대화 끝
                DialogManager.instance.isSentenceEnd = false;
                //텍스트 비우기
                DialogManager.instance.writerText = "";
            }
        }
    }

    IEnumerator TextPractice()
    {
        #region 단서
        //2001 : 청이의 거짓말
        if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[28].npc_name, dialogdb.NPC_01[28].comment));
        }

        //2003 : 청이와 장터
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[44].npc_name, dialogdb.NPC_01[44].comment));
        }

        //2004 : 청이와 남자
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[52].npc_name, dialogdb.NPC_01[52].comment));
        }

        //2005 : 누군가의 아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[63].npc_name, dialogdb.NPC_01[63].comment));
        }

        //2006 : 송나라 상인과 청이 (추가 대사 있음)
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[538].npc_name, dialogdb.NPC_01[538].comment, true));

            //선택지를 고르지 않았다면
            if (!EventManager.instance.selectEndCheck.select2006_End)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[71].npc_name, dialogdb.NPC_01[71].comment, true));
                EventManager.instance.SelectStart(NPCName.boatman, 2006);
            }
            else
            {
                //선택지 1번을 골랐을 경우
                if (int_Select2006Num == 1)
                {
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[73].npc_name, dialogdb.NPC_01[73].comment, true));
                    //청이의 거래 단서 획득
                    ObjectManager.instance.GetClue(2012);
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[74].npc_name, dialogdb.NPC_01[74].comment));
                }
                else
                {
                    //만약 하루가 지났다면
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

        //2007 : 승려와 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[84].npc_name, dialogdb.NPC_01[84].comment));
        }

        //2008 : 승려의 마음
        else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[92].npc_name, dialogdb.NPC_01[92].comment));
        }

        //2009 : 청이의 도움
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[100].npc_name, dialogdb.NPC_01[100].comment));
        }

        //2011 : 공양미의 출처
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[119].npc_name, dialogdb.NPC_01[119].comment));
        }

        //2012 : 청이의 거래
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[127].npc_name, dialogdb.NPC_01[127].comment));
        }

        //2013 : 향리집 셋째아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[135].npc_name, dialogdb.NPC_01[135].comment));
        }

        //2014 : 잠잠해져야할 물살
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            //뜨지않는 배 단서 획득
            ObjectManager.instance.GetClue(2019);

            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[546].npc_name, dialogdb.NPC_01[546].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[143].npc_name, dialogdb.NPC_01[143].comment));
        }

        //2015 : 청이가 사간 것
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[151].npc_name, dialogdb.NPC_01[151].comment));
        }

        //2017 : 배의 출항
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[169].npc_name, dialogdb.NPC_01[169].comment));
        }

        //2018 : 노점의 단골
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[188].npc_name, dialogdb.NPC_01[188].comment));
        }

        //2019 : 뜨지않는 배
        else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[196].npc_name, dialogdb.NPC_01[196].comment));
        }

        //2020 : 사공에게 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            //사공의 물건 단서 획득
            ObjectManager.instance.GetClue(2021);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[552].npc_name, dialogdb.NPC_01[552].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[205].npc_name, dialogdb.NPC_01[205].comment));
        }

        //2021 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[213].npc_name, dialogdb.NPC_01[213].comment));
        }

        //2022 : 뱃길을 잠재울 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[227].npc_name, dialogdb.NPC_01[227].comment));
        }

        //2023 : 3월 보름날
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[555].npc_name, dialogdb.NPC_01[555].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[236].npc_name, dialogdb.NPC_01[236].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[237].npc_name, dialogdb.NPC_01[237].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[238].npc_name, dialogdb.NPC_01[238].comment, true));

            //개울가 뱃사공 보이기
            gameObject_BoatMan2.SetActive(true);
            boatMan2_Show = true;

            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[239].npc_name, dialogdb.NPC_01[239].comment));
        }
        #endregion

        #region 아이템
        //1000 : 쌀
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[289].npc_name, dialogdb.NPC_01[289].comment));
        }

        //1005 : 주먹밥
        else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[297].npc_name, dialogdb.NPC_01[297].comment));
        }

        //1006 : 비녀 
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[305].npc_name, dialogdb.NPC_01[305].comment));
        }

        //1007 : 먹
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[313].npc_name, dialogdb.NPC_01[313].comment));
        }

        //1009 : 꽃
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[328].npc_name, dialogdb.NPC_01[328].comment));
        }

        //1011 : 사공의 물건 (이후 대사 추가)
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[531].npc_name, dialogdb.NPC_01[531].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[344].npc_name, dialogdb.NPC_01[344].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[345].npc_name, dialogdb.NPC_01[345].comment, true));
            boatManObject = true;
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[346].npc_name, dialogdb.NPC_01[346].comment));
        }

        //1013 : 새끼줄1
        else if (ObjectManager.instance.GetEquipObjectKey() == 1013)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[859].npc_name, dialogdb.NPC_01[859].comment));
        }
        //1014 : 새끼줄2
        else if (ObjectManager.instance.GetEquipObjectKey() == 1014)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[867].npc_name, dialogdb.NPC_01[867].comment));
        }
        #endregion

        #region 조합 단서
        //4023 : 공양미를 구한 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[355].npc_name, dialogdb.NPC_01[355].comment));
        }

        //4015 : 이틀전에 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[363].npc_name, dialogdb.NPC_01[363].comment));
        }

        //4017 : 청이와 그의 관계
        else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[371].npc_name, dialogdb.NPC_01[371].comment));
        }

        //4033 : 무역의 중단
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[560].npc_name, dialogdb.NPC_01[560].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[387].npc_name, dialogdb.NPC_01[387].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[389].npc_name, dialogdb.NPC_01[389].comment, true));

            //뱃길을 잠재울 방법 단서획득
            ObjectManager.instance.GetClue(2022);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[390].npc_name, dialogdb.NPC_01[390].comment));
        }

        //4018 : 청이의 가출
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[397].npc_name, dialogdb.NPC_01[397].comment));
        }
        #endregion

        //기본 대사
        else
        {
            yield return StartCoroutine(DialogManager.instance.NormalChat("뱃사공"));
        }
    }

    //송나라 상인과 청이 1번 대사
    IEnumerator Select2006_Sentence1()
    {
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[73].npc_name, dialogdb.NPC_01[73].comment, true));
        //청이의 거래 단서 획득
        ObjectManager.instance.GetClue(2012);
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[74].npc_name, dialogdb.NPC_01[74].comment));
    }

    //송나라 상인과 청이 2번 대사
    IEnumerator Select2006_Sentence2()
    {
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[75].npc_name, dialogdb.NPC_01[75].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[76].npc_name, dialogdb.NPC_01[76].comment));

    }

    //송나라 상인과 청이 선택지 1번 대사 출력
    public void PrintSelect2006_Sentence1()
    {
        StartCoroutine(Select2006_Sentence1());
    }

    //송나라 상인과 청이 선택지 2번 대사 출력
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
    //    //대화 중복실행 방지
    //    remainSentence = true;

    //    string narrator = characternameText = CharacterName.text = npcDatabaseScr.NPC_01[7].npc_name;
    //    string narration = npcDatabaseScr.NPC_01[7].comment;
    //    string narration_2 = npcDatabaseScr.NPC_01[404].comment;

    //    RandomNum = Random.Range(0, 2);
    //    Debug.Log(RandomNum);

    //    //텍스트 타이핑
    //    if (RandomNum == 0)
    //    {
    //        for (int a = 0; a < narration.Length; a++)
    //        {
    //            writerText += narration[a];
    //            ChatText.text = writerText;

    //            //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
    //            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
    //            {
    //                ChatText.text = narration;

    //                //남은대화 없음
    //                remainSentence = true;
    //                //대화 끝
    //                isSentenceEnd = true;

    //                //for문 조건 충족
    //                a = narration.Length;
    //                ////대화 끝
    //                //isSentenceEnd = true;
    //            }

    //            //대사가 전부 출력되지 않았을 경우
    //            if (a < narration.Length)
    //            {
    //                //대사 타이핑 속도
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

    //            //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
    //            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
    //            {
    //                ChatText.text = narration_2;

    //                //남은대화 없음
    //                remainSentence = true;
    //                //대화 끝
    //                isSentenceEnd = true;

    //                //for문 조건 충족
    //                a = narration_2.Length;
    //                ////대화 끝
    //                //isSentenceEnd = true;
    //            }

    //            //대사가 전부 출력되지 않았을 경우
    //            if (a < narration_2.Length)
    //            {
    //                yield return new WaitForSeconds(0.02f);
    //            }
    //        }
    //        yield return null;
    //    }
    //    Debug.Log(writerText);

    //    //대사 출력이 모두 완료 되었다면
    //    if (ChatText.text == narration || ChatText.text == narration_2)
    //    {
    //        //대화 종료 조건 충족
    //        remainSentence = true;
    //        isSentenceEnd = true;
    //    }
    //}

    //IEnumerator ItemClueChat(string narrator, string narration)
    //{
    //    //남은대화 있음
    //    remainSentence = true;

    //    Debug.Log(narration);
    //    CharacterName.text = narrator;
    //    //characternameText = narrator;
    //    //narrator = CharacterName.text;

    //    //심학규의 대사일경우
    //    if (narrator == "심학규")
    //    {
    //        //초상화 변경
    //        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[1];
    //    }
    //    else
    //    {
    //        //초상화 변경
    //        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
    //    }

    //    //텍스트 타이핑
    //    for (int a = 0; a < narration.Length; a++)
    //    {
    //        writerText += narration[a];
    //        ChatText.text = writerText;

    //        //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
    //        if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
    //        {
    //            ChatText.text = narration;

    //            //남은대화 없음
    //            remainSentence = true;
    //            //대화 끝
    //            isSentenceEnd = true;

    //            //for문 조건 충족
    //            a = narration.Length;
    //        }

    //        //대사 출력 중일 경우에만
    //        if (ChatText.text != narration)
    //        {
    //            //텍스트 타이핑 시간 조절
    //            yield return new WaitForSeconds(0.02f);
    //        }
    //    }

    //    //대사 출력이 모두 완료 되었다면
    //    if (ChatText.text == narration)
    //    {
    //        //대화 종료 조건 충족
    //        remainSentence = true;
    //        isSentenceEnd = true;
    //    }
    //}

    ////오버로드
    //IEnumerator ItemClueChat(string narrator, string narration, bool _remainSentence)
    //{
    //    //심학규의 대사일경우
    //    if (narrator == "심학규")
    //    {
    //        //초상화 변경
    //        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[1];
    //    }
    //    else
    //    {
    //        //초상화 변경
    //        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
    //    }

    //    //남은 대화가 있을경우
    //    if (_remainSentence == true)
    //    {
    //        //남은대화 있음
    //        remainSentence = true;

    //        CharacterName.text = narrator;

    //        //텍스트 타이핑
    //        for (int a = 0; a < narration.Length; a++)
    //        {
    //            writerText += narration[a];
    //            ChatText.text = writerText;

    //            //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
    //            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
    //            {
    //                writerText = narration;
    //                ChatText.text = narration;

    //                //남은대화 없음
    //                remainSentence = true;
    //                ////대화 끝
    //                //isSentenceEnd = true;

    //                //for문 조건 충족
    //                a = narration.Length;
    //            }

    //            //대사 출력 중일 경우에만
    //            if (ChatText.text != narration)
    //            {
    //                //텍스트 타이핑 시간 조절
    //                yield return new WaitForSeconds(0.02f);
    //            }

    //        }

    //        //대사 출력 후 잠깐 딜레이
    //        yield return new WaitForSeconds(0.1f);

    //        //Z키를 다시 누를 때까지 무한정 대기
    //        while (true)
    //        {
    //            if (ChatText.text == narration && Input.GetKeyDown(KeyCode.Z))
    //            {
    //                Debug.Log("Text 비우기");

    //                //Text 비우기
    //                writerText = "";

    //                break;
    //            }
    //            yield return null;
    //        }
    //    }
    //}

    //IEnumerator ItemClueChat2(string narrator, string narration, bool _remainSentence)
    //{
    //    //남은 대화가 있을경우
    //    if (_remainSentence == true)
    //    {
    //        //남은대화 있음
    //        remainSentence = true;

    //        Debug.Log(narration);
    //        int a = 0;
    //        CharacterName.text = narrator;
    //        //characternameText = narrator;


    //        //narrator = CharacterName.text;

    //        //텍스트 타이핑
    //        for (a = 0; a < narration.Length; a++)
    //        {
    //            writerText += narration[a];
    //            ChatText.text = writerText;

    //            //텍스트 타이핑 시간 조절
    //            //yield return null;
    //            yield return new WaitForSeconds(0.02f);

    //            //중간에 Z키를 누르면
    //            if (Input.GetKeyDown(KeyCode.Z))
    //            {
    //                break;
    //            }
    //        }

    //        //Z키를 다시 누를 때까지 무한정 대기
    //        while (true)
    //        {
    //            if (Input.GetKeyDown(KeyCode.Z))
    //            {
    //                //Text 비우기
    //                writerText = "";
    //                break;
    //            }
    //            yield return null;
    //        }
    //    }

    ////키(default : space)를 다시 누를 때까지 무한정 대기
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
    //    //2006 : 송나라 상인과 청이 (추가 대사 있음)
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