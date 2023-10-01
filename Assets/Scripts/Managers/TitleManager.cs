using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //클릭한 슬롯 int
    public int int_ClickSlotNum;

    //상태 값
    public LoadSceneState loadSenceState = LoadSceneState.Nomal;

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
    }

    //Start Button Clik
    public void StartButton_Click()
    {
        int_ClickSlotNum = 4;

        loadSenceState = LoadSceneState.Nomal;

        LoadMainScene();
    }

    //Load Button Click
    public void LoadButton_Click()
    {
        //Load 창 띄우기
        gameObject_LoadWindow.SetActive(true);
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
        gameObject_LoadCheckWindow.SetActive(true);

        //클릭 슬롯 번호 초기화
        int_ClickSlotNum = _slotNum;
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
            case 1:
                loadSenceState = LoadSceneState.Slot1;
                break;

            case 2:
                loadSenceState = LoadSceneState.Slot2;
                break;

            case 3:
                loadSenceState = LoadSceneState.Slot3;
                break;

            default:
                loadSenceState = LoadSceneState.Nomal;
                break;
        }

        //MainScene 불러오기
        LoadMainScene();
    }
    
}
