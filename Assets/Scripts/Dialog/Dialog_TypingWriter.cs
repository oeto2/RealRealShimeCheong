using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter : MonoBehaviour
{
    // 실제 채팅이 나오는 텍스트
    public Text ChatText;

    // 캐릭터 이름이 나오는 텍스트
    public Text CharacterName;

    // 대화를 빠르게 넘길 수 있는 키(default : space)
    public List<KeyCode> skipButton;

    public string writerText = "";

    bool isButtonClicked = false;


    //최초 클릭
    void Start()
    {
        //StartCoroutine(TextPractice());
        TextPractice();
        StopCoroutine(TextPractice());
    }


    void Update()
    {

        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }

        StopCoroutine(TextPractice());

        /* if (Input.GetMouseButtonDown(0))
         {
             StartCoroutine(TextPractice());
         }*/

        /*if (Input.GetMouseButtonDown(1))
                {
                    StopCoroutine(TextPractice());
                }*/
    }


    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(TextPractice());
            StopCoroutine(TextPractice());
        }
    }

    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        CharacterName.text = narrator;
        writerText = "";

        //텍스트 타이핑
        for (a = 0; a < narration.Length; a++)
        //for (a = 0; a < textSpeed; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            //텍스트 타이핑 시간 조절
            //yield return null;
            yield return new WaitForSeconds(0.03f);
        }

        //키(default : space)를 다시 누를 때까지 무한정 대기
        while (true)
        {
            if (isButtonClicked)
            {
                isButtonClicked = false;
                break;
            }
            yield return null;
        }
    }

    IEnumerator TextPractice()
    {
        yield return StartCoroutine(NormalChat("나는봇짐", "?이것은 봇짐이다 다음 대화 전환은 space"));
        yield return StartCoroutine(NormalChat("나는봇짐", "?안녕하세요, 반갑습니다. 대화 전환 테스트입니다 이것은 테스트지? 그럼 테스트지 테스트야 테스트군 테스트"));
    }
}