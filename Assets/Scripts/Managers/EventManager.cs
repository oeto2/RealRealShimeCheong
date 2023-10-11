using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//세이브할 데이터
public class EventSaveData
{
    //생성자
    public EventSaveData(bool _joomuckBob, bool _binyeo, bool _flower, bool _muck, bool _boridduck,
        bool _deliveryMuck, bool _muckEvent_End, bool _joomackPuzzle_End, bool _giveFlower, bool _day15ClueTalk, bool _day15ClueGet, bool _giveBoridduck_End
        , bool _select2006_End)
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
}

//로드할 데이터
public class EventLoadData
{
    //생성자
    public EventLoadData(bool _joomuckBob, bool _binyeo, bool _flower, bool _muck, bool _boridduck,
        bool _deliveryMuck, bool _muckEvent_End, bool _joomackPuzzle_End, bool _giveFlower, bool _day15ClueTalk, bool _day15ClueGet, bool _giveBoridduck_End
        , bool _select2006_End)
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
    beggar,
    Guidyck,
    Songnara,
    budhist,
    BusinessMan,
    JangSeong,
    Shimbongsa
}


public class EventManager : MonoBehaviour
{
    //외부 스크립트 참조
    public Dialog_TypingWriter_BoatMan boatManDialogueScr;
    public Dialog_TypingWriter_BoatMan2 boatManDialogueScr2;

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

    //선택지의 키값
    public int int_selectKeyNum;

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
            , selectEndCheck.select2006_End);

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
        }
    }

    //선택지 시작
    public void SelectStart(NPCName _npcName, int _SelectNum)
    {
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
        }
    }

    //선택지 1번 선택
    public void SelectNum1_Click()
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

                //해당 단서를 보유중이라면
                if(ObjectManager.instance.GetClue_Check(2000))
                {
                }
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
        }
    }
}
