using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

//저장할 데이터
[System.Serializable]
public class TimeSaveData
{
    //생성자
    public TimeSaveData(float _Time, int _Day)
    {
        time = _Time; day = _Day;
    }

    //저장할 시간
    public float time;

    //저장할 날짜
    public int day;
}

//불러올 데이터
[System.Serializable]
public class TimeLoadData
{
    //생성자
    public TimeLoadData(float _Time, int _Day)
    {
        time = _Time; day = _Day;
    }

    //불러올 시간
    public float time;

    //불러올 날짜
    public int day;
}

public class TimeManager : MonoBehaviour
{
    //외부 스크립트 참조
    public TutorialManager tutorialManagerScr;
    public UIManager uiManagerScr;

    //캘린더의 애니메이터
    public Animator animator_Celender;

    //시간을 표시할 텍스트
    public Text text_TimeText;

    //날짜 UI
    public GameObject gameObjcet_DayUI;

    //실제 시간
    [SerializeField]
    private float float_RealTime;

    //시간 배속
    [SerializeField]
    private int timeMultipleCation;

    //하루가 지나기위해 걸리는시간
    public int int_DayMinute;

    //몇일 째인지 확인하는 변수
    public int int_DayCount = 1;

    //시간 멈추기
    private bool timeStop;

    //플레이타임 시간
    private float float_PlayTimeHour = 0;
    //플레이타임 분
    private float float_PlayTimeMinute = 0;
    //플레이타임 초
    public float float_PlayTimeSec = 0;

    //누적 플레이 시간
    public float float_SavePlayTime = 0;

    //저장할 데이터 클래스
    public TimeSaveData curTimeSaveData;

    //저장 파일 위치
    private string saveFilePath;

    //불러올 데이터 클래스
    public TimeLoadData curTimeLoadData;

    //플레이타임
    private string playTime;

    //해시계 이미지 번호
    public int sunClcokImageNum;

    private void Start()
    {
        //저장 파일 위치
        saveFilePath = Application.persistentDataPath + "/TimeDataText.txt";
    }

    private void FixedUpdate()
    {
        //해시계 애니메이션
        ChageSunClock();

        //플레이 타임 = 델타 타임
        float_PlayTimeSec += Time.deltaTime;

        //Debug.Log(float_PlayTimeSec);

        if (!timeStop)
        {
            //실제 시간 = 시간 + 배속
            float_RealTime += Time.deltaTime * timeMultipleCation;
            text_TimeText.text = float_RealTime.ToString("F0");

            //만약 하루가 지났을 경우
            if (int_DayMinute <= float_RealTime)
            {
                //날짜 증가
                int_DayCount++;

                //시간 리셋
                ResetTime();

                //캘린더 애니메이션 진행
                NextDayAnimaton(int_DayCount);

                //시간 정지
                StopTime();

                //하루가 지났음
                tutorialManagerScr.PassDay();
            }
        }
    }

    //날짜가 지났을때 실행할 애니메이션
    public void NextDayAnimaton(int _day)
    {
        //캘린더 날짜 바꾸기
        animator_Celender.SetInteger("DayNum", _day);
    }

    //날짜 UI 보여주기
    public void ShowDayUI()
    {
        gameObjcet_DayUI.SetActive(true);
    }

    //날짜 UI 끄기
    public void CloseDayUI()
    {
        gameObjcet_DayUI.SetActive(false);
    }

    //시간 리셋
    public void ResetTime()
    {
        float_RealTime = 0f;
        text_TimeText.text = float_RealTime.ToString("F0");
    }

    //시간 멈추기
    public void StopTime()
    {
        timeStop = true;
    }

    //시간 계속가기
    public void ContinueTime()
    {
        timeStop = false;
    }

    //하루 지난뒤 대화 완료
    public void PassDaySentenceEnd()
    {
        //시간 계속 흐르기
        ContinueTime();
    }

    //하루 지나기
    public void PassOneDay()
    {
        //하루 지나기
        int_DayCount++;
        //시간 0으로 초기화
        float_RealTime = 0;
        //캘린더 날짜 변경
        NextDayAnimaton(int_DayCount);
    }


    //데이터 저장
    public void Save(int _slotNum)
    {
        Debug.Log("Save TimeManagerData");
        

        //저장할 데이터 넣기
        curTimeSaveData = new TimeSaveData(float_RealTime, int_DayCount);

        //세이브 데이터
        string jSaveData = JsonUtility.ToJson(curTimeSaveData);

        //데이터 파일 생성
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);
    }

    //데이터 로드
    public void Load(int _SlotNum)
    {
        Debug.Log("Load TimeManagerData");

        //세이브 파일 읽어오기
        string jLoadData = File.ReadAllText(saveFilePath + _SlotNum.ToString());

        //읽어온 파일 리스트에 저장
        curTimeLoadData = JsonUtility.FromJson<TimeLoadData>(jLoadData);

        //시간 재설정
        float_RealTime = curTimeLoadData.time;

        //날짜 재설정
        int_DayCount = curTimeLoadData.day;

        //캘린더 날짜 변경
        NextDayAnimaton(int_DayCount);
    }

    //UI 슬롯에 표시할 날짜
    public string GetDayCount()
    {
        return int_DayCount.ToString() + "일째";
    }

    //플레이 타임을 구하는 메서드
    public string GetPlayTimeText()
    {
        //게임을 1분 이상 했다면
        if (float_PlayTimeSec / 60 >= 1)
        {
            float_PlayTimeMinute = float_PlayTimeSec / 60;
        }
        //아니면 0
        else
        {
            float_PlayTimeMinute = 0;
        }

        //게임을 60분 이상 했다면
        if (float_PlayTimeSec / 3600 >= 1)
        {
            float_PlayTimeHour = float_PlayTimeSec / 3600;
        }
        //아니면 0
        else
        {
            float_PlayTimeHour = 0;
        }

        playTime = "진행 시간 : " + MathF.Truncate(float_PlayTimeHour) + "시간 " + MathF.Truncate(float_PlayTimeMinute) + "분";

        Debug.Log("playTimetext : " + playTime);

        return playTime;
    }

    //넘겨줄 시간 값
    public float GetPlayTimeSec(int _slotNum)
    {
        Debug.Log("넘겨준 플레이 타임 : " + float_PlayTimeSec);

        //현재 플레이 타임 값을 넘겨줌
        return float_PlayTimeSec;
    }

    //받아올 기존 플레이타임 값
    public void SetPlayTimeSec(float _playTime)
    {
        Debug.Log("저장된 플레이 타임 : " + _playTime.ToString());

        //누적 플레이 시간 받아오기
        float_SavePlayTime = _playTime;
        //현재 플레이 타임을 누적 플레이 타임 값으로 변경
        float_PlayTimeSec = float_SavePlayTime;
    }

    //해시계 UI 이미지 변경
    public void ChageSunClock()
    {
        sunClcokImageNum = ((int)MathF.Truncate(float_RealTime) / 5);
        uiManagerScr.ChangeSunClockImage(sunClcokImageNum);
    }
}
