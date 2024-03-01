using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.UI;

public class Calendar_Controller : MonoBehaviour
{
    [SerializeField] private Sprite[] CalendarSprites;
    [SerializeField] private Image CalendarUI;
    [SerializeField] float animationSpeed;
    [SerializeField] GameObject BlindBackGround;
    [SerializeField] private int curCalendarIdex;

    private void Start()
    {
        RefreshCalendar();
        DataManager.instance.LoadEvent += RefreshCalendar;
        TimeManager.instance.NextDayEvent += StartAnimation;
    }

    //켈린더 새로고침
    private void RefreshCalendar()
    {
        int day = TimeManager.instance.int_DayCount;
        if (day <= 1)
        {
            --day;
            CalendarUI.sprite = CalendarSprites[day];
        }
        else
        {
            day = (--day * 5) -1;
            CalendarUI.sprite = CalendarSprites[day];
        }
        curCalendarIdex = day;
    }

    //캘린더 애니메이션 실행
    private void StartAnimation()
    {
        StartCoroutine(NextCalendarAnimation(animationSpeed));
    }
    
    //캘린더 애니메이션
    private IEnumerator NextCalendarAnimation(float _animationtime)
    {
        WaitForSeconds animationDealyTime = new WaitForSeconds(_animationtime);
        int targetCalendarId = (curCalendarIdex <= 1) ? curCalendarIdex + 5 : curCalendarIdex + 4;
        BlindBackGround.SetActive(true);

        while (curCalendarIdex <= targetCalendarId)
        {
            CalendarUI.sprite = CalendarSprites[curCalendarIdex];
            yield return animationDealyTime;
            curCalendarIdex++;
        }

        yield return new WaitForSeconds(0.5f);
        BlindBackGround.SetActive(false);
        RefreshCalendar();
    }
}
