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
    flwoer,
    muck,
    boridduck
}


public class EventManager : MonoBehaviour
{
    //�̱��� ����
    public static EventManager instance = null;

    //�̺�Ʈ üũ
    public EventCheck eventCheck;

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
            case Events.flwoer:
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
