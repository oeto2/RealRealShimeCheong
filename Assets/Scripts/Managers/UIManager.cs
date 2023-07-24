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
    //조합창 오브젝트
    public GameObject gameObject_CombineWindow;

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

    private void Awake()
    {
        //마우스 포인터 끄기
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        isMonuseOn = Cursor.visible;

        //마우스 포인터를 켜는 조건
        if (gameObject_CombineWindow.activeSelf || gameObject_ItemWindow.activeSelf || gameObject_Option.activeSelf || gameObject_MapWindow.activeSelf)
        {
            //마우스 포인터 켜기
            Cursor.visible = true;
        }

        //마우스 포인터를 끄는 조건
        if(!gameObject_CombineWindow.activeSelf && !gameObject_ItemWindow.activeSelf && !gameObject_Option.activeSelf && !gameObject_MapWindow.activeSelf)
        {
            //마우스 포인터 끄기
            Cursor.visible = false;
        }

        //아이템 창 관련 코드
        #region
        //아이템 창 비활성화 상태에서 X키를 누를 경우
        if (Input.GetKeyDown(KeyCode.X) && !gameObject_ItemWindow.activeSelf && !isMapWindowLaunch && !isOptionLaunch && !isCombineLaunch)
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
        if (Input.GetKeyDown(KeyCode.M) && !gameObject_MapWindow.activeSelf && !isItemWindowLaunch && !isOptionLaunch && !isCombineLaunch)
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
        if (!isOptionLaunch && !isItemWindowLaunch && !isMapWindowLaunch && !isCombineLaunch && Input.GetKeyDown(KeyCode.Escape))
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

        //조합창 관련 코드
        #region
        //조합창이 실행중이라면
        if(gameObject_CombineWindow.activeSelf)
        {
            isCombineLaunch = true;
        }

        //조합창이 실행중이지 않다면
        if(!gameObject_CombineWindow.activeSelf)
        {
            Invoke("CombineFalgFalse", 0.2f);
        }


        //조합창이 실행중이고 ESC키를 눌렀을경우
        if (isCombineLaunch && Input.GetKeyDown(KeyCode.Escape))
        {
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
        gameObject_Option.SetActive(false);
    }

    //조합창 켜기
    public void CombineWindowLaunch()
    {
        if(!isOptionLaunch && !isItemWindowLaunch && !isMapWindowLaunch)
        {
            gameObject_CombineWindow.SetActive(true);
        }
    }

    //조합창 끄기
    public void CombineWindowExit()
    {
        gameObject_CombineWindow.SetActive(false);
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

    #endregion
}
