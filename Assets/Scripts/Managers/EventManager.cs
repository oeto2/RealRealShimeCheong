using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

//세이브할 데이터
public class EventSaveData
{
    //생성자
    public EventSaveData(bool _joomuckBob, bool _binyeo, bool _flower, bool _muck, bool _boridduck,
        bool _deliveryMuck, bool _muckEvent_End, bool _joomackPuzzle_End, bool _giveFlower, bool _day15ClueTalk, bool _day15ClueGet, bool _giveBoridduck_End
        , bool _select2006_End, bool _boatManObject, bool _talkClue_6045, bool _drinkHerb)
    {
        joomuckBob = _joomuckBob;
        binyeo = _binyeo;
        flower = _flower;
        muck = _muck;
        boridduck = _boridduck;
        deliveryMuck = _deliveryMuck;
        muckEvent_End = _muckEvent_End;
        joomackPuzzle_End = _joomackPuzzle_End;
        giveFlower = _giveFlower;
        day15ClueTalk = _day15ClueTalk;
        day15ClueGet = _day15ClueGet;
        giveBoridduck_End = _giveBoridduck_End;
        select2006_End = _select2006_End;
        boatManObject = _boatManObject;
        talkClue_6045 = _talkClue_6045;
        drinkHerb = _drinkHerb;
    }

    //이벤트 활성화 여부
    public bool joomuckBob;
    public bool binyeo;
    public bool flower;
    public bool muck;
    public bool boridduck;
    //3월 보름날 이벤트
    public bool day15ClueTalk;


    //먹을 전달 했는지 여부
    public bool deliveryMuck;
    //꽃을 전달 했는지 여부
    public bool giveFlower;

    //이벤트 완료 여부
    public bool muckEvent_End;
    public bool joomackPuzzle_End;
    public bool day15ClueGet;
    public bool giveBoridduck_End;

    //선택지 완료 여부
    public bool select2006_End;

    //사공의 물건 전달 여부
    public bool boatManObject;

    //장지언과 6045대화를 진행했는지
    public bool talkClue_6045;

    //약초물을 마셨는지
    public bool drinkHerb;
}

//로드할 데이터
public class EventLoadData
{
    //생성자
    public EventLoadData(bool _joomuckBob, bool _binyeo, bool _flower, bool _muck, bool _boridduck,
        bool _deliveryMuck, bool _muckEvent_End, bool _joomackPuzzle_End, bool _giveFlower, bool _day15ClueTalk, bool _day15ClueGet, bool _giveBoridduck_End
        , bool _select2006_End, bool _boatManObject, bool _talkClue_6045, bool _drinkHerb)
    {
        joomuckBob = _joomuckBob;
        binyeo = _binyeo;
        flower = _flower;
        muck = _muck;
        boridduck = _boridduck;
        deliveryMuck = _deliveryMuck;
        muckEvent_End = _muckEvent_End;
        joomackPuzzle_End = _joomackPuzzle_End;
        giveFlower = _giveFlower;
        day15ClueTalk = _day15ClueTalk;
        day15ClueGet = _day15ClueGet;
        giveBoridduck_End = _giveBoridduck_End;
        select2006_End = _select2006_End;
        boatManObject = _boatManObject;

        talkClue_6045 = _talkClue_6045;

        drinkHerb = _drinkHerb;
    }

    //이벤트 활성화 여부
    public bool joomuckBob;
    public bool binyeo;
    public bool flower;
    public bool muck;
    public bool boridduck;
    //3월 보름날 이벤트
    public bool day15ClueTalk;

    //먹을 전달 했는지 여부
    public bool deliveryMuck;
    //꽃을 전달 했는지 여부
    public bool giveFlower;

    //이벤트 완료 여부
    public bool muckEvent_End;
    public bool joomackPuzzle_End;
    public bool day15ClueGet;
    public bool giveBoridduck_End;

    //선택지 완료 여부
    public bool select2006_End;

    //사공의 물건 전달 여부
    public bool boatManObject;

    //장지언과 6045대화를 진행했는지
    public bool talkClue_6045;

    //약초물을 마셨는지
    public bool drinkHerb;
}


//게임내의 이벤트의 발생 체크
[System.Serializable]
public class EventCheck
{
    public bool joomackBab;
    public bool binyeo;
    public bool flower;
    public bool muck;
    public bool boridduck;
}

//게임내의 이벤트 진행사항 체크
[System.Serializable]
public class EventProgress
{
    //먹을 전달 했는지 여부
    public bool deliveryMuck;
    //주막 퍼즐을 클리어 했는지 여부
    public bool joomackPuzzle_Clear;
    //꽃을 전달했는지 여부
    public bool giveFlowerEnd;
    //3월 보름날 단서 대화를 시작했는지 여부
    public bool day15ClueStart;
}

//이벤트를 마무리했는지 체크
[System.Serializable]
public class EventEndCheck
{
    //먹 이벤트를 완료했는지
    public bool muckEvent_End;
    //보리떡 전달을 완료헀는지
    public bool giveBoridduck_End;
    //3월 보름날 단서를 획득 했는지 여부
    public bool day15ClueGet;
}

//선택지를 마무리했는지 체크
[System.Serializable]
public class SelectEndCheck
{
    //송나라 상인과 청이 선택지를 완료 했는지
    public bool select2006_End;
}

//이벤트 목록
public enum Events
{
    JoomuckBab,
    binyeo,
    flower,
    muck,
    boridduck,
    Lenght
}

//NPC 이름들
public enum NPCName
{
    NONE,
    Bbangduck,
    boatman,
    boatman2,
    boatman3,
    beggar,
    Guidyck,
    Songnara,
    budhist,
    BusinessMan,
    JangSeong,
    Shimbongsa,
    Shimbongsa2,
    shimCheong,

    //이벤트
    Herb,
    Herb2
}


public class EventManager : MonoBehaviour
{
    //외부 스크립트 참조
    public Dialog_TypingWriter_BoatMan boatManDialogueScr;
    public Dialog_TypingWriter_BoatMan2 boatManDialogueScr2;
    public BoatMan3 boatManDialogueScr3;
    public Dialog_TypingWriter_Jangjieon jangjieonDialogueScr;
    public JoomuckBab joomuckBabScr;

    //플레이어 빛 스크립트
    public Light2D light2dScr;
    
    //싱글톤 패턴
    public static EventManager instance = null;

    //이벤트 체크
    public EventCheck eventCheck;

    //이벤트 진행 체크
    public EventProgress eventProgress;

    //이벤트 완료 체크
    public EventEndCheck eventEndCheck;

    //선택지 완료 체크
    public SelectEndCheck selectEndCheck;

    //이벤트 리스트
    public List<EventCheck> eventList;

    //저장할 데이터 클래스
    public EventSaveData curEventSaveData;

    //저장 파일 위치
    public string saveFilePath;

    //불러올 데이터 클래스
    public EventLoadData curEventLoadData;

    //선택지 UI
    public GameObject gameObject_SelectUI;

    //선택지에 사용할 NPC 이름
    public NPCName selectNPCName = NPCName.NONE;

    //선택지 1번 Text
    public Text text_selectNum1;

    //선택지 2번 Text
    public Text text_selectNum2;

    //선택지 3번 Text
    public Text text_selectNum3;

    //선택지의 키값
    public int int_selectKeyNum;

    //뱃사공3 선택지 Text UI
    public GameObject gameObject_BoatMan3Text;

    //약초물 마시기 선택지 Text UI
    public GameObject gameObject_DrinkHerbText;

    //선택지 2번 오브젝트
    public GameObject gameObject_SelectNum2;

    //선택지 3번 오브젝트
    public GameObject gameObject_SelectNum3;

    //약초를 마셨는지
    public bool drinkHerb;


    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        #endregion

        //저장 파일 위치
        saveFilePath = Application.persistentDataPath + "/EventSaveText.txt";
    }


    //이벤트가 활성화 중인지 확인후 bool 값을 리턴해주는 메서드
    public bool GetEventBool(Events _eventName)
    {
        //반환해줄 리턴값
        bool retrunValue = false;

        switch (_eventName)
        {
            case Events.binyeo:
                if (eventCheck.binyeo == true)
                {
                    retrunValue = true;
                    break;
                }
                else
                {
                    retrunValue = false;
                    break;
                }

            case Events.boridduck:
                if (eventCheck.boridduck == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case Events.flower:
                if (eventCheck.flower == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case Events.JoomuckBab:
                if (eventCheck.joomackBab == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case Events.muck:
                if (eventCheck.muck == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        return retrunValue;
    }


    //이벤트 활성화 메서드
    public void EventActive(Events _eventName)
    {
        switch (_eventName)
        {
            //비녀 이벤트 활성화
            case Events.binyeo:
                eventCheck.binyeo = true;
                break;

            //보리떡 이벤트 활성화
            case Events.boridduck:
                eventCheck.boridduck = true;
                break;

            //꽃 이벤트 활성화
            case Events.flower:
                eventCheck.flower = true;
                break;

            //주먹밥 이벤트 활성화
            case Events.JoomuckBab:
                eventCheck.joomackBab = true;
                break;

            //먹 이벤트 활성화
            case Events.muck:
                eventCheck.muck = true;
                break;
        }
    }

    //데이터 저장
    public void Save(int _slotNum)
    {
        Debug.Log("Save EventData");

        //저장할 데이터 넣기
        curEventSaveData = new EventSaveData(eventCheck.joomackBab, eventCheck.binyeo,
            eventCheck.flower, eventCheck.muck, eventCheck.boridduck, eventProgress.deliveryMuck, eventEndCheck.muckEvent_End
            , eventProgress.joomackPuzzle_Clear, eventProgress.giveFlowerEnd, eventProgress.day15ClueStart, eventEndCheck.day15ClueGet, eventEndCheck.giveBoridduck_End
            , selectEndCheck.select2006_End, boatManDialogueScr.boatManObject, jangjieonDialogueScr.talkClue_6045, drinkHerb);

        //세이브 데이터
        string jSaveData = JsonUtility.ToJson(curEventSaveData);

        //데이터 파일 생성
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);
    }

    //데이터 로드
    public void Load(int _SlotNum)
    {
        if (_SlotNum <= 2)
        {
            Debug.Log("Load EventLoadData");

            //세이브 파일 읽어오기
            string jLoadData = File.ReadAllText(saveFilePath + _SlotNum.ToString());

            //읽어온 파일 리스트에 저장
            curEventLoadData = JsonUtility.FromJson<EventLoadData>(jLoadData);

            //이벤트 발생 초기화
            eventCheck.joomackBab = curEventLoadData.joomuckBob;
            eventCheck.binyeo = curEventLoadData.binyeo;
            eventCheck.boridduck = curEventLoadData.boridduck;
            eventCheck.flower = curEventLoadData.flower;
            eventCheck.muck = curEventLoadData.muck;

            //이벤트 진행 상황 초기화
            eventProgress.deliveryMuck = curEventLoadData.deliveryMuck;
            eventProgress.joomackPuzzle_Clear = curEventLoadData.joomackPuzzle_End;
            eventProgress.giveFlowerEnd = curEventLoadData.giveFlower;

            //이벤트 완료 상황 초기화
            eventEndCheck.muckEvent_End = curEventLoadData.muckEvent_End;

            //사공의 물건 진행상황 초기화
            boatManDialogueScr.boatManObject = curEventLoadData.boatManObject;

            //장지언 대화 진행상황 저장
            jangjieonDialogueScr.talkClue_6045 = curEventLoadData.talkClue_6045;

            //약초물 플래그 초기화
            drinkHerb = curEventLoadData.drinkHerb;

            //플레이어 속도, 시야 초기화
            PlayerStateReset();

            //만약 약초를 마신 상태라면 이속,시야 2배
            if (curEventLoadData.drinkHerb)
            {
                PlayerStateMultiple();

                //플래그 초기화
                joomuckBabScr.drinkHerb = true;
            }
        }
    }

    //선택지 시작
    public void SelectStart(NPCName _npcName, int _SelectNum)
    {
        Debug.Log("선택지 시작");

        //선택지 시작
        GameManager.instance.isPlayerSelecting = true;

        //선택지 키값 변경
        int_selectKeyNum = _SelectNum;

        //선택지 UI 띄우기
        gameObject_SelectUI.SetActive(true);

        //마우스 UI 보이기
        UIManager.instance.ShowCursor();

        switch (_npcName)
        {
            case NPCName.NONE:
                //선택지 종료
                gameObject_SelectUI.SetActive(false);
                break;

            //뱃사공 선택지
            case NPCName.boatman:

                //송나라 상인과 청이 선택지일 경우
                if (ObjectManager.instance.GetEquipObjectKey() == 2006)
                {
                    //NPC 다이얼로그 종료
                    DialogManager.instance.Dialouge_System.SetActive(false);

                    //1번 선택지 대사 입력
                    text_selectNum1.text = "내가 청이 아비 되는 사람이오. 솔직하게 말해주시오.";
                    //2번 선택지 대사 입력
                    text_selectNum2.text = "나도 그 이야기라면 들었소. 송 사람들이 너무하던데 말이오!";
                }
                break;

            //뱃사공 선택지2
            case NPCName.boatman2:

                //NPC 다이얼로그 종료
                DialogManager.instance.Dialouge_System.SetActive(false);

                //1번 선택지 대사 입력
                text_selectNum1.text = "예";
                //2번 선택지 대사 입력
                text_selectNum2.text = "아니오";

                break;

            //뱃사공 선택지3
            case NPCName.boatman3:

                //선택지 대사 보이기
                gameObject_BoatMan3Text.SetActive(true);

                //NPC 다이얼로그 종료
                DialogManager.instance.Dialouge_System.SetActive(false);

                //1번 선택지 대사 입력
                text_selectNum1.text = "예";
                //2번 선택지 대사 입력
                text_selectNum2.text = "아니오";

                break;

            //심봉사 선택지
            case NPCName.Shimbongsa:

                //NPC 다이얼로그 종료
                DialogManager.instance.Dialouge_System.SetActive(false);

                //만약 장지언과 6045번 단서 대화를 하지 않았을 경우
                if (!jangjieonDialogueScr.talkClue_6045)
                {
                    //2번 선택지 비활성화
                    gameObject_SelectNum2.SetActive(false);

                    //1번 선택지 대사 입력
                    text_selectNum1.text = "구하러 뛰어든다";
                }

                else
                {
                    //1번 선택지 대사 입력
                    text_selectNum1.text = "구하러 뛰어든다";

                    //2번 선택지 대사 입력
                    text_selectNum2.text = "가만히 있는다";
                }
                break;

            //심봉사 선택지2
            case NPCName.Shimbongsa2:

                //NPC 다이얼로그 종료
                DialogManager.instance.Dialouge_System.SetActive(false);

                //선택지 3번 보이기
                gameObject_SelectNum3.SetActive(true);

                //1번 선택지 대사 입력
                text_selectNum1.text = "얼마나 걱정한줄아느냐? 어찌 이런 일을 벌인 것이야!";

                //2번 선택지 대사 입력
                text_selectNum2.text = "미안하다.청아.";

                //3번 선택지 대사 입력
                text_selectNum3.text = "(아무말도 하지 않는다.)";

                break;



            //약초 마시기 선택지
            case NPCName.Herb:

                //NPC 다이얼로그 종료
                DialogManager.instance.Dialouge_System.SetActive(false);

                //약초 마시기 Text 띄우기
                gameObject_DrinkHerbText.SetActive(true);
                    
                //1번 선택지 대사 입력
                text_selectNum1.text = "예";

                //2번 선택지 대사 입력
                text_selectNum2.text = "아니오";

                break;
        }
    }

    //선택지 1번 선택
    public void SelectNum1_Click()
    {
        //2번 선택지 보이기
        gameObject_SelectNum2.SetActive(true);

        //선택지 종료
        GameManager.instance.isPlayerSelecting = false;

        //커서 끄기
        UIManager.instance.BlindCursor();

        switch (int_selectKeyNum)
        {
            //송나라 상인과 청이 선택지일 경우
            case 2006:
                //선택지 종료
                gameObject_SelectUI.SetActive(false);

                //선택지 1번 선택
                boatManDialogueScr.int_Select2006Num = 1;

                //다이얼로그 활성화
                DialogManager.instance.Dialouge_System.SetActive(true);

                //대사 출력
                boatManDialogueScr.PrintSelect2006_Sentence1();

                //선택지 완료
                selectEndCheck.select2006_End = true;
                break;

            //뱃사공2 선택지일 경우
            case 7355:

                //사공의 물건 퀘스트를 완료하지 못했다면
                if (boatManDialogueScr.boatManObject == false)
                {
                    //선택지 창 끄기
                    gameObject_SelectUI.SetActive(false);

                    //계란유골 배드엔딩
                    boatManDialogueScr2.StartBoatManEnding_1();
                }

                //사공의 물건 퀘스트 완료
                else
                {
                    //청이의 물건 단서를 보유중이지 않다면
                    if (!ObjectManager.instance.GetClue_Check(9000))
                    {
                        Debug.Log("청이 단서 미보유 엔딩");

                        //선택지 창 끄기
                        gameObject_SelectUI.SetActive(false);

                        //계란유골 배드엔딩
                        boatManDialogueScr2.StartBoatManEnding_1();
                    }

                    //청이의 물건 단서를 보유 중이라면
                    else
                    {
                        //선택지 창 끄기
                        gameObject_SelectUI.SetActive(false);

                        //굿/진엔딩 루트 진입
                        boatManDialogueScr2.StartGoodEndingRoot();
                    }
                }
                break;

            //뱃사공3 선택지일 경우
            case 7194:

                //선택 완료
                boatManDialogueScr3.isSelectDone = true;

                //선택지 대사 끄기
                gameObject_BoatMan3Text.SetActive(false);

                //선택지 종료
                gameObject_SelectUI.SetActive(false);

                //엔딩 진행
                boatManDialogueScr3.StartEndingSentence();

                break;

            //심봉사 선택지일 경우
            case 7287:
                //다이얼로그 플래그 초기화
                jangjieonDialogueScr.remainSentence = true;
                jangjieonDialogueScr.isSentenceEnd = true;

                //다이얼로그 띄우기
                DialogManager.instance.Dialouge_System.SetActive(true);

                //선택지 종료
                gameObject_SelectUI.SetActive(false);

                //엔딩 진행
                jangjieonDialogueScr.StartGoodEnidng();
                break;

            //심봉사2 선택지일 경우
            case 7299:
                //다이얼로그 플래그 초기화
                jangjieonDialogueScr.remainSentence = true;
                jangjieonDialogueScr.isSentenceEnd = true;

                //다이얼로그 띄우기
                DialogManager.instance.Dialouge_System.SetActive(true);

                //선택지 종료
                gameObject_SelectUI.SetActive(false);

                //굿엔딩 진행
                jangjieonDialogueScr.StartGoodEnding_1();

                break;

            //약초물 마시기1
            case 5799:
                
                //이벤트 진행 상황 변경
                joomuckBabScr.makeHerbOrder = JoomuckBab.MakeHerbOrder.DrinkHerb2;

                //선택지 종료
                gameObject_SelectUI.SetActive(false);

                //약초물 마심
                drinkHerb = true;

                //이동속도 2배 증가, 화면 밝기 증가
                PlayerStateMultiple();

                //대사 진행
                DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(348), true);

                break;

            //약초물 마시기2
            case 7009:

                //이벤트 진행 상황 변경
                joomuckBabScr.makeHerbOrder = JoomuckBab.MakeHerbOrder.Edning;

                //선택지 종료
                gameObject_SelectUI.SetActive(false);

                //엔딩 시작
                Debug.Log("과유불급 엔딩");
                DialogManager.instance.StartBadEndingSentence();

                break;
        }
    }

    //선택지 2번 선택
    public void SelectNum2_Click()
    {
        //선택지 종료
        GameManager.instance.isPlayerSelecting = false;

        //커서 끄기
        UIManager.instance.BlindCursor();

        switch (int_selectKeyNum)
        {
            //송나라 상인과 청이 선택지일 경우
            case 2006:

                //선택지 종료
                gameObject_SelectUI.SetActive(false);

                //선택지 2번 선택
                boatManDialogueScr.int_Select2006Num = 2;

                //선택지 날짜 넘겨주기
                boatManDialogueScr.int_select2006Day = TimeManager.instance.int_DayCount;

                //다이얼로그 활성화
                DialogManager.instance.Dialouge_System.SetActive(true);

                //대사 출력
                boatManDialogueScr.PrintSelect2006_Sentence2();

                //선택지 완료
                selectEndCheck.select2006_End = true;
                break;

            //뱃사공2 선택지일 경우
            case 7355:

                //선택지 종료
                gameObject_SelectUI.SetActive(false);

                //플레이어 움직임 제한 해제
                Controller.instance.TalkEnd();

                //뱃사공 대화 제어 플래그 초기화
                boatManDialogueScr2.isSentenceEnd = true;
                boatManDialogueScr2.remainSentence = true;

                break;

            //뱃사공3 선택지일 경우
            case 7194:

                //선택지 대사 끄기
                gameObject_BoatMan3Text.SetActive(false);

                //선택지 종료
                gameObject_SelectUI.SetActive(false);

                //플레이어 움직임 제한 해제
                Controller.instance.TalkEnd();
                break;

            //심봉사 선택지일 경우
            case 7287:
                //다이얼로그 플래그 초기화
                jangjieonDialogueScr.remainSentence = true;
                jangjieonDialogueScr.isSentenceEnd = true;

                //다이얼로그 띄우기
                DialogManager.instance.Dialouge_System.SetActive(true);

                //선택지 종료
                gameObject_SelectUI.SetActive(false);

                //엔딩 진행
                jangjieonDialogueScr.StartRealEndingRoot();

                break;

            //심봉사2 선택지일 경우
            case 7299:
                //다이얼로그 플래그 초기화
                jangjieonDialogueScr.remainSentence = true;
                jangjieonDialogueScr.isSentenceEnd = true;

                //다이얼로그 띄우기
                DialogManager.instance.Dialouge_System.SetActive(true);

                //선택지 종료
                gameObject_SelectUI.SetActive(false);

                //엔딩 진행
                jangjieonDialogueScr.StartGoodEnding_2();

                break;


            //약초물 마시기1
            case 5799:

                //선택지 종료
                gameObject_SelectUI.SetActive(false);

                break;

            //약초물 마시기2
            case 7009:

                //선택지 종료
                gameObject_SelectUI.SetActive(false);

                break;
        }
    }

    //선택지 3번 선택
    public void SelectNum3_Click()
    {
        //선택지 종료
        GameManager.instance.isPlayerSelecting = false;

        //커서 끄기
        UIManager.instance.BlindCursor();

        switch (int_selectKeyNum)
        {

            //심봉사2 선택지일 경우
            case 7299:
                //다이얼로그 플래그 초기화
                jangjieonDialogueScr.remainSentence = true;
                jangjieonDialogueScr.isSentenceEnd = true;

                //다이얼로그 띄우기
                DialogManager.instance.Dialouge_System.SetActive(true);

                //선택지 종료
                gameObject_SelectUI.SetActive(false);

                //진 엔딩 진행
                jangjieonDialogueScr.StartRealEnding();
                break;
        }
    }

    //속도, 시야 리셋
    public void PlayerStateReset()
    {
        Controller.instance.moveSpeed = 10;
        light2dScr.pointLightOuterRadius = 6;
    }

    //속도, 시야 2배
    public void PlayerStateMultiple()
    {
        Controller.instance.moveSpeed *= 2;
        light2dScr.pointLightOuterRadius *= 2;
    }
}

