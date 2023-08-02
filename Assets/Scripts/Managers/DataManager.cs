using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //외부 스크립트 참조
    public TimeManager timeManagerScr;
    public GameManager gameManagerScr;

    //세이브 버튼
    public void SaveButton()
    {
        Debug.Log("Save");

        //TimeManager Data 저장
        timeManagerScr.Save();

        //GameManager Data 저장
        gameManagerScr.Save();
    }

    //로드 버튼
    public void LoadButton()
    {
        Debug.Log("Load");

        //TimeManagerData Load
        timeManagerScr.Load();

        //GameManager Data Load
        gameManagerScr.Load();
    }
}
