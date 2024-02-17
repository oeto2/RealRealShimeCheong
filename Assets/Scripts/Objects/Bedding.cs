using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedding : MonoBehaviour
{
    //외부 스크립트 참조
    public TimeManager timeManagerScr;
    public TutorialManager tutorialManagerScr;
    public UIManager uiManagerScr;
    public Controller playerCtrlScr;

    //Sleep Ui 오브젝트
    public GameObject gameObjcet_SleepUI;

    //Sleep BackGround Object
    public GameObject gameObject_SleepBG;

    //Z키 오브젝트
    public GameObject gameObject_Zkey;

    //플레이어가 오브젝트와 닿았는지 확인하는 flag
    public bool isTouch;
    
    // Update is called once per frame
    void Update()
    {
        //만약에 플레이어가 오브젝트와 접촉후 z키를 눌렀을 경우 (이벤트를 전부 다 봤을 경우에만 동작)
        if(isTouch && Input.GetKeyDown(KeyCode.Z) && tutorialManagerScr.events == TutorialEvents.Done && !DialogManager.instance.Dialouge_Canvas.activeSelf)
        {
            //Sleep UI 보여주기
            gameObjcet_SleepUI.SetActive(true);

            //플레이어 이동제한
            playerCtrlScr.PlayerMoveStop();
        }

        //창이 활성화된 상태에서 ESC키를 눌렀을 경우
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameObjcet_SleepUI.activeSelf)
            {
                //Sleep UI 끄기
                gameObjcet_SleepUI.SetActive(false);

                //옵션창 UI 끄기
                uiManagerScr.gameObject_Option.SetActive(false);

                //플레이어 이동제한 해제
                playerCtrlScr.PlayerMoveStart();
            }
        }
    }

    //오브젝트와 접촉
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject Obj = collision.gameObject;

        //충돌한 오브젝트가 Player일 경우
        if (Obj.CompareTag("Player"))
        {
            isTouch = true;

            //하루가 지난 뒤 라면
            if(tutorialManagerScr.tutorialEventNum >= 4)
            {
                //Z키 보이기
                gameObject_Zkey.SetActive(true);
            }
        }
    }

    //오브젝트와 접촉중
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject Obj = collision.gameObject;

        //충돌한 오브젝트가 Player일 경우
        if (Obj.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    //오브젝트의 충돌범위에서 벗어남
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;

        //Z키 비활성화
        gameObject_Zkey.SetActive(false);
    }

    //예 버튼을 눌렀을 경우
    public void YesButton()
    {
        //하루 지나기
        timeManagerScr.PassOneDay();
        //Sleep UI 끄기
        gameObjcet_SleepUI.SetActive(false);
        //Sleep BackRound Ani Start
        ShowSleepBG();
        //1초뒤에 오브젝트 비활성화
        Invoke("CloseSleepBG", 2f);
    }

    //아니오 버튼을 눌렀을 경우
    public void NoButton()
    {
        //Sleep UI 끄기
        gameObjcet_SleepUI.SetActive(false);
    }

    public void ShowSleepBG()
    {
        gameObject_SleepBG.SetActive(true);
    }

    public void CloseSleepBG()
    {
        gameObject_SleepBG.SetActive(false);
    }

    public void CloseButtonClick()
    {
        gameObjcet_SleepUI.SetActive(false);
    }
}
