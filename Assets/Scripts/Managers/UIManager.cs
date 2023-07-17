using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    //아이템창 인터페이스 오브젝트
    public GameObject gameObject_ItemWindow;
    //지도 오브젝트
    public GameObject gameObject_MapWindow;
    //옵션창 오브젝트
    public GameObject gameObject_Option;

    //아이템 창이 실행중인지 확인하는 flag
    public bool isItemWindowLaunch;
    //지도가 실행중인지 확인 하는 flag
    public bool isMapWindowLaunch;
    //옵션창이 실행중인지 확인하는 flag
    public bool isOptionLaunch;

    //탭 버튼의 원래 색깔
    private Color originColor = new Color32(255, 255, 255, 255);

    //탭 버튼의 비활성화 색깔
    private Color falseColor = new Color32(170, 170, 170, 255);

    //Itme Tap Button Image
    public Image itemTapImage;
    
    //Clue Tap Button Image
    public Image clueTapImage;

    // Update is called once per frame
    void Update()
    {
        //아이템 창 관련 코드
        #region
        //아이템 창 비활성화 상태에서 X키를 누를 경우
        if (Input.GetKeyDown(KeyCode.X) && !gameObject_ItemWindow.activeSelf && !isMapWindowLaunch && !isOptionLaunch)
        {
            //아이템 창 실행
            ItemWindowLaunch();
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
        //지도가 실행중이 아닌데 M키를 눌렀을 경우
        if (Input.GetKeyDown(KeyCode.M) && !gameObject_MapWindow.activeSelf && !isItemWindowLaunch && !isOptionLaunch)
        {
            //지도 오브젝트 활성화
            gameObject_MapWindow.SetActive(true);
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

        //아이템창 ,지도 ,옵션창이 실행중이지 않을때 ESC키를 눌렀을 경우
        if (!isOptionLaunch && !isItemWindowLaunch && !isMapWindowLaunch && Input.GetKeyDown(KeyCode.Escape))
        {
            //옵션창 보여주기
            gameObject_Option.SetActive(true);
        }

        //옵션창이 실행중일때 ESC를 눌렀을 경우
        else if (isOptionLaunch && Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject_Option.SetActive(false);
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
        #endregion

    }

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
        gameObject_Option.SetActive(false);
    }

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

    //아이템 탭 버튼 색깔 변경
    public void ChangeItemTapColor()
    {
        itemTapImage.color = falseColor;
        clueTapImage.color = originColor;
    }

    //단서 탭 버튼 색깔 변경
    public void ChangeClueTapColor()
    {
        clueTapImage.color = falseColor;
        itemTapImage.color = originColor;
    }
}
