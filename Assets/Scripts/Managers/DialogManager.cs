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

    //시스템 초상화
    public Image System_Portrait;

    //초상화 스프라이트 이미지들
    public Sprite[] sprites;

    //출력중인 대사 값
    public string writerText = "";

    //시스템 메세지 코루틴이 이미 실행중인지 확인하는 flag
    public bool isSentence_Start;

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


    //시스템 메세지 코루틴
    IEnumerator SystemMessage(string _narration, bool _exit)
    {
        //코루틴 중복 실행 방지
        isSentence_Start = true;

        //Text 비우기
        writerText = "";

        //초상화 변경
        System_Portrait.sprite = sprites[0];

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

    

    //시스템 메세지를 시작해주는 메서드
    public void Start_SystemMessage(string _narration, bool _exit)
    {
        //코루틴 중복실행 방지
        if (!isSentence_Start)
        {
            StartCoroutine(SystemMessage(_narration, _exit));
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
}
