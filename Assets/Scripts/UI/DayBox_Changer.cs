using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayBox_Changer : MonoBehaviour
{
    //날짜 박스 캘린더 이미지
    [SerializeField] private Sprite[] DayBoxes;

    private Image DayBoxUI;

    private void Awake()
    {
        DayBoxUI = GetComponent<Image>();
        TimeManager.instance.NextDayEvent += RefreshDayBoxes_Image;
        DataManager.instance.LoadEvent += RefreshDayBoxes_Image;
    }

    //날짜 새로 고침 
    public void RefreshDayBoxes_Image()
    {
        int time = TimeManager.instance.int_DayCount - 1;
        
        if (time < 16)
        {
            DayBoxUI.sprite = DayBoxes[time];
        }
    }
}
