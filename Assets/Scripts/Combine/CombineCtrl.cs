using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineCtrl : MonoBehaviour
{
    //외부 스크립트 참조
    public UIManager uimanagerScr;

    //오브젝트에 접근했는지 확인하는 flag
    private bool isCloser;

    //Z키 오브젝트
    public GameObject gameObject_Zkey;

    private void Update()
    {
        //오브젝트와 충돌중이고 Z키를 눌렀을경우
        if (isCloser && Input.GetKeyDown(KeyCode.Z) &&
            (uimanagerScr.tutorialManagerScr.events == TutorialEvents.Done || uimanagerScr.tutorialManagerScr.events == TutorialEvents.PassOneDay))
        {
            //조합창 실행
            uimanagerScr.CombineWindowLaunch();
        }
    }

    //충돌했을 경우
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //만약 충돌 Collsion의 Tag가 Player라면
        if (collision.CompareTag("Player"))
        {
            isCloser = true;

            //향리댁과 대화를 마친 이후라면
            if (TutorialManager.instance.tutorialEventNum >= 3)
            {
                //Z키 보이기
                gameObject_Zkey.SetActive(true);
            }
        }
    }
    //충돌중일 경우
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCloser = true;
        }
    }

    //충돌 범위를 벗어났을 경우
    private void OnTriggerExit2D(Collider2D collision)
    {
        isCloser = false;

        //Z키 끄기
        gameObject_Zkey.SetActive(false);
       
    }
}
