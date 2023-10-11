using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//���̺��� ������
public class EventSaveData
{
    //������
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

    //�̺�Ʈ Ȱ��ȭ ����
    public bool joomuckBob;
    public bool binyeo;
    public bool flower;
    public bool muck;
    public bool boridduck;
    //3�� ������ �̺�Ʈ
    public bool day15ClueTalk;


    //���� ���� �ߴ��� ����
    public bool deliveryMuck;
    //���� ���� �ߴ��� ����
    public bool giveFlower;

    //�̺�Ʈ �Ϸ� ����
    public bool muckEvent_End;
    public bool joomackPuzzle_End;
    public bool day15ClueGet;
    public bool giveBoridduck_End;

    //������ �Ϸ� ����
    public bool select2006_End;
}

//�ε��� ������
public class EventLoadData
{
    //������
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

    //�̺�Ʈ Ȱ��ȭ ����
    public bool joomuckBob;
    public bool binyeo;
    public bool flower;
    public bool muck;
    public bool boridduck;
    //3�� ������ �̺�Ʈ
    public bool day15ClueTalk;

    //���� ���� �ߴ��� ����
    public bool deliveryMuck;
    //���� ���� �ߴ��� ����
    public bool giveFlower;

    //�̺�Ʈ �Ϸ� ����
    public bool muckEvent_End;
    public bool joomackPuzzle_End;
    public bool day15ClueGet;
    public bool giveBoridduck_End;

    //������ �Ϸ� ����
    public bool select2006_End;
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
    //�ָ� ������ Ŭ���� �ߴ��� ����
    public bool joomackPuzzle_Clear;
    //���� �����ߴ��� ����
    public bool giveFlowerEnd;
    //3�� ������ �ܼ� ��ȭ�� �����ߴ��� ����
    public bool day15ClueStart;
}

//�̺�Ʈ�� �������ߴ��� üũ
[System.Serializable]
public class EventEndCheck
{
    //�� �̺�Ʈ�� �Ϸ��ߴ���
    public bool muckEvent_End;
    //������ ������ �Ϸ�������
    public bool giveBoridduck_End;
    //3�� ������ �ܼ��� ȹ�� �ߴ��� ����
    public bool day15ClueGet;
}

//�������� �������ߴ��� üũ
[System.Serializable]
public class SelectEndCheck
{
    //�۳��� ���ΰ� û�� �������� �Ϸ� �ߴ���
    public bool select2006_End;
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

//NPC �̸���
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
    //�ܺ� ��ũ��Ʈ ����
    public Dialog_TypingWriter_BoatMan boatManDialogueScr;
    public Dialog_TypingWriter_BoatMan2 boatManDialogueScr2;

    //�̱��� ����
    public static EventManager instance = null;

    //�̺�Ʈ üũ
    public EventCheck eventCheck;

    //�̺�Ʈ ���� üũ
    public EventProgress eventProgress;

    //�̺�Ʈ �Ϸ� üũ
    public EventEndCheck eventEndCheck;

    //������ �Ϸ� üũ
    public SelectEndCheck selectEndCheck;

    //�̺�Ʈ ����Ʈ
    public List<EventCheck> eventList;

    //������ ������ Ŭ����
    public EventSaveData curEventSaveData;

    //���� ���� ��ġ
    public string saveFilePath;

    //�ҷ��� ������ Ŭ����
    public EventLoadData curEventLoadData;

    //������ UI
    public GameObject gameObject_SelectUI;

    //�������� ����� NPC �̸�
    public NPCName selectNPCName = NPCName.NONE;

    //������ 1�� Text
    public Text text_selectNum1;

    //������ 2�� Text
    public Text text_selectNum2;

    //�������� Ű��
    public int int_selectKeyNum;

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


    //�̺�Ʈ Ȱ��ȭ �޼���
    public void EventActive(Events _eventName)
    {
        switch (_eventName)
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
            eventCheck.flower, eventCheck.muck, eventCheck.boridduck, eventProgress.deliveryMuck, eventEndCheck.muckEvent_End
            , eventProgress.joomackPuzzle_Clear, eventProgress.giveFlowerEnd, eventProgress.day15ClueStart, eventEndCheck.day15ClueGet, eventEndCheck.giveBoridduck_End
            , selectEndCheck.select2006_End);

        //���̺� ������
        string jSaveData = JsonUtility.ToJson(curEventSaveData);

        //������ ���� ����
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);
    }

    //������ �ε�
    public void Load(int _SlotNum)
    {
        if (_SlotNum <= 2)
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
            eventProgress.joomackPuzzle_Clear = curEventLoadData.joomackPuzzle_End;
            eventProgress.giveFlowerEnd = curEventLoadData.giveFlower;

            //�̺�Ʈ �Ϸ� ��Ȳ �ʱ�ȭ
            eventEndCheck.muckEvent_End = curEventLoadData.muckEvent_End;
        }
    }

    //������ ����
    public void SelectStart(NPCName _npcName, int _SelectNum)
    {
        //������ ����
        GameManager.instance.isPlayerSelecting = true;

        //������ Ű�� ����
        int_selectKeyNum = _SelectNum;

        //������ UI ����
        gameObject_SelectUI.SetActive(true);

        //���콺 UI ���̱�
        UIManager.instance.ShowCursor();

        switch (_npcName)
        {
            case NPCName.NONE:
                //������ ����
                gameObject_SelectUI.SetActive(false);
                break;

            //���� ������
            case NPCName.boatman:

                //�۳��� ���ΰ� û�� �������� ���
                if (ObjectManager.instance.GetEquipObjectKey() == 2006)
                {
                    //NPC ���̾�α� ����
                    DialogManager.instance.Dialouge_System.SetActive(false);

                    //1�� ������ ��� �Է�
                    text_selectNum1.text = "���� û�� �ƺ� �Ǵ� ����̿�. �����ϰ� �����ֽÿ�.";
                    //2�� ������ ��� �Է�
                    text_selectNum2.text = "���� �� �̾߱��� �����. �� ������� �ʹ��ϴ��� ���̿�!";
                }
                break;

            //���� ������2
            case NPCName.boatman2:

                //NPC ���̾�α� ����
                DialogManager.instance.Dialouge_System.SetActive(false);

                //1�� ������ ��� �Է�
                text_selectNum1.text = "��";
                //2�� ������ ��� �Է�
                text_selectNum2.text = "�ƴϿ�";

                break;
        }
    }

    //������ 1�� ����
    public void SelectNum1_Click()
    {
        //������ ����
        GameManager.instance.isPlayerSelecting = false;

        //Ŀ�� ����
        UIManager.instance.BlindCursor();

        switch (int_selectKeyNum)
        {
            //�۳��� ���ΰ� û�� �������� ���
            case 2006:
                //������ ����
                gameObject_SelectUI.SetActive(false);

                //������ 1�� ����
                boatManDialogueScr.int_Select2006Num = 1;

                //���̾�α� Ȱ��ȭ
                DialogManager.instance.Dialouge_System.SetActive(true);

                //��� ���
                boatManDialogueScr.PrintSelect2006_Sentence1();

                //������ �Ϸ�
                selectEndCheck.select2006_End = true;
                break;

            //����2 �������� ���
            case 7355:

                //�ش� �ܼ��� �������̶��
                if(ObjectManager.instance.GetClue_Check(2000))
                {
                }
                break;
        }
    }

    //������ 2�� ����
    public void SelectNum2_Click()
    {
        //������ ����
        GameManager.instance.isPlayerSelecting = false;

        //Ŀ�� ����
        UIManager.instance.BlindCursor();

        switch (int_selectKeyNum)
        {
            //�۳��� ���ΰ� û�� �������� ���
            case 2006:

                //������ ����
                gameObject_SelectUI.SetActive(false);

                //������ 2�� ����
                boatManDialogueScr.int_Select2006Num = 2;

                //������ ��¥ �Ѱ��ֱ�
                boatManDialogueScr.int_select2006Day = TimeManager.instance.int_DayCount;

                //���̾�α� Ȱ��ȭ
                DialogManager.instance.Dialouge_System.SetActive(true);

                //��� ���
                boatManDialogueScr.PrintSelect2006_Sentence2();

                //������ �Ϸ�
                selectEndCheck.select2006_End = true;
                break;

            //����2 �������� ���
            case 7355:

                //������ ����
                gameObject_SelectUI.SetActive(false);

                //�÷��̾� ������ ���� ����
                Controller.instance.TalkEnd();

                //���� ��ȭ ���� �÷��� �ʱ�ȭ
                boatManDialogueScr2.isSentenceEnd = true;
                boatManDialogueScr2.remainSentence = true;

                break;
        }
    }
}
