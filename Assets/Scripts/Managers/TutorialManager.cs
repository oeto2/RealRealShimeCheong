using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//������ Ʃ�丮�� ������
[System.Serializable]
public class TutorialSaveData
{
    //������
    public TutorialSaveData(int _tutoralEventNum, bool _getBotzime, bool _getMap, bool _isLightsOn)
    {
        tutoralEventNum = _tutoralEventNum;
        getBotzime = _getBotzime;
        getMap = _getMap;
        isLightsOn = _isLightsOn;
    }

    //Ʃ�丮�� �̺�Ʈ ��ȣ
    public int tutoralEventNum;
    //���� ȹ�� ����
    public bool getBotzime;
    //���� ȹ�� ����
    public bool getMap;
    //���� �״��� ���״���
    public bool isLightsOn;
}

//������ Ʃ�丮�� ������
[System.Serializable]
public class TutorialLoadData
{
    //������
    public TutorialLoadData(int _tutoralEventNum, bool _getBotzime, bool _getMap, bool _isLightsOn)
    {
        tutoralEventNum = _tutoralEventNum;
        getBotzime = _getBotzime;
        getMap = _getMap;
        isLightsOn = _isLightsOn;
    }

    //Ʃ�丮�� �̺�Ʈ ��ȣ
    public int tutoralEventNum;
    //���� ȹ�� ����
    public bool getBotzime;
    //���� ȹ�� ����
    public bool getMap;
    //���� �״��� ���״���
    public bool isLightsOn;
}

//Event
public enum TutorialEvents
{
    TurnOnLights = 0,
    GetItems = 1,
    TalkToHyang = 2,
    PassOneDay = 3,
    Done

}

public class TutorialManager : MonoBehaviour
{
    //�̱��� ����
    public static TutorialManager instance = null;

    //�ܺ� ��ũ��Ʈ
    public TurnOnLight turnOnLightScr;
    public ObjectManager objectManagerScr;
    public Controller playerCtrlScr;
    public Dialog_TypingWriter_ShimBongSa playerDialogueScr;
    public ObjectControll objCtrlScr;
    public TimeManager timeManagerScr;
    public GameManager gameManagerScr;
    public BoxAction boxActScr;
    public Dialog_TypingWriter_JangSeong dialogueHyangScr;
    public PlayerAnimation playerAnimationScr;

    //�����̼� ������Ʈ
    public GameObject gameObject_NarationBG;

    //UI Canvas
    public GameObject gameObject_UICanvas;

    //���̾�α� UI
    public GameObject gameObject_Dialogue;

    //Input Key Text Object
    public GameObject gameObject_InputKeyText;

    //���� �� ������Ʈ
    public GameObject gameObject_DeungJanLight;

    //��û���� �޸�
    public GameObject gameObject_shimeNote;


    //�����̼� BackGround Image
    public Image image_NarationBG;

    //�����̼� �ؽ�Ʈ
    public Text text_Naration;

    //�����̼ǹ�� �ִϸ�����
    public Animator animator_NarationBG;

    //��û�� �޸� ��������� : true, false
    private bool showNote;

    //��û�� �޸� ������
    [SerializeField]
    private bool closeNote;

    //ù��° ��ȭ ��
    public bool setence1End;

    //������Ʈ �Ѵ� ȹ��
    public bool getObjects;

    //�Ϸ簡 �������� Ȯ���ϴ� falg
    public bool passDay;

    //���� ��ȭ ��
    public bool SentenceEnd_Bbang;
    //�⸮�� ��ȭ ��
    public bool SentenceEnd_Hyang;
    //�⸮�� ��ȭ ������ �ɺ��� ��ȭ ��
    private bool SentenceEnd_HyangShim;

    //�帧 ����� Flags
    public bool HyangTalkEnd;
    private bool PassDayTalkEnd1;
    private bool PassDayTalkEnd2;
    private bool PassDayTalkEnd3;

    //������ Ʃ�丮�� ������ Ŭ����
    public TutorialSaveData curTutorialSaveData;

    //�ҷ��� Ʃ�丮�� ������ Ŭ����
    public TutorialLoadData curTutorialLoadData;

    private string saveFilePath;

    //Ʃ�丮�� �̺�Ʈ
    public TutorialEvents events = TutorialEvents.TurnOnLights;

    //Ʃ�丮�� �̺�Ʈ ��ȣ
    public int tutorialEventNum = 0;

    private void Awake()
    {
        instance = this.gameObject.GetComponent<TutorialManager>();

        //���̺� ������ ���� ��ġ
        saveFilePath = Application.persistentDataPath + "/TutorialDataText.txt";
    }

    // Update is called once per frame
    void Update()
    {
        #region Ʃ�丮�� Chaptor 1 End

        if (!setence1End)
        {
            //�����̽� �ٸ� ���� ���� â ����
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
            {
                //Player �̵� ���� ����
                playerCtrlScr.TalkEnd();

                //Input key Text OFF
                gameObject_InputKeyText.SetActive(false);

                //�ؽ�Ʈ â ����
                text_Naration.text = "";

                //������ �����̼� ��� FadeOut ����
                animator_NarationBG.SetBool("FadeOutStart", true);

                //�����̼� ��� ����
                Invoke("ActiveFalse_NarationBG", 1.5f);
            }

            //Evnet 0 : ���� Ű��
            //���� ���������
            if (turnOnLightScr.isTrunOnLight && !showNote && events == TutorialEvents.TurnOnLights)
            {
                Debug.Log("����");
                //1�ʵڿ� �޸� ����
                Invoke("ShowShimNote", 1f);

                //Player �̵�����
                playerCtrlScr.TalkStart();

                showNote = true;
            }

            //��Ʈ�� �а� �� �� Z or Space�� ������
            if (gameObject_shimeNote.activeSelf && events == TutorialEvents.TurnOnLights)
            {
                //�޸� ����
                if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space)) && !closeNote)
                {
                    //�޸� ����
                    gameObject_shimeNote.SetActive(false);

                    //1�� ��ȭ ����
                    playerDialogueScr.Start_Sentence1();

                    Invoke("CloseNote", 0.2f);

                    //���1 ����
                    setence1End = true;

                    //���� �̺�Ʈ ����
                    events = TutorialEvents.GetItems;
                    tutorialEventNum = 1;
                }
            }
        }
        #endregion

        #region ������Ʈ ȹ�� Ʃ�丮��

        if (setence1End && !getObjects && events == TutorialEvents.GetItems)
        {

            if (!gameObject_Dialogue.activeSelf && objCtrlScr.getMap && objCtrlScr.getBotzime)
            {
                //�Ѵ� ȹ�� ��ȭ ����
                playerDialogueScr.Start_Sentence_GetObjcets();

                getObjects = true;

                //���� �̺�Ʈ
                tutorialEventNum = 2;

                //�⸮�� ��ȭ�̺�Ʈ
                events = TutorialEvents.TalkToHyang;
            }
        }
        #endregion

        //Evnet 2 : �⸮��� ��ȭ����
        //�⸮�� ��ȭ�� ��� ������ Z Ű�� �������
        if (events == TutorialEvents.TalkToHyang && HyangTalkEnd && Input.GetKeyDown(KeyCode.Z))
        {
            //�̵����� ����
            playerCtrlScr.TalkEnd();

            //���̾�α� ����
            gameObject_Dialogue.SetActive(false);
            DialogManager.instance.Dialouge_System.SetActive(false);

            //�⸮�� SentenceEnd
            dialogueHyangScr.isSentenceEnd = true;

            //�ð� ����
            timeManagerScr.ResetTime();

            //��¥ UI �����ֱ�
            timeManagerScr.ShowDayUI();

            //�ð� �帣��
            timeManagerScr.ContinueTime();
            SentenceEnd_HyangShim = true;

            //���� �̺�Ʈ
            tutorialEventNum = 3;
            events = TutorialEvents.PassOneDay;
        }

        //Evnet 3 : �Ϸ縦 ������
        //�Ϸ簡 ������ ���
        if (passDay && !PassDayTalkEnd1)
        {
            //��ȭ ����
            playerCtrlScr.TalkStart();

            //�Ϸ� ������ ��ȭ 1
            playerDialogueScr.Start_Sentence_PassDay();

            PassDayTalkEnd1 = true;
        }

        if (playerDialogueScr.isTalkEnd && PassDayTalkEnd1 && !PassDayTalkEnd2 && Input.GetKeyDown(KeyCode.Z) && passDay)
        {
            //�Ϸ� ������ ��ȭ 2
            playerDialogueScr.Start_Sentence_PassDay2();

            PassDayTalkEnd2 = true;
        }

        if (playerDialogueScr.isTalkEnd && PassDayTalkEnd2 && !PassDayTalkEnd3 && Input.GetKeyDown(KeyCode.Z) && passDay)
        {
            //�Ϸ� ������ ��ȭ 3
            playerDialogueScr.Start_Sentence_PassDay3();

            PassDayTalkEnd3 = true;
        }

        if (playerDialogueScr.isTalkEnd && PassDayTalkEnd3 && Input.GetKeyDown(KeyCode.Z) && passDay && events == TutorialEvents.PassOneDay)
        {
            Debug.Log("Ʃ�丮�� ��");
            //��ȭ ��
            playerCtrlScr.TalkEnd();

            //���̾�α� â ����
            gameObject_Dialogue.SetActive(false);

            //�÷��� �ʱ�ȭ
            passDay = false;

            //�ð� �帣��
            timeManagerScr.ContinueTime();

            //���� �̺�Ʈ
            tutorialEventNum = 4;
            events = TutorialEvents.Done;
        }
    }

    //�Ϸ簡 ��������(TimeManager���� ����)
    public void PassDay()
    {
        //Player ��ġ �ʱ�ȭ
        gameManagerScr.ReturnPlayer();

        if(TimeManager.instance.int_DayCount == 2)
        {
            //Player ������ ����
            playerCtrlScr.TalkStart();

            //�ɺ��� �̵��� ���ʵڿ� ���̾�α� ������
            Invoke("PassDayTrue", 1.5f);
        }

        //��� ��� �ʱ�ȭ
        timeManagerScr.ResetBGColor();

        //����� ���� �÷��� ����
        timeManagerScr.curObjectRGB = timeManagerScr.startRGBValue;
    }

    //PassDay Flag Dealy��
    private void PassDayTrue()
    {
        passDay = true;
    }


    //�����̼� ��� ����
    private void ActiveFalse_NarationBG()
    {
        gameObject_NarationBG.SetActive(false);
    }

    //��û���� ���� �����ֱ�
    private void ShowShimNote()
    {
        gameObject_shimeNote.SetActive(true);
    }

    private void CloseNote()
    {
        closeNote = true;
    }

    //1�� ��ȭ�� ��������
    private void Sentence1End()
    {
        setence1End = true;
    }

    //Ʃ�丮�� ���� ��ȭ�� ��� ������ �Ǿ�����
    public void TutorialSenteceEnd_Bbang()
    {
        SentenceEnd_Bbang = true;
    }

    //Ʃ�丮�� �⸮���� ���ϴ°� ��� ��������
    public void TutorialSentenceEnd_Hyang()
    {
        SentenceEnd_Hyang = true;
    }

    //Save Data
    public void Save(int _slotNum)
    {
        Debug.Log("Save TutorialData");

        //������ ������ �ֱ�
        curTutorialSaveData = new TutorialSaveData(tutorialEventNum, objCtrlScr.getBotzime, objCtrlScr.getMap, turnOnLightScr.isLightsOn);

        //���̺� ������
        string jSaveData = JsonUtility.ToJson(curTutorialSaveData);

        //������ ���� ����
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);
    }

    //������ �ε�
    public void Load(int _slotNum)
    {
        if(_slotNum <= 2)
        {
            //���̺� ���� �о����
            string jLoadData = File.ReadAllText(saveFilePath + _slotNum.ToString());

            //�о�� ���� ����Ʈ�� ����
            curTutorialLoadData = JsonUtility.FromJson<TutorialLoadData>(jLoadData);

            //����� EventNum �ҷ�����
            tutorialEventNum = curTutorialLoadData.tutoralEventNum;

            //Event ���� ����
            EventsStateSetting();

            //Event ����
            EventSetting(curTutorialLoadData.getBotzime, curTutorialLoadData.getMap, curTutorialLoadData.isLightsOn);
        }

        Debug.Log("Load TutorialData");
    }

    //Events State Setting
    public void EventsStateSetting()
    {
        //(Enum)Events �� ����
        switch (tutorialEventNum)
        {
            case 0:
                events = TutorialEvents.TurnOnLights;
                break;
            case 1:
                events = TutorialEvents.GetItems;
                break;
            case 2:
                events = TutorialEvents.TalkToHyang;
                break;
            case 3:
                events = TutorialEvents.PassOneDay;
                break;
            case 4:
                events = TutorialEvents.Done;
                break;
        }
    }

    //�̺�Ʈ ����
    private void EventSetting(bool _getBotzime, bool _getMap, bool _isLightsOn)
    {
        switch (events)
        {
            //�̺�Ʈ 0 : ���� �Ѷ�
            case TutorialEvents.TurnOnLights:
                {
                    //Flag ����
                    showNote = false;
                    closeNote = false;
                    setence1End = false;
                    getObjects = false;
                    passDay = false;
                    SentenceEnd_Bbang = false;
                    SentenceEnd_Hyang = false;
                    SentenceEnd_HyangShim = false;
                    HyangTalkEnd = false;
                    PassDayTalkEnd1 = false;
                    PassDayTalkEnd2 = false;
                    PassDayTalkEnd3 = false;

                    //Player �⺻�ִϸ��̼����� ����
                    playerAnimationScr.ChangeAnimationNomal();
                    
                    //Object ����
                  
                    //���� ����
                    objCtrlScr.ResetBotzime();
                    //���� ����
                    objCtrlScr.ResetMap();

                    //UI ����
                    //UI Canvas ����
                    gameObject_UICanvas.SetActive(false);
                    //��¥ UI����
                    timeManagerScr.CloseDayUI();

                    //�÷��̾� �̵�����
                    playerCtrlScr.TalkEnd();
                    break;
                }

            //�̺�Ʈ 1 : �������� ȹ���ض�
            case TutorialEvents.GetItems:
                {
                    //Flag ����
                    showNote = true;
                    closeNote = true;
                    setence1End = true;
                    getObjects = false;
                    passDay = false;
                    SentenceEnd_Bbang = false;
                    SentenceEnd_Hyang = false;
                    SentenceEnd_HyangShim = false;
                    HyangTalkEnd = false;
                    PassDayTalkEnd1 = false;
                    PassDayTalkEnd2 = false;
                    PassDayTalkEnd3 = false;

                    //Object ����

                    //���ܺ��� �����־��ٸ�
                    if(_isLightsOn)
                    {
                        //���ܺ� �ѱ�
                        turnOnLightScr.TurnOnLights();
                    }

                    //���ܺ��� ���� �־��ٸ�
                    else if (!_isLightsOn)
                    {
                        //���ܺ� ����
                        turnOnLightScr.TurnOFFLights();
                    }

                    //������ ȹ�� �ߴٸ�
                    if (_getBotzime && !_getMap)
                    {
                        objCtrlScr.LoadBotzime();
                        objCtrlScr.ResetMap();
                        boxActScr.isFirst = true;

                        //Player ���� �ִϸ��̼����� ����
                        playerAnimationScr.ChangeAnimationBotzime();

                        //���� ���� �̹����� ����
                        boxActScr.spriteRender.sprite = boxActScr.sprite_Box[0];

                        //UI ����
                        //UI Canvas �ѱ�
                        gameObject_UICanvas.SetActive(true);
                        //��¥ UI����
                        timeManagerScr.CloseDayUI();
                        //�÷��̾� �̵�����
                        playerCtrlScr.TalkEnd();
                    }
                    //������ ȹ�� �ߴٸ�
                    else if (!_getBotzime && _getMap)
                    {
                        //Player �⺻�ִϸ��̼����� ����
                        playerAnimationScr.ChangeAnimationNomal();
                        objCtrlScr.LoadMap();
                        objCtrlScr.ResetBotzime();

                        //���� ���� �̹����� ����
                        boxActScr.spriteRender.sprite = boxActScr.sprite_Box[1];

                        //�ڽ� �÷��� �ʱ�ȭ
                        boxActScr.isFirst = false;

                        //UI ����
                        //UI Canvas �ѱ�
                        gameObject_UICanvas.SetActive(true);
                        //��¥ UI����
                        timeManagerScr.CloseDayUI();
                        //�÷��̾� �̵�����
                        playerCtrlScr.TalkEnd();
                    }
                    else
                    {
                        //Player �⺻�ִϸ��̼����� ����
                        playerAnimationScr.ChangeAnimationNomal();

                        //���� ����
                        objCtrlScr.ResetBotzime();
                        //���� ����
                        objCtrlScr.ResetMap();
                        boxActScr.isFirst = true;
                        //���� ���� �̹����� ����
                        boxActScr.spriteRender.sprite = boxActScr.sprite_Box[0];
                        //�÷��̾� �̵�����
                        playerCtrlScr.TalkEnd();

                        //UI Canvas ����
                        gameObject_UICanvas.SetActive(false);
                        //��¥ UI����
                        timeManagerScr.CloseDayUI();
                    }

                    break;
                }

            //�̺�Ʈ 2 : �⸮��� ��ȭ
            case TutorialEvents.TalkToHyang:
                {
                    //Flag ����
                    showNote = true;
                    closeNote = true;
                    setence1End = true;
                    getObjects = true;
                    passDay = false;
                    SentenceEnd_Bbang = true;
                    SentenceEnd_Hyang = false;
                    SentenceEnd_HyangShim = false;
                    HyangTalkEnd = false;
                    PassDayTalkEnd1 = false;
                    PassDayTalkEnd2 = false;
                    PassDayTalkEnd3 = false;

                    //Object ����

                    //���ܺ��� �����־��ٸ�
                    if (_isLightsOn)
                    {
                        //���ܺ� �ѱ�
                        turnOnLightScr.TurnOnLights();
                    }

                    //���ܺ��� ���� �־��ٸ�
                    else if (!_isLightsOn)
                    {
                        //���ܺ� ����
                        turnOnLightScr.TurnOFFLights();
                    }

                    //Player ���� �ִϸ��̼����� ����
                    playerAnimationScr.ChangeAnimationBotzime();
                    //���� ȹ��
                    objCtrlScr.LoadBotzime();
                    //���� ȹ��
                    objCtrlScr.LoadMap();
                    //�ڽ� �÷��� �ʱ�ȭ
                    boxActScr.isFirst = false;


                    //UI ����
                    //UI Canvas �ѱ�
                    gameObject_UICanvas.SetActive(true);
                    //��¥ UI����
                    timeManagerScr.CloseDayUI();
                    //�÷��̾� �̵�����
                    playerCtrlScr.TalkEnd();
                    break;
                }

            //�̺�Ʈ 3: �Ϸ縦 ������
            case TutorialEvents.PassOneDay:
                {
                    //Flag ����
                    showNote = true;
                    closeNote = true;
                    setence1End = true;
                    getObjects = true;
                    passDay = false;
                    SentenceEnd_Bbang = true;
                    SentenceEnd_Hyang = true;
                    SentenceEnd_HyangShim = true;
                    HyangTalkEnd = true;
                    PassDayTalkEnd1 = false;
                    PassDayTalkEnd2 = false;
                    PassDayTalkEnd3 = false;

                    //Object ����

                    //���ܺ��� �����־��ٸ�
                    if (_isLightsOn)
                    {
                        //���ܺ� �ѱ�
                        turnOnLightScr.TurnOnLights();
                    }

                    //���ܺ��� ���� �־��ٸ�
                    else if (!_isLightsOn)
                    {
                        //���ܺ� ����
                        turnOnLightScr.TurnOFFLights();
                    }

                    //Player ���� �ִϸ��̼����� ����
                    playerAnimationScr.ChangeAnimationBotzime();
                    //���� ȹ��
                    objCtrlScr.LoadBotzime();
                    //���� ȹ��
                    objCtrlScr.LoadMap();
                    //�ڽ� �÷��� �ʱ�ȭ
                    boxActScr.isFirst = false;

                    //UI ����
                    //UI Canvas �ѱ�
                    gameObject_UICanvas.SetActive(true);
                    //��¥ UI �ѱ�
                    timeManagerScr.ShowDayUI();
                    //�÷��̾� �̵�����
                    playerCtrlScr.TalkEnd();
                    //�ð� �帣��
                    timeManagerScr.ContinueTime();
                    break;
                }

            //�̺�Ʈ 4: �̺�Ʈ ��
            case TutorialEvents.Done:
                {
                    //Flag ����
                    showNote = true;
                    closeNote = true;
                    setence1End = true;
                    getObjects = true;
                    passDay = true;
                    SentenceEnd_Bbang = true;
                    SentenceEnd_Hyang = true;
                    SentenceEnd_HyangShim = true;
                    HyangTalkEnd = true;
                    PassDayTalkEnd1 = true;
                    PassDayTalkEnd2 = true;
                    PassDayTalkEnd3 = true;

                    //Object ����

                    //���ܺ��� �����־��ٸ�
                    if (_isLightsOn)
                    {
                        //���ܺ� �ѱ�
                        turnOnLightScr.TurnOnLights();
                    }

                    //���ܺ��� ���� �־��ٸ�
                    else if (!_isLightsOn)
                    {
                        //���ܺ� ����
                        turnOnLightScr.TurnOFFLights();
                    }

                    //Player ���� �ִϸ��̼����� ����
                    playerAnimationScr.ChangeAnimationBotzime();
                    //���� ȹ��
                    objCtrlScr.LoadBotzime();
                    //���� ȹ��
                    objCtrlScr.LoadMap();
                    //�ڽ� �÷��� �ʱ�ȭ
                    boxActScr.isFirst = false;


                    //UI ����
                    //UI Canvas �ѱ�
                    gameObject_UICanvas.SetActive(true);
                    //��¥ UI �ѱ�
                    timeManagerScr.ShowDayUI();
                    //�÷��̾� �̵�����
                    playerCtrlScr.TalkEnd();
                    //�ð� �帣��
                    timeManagerScr.ContinueTime();
                    break;
                }
        }
    }

    //��ȭ ������ ����
    public bool SentenceCondition()
    {
        if(events != TutorialEvents.TurnOnLights && events != TutorialEvents.GetItems && events != TutorialEvents.TalkToHyang)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
