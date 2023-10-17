using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

//���̺��� ������
public class EventSaveData
{
    //������
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

    //����� ���� ���� ����
    public bool boatManObject;

    //������� 6045��ȭ�� �����ߴ���
    public bool talkClue_6045;

    //���ʹ��� ���̴���
    public bool drinkHerb;
}

//�ε��� ������
public class EventLoadData
{
    //������
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

    //����� ���� ���� ����
    public bool boatManObject;

    //������� 6045��ȭ�� �����ߴ���
    public bool talkClue_6045;

    //���ʹ��� ���̴���
    public bool drinkHerb;
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

    //�̺�Ʈ
    Herb,
    Herb2
}


public class EventManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public Dialog_TypingWriter_BoatMan boatManDialogueScr;
    public Dialog_TypingWriter_BoatMan2 boatManDialogueScr2;
    public BoatMan3 boatManDialogueScr3;
    public Dialog_TypingWriter_Jangjieon jangjieonDialogueScr;
    public JoomuckBab joomuckBabScr;

    //�÷��̾� �� ��ũ��Ʈ
    public Light2D light2dScr;
    
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

    //������ 3�� Text
    public Text text_selectNum3;

    //�������� Ű��
    public int int_selectKeyNum;

    //����3 ������ Text UI
    public GameObject gameObject_BoatMan3Text;

    //���ʹ� ���ñ� ������ Text UI
    public GameObject gameObject_DrinkHerbText;

    //������ 2�� ������Ʈ
    public GameObject gameObject_SelectNum2;

    //������ 3�� ������Ʈ
    public GameObject gameObject_SelectNum3;

    //���ʸ� ���̴���
    public bool drinkHerb;


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
            , selectEndCheck.select2006_End, boatManDialogueScr.boatManObject, jangjieonDialogueScr.talkClue_6045, drinkHerb);

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

            //����� ���� �����Ȳ �ʱ�ȭ
            boatManDialogueScr.boatManObject = curEventLoadData.boatManObject;

            //������ ��ȭ �����Ȳ ����
            jangjieonDialogueScr.talkClue_6045 = curEventLoadData.talkClue_6045;

            //���ʹ� �÷��� �ʱ�ȭ
            drinkHerb = curEventLoadData.drinkHerb;

            //�÷��̾� �ӵ�, �þ� �ʱ�ȭ
            PlayerStateReset();

            //���� ���ʸ� ���� ���¶�� �̼�,�þ� 2��
            if (curEventLoadData.drinkHerb)
            {
                PlayerStateMultiple();

                //�÷��� �ʱ�ȭ
                joomuckBabScr.drinkHerb = true;
            }
        }
    }

    //������ ����
    public void SelectStart(NPCName _npcName, int _SelectNum)
    {
        Debug.Log("������ ����");

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

            //���� ������3
            case NPCName.boatman3:

                //������ ��� ���̱�
                gameObject_BoatMan3Text.SetActive(true);

                //NPC ���̾�α� ����
                DialogManager.instance.Dialouge_System.SetActive(false);

                //1�� ������ ��� �Է�
                text_selectNum1.text = "��";
                //2�� ������ ��� �Է�
                text_selectNum2.text = "�ƴϿ�";

                break;

            //�ɺ��� ������
            case NPCName.Shimbongsa:

                //NPC ���̾�α� ����
                DialogManager.instance.Dialouge_System.SetActive(false);

                //���� ������� 6045�� �ܼ� ��ȭ�� ���� �ʾ��� ���
                if (!jangjieonDialogueScr.talkClue_6045)
                {
                    //2�� ������ ��Ȱ��ȭ
                    gameObject_SelectNum2.SetActive(false);

                    //1�� ������ ��� �Է�
                    text_selectNum1.text = "���Ϸ� �پ���";
                }

                else
                {
                    //1�� ������ ��� �Է�
                    text_selectNum1.text = "���Ϸ� �پ���";

                    //2�� ������ ��� �Է�
                    text_selectNum2.text = "������ �ִ´�";
                }
                break;

            //�ɺ��� ������2
            case NPCName.Shimbongsa2:

                //NPC ���̾�α� ����
                DialogManager.instance.Dialouge_System.SetActive(false);

                //������ 3�� ���̱�
                gameObject_SelectNum3.SetActive(true);

                //1�� ������ ��� �Է�
                text_selectNum1.text = "�󸶳� �������پƴ���? ���� �̷� ���� ���� ���̾�!";

                //2�� ������ ��� �Է�
                text_selectNum2.text = "�̾��ϴ�.û��.";

                //3�� ������ ��� �Է�
                text_selectNum3.text = "(�ƹ����� ���� �ʴ´�.)";

                break;



            //���� ���ñ� ������
            case NPCName.Herb:

                //NPC ���̾�α� ����
                DialogManager.instance.Dialouge_System.SetActive(false);

                //���� ���ñ� Text ����
                gameObject_DrinkHerbText.SetActive(true);
                    
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
        //2�� ������ ���̱�
        gameObject_SelectNum2.SetActive(true);

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

                //����� ���� ����Ʈ�� �Ϸ����� ���ߴٸ�
                if (boatManDialogueScr.boatManObject == false)
                {
                    //������ â ����
                    gameObject_SelectUI.SetActive(false);

                    //������� ��忣��
                    boatManDialogueScr2.StartBoatManEnding_1();
                }

                //����� ���� ����Ʈ �Ϸ�
                else
                {
                    //û���� ���� �ܼ��� ���������� �ʴٸ�
                    if (!ObjectManager.instance.GetClue_Check(9000))
                    {
                        Debug.Log("û�� �ܼ� �̺��� ����");

                        //������ â ����
                        gameObject_SelectUI.SetActive(false);

                        //������� ��忣��
                        boatManDialogueScr2.StartBoatManEnding_1();
                    }

                    //û���� ���� �ܼ��� ���� ���̶��
                    else
                    {
                        //������ â ����
                        gameObject_SelectUI.SetActive(false);

                        //��/������ ��Ʈ ����
                        boatManDialogueScr2.StartGoodEndingRoot();
                    }
                }
                break;

            //����3 �������� ���
            case 7194:

                //���� �Ϸ�
                boatManDialogueScr3.isSelectDone = true;

                //������ ��� ����
                gameObject_BoatMan3Text.SetActive(false);

                //������ ����
                gameObject_SelectUI.SetActive(false);

                //���� ����
                boatManDialogueScr3.StartEndingSentence();

                break;

            //�ɺ��� �������� ���
            case 7287:
                //���̾�α� �÷��� �ʱ�ȭ
                jangjieonDialogueScr.remainSentence = true;
                jangjieonDialogueScr.isSentenceEnd = true;

                //���̾�α� ����
                DialogManager.instance.Dialouge_System.SetActive(true);

                //������ ����
                gameObject_SelectUI.SetActive(false);

                //���� ����
                jangjieonDialogueScr.StartGoodEnidng();
                break;

            //�ɺ���2 �������� ���
            case 7299:
                //���̾�α� �÷��� �ʱ�ȭ
                jangjieonDialogueScr.remainSentence = true;
                jangjieonDialogueScr.isSentenceEnd = true;

                //���̾�α� ����
                DialogManager.instance.Dialouge_System.SetActive(true);

                //������ ����
                gameObject_SelectUI.SetActive(false);

                //�¿��� ����
                jangjieonDialogueScr.StartGoodEnding_1();

                break;

            //���ʹ� ���ñ�1
            case 5799:
                
                //�̺�Ʈ ���� ��Ȳ ����
                joomuckBabScr.makeHerbOrder = JoomuckBab.MakeHerbOrder.DrinkHerb2;

                //������ ����
                gameObject_SelectUI.SetActive(false);

                //���ʹ� ����
                drinkHerb = true;

                //�̵��ӵ� 2�� ����, ȭ�� ��� ����
                PlayerStateMultiple();

                //��� ����
                DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(348), true);

                break;

            //���ʹ� ���ñ�2
            case 7009:

                //�̺�Ʈ ���� ��Ȳ ����
                joomuckBabScr.makeHerbOrder = JoomuckBab.MakeHerbOrder.Edning;

                //������ ����
                gameObject_SelectUI.SetActive(false);

                //���� ����
                Debug.Log("�����ұ� ����");
                DialogManager.instance.StartBadEndingSentence();

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

            //����3 �������� ���
            case 7194:

                //������ ��� ����
                gameObject_BoatMan3Text.SetActive(false);

                //������ ����
                gameObject_SelectUI.SetActive(false);

                //�÷��̾� ������ ���� ����
                Controller.instance.TalkEnd();
                break;

            //�ɺ��� �������� ���
            case 7287:
                //���̾�α� �÷��� �ʱ�ȭ
                jangjieonDialogueScr.remainSentence = true;
                jangjieonDialogueScr.isSentenceEnd = true;

                //���̾�α� ����
                DialogManager.instance.Dialouge_System.SetActive(true);

                //������ ����
                gameObject_SelectUI.SetActive(false);

                //���� ����
                jangjieonDialogueScr.StartRealEndingRoot();

                break;

            //�ɺ���2 �������� ���
            case 7299:
                //���̾�α� �÷��� �ʱ�ȭ
                jangjieonDialogueScr.remainSentence = true;
                jangjieonDialogueScr.isSentenceEnd = true;

                //���̾�α� ����
                DialogManager.instance.Dialouge_System.SetActive(true);

                //������ ����
                gameObject_SelectUI.SetActive(false);

                //���� ����
                jangjieonDialogueScr.StartGoodEnding_2();

                break;


            //���ʹ� ���ñ�1
            case 5799:

                //������ ����
                gameObject_SelectUI.SetActive(false);

                break;

            //���ʹ� ���ñ�2
            case 7009:

                //������ ����
                gameObject_SelectUI.SetActive(false);

                break;
        }
    }

    //������ 3�� ����
    public void SelectNum3_Click()
    {
        //������ ����
        GameManager.instance.isPlayerSelecting = false;

        //Ŀ�� ����
        UIManager.instance.BlindCursor();

        switch (int_selectKeyNum)
        {

            //�ɺ���2 �������� ���
            case 7299:
                //���̾�α� �÷��� �ʱ�ȭ
                jangjieonDialogueScr.remainSentence = true;
                jangjieonDialogueScr.isSentenceEnd = true;

                //���̾�α� ����
                DialogManager.instance.Dialouge_System.SetActive(true);

                //������ ����
                gameObject_SelectUI.SetActive(false);

                //�� ���� ����
                jangjieonDialogueScr.StartRealEnding();
                break;
        }
    }

    //�ӵ�, �þ� ����
    public void PlayerStateReset()
    {
        Controller.instance.moveSpeed = 10;
        light2dScr.pointLightOuterRadius = 6;
    }

    //�ӵ�, �þ� 2��
    public void PlayerStateMultiple()
    {
        Controller.instance.moveSpeed *= 2;
        light2dScr.pointLightOuterRadius *= 2;
    }
}

