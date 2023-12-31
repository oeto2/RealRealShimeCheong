using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_BusinessMan : MonoBehaviour
{
    //외부 스크립트 참조
    public BeadMove beadmoveScr;

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

    public S_NPCdatabase_Yes dialogdb;

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
            Debug.Log("z키 누름! 장사꾼!!!!");
            //bool_isBotjim = true;
            //controller_scr.TalkStart();

            if (bool_isNPC == false && !DialogManager.instance.remainSentence)
            {
                Debug.Log("대화 실행");
                images_NPC.SetActive(true);
                StartCoroutine(TextPractice());
                Trigger_NPC.instance.isNPCTrigger = true;
            }

            //대화가 끝났을 경우
            else if (DialogManager.instance.isSentenceEnd)
            {
                images_NPC.SetActive(false);
                // images_NPC_portrait.SetActive(false);
                //대사 비우기
                StopAllCoroutines();
                Trigger_NPC.instance.isNPCTrigger = false;
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


    //구슬 퍼즐 시작
    IEnumerator BeadPuzzlePlay()
    {
        //다이얼로그 창 종료
        images_NPC.SetActive(false);

        //구슬 퍼즐 시작
        GameManager.instance.PlayBeadPuzzle();

        //퍼즐 클리어 전까지 무한 대기
        while (true)
        {
            if (beadmoveScr.isClear)
            {
                break;
            }
            yield return null;
        }
    }

    IEnumerator TextPractice()
    {
        //만약 먹 전달을 완료 했다면
        if (EventManager.instance.eventEndCheck.muckEvent_End == true)
        {
            //이벤트 완료 끄기
            EventManager.instance.eventEndCheck.muckEvent_End = false;
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[153].npc_name, dialogdb.NPC_01[153].comment, true));

            //짚신을 사간 청이 단서 획득
            ObjectManager.instance.GetClue(2016);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[154].npc_name, dialogdb.NPC_01[154].comment));
        }

        else
        {
            #region 단서
            //2001 : 청이의 거짓말
            if (ObjectManager.instance.GetEquipObjectKey() == 2001)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[25].npc_name, dialogdb.NPC_01[25].comment));
            }

            //2002 : 청이의 행방
            else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[33].npc_name, dialogdb.NPC_01[33].comment));
            }

            //2003 : 청이와 장터
            else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
            {
                //청이가 사간것 획득
                ObjectManager.instance.GetClue(2015);

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[535].npc_name, dialogdb.NPC_01[535].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[41].npc_name, dialogdb.NPC_01[41].comment));
            }

            //2005 : 누군가의 아들
            else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[60].npc_name, dialogdb.NPC_01[60].comment));
            }

            //2006 : 송나라 상인과 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[68].npc_name, dialogdb.NPC_01[68].comment));
            }

            //2007 : 승려와 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[81].npc_name, dialogdb.NPC_01[81].comment));
            }

            //2010 : 공양미 삼백석
            else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[107].npc_name, dialogdb.NPC_01[107].comment));
            }

            //2011 : 공양미의 출처
            else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[116].npc_name, dialogdb.NPC_01[116].comment));
            }

            //2012 : 청이의 거래
            else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[124].npc_name, dialogdb.NPC_01[124].comment));
            }

            //2013 : 향리집 셋째아들
            else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
            {
                //노점의 단골 단서 획득
                ObjectManager.instance.GetClue(2018);

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[545].npc_name, dialogdb.NPC_01[545].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[132].npc_name, dialogdb.NPC_01[132].comment));
            }

            //2014 : 잠잠해져야할 물살
            else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[140].npc_name, dialogdb.NPC_01[140].comment));
            }

            //2015 : 청이가 사간 것 (이후 대사 추가?)
            else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[547].npc_name, dialogdb.NPC_01[547].comment, true));

                //아직 먹 전달을 못했으면
                if (EventManager.instance.eventProgress.deliveryMuck != true)
                {
                    //먹 전달 이벤트 시작
                    EventManager.instance.EventActive(Events.muck);

                    //먹 획득
                    ObjectManager.instance.GetItem(1007);

                }

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[148].npc_name, dialogdb.NPC_01[148].comment));
            }

            //2016 : 짚신을 사간 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[158].npc_name, dialogdb.NPC_01[158].comment));
            }

            //2017 : 배의 출항
            else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[168].npc_name, dialogdb.NPC_01[168].comment));
            }

            //2018 : 노점의 단골
            else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[185].npc_name, dialogdb.NPC_01[185].comment));
            }

            //2019 : 뜨지않는 배
            else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[193].npc_name, dialogdb.NPC_01[193].comment));
            }

            //2020 : 사공에게 있었던 일
            else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[202].npc_name, dialogdb.NPC_01[202].comment));
            }

            //2021 : 사공의 물건 (구슬 퍼즐? 대사 추가)
            else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[553].npc_name, dialogdb.NPC_01[553].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[210].npc_name, dialogdb.NPC_01[210].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[215].npc_name, dialogdb.NPC_01[215].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[216].npc_name, dialogdb.NPC_01[216].comment, true));

                //퍼즐 시작
                yield return StartCoroutine(BeadPuzzlePlay());
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[217].npc_name, dialogdb.NPC_01[217].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[218].npc_name, dialogdb.NPC_01[218].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[219].npc_name, dialogdb.NPC_01[219].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[220].npc_name, dialogdb.NPC_01[220].comment));
            }

            //2023 : 3월보름날
            else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[232].npc_name, dialogdb.NPC_01[232].comment));
            }
            #endregion

            #region 아이템
            //1000 : 쌀
            else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[286].npc_name, dialogdb.NPC_01[286].comment));
            }

            //1005 : 주먹밥
            else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[295].npc_name, dialogdb.NPC_01[295].comment));
            }

            //1006 : 비녀 (이벤트?)
            else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[526].npc_name, dialogdb.NPC_01[526].comment, true));

                //베드엔딩1 양상군자
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[302].npc_name, dialogdb.NPC_01[302].comment, true));

                //배경 이미지 변경
                EndingManager.instance.ShowEndingBG();
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[410].npc_name, dialogdb.NPC_01[410].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[411].npc_name, dialogdb.NPC_01[411].comment, true));

                //베드엔딩 컬러로 변경
                EndingManager.instance.ChangeToBadEndingBG();
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[412].npc_name, dialogdb.NPC_01[412].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[413].npc_name, dialogdb.NPC_01[413].comment, true));
                //타이틀 이동
                EndingManager.instance.LoadTitleScene();

            }

            //1007 : 먹
            else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[310].npc_name, dialogdb.NPC_01[310].comment));
            }

            //1009 : 꽃
            else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[325].npc_name, dialogdb.NPC_01[325].comment));
            }

            //1011 : 사공의 물건 (이후대사 추가)
            else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[341].npc_name, dialogdb.NPC_01[341].comment));
            }

            //1013 : 새끼줄1
            else if (ObjectManager.instance.GetEquipObjectKey() == 1013)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[854].npc_name, dialogdb.NPC_01[854].comment));
            }
            //1014 : 새끼줄2
            else if (ObjectManager.instance.GetEquipObjectKey() == 1014)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[864].npc_name, dialogdb.NPC_01[864].comment));
            }
            #endregion

            #region 조합 단서
            //4023 : 공양미를 구한 방법
            else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[352].npc_name, dialogdb.NPC_01[352].comment));
            }

            //8032 : 사라진 두사람
            else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[376].npc_name, dialogdb.NPC_01[376].comment));
            }

            //4033 : 무역의 중단
            else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[560].npc_name, dialogdb.NPC_01[560].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[384].npc_name, dialogdb.NPC_01[384].comment));
            }

            //4018 : 청이의 가출
            else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[394].npc_name, dialogdb.NPC_01[394].comment));
            }
            #endregion

            //기본 대사
            else
            {
                yield return StartCoroutine(DialogManager.instance.NormalChat("장사꾼"));
            }
        }
    }

    #region PreviousCode

    //IEnumerator NormalChat()
    //{
    //    //대화 중복실행 방지
    //    remainSentence = true;

    //    string narrator = characternameText = CharacterName.text = npcDatabaseScr.NPC_01[4].npc_name;
    //    string narration = npcDatabaseScr.NPC_01[4].comment;
    //    string narration_2 = npcDatabaseScr.NPC_01[402].comment;

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
    #endregion
}