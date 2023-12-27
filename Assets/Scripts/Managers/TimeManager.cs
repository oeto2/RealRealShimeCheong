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
    public TimeSaveData(float _Time, int _Day, byte _curColorValue, bool _realTimeStop)
    {
        time = _Time;
        day = _Day; 
        curColorValue = _curColorValue;
        realTimeStop = _realTimeStop;
    }

    //저장할 시간
    public float time;

    //저장할 날짜
    public int day;

    //현재 배경의 컬러값
    public byte curColorValue;

    //시간 정지 여부
    public bool realTimeStop;
}

//불러올 데이터
[System.Serializable]
public class TimeLoadData
{
    //생성자
    public TimeLoadData(float _Time, int _Day, byte _curColorValue, bool _realTimeStop)
    {
        time = _Time;
        day = _Day;
        curColorValue = _curColorValue;
        realTimeStop = _realTimeStop;
    }

    //불러올 시간
    public float time;

    //불러올 날짜
    public int day;

    //현재 배경의 컬러값
    public byte curColorValue;

    //시간 정지 여부
    public bool realTimeStop;
}

public class TimeManager : MonoBehaviour
{
    //외부 스크립트 참조
    public TutorialManager tutorialManagerScr;
    public UIManager uiManagerScr;
    public MoonLight moonlightScr;
    public CameraMove cameraMoveScr;

    //캘린더의 애니메이터
    public Animator animator_Celender;

    //시간을 표시할 텍스트
    public Text text_TimeText;

    //날짜 UI
    public GameObject gameObjcet_DayUI;

    //실제 시간
    [SerializeField]
    private float float_RealTime;
    private int int_RealTime;

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

    //어두워져야하는 오브젝트들
    public SpriteRenderer[] spriteRen_ObumbrateObj;

    //현재 오브젝트들의 RGB값
    public Color32 curObjectRGB;

    //최소 밝기
    public int minBrightness;

    //오브젝트들의 아침 RGB값
    public Color32 startRGBValue;

    //오브젝트들의 밤 RGB값
    public Color32 nightRGBValue;

    //오브젝트 밝기 세팅이 끝났는지 
    private bool isSettingEnd;

    //1초 코루틴이 실행중인지
    private bool isOneSecStart;

    //주막의 스프라이트 렌더
    public SpriteRenderer spriteRen_Joomack;

    //주막의 스파라이트 이미지들
    public Sprite[] sprite_Joomacks;

    //싱글톤 패턴
    public static TimeManager instance = null;

    //시간이 정지 되었는지 확인하는 flag
    public bool isTimeStop;

    //시간 진짜 정지 (엔딩)
    public bool realTimeStop;

    //고립무원 엔딩을 위한 플래그
    public bool isEndingStart;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        //저장 파일 위치
        saveFilePath = Application.persistentDataPath + "/TimeDataText.txt";


        //오브젝트의 현재 RGB값
        curObjectRGB = startRGBValue;
    }

    private void Update()
    {
        //플레이어가 집 안에 있을 경우 시간 정지
        if (cameraMoveScr.int_CurLimitNum == 0 || cameraMoveScr.int_CurLimitNum == 1 || tutorialManagerScr.events == TutorialEvents.TurnOnLights
            || tutorialManagerScr.events == TutorialEvents.GetItems || tutorialManagerScr.events == TutorialEvents.TalkToHyang)
        {
            TimeManager.instance.StopTime();
        }
    }

    private void FixedUpdate()
    {
        //날짜가 모두 지났을 경우
        if(int_DayCount == 15)
        {
            if(!isEndingStart)
            {
                isEndingStart = true;
                //배드엔딩 진행
                DialogManager.instance.StartBadEndingSentence2();
            }
        }


        //float RealTime을 int로 변환후 프레임당 갱신
        int_RealTime = (int)MathF.Truncate(float_RealTime);

        //오브젝트들 밝기 설정
        if (!isSettingEnd)
        {
            ObjectStartRGB_Value(startRGBValue.r);
        }


        //오브젝트 밝기 조절
        if (!isOneSecStart)
        {
            StartCoroutine(OneSecStartCoroutine());
        }


        //해시계 애니메이션
        ChageSunClock();

        //2초에 한번 달 밝기 조절
        if ((int)MathF.Truncate(float_RealTime) % 2 == 0)
        {
            //달 밝기 바꾸기
            moonlightScr.BrightenMoon((byte)MathF.Truncate(float_RealTime));

            //오브젝트 밝기 바꾸기
            //ObumbrateObject((int)MathF.Truncate(curObjectRGB);
        }

        //플레이 타임 = 델타 타임
        float_PlayTimeSec += Time.deltaTime;

        //Debug.Log(float_PlayTimeSec);

        if (!timeStop && !realTimeStop)
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
                if(int_DayCount == 2)
                {
                    StopTime();
                }
                
                //아직 D-day가 되지않았다면
                if(int_DayCount < 15)
                {
                    //플레이어 위치 변경 및 대사
                    tutorialManagerScr.PassDay();
                }

                //만약 약초물을 마신 상태라면
                if (EventManager.instance.drinkHerb)
                {
                    //약초물 플래그 초기화

                    EventManager.instance.drinkHerb = false;

                    //플레이어 스텟 초기화
                    EventManager.instance.PlayerStateReset();
                }
            }


            #region 주막 이미지변경

            //0 ~ 49초
            else if(int_RealTime >= 0 && int_RealTime <50)
            {
                if(spriteRen_Joomack.sprite != sprite_Joomacks[0])
                {
                    ChageJoomack(sprite_Joomacks[0]);
                }
            }

            //50 ~ 99초
            else if (int_RealTime >= 50 && int_RealTime < 100)
            {
                if (spriteRen_Joomack.sprite != sprite_Joomacks[1])
                {
                    ChageJoomack(sprite_Joomacks[1]);
                }
            }

            //100 ~ 149초
            else if (int_RealTime >= 100 && int_RealTime < 149)
            {
                if (spriteRen_Joomack.sprite != sprite_Joomacks[2])
                {
                    ChageJoomack(sprite_Joomacks[2]);
                }
            }

            //150 ~ 199초
            else if (int_RealTime >= 150 && int_RealTime < 199)
            {
                if (spriteRen_Joomack.sprite != sprite_Joomacks[3])
                {
                    ChageJoomack(sprite_Joomacks[3]);
                }
            }

            //200 ~ 250초
            else if (int_RealTime >= 200 && int_RealTime < 250)
            {
                if (spriteRen_Joomack.sprite != sprite_Joomacks[4])
                {
                    ChageJoomack(sprite_Joomacks[4]);
                }
            }

            //250 ~ 299초
            else if (int_RealTime >= 250 && int_RealTime < 300)
            {
                if (spriteRen_Joomack.sprite != sprite_Joomacks[5])
                {
                    ChageJoomack(sprite_Joomacks[5]);
                }
            }

            #endregion


            
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
        isTimeStop = true;
        //시간 멈추기
        timeStop = true;
    }

    //시간 계속가기
    public void ContinueTime()
    {
        isTimeStop = false;

        //시간 흐르기
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
        curTimeSaveData = new TimeSaveData(float_RealTime, int_DayCount, curObjectRGB.r, realTimeStop);

        //세이브 데이터
        string jSaveData = JsonUtility.ToJson(curTimeSaveData);

        //데이터 파일 생성
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);
    }

    //데이터 로드
    public void Load(int _SlotNum)
    {
        Debug.Log("Load TimeManagerData");
        Debug.Log(_SlotNum);

        if(_SlotNum <= 2)
        {
            //세이브 파일 읽어오기
            string jLoadData = File.ReadAllText(saveFilePath + _SlotNum.ToString());

            //읽어온 파일 리스트에 저장
            curTimeLoadData = JsonUtility.FromJson<TimeLoadData>(jLoadData);

            //시간 재설정
            float_RealTime = curTimeLoadData.time;

            //날짜 재설정
            int_DayCount = curTimeLoadData.day;

            //시간 Text 변경
            text_TimeText.text = float_RealTime.ToString("F0");

            //시간 정지 설정
            realTimeStop = curTimeLoadData.realTimeStop;

            //컬러값 재설정
            curObjectRGB = new Color32(curTimeLoadData.curColorValue, curTimeLoadData.curColorValue,
                curTimeLoadData.curColorValue, 255);

            for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
            {
                //만약 색을 바꿀 오브젝트가 존재한다면
                if (spriteRen_ObumbrateObj[i] != null)
                {
                    //색깔 변경
                    spriteRen_ObumbrateObj[i].color = curObjectRGB;
                }
            }

            //캘린더 날짜 변경
            NextDayAnimaton(int_DayCount);
        }
        
    }

    //넘겨줄 현재 날짜
    public int GetDay()
    {
        return int_DayCount;
    }

    //UI 슬롯에 표시할 날짜
    public string GetDayCountText()
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

    //오브젝트들 시작 RGB값
    public void ObjectStartRGB_Value(int _rgb)
    {
        //시작 RGB값으로 변경
        for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
        {
            //만약 색을 바꿀 오브젝트가 존재한다면
            if (spriteRen_ObumbrateObj[i] != null)
            {
                spriteRen_ObumbrateObj[i].color = new Color32((byte)_rgb, (byte)_rgb, (byte)_rgb, 255);
            }
        }
        isSettingEnd = true;
    }


    //오브젝트 어둡게하기
    public void ObumbrateObject(Color32 _curObjectColor)
    {

        //아침에 증가되야하는 값 = (255 - 시작 RGB값) / 아침시간
        int pluseValue_moring = (int)MathF.Truncate((255 - startRGBValue.r) / 35);

        //밤에 줄어들어야하는 값 = (255 - 밤 RGB값) / 밤 시간
        int minusValeue_night = (int)MathF.Truncate((255 - nightRGBValue.r) / 60);

        ////만약 다음날이 되었을경우
        //if ((int)MathF.Truncate(float_RealTime) == 0)
        //{
        //    Debug.Log("0시");
        //    for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
        //    {
        //        //만약 색을 바꿀 오브젝트가 존재한다면
        //        if (spriteRen_ObumbrateObj[i] != null)
        //        {
        //            //색깔 변경 (시작 값)
        //            spriteRen_ObumbrateObj[i].color = startRGBValue;
        //        }
        //    }
        //}

        //만약 아침일 경우(0 ~ 35초) = 초당 3씩 밝아짐
        if ((int)MathF.Truncate(float_RealTime) > 0 && (int)MathF.Truncate(float_RealTime) <= 35)
        {
            Debug.Log("아침");

            for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
            {
                //만약 색을 바꿀 오브젝트가 존재한다면
                if (spriteRen_ObumbrateObj[i] != null)
                {
                    //색깔 변경
                    spriteRen_ObumbrateObj[i].color = new Color32((byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), 255);
                }
            }

            //현재 오브젝트 RGB값 갱신
            curObjectRGB = new Color32((byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), 255);
        }

        //만약 낮이 되었을 경우 (35 ~ 150초)
        else if ((int)MathF.Truncate(float_RealTime) >= 35 && (int)MathF.Truncate(float_RealTime) <= 150)
        {
            Debug.Log("낮");

            if (curObjectRGB.r < 255)
            {
                if (curObjectRGB.r < 250)
                {
                    for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
                    {
                        //만약 색을 바꿀 오브젝트가 존재한다면
                        if (spriteRen_ObumbrateObj[i] != null)
                        {
                            //색깔 변경
                            spriteRen_ObumbrateObj[i].color = new Color32((byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), 255);
                        }
                    }

                    //현재 오브젝트 RGB값 갱신
                    curObjectRGB = new Color32((byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), 255);
                }

                else if (curObjectRGB.r > 250)
                {
                    for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
                    {
                        //만약 색을 바꿀 오브젝트가 존재한다면
                        if (spriteRen_ObumbrateObj[i] != null)
                        {
                            //색깔 변경
                            spriteRen_ObumbrateObj[i].color = new Color32(255, 255, 255, 255);
                        }
                    }

                    //현재 오브젝트 RGB값 갱신
                    curObjectRGB = new Color32((byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), 255);
                }

            }
        }


        // 만약 밤이 되었을 경우 (150 ~ 210)
        else if ((int)MathF.Truncate(float_RealTime) > 150 && (int)MathF.Truncate(float_RealTime) <= 210)
        {
            Debug.Log("밤");

            //현재 맵의 밝기가 밤의 밝기보다 밝다면
            if (curObjectRGB.r >= nightRGBValue.r)
            {
                for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
                {
                    //만약 색을 바꿀 오브젝트가 존재한다면
                    if (spriteRen_ObumbrateObj[i] != null)
                    {
                        //색깔 변경
                        spriteRen_ObumbrateObj[i].color = new Color32((byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), 255);
                    }
                }

                //현재 오브젝트 RGB값 갱신
                curObjectRGB = new Color32((byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), 255);
            }   
        }

        //야간 (210 ~ 300)
        else if(((int)MathF.Truncate(float_RealTime) > 210 && (int)MathF.Truncate(float_RealTime) <= 300))
        {
            Debug.Log("한밤중");

            //현재 맵의 밝기가 밤의 밝기보다 밝다면
            if (curObjectRGB.r >= nightRGBValue.r)
            {
                for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
                {
                    //만약 색을 바꿀 오브젝트가 존재한다면
                    if(spriteRen_ObumbrateObj[i] != null )
                    {
                        //색깔 변경
                        spriteRen_ObumbrateObj[i].color = new Color32((byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), 255);
                    }
                }

                //현재 오브젝트 RGB값 갱신
                curObjectRGB = new Color32((byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), 255);
            }
        }
    }


    //1초에 한번 실행하는 코루틴
    IEnumerator OneSecStartCoroutine()
    {
        isOneSecStart = true;
        yield return new WaitForSeconds(1f);

        Debug.Log("배경 밝기 조절");

        if(!timeStop)
        {
            //오브젝트 밝기 바꾸기
            ObumbrateObject(curObjectRGB);
        }

        isOneSecStart = false;
    }


    //주막 이미지 변경
    public void ChageJoomack(Sprite _sprite)
    {
        spriteRen_Joomack.sprite = _sprite;
    }

    //배경 컬러 초기값으로 변경
    public void ResetBGColor()
    {
        for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
        {
            //만약 색을 바꿀 오브젝트가 존재한다면
            if (spriteRen_ObumbrateObj[i] != null)
            {
                //색깔 변경 (시작 값)
                spriteRen_ObumbrateObj[i].color = startRGBValue;
            }
        }
    }

    //시간 진짜 정지
    public void RealTimeStop()
    {
        realTimeStop = true;
    }
    
}
