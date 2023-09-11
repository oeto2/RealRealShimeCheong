using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//세이브할 데이터
public class EventSaveData
{
    //생성자
    public EventSaveData(bool _joomuckBob, bool _binyeo, bool _flower, bool _muck, bool _boridduck, bool _deliveryMuck, bool _muckEvent_End)
    {
        joomuckBob = _joomuckBob;
        binyeo = _binyeo;
        flower = _flower;
        muck = _muck;
        boridduck = _boridduck;
        deliveryMuck = _deliveryMuck;
        muckEvent_End = _muckEvent_End;
    }

    //이벤트 활성화 여부
    public bool joomuckBob;
    public bool binyeo;
    public bool flower;
    public bool muck;
    public bool boridduck;

    //먹을 전달 했는지 여부
    public bool deliveryMuck;

    //먹 이벤트를 완료했는지
    public bool muckEvent_End;
}

//로드할 데이터
public class EventLoadData
{
    //생성자
    public EventLoadData(bool _joomuckBob, bool _binyeo, bool _flower, bool _muck, bool _boridduck, bool _deliveryMuck, bool _muckEvent_End)
    {
        joomuckBob = _joomuckBob;
        binyeo = _binyeo;
        flower = _flower;
        muck = _muck;
        boridduck = _boridduck;
        deliveryMuck = _deliveryMuck;
        muckEvent_End = _muckEvent_End;
    }

    //이벤트 활성화 여부
    public bool joomuckBob;
    public bool binyeo;
    public bool flower;
    public bool muck;
    public bool boridduck;

    //먹을 전달 했는지 여부
    public bool deliveryMuck;

    //먹 이벤트를 완료했는지
    public bool muckEvent_End;
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
}

//이벤트를 마무리했는지 체크
[System.Serializable]
public class EventEndCheck
{
    //먹 이벤트를 완료했는지
    public bool muckEvent_End;
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


public class EventManager : MonoBehaviour
{
    //싱글톤 패턴
    public static EventManager instance = null;

    //이벤트 체크
    public EventCheck eventCheck;

    //이벤트 진행 체크
    public EventProgress eventProgress;

    //이벤트 완료 체크
    public EventEndCheck eventEndCheck;

    //이벤트 리스트
    public List<EventCheck> eventList;

    //저장할 데이터 클래스
    public EventSaveData curEventSaveData;

    //저장 파일 위치
    public string saveFilePath;

    //불러올 데이터 클래스
    public EventLoadData curEventLoadData;

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

        switch(_eventName)
        {
            case Events.binyeo:
                if(eventCheck.binyeo == true)
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
        switch(_eventName)
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
            eventCheck.flower, eventCheck.muck, eventCheck.boridduck, eventProgress.deliveryMuck, eventEndCheck.muckEvent_End);

        //세이브 데이터
        string jSaveData = JsonUtility.ToJson(curEventSaveData);

        //데이터 파일 생성
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);
    }
    
    //데이터 로드
    public void Load(int _SlotNum)
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

        //이벤트 완료 상황 초기화
        eventEndCheck.muckEvent_End = curEventLoadData.muckEvent_End;
    }

}
