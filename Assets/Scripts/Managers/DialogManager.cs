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

    //시스템 초상화
    public Image System_Portrait;

    //초상화 스프라이트 이미지들
    [Tooltip("0:심학, 1:뺑덕, 2:거지, 3:스님, 4:귀덕, 5:장사, 6:장승, 7:장승2, 8:뱃사, 9:심청, 10:송나라")]
    public Sprite[] npc_Sprites;

    //출력중인 대사 값
    public string writerText = "";

    //시스템 메세지 코루틴이 이미 실행중인지 확인하는 flag
    public bool isSentence_Start;

    //대화가 전부 출력 되었는지
    public bool isSentenceEnd = false;

    //남은 대화가 더 있는지
    public bool remainSentence = false;

    // 글자색 설정 변수
    bool t_white = false;
    bool t_red = false;

    // 글자색 설정 문자는 대사 출력 무시
    bool t_ignore = false;

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
        GenerateData();
    }



    //다이얼로그 대화 출력
    IEnumerator ItemClueChat(string narrator, string narration, bool _remainSentence)
    {
        //Npc 초상화 자동 변경
        ChangeNpcPortrait(narrator);

        string t_letter = "";

        //심학규의 대사일 경우
        if (narrator == "심학규")
        {
            //초상화 변경
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = npc_Sprites[1];
        }
        else
        {
            //초상화 변경
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = npc_Sprites[0];
        }

        //남은 대화가 있을경우
        if (_remainSentence == true)
        {
            //남은대화 있음
            remainSentence = true;

            text_NpcName.text = narrator;

            //텍스트 타이핑
            for (int a = 0; a < narration.Length; a++)
            {
                //writerText += narration[a];
                //ChatText.text = writerText;

                switch (narration[a])
                {
                    case 'ⓡ':
                        t_white = false;
                        t_red = true;
                        t_ignore = true;
                        break;
                    case 'ⓦ':
                        t_white = true;
                        t_red = false;
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
                        t_letter = "<color=#B40404>" + narration[a] + "</color>";
                        Debug.Log("1_red");
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
                if (ChatText.text == writerText && Input.GetKeyDown(KeyCode.Z))
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


















    void GenerateData()
    {
        DialogData.Add(5000, new string[] { "?이것은 테스트지?", "그럼 테스트지 테스트야 테스트군 테스트똻" });
    }

    public string GetTalk(int id, int idx_Dialog)
    {
        return DialogData[id][idx_Dialog];
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
        System_Portrait.sprite = npc_Sprites[0];

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
        System_Portrait.sprite = npc_Sprites[0];

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
            StartCoroutine(SystemMessage(_narrator,_narration, _exit));
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

    //NPC 초상화 변경
    public void ChangeNpcPortrait(string _narrator)
    {
        Debug.Log($"초상화 변경 실행 : {_narrator}");
        

        switch (_narrator)
        {
            case "심학규":

                //초상화 변경
                System_Portrait.sprite = npc_Sprites[0];

                //이름 변경
                text_NpcName.text = "심학규";
                break;

            case "뺑덕어멈":

                //초상화 변경
                System_Portrait.sprite = npc_Sprites[1];

                //이름 변경
                text_NpcName.text = "뺑덕어멈";
                break;

            case "거지":

                //초상화 변경
                System_Portrait.sprite = npc_Sprites[2];

                //이름 변경
                text_NpcName.text = "거지";
                break;

            case "스님":

                //초상화 변경
                System_Portrait.sprite = npc_Sprites[3];

                //이름 변경
                text_NpcName.text = "스님";
                break;

            case "귀덕어멈":

                //초상화 변경
                System_Portrait.sprite = npc_Sprites[4];

                //이름 변경
                text_NpcName.text = "귀덕어멈";
                break;

            case "장사꾼":

                //초상화 변경
                System_Portrait.sprite = npc_Sprites[5];

                //이름 변경
                text_NpcName.text = "장사꾼";
                break;

            case "향리 댁 부인":

                //초상화 변경
                System_Portrait.sprite = npc_Sprites[6];

                //이름 변경
                text_NpcName.text = "향리 댁 부인";
                break;

            case "뱃사공":

                //초상화 변경
                System_Portrait.sprite = npc_Sprites[8];

                //이름 변경
                text_NpcName.text = "뱃사공";
                break;

            case "심청":

                //초상화 변경
                System_Portrait.sprite = npc_Sprites[9];

                //이름 변경
                text_NpcName.text = "심청";
                break;

            case "송나라 상인":

                //초상화 변경
                System_Portrait.sprite = npc_Sprites[10];

                //이름 변경
                text_NpcName.text = "송나라 상인";
                break;

        }
    }

    //다이얼로그 비우기
    public void CleanDialogue()
    {
        //NPC 이름 비우기
        text_NpcName.text = "";
    }
}
