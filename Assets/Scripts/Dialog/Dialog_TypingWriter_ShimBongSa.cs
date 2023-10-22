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
            tutorial_SentenceList.Add(new TutorialSentence("심청이를 안본지 이틀이 지났다, 간만의 외출이니 나갈 채비를 해보자."));//0
            tutorial_SentenceList.Add(new TutorialSentence(" Z를 누르면 주변의 물건과 상호작용할 수 있다."));
            tutorial_SentenceList.Add(new TutorialSentence("봇짐을 챙겼다. X를 눌러 봇짐을 열어볼 수 있다."));
            tutorial_SentenceList.Add(new TutorialSentence("늘 나의 눈이 되어주는 지도를 챙겼다."));
            tutorial_SentenceList.Add(new TutorialSentence("채비가 끝났으니 심청이를 만나러 향리 댁으로 가자.")); 
            tutorial_SentenceList.Add(new TutorialSentence("아이템이나 단서를 장착 후 Z를 누르면 그에 따른 상호작용이 일어납니다.")); //5
            tutorial_SentenceList.Add(new TutorialSentence("청이가 향리 댁에 오지 않았다고 한다."));
            tutorial_SentenceList.Add(new TutorialSentence(" 어찌 된 일인지 주변을 수소문 해 보자."));
            tutorial_SentenceList.Add(new TutorialSentence(" 게임에서의 하루는 실제 시간의 5분입니다. 하루가 지나면 심학규의 집으로 귀환 됩니다."));
            tutorial_SentenceList.Add(new TutorialSentence("사람들의 이야기를 들어보아도, 아무래도 청이는 누군가의 꾀임에 넘어갔음이 틀림없다."));
            tutorial_SentenceList.Add(new TutorialSentence(" 청이를 데려간 범인을 알아내야 한다.")); //10
            tutorial_SentenceList.Add(new TutorialSentence(" 이제까지 알아낸 정보를 조합해 새로운 단서를 만들 수 있다."));
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
    IEnumerator StartChat(string narrator, string _narration, bool _clear)
    {
        Debug.Log("다이얼로그 실행");

        isTalkEnd = false;

        //UI상의 캐릭터 이름 
        CharacterName.text = narrator;

        //텍스트 타이핑
        for (int a = 0; a < _narration.Length; a++)
        {
            writerText += _narration[a];
            ChatText.text = writerText;

            //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
            {
                ChatText.text = _narration;

                //for문 조건 충족
                a = _narration.Length;
            }

            //대사 출력 중일 경우에만
            if (ChatText.text != _narration)
            {
                //텍스트 타이핑 시간 조절
                yield return new WaitForSeconds(0.02f);
            }
        }

        //Z키를 다시 누를 때까지 무한정 대기
        while (true)
        {
            if (ChatText.text == _narration && Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("Text 비우기");

                //Text 비우기
                writerText = "";

                break;
            }
            yield return null;
        }
    }

    //튜토리얼 대화 출력 메서드 모음
    #region
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

        if (isTalkEnd)
        {
            //대화 실행시 다이얼로그창 띄우기
            gameObject_Dialougue.SetActive(true);

            StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[1].sentence, false));
        }
    }

    //봇짐 획득 대사
    public void Start_Sentence_GetBotzime()
    {
        Debug.Log("봇짐 획득 대화");

        //대화 실행시 다이얼로그창 띄우기
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[2].sentence, true));
    }

    //지도 획득 대사
    public void Start_Sentence_GetMap()
    {
        Debug.Log("지도 획득 대화");

        //대화 실행시 다이얼로그창 띄우기
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[3].sentence, true));
    }

    //지도, 봇짐 둘다 획득 대사
    public void Start_Sentence_GetObjcets()
    {
        Debug.Log("둘다 획득 대화");

        //대화 실행시 다이얼로그창 띄우기
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[4].sentence, true));
    }

    //뺑덕 어멈 대화 끝난뒤 대화
    public void Start_Sentence_BbangEnd()
    {
        Debug.Log("뺑덕 대화 끝");

        //대화 실행시 다이얼로그창 띄우기
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[5].sentence, true));
    }

    //향리댁 대화 끝난뒤 대화
    public void Start_Sentence_HyangEnd1()
    {
        Debug.Log("향리댁 대화 끝");

        //대화 실행시 다이얼로그창 띄우기
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[6].sentence, true));
    }

    //향리댁 대화 끝난뒤 대화2
    public void Start_Sentence_HyangEnd2()
    {
        Debug.Log("향리댁 대화 끝2");

        //대화 실행시 다이얼로그창 띄우기
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[7].sentence, false));
    }

    //향리댁 대화 끝난뒤 대화3
    public void Start_Sentence_HyangEnd3()
    {
        Debug.Log("향리댁 대화 끝3");

        //대화 실행시 다이얼로그창 띄우기
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[8].sentence, true));
    }

    //하루가 지난뒤 대화
    public void Start_Sentence_PassDay()
    {
        Debug.Log("하루 지난뒤 대화");

        //대화 실행시 다이얼로그창 띄우기
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[9].sentence, true));
    }

    //하루가 지난뒤 대화
    public void Start_Sentence_PassDay2()
    {
        Debug.Log("하루 지난뒤 대화2");

        //대화 실행시 다이얼로그창 띄우기
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[10].sentence, true));
    }

    //하루가 지난뒤 대화
    public void Start_Sentence_PassDay3()
    {
        Debug.Log("하루 지난뒤 대화3");

        //대화 실행시 다이얼로그창 띄우기
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[11].sentence, false));
    }
    #endregion
}