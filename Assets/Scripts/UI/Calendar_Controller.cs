using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Calendar_Controller : MonoBehaviour
{
    [SerializeField] private Sprite[] CalendarSprites;
    [SerializeField] private Image CalendarUI;
    [SerializeField] float animationSpeed;
    [SerializeField] GameObject BlindBackGround;
    [SerializeField] private int curCalendarIndex;

    private void Start()
    {
        RefreshCalendar();
        DataManager.instance.LoadEvent += RefreshCalendar;
        TimeManager.instance.NextDayEvent += StartAnimation;
    }

    //�̸��� ���ΰ�ħ
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
        curCalendarIndex = day;
    }

    //Ķ���� �ִϸ��̼� ����
    private void StartAnimation()
    {
        StartCoroutine(NextCalendarAnimation(animationSpeed));
    }
    
    //Ķ���� �ִϸ��̼�
    private IEnumerator NextCalendarAnimation(float _animationtime)
    {
        WaitForSeconds animationDealyTime = new WaitForSeconds(_animationtime);
        int targetCalendarId = (curCalendarIndex <= 1) ? curCalendarIndex + 4 : curCalendarIndex + 5;
        BlindBackGround.SetActive(true);

        while (curCalendarIndex <= targetCalendarId)
        {
            CalendarUI.sprite = CalendarSprites[curCalendarIndex];
            yield return animationDealyTime;
            curCalendarIndex++;
        }

        yield return new WaitForSeconds(0.5f);
        BlindBackGround.SetActive(false);
        RefreshCalendar();
    }
}
