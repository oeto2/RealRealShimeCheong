using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//���̺��� ������
public class EventSaveData
{
    //������
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

    //�̺�Ʈ Ȱ��ȭ ����
    public bool joomuckBob;
    public bool binyeo;
    public bool flower;
    public bool muck;
    public bool boridduck;

    //���� ���� �ߴ��� ����
    public bool deliveryMuck;

    //�� �̺�Ʈ�� �Ϸ��ߴ���
    public bool muckEvent_End;
}

//�ε��� ������
public class EventLoadData
{
    //������
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

    //�̺�Ʈ Ȱ��ȭ ����
    public bool joomuckBob;
    public bool binyeo;
    public bool flower;
    public bool muck;
    public bool boridduck;

    //���� ���� �ߴ��� ����
    public bool deliveryMuck;

    //�� �̺�Ʈ�� �Ϸ��ߴ���
    public bool muckEvent_End;
}


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

//���ӳ��� �̺�Ʈ ������� üũ
[System.Serializable]
public class EventProgress
{
    //���� ���� �ߴ��� ����
    public bool deliveryMuck;
}

//�̺�Ʈ�� �������ߴ��� üũ
[System.Serializable]
public class EventEndCheck
{
    //�� �̺�Ʈ�� �Ϸ��ߴ���
    public bool muckEvent_End;
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

    //�̺�Ʈ ���� üũ
    public EventProgress eventProgress;

    //�̺�Ʈ �Ϸ� üũ
    public EventEndCheck eventEndCheck;

    //�̺�Ʈ ����Ʈ
    public List<EventCheck> eventList;

    //������ ������ Ŭ����
    public EventSaveData curEventSaveData;

    //���� ���� ��ġ
    public string saveFilePath;

    //�ҷ��� ������ Ŭ����
    public EventLoadData curEventLoadData;

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

        //���� ���� ��ġ
        saveFilePath = Application.persistentDataPath + "/EventSaveText.txt";
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

    //������ ����
    public void Save(int _slotNum)
    {
        Debug.Log("Save EventData");

        //������ ������ �ֱ�
        curEventSaveData = new EventSaveData(eventCheck.joomackBab, eventCheck.binyeo, 
            eventCheck.flower, eventCheck.muck, eventCheck.boridduck, eventProgress.deliveryMuck, eventEndCheck.muckEvent_End);

        //���̺� ������
        string jSaveData = JsonUtility.ToJson(curEventSaveData);

        //������ ���� ����
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);
    }
    
    //������ �ε�
    public void Load(int _SlotNum)
    {
        Debug.Log("Load EventLoadData");

        //���̺� ���� �о����
        string jLoadData = File.ReadAllText(saveFilePath + _SlotNum.ToString());

        //�о�� ���� ����Ʈ�� ����
        curEventLoadData = JsonUtility.FromJson<EventLoadData>(jLoadData);

        //�̺�Ʈ �߻� �ʱ�ȭ
        eventCheck.joomackBab = curEventLoadData.joomuckBob;
        eventCheck.binyeo = curEventLoadData.binyeo;
        eventCheck.boridduck = curEventLoadData.boridduck;
        eventCheck.flower = curEventLoadData.flower;
        eventCheck.muck = curEventLoadData.muck;

        //�̺�Ʈ ���� ��Ȳ �ʱ�ȭ
        eventProgress.deliveryMuck = curEventLoadData.deliveryMuck;

        //�̺�Ʈ �Ϸ� ��Ȳ �ʱ�ȭ
        eventEndCheck.muckEvent_End = curEventLoadData.muckEvent_End;
    }

}
