using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    //캘린더의 애니메이터
    public Animator animator_Celender;

    //시간을 표시할 텍스트
    public Text text_TimeText;

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

    public float Timetiem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //실제 시간 = 시간 + 배속
        float_RealTime += Time.deltaTime * timeMultipleCation;
        text_TimeText.text = float_RealTime.ToString("F0");

        //만약 int_DayMinute 만큼 시간이 진행되었을경우
        if (int_DayMinute <= float_RealTime)
        {
            //날짜 증가
            int_DayCount++;

            //시간 초기화
            float_RealTime = 0;

            //캘린더 애니메이션 진행
            NextDayAnimaton(int_DayCount);
        }
    }

    //날짜가 지났을때 실행할 애니메이션
    public void NextDayAnimaton(int _day)
    {
        //캘린더 날짜 바꾸기
        animator_Celender.SetInteger("DayNum", _day);
    }
}
