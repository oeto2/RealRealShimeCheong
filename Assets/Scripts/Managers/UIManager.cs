using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//저장할 UI 데이터들
[System.Serializable]
public class SaveUiData
{
    //생성자
    public SaveUiData(string _placeName, int _day, string _playTimeText, float _playTimeSec, int _sunClockNum)
    {
        placeName = _placeName;
        day = _day;
        playTimeText = _playTimeText;
        playTimeSec = _playTimeSec;
        sunClockNum = _sunClockNum;
    }

    //장소 이름
    public string placeName;

    //날짜
    public int day;

    //플레이 타임 텍스트
    public string playTimeText;

    //플레이 타임 시간
    public float playTimeSec;

    //해시계 이미지 번호
    public int sunClockNum;
}

//불러올 UI 데이터들
public class LoadUiData
{
    //생성자
    public LoadUiData(string _placeName, int _day, string _playTimeText, float _playTimeSec, int _sunClockNum)
    {
        placeName = _placeName;
        day = _day;
        playTimeText = _playTimeText;
        playTimeSec = _playTimeSec;
        sunClockNum = _sunClockNum;
    }

    //장소 이름
    public string placeName;

    //날짜
    public int day;

    //플레이 타임 텍스트
    public string playTimeText;

    //플레이 타임 시간
    public float playTimeSec;

    //해시계 이미지 번호
    public int sunClockNum;
}

public class UIManager : MonoBehaviour
{
    //외부 스크립트
    public CameraMove cameraMoveScr;
    public GameManager gameManagerScr;
    public TimeManager timeManagerScr;
    public Controller playerCtrlScr;
    public EffectSoundManager effectSoundManagerScr;
    public PinAction pinActionScr;
    public CursorCtrl cursorCtrlScr;
    public ObjectControll objectCtrlScr;

    //아이템창 인터페이스 오브젝트
    public GameObject gameObject_ItemWindow;
    //지도 오브젝트
    public GameObject gameObject_MapWindow;
    //옵션창 오브젝트
    public GameObject gameObject_Option;
    //조합창 오브젝트
    public GameObject gameObject_CombineWindow;
    //세이브창 오브젝트
    public GameObject gameObject_SaveWindow;
    //로드창 오브젝트
    public GameObject gameObject_LoadWindow;
    //세이브 체크 오브젝트
    public GameObject gameObject_SaveCheckWindow;

    //아이템 창이 실행중인지 확인하는 flag
    public bool isItemWindowLaunch;
    //지도가 실행중인지 확인 하는 flag
    public bool isMapWindowLaunch;
    //옵션창이 실행중인지 확인하는 flag
    public bool isOptionLaunch;
    //조합창이 실행중인지 확인하는 flag
    public bool isCombineLaunch;
    //마우스가 켜졌는지 확인하는 falg
    public bool isMonuseOn;

    //탭 버튼의 원래 색깔
    private Color originColor = new Color32(255, 255, 255, 255);

    //탭 버튼의 비활성화 색깔
    private Color falseColor = new Color32(170, 170, 170, 255);

    //Itme Tap Button Image
    public Image itemTapImage;
    public Image itemTapImage2;


    //Clue Tap Button Image
    public Image clueTapImage;
    public Image clueTapImage2;


    //Save Slot Place Name text
    public Text[] text_SavePlaceName;
    //Load Slot Place Name Text
    public Text[] text_LoadPlaceName;

    //Save Slot DayCount text
    public Text[] text_SaveDayCount;
    //Load Slot DayCount Text
    public Text[] text_LoadDayCount;

    //Save Slot PlayTime text
    public Text[] text_SavePlayTime;
    //Load Slot PlayTime text
    public Text[] text_LoadPlayTime;

    //Save Slot SunClock Image
    public Image[] image_SaveSunClock;
    //Load Slot SunClock Image
    public Image[] image_LoadSunClock;

    //저장할 UI데이터 클래스
    public SaveUiData curSaveUIData;

    //저장 파일 위치
    private string saveFilePath;

    //슬롯의 총 갯수
    public int totalSlotNum;

    //로드할 데이터를 받아올 클래스
    public LoadUiData curLoadUiData;

    //플레이 타임값을 받아올 클래스
    public LoadUiData curLoadUiData2;

    //현재 해시계 이미지
    public Image image_CurSunClock;

    //현재 해시계 스프라이트
    public Sprite sprite_CurSundClock;

    //해시계 스프라이트 모든 이미지들
    public Sprite[] sprite_AllSunClock;

    //해시계 이미지 번호
    public int int_SunClockNum = 0;

    //Save Slot UI 캘린더 이미지
    public Image[] image_SaveUICalendar;

    //Load Slot UI 캘린더 이미지
    public Image[] image_LoadUICalendar;

    //캘린더 스프라이트 모음
    public Sprite[] sprite_AllCalendar;

    //커서 불빛 오브젝트
    public GameObject gameObjcet_CursorLights;

    private void Awake()
    {
        //저장 파일 위치
        saveFilePath = Application.persistentDataPath + "/UiDataText.txt";

        totalSlotNum = text_LoadPlaceName.Length;
    }

    private void Start()
    {
        //슬롯에 UI 데이터 보여주기
        ShowUiDataToSlot();
    }
    // Update is called once per frame
    void Update()
    {
        //아이템 창 관련 코드
        #region
        //아이템 창을 켜는 조건
        if (Input.GetKeyDown(KeyCode.X) && !gameObject_ItemWindow.activeSelf && !isMapWindowLaunch && !isOptionLaunch && 
            !isCombineLaunch && !playerCtrlScr.isTalk && objectCtrlScr.getBotzime)
        {
            //아이템 창 실행
            ItemWindowLaunch();

            //커서 이미지 변경
            cursorCtrlScr.ChangeCursor(cursorCtrlScr.sprite_idle);
        }

        //아이템 창 활성화 상태에서 X키 or ESC를 누를 경우
        else if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape)) && gameObject_ItemWindow.activeSelf)
        {
            //아이템 창 종료
            ItemWindowExit();
        }

        //아이템 창이 실행중일 경우
        if (gameObject_ItemWindow.activeSelf)
        {
            isItemWindowLaunch = true;
        }

        //아이템 창이 비활성화일 경우
        if (!gameObject_ItemWindow.activeSelf)
        {
            //isMapWindowLaunch = false
            Invoke("itemFalgFalse", 0.2f);
        }
        #endregion

        //맵 관련 코드
        #region
        //지도를 펼치는 조건
        if (Input.GetKeyDown(KeyCode.M) && !gameObject_MapWindow.activeSelf && !isItemWindowLaunch && !isOptionLaunch &&
            !isCombineLaunch && !playerCtrlScr.isTalk && objectCtrlScr.getMap)
        {
            //지도 오브젝트 활성화
            gameObject_MapWindow.SetActive(true);

            //지도의 Pin위치 값 변경
            pinActionScr.PinPosChange(gameManagerScr.int_PinPosNum);

            //효과음 출력
            effectSoundManagerScr.PlayOpenMapSound();

            //커서 이미지 변경
            cursorCtrlScr.ChangeCursor(cursorCtrlScr.sprite_idle);
        }

        //지도가 실행중인데 M키 or ESC키를 눌렀을경우
        else if ((Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape)) && gameObject_MapWindow.activeSelf)
        {
            //지도 오브젝트 비활성화
            gameObject_MapWindow.SetActive(false);
        }

        //지도가 실행중일 경우
        if (gameObject_MapWindow.activeSelf)
        {
            isMapWindowLaunch = true;
        }
        //지도가 실행중이 아닐경우
        if (!gameObject_MapWindow.activeSelf)
        {
            //isMapWindowLaunch = false
            Invoke("MapFalgFalse", 0.2f);
        }
        #endregion

        //옵션 관련 코드
        #region

        //옵션창을 띄우는 조건
        if (!isOptionLaunch && !isItemWindowLaunch && !isMapWindowLaunch && !isCombineLaunch && Input.GetKeyDown(KeyCode.Escape) && !playerCtrlScr.isTalk)
        {
            //옵션창 보여주기
            gameObject_Option.SetActive(true);

            //커서 이미지 변경
            cursorCtrlScr.ChangeCursor(cursorCtrlScr.sprite_idle);

            //플레이어 이동제한
            playerCtrlScr.PlayerMoveStop();
        }

        //옵션창이 실행중일때 ESC를 눌렀을 경우
        else if (isOptionLaunch && Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject_Option.SetActive(false);

            //플레이어 이동제한 해제
            playerCtrlScr.PlayerMoveStart();
        }

        //옵션창이 실행중일경우
        if (gameObject_Option.activeSelf)
        {
            isOptionLaunch = true;
        }

        //옵션창이 실행중이지 않을경우
        else if (!gameObject_Option.activeSelf)
        {
            isOptionLaunch = false;
        }

        //세이브창이 실행중일때 ESC
        if (gameObject_SaveWindow.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            //세이브창 끄기
            gameObject_SaveWindow.SetActive(false);
            //옵션창 켜기
            gameObject_Option.SetActive(true);
        }

        //로드창이 실행중일때 ESC
        if (gameObject_LoadWindow.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            //로드창 끄기
            gameObject_LoadWindow.SetActive(false);
            //옵션창 켜기
            gameObject_Option.SetActive(true);
        }
        #endregion

        //조합창 관련 코드
        #region
        //조합창을 띄우는 조건
        if (!gameObject_CombineWindow.activeSelf && !playerCtrlScr.isTalk && Input.GetKeyDown(KeyCode.Z))
        {
            //커서 이미지 변경
            cursorCtrlScr.ChangeCursor(cursorCtrlScr.sprite_idle);
        }

        ////조합창이 실행중이지 않다면
        //if (!gameObject_CombineWindow.activeSelf)
        //{
        //    Invoke("CombineFalgFalse", 0.2f);
        //}

        //조합창이 실행중이고 ESC키를 눌렀을경우
        if (isCombineLaunch && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("조합창 닫기");
            CombineWindowExit();
        }
        #endregion
    }

    //아이템,조합,단서창 껐다 켜기
    #region

    //아이템 창 실행
    public void ItemWindowLaunch()
    {
        //아이템 창 오브젝트 활성화
        gameObject_ItemWindow.SetActive(true);
    }

    //아이템 창 종료
    public void ItemWindowExit()
    {
        //아이템 창 오브젝트 비활성화
        gameObject_ItemWindow.SetActive(false);
    }

    //지도 켜기
    public void MapWindowLaunch()
    {
        gameObject_MapWindow.SetActive(true);
    }

    //지도 끄기
    public void MapWindowExit()
    {
        gameObject_MapWindow.SetActive(false);
    }

    //옵션창 끄기
    public void OptionExit()
    {
        //플레이어 이동제한 해제
        playerCtrlScr.PlayerMoveStart();

        gameObject_Option.SetActive(false);
    }

    //조합창 켜기
    public void CombineWindowLaunch()
    {
        if (!isOptionLaunch && !isItemWindowLaunch && !isMapWindowLaunch)
        {
            isCombineLaunch = true;

            //시간 정지
            TimeManager.instance.StopTime();
            gameObject_CombineWindow.SetActive(true);

            //플레이어 이동제한
            playerCtrlScr.PlayerMoveStop();
        }
    }

    //조합창 끄기
    public void CombineWindowExit()
    {
        isCombineLaunch = false;

        //시간 흐르기
        TimeManager.instance.ContinueTime();
        gameObject_CombineWindow.SetActive(false);

        //플레이어 이동제한 해제
        playerCtrlScr.PlayerMoveStart();
    }
    #endregion

    //Falg 딜레이
    #region
    //isItemWindowLaunch = false;
    private void itemFalgFalse()
    {
        isItemWindowLaunch = false;
    }

    //isMapWindowLaunch = false;
    private void MapFalgFalse()
    {
        isMapWindowLaunch = false;
    }

    //isCombineLaunch = false;
    private void CombineFalgFalse()
    {
        isCombineLaunch = false;
    }
    #endregion

    //오브젝트 색깔 변경
    #region
    //아이템 탭 버튼 색깔 변경
    public void ChangeItemTapColor()
    {
        itemTapImage.color = falseColor;
        clueTapImage.color = originColor;
    }

    //조합 아이템 탭 버튼 색깔 변경
    public void ChangeCombineItemTapColor()
    {
        itemTapImage2.color = falseColor;
        clueTapImage2.color = originColor;
    }

    //단서 탭 버튼 색깔 변경
    public void ChangeClueTapColor()
    {
        clueTapImage.color = falseColor;
        itemTapImage.color = originColor;
    }

    //조합 단서 탭 버튼 색깔 변경
    public void ChangeCombineClueTapColor()
    {
        clueTapImage2.color = falseColor;
        itemTapImage2.color = originColor;
    }
    #endregion

    //옵션창 구성
    #region

    //게임 종료 버튼
    public void ExitButton()
    {
        //종료
        Application.Quit();
    }

    //세이브창 띄우기
    public void ShowSaveWindow()
    {
        //세이브창 띄우기
        gameObject_SaveWindow.SetActive(true);
        //옵션창 끄기
        gameObject_Option.SetActive(false);
    }

    //로드창 띄우기
    public void ShowLoadWindow()
    {
        //로드창 띄우기
        gameObject_LoadWindow.SetActive(true);
        //옵션창 끄기
        gameObject_Option.SetActive(false);
    }

    //세이브창 끄기
    public void ExitSaveWindow()
    {
        //세이브창 끄기
        gameObject_SaveWindow.SetActive(false);
        //옵션창 띄우기
        gameObject_Option.SetActive(true);
    }

    //로드창 끄기
    public void ExitLoadWindow()
    {
        //로드창 끄기
        gameObject_LoadWindow.SetActive(false);
        //옵션창 띄우기
        gameObject_Option.SetActive(true);
    }

    #endregion


    //UI 데이터 저장하기
    public void Save(int _slotNum)
    {
        //Debug.Log("Save UIManagerData");

        //현재 해시계 sprite구하기
        GetCurSunClockSprite();

        //해시계 이미지 번호 구하기
        GetSunClockNum();

        //저장할 데이터 넣기
        curSaveUIData = new SaveUiData(gameManagerScr.GetPlaceName(),timeManagerScr.GetDay(),timeManagerScr.GetPlayTimeText(),timeManagerScr.GetPlayTimeSec(_slotNum), int_SunClockNum);

        //세이브 데이터
        string jSaveData = JsonUtility.ToJson(curSaveUIData);

        //데이터 파일 생성
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);

        //슬롯에 UI 데이터 보여주기
        ShowUiDataToSlot();

        //세이브 슬롯 캘린더 UI 변경
        ChangeSlotUICalendar(_slotNum, curSaveUIData.day - 1);

        //Debug.Log("TimeManager PlayeTime : " + timeManagerScr.GetPlayTimeText());
    }

    //슬롯에 UI 데이터 보여주기
    public void ShowUiDataToSlot()
    {
        //Debug.Log("ShowUiDataToSlot");
        if(text_LoadPlaceName != null)
        {
            for (int i = 0; i < text_SavePlaceName.Length; i++)
            {
                //만약 i번째 슬롯에 해당하는 SaveData jsonFile이 존재한다면
                if (File.Exists(saveFilePath + i.ToString()) == true)
                {
                    Debug.Log("슬롯 Ui데이터 갱신" + i.ToString());

                    //파일 읽어오기
                    string jLoadData = File.ReadAllText(saveFilePath + i.ToString());

                    //curLoadUiData에 역직렬화
                    curLoadUiData = JsonUtility.FromJson<LoadUiData>(jLoadData);

                    //저장슬롯의 장소 UI Text 변경
                    text_SavePlaceName[i].text = curLoadUiData.placeName;
                    //로드슬롯의 장소 UI Text 변경
                    text_LoadPlaceName[i].text = curLoadUiData.placeName;

                    ////저장슬롯의 날짜 UI Text 변경
                    //text_SaveDayCount[i].text = curLoadUiData.day;
                    ////로드슬롯의 날짜 UI Text 변경
                    //text_LoadDayCount[i].text = curLoadUiData.day;

                    //저장슬롯의 플레이타임 UI Text 변경
                    text_SavePlayTime[i].text = curLoadUiData.playTimeText;
                    //로드슬롯의 날짜 UI Text 변경
                    text_LoadPlayTime[i].text = curLoadUiData.playTimeText;

                    //저장슬롯의 해시계 UI image 변경
                    image_SaveSunClock[i].sprite = sprite_AllSunClock[curLoadUiData.sunClockNum];
                    //로드슬롯의 해시계 Ui image 변경
                    image_LoadSunClock[i].sprite = sprite_AllSunClock[curLoadUiData.sunClockNum];

                    //저장슬롯의 캘린더 UI image 변경
                    image_SaveUICalendar[i].sprite = sprite_AllCalendar[curLoadUiData.day - 1];
                    //로드슬롯의 캘린더 UI image 변경
                    image_LoadUICalendar[i].sprite = sprite_AllCalendar[curLoadUiData.day - 1];


                    ////플레이타임 변경
                    //timeManagerScr.SetPlayTimeSec(curLoadUiData.playTimeSec);
                }
            }
        }
    }

    //UI 데이터 불러오기(플레이임, 해시계 UI)
    public void Load(int _slotNum)
    {
        //파일 읽어오기
        string jLoadData = File.ReadAllText(saveFilePath + _slotNum.ToString());

        //curLoadUiData에 역직렬화
        curLoadUiData2 = JsonUtility.FromJson<LoadUiData>(jLoadData);

        //해당하는 슬롯에 플레이 타임값을 받아옴
        timeManagerScr.SetPlayTimeSec(curLoadUiData2.playTimeSec);

        //해시계 UI 이미지 변경
        image_CurSunClock.sprite = sprite_AllSunClock[curLoadUiData2.sunClockNum];
        Debug.Log($"해시계 이미지변경 : {curLoadUiData2.sunClockNum}");
    }

    //현재 해시계 이미지 구하기
    public void GetCurSunClockSprite()
    {
        //현재 해시계 스프라이트 이미지 갱신
        sprite_CurSundClock = image_CurSunClock.sprite;
    }

    //해시계 이미지 번호 구하기
    public void GetSunClockNum()
    {
        for (int i = 0; i < sprite_AllSunClock.Length; i++)
        {
            //현재 해시계 이미지랑 같은 이미지의 인덱스 넘 구하기
            if (sprite_AllSunClock[i] == sprite_CurSundClock)
            {
                int_SunClockNum = i;
            }
        }
    }

    //해시계 이미지 변경 (TimeManager에서 관리)
    public void ChangeSunClockImage(int _sunClockNum)
    {
        //해시계 스프라이트 이미지 변경
        image_CurSunClock.sprite = sprite_AllSunClock[_sunClockNum];
    }

    //Slot UI 캘린더 스프라이트 이미지 변경
    public void ChangeSlotUICalendar(int _slotNum, int _day)
    {
        image_SaveUICalendar[_slotNum].sprite = sprite_AllCalendar[_day];
        image_LoadUICalendar[_slotNum].sprite = sprite_AllCalendar[_day];
    }


    //UI가 실행중인지 확인할수있는 메서드
    public bool GetUiVisible()
    {
        if(gameObject_ItemWindow.activeSelf || gameObject_CombineWindow.activeSelf || gameObject_LoadWindow.activeSelf || gameObject_MapWindow.activeSelf ||
            gameObject_Option.activeSelf || gameObject_SaveWindow.activeSelf || gameManagerScr.isJoomackPuzzleStart)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }

    //커서 이미지 보여주기
    public void ShowCursor()
    {
        cursorCtrlScr.ChangeCursor(cursorCtrlScr.sprite_idle);
    }

    //커서 끄기
    public void BlindCursor()
    {
        cursorCtrlScr.ChangeCursor(cursorCtrlScr.sprite_None);
    }

    //커서 불빛 켜기
    public void ShowCursorLight()
    {
        gameObjcet_CursorLights.SetActive(true);
    }

    //커서 불빛 끄기
    public void BlindCursorLight()
    {
        gameObjcet_CursorLights.SetActive(false);
    }
}
