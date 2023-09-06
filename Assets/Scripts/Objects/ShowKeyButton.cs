using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKeyButton : MonoBehaviour
{
    //외부 스크립트 참조
    public TutorialManager tutorialManagerScr;

    //보여줄 KeyButton
    public GameObject gameObject_Keybutton;

    //처음부터 보여줄건지 확인하는 flag
    public bool showFirst;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //처음부터 보여줄건지
        if (showFirst)
        {
            if (collision.CompareTag("Player"))
            {
                gameObject_Keybutton.SetActive(true);
            }
        }

        else
        {
            //튜토리얼 0번 이벤트 이후 보여주기
            if (collision.CompareTag("Player") && tutorialManagerScr.events != TutorialEvents.TurnOnLights)
            {
                gameObject_Keybutton.SetActive(true);

            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //처음부터 보여줄건지
        if (showFirst)
        {
            if (collision.CompareTag("Player"))
            {
                gameObject_Keybutton.SetActive(false);
            }
        }

        else
        {
            //튜토리얼 0번 이벤트 이후 보여주기
            if (collision.CompareTag("Player") && tutorialManagerScr.events != TutorialEvents.TurnOnLights)
            {
                gameObject_Keybutton.SetActive(false);

            }
        }
    }
}
