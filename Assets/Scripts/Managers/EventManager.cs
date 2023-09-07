using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임내에 이벤트의 발생 체크
[System.Serializable]
public class EventCheck
{
    public bool joomackBab;
    public bool binyeo;
    public bool flower;
    public bool muck;
    public bool boridduck;
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

    //이벤트 리스트
    public List<EventCheck> eventList;

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
}
