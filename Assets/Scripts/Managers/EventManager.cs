using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ӳ��� �̺�Ʈ�� �߻� üũ
[System.Serializable]
public class EventCheck
{
    public bool joomackBab;
    public bool binyeo;
    public bool flower;
    public bool muck;
    public bool boridduck;
}

//�̺�Ʈ ���
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
    //�̱��� ����
    public static EventManager instance = null;

    //�̺�Ʈ üũ
    public EventCheck eventCheck;

    //�̺�Ʈ ����Ʈ
    public List<EventCheck> eventList;

    private void Awake()
    {
        #region �̱���
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


    //�̺�Ʈ�� Ȱ��ȭ ������ Ȯ���� bool ���� �������ִ� �޼���
    public bool GetEventBool(Events _eventName)
    {
        //��ȯ���� ���ϰ�
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


    //�̺�Ʈ Ȱ��ȭ �޼���
    public void EventActive(Events _eventName)
    {
        switch(_eventName)
        {
            //��� �̺�Ʈ Ȱ��ȭ
            case Events.binyeo:
                eventCheck.binyeo = true;
                break;

            //������ �̺�Ʈ Ȱ��ȭ
            case Events.boridduck:
                eventCheck.boridduck = true;
                break;

            //�� �̺�Ʈ Ȱ��ȭ
            case Events.flower:
                eventCheck.flower = true;
                break;
            
            //�ָԹ� �̺�Ʈ Ȱ��ȭ
            case Events.JoomuckBab:
                eventCheck.joomackBab = true;
                break;

            //�� �̺�Ʈ Ȱ��ȭ
            case Events.muck:
                eventCheck.muck = true;
                break;
        }
    }
}
