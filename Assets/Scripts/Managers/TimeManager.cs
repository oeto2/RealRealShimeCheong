using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public TutorialManager tutorialManagerScr;

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
    [SerializeField]
    private int int_DayCount = 1;

    //시간 멈추기
    private bool timeStop;

    private void FixedUpdate()
    {
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
}
