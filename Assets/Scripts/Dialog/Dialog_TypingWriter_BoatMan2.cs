using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_BoatMan2 : MonoBehaviour
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

    //뱃사공2 트리거 스크립트
    public BoatMan2_Trigger boatman2_TriggerScr;


    //최초 클릭
    void Start()
    {
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
             && TutorialManager.instance.SentenceCondition() && boatman2_TriggerScr.isTouch)
        {
            Debug.Log("z키 누름! 뱃사공!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false && !remainSentence)
            {
                Debug.Log("대화 실행");
                images_NPC.SetActive(true);
                StartCoroutine(TextPractice2());
                Trigger_NPC.instance.isNPCTrigger = true;
                //초상화 변경
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
                //bool_isNPC = true;
            }

            //대화가 끝났을 경우
            else if (isSentenceEnd)
            {
                //isSelection_5136 = false;

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
        int a = 0;
        string narrator = characternameText = CharacterName.text = npcDatabaseScr.NPC_01[765].npc_name;
        string narration = npcDatabaseScr.NPC_01[765].comment;

        Debug.Log(RandomNum);


        for (a = 0; a < narration.Length; a++)
        //for (a = 0; a < textSpeed; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            //텍스트 타이핑 시간 조절
            //yield return null;

            if (Input.GetKeyDown(KeyCode.Z))
            {
                //남은대화 없음
                remainSentence = true;
                //대화 끝
                isSentenceEnd = true;
            }

            yield return new WaitForSeconds(0.02f);
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            //대화 끝
            isSentenceEnd = true;
        }

        Debug.Log(writerText);
        //writerText = "";

        //키(default : space)를 다시 누를 때까지 무한정 대기
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
        //남은대화 있음
        remainSentence = true;

        Debug.Log(narration);
        int a = 0;
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
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            if (Input.GetKeyDown(KeyCode.Z))
            {
                //남은대화 없음
                remainSentence = true;
                //대화 끝
                isSentenceEnd = true;
            }

            //텍스트 타이핑 시간 조절
            //yield return null;

            yield return new WaitForSeconds(0.02f);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //대화 끝
            isSentenceEnd = true;
        }

        //Z키를 다시 누를 때까지 무한정 대기
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //남은대화 없음
                remainSentence = true;
                //대화 끝
                isSentenceEnd = true;
                break;
            }
            yield return null;
        }

        ////키(default : space)를 다시 누를 때까지 무한정 대기
        //while (true)
        //    if (isButtonClicked)
        //    {
        //        isButtonClicked = false;
        //        break;
        //    }
        //    yield return null;
        //}
    }

    //오버로드
    IEnumerator ItemClueChat(string narrator, string narration, bool _remainSentence)
    {
        //대화창 보이기
        images_NPC.SetActive(true);

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

            Debug.Log(narration);
            int a = 0;
            CharacterName.text = narrator;
            //characternameText = narrator;


            //narrator = CharacterName.text;

            //텍스트 타이핑
            for (a = 0; a < narration.Length; a++)
            {
                writerText += narration[a];
                ChatText.text = writerText;

                //텍스트 타이핑 시간 조절
                //yield return null;
                yield return new WaitForSeconds(0.02f);

                //중간에 Z키를 누르면
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    break;
                }
            }

            //Z키를 다시 누를 때까지 무한정 대기
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    //Text 비우기
                    writerText = "";
                    break;
                }
                yield return null;
            }
        }

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

    }

    IEnumerator ItemClueChat2(string narrator, string narration, bool _remainSentence)
    {
        //남은 대화가 있을경우
        if (_remainSentence == true)
        {
            //남은대화 있음
            remainSentence = true;

            Debug.Log(narration);
            int a = 0;
            CharacterName.text = narrator;
            //characternameText = narrator;


            //narrator = CharacterName.text;

            //텍스트 타이핑
            for (a = 0; a < narration.Length; a++)
            {
                writerText += narration[a];
                ChatText.text = writerText;

                //텍스트 타이핑 시간 조절
                //yield return null;
                yield return new WaitForSeconds(0.02f);

                //중간에 Z키를 누르면
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    break;
                }
            }

            //Z키를 다시 누를 때까지 무한정 대기
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    //Text 비우기
                    writerText = "";
                    break;
                }
                yield return null;
            }
        }
    }

    IEnumerator TextPractice2()
    {
        //기본 대사 진행
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[765].npc_name, npcDatabaseScr.NPC_01[765].comment, true));

        //선택지 시작
        EventManager.instance.SelectStart(NPCName.boatman2, 7355);
    }

    //계란유골 배드엔딩 코루틴
    IEnumerator BoatManEnding()
    {
        //화면 어둡게 하기
        EndingManager.instance.ShowEndingBG();

        //대사 진행
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[766].npc_name, npcDatabaseScr.NPC_01[766].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[767].npc_name, npcDatabaseScr.NPC_01[767].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[768].npc_name, npcDatabaseScr.NPC_01[768].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[769].npc_name, npcDatabaseScr.NPC_01[769].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[770].npc_name, npcDatabaseScr.NPC_01[770].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[771].npc_name, npcDatabaseScr.NPC_01[771].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[772].npc_name, npcDatabaseScr.NPC_01[772].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[773].npc_name, npcDatabaseScr.NPC_01[773].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[774].npc_name, npcDatabaseScr.NPC_01[774].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[775].npc_name, npcDatabaseScr.NPC_01[775].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[776].npc_name, npcDatabaseScr.NPC_01[776].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[777].npc_name, npcDatabaseScr.NPC_01[777].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[778].npc_name, npcDatabaseScr.NPC_01[778].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[779].npc_name, npcDatabaseScr.NPC_01[779].comment, true));

        //타이틀 화면 이동
        EndingManager.instance.LoadTitleScene();
    }

    //계란 유골 배드엔딩 시작
    public void StartBoatManEnding_1()
    {
        StartCoroutine(BoatManEnding());
    }


    //굿 엔딩 진입 코루틴
    IEnumerator GoodEndingRoot()
    {
        //화면 어둡게 하기
        EndingManager.instance.ShowEndingBG();

        //대사 진행
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[240].npc_name, npcDatabaseScr.NPC_01[240].comment, true));

        //심봉사, 위치 변경
        GameManager.instance.TransferPlayer(GameManager.instance.oceanSponPos.position, 6);

        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[241].npc_name, npcDatabaseScr.NPC_01[241].comment, true));

        //배경 천천히 밝게하기
        EndingManager.instance.BrightEndingBG();

        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[242].npc_name, npcDatabaseScr.NPC_01[242].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[243].npc_name, npcDatabaseScr.NPC_01[243].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[244].npc_name, npcDatabaseScr.NPC_01[244].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[245].npc_name, npcDatabaseScr.NPC_01[245].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[246].npc_name, npcDatabaseScr.NPC_01[246].comment, true));
        yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[247].npc_name, npcDatabaseScr.NPC_01[247].comment, true));

        //다이얼로그창 끄기
        DialogManager.instance.Dialouge_System.SetActive(false);
        remainSentence = true;
        isSentenceEnd = true;
        controller_scr.TalkEnd();
    }

    //계란 유골 배드엔딩 시작
    public void StartGoodEndingRoot()
    {
        StartCoroutine(GoodEndingRoot());
    }
}