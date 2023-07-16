using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    //시간을 표시할 텍스트
    public Text text_TimeText;

    //실제 시간
    private float RealTimeSec;

    //시간 배속
    [SerializeField]
    private int timeMultipleCation;

    //분
    private int minute = 0;

    //일
    private int day = 0;

    //하루를 몇분으로 할건지
    [SerializeField]
    private int dayMinute = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RealTimeSec += Time.deltaTime * timeMultipleCation;
        text_TimeText.text = day + "일      " +  minute + "분 " + RealTimeSec.ToString("F0") + "초";

        //1분은 60초
        if(RealTimeSec >= 60)
        {
            minute++;
            RealTimeSec = 0;
        }

        //분이 설정한 dayminute 값보다 커졌거나 같을 경우
        if(minute >= dayMinute)
        {
            day++;
            minute = 0;
        }
    }
}
