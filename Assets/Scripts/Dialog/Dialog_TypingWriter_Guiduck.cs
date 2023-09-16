using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Guiduck : MonoBehaviour
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

    //대화가 전부 출력 되었는지
    public bool isSentenceEnd = false;

    //남은 대화가 더 있는지
    public bool remainSentence = false;

    // 랜덤 대사 출력 변수
    private int RandomNum;

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

    void Update()
    {
        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("z키 누름! 귀덕어멈!!!!");

            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false && !remainSentence)
            {
                Debug.Log("귀덕어멈 대화 시작");
                images_NPC.SetActive(true);
                bool_isNPC = true;
                StartCoroutine(TextPractice());

                Trigger_NPC.instance.isNPCTrigger = true;
                //GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
            }

            else if(isSentenceEnd)
            {
                Debug.Log("귀덕어멈 대화 끝");

                //캐릭터 이동제한 해제
                controller_scr.TalkEnd();

                images_NPC.SetActive(false);
                // images_NPC_portrait.SetActive(false);
                bool_isNPC = false;
                trigger_npc.isNPCTrigger = false;

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
        //초상화 변경
        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];

        int a = 0;
        string narrator = characternameText = CharacterName.text = dialogdb.NPC_01[2].npc_name;
        string narration = dialogdb.NPC_01[2].comment;
        string narration_2 = dialogdb.NPC_01[400].comment;
        RandomNum = Random.Range(0, 2);

        //narrator = CharacterName.text;

        //텍스트 타이핑
        if (RandomNum == 0)
        {
            for (a = 0; a < narration.Length; a++)
            //for (a = 0; a < textSpeed; a++)
            {
                writerText += narration[a];
                ChatText.text = writerText;

                //텍스트 타이핑 시간 조절
                //yield return null;

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    //대화 끝
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

                //텍스트 타이핑 시간 조절
                //yield return null;

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    //대화 끝
                    isSentenceEnd = true;
                }

                yield return new WaitForSeconds(0.02f);
            }
            yield return null;
        }
        Debug.Log(writerText);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //대화 끝
            isSentenceEnd = true;
        }

        //키(default : space)를 다시 누를 때까지 무한정 대기
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //대화 끝
                isSentenceEnd = true;
                break;
            }

            yield return null;
        }
    }

    IEnumerator ItemClueChat(string narrator, string narration)
    {
        int a = 0;
        CharacterName.text = narrator;
        //characternameText = narrator;
        writerText = "";

        //narrator = CharacterName.text;

        //텍스트 타이핑
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            //텍스트 타이핑 시간 조절
            //yield return null;

            if (Input.GetKeyDown(KeyCode.Z))
            {
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

        //키(default : space)를 다시 누를 때까지 무한정 대기
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //대화 끝
                isSentenceEnd = true;
                break;
            }
            yield return null;
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

    IEnumerator TextPractice()
    {
        //만약 3월15일 대사 이벤트 중이라면
        if(EventManager.instance.eventProgress.day15ClueStart && !EventManager.instance.eventEndCheck.day15ClueGet)
            
        {
            //주막 퍼즐 완료 후 꽃 전달을 완료 했을 때
            if (EventManager.instance.eventProgress.joomackPuzzle_Clear && EventManager.instance.eventProgress.giveFlowerEnd && 
                EventManager.instance.eventEndCheck.giveBoridduck_End)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[180].npc_name, dialogdb.NPC_01[180].comment,true));
                //3월 보름날 단서 획득
                ObjectManager.instance.GetClue(2023);
                //3월 보름날 단서 획득 이벤트 종료
                EventManager.instance.eventEndCheck.day15ClueGet = true;
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[181].npc_name, dialogdb.NPC_01[181].comment));
            }

            //배의 출항 단서로 대화시
            else if(ObjectManager.instance.GetEquipObjectKey() == 2017)
            {
                ////주막퍼즐 시작
                //GameManager.instance.JoomackPuzzleStart();
                Debug.Log("배의 출항 대사 실행");

                //주막 퍼즐을 클리어 했다면
                if (EventManager.instance.eventProgress.joomackPuzzle_Clear)
                {
                    yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[171].npc_name, dialogdb.NPC_01[171].comment, true));
                    //보리떡 획득
                    ObjectManager.instance.GetItem(1008);
                    //보리떡 전달 이벤트 시작
                    EventManager.instance.EventActive(Events.boridduck);
                    yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[172].npc_name, dialogdb.NPC_01[172].comment));
                }
                else
                {
                    //주막 퍼즐을 클리어하지 못했다면
                    yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[164].npc_name, dialogdb.NPC_01[164].comment, true));
                    //주막 퍼즐 시작
                    GameManager.instance.JoomackPuzzleStart();
                }
            }

            //주막 퍼즐 미완료 후 꽃 전달을 완료 했을 경우
            else
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[178].npc_name, dialogdb.NPC_01[178].comment, true));
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[179].npc_name, dialogdb.NPC_01[179].comment));
            }
        }

        //이벤트 중이 아니라면
        else
        {
            #region 단서
            //2000 : 승상댁의 수양딸
            if (ObjectManager.instance.GetEquipObjectKey() == 2000)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[10].npc_name, dialogdb.NPC_01[10].comment));
            }
            //2001 : 청이의 거짓말
            else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[23].npc_name, dialogdb.NPC_01[23].comment));
            }
            //2002 : 청이의 행방
            else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
            {
                //청이와 장터 획득
                ObjectManager.instance.GetClue(2003);

                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[31].npc_name, dialogdb.NPC_01[31].comment));
            }
            //2003 : 청이와 장터
            else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[39].npc_name, dialogdb.NPC_01[39].comment));
            }
            //2004 : 청이와 사내
            else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
            {
                //승려와 청이
                ObjectManager.instance.GetClue(2007);

                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[47].npc_name, dialogdb.NPC_01[47].comment));
            }
            //2005 : 누군가의 아들
            else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[58].npc_name, dialogdb.NPC_01[58].comment));
            }
            //2006 : 송나라 상인과 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[66].npc_name, dialogdb.NPC_01[66].comment));
            }
            //2007 : 승려와 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[79].npc_name, dialogdb.NPC_01[79].comment));
            }
            //2008 : 승려의 마음
            else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[87].npc_name, dialogdb.NPC_01[87].comment));
            }
            //2009 : 청이의 도움
            else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[95].npc_name, dialogdb.NPC_01[95].comment));
            }
            //2010 : 공양미 삼백 석
            else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
            {
                //공양미의 출처 단서 획득
                ObjectManager.instance.GetClue(2011);

                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[105].npc_name, dialogdb.NPC_01[105].comment));
            }
            //2011 : 공양미의 출처
            else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[114].npc_name, dialogdb.NPC_01[114].comment));
            }
            //2012 : 청이의 거래
            else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[122].npc_name, dialogdb.NPC_01[122].comment));
            }
            //2014 : 잠잠해져야 할 물살
            else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
            {
                // 기본 대사와 같음, 생략 가능할지도
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[138].npc_name, dialogdb.NPC_01[138].comment));
            }
            //2015 : 청이가 사간 것
            else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[146].npc_name, dialogdb.NPC_01[146].comment));
            }
            //2016 : 짚신을 사간 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[156].npc_name, dialogdb.NPC_01[156].comment));
            }
            //2017 : 배의 출항
            else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
            {
                ////주막퍼즐 시작
                //GameManager.instance.JoomackPuzzleStart();
                Debug.Log("배의 출항 대사 실행");

                //주막 퍼즐을 클리어 했다면
                if (EventManager.instance.eventProgress.joomackPuzzle_Clear)
                {
                    yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[171].npc_name, dialogdb.NPC_01[171].comment, true));
                    //보리떡 획득
                    ObjectManager.instance.GetItem(1008);
                    //보리떡 전달 이벤트 시작
                    EventManager.instance.EventActive(Events.boridduck);
                    yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[172].npc_name, dialogdb.NPC_01[172].comment));
                }

                else
                {
                    //주막 퍼즐을 클리어하지 못했다면
                    yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[164].npc_name, dialogdb.NPC_01[164].comment, true));
                    //주막 퍼즐 시작
                    GameManager.instance.JoomackPuzzleStart();
                }
            }
            //2018 : 노점의 단골
            else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[183].npc_name, dialogdb.NPC_01[183].comment));
            }
            //2019 : 뜨지 않는 배
            else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[191].npc_name, dialogdb.NPC_01[191].comment));
            }
            //2021 : 사공의 물건
            else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[208].npc_name, dialogdb.NPC_01[208].comment));
            }
            //2023 : 3월 보름날
            else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[230].npc_name, dialogdb.NPC_01[230].comment));
            }
            #endregion

            #region 아이템
            //1000 : 쌀
            else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[284].npc_name, dialogdb.NPC_01[284].comment));
            }
            //1001 : 장작
            else if (ObjectManager.instance.GetEquipObjectKey() == 1001)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[250].npc_name, dialogdb.NPC_01[250].comment));
            }
            //1002 : 부시와 부싯돌
            else if (ObjectManager.instance.GetEquipObjectKey() == 1002)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[258].npc_name, dialogdb.NPC_01[258].comment));
            }
            //1003 : 바가지
            else if (ObjectManager.instance.GetEquipObjectKey() == 1003)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[267].npc_name, dialogdb.NPC_01[267].comment));
            }
            //1005 : 주먹밥
            else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[293].npc_name, dialogdb.NPC_01[293].comment));
            }
            //1006 : 비녀
            else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[300].npc_name, dialogdb.NPC_01[300].comment));
            }
            //1007 : 먹
            else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[308].npc_name, dialogdb.NPC_01[308].comment));
            }
            //1008 : 보리떡
            else if (ObjectManager.instance.GetEquipObjectKey() == 1008)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[316].npc_name, dialogdb.NPC_01[316].comment));
            }
            //1009 : 꽃
            else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
            {
                //꽃 아이템제거
                ObjectManager.instance.RemoveItem(1009);

                //꽃 전달 완료
                EventManager.instance.eventProgress.giveFlowerEnd = true;

                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[175].npc_name, dialogdb.NPC_01[175].comment, true));
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[176].npc_name, dialogdb.NPC_01[176].comment, true));

                //약초 아이템 획득
                ObjectManager.instance.GetItem(1010);
                //3월 보름날 대화 이벤트 시작
                EventManager.instance.eventProgress.day15ClueStart = true;
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[177].npc_name, dialogdb.NPC_01[177].comment));
            }
            //1011 : 사공의 물건
            else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[338].npc_name, dialogdb.NPC_01[338].comment));
            }
            #endregion

            #region 조합 단서
            //4023 : 공양미를 구한 방법
            else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[350].npc_name, dialogdb.NPC_01[350].comment));
            }
            //4015 : 청이가 사라진 날
            else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
            {
                //사공에게 있었던일 단서 획득
                ObjectManager.instance.GetClue(2020);

                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[358].npc_name, dialogdb.NPC_01[358].comment));
            }
            //4017 : 청이와 그의 관계
            else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[366].npc_name, dialogdb.NPC_01[366].comment));
            }
            //8032 : 함께 사라진 두 사람
            else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[374].npc_name, dialogdb.NPC_01[374].comment));
            }
            //4033 : 무역의 중단
            else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[382].npc_name, dialogdb.NPC_01[382].comment));
            }
            //4018 : 청이의 가출
            else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
            {
                yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[392].npc_name, dialogdb.NPC_01[392].comment));
            }
            #endregion

            #region 기본 대사
            else
            {
                yield return StartCoroutine(NormalChat());
            }
            #endregion
        }
    }

    //다이얼로그 대화 실행
    public void StartDialogSentence()
    {
        StartCoroutine(TextPractice());
    }
}