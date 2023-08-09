using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//저장할 튜토리얼 데이터
[System.Serializable]
public class TutorialSaveData
{
    //생성자
    public TutorialSaveData(int _tutoralEventNum, bool _getBotzime, bool _getMap, bool _isLightsOn)
    {
        tutoralEventNum = _tutoralEventNum;
        getBotzime = _getBotzime;
        getMap = _getMap;
        isLightsOn = _isLightsOn;
    }

    //튜토리얼 이벤트 번호
    public int tutoralEventNum;
    //봇짐 획득 여부
    public bool getBotzime;
    //지도 획득 여부
    public bool getMap;
    //불을 켰는지 안켰는지
    public bool isLightsOn;
}

//저장할 튜토리얼 데이터
[System.Serializable]
public class TutorialLoadData
{
    //생성자
    public TutorialLoadData(int _tutoralEventNum, bool _getBotzime, bool _getMap, bool _isLightsOn)
    {
        tutoralEventNum = _tutoralEventNum;
        getBotzime = _getBotzime;
        getMap = _getMap;
        isLightsOn = _isLightsOn;
    }

    //튜토리얼 이벤트 번호
    public int tutoralEventNum;
    //봇짐 획득 여부
    public bool getBotzime;
    //지도 획득 여부
    public bool getMap;
    //불을 켰는지 안켰는지
    public bool isLightsOn;
}

//Event
public enum Events
{
    TurnOnLights = 0,
    GetItems = 1,
    TalkToBBangDuck = 2,
    TalkToHyang = 3,
    PassOneDay = 4,
    Done

}

public class TutorialManager : MonoBehaviour
{
    //외부 스크립트
    public TurnOnLight turnOnLightScr;
    public ObjectManager objectManagerScr;
    public Controller playerCtrlScr;
    public Dialog_TypingWriter_ShimBongSa playerDialogueScr;
    public ObjectControll objCtrlScr;
    public TimeManager timeManagerScr;
    public GameManager gameManagerScr;

    //나레이션 오브젝트
    public GameObject gameObject_NarationBG;

    //UI Canvas
    public GameObject gameObject_UICanvas;

    //다이얼로그 UI
    public GameObject gameObject_Dialogue;

    //Input Key Text Object
    public GameObject gameObject_InputKeyText;

    //등잔 불 오브젝트
    public GameObject gameObject_DeungJanLight;

    //심청이의 메모
    public GameObject gameObject_shimeNote;

    //나레이션 BackGround Image
    public Image image_NarationBG;

    //나레이션 텍스트
    public Text text_Naration;

    //나레이션배경 애니메이터
    public Animator animator_NarationBG;

    //심청이 메모를 보여줬는지 : true, false
    private bool showNote;

    //심청이 메모를 껐는지
    [SerializeField]
    private bool closeNote;

    //첫번째 대화 끝
    public bool setence1End;

    //오브젝트 둘다 획득
    public bool getObjects;

    //하루가 지났는지 확인하는 falg
    public bool passDay;

    //뺑덕 대화 끝
    public bool SentenceEnd_Bbang;
    //향리댁 대화 끝
    public bool SentenceEnd_Hyang;
    //향리댁 대화 끝난뒤 심봉사 대화 끝
    private bool SentenceEnd_HyangShim;

    //흐름 제어용 Flags
    private bool BangtalkEnd;
    private bool HyangTalkEnd1;
    private bool HyangTalkEnd2;
    private bool HyangTalkEnd3;
    private bool PassDayTalkEnd1;
    private bool PassDayTalkEnd2;
    private bool PassDayTalkEnd3;

    //저장할 튜토리얼 데이터 클래스
    public TutorialSaveData curTutorialSaveData;
    //불러올 튜토리얼 데이터 클래스
    public TutorialLoadData curTutorialLoadData;

    private string saveFilePath;

    //튜토리얼 이벤트
    public Events events = Events.TurnOnLights;

    //튜토리얼 이벤트 번호
    public int tutorialEventNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        //세이브 데이터 파일 위치
        saveFilePath = Application.persistentDataPath + "/TutorialDataText.txt";
        //튜토리얼 시작 전 사전 작업들
        #region
        ////UI Canvas OFF
        //gameObject_UICanvas.SetActive(false);
        //Player 이동 제한
        playerCtrlScr.TalkStart();

        //시간 멈추기
        timeManagerScr.StopTime();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region 튜토리얼 Chaptor 1 End

        if (!setence1End)
        {
            //스페이스 바를 눌러 독백 창 끄기
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
            {

                //Player 이동 제한 해제
                playerCtrlScr.TalkEnd();

                //Input key Text OFF
                gameObject_InputKeyText.SetActive(false);

                //텍스트 창 비우기
                text_Naration.text = "";

                //서서히 나레이션 배경 FadeOut 시작
                animator_NarationBG.SetBool("FadeOutStart", true);

                //나레이션 배경 끄기
                Invoke("ActiveFalse_NarationBG", 1.5f);
            }

            //Evnet 0 : 불을 키자
            //불이 켜졌을경우
            if (turnOnLightScr.isTrunOnLight && !showNote && events == Events.TurnOnLights)
            {
                Debug.Log("실행");
                //1초뒤에 메모 등작
                Invoke("ShowShimNote", 1f);

                //Player 이동제한
                playerCtrlScr.TalkStart();

                showNote = true;

                
            }

            //노트를 읽고 난 뒤 Z or Space를 누른다
            if (gameObject_shimeNote.activeSelf)
            {
                //메모 끄기
                if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space)) && !closeNote)
                {
                    //Player 이동제한
                    playerCtrlScr.TalkStart();

                    //메모 끄기
                    gameObject_shimeNote.SetActive(false);

                    //1번 대화 실행
                    playerDialogueScr.Start_Sentence1();

                    Invoke("CloseNote", 0.2f);
                }
            }

            //이어서 대화 시작
            if (!setence1End && playerDialogueScr.isTalkEnd && closeNote && Input.GetKeyDown(KeyCode.Z))
            {
                //Player 이동제한
                playerCtrlScr.TalkStart();

                playerDialogueScr.Start_Sentence1_2();

                Invoke("Sentence1End", 0.2f);
            }
        }

        //2번째 대화가 끝나고 Z키 누르면 다이얼로그창 끄기
        if (setence1End && Input.GetKeyDown(KeyCode.Z) && !objCtrlScr.getBotzime && !objCtrlScr.getMap && playerDialogueScr.isTalkEnd)
        {
            //Player 이동제한 해제
            playerCtrlScr.TalkEnd();

            //UI 캔버스 보이기
            gameObject_UICanvas.SetActive(true);


            gameObject_Dialogue.SetActive(false);

            //다음 이벤트
            tutorialEventNum = 1;
            events = Events.GetItems;
        }
        #endregion

        #region 오브젝트 획득 튜토리얼

        if (setence1End && !getObjects)
        {
            //Evnet 1 : 봇짐 또는 지도를 획득하자
            //봇짐 획득 후 창끄기
            if (setence1End && Input.GetKeyDown(KeyCode.Z) && objCtrlScr.getBotzime && playerDialogueScr.isTalkEnd)
            {
                //Player 이동제한 해제
                playerCtrlScr.TalkEnd();

                gameObject_Dialogue.SetActive(false);
            }

            //맵 획득 후 창끄기
            if (setence1End && Input.GetKeyDown(KeyCode.Z) && objCtrlScr.getMap && playerDialogueScr.isTalkEnd)
            {
                //Player 이동제한 해제
                playerCtrlScr.TalkEnd();

                gameObject_Dialogue.SetActive(false);
            }

            if (!gameObject_Dialogue.activeSelf && objCtrlScr.getMap && objCtrlScr.getBotzime)
            {
                //둘다 획득 대화 실행
                playerDialogueScr.Start_Sentence_GetObjcets();

                //Player 이동제한
                playerCtrlScr.TalkStart();

                getObjects = true;
            }
        }
        #endregion

        if (setence1End && getObjects && Input.GetKeyDown(KeyCode.Z) && playerDialogueScr.isTalkEnd && events == Events.GetItems)
        {
            //Player 이동제한 해제
            playerCtrlScr.TalkEnd();

            gameObject_Dialogue.SetActive(false);

            //다음 이벤트
            tutorialEventNum = 2;
            events = Events.TalkToBBangDuck;
        }

        //Evnet 2 : 뺑떡 어멈과 대화하자
        //뺑떡 어멈의 말이 끝났을 경우
        if (setence1End && getObjects && SentenceEnd_Bbang && !BangtalkEnd)
        {
            Debug.Log("뺑떡이야기 후 대화 실행");

            playerCtrlScr.TalkStart();

            playerDialogueScr.Start_Sentence_BbangEnd();

            BangtalkEnd = true;
        }

        //문장이 전부 출력이 되었다면 Z키를 눌러 다이얼로그 끄기
        if (playerDialogueScr.isTalkEnd && Input.GetKeyDown(KeyCode.Z) && BangtalkEnd && !SentenceEnd_Hyang && events == Events.TalkToBBangDuck)
        {
            playerCtrlScr.TalkEnd();

            gameObject_Dialogue.SetActive(false);

            //다음 이벤트
            tutorialEventNum = 3;
            events = Events.TalkToHyang;
        }

        //Evnet 3 : 향리댁과 대화하자
        //향리댁 대화가 모두 끝났을 경우
        if (getObjects && SentenceEnd_Bbang && SentenceEnd_Hyang && !HyangTalkEnd1 && !HyangTalkEnd2)
        {
            playerCtrlScr.TalkStart();

            //향리댁 1번대화
            playerDialogueScr.Start_Sentence_HyangEnd1();

            HyangTalkEnd1 = true;

            
        }

        //1번대화가 모두 끝나고 Z키 누를경우
        if (SentenceEnd_Hyang && HyangTalkEnd1 && playerDialogueScr.isTalkEnd && Input.GetKeyDown(KeyCode.Z) && !HyangTalkEnd2)
        {
            //향리댁 2번 대화
            playerDialogueScr.Start_Sentence_HyangEnd2();

            HyangTalkEnd2 = true;
        }

        //2번대화가 모두 끝나고 Z키를 누를경우
        if (playerDialogueScr.isTalkEnd && HyangTalkEnd2 && Input.GetKeyDown(KeyCode.Z) && !HyangTalkEnd3)
        {
            //향리댁 3번 대화
            playerDialogueScr.Start_Sentence_HyangEnd3();

            HyangTalkEnd3 = true;
        }

        //3번대화가 모두 끝나고 Z 키를 누를경우
        if (playerDialogueScr.isTalkEnd && HyangTalkEnd1 && HyangTalkEnd3 && Input.GetKeyDown(KeyCode.Z) && !SentenceEnd_HyangShim && events == Events.TalkToHyang)
        {
            //대화 끝
            playerCtrlScr.TalkEnd();

            //다이얼로그 끄기
            gameObject_Dialogue.SetActive(false);

            //시간 리셋
            timeManagerScr.ResetTime();

            //날짜 UI 보여주기
            timeManagerScr.ShowDayUI();

            //시간 흐르기
            timeManagerScr.ContinueTime();

            SentenceEnd_HyangShim = true;

            //다음 이벤트
            tutorialEventNum = 4;
            events = Events.PassOneDay;
        }

        //Evnet 4 : 하루를 보내자
        //하루가 지났을 경우
        if (passDay && !PassDayTalkEnd1)
        {
            //대화 시작
            playerCtrlScr.TalkStart();

            //하루 지나고 대화 1
            playerDialogueScr.Start_Sentence_PassDay();

            PassDayTalkEnd1 = true;
        }

        if (playerDialogueScr.isTalkEnd && PassDayTalkEnd1 && !PassDayTalkEnd2 && Input.GetKeyDown(KeyCode.Z) && passDay)
        {
            //하루 지나고 대화 2
            playerDialogueScr.Start_Sentence_PassDay2();

            PassDayTalkEnd2 = true;
        }

        if (playerDialogueScr.isTalkEnd && PassDayTalkEnd2 && !PassDayTalkEnd3 && Input.GetKeyDown(KeyCode.Z) && passDay)
        {
            //하루 지나고 대화 3
            playerDialogueScr.Start_Sentence_PassDay3();

            PassDayTalkEnd3 = true;
        }

        if (playerDialogueScr.isTalkEnd && PassDayTalkEnd3 && Input.GetKeyDown(KeyCode.Z) && passDay && events == Events.PassOneDay)
        {
            Debug.Log("튜토리얼 끝");
            //대화 끝
            playerCtrlScr.TalkEnd();

            //다이얼로그 창 끄기
            gameObject_Dialogue.SetActive(false);

            //플래그 초기화
            passDay = false;

            //시간 흐르기
            timeManagerScr.ContinueTime();

            //다음 이벤트
            tutorialEventNum = 5;
            events = Events.Done;
        }
    }

    //하루가 지났는지(TimeManager에서 관리)
    public void PassDay()
    {
        //Player 위치 초기화
        gameManagerScr.ReturnPlayer();

        //Player 움직임 정지
        playerCtrlScr.TalkStart();

        //심봉사 이동후 몇초뒤에 다이얼로그 띄울건지
        Invoke("PassDayTrue", 1.5f);

        
    }

    //PassDay Flag Dealy용
    private void PassDayTrue()
    {
        passDay = true;
    }


    //나레이션 배경 끄기
    private void ActiveFalse_NarationBG()
    {
        gameObject_NarationBG.SetActive(false);
    }

    //심청이의 쪽지 보여주기
    private void ShowShimNote()
    {
        gameObject_shimeNote.SetActive(true);
    }

    private void CloseNote()
    {
        closeNote = true;
    }

    //1번 대화가 끝났는지
    private void Sentence1End()
    {
        setence1End = true;
    }

    //튜토리얼 뺑덕 대화가 모두 마무리 되었는지
    public void TutorialSenteceEnd_Bbang()
    {
        SentenceEnd_Bbang = true;
    }

    //튜토리얼 향리댁이 말하는게 모두 끝났는지
    public void TutorialSentenceEnd_Hyang()
    {
        SentenceEnd_Hyang = true;
    }

    //Save Data
    public void Save(int _slotNum)
    {
        Debug.Log("Save TutorialData");

        //저장할 데이터 넣기
        curTutorialSaveData = new TutorialSaveData(tutorialEventNum, objCtrlScr.getBotzime, objCtrlScr.getMap, turnOnLightScr.isLightsOn);

        //세이브 데이터
        string jSaveData = JsonUtility.ToJson(curTutorialSaveData);

        //데이터 파일 생성
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);
    }

    //데이터 로드
    public void Load(int _slotNum)
    {
        Debug.Log("Load TutorialData");

        //세이브 파일 읽어오기
        string jLoadData = File.ReadAllText(saveFilePath + _slotNum.ToString());

        //읽어온 파일 리스트에 저장
        curTutorialLoadData = JsonUtility.FromJson<TutorialLoadData>(jLoadData);

        //저장된 EventNum 불러오기
        tutorialEventNum = curTutorialLoadData.tutoralEventNum;

        //Event 상태 세팅
        EventsStateSetting();

        //Event 세팅
        EventSetting(curTutorialLoadData.getBotzime, curTutorialLoadData.getMap, curTutorialLoadData.isLightsOn);
    }

    //Events State Setting
    public void EventsStateSetting()
    {
        //(Enum)Events 값 세팅
        switch (tutorialEventNum)
        {
            case 0:
                events = Events.TurnOnLights;
                break;
            case 1:
                events = Events.GetItems;
                break;
            case 2:
                events = Events.TalkToBBangDuck;
                break;
            case 3:
                events = Events.TalkToHyang;
                break;
            case 4:
                events = Events.PassOneDay;
                break;
            case 5:
                events = Events.Done;
                break;
        }
    }

    //이벤트 세팅
    private void EventSetting(bool _getBotzime, bool _getMap, bool _isLightsOn)
    {
        switch (events)
        {
            //이벤트 0 : 불을 켜라
            case Events.TurnOnLights:
                {
                    //Flag 설정
                    showNote = false;
                    closeNote = false;
                    setence1End = false;
                    getObjects = false;
                    passDay = false;
                    SentenceEnd_Bbang = false;
                    SentenceEnd_Hyang = false;
                    SentenceEnd_HyangShim = false;
                    BangtalkEnd = false;
                    HyangTalkEnd1 = false;
                    HyangTalkEnd2 = false;
                    HyangTalkEnd3 = false;
                    PassDayTalkEnd1 = false;
                    PassDayTalkEnd2 = false;
                    PassDayTalkEnd3 = false;

                    //Object 설정

                    //등잔불 리셋
                    turnOnLightScr.TurnOFFLights();
                    //봇짐 리셋
                    objCtrlScr.ResetBotzime();
                    //지도 리셋
                    objCtrlScr.ResetMap();

                    //UI 설정
                    //UI Canvas 끄기
                    gameObject_UICanvas.SetActive(false);
                    //날짜 UI끄기
                    timeManagerScr.CloseDayUI();
                    //시간 흐르기
                    

                    //플레이어 이동제어
                    playerCtrlScr.TalkEnd();
                    break;
                }

            //이벤트 1 : 아이템을 획득해라
            case Events.GetItems:
                {
                    //Flag 설정
                    showNote = true;
                    closeNote = true;
                    setence1End = true;
                    getObjects = false;
                    passDay = false;
                    SentenceEnd_Bbang = false;
                    SentenceEnd_Hyang = false;
                    SentenceEnd_HyangShim = false;
                    BangtalkEnd = false;
                    HyangTalkEnd1 = false;
                    HyangTalkEnd2 = false;
                    HyangTalkEnd3 = false;
                    PassDayTalkEnd1 = false;
                    PassDayTalkEnd2 = false;
                    PassDayTalkEnd3 = false;

                    //Object 설정

                    //등잔불이 켜져있었다면
                    if(_isLightsOn)
                    {
                        //등잔불 켜기
                        turnOnLightScr.TurnOnLights();
                    }

                    //등잔불이 꺼져 있었다면
                    else if (!_isLightsOn)
                    {
                        //등잔불 끄기
                        turnOnLightScr.TurnOFFLights();
                    }

                    //봇짐만 획득 했다면
                    if (_getBotzime && !_getMap)
                    {
                        objCtrlScr.LoadBotzime();
                        objCtrlScr.ResetMap();
                    }
                    //지도만 획득 했다면
                    else if (!_getBotzime && _getMap)
                    {
                        objCtrlScr.LoadMap();
                        objCtrlScr.ResetBotzime();
                    }
                    else
                    {
                        //봇짐 리셋
                        objCtrlScr.ResetBotzime();
                        //지도 리셋
                        objCtrlScr.ResetMap();
                    }

                    //UI 설정
                    //UI Canvas 켜기
                    gameObject_UICanvas.SetActive(true);
                    //날짜 UI끄기
                    timeManagerScr.CloseDayUI();
                    //플레이어 이동제어
                    playerCtrlScr.TalkEnd();
                    break;
                }

            //이벤트 2 : 뺑떡과 대화
            case Events.TalkToBBangDuck:
                {
                    //Flag 설정
                    showNote = true;
                    closeNote = true;
                    setence1End = true;
                    getObjects = true;
                    passDay = false;
                    SentenceEnd_Bbang = false;
                    SentenceEnd_Hyang = false;
                    SentenceEnd_HyangShim = false;
                    BangtalkEnd = false;
                    HyangTalkEnd1 = false;
                    HyangTalkEnd2 = false;
                    HyangTalkEnd3 = false;
                    PassDayTalkEnd1 = false;
                    PassDayTalkEnd2 = false;
                    PassDayTalkEnd3 = false;

                    //Object 설정

                    //등잔불이 켜져있었다면
                    if (_isLightsOn)
                    {
                        //등잔불 켜기
                        turnOnLightScr.TurnOnLights();
                    }

                    //등잔불이 꺼져 있었다면
                    else if (!_isLightsOn)
                    {
                        //등잔불 끄기
                        turnOnLightScr.TurnOFFLights();
                    }

                    //봇짐 획득
                    objCtrlScr.LoadBotzime();
                    //지도 획득
                    objCtrlScr.LoadMap();

                    //UI 설정
                    //UI Canvas 켜기
                    gameObject_UICanvas.SetActive(true);
                    //날짜 UI끄기
                    timeManagerScr.CloseDayUI();
                    //플레이어 이동제어
                    playerCtrlScr.TalkEnd();
                    break;
                }

            //이벤트 3 : 향리댁과 대화
            case Events.TalkToHyang:
                {
                    //Flag 설정
                    showNote = true;
                    closeNote = true;
                    setence1End = true;
                    getObjects = true;
                    passDay = false;
                    SentenceEnd_Bbang = true;
                    SentenceEnd_Hyang = false;
                    SentenceEnd_HyangShim = false;
                    BangtalkEnd = true;
                    HyangTalkEnd1 = false;
                    HyangTalkEnd2 = false;
                    HyangTalkEnd3 = false;
                    PassDayTalkEnd1 = false;
                    PassDayTalkEnd2 = false;
                    PassDayTalkEnd3 = false;

                    //Object 설정

                    //등잔불이 켜져있었다면
                    if (_isLightsOn)
                    {
                        //등잔불 켜기
                        turnOnLightScr.TurnOnLights();
                    }

                    //등잔불이 꺼져 있었다면
                    else if (!_isLightsOn)
                    {
                        //등잔불 끄기
                        turnOnLightScr.TurnOFFLights();
                    }

                    //봇짐 획득
                    objCtrlScr.LoadBotzime();
                    //지도 획득
                    objCtrlScr.LoadMap();

                    //UI 설정
                    //UI Canvas 켜기
                    gameObject_UICanvas.SetActive(true);
                    //날짜 UI끄기
                    timeManagerScr.CloseDayUI();
                    //플레이어 이동제어
                    playerCtrlScr.TalkEnd();
                    break;
                }

            //이벤트 4 : 하루를 보내자
            case Events.PassOneDay:
                {
                    //Flag 설정
                    showNote = true;
                    closeNote = true;
                    setence1End = true;
                    getObjects = true;
                    passDay = false;
                    SentenceEnd_Bbang = true;
                    SentenceEnd_Hyang = true;
                    SentenceEnd_HyangShim = true;
                    BangtalkEnd = true;
                    HyangTalkEnd1 = true;
                    HyangTalkEnd2 = true;
                    HyangTalkEnd3 = true;
                    PassDayTalkEnd1 = false;
                    PassDayTalkEnd2 = false;
                    PassDayTalkEnd3 = false;

                    //Object 설정

                    //등잔불이 켜져있었다면
                    if (_isLightsOn)
                    {
                        //등잔불 켜기
                        turnOnLightScr.TurnOnLights();
                    }

                    //등잔불이 꺼져 있었다면
                    else if (!_isLightsOn)
                    {
                        //등잔불 끄기
                        turnOnLightScr.TurnOFFLights();
                    }

                    //봇짐 획득
                    objCtrlScr.LoadBotzime();
                    //지도 획득
                    objCtrlScr.LoadMap();

                    //UI 설정
                    //UI Canvas 켜기
                    gameObject_UICanvas.SetActive(true);
                    //날짜 UI 켜기
                    timeManagerScr.ShowDayUI();
                    //플레이어 이동제어
                    playerCtrlScr.TalkEnd();
                    //시간 흐르기
                    timeManagerScr.ContinueTime();
                    break;
                }

            //이벤트 5 : 이벤트 끝
            case Events.Done:
                {
                    //Flag 설정
                    showNote = true;
                    closeNote = true;
                    setence1End = true;
                    getObjects = true;
                    passDay = true;
                    SentenceEnd_Bbang = true;
                    SentenceEnd_Hyang = true;
                    SentenceEnd_HyangShim = true;
                    BangtalkEnd = true;
                    HyangTalkEnd1 = true;
                    HyangTalkEnd2 = true;
                    HyangTalkEnd3 = true;
                    PassDayTalkEnd1 = true;
                    PassDayTalkEnd2 = true;
                    PassDayTalkEnd3 = true;

                    //Object 설정

                    //등잔불이 켜져있었다면
                    if (_isLightsOn)
                    {
                        //등잔불 켜기
                        turnOnLightScr.TurnOnLights();
                    }

                    //등잔불이 꺼져 있었다면
                    else if (!_isLightsOn)
                    {
                        //등잔불 끄기
                        turnOnLightScr.TurnOFFLights();
                    }

                    //봇짐 획득
                    objCtrlScr.LoadBotzime();
                    //지도 획득
                    objCtrlScr.LoadMap();

                    //UI 설정
                    //UI Canvas 켜기
                    gameObject_UICanvas.SetActive(true);
                    //날짜 UI 켜기
                    timeManagerScr.ShowDayUI();
                    //플레이어 이동제어
                    playerCtrlScr.TalkEnd();
                    //시간 흐르기
                    timeManagerScr.ContinueTime();
                    break;
                }
        }
    }
}
