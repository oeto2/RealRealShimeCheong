using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoomackPuzzle : MonoBehaviour
{
    //주막 퍼즐 UI
    public GameObject gameObject_JoomackUI;

    //주막 퍼즐 정답 텍스트 1번
    public Text text_Answer1;

    //주막 퍼즐 정답 텍스트 2번
    public Text text_Answer2;

    //정답 1번의 값
    public int int_Answer1Value = 1;

    //정답 2번의 값
    public int int_Answer2Value = 1;

    //퍼즐을 클리어 했는지
    public bool isClear;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isJoomackPuzzleStart)
        {
        }
    }

    //정답 1번 버튼 클릭 시
    public void Answer1Button_Click()
    {
        if(int_Answer1Value <9)
        {
            int_Answer1Value++;
        }
        else
        {
            int_Answer1Value = 1;
        }

        switch (int_Answer1Value)
        {
            case 1:
                text_Answer1.text = "한";
                break;
            case 2:
                text_Answer1.text = "두";
                break;
            case 3:
                text_Answer1.text = "삼";
                break;
            case 4:
                text_Answer1.text = "넉";
                break;
            case 5:
                text_Answer1.text = "오";
                break;
            case 6:
                text_Answer1.text = "육";
                break;
            case 7:
                text_Answer1.text = "칠";
                break;
            case 8:
                text_Answer1.text = "팔";
                break;
            case 9:
                text_Answer1.text = "구";
                break;
        }
    }

    //정답 2번 버튼 클릭 시
    public void AnswerButton2_Click()
    {
        if(int_Answer2Value == 1)
        {
            int_Answer2Value = 0;
        }
        else
        {
            int_Answer2Value = 1;
        }

        switch(int_Answer2Value)
        {
            case 0:
                text_Answer2.text = " ";
                break;
            case 1:
                text_Answer2.text = "반";
                break;
        }
    }

    //확인 버튼 클릭 시
    public void EnterButton_Click()
    {
        //두필 반 입력시
        if(int_Answer1Value == 2 && int_Answer2Value == 1)
        {
            Debug.Log("퍼즐 클리어");
            isClear = true;
            GameManager.instance.JoomackPuzzleClear();
        }
    }

    //주막 UI 보여주기
    public void ShowJoomackUI()
    {
        gameObject_JoomackUI.SetActive(true);
    }

    //주막 UI 끄기
    public void JoomackUIClose()
    {
        gameObject_JoomackUI.SetActive(false);
    }

    //새끼줄 UI 보여주기
    public void ShowStrawUI()
    {
        gameObject_JoomackUI.SetActive(true);
    }

}
