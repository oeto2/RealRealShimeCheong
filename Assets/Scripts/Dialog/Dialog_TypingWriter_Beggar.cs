using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Beggar : MonoBehaviour
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

    //1005번 주먹밥 처리용
    public bool isJoomuckBab = false;

    // 랜덤 대사 출력 변수
    private int RandomNum;

    //대화가 전부 출력 되었는지
    public bool isSentenceEnd = false;

    //남은 대화가 더 있는지
    public bool remainSentence = false;
    public S_NPCdatabase_Yes dialogdb;


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
            Debug.Log("z키 누름! 상거지다!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();

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


    IEnumerator NormalChat()
    {
        //대화 중복실행 방지
        remainSentence = true;

        //초상화 변경
        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];

        string narrator = characternameText = CharacterName.text = npcDatabaseScr.NPC_01[6].npc_name;
        string narration = npcDatabaseScr.NPC_01[6].comment;
        string narration_2 = npcDatabaseScr.NPC_01[403].comment;

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
        images_NPC.SetActive(true);

        //심학규의 대사일경우
        if (narrator == "심학규")
        {
            //초상화 변경
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[1];
        }
        else
        {
            Debug.Log("거지 초상화 변경");
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

    IEnumerator ClearChat(string narrator, string narration, bool _remainSentence)
    {
        //images_NPC.SetActive(false);
        CharacterName.text = narrator;




        images_NPC.SetActive(false);
        // images_NPC_portrait.SetActive(false);
        //대사 비우기
        writerText = "";
        //StopAllCoroutines();
        Trigger_NPC.instance.isNPCTrigger = false;
        bool_isNPC = false;

        yield return null;
    }

        IEnumerator TextPractice()
    {
        #region 단서

        //2000 : 승상댁의 수양딸
        if (ObjectManager.instance.GetEquipObjectKey() == 2000)
        {
            //주먹밥 이벤트 활성화
            EventManager.instance.EventActive(Events.JoomuckBab);

            //대화 진행
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[532].npc_name, dialogdb.NPC_01[532].comment, true));

            //청이의 행방 단서 획득
            ObjectManager.instance.GetClue(2002);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[14].npc_name, dialogdb.NPC_01[14].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[17].npc_name, dialogdb.NPC_01[17].comment));
        }

        //2001 : 청이의 거짓말
        else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[27].npc_name, dialogdb.NPC_01[27].comment));
        }

        //2002 : 청이의 행방
        else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[35].npc_name, dialogdb.NPC_01[35].comment));
        }

        //2003 : 청이와 장터
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[43].npc_name, dialogdb.NPC_01[43].comment));
        }

        //2004 : 청이와 남자
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            //누군가의 아들 단서 획득
            ObjectManager.instance.GetClue(2005);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[536].npc_name, dialogdb.NPC_01[536].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[51].npc_name, dialogdb.NPC_01[51].comment));
        }

        //2005 : 누군가의 아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[62].npc_name, dialogdb.NPC_01[62].comment));
        }

        //2006 : 송나라 상인과 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[70].npc_name, dialogdb.NPC_01[70].comment));
        }

        //2008 : 승려의 마음
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[70].npc_name, dialogdb.NPC_01[70].comment));
        }

        //2009 : 청이의 도움
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[99].npc_name, dialogdb.NPC_01[99].comment));
        }

        //2010 : 공양미 삼백석
        else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[109].npc_name, dialogdb.NPC_01[109].comment));
        }

        //2010 : 공양미의 출처
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[118].npc_name, dialogdb.NPC_01[118].comment));
        }

        //2012 : 청이의 거래
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[126].npc_name, dialogdb.NPC_01[126].comment));
        }

        //2013 : 향리댁 셋째아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[134].npc_name, dialogdb.NPC_01[134].comment));
        }

        //2014 : 잠잠해져야할 물살
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[142].npc_name, dialogdb.NPC_01[142].comment));
        }

        //2015 : 청이가 사간 것
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[150].npc_name, dialogdb.NPC_01[150].comment));
        }

        //2016 : 짚신을 사간 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[160].npc_name, dialogdb.NPC_01[160].comment));
        }

        //2017 : 배의 출항
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[166].npc_name, dialogdb.NPC_01[166].comment));
        }

        //2020 : 사공에게 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[204].npc_name, dialogdb.NPC_01[204].comment));
        }

        //2021 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[212].npc_name, dialogdb.NPC_01[212].comment));
        }

        //2023 : 3월의 보름날
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[234].npc_name, dialogdb.NPC_01[234].comment));
        }
        #endregion

        #region 아이템
        //1000 : 쌀
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[288].npc_name, dialogdb.NPC_01[288].comment));
        }

        //1005 : 주먹밥
        else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[525].npc_name, dialogdb.NPC_01[525].comment, true));

            //시스템 메세지
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[17].npc_name, dialogdb.NPC_01[17].comment, true));
            //주먹밥 아이템 제거
            ObjectManager.instance.RemoveItem(1005);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[18].npc_name, dialogdb.NPC_01[18].comment, true));

            //주먹밥 먹는 중..
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[809].npc_name, dialogdb.NPC_01[809].comment, true));

            //StopAllCoroutines();
            //대화
            //yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[19].npc_name, npcDatabaseScr.NPC_01[19].comment, true));
            //yield return StartCoroutine(ClearChat(npcDatabaseScr.NPC_01[18].npc_name, npcDatabaseScr.NPC_01[18].comment, true));

            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[19].npc_name, dialogdb.NPC_01[19].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[809].npc_name, dialogdb.NPC_01[809].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[20].npc_name, dialogdb.NPC_01[20].comment, true));
            //청이와 사내 단서 획득
            ObjectManager.instance.GetClue(2004);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[21].npc_name, dialogdb.NPC_01[21].comment));
        }

        //1006 : 비녀
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[304].npc_name, dialogdb.NPC_01[304].comment));
        }

        //1007 : 먹
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[312].npc_name, dialogdb.NPC_01[312].comment));
        }

        //1008 : 보리떡
        else if (ObjectManager.instance.GetEquipObjectKey() == 1008) 
        {
            //보리떡 제거
            ObjectManager.instance.RemoveItem(1008);
            //보리떡 전달 완료
            EventManager.instance.eventEndCheck.giveBoridduck_End = true;
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[528].npc_name, dialogdb.NPC_01[528].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[173].npc_name, dialogdb.NPC_01[173].comment, true));
            //꽃 획득
            ObjectManager.instance.GetItem(1009);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[174].npc_name, dialogdb.NPC_01[174].comment));
        }

        //1009 : 꽃
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[327].npc_name, dialogdb.NPC_01[327].comment));
        }

        //1011 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[343].npc_name, dialogdb.NPC_01[343].comment));
        }
        #endregion

        #region 조합 단서

        //4023 : 공양미를 구한 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[354].npc_name, dialogdb.NPC_01[354].comment));
        }

        //4015 : 이틀전에 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[362].npc_name, dialogdb.NPC_01[362].comment));
        }

        //4017 : 청이와 그의 관계
        else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[370].npc_name, dialogdb.NPC_01[370].comment));
        }

        //8032 : 함께 사라진 두사람
        else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[378].npc_name, dialogdb.NPC_01[378].comment));
        }

        //4033 : 무역의 중단
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[385].npc_name, dialogdb.NPC_01[385].comment));
        }

        //4018 : 청이의 가출
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[395].npc_name, dialogdb.NPC_01[395].comment));
        }
        #endregion

        //기본 대사
        else
        {
            yield return StartCoroutine(DialogManager.instance.NormalChat("거지"));
        }
    }
}