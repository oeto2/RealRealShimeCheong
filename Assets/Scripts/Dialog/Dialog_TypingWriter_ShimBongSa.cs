using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


//Tutorial Sentence를 정리해둔 Class
[System.Serializable]
public class TutorialSentence
{
    public TutorialSentence(string _sentence)
    {
        sentence = _sentence;
    }

    public string sentence;
}

public class Dialog_TypingWriter_ShimBongSa : MonoBehaviour
{
    // 실제 채팅이 나오는 텍스트
    public Text ChatText;

    // 캐릭터 이름이 나오는 텍스트
    public Text CharacterName;

    // 대화를 빠르게 넘길 수 있는 키(default : space)
    public List<KeyCode> skipButton;

    public string writerText = "";

    bool isButtonClicked = false;

    public bool bool_isBbang = false;

    //Dialogue UI Objcet
    public GameObject gameObject_Dialougue;

    //튜토리얼 대화 리스트
    public List<TutorialSentence> tutorial_SentenceList;

    //대화 나레이터 이름
    [Tooltip("String : 심봉사")]
    private string narator_Name = "심봉사";

    //대화 끝났는지 확인하는 falg
    [Tooltip("대화시작 : False, 대화 끝 : true")]
    public bool isTalkEnd;

    private void Start()
    {
        //튜토리얼 대화 모음
        {
            tutorial_SentenceList.Add(new TutorialSentence("심청이를 안본지 이틀이 지났다, 간만의 외출이니 나갈 채비를 해보자."));
            tutorial_SentenceList.Add(new TutorialSentence(" Z를 누르면 주변의 물건과 상호작용할 수 있다."));
            tutorial_SentenceList.Add(new TutorialSentence("봇짐을 챙겼다. X를 눌러 봇짐을 열어볼 수 있다."));
            tutorial_SentenceList.Add(new TutorialSentence("늘 나의 눈이 되어주는 지도도 챙겨야 한다."));
            tutorial_SentenceList.Add(new TutorialSentence("채비가 끝났으니 심청이를 만나러 향리 댁으로 가자."));
        }
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
        //StopCoroutine(TextPractice());
    }
  

    //대화 실행
    IEnumerator StartChat(string narrator, string narration, bool _clear)
    {
        Debug.Log("다이얼로그 실행");

        isTalkEnd = false;

        int a = 0;

        //UI상의 캐릭터 이름 
        CharacterName.text = narrator;

        //True : Text창 비우고 대화 시작, False : 이전 대화 이어서 대화 시작
        if(_clear)
        {
            writerText = "";
        }

        //텍스트 타이핑(받은 string의 길이만큼)
        for (a = 0; a < narration.Length; a++)
        //for (a = 0; a < textSpeed; a++)
        {
            //쓸내용에 1글자씩 더하기
            writerText += narration[a];
            ChatText.text = writerText;

            //텍스트 타이핑 시간 조절
            //yield return null;
            yield return new WaitForSeconds(0.05f);
        }

        isTalkEnd = true;
        //yield return null;

        ////키(default : space)를 다시 누를 때까지 무한정 대기
        //while (true)
        //{
        //    if (isButtonClicked)
        //    {
        //        isButtonClicked = false;
        //        break;
        //    }
        //    yield return null;
        //}
    }

    //IEnumerator TextPractice()
    //{
    //    //yield return StartCoroutine(NormalChat("뺑덕어멈", "호호, 무슨 일이신가요?"));
    //    //yield return StartCoroutine(NormalChat("뺑덕어멈", "이번에 들여온 비녀가 그렇게 예쁘던데,,,"));
    //}

    //1번 대화 실행 (Tutorial Manager에서 관리)
    public void Start_Sentence1()
    {
        Debug.Log("다이얼로그 대화1");

        //대화 실행시 다이얼로그창 띄우기
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[0].sentence, true));
    }

    public void Start_Sentence1_2()
    {
        Debug.Log("다이얼로그 대화1_2");

        if(isTalkEnd)
        {
            //대화 실행시 다이얼로그창 띄우기
            gameObject_Dialougue.SetActive(true);

            StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[1].sentence, false));
        }
        
    }
}