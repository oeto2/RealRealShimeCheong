using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    //싱글톤 패턴
    public static TitleManager instance = null;

    //Load 창 오브젝트
    public GameObject gameObject_LoadWindow;

    //Load 확인창 오브젝트
    public GameObject gameObject_LoadCheckWindow;

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

    //Scene을 불러와주는 메서드(Start Button UI로 실행)
    public void LoadMainScene()
    {
        //TextScnene 불러오기
        SceneManager.LoadScene("TestScene");
    }

    //Load 확인 창 띄우기
    public void ShowLoadCheckWIndow()
    {
        gameObject_LoadCheckWindow.SetActive(true);
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
    }
    
}
