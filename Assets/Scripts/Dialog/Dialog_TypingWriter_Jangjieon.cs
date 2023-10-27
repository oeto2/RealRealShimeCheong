using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Jangjieon : MonoBehaviour
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

    // 랜덤 대사 출력 변수
    private int RandomNum;

    //최초에만 출력되도록 하는 확인용
    public bool isNPC_Start = true;

    //대화가 전부 출력 되었는지
    public bool isSentenceEnd = false;

    //남은 대화가 더 있는지
    public bool remainSentence = false;

    //바다의 바쳐질 제물 대사를 진행했는지
    public bool talkClue_6045;

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
            Debug.Log("z키 누름! 장지언!!!!");

            controller_scr.TalkStart();
            if (bool_isNPC == false && !DialogManager.instance.remainSentence)
            {
                StartCoroutine(TextPractice());
                images_NPC.SetActive(true);
                bool_isNPC = true;
                Trigger_NPC.instance.isNPCTrigger = true;
            }
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

    IEnumerator TextPractice()
    {
        #region 단서
        //2001 : 향리댁 수양 딸 
        if (ObjectManager.instance.GetEquipObjectKey() == 2000)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[563].npc_name, dialogdb.NPC_01[563].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[564].npc_name, dialogdb.NPC_01[564].comment));
        }
        //2001 : 청이의 거짓말
        else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[565].npc_name, dialogdb.NPC_01[565].comment));
        }
        //2002 : 청이의 행방
        else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[566].npc_name, dialogdb.NPC_01[566].comment));
        }
        //2004 : 청이와 사내
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[567].npc_name, dialogdb.NPC_01[567].comment));
        }
        //2005 : 누군가의 아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[568].npc_name, dialogdb.NPC_01[568].comment));
        }
        //2006 : 송나라 상인과 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[569].npc_name, dialogdb.NPC_01[569].comment));
        }
        //2007 : 승려와 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[570].npc_name, dialogdb.NPC_01[570].comment));
        }
        //2008 : 승려의 마음
        else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[571].npc_name, dialogdb.NPC_01[571].comment));
        }
        //2009 : 청이의 도움
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[572].npc_name, dialogdb.NPC_01[572].comment));
        }
        //2010 : 공양미 삼백 석
        else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[573].npc_name, dialogdb.NPC_01[573].comment));
        }
        //2011 : 공양미의 출처
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[574].npc_name, dialogdb.NPC_01[574].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[575].npc_name, dialogdb.NPC_01[575].comment));
        }
        //2012 : 청이의 거래
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[576].npc_name, dialogdb.NPC_01[576].comment));
        }
        //2013 : 향리댁 셋째 아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[577].npc_name, dialogdb.NPC_01[577].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[578].npc_name, dialogdb.NPC_01[578].comment));
        }
        //2014 : 잠잠해져야 할 물살
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            // 기본 대사와 같음, 생략 가능할지도
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[579].npc_name, dialogdb.NPC_01[579].comment));
        }
        //2015 : 청이가 사간 것
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[580].npc_name, dialogdb.NPC_01[580].comment));
        }
        //2016 : 짚신을 사간 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[581].npc_name, dialogdb.NPC_01[581].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[582].npc_name, dialogdb.NPC_01[582].comment));
        }
        //2017 : 배의 출항
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[583].npc_name, dialogdb.NPC_01[583].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[584].npc_name, dialogdb.NPC_01[584].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[585].npc_name, dialogdb.NPC_01[585].comment));
        }
        //2018 : 노점의 단골
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[586].npc_name, dialogdb.NPC_01[586].comment));
        }
        //2019 : 뜨지 않는 배
        else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[587].npc_name, dialogdb.NPC_01[587].comment));
        }
        //2020 : 사공에게 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[588].npc_name, dialogdb.NPC_01[588].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[589].npc_name, dialogdb.NPC_01[589].comment));
        }
        //2021 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[590].npc_name, dialogdb.NPC_01[590].comment));
        }
        //2022 : 뱃길을 잠재울 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[591].npc_name, dialogdb.NPC_01[591].comment));
        }
        //2023 : 3월 보름날
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[592].npc_name, dialogdb.NPC_01[592].comment));
        }
        #endregion

        #region 아이템
        
        #endregion

        #region 조합 단서
        //4023 : 공양미를 구한 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[593].npc_name, dialogdb.NPC_01[593].comment));
        }
        //4015 : 청이가 사라진 날
        else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[594].npc_name, dialogdb.NPC_01[594].comment));
        }
        //4017 : 청이와 그의 관계
        else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[595].npc_name, dialogdb.NPC_01[595].comment));
        }
        //8032 : 함께 사라진 두 사람
        else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[596].npc_name, dialogdb.NPC_01[596].comment));
        }
        //4033 : 무역의 중단
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[597].npc_name, dialogdb.NPC_01[597].comment));
        }
        //4018 : 청이의 가출
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[598].npc_name, dialogdb.NPC_01[598].comment));
        }
        //6045 : 바다의 바쳐질 제물
        else if (ObjectManager.instance.GetEquipObjectKey() == 6045)
        {
            //가만히 있는다 대사 획득
            talkClue_6045 = true;
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[599].npc_name, dialogdb.NPC_01[599].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[600].npc_name, dialogdb.NPC_01[600].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[601].npc_name, dialogdb.NPC_01[601].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[602].npc_name, dialogdb.NPC_01[602].comment));
        }
        #endregion

        #region 기본 대사
        else
        {
            yield return StartCoroutine(DialogManager.instance.NormalChat("장지언"));
        }
        #endregion
    }

    //굿엔딩 : 분골쇄신
    IEnumerator GoodEndingStart()
    {
        //화면 어둡게하기
        EndingManager.instance.ResetEndingBG();
        EndingManager.instance.ShowEndingBG();
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[698].npc_name, dialogdb.NPC_01[698].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[699].npc_name, dialogdb.NPC_01[699].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[700].npc_name, dialogdb.NPC_01[700].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[701].npc_name, dialogdb.NPC_01[701].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[702].npc_name, dialogdb.NPC_01[702].comment, true));

        //타이틀 이동
        EndingManager.instance.LoadTitleScene();
    }

    //굿엔딩 시작
    public void StartGoodEnidng()
    {
        StartCoroutine(GoodEndingStart());
    }

    //진엔딩 루트
    IEnumerator RealEndingRoot()
    {
        //화면 어둡게하기
        EndingManager.instance.ResetEndingBG();
        EndingManager.instance.ShowEndingBG();

        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[703].npc_name, dialogdb.NPC_01[703].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[704].npc_name, dialogdb.NPC_01[704].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[705].npc_name, dialogdb.NPC_01[705].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[706].npc_name, dialogdb.NPC_01[706].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[707].npc_name, dialogdb.NPC_01[707].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[708].npc_name, dialogdb.NPC_01[708].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[709].npc_name, dialogdb.NPC_01[709].comment, true));

        //선택지 시작
        EventManager.instance.SelectStart(NPCName.Shimbongsa2, 7299);
    }

    //진엔딩 루트 시작
    public void StartRealEndingRoot()
    {
        StartCoroutine(RealEndingRoot());
    }


    //굿엔딩1
    IEnumerator GoodEndingSentence_1()
    {
        //화면 어둡게하기
        EndingManager.instance.ResetEndingBG();
        EndingManager.instance.ShowEndingBG();

        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[710].npc_name, dialogdb.NPC_01[709].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[711].npc_name, dialogdb.NPC_01[711].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[712].npc_name, dialogdb.NPC_01[712].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[713].npc_name, dialogdb.NPC_01[713].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[714].npc_name, dialogdb.NPC_01[714].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[715].npc_name, dialogdb.NPC_01[715].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[716].npc_name, dialogdb.NPC_01[716].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[717].npc_name, dialogdb.NPC_01[717].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[718].npc_name, dialogdb.NPC_01[718].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[719].npc_name, dialogdb.NPC_01[719].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[720].npc_name, dialogdb.NPC_01[720].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[721].npc_name, dialogdb.NPC_01[721].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[722].npc_name, dialogdb.NPC_01[722].comment, true));

        //타이틀로 이동
        EndingManager.instance.LoadTitleScene();
    }

    //굿엔딩 1 시작
    public void StartGoodEnding_1()
    {
        StartCoroutine(GoodEndingSentence_1());
    }

    //굿엔딩2
    IEnumerator GoodEndingSentence_2()
    {
        //화면 어둡게하기
        EndingManager.instance.ResetEndingBG();
        EndingManager.instance.ShowEndingBG();
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[723].npc_name, dialogdb.NPC_01[723].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[724].npc_name, dialogdb.NPC_01[724].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[725].npc_name, dialogdb.NPC_01[725].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[726].npc_name, dialogdb.NPC_01[726].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[727].npc_name, dialogdb.NPC_01[727].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[728].npc_name, dialogdb.NPC_01[728].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[729].npc_name, dialogdb.NPC_01[729].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[730].npc_name, dialogdb.NPC_01[730].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[731].npc_name, dialogdb.NPC_01[731].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[732].npc_name, dialogdb.NPC_01[732].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[733].npc_name, dialogdb.NPC_01[733].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[734].npc_name, dialogdb.NPC_01[734].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[735].npc_name, dialogdb.NPC_01[735].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[736].npc_name, dialogdb.NPC_01[736].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[737].npc_name, dialogdb.NPC_01[737].comment, true));

        //타이틀로 이동
        EndingManager.instance.LoadTitleScene();
    }

    //굿엔딩 2 시작
    public void StartGoodEnding_2()
    {
        StartCoroutine(GoodEndingSentence_2());
    }

    //진엔딩
    IEnumerator RealEndingSentence()
    {
        //화면 어둡게하기
        EndingManager.instance.ResetEndingBG();
        EndingManager.instance.ShowEndingBG();

        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[738].npc_name, dialogdb.NPC_01[738].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[739].npc_name, dialogdb.NPC_01[739].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[740].npc_name, dialogdb.NPC_01[740].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[741].npc_name, dialogdb.NPC_01[741].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[742].npc_name, dialogdb.NPC_01[742].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[743].npc_name, dialogdb.NPC_01[743].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[744].npc_name, dialogdb.NPC_01[744].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[745].npc_name, dialogdb.NPC_01[745].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[746].npc_name, dialogdb.NPC_01[746].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[747].npc_name, dialogdb.NPC_01[747].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[748].npc_name, dialogdb.NPC_01[748].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[749].npc_name, dialogdb.NPC_01[749].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[750].npc_name, dialogdb.NPC_01[750].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[751].npc_name, dialogdb.NPC_01[751].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[752].npc_name, dialogdb.NPC_01[752].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[753].npc_name, dialogdb.NPC_01[753].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[754].npc_name, dialogdb.NPC_01[754].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[755].npc_name, dialogdb.NPC_01[755].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[756].npc_name, dialogdb.NPC_01[756].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[757].npc_name, dialogdb.NPC_01[757].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[758].npc_name, dialogdb.NPC_01[758].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[759].npc_name, dialogdb.NPC_01[759].comment, true));

        //엔딩배경 색 변경
        EndingManager.instance.ShowRealEndingBG();

        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[760].npc_name, dialogdb.NPC_01[760].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[761].npc_name, dialogdb.NPC_01[761].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[762].npc_name, dialogdb.NPC_01[762].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[763].npc_name, dialogdb.NPC_01[763].comment, true));

        //타이틀로 이동
        EndingManager.instance.LoadTitleScene();
    }

    //진엔딩 시작
    public void StartRealEnding()
    {
        StartCoroutine(RealEndingSentence());
    }

    #region PreviousCode
    //public void OnTriggerEnter2D(Collider2D other)
    //{
    //    isNPCTrigger = true;
    //    if (other.CompareTag("Player"))
    //    {
    //        OnClickdown();
    //    }
    //}
    //public void OnClickdown()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
    //    {
    //        Debug.Log("이건 Touch! 장지언!!!!");
    //        //StartCoroutine(TextPractice());
    //        //bool_isBotjim = true;
    //        if (bool_isNPC == true)
    //        {
    //            Controller.instance.TalkStart();
    //            images_NPC.SetActive(true);
    //            bool_isNPC = false;

    //            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
    //        }
    //        else
    //        {
    //            images_NPC.SetActive(false);
    //            // images_NPC_portrait.SetActive(false);
    //            bool_isNPC = true;
    //            //StopCoroutine(TextPractice());
    //            Controller.instance.TalkEnd();
    //        }
    //    }
    //}

    //IEnumerator NormalChat_4999(string narrator, string narration)
    //{
    //    int a = 0;
    //    /*
    //    // 글자색 설정 변수
    //    bool t_white = false;
    //    bool t_red = false;

    //    // 글자색 설정 문자는 대사 출력 무시
    //    bool t_ignore = false;
    //    */
    //    //CharacterName.text = narrator;
    //    narrator = characternameText = dialogdb.NPC_01[563].npc_name;
    //    //CharacterName.text = narrator;
    //    writerText = dialogdb.NPC_01[0].comment;
    //    Debug.Log(characternameText);
    //    //writerText = "";

    //    //narrator = CharacterName.text;
    //    //yield return null;
    //    //텍스트 타이핑
    //    for (a = 0; a < narration.Length; a++)
    //    //for (a = 0; a < 62; a++)
    //    {
    //        /*string t_letter = narration[a].ToString();
    //        //string t_letter;
    //        switch (narration[a])
    //        {
    //            case 'ⓡ':
    //                t_white = false;
    //                t_red = true;
    //                t_ignore = true;
    //                break;
    //            //case 'ⓦ':
    //                //t_white = true;
    //                //t_red = false;
    //                //t_ignore = true;
    //                break;
    //        }
    //        if (t_ignore==true)
    //        {
    //            if (t_white)
    //            {
    //                t_letter = "<color=#ffffff>" + narration[a] + "</color>";    // HTML Tag
    //	Debug.Log(t_letter);
    //                Debug.Log('1');

    //}

    //else if (t_red)
    //            {
    //                t_letter = "<color=#B40404>" + narration[a] + "</color>";
    //                Debug.Log(t_letter);
    //                Debug.Log('2');
    //            }
    //            Debug.Log(writerText);
    //            //ChatText.text = writerText;
    //            //writerText += t_letter; // 특수문자가 아니라면 대사 출력
    //            //writerText += narration[a];
    //            //ChatText.text = writerText;
    //            //t_ignore = false; // 한 글자 찍었으면 다시 false
    //        }*/

    //        writerText += narration[a];
    //        ChatText.text = writerText;
    //        //t_ignore = false; // 한 글자 찍었으면 다시 false
    //        //ChatText.text = t_letter;
    //        //writerText += t_letter; // 특수문자가 아니라면 대사 출력
    //        //ChatText.text = writerText;
    //        //텍스트 타이핑 시간 조절
    //        yield return new WaitForSeconds(0.07f);
    //    }
    //    yield return null;
    //    //키(default : space)를 다시 누를 때까지 무한정 대기
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

    //IEnumerator NormalChat()
    //{
    //    //대화 중복실행 방지
    //    remainSentence = true;

    //    string narrator = characternameText = CharacterName.text = dialogdb.NPC_01[563].npc_name;
    //    string narration = dialogdb.NPC_01[603].comment;
    //    isNPC_Start = false;
    //    //narrator = CharacterName.text;


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
    //            ////대화 끝
    //            //isSentenceEnd = true;
    //        }

    //        //대사가 전부 출력되지 않았을 경우
    //        if (a < narration.Length)
    //        {
    //            //대사 타이핑 속도
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
    //IEnumerator ItemClueChat(string narrator, string narration)
    //{
    //    //다이얼로그창 띄우기
    //    images_NPC.SetActive(true);

    //    //첫대사 플래그 false
    //    isNPC_Start = false;

    //    //남은대화 있음
    //    remainSentence = true;

    //    Debug.Log(narration);
    //    CharacterName.text = narrator;

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