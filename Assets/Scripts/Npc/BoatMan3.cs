using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatMan3 : MonoBehaviour
{
    //뱃사공과 닿았는지
    public bool isTouch;

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

    //송나라 상인과 청이 선택지 값
    public int int_Select2006Num = 0;

    //2006번 선택지 고른 날짜
    public int int_select2006Day = 0;

    //선택을 완료했는지
    public bool isSelectDone;

    //최초 클릭
    void Start()
    {
        CharacterName.text = "";
        ChatText.text = "";
    }

    void Update()
    {
        //if(isTouch && Input.GetKeyDown(KeyCode.Z) && !EventManager.instance.gameObject_SelectUI.activeSelf)
        //{
        //    
        //}

        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && UIManager.instance.SentenceCondition()
             && TutorialManager.instance.SentenceCondition() && isTouch && !isSelectDone)
        {
            Debug.Log("z키 누름! 뱃사공!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false && !DialogManager.instance.remainSentence)
            {
                Debug.Log("대화 실행");
                images_NPC.SetActive(true);
                StartCoroutine(TextPractice());
            }

            //대화가 끝났을 경우
            else if (DialogManager.instance.isSentenceEnd)
            {
                //isSelection_5136 = false;

                images_NPC.SetActive(false);
                //대사 비우기
                StopAllCoroutines();
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
        //선택지 시작
       EventManager.instance.SelectStart(NPCName.boatman3, 7194);
        yield return null;
    }

    IEnumerator EndingSentence()
    {
        //배경 어둡게 변경
        EndingManager.instance.ShowEndingBG();
        EndingManager.instance.BrightEndingBG();

        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[681].npc_name, dialogdb.NPC_01[681].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[682].npc_name, dialogdb.NPC_01[682].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[683].npc_name, dialogdb.NPC_01[683].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[684].npc_name, dialogdb.NPC_01[684].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[685].npc_name, dialogdb.NPC_01[685].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[686].npc_name, dialogdb.NPC_01[686].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[687].npc_name, dialogdb.NPC_01[687].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[688].npc_name, dialogdb.NPC_01[688].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[689].npc_name, dialogdb.NPC_01[689].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[690].npc_name, dialogdb.NPC_01[690].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[691].npc_name, dialogdb.NPC_01[691].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[692].npc_name, dialogdb.NPC_01[692].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[693].npc_name, dialogdb.NPC_01[693].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[694].npc_name, dialogdb.NPC_01[694].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[695].npc_name, dialogdb.NPC_01[695].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[696].npc_name, dialogdb.NPC_01[696].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[697].npc_name, dialogdb.NPC_01[697].comment, true));

        //선택지 진행 (1.구하러 뛰어든다, 2.가만히 있는다)
        EventManager.instance.SelectStart(NPCName.Shimbongsa, 7287);
    }

    //엔딩 대사 시작
    public void StartEndingSentence()
    {
        StartCoroutine(EndingSentence());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }

    #region Previous code
    //IEnumerator NormalChat()
    //{
    //    //대화 중복실행 방지
    //    remainSentence = true;

    //    string narrator = characternameText = CharacterName.text = npcDatabaseScr.NPC_01[765].npc_name;
    //    string narration = npcDatabaseScr.NPC_01[765].comment;

    //    Debug.Log(RandomNum);


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
    //    //대화창 보이기
    //    images_NPC.SetActive(true);

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
    //}
    #endregion
}
