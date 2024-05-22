using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using Unity.VisualScripting;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;

    //외부 스크립트 참조
    public UIManager uiManagerScr;
    public TimeManager timeManagerScr;
    public GameManager gameManagerScr;
    public ObjectManager objectManagerScr;
    public TutorialManager tutorialManagerScr;
    public EventManager eventManagerScr;

    //로딩 시간
    public int int_LodingTime;

    //로딩 이미지 오브젝트
    public GameObject gameObject_Loading;

    //세이브 체크 오브젝트
    public GameObject gameObject_SaveCheckWindow;

    //로드 체크 오브젝트
    public GameObject gameObjcet_LoadCheckWindow;

    //세이브 슬롯 번호
    [SerializeField] private int int_SaveSlotNum;

    //로드 슬롯 번호
    [SerializeField] private int int_LoadSlotNum;

    //TitleManager
    public TitleManager titleManagerScr;

    //시작 이벤트 오브젝트
    public GameObject gameObject_StartMessage;

    //이벤트들
    public event Action LoadEvent;
    public event Action SaveEvent;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //타이틀 화면으로 부터 데이터 받기
        if(GameObject.Find("TitleManager") != null)
        {
            //TitleManager 찾기
            titleManagerScr = GameObject.Find("TitleManager").GetComponent<TitleManager>();

            //TitleManager 상태에 따라 저장데이터 불러오기
            switch (titleManagerScr.loadSenceState)
            {
                case LoadSceneState.Slot1:

                    //슬롯 번호 바꾸기
                    int_LoadSlotNum = titleManagerScr.int_ClickSlotNum;

                    //해당 데이터 불러오기
                    LoadYesButton();

                    //시작 이벤트 화면 끄기
                    gameObject_StartMessage.SetActive(false);

                    //이동제한 해제
                    Controller.instance.TalkEnd();

                    break;

                case LoadSceneState.Slot2:

                    //슬롯 번호 바꾸기
                    int_LoadSlotNum = titleManagerScr.int_ClickSlotNum;

                    //해당 데이터 불러오기
                    LoadYesButton();

                    //시작 이벤트 화면 끄기
                    gameObject_StartMessage.SetActive(false);

                    //이동제한 해제
                    Controller.instance.TalkEnd();


                    break;

                case LoadSceneState.Slot3:

                    //슬롯 번호 바꾸기
                    int_LoadSlotNum = titleManagerScr.int_ClickSlotNum;

                    //해당 데이터 불러오기
                    LoadYesButton();

                    //시작 이벤트 화면 끄기
                    gameObject_StartMessage.SetActive(false);

                    //이동제한 해제
                    Controller.instance.TalkEnd();

                    break;

                default:
                    //플레이어 이동 제한
                    Controller.instance.TalkStart();
                    break;
            }
        }
    }

    //세이브 Yes Button
    public void SaveYesButton()
    {
        //TimeManager Data 저장
        timeManagerScr.Save(int_SaveSlotNum);

        //GameManager Data 저장
        gameManagerScr.Save(int_SaveSlotNum);

        //ObjectManager Data 저장
        objectManagerScr.Save(int_SaveSlotNum);

        //UiManager Data 저장
        uiManagerScr.Save(int_SaveSlotNum);

        //Tutorial Data 저장
        tutorialManagerScr.Save(int_SaveSlotNum);

        eventManagerScr.Save(int_SaveSlotNum);

        //세이브 확인창 끄기
        gameObject_SaveCheckWindow.SetActive(false);

        //세이브 이벤트 호출
        CallSaveEvent();
    }

    //세이브 No Button
    public void SaveNoButton()
    {
        gameObject_SaveCheckWindow.SetActive(false);
    }
    
    //로드 Yes버튼
    public void LoadYesButton()
    {
        //로딩
        StartCoroutine(Loading());

        //TimeManagerData Load
        timeManagerScr.Load(int_LoadSlotNum);

        //GameManager Data Load
        gameManagerScr.Load(int_LoadSlotNum);

        //ObjectManager Data Load
        objectManagerScr.Load(int_LoadSlotNum);

        //UiManager PlayTime Data Load
        uiManagerScr.Load(int_LoadSlotNum);

        //Tutorial Data Load
        tutorialManagerScr.Load(int_LoadSlotNum);

        //Event Data Load
        eventManagerScr.Load(int_LoadSlotNum);

        //로드 이벤트 호출
        CallLoadEvent();
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
        //만약 해당 슬롯의 SaveData jsonFile이 존재한다면
        if (File.Exists(UIManager.instance.saveFilePath + _slotNum) == true)
        {
            //로드 슬롯 번호 저장
            int_LoadSlotNum = _slotNum;
            //로드 확인 창 띄우기
            gameObjcet_LoadCheckWindow.SetActive(true);
        }    
    }
    public void CallSaveEvent()
    {
        SaveEvent?.Invoke();
    }

    public void CallLoadEvent()
    {
        LoadEvent?.Invoke();
    }
}
