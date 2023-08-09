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
public enum Events
{
    TurnOnLights = 0,
    GetItems = 1,
    TalkToBBangDuck = 2,
    TalkToHyang = 3,
    PassOneDay = 4,
    Done

}

public class TutorialManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ
    public TurnOnLight turnOnLightScr;
    public ObjectManager objectManagerScr;
    public Controller playerCtrlScr;
    public Dialog_TypingWriter_ShimBongSa playerDialogueScr;
    public ObjectControll objCtrlScr;
    public TimeManager timeManagerScr;
    public GameManager gameManagerScr;

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
    private bool BangtalkEnd;
    private bool HyangTalkEnd1;
    private bool HyangTalkEnd2;
    private bool HyangTalkEnd3;
    private bool PassDayTalkEnd1;
    private bool PassDayTalkEnd2;
    private bool PassDayTalkEnd3;

    //������ Ʃ�丮�� ������ Ŭ����
    public TutorialSaveData curTutorialSaveData;
    //�ҷ��� Ʃ�丮�� ������ Ŭ����
    public TutorialLoadData curTutorialLoadData;

    private string saveFilePath;

    //Ʃ�丮�� �̺�Ʈ
    public Events events = Events.TurnOnLights;

    //Ʃ�丮�� �̺�Ʈ ��ȣ
    public int tutorialEventNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        //���̺� ������ ���� ��ġ
        saveFilePath = Application.persistentDataPath + "/TutorialDataText.txt";
        //Ʃ�丮�� ���� �� ���� �۾���
        #region
        ////UI Canvas OFF
        //gameObject_UICanvas.SetActive(false);
        //Player �̵� ����
        playerCtrlScr.TalkStart();

        //�ð� ���߱�
        timeManagerScr.StopTime();
        #endregion
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
            if (turnOnLightScr.isTrunOnLight && !showNote && events == Events.TurnOnLights)
            {
                Debug.Log("����");
                //1�ʵڿ� �޸� ����
                Invoke("ShowShimNote", 1f);

                //Player �̵�����
                playerCtrlScr.TalkStart();

                showNote = true;

                
            }

            //��Ʈ�� �а� �� �� Z or Space�� ������
            if (gameObject_shimeNote.activeSelf)
            {
                //�޸� ����
                if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space)) && !closeNote)
                {
                    //Player �̵�����
                    playerCtrlScr.TalkStart();

                    //�޸� ����
                    gameObject_shimeNote.SetActive(false);

                    //1�� ��ȭ ����
                    playerDialogueScr.Start_Sentence1();

                    Invoke("CloseNote", 0.2f);
                }
            }

            //�̾ ��ȭ ����
            if (!setence1End && playerDialogueScr.isTalkEnd && closeNote && Input.GetKeyDown(KeyCode.Z))
            {
                //Player �̵�����
                playerCtrlScr.TalkStart();

                playerDialogueScr.Start_Sentence1_2();

                Invoke("Sentence1End", 0.2f);
            }
        }

        //2��° ��ȭ�� ������ ZŰ ������ ���̾�α�â ����
        if (setence1End && Input.GetKeyDown(KeyCode.Z) && !objCtrlScr.getBotzime && !objCtrlScr.getMap && playerDialogueScr.isTalkEnd)
        {
            //Player �̵����� ����
            playerCtrlScr.TalkEnd();

            //UI ĵ���� ���̱�
            gameObject_UICanvas.SetActive(true);


            gameObject_Dialogue.SetActive(false);

            //���� �̺�Ʈ
            tutorialEventNum = 1;
            events = Events.GetItems;
        }
        #endregion

        #region ������Ʈ ȹ�� Ʃ�丮��

        if (setence1End && !getObjects)
        {
            //Evnet 1 : ���� �Ǵ� ������ ȹ������
            //���� ȹ�� �� â����
            if (setence1End && Input.GetKeyDown(KeyCode.Z) && objCtrlScr.getBotzime && playerDialogueScr.isTalkEnd)
            {
                //Player �̵����� ����
                playerCtrlScr.TalkEnd();

                gameObject_Dialogue.SetActive(false);
            }

            //�� ȹ�� �� â����
            if (setence1End && Input.GetKeyDown(KeyCode.Z) && objCtrlScr.getMap && playerDialogueScr.isTalkEnd)
            {
                //Player �̵����� ����
                playerCtrlScr.TalkEnd();

                gameObject_Dialogue.SetActive(false);
            }

            if (!gameObject_Dialogue.activeSelf && objCtrlScr.getMap && objCtrlScr.getBotzime)
            {
                //�Ѵ� ȹ�� ��ȭ ����
                playerDialogueScr.Start_Sentence_GetObjcets();

                //Player �̵�����
                playerCtrlScr.TalkStart();

                getObjects = true;
            }
        }
        #endregion

        if (setence1End && getObjects && Input.GetKeyDown(KeyCode.Z) && playerDialogueScr.isTalkEnd && events == Events.GetItems)
        {
            //Player �̵����� ����
            playerCtrlScr.TalkEnd();

            gameObject_Dialogue.SetActive(false);

            //���� �̺�Ʈ
            tutorialEventNum = 2;
            events = Events.TalkToBBangDuck;
        }

        //Evnet 2 : ���� ��ذ� ��ȭ����
        //���� ����� ���� ������ ���
        if (setence1End && getObjects && SentenceEnd_Bbang && !BangtalkEnd)
        {
            Debug.Log("�����̾߱� �� ��ȭ ����");

            playerCtrlScr.TalkStart();

            playerDialogueScr.Start_Sentence_BbangEnd();

            BangtalkEnd = true;
        }

        //������ ���� ����� �Ǿ��ٸ� ZŰ�� ���� ���̾�α� ����
        if (playerDialogueScr.isTalkEnd && Input.GetKeyDown(KeyCode.Z) && BangtalkEnd && !SentenceEnd_Hyang && events == Events.TalkToBBangDuck)
        {
            playerCtrlScr.TalkEnd();

            gameObject_Dialogue.SetActive(false);

            //���� �̺�Ʈ
            tutorialEventNum = 3;
            events = Events.TalkToHyang;
        }

        //Evnet 3 : �⸮��� ��ȭ����
        //�⸮�� ��ȭ�� ��� ������ ���
        if (getObjects && SentenceEnd_Bbang && SentenceEnd_Hyang && !HyangTalkEnd1 && !HyangTalkEnd2)
        {
            playerCtrlScr.TalkStart();

            //�⸮�� 1����ȭ
            playerDialogueScr.Start_Sentence_HyangEnd1();

            HyangTalkEnd1 = true;

            
        }

        //1����ȭ�� ��� ������ ZŰ �������
        if (SentenceEnd_Hyang && HyangTalkEnd1 && playerDialogueScr.isTalkEnd && Input.GetKeyDown(KeyCode.Z) && !HyangTalkEnd2)
        {
            //�⸮�� 2�� ��ȭ
            playerDialogueScr.Start_Sentence_HyangEnd2();

            HyangTalkEnd2 = true;
        }

        //2����ȭ�� ��� ������ ZŰ�� �������
        if (playerDialogueScr.isTalkEnd && HyangTalkEnd2 && Input.GetKeyDown(KeyCode.Z) && !HyangTalkEnd3)
        {
            //�⸮�� 3�� ��ȭ
            playerDialogueScr.Start_Sentence_HyangEnd3();

            HyangTalkEnd3 = true;
        }

        //3����ȭ�� ��� ������ Z Ű�� �������
        if (playerDialogueScr.isTalkEnd && HyangTalkEnd1 && HyangTalkEnd3 && Input.GetKeyDown(KeyCode.Z) && !SentenceEnd_HyangShim && events == Events.TalkToHyang)
        {
            //��ȭ ��
            playerCtrlScr.TalkEnd();

            //���̾�α� ����
            gameObject_Dialogue.SetActive(false);

            //�ð� ����
            timeManagerScr.ResetTime();

            //��¥ UI �����ֱ�
            timeManagerScr.ShowDayUI();

            //�ð� �帣��
            timeManagerScr.ContinueTime();

            SentenceEnd_HyangShim = true;

            //���� �̺�Ʈ
            tutorialEventNum = 4;
            events = Events.PassOneDay;
        }

        //Evnet 4 : �Ϸ縦 ������
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

        if (playerDialogueScr.isTalkEnd && PassDayTalkEnd3 && Input.GetKeyDown(KeyCode.Z) && passDay && events == Events.PassOneDay)
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
            tutorialEventNum = 5;
            events = Events.Done;
        }
    }

    //�Ϸ簡 ��������(TimeManager���� ����)
    public void PassDay()
    {
        //Player ��ġ �ʱ�ȭ
        gameManagerScr.ReturnPlayer();

        //Player ������ ����
        playerCtrlScr.TalkStart();

        //�ɺ��� �̵��� ���ʵڿ� ���̾�α� ������
        Invoke("PassDayTrue", 1.5f);

        
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
        Debug.Log("Load TutorialData");

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

    //Events State Setting
    public void EventsStateSetting()
    {
        //(Enum)Events �� ����
        switch (tutorialEventNum)
        {
            case 0:
                events = Events.TurnOnLights;
                break;
            case 1:
                events = Events.GetItems;
                break;
            case 2:
                events = Events.TalkToBBangDuck;
                break;
            case 3:
                events = Events.TalkToHyang;
                break;
            case 4:
                events = Events.PassOneDay;
                break;
            case 5:
                events = Events.Done;
                break;
        }
    }

    //�̺�Ʈ ����
    private void EventSetting(bool _getBotzime, bool _getMap, bool _isLightsOn)
    {
        switch (events)
        {
            //�̺�Ʈ 0 : ���� �Ѷ�
            case Events.TurnOnLights:
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
                    BangtalkEnd = false;
                    HyangTalkEnd1 = false;
                    HyangTalkEnd2 = false;
                    HyangTalkEnd3 = false;
                    PassDayTalkEnd1 = false;
                    PassDayTalkEnd2 = false;
                    PassDayTalkEnd3 = false;

                    //Object ����

                    //���ܺ� ����
                    turnOnLightScr.TurnOFFLights();
                    //���� ����
                    objCtrlScr.ResetBotzime();
                    //���� ����
                    objCtrlScr.ResetMap();

                    //UI ����
                    //UI Canvas ����
                    gameObject_UICanvas.SetActive(false);
                    //��¥ UI����
                    timeManagerScr.CloseDayUI();
                    //�ð� �帣��
                    

                    //�÷��̾� �̵�����
                    playerCtrlScr.TalkEnd();
                    break;
                }

            //�̺�Ʈ 1 : �������� ȹ���ض�
            case Events.GetItems:
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
                    BangtalkEnd = false;
                    HyangTalkEnd1 = false;
                    HyangTalkEnd2 = false;
                    HyangTalkEnd3 = false;
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
                    }
                    //������ ȹ�� �ߴٸ�
                    else if (!_getBotzime && _getMap)
                    {
                        objCtrlScr.LoadMap();
                        objCtrlScr.ResetBotzime();
                    }
                    else
                    {
                        //���� ����
                        objCtrlScr.ResetBotzime();
                        //���� ����
                        objCtrlScr.ResetMap();
                    }

                    //UI ����
                    //UI Canvas �ѱ�
                    gameObject_UICanvas.SetActive(true);
                    //��¥ UI����
                    timeManagerScr.CloseDayUI();
                    //�÷��̾� �̵�����
                    playerCtrlScr.TalkEnd();
                    break;
                }

            //�̺�Ʈ 2 : ������ ��ȭ
            case Events.TalkToBBangDuck:
                {
                    //Flag ����
                    showNote = true;
                    closeNote = true;
                    setence1End = true;
                    getObjects = true;
                    passDay = false;
                    SentenceEnd_Bbang = false;
                    SentenceEnd_Hyang = false;
                    SentenceEnd_HyangShim = false;
                    BangtalkEnd = false;
                    HyangTalkEnd1 = false;
                    HyangTalkEnd2 = false;
                    HyangTalkEnd3 = false;
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

                    //���� ȹ��
                    objCtrlScr.LoadBotzime();
                    //���� ȹ��
                    objCtrlScr.LoadMap();

                    //UI ����
                    //UI Canvas �ѱ�
                    gameObject_UICanvas.SetActive(true);
                    //��¥ UI����
                    timeManagerScr.CloseDayUI();
                    //�÷��̾� �̵�����
                    playerCtrlScr.TalkEnd();
                    break;
                }

            //�̺�Ʈ 3 : �⸮��� ��ȭ
            case Events.TalkToHyang:
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
                    BangtalkEnd = true;
                    HyangTalkEnd1 = false;
                    HyangTalkEnd2 = false;
                    HyangTalkEnd3 = false;
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

                    //���� ȹ��
                    objCtrlScr.LoadBotzime();
                    //���� ȹ��
                    objCtrlScr.LoadMap();

                    //UI ����
                    //UI Canvas �ѱ�
                    gameObject_UICanvas.SetActive(true);
                    //��¥ UI����
                    timeManagerScr.CloseDayUI();
                    //�÷��̾� �̵�����
                    playerCtrlScr.TalkEnd();
                    break;
                }

            //�̺�Ʈ 4 : �Ϸ縦 ������
            case Events.PassOneDay:
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
                    BangtalkEnd = true;
                    HyangTalkEnd1 = true;
                    HyangTalkEnd2 = true;
                    HyangTalkEnd3 = true;
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

                    //���� ȹ��
                    objCtrlScr.LoadBotzime();
                    //���� ȹ��
                    objCtrlScr.LoadMap();

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

            //�̺�Ʈ 5 : �̺�Ʈ ��
            case Events.Done:
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
                    BangtalkEnd = true;
                    HyangTalkEnd1 = true;
                    HyangTalkEnd2 = true;
                    HyangTalkEnd3 = true;
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

                    //���� ȹ��
                    objCtrlScr.LoadBotzime();
                    //���� ȹ��
                    objCtrlScr.LoadMap();

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
}
