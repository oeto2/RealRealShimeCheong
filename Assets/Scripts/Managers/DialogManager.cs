using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 커스텀 클래스를 인스펙터 창에서 수정하기 위해서 추가
[System.Serializable]
public class Dialogue
{
    // 캐릭터 이름을 npc_name 창에 띄우기
    [Tooltip("대사 치는 캐릭터 이름")]
    public string name; // 캐릭터 이름

    // 대사 내용을 담을 배열 정의
    [Tooltip("대사 내용")]
    public string[] context;
}

[System.Serializable]
public class DialogueEvent
{
    // 대화 이벤트 이름 정의
    public string name;

    // vector2(x,y) x줄부터 y줄까지의 대사를 가져오기
    public Vector2 line;

    // 대사를 여러 명이 할 수 있도록 배열로 생성
    public Dialogue[] dialogues;
}

public class DialogManager : MonoBehaviour
{
    Dictionary<int, string[]> DialogData;

    //외부 스크립트 참조
    public Controller playerCtrlScr;

    //싱글톤
    public static DialogManager instance = null;

    //대화 데이터베이스
    public S_NPCdatabase_Yes npcDatabaseScr;

    //시스템 다이얼로그 오브젝트
    public GameObject Dialouge_System;

    //시스템 다이얼로그 텍스트
    public Text ChatText;

    //NPC 이름 Text
    public Text text_NpcName;

    //Npc 초상화
    public Image Npc_Portrait;

    //Player 초상화
    public Image player_Portrait;

    //대화종료 커서 오브젝트
    public GameObject obj_TalkEndCur;


    //초상화 스프라이트 이미지들
    [Tooltip("0:Null, 1:뺑덕, 2:거지, 3:승려, 4:귀덕, 5:장사, 6:장승, 7:장승2, 8:뱃사, 9:심청, 10:송나라, 11: 장지언")]
    public Sprite[] npc_Sprites;


    //Player 스프라이트 이미지들
    [Tooltip("0: Null, 1:기본")]
    public Sprite[] player_sprites;

    //출력중인 대사 값
    public string writerText = "";

    //시스템 메세지 코루틴이 이미 실행중인지 확인하는 flag
    public bool isSentence_Start;

    //대화가 전부 출력 되었는지
    public bool isSentenceEnd = false;

    //남은 대화가 더 있는지
    public bool remainSentence = false;

    // 글자색 설정 변수
    bool t_white = false;   // 노말 대사
    bool t_red = false;     // 강조 대사
    bool t_blue = false;    // 진엔딩
    bool t_violet = false;  // 추가 예정

    // 글자색 설정 문자는 대사 출력 무시
    bool t_ignore = false;

    // 랜덤 대사 출력 변수
    private int RandomNum;

    public Coroutine DialogueItemClue;

    //텍스트 스킵을 했는지 확인하는 flag
    public bool isSkip;
    //타이핑 속도
    public float typingSpeed = 0.02f;
    //스킵 타이핑 속도
    public float skipTypingSpeed = 0.0005f;


    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        DialogData = new Dictionary<int, string[]>();
    }

    //NPC 기본대사
    public IEnumerator NormalChat(string _narrator)
    {
        //커서 숨기기
        obj_TalkEndCur.SetActive(false);

        //시간 정지
        TimeManager.instance.StopTime();

        //다이얼로그 택스트 소리 재생
        EffectSoundManager.instance.PlayTalkTextSound();

        //다이얼로그 비우기
        CleanDialogue();

        //Player, Npc 초상화 초기화
        ResetNpcPortrait();
        ResetPlayerPortrait();

        //다이얼로그 Text 비우기
        CleanDialogue();

        //대화 중복실행 방지
        remainSentence = true;

        //Npc 초상화 자동 변경
        ChangeNpcPortrait(_narrator);

        //narratior 값에 따라 해당하는 랜덤 대사값 넘겨줌
        string narration = RandomNpcSentence(_narrator);
        string narration_2 = RandomNpcSentence2(_narrator);
        string narration_3 = RandomNpcSentence3(_narrator);

        //송나라 상인일 경우
        if (_narrator == "송나라 상인")
        {
            RandomNum = Random.Range(0, 3);
        }
        //그 외
        else
        {
            RandomNum = Random.Range(0, 2);
        }

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
                    //타이핑 속도 변경
                    typingSpeed = skipTypingSpeed;
                }

                //대사가 전부 출력되지 않았을 경우
                if (a < narration.Length)
                {
                    //대사 타이핑 속도
                    yield return new WaitForSeconds(typingSpeed);
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
                    //타이핑 속도 변경
                    typingSpeed = skipTypingSpeed;
                }

                //대사가 전부 출력되지 않았을 경우
                if (a < narration_2.Length)
                {
                    yield return new WaitForSeconds(typingSpeed);
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
                    //타이핑 속도 변경
                    typingSpeed = skipTypingSpeed;
                }

                //대사가 전부 출력되지 않았을 경우
                if (a < narration_3.Length)
                {
                    yield return new WaitForSeconds(typingSpeed);
                }
            }
            yield return null;
        }
        Debug.Log(ChatText);

        //대사 출력이 모두 완료 되었다면
        if (ChatText.text == narration || ChatText.text == narration_2 || ChatText.text == narration_3)
        {
            //대화종료 커서 보이기
            obj_TalkEndCur.SetActive(true);

            //대화 종료 조건 충족
            remainSentence = true;
            isSentenceEnd = true;

            //타이핑 속도 초기화
            typingSpeed = 0.02f;
        }
    }

    public IEnumerator ItemClueChat(string narrator, string narration)
    {
        //커서 숨기기
        obj_TalkEndCur.SetActive(false);

        //시간 정지
        TimeManager.instance.StopTime();

        //다이얼로그 비우기
        CleanDialogue();

        //다이얼로그 창 띄우기
        Dialouge_System.SetActive(true);

        //Player, Npc 초상화 초기화
        ResetNpcPortrait();
        ResetPlayerPortrait();

        Debug.Log("대화출력1");

        //남은대화 있음
        remainSentence = true;

        //다이얼로그 Text 비우기
        CleanDialogue();

        //Npc 초상화 자동 변경
        ChangeNpcPortrait(narrator);
        //Player 초상화 자동 변경
        ChagePlayerPortrait(narrator);

        Debug.Log(narration);
        int a = 0;

        string t_letter = "";

        //텍스트 타이핑
        for (a = 0; a < narration.Length; a++)
        {
            //writerText += narration[a];
            //ChatText.text = writerText;
            switch (narration[a])
            {
                // red
                case 'ⓡ':
                    t_white = false;
                    t_red = true;
                    t_blue = false;
                    t_violet = false;
                    t_ignore = true;
                    break;
                // write
                case 'ⓦ':
                    t_white = true;
                    t_red = false;
                    t_blue = false;
                    t_violet = false;
                    t_ignore = true;
                    break;
                // blue
                case 'ⓑ':
                    t_white = false;
                    t_red = false;
                    t_blue = true;
                    t_violet = false;
                    t_ignore = true;
                    break;
                // violet
                case 'ⓥ':
                    t_white = false;
                    t_red = false;
                    t_blue = false;
                    t_violet = true;
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
                    //t_letter = "<color=#B40404>" + narration[a] + "</color>";
                    t_letter = "<color=#850000>" + "<b>" + narration[a] + "</b>" + "</color>";
                    Debug.Log("1_red");
                }

                else if (t_blue)
                {
                    t_letter = "<color=#0d4577>" + "<b>" + narration[a] + "</b>" + "</color>";
                    Debug.Log("2_blue");
                }

                else if (t_violet)
                {
                    t_letter = "<color=#6a2c7a>" + "<b>" + narration[a] + "</b>" + "</color>";
                    Debug.Log("3_violet");
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
                //타이핑 속도 변경
                typingSpeed = skipTypingSpeed;
            }

            //대사 출력 중일 경우에만
            if (ChatText.text != narration)
            {
                //텍스트 타이핑 시간 조절
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        //타이핑 속도 변경
        typingSpeed = 0.02f;

        //대화 종료 조건 충족
        remainSentence = true;
        isSentenceEnd = true;
    }

    //다이얼로그 대화 출력
    public IEnumerator ItemClueChat(string narrator, string narration, bool _remainSentence)
    {
        //커서 숨기기
        obj_TalkEndCur.SetActive(false);

        //시간 정지
        TimeManager.instance.StopTime();

        //다이얼로그 비우기
        CleanDialogue();

        //다이얼로그 창 띄우기
        Dialouge_System.SetActive(true);

        //Player, Npc 초상화 초기화
        ResetNpcPortrait();
        ResetPlayerPortrait();


        //Player, Npc 초상화 보이기
        ChangeNpcPortrait(narrator);
        ChagePlayerPortrait(narrator);

        Debug.Log("대화출력2");

        string t_letter = "";

        //남은 대화가 있을 경우
        if (_remainSentence == true)
        {
            //남은대화 있음
            remainSentence = true;
            text_NpcName.text = narrator;

            // 거지 - 주먹밥 다이얼로그 대화 예외 처리
            if (narration == npcDatabaseScr.NPC_01[869].comment)
            {
                Debug.Log(narration);
                Debug.Log("1005번, 18번");
                Dialouge_System.SetActive(false);
            }
            if (narration != npcDatabaseScr.NPC_01[869].comment)
            {
                Debug.Log(narration);
                Debug.Log("1005번, 19번");
                Dialouge_System.SetActive(true);
            }

            //텍스트 타이핑
            for (int a = 0; a < narration.Length; a++)
            {
                //writerText += narration[a];
                //ChatText.text = writerText;

                switch (narration[a])
                {
                    // red
                    case 'ⓡ':
                        t_white = false;
                        t_red = true;
                        t_blue = false;
                        t_violet = false;
                        t_ignore = true;
                        break;
                    // write
                    case 'ⓦ':
                        t_white = true;
                        t_red = false;
                        t_blue = false;
                        t_violet = false;
                        t_ignore = true;
                        break;
                    // blue
                    case 'ⓑ':
                        t_white = false;
                        t_red = false;
                        t_blue = true;
                        t_violet = false;
                        t_ignore = true;
                        break;
                    // violet
                    case 'ⓥ':
                        t_white = false;
                        t_red = false;
                        t_blue = false;
                        t_violet = true;
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
                        //t_letter = "<color=#B40404>" + narration[a] + "</color>";
                        t_letter = "<color=#850000>" + "<b>" + narration[a] + "</b>" + "</color>";
                        //t_letter = "<color=#222222>" + "<b>" + narration[a] + "</b>" + "</color>";
                        Debug.Log("1_red");
                    }

                    else if (t_blue)
                    {
                        t_letter = "<color=#0d4577>" + "<b>" + narration[a] + "</b>" + "</color>";
                        Debug.Log("2_blue");
                    }

                    else if (t_violet)
                    {
                        t_letter = "<color=#6a2c7a>" + "<b>" + narration[a] + "</b>" + "</color>";
                        Debug.Log("3_violet");
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
                    //타이핑 속도 변경
                    typingSpeed = skipTypingSpeed;
                }

                //대사 출력 중일 경우에만
                if (ChatText.text != narration)
                {
                    //텍스트 타이핑 시간 조절
                    yield return new WaitForSeconds(typingSpeed);
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

                    //타이핑 속도 초기화
                    typingSpeed = 0.02f;

                    //Text 비우기
                    writerText = "";

                    break;
                }
                yield return null;
            }
        }
    }

    //해당하는 인덱스 값의 대화를 반환해주는 메서드
    public string GetNpcSentence(int _indexNum)
    {
        return npcDatabaseScr.NPC_01[_indexNum].comment;
    }

    //해당하는 인덱스 값의 NPC 이름을 반환해주는 메서드
    public string GetNpcName(int _indexNum)
    {
        return npcDatabaseScr.NPC_01[_indexNum].npc_name;
    }

    #region 시스템 메세지 로직
    //시스템 메세지 코루틴
    IEnumerator SystemMessage(string _narration, bool _exit)
    {
        //다이얼로그 비우기
        CleanDialogue();

        //코루틴 중복 실행 방지
        isSentence_Start = true;

        //Text 비우기
        writerText = "";

        //초상화 변경
        Npc_Portrait.sprite = npc_Sprites[0];

        //시스템 다이얼로그 활성화
        Dialouge_System.SetActive(true);

        int a = 0;

        for (a = 0; a < _narration.Length; a++)
        //for (a = 0; a < textSpeed; a++)
        {
            writerText += _narration[a];
            ChatText.text = writerText;

            //텍스트 타이핑 시간 조절
            //yield return null;

            if (a > 2 && Input.GetKeyDown(KeyCode.Z))
            {
                //코루틴 중복 실행 방지해제
                isSentence_Start = false;

                //시스템 다이얼로그 비활성화
                //Dialouge_System.SetActive(false);
            }

            yield return new WaitForSeconds(0.02f);
        }
        yield return null;

        //대사 출력 후 잠깐 딜레이
        yield return new WaitForSeconds(0.1f);

        //Z키를 다시 누를 때까지 무한정 대기
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //코루틴 중복 실행 방지해제
                isSentence_Start = false;

                if (_exit)
                {
                    //시스템 다이얼로그 비활성화
                    Dialouge_System.SetActive(false);
                }

                //Text 비우기
                writerText = "";
                break;
            }
            yield return null;
        }
    }

    //시스템 메세지 코루틴
    IEnumerator SystemMessage(string _narrator, string _narration, bool _exit)
    {
        //화자에 따라 초상화, 이름 변경
        ChangeNpcPortrait(_narrator);


        //코루틴 중복 실행 방지
        isSentence_Start = true;

        //Text 비우기
        writerText = "";

        //초상화 변경
        Npc_Portrait.sprite = npc_Sprites[0];

        //시스템 다이얼로그 활성화
        Dialouge_System.SetActive(true);

        //텍스트 타이핑
        for (int a = 0; a < _narration.Length; a++)
        {
            writerText += _narration[a];
            ChatText.text = writerText;

            //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
            {
                ChatText.text = _narration;

                //for문 조건 충족
                a = _narration.Length;
            }

            //대사 출력 중일 경우에만
            if (ChatText.text != _narration)
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
            if (ChatText.text == _narration && Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("Text 비우기");

                //Text 비우기
                writerText = "";

                break;
            }
            yield return null;
        }
    }

    //시스템 메세지를 시작해주는 메서드
    public void Start_SystemMessage(string _narration, bool _exit)
    {
        //코루틴 중복실행 방지
        if (!isSentence_Start)
        {
            StartCoroutine(SystemMessage(_narration, _exit));
        }
    }

    //시스템 메세지를 시작해주는 메서드(이름 포함)
    public void Start_SystemMessage(string _narrator, string _narration, bool _exit)
    {
        //코루틴 중복실행 방지
        if (!isSentence_Start)
        {
            StartCoroutine(SystemMessage(_narrator, _narration, _exit));
        }
    }

    //시스템 메세지가 끝나면 true, 아니면 false 반환)
    public bool IsSystemMessageEnd()
    {
        //다이얼로그 창이 종료 되었다면
        if (Dialouge_System.activeSelf == false)
        {
            return true;
        }

        //아니면
        else
        {
            return false;
        }
    }
    #endregion

    #region 약초 이벤트 대사
    //약초 넣기 대사 출력
    IEnumerator SystemMessage_HerbSentence()
    {
        //약초 넣기 시스템 메세지 출력
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(530), true));

        //이후에 7398로 수정하기
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(531), true));

        //다이얼로그 종료
        Dialouge_System.SetActive(false);
    }

    //약초 넣기 대사 시작
    public void StartPushHerbSentence()
    {
        StartCoroutine(SystemMessage_HerbSentence());
    }

    #endregion


    #region 가마솥 이벤트 대사

    //물바가지 대사
    IEnumerator SystemMessage_WaterBagage()
    {
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(522), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(282), true));
    }

    //물바가지 대사2
    IEnumerator SystemMessage_WaterBagage_2()
    {
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(522), true));
    }

    //물 바가지 대사 출력
    public void Start_WaterBageSentence()
    {
        StartCoroutine(SystemMessage_WaterBagage());
    }

    //물 바가지 대사 출력2
    public void Start_WaterBageSentence_2()
    {
        StartCoroutine(SystemMessage_WaterBagage_2());
    }


    //솥에 쌀만 넣었을 경우 대사
    IEnumerator SystemMessage_RiceSentence()
    {
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(523), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(291), true));
    }

    //솥에 쌀만 넣었을 경우 대사 출력
    public void Start_RiceSentence()
    {
        StartCoroutine(SystemMessage_RiceSentence());
    }

    #endregion

    //뱃사공 3 대사
    IEnumerator BoatMan3_Sentence()
    {
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(604), true));
    }

    //뱃사공3 대사출력
    public void Start_BoatMan3_Sentence()
    {
        StartCoroutine(BoatMan3_Sentence());
    }

    //과유불급 엔딩대화
    IEnumerator BadEndingSentence()
    {
        //배경 어둡게 변경
        EndingManager.instance.ShowEndingBG();

        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(419), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(420), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(421), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(422), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(423), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(424), true));

        //타이틀 이동
        EndingManager.instance.LoadTitleScene();
    }

    //과유불급 엔딩 시작
    public void StartBadEndingSentence()
    {
        StartCoroutine(BadEndingSentence());
    }

    //고립무원 엔딩대화
    IEnumerator BadEndingSentence2()
    {
        //배경 어둡게 변경
        EndingManager.instance.ShowEndingBG();

        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(795), DialogManager.instance.GetNpcSentence(795), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(796), DialogManager.instance.GetNpcSentence(796), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(797), DialogManager.instance.GetNpcSentence(797), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(798), DialogManager.instance.GetNpcSentence(798), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(799), DialogManager.instance.GetNpcSentence(799), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(800), DialogManager.instance.GetNpcSentence(800), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(801), DialogManager.instance.GetNpcSentence(801), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(802), DialogManager.instance.GetNpcSentence(802), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(803), DialogManager.instance.GetNpcSentence(803), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(804), DialogManager.instance.GetNpcSentence(804), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(805), DialogManager.instance.GetNpcSentence(805), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(806), DialogManager.instance.GetNpcSentence(806), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(807), DialogManager.instance.GetNpcSentence(807), true));

        //타이틀 이동
        EndingManager.instance.LoadTitleScene();
    }

    //고립무원 엔딩 시작
    public void StartBadEndingSentence2()
    {
        //고립무원 엔딩 시작
        StartCoroutine(BadEndingSentence2());
    }

    //Player 초상화 변경
    public void ChagePlayerPortrait(string _narrator)
    {
        switch (_narrator)
        {
            case "심학규":
                player_Portrait.sprite = player_sprites[1];
                break;
        }
    }

    //NPC 초상화 변경
    public void ChangeNpcPortrait(string _narrator)
    {
        Debug.Log($"초상화 변경 실행 : {_narrator}");


        switch (_narrator)
        {
            case "뺑덕 어멈":

                //초상화 변경
                Npc_Portrait.sprite = npc_Sprites[1];
                //심학규 이미지 리셋
                ResetPlayerPortrait();

                //이름 변경
                text_NpcName.text = "뺑덕 어멈";
                break;

            case "거지":

                //초상화 변경
                Npc_Portrait.sprite = npc_Sprites[2];
                //심학규 이미지 리셋
                ResetPlayerPortrait();

                //이름 변경
                text_NpcName.text = "거 지";
                break;

            case "승려":

                //초상화 변경
                Npc_Portrait.sprite = npc_Sprites[3];
                //심학규 이미지 리셋
                ResetPlayerPortrait();

                //이름 변경
                text_NpcName.text = "승 려";
                break;

            case "귀덕 어멈":

                //초상화 변경
                Npc_Portrait.sprite = npc_Sprites[4];
                //심학규 이미지 리셋
                ResetPlayerPortrait();

                //이름 변경
                text_NpcName.text = "귀덕 어멈";
                break;

            case "장사꾼":

                //초상화 변경
                Npc_Portrait.sprite = npc_Sprites[5];
                //심학규 이미지 리셋
                ResetPlayerPortrait();

                //이름 변경
                text_NpcName.text = "장사꾼";
                break;

            case "향리 댁 부인":

                //초상화 변경
                Npc_Portrait.sprite = npc_Sprites[6];
                //심학규 이미지 리셋
                ResetPlayerPortrait();

                //이름 변경
                text_NpcName.text = "향리 댁 부인";
                break;

            case "뱃사공":

                //초상화 변경
                Npc_Portrait.sprite = npc_Sprites[8];
                //심학규 이미지 리셋
                ResetPlayerPortrait();

                //이름 변경
                text_NpcName.text = "뱃사공";
                break;

            case "심청":

                //초상화 변경
                Npc_Portrait.sprite = npc_Sprites[9];
                //심학규 이미지 리셋
                ResetPlayerPortrait();

                //이름 변경
                text_NpcName.text = "심 청";
                break;

            case "송나라 상인":

                //초상화 변경
                Npc_Portrait.sprite = npc_Sprites[10];
                //심학규 이미지 리셋
                ResetPlayerPortrait();

                //이름 변경
                text_NpcName.text = "송나라 상인";
                break;

            case "장지언":

                //초상화 변경
                Npc_Portrait.sprite = npc_Sprites[11];
                //심학규 이미지 리셋
                ResetPlayerPortrait();

                //이름 변경
                text_NpcName.text = "장지언";
                break;

        }
    }

    //NPC 랜덤 대사값1
    public string RandomNpcSentence(string _narrator)
    {
        Debug.Log($"랜덤 대사 값 반환 : {_narrator}");

        switch (_narrator)
        {
            case "심학규":
                return null;

            case "뺑덕 어멈":
                return npcDatabaseScr.NPC_01[1].comment;

            case "거지":
                return npcDatabaseScr.NPC_01[6].comment;

            case "승려":
                return npcDatabaseScr.NPC_01[5].comment;

            case "귀덕 어멈":
                return npcDatabaseScr.NPC_01[2].comment;

            case "장사꾼":
                return npcDatabaseScr.NPC_01[4].comment;

            case "향리 댁 부인":
                return npcDatabaseScr.NPC_01[3].comment;

            case "뱃사공":
                return npcDatabaseScr.NPC_01[7].comment;

            case "심청":
                return null;

            case "송나라 상인":
                return npcDatabaseScr.NPC_01[8].comment;

            case "장지언":
                return npcDatabaseScr.NPC_01[563].comment;

            default:
                return null;
        }
    }

    //NPC 랜덤 대사값2
    public string RandomNpcSentence2(string _narrator)
    {
        Debug.Log($"랜덤 대사 값 반환 : {_narrator}");

        switch (_narrator)
        {
            case "심학규":
                return null;

            case "뺑덕 어멈":
                return npcDatabaseScr.NPC_01[399].comment;

            case "거지":
                return npcDatabaseScr.NPC_01[403].comment;

            case "승려":
                return npcDatabaseScr.NPC_01[407].comment;

            case "귀덕 어멈":
                return npcDatabaseScr.NPC_01[400].comment;

            case "장사꾼":
                return npcDatabaseScr.NPC_01[402].comment;

            case "향리 댁 부인":
                return npcDatabaseScr.NPC_01[401].comment;

            case "뱃사공":
                return npcDatabaseScr.NPC_01[404].comment;

            case "심청":
                return null;

            case "송나라 상인":
                return npcDatabaseScr.NPC_01[405].comment;

            case "장지언":
                return npcDatabaseScr.NPC_01[603].comment;

            default:
                return null;
        }
    }

    //NPC 랜덤 대사값3
    public string RandomNpcSentence3(string _narrator)
    {
        Debug.Log($"랜덤 대사 값 반환 : {_narrator}");

        switch (_narrator)
        {
            case "심학규":
                return null;

            case "뺑덕 어멈":
                return npcDatabaseScr.NPC_01[399].comment;

            case "거지":
                return npcDatabaseScr.NPC_01[403].comment;

            case "승려":
                return null;

            case "귀덕 어멈":
                return npcDatabaseScr.NPC_01[400].comment;

            case "장사꾼":
                return npcDatabaseScr.NPC_01[402].comment;

            case "향리 댁 부인":
                return npcDatabaseScr.NPC_01[401].comment;

            case "뱃사공":
                return npcDatabaseScr.NPC_01[404].comment;

            case "심청":
                return null;

            case "송나라 상인":
                return npcDatabaseScr.NPC_01[406].comment;

            default:
                return null;
        }
    }

    //다이얼로그 대사 값 비우기
    public void CleanDialogue()
    {
        ChatText.text = "";
        writerText = "";
    }

    //Npc초상화 초기화
    public void ResetNpcPortrait()
    {
        Npc_Portrait.sprite = npc_Sprites[0];
    }

    //Player초상화 초기화
    public void ResetPlayerPortrait()
    {
        player_Portrait.sprite = npc_Sprites[0];
    }

    //다이얼로그 종료
    public void Dialouge_End()
    {
        Dialouge_System.SetActive(false);
        isSentenceEnd = false;
        remainSentence = false;
        playerCtrlScr.TalkEnd();
    }
}