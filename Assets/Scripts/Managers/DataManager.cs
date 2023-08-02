using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //외부 스크립트 참조
    public TimeManager timeManagerScr;
    public GameManager gameManagerScr;

    //세이브 버튼
    public void SaveButton(int _slotNum)
    {
        Debug.Log("Save");

        //TimeManager Data 저장
        timeManagerScr.Save(_slotNum);

        //GameManager Data 저장
        gameManagerScr.Save(_slotNum);
    }

    //로드 버튼
    public void LoadButton(int _slotNum)
    {
        Debug.Log("Load");

        //TimeManagerData Load
        timeManagerScr.Load(_slotNum);

        //GameManager Data Load
        gameManagerScr.Load(_slotNum);
    }
}
