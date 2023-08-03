using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    //외부 스크립트 참조
    public UIManager uiManagerScr;
    public TimeManager timeManagerScr;
    public GameManager gameManagerScr;
    public ObjectManager objectManagerScr;

    //로딩 시간
    public int int_LodingTime;

    //로딩 이미지 오브젝트
    public GameObject gameObject_Loading;

    //세이브 체크 오브젝트
    public GameObject gameObject_SaveCheckWindow;

    //로드 체크 오브젝트
    public GameObject gameObjcet_LoadCheckWindow;

    //세이브 슬롯 번호
    private int int_SaveSlotNum;

    //로드 슬롯 번호
    private int int_LoadSlotNum;

    //세이브 Yes Button
    public void SaveYesButton()
    {
        Debug.Log("Save");

        //TimeManager Data 저장
        timeManagerScr.Save(int_SaveSlotNum);

        //GameManager Data 저장
        gameManagerScr.Save(int_SaveSlotNum);

        //ObjectManager Data 저장
        objectManagerScr.Save(int_SaveSlotNum);

        //세이브 확인창 끄기
        gameObject_SaveCheckWindow.SetActive(false);
    }

    //세이브 No Button
    public void SaveNoButton()
    {
        gameObject_SaveCheckWindow.SetActive(false);
    }
    
    //로드 Yes버튼
    public void LoadYesButton()
    {
        Debug.Log("Load");

        //로딩
        StartCoroutine(Loading());

        //TimeManagerData Load
        timeManagerScr.Load(int_LoadSlotNum);

        //GameManager Data Load
        gameManagerScr.Load(int_LoadSlotNum);

        //ObjectManager Data Load
        objectManagerScr.Load(int_LoadSlotNum);
    }

    //로드 No버튼
    public void LoadNoButton()
    {
        gameObjcet_LoadCheckWindow.SetActive(false);
    }

    //로딩 중
    IEnumerator Loading()
    {
        //로딩중 이미지 띄우기
        gameObject_Loading.SetActive(true);

        //옵션, 로드창 종료
        gameObjcet_LoadCheckWindow.SetActive(false);
        uiManagerScr.ExitLoadWindow();
        uiManagerScr.OptionExit();

        yield return new WaitForSeconds(int_LodingTime);

        gameObject_Loading.SetActive(false);
        
    }

    //세이브 슬롯 클릭
    public void SaveSlotClick(int _slotNum)
    {
        //세이브 슬롯 번호 저장
        int_SaveSlotNum = _slotNum;
        //세이브 확인 창 띄우기
        gameObject_SaveCheckWindow.SetActive(true);
    }

    //로드 슬롯 클릭
    public void LoadSlotClick(int _slotNum)
    {
        //로드 슬롯 번호 저장
        int_LoadSlotNum = _slotNum;
        //로드 확인 창 띄우기
        gameObjcet_LoadCheckWindow.SetActive(true);
    }
}
