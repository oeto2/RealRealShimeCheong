using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

//LoadScene할때 넘겨줄 상태값
public enum LoadSceneState
{
    Slot1, //1번 슬롯
    Slot2, //2번 슬롯
    Slot3, //3번 슬롯
    Nomal, //처음부터
}

public class TitleManager : MonoBehaviour
{
    //싱글톤 패턴
    public static TitleManager instance = null;

    //Load 창 오브젝트
    public GameObject gameObject_LoadWindow;

    //Load 확인창 오브젝트
    public GameObject gameObject_LoadCheckWindow;

    //Load Slot Place Name Text
    public Text[] text_LoadPlaceName;

    //Load Slot DayCount Text
    public Text[] text_LoadDayCount;

    //Load Slot PlayTime text
    public Text[] text_LoadPlayTime;

    //Load Slot SunClock Image
    public Image[] image_LoadSunClock;

    //Load Slot UI 캘린더 이미지
    public Image[] image_LoadUICalendar;

    //캘린더 스프라이트 모음
    public Sprite[] sprite_AllCalendar;

    //로드할 데이터를 받아올 클래스
    public LoadUiData curLoadUiData;

    //해시계 스프라이트 모든 이미지들
    public Sprite[] sprite_AllSunClock;

    //클릭한 슬롯 int
    public int int_ClickSlotNum;

    //상태 값
    public LoadSceneState loadSenceState = LoadSceneState.Nomal;

    //저장 파일 위치
    public string saveFilePath;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        //저장 파일 위치
        saveFilePath = Application.persistentDataPath + "/UiDataText.txt";
    }

    //게임을 종료해주는 메서드
    public void ExitGame()
    {
        if (gameObject_LoadWindow.activeSelf == false)
        {
            //실행 종료
            Application.Quit();
        }
    }

    //Start Button Clik
    public void StartButton_Click()
    {
        if (gameObject_LoadWindow.activeSelf == false)
        {
            int_ClickSlotNum = 4;

            loadSenceState = LoadSceneState.Nomal;

            LoadMainScene();
        }
    }

    //Load Button Click
    public void LoadButton_Click()
    {
        //Load 창 띄우기
        gameObject_LoadWindow.SetActive(true);

        //Load창 UI 데이터 불러오기
        ShowUiDataToSlot();
    }

    //Scene을 불러와주는 메서드(Start Button UI로 실행)
    public void LoadMainScene()
    {
        //TextScnene 불러오기
        SceneManager.LoadScene("TestScene");
    }

    //Load 확인 창 띄우기
    public void ShowLoadCheckWIndow(int _slotNum)
    {
        //만약 i번째 슬롯에 해당하는 SaveData jsonFile이 존재한다면
        if (File.Exists(saveFilePath + _slotNum) == true)
        {
            //클릭 슬롯 번호 초기화
            int_ClickSlotNum = _slotNum;

            gameObject_LoadCheckWindow.SetActive(true);
        }    
    }

    //Load 창 끄기
    public void CloseLoadWindow()
    {
        gameObject_LoadWindow.SetActive(false);
    }

    //아니오 버튼 클릭
    public void NoButton_Click()
    {
        gameObject_LoadCheckWindow.SetActive(false);
    }

    //예 버튼 클릭
    public void YesButton_Click()
    {
        gameObject_LoadCheckWindow.SetActive(false);

        //Load State 변경
        switch(int_ClickSlotNum)
        {
            case 0:
                loadSenceState = LoadSceneState.Slot1;
                break;

            case 1:
                loadSenceState = LoadSceneState.Slot2;
                break;

            case 2:
                loadSenceState = LoadSceneState.Slot3;
                break;

            default:
                loadSenceState = LoadSceneState.Nomal;
                break;
        }

        //MainScene 불러오기
        LoadMainScene();
    }

    //슬롯에 UI 데이터 보여주기
    public void ShowUiDataToSlot()
    {
        //Debug.Log("ShowUiDataToSlot");
        if (text_LoadPlaceName != null)
        {
            for (int i = 0; i < text_LoadPlaceName.Length; i++)
            {
                //만약 i번째 슬롯에 해당하는 SaveData jsonFile이 존재한다면
                if (File.Exists(saveFilePath + i.ToString()) == true)
                {
                    Debug.Log("슬롯 Ui데이터 갱신" + i.ToString());

                    //파일 읽어오기
                    string jLoadData = File.ReadAllText(saveFilePath + i.ToString());

                    //curLoadUiData에 역직렬화
                    curLoadUiData = JsonUtility.FromJson<LoadUiData>(jLoadData);
                   
                    //로드슬롯의 장소 UI Text 변경
                    text_LoadPlaceName[i].text = curLoadUiData.placeName;

                    //로드슬롯의 날짜 UI Text 변경
                    text_LoadPlayTime[i].text = curLoadUiData.playTimeText;
                  
                    //로드슬롯의 해시계 Ui image 변경
                    image_LoadSunClock[i].sprite = sprite_AllSunClock[curLoadUiData.sunClockNum];
                 
                    //로드슬롯의 캘린더 UI image 변경
                    image_LoadUICalendar[i].sprite = sprite_AllCalendar[curLoadUiData.day - 1];

                }
            }
        }
    }
}
