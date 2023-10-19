using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_JangSeong : MonoBehaviour
{
    //외부 스크립트 참조
    public TutorialManager tutorialManagerScr;

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

    //대화가 전부 출력 되었는지
    public bool isSentenceEnd = false;

    //남은 대화가 더 있는지
    public bool remainSentence = false;

    // 랜덤 대사 출력 변수
    private int RandomNum;

    // 선택지 UI 출력
    public GameObject Canvas_Selection_UI;

    // 선택지 발생!
    public Text Selection_Text_Name;

    // 선택지 1 대사 텍스트
    public Text Selection_Text1;

    // 선택지 2 대사 텍스트
    public Text Selection_Text2;

    //함께 사라진 두 사람 대화를 했는지 체크 (대화시 True)
    public bool clue8032Talk;

    //// 선택지 확인 변수
    //public bool isSelection_yes = false;
    //public bool isSelection_no = false;

    //public bool isSelection_2023;

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

    //최초 클릭
    void Start()
    {
        CharacterName.text = "";
        ChatText.text = "";
        /*
        Selection_Text_Name.text = "선택지 발생!";
        Selection_Text1.text = "예";
        Selection_Text2.text = "아니오";
        */
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

        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger && UIManager.instance.SentenceCondition())
        {
            Debug.Log("z키 누름! 장승상댁!!!!");
            //bool_isBotjim = true;
            //플레이어 이동제한
            controller_scr.TalkStart();

            if (bool_isNPC == false && !remainSentence)
            {

                //선택지 UI
                Canvas_Selection_UI.SetActive(false);

                //다이얼로그 UI
                images_NPC.SetActive(true);

                //대사 출력
                StartCoroutine(TextPractice());
                Trigger_NPC.instance.isNPCTrigger = true;
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
            }

            //대화 종료
            else if (isSentenceEnd)
            {
                //플레이어 이동제한 해제
                controller_scr.TalkEnd();

                //isSelection_2023 = false;

                images_NPC.SetActive(false);
                bool_isNPC = false;

                writerText = "";
                StopAllCoroutines();


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

        Debug.Log("향리댁 기본대사 진행");

        //초상화 변경
        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];

        string narrator = characternameText = CharacterName.text = dialogdb.NPC_01[3].npc_name;
        string narration = dialogdb.NPC_01[3].comment;
        string narration_2 = dialogdb.NPC_01[401].comment;
        RandomNum = Random.Range(0, 2);

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

    IEnumerator ItemClueChat(string narrator, string narration)
    {
        //남은대화 있음
        remainSentence = true;

        CharacterName.text = narrator;

        //심학규의 대사일 경우
        if (narrator == "심학규")
        {
            //초상화 변경
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[1];
        }
        //향리댁 부인의 대사일 경우
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
        //만약 향리댁 대화 튜토리얼 중이라면
        if (tutorialManagerScr.events == TutorialEvents.TalkToHyang)
        {
            Debug.Log("향리댁 튜토리얼 대사");
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[3].npc_name, dialogdb.NPC_01[3].comment, true));
            yield return StartCoroutine(ItemClueChat("심학규", "청이가 향리 댁에 오지 않았다고 한다.", true));
            yield return StartCoroutine(ItemClueChat("심학규", " 어찌 된 일인지 주변을 수소문 해 보자.", true));
            yield return StartCoroutine(ItemClueChat("심학규", " 게임에서의 하루는 실제 시간의 5분입니다. 하루가 지나면 심학규의 집으로 귀환 됩니다.", true));
            tutorialManagerScr.HyangTalkEnd = true;
        }

        else
        {
            #region 단서
            //2000 : 승상댁의 수양딸
            if (ObjectManager.instance.GetEquipObjectKey() == 2000)
            {
                //단서획득
                ObjectManager.instance.GetClue(2001);
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[532].npc_name, dialogdb.NPC_01[532].comment, true));
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[11].npc_name, dialogdb.NPC_01[11].comment));
            }
            //2001 : 청이의 거짓말
            else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[24].npc_name, dialogdb.NPC_01[24].comment));
            }
            //2002 : 청이의 행방
            else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[32].npc_name, dialogdb.NPC_01[32].comment));
            }
            //2003 : 청이와 장터
            else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[40].npc_name, dialogdb.NPC_01[40].comment));
            }
            //2004 : 청이와 사내
            else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[48].npc_name, dialogdb.NPC_01[48].comment));
            }
            //2005 : 누군가의 아들
            else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
            {
                //향리댁 셋째 아들 단서 획득
                ObjectManager.instance.GetClue(2013);

                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[537].npc_name, dialogdb.NPC_01[537].comment, true));
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[59].npc_name, dialogdb.NPC_01[59].comment));
            }
            //2006 : 송나라 상인과 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[67].npc_name, dialogdb.NPC_01[67].comment));
            }
            //2007 : 승려와 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[80].npc_name, dialogdb.NPC_01[80].comment));
            }
            //2008 : 승려의 마음
            else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[88].npc_name, dialogdb.NPC_01[88].comment));
            }
            //2009 : 청이의 도움
            else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[541].npc_name, dialogdb.NPC_01[541].comment, true));
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[96].npc_name, dialogdb.NPC_01[96].comment, true));
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[102].npc_name, dialogdb.NPC_01[102].comment, true));

                //배의 출항 단서획득
                ObjectManager.instance.GetClue(2017);

                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[103].npc_name, dialogdb.NPC_01[103].comment));
            }
            //2010 : 공양미 삼백 석
            else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[106].npc_name, dialogdb.NPC_01[106].comment));
            }
            //2011 : 공양미의 출처
            else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[115].npc_name, dialogdb.NPC_01[115].comment));
            }
            //2012 : 청이의 거래
            else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[123].npc_name, dialogdb.NPC_01[123].comment));
            }
            //2013 : 향리 댁 셋째 아들
            else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[131].npc_name, dialogdb.NPC_01[131].comment));
            }
            //2014 : 잠잠해져야 할 물살
            else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[139].npc_name, dialogdb.NPC_01[139].comment));
            }
            //2015 : 청이가 사간 것
            else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[147].npc_name, dialogdb.NPC_01[147].comment));
            }
            //2016 : 짚신을 사간 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[157].npc_name, dialogdb.NPC_01[157].comment));
            }
            //2017 : 배의 출항
            else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[167].npc_name, dialogdb.NPC_01[167].comment));
            }
            //2018 : 노점의 단골
            else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[184].npc_name, dialogdb.NPC_01[184].comment));
            }
            //2019 : 뜨지 않는 배
            else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[192].npc_name, dialogdb.NPC_01[192].comment));
            }
            //2020 : 사공에게 있었던 일
            else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[201].npc_name, dialogdb.NPC_01[201].comment));
            }
            //2021 : 사공의 물건
            else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[207].npc_name, dialogdb.NPC_01[207].comment));
            }
            //2022 : 뱃길을 잠재울 방법
            else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[223].npc_name, dialogdb.NPC_01[223].comment));
            }
            //2023 : 3월 보름날
            else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[555].npc_name, dialogdb.NPC_01[555].comment, true));
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[231].npc_name, dialogdb.NPC_01[231].comment, true));

                //엔딩 화면 보이기
                EndingManager.instance.ShowEndingBG();
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[676].npc_name, dialogdb.NPC_01[676].comment, true));
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[677].npc_name, dialogdb.NPC_01[677].comment, true));
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[678].npc_name, dialogdb.NPC_01[678].comment, true));
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[679].npc_name, dialogdb.NPC_01[679].comment, true));
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[680].npc_name, dialogdb.NPC_01[680].comment, true));

                //타이틀 화면 이동
                EndingManager.instance.LoadTitleScene();
            }
            #endregion

            #region 아이템
            //1000 : 쌀
            else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[285].npc_name, dialogdb.NPC_01[285].comment));
            }
            //1005 : 주먹밥
            else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[294].npc_name, dialogdb.NPC_01[294].comment));
            }
            //1007 : 먹
            else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[527].npc_name, dialogdb.NPC_01[527].comment, true));
                //먹 아이템 제거
                ObjectManager.instance.RemoveItem(1007);
                //먹 전달 완료
                EventManager.instance.eventProgress.deliveryMuck = true;
                //먹 전달 이벤트 완료
                EventManager.instance.eventEndCheck.muckEvent_End = true;

                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[309].npc_name, dialogdb.NPC_01[309].comment));
            }
            //1009 : 꽃
            else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[324].npc_name, dialogdb.NPC_01[324].comment));
            }
            //1011 : 사공의 물건
            else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[340].npc_name, dialogdb.NPC_01[340].comment));
            }

            #endregion

            #region 조합 단서
            //4023 : 공양미를 구한 방법
            else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[351].npc_name, dialogdb.NPC_01[351].comment));
            }
            //4015 : 청이가 사라진 날
            else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[359].npc_name, dialogdb.NPC_01[359].comment));
            }
            //4017 : 청이와 그의 관계
            else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[367].npc_name, dialogdb.NPC_01[367].comment));
            }
            //8032 : 함께 사라진 두 사람
            else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
            {

                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[559].npc_name, dialogdb.NPC_01[559].comment, true));
                //첫번째 대화 시
                if (!clue8032Talk)
                {
                    clue8032Talk = true;
                    yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[375].npc_name, dialogdb.NPC_01[375].comment));
                }
                else
                {
                    //베드엔딩2 구화지문

                    //엔딩 배경 보이기
                    EndingManager.instance.ShowEndingBG();
                    yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[414].npc_name, dialogdb.NPC_01[414].comment, true));
                    yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[415].npc_name, dialogdb.NPC_01[415].comment, true));

                    //베드엔딩 이미지로 변경
                    EndingManager.instance.ChangeToBadEndingBG();

                    yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[416].npc_name, dialogdb.NPC_01[416].comment, true));
                    yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[417].npc_name, dialogdb.NPC_01[417].comment, true));
                    yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[418].npc_name, dialogdb.NPC_01[418].comment, true));

                    //타이틀로 이동
                    EndingManager.instance.LoadTitleScene();
                }

            }
            //4033 : 무역의 중단
            else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[383].npc_name, dialogdb.NPC_01[383].comment));
            }
            //4018 : 청이의 가출
            else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[393].npc_name, dialogdb.NPC_01[393].comment));
            }
            #endregion

            #region 기본 대사
            else
            {
                Debug.Log("기본대사 실행");
                yield return StartCoroutine(NormalChat());
            }
            #endregion
        }
    }
}