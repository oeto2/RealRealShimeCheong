using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//������ UI �����͵�
[System.Serializable]
public class SaveUiData
{
    //������
    public SaveUiData(string _placeName, int _day, string _playTimeText, float _playTimeSec, int _sunClockNum)
    {
        placeName = _placeName;
        day = _day;
        playTimeText = _playTimeText;
        playTimeSec = _playTimeSec;
        sunClockNum = _sunClockNum;
    }

    //��� �̸�
    public string placeName;

    //��¥
    public int day;

    //�÷��� Ÿ�� �ؽ�Ʈ
    public string playTimeText;

    //�÷��� Ÿ�� �ð�
    public float playTimeSec;

    //�ؽð� �̹��� ��ȣ
    public int sunClockNum;
}

//�ҷ��� UI �����͵�
public class LoadUiData
{
    //������
    public LoadUiData(string _placeName, int _day, string _playTimeText, float _playTimeSec, int _sunClockNum)
    {
        placeName = _placeName;
        day = _day;
        playTimeText = _playTimeText;
        playTimeSec = _playTimeSec;
        sunClockNum = _sunClockNum;
    }

    //��� �̸�
    public string placeName;

    //��¥
    public int day;

    //�÷��� Ÿ�� �ؽ�Ʈ
    public string playTimeText;

    //�÷��� Ÿ�� �ð�
    public float playTimeSec;

    //�ؽð� �̹��� ��ȣ
    public int sunClockNum;
}

public class UIManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ
    public CameraMove cameraMoveScr;
    public GameManager gameManagerScr;
    public TimeManager timeManagerScr;
    public Controller playerCtrlScr;
    public EffectSoundManager effectSoundManagerScr;
    public PinAction pinActionScr;
    public CursorCtrl cursorCtrlScr;
    public ObjectControll objectCtrlScr;

    //������â �������̽� ������Ʈ
    public GameObject gameObject_ItemWindow;
    //���� ������Ʈ
    public GameObject gameObject_MapWindow;
    //�ɼ�â ������Ʈ
    public GameObject gameObject_Option;
    //����â ������Ʈ
    public GameObject gameObject_CombineWindow;
    //���̺�â ������Ʈ
    public GameObject gameObject_SaveWindow;
    //�ε�â ������Ʈ
    public GameObject gameObject_LoadWindow;
    //���̺� üũ ������Ʈ
    public GameObject gameObject_SaveCheckWindow;

    //������ â�� ���������� Ȯ���ϴ� flag
    public bool isItemWindowLaunch;
    //������ ���������� Ȯ�� �ϴ� flag
    public bool isMapWindowLaunch;
    //�ɼ�â�� ���������� Ȯ���ϴ� flag
    public bool isOptionLaunch;
    //����â�� ���������� Ȯ���ϴ� flag
    public bool isCombineLaunch;
    //���콺�� �������� Ȯ���ϴ� falg
    public bool isMonuseOn;

    //�� ��ư�� ���� ����
    private Color originColor = new Color32(255, 255, 255, 255);

    //�� ��ư�� ��Ȱ��ȭ ����
    private Color falseColor = new Color32(170, 170, 170, 255);

    //Itme Tap Button Image
    public Image itemTapImage;
    public Image itemTapImage2;


    //Clue Tap Button Image
    public Image clueTapImage;
    public Image clueTapImage2;


    //Save Slot Place Name text
    public Text[] text_SavePlaceName;
    //Load Slot Place Name Text
    public Text[] text_LoadPlaceName;

    //Save Slot DayCount text
    public Text[] text_SaveDayCount;
    //Load Slot DayCount Text
    public Text[] text_LoadDayCount;

    //Save Slot PlayTime text
    public Text[] text_SavePlayTime;
    //Load Slot PlayTime text
    public Text[] text_LoadPlayTime;

    //Save Slot SunClock Image
    public Image[] image_SaveSunClock;
    //Load Slot SunClock Image
    public Image[] image_LoadSunClock;

    //������ UI������ Ŭ����
    public SaveUiData curSaveUIData;

    //���� ���� ��ġ
    private string saveFilePath;

    //������ �� ����
    public int totalSlotNum;

    //�ε��� �����͸� �޾ƿ� Ŭ����
    public LoadUiData curLoadUiData;

    //�÷��� Ÿ�Ӱ��� �޾ƿ� Ŭ����
    public LoadUiData curLoadUiData2;

    //���� �ؽð� �̹���
    public Image image_CurSunClock;

    //���� �ؽð� ��������Ʈ
    public Sprite sprite_CurSundClock;

    //�ؽð� ��������Ʈ ��� �̹�����
    public Sprite[] sprite_AllSunClock;

    //�ؽð� �̹��� ��ȣ
    public int int_SunClockNum = 0;

    //Save Slot UI Ķ���� �̹���
    public Image[] image_SaveUICalendar;

    //Load Slot UI Ķ���� �̹���
    public Image[] image_LoadUICalendar;

    //Ķ���� ��������Ʈ ����
    public Sprite[] sprite_AllCalendar;

    //Ŀ�� �Һ� ������Ʈ
    public GameObject gameObjcet_CursorLights;

    private void Awake()
    {
        //���� ���� ��ġ
        saveFilePath = Application.persistentDataPath + "/UiDataText.txt";

        totalSlotNum = text_LoadPlaceName.Length;
    }

    private void Start()
    {
        //���Կ� UI ������ �����ֱ�
        ShowUiDataToSlot();
    }
    // Update is called once per frame
    void Update()
    {
        //������ â ���� �ڵ�
        #region
        //������ â�� �Ѵ� ����
        if (Input.GetKeyDown(KeyCode.X) && !gameObject_ItemWindow.activeSelf && !isMapWindowLaunch && !isOptionLaunch && 
            !isCombineLaunch && !playerCtrlScr.isTalk && objectCtrlScr.getBotzime)
        {
            //������ â ����
            ItemWindowLaunch();

            //Ŀ�� �̹��� ����
            cursorCtrlScr.ChangeCursor(cursorCtrlScr.sprite_idle);
        }

        //������ â Ȱ��ȭ ���¿��� XŰ or ESC�� ���� ���
        else if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape)) && gameObject_ItemWindow.activeSelf)
        {
            //������ â ����
            ItemWindowExit();
        }

        //������ â�� �������� ���
        if (gameObject_ItemWindow.activeSelf)
        {
            isItemWindowLaunch = true;
        }

        //������ â�� ��Ȱ��ȭ�� ���
        if (!gameObject_ItemWindow.activeSelf)
        {
            //isMapWindowLaunch = false
            Invoke("itemFalgFalse", 0.2f);
        }
        #endregion

        //�� ���� �ڵ�
        #region
        //������ ��ġ�� ����
        if (Input.GetKeyDown(KeyCode.M) && !gameObject_MapWindow.activeSelf && !isItemWindowLaunch && !isOptionLaunch &&
            !isCombineLaunch && !playerCtrlScr.isTalk && objectCtrlScr.getMap)
        {
            //���� ������Ʈ Ȱ��ȭ
            gameObject_MapWindow.SetActive(true);

            //������ Pin��ġ �� ����
            pinActionScr.PinPosChange(gameManagerScr.int_PinPosNum);

            //ȿ���� ���
            effectSoundManagerScr.PlayOpenMapSound();

            //Ŀ�� �̹��� ����
            cursorCtrlScr.ChangeCursor(cursorCtrlScr.sprite_idle);
        }

        //������ �������ε� MŰ or ESCŰ�� ���������
        else if ((Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape)) && gameObject_MapWindow.activeSelf)
        {
            //���� ������Ʈ ��Ȱ��ȭ
            gameObject_MapWindow.SetActive(false);
        }

        //������ �������� ���
        if (gameObject_MapWindow.activeSelf)
        {
            isMapWindowLaunch = true;
        }
        //������ �������� �ƴҰ��
        if (!gameObject_MapWindow.activeSelf)
        {
            //isMapWindowLaunch = false
            Invoke("MapFalgFalse", 0.2f);
        }
        #endregion

        //�ɼ� ���� �ڵ�
        #region

        //�ɼ�â�� ���� ����
        if (!isOptionLaunch && !isItemWindowLaunch && !isMapWindowLaunch && !isCombineLaunch && Input.GetKeyDown(KeyCode.Escape) && !playerCtrlScr.isTalk)
        {
            //�ɼ�â �����ֱ�
            gameObject_Option.SetActive(true);

            //Ŀ�� �̹��� ����
            cursorCtrlScr.ChangeCursor(cursorCtrlScr.sprite_idle);

            //�÷��̾� �̵�����
            playerCtrlScr.PlayerMoveStop();
        }

        //�ɼ�â�� �������϶� ESC�� ������ ���
        else if (isOptionLaunch && Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject_Option.SetActive(false);

            //�÷��̾� �̵����� ����
            playerCtrlScr.PlayerMoveStart();
        }

        //�ɼ�â�� �������ϰ��
        if (gameObject_Option.activeSelf)
        {
            isOptionLaunch = true;
        }

        //�ɼ�â�� ���������� �������
        else if (!gameObject_Option.activeSelf)
        {
            isOptionLaunch = false;
        }

        //���̺�â�� �������϶� ESC
        if (gameObject_SaveWindow.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            //���̺�â ����
            gameObject_SaveWindow.SetActive(false);
            //�ɼ�â �ѱ�
            gameObject_Option.SetActive(true);
        }

        //�ε�â�� �������϶� ESC
        if (gameObject_LoadWindow.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            //�ε�â ����
            gameObject_LoadWindow.SetActive(false);
            //�ɼ�â �ѱ�
            gameObject_Option.SetActive(true);
        }
        #endregion

        //����â ���� �ڵ�
        #region
        //����â�� ���� ����
        if (!gameObject_CombineWindow.activeSelf && !playerCtrlScr.isTalk && Input.GetKeyDown(KeyCode.Z))
        {
            //Ŀ�� �̹��� ����
            cursorCtrlScr.ChangeCursor(cursorCtrlScr.sprite_idle);
        }

        ////����â�� ���������� �ʴٸ�
        //if (!gameObject_CombineWindow.activeSelf)
        //{
        //    Invoke("CombineFalgFalse", 0.2f);
        //}

        //����â�� �������̰� ESCŰ�� ���������
        if (isCombineLaunch && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("����â �ݱ�");
            CombineWindowExit();
        }
        #endregion
    }

    //������,����,�ܼ�â ���� �ѱ�
    #region

    //������ â ����
    public void ItemWindowLaunch()
    {
        //������ â ������Ʈ Ȱ��ȭ
        gameObject_ItemWindow.SetActive(true);
    }

    //������ â ����
    public void ItemWindowExit()
    {
        //������ â ������Ʈ ��Ȱ��ȭ
        gameObject_ItemWindow.SetActive(false);
    }

    //���� �ѱ�
    public void MapWindowLaunch()
    {
        gameObject_MapWindow.SetActive(true);
    }

    //���� ����
    public void MapWindowExit()
    {
        gameObject_MapWindow.SetActive(false);
    }

    //�ɼ�â ����
    public void OptionExit()
    {
        //�÷��̾� �̵����� ����
        playerCtrlScr.PlayerMoveStart();

        gameObject_Option.SetActive(false);
    }

    //����â �ѱ�
    public void CombineWindowLaunch()
    {
        if (!isOptionLaunch && !isItemWindowLaunch && !isMapWindowLaunch)
        {
            isCombineLaunch = true;

            //�ð� ����
            TimeManager.instance.StopTime();
            gameObject_CombineWindow.SetActive(true);

            //�÷��̾� �̵�����
            playerCtrlScr.PlayerMoveStop();
        }
    }

    //����â ����
    public void CombineWindowExit()
    {
        isCombineLaunch = false;

        //�ð� �帣��
        TimeManager.instance.ContinueTime();
        gameObject_CombineWindow.SetActive(false);

        //�÷��̾� �̵����� ����
        playerCtrlScr.PlayerMoveStart();
    }
    #endregion

    //Falg ������
    #region
    //isItemWindowLaunch = false;
    private void itemFalgFalse()
    {
        isItemWindowLaunch = false;
    }

    //isMapWindowLaunch = false;
    private void MapFalgFalse()
    {
        isMapWindowLaunch = false;
    }

    //isCombineLaunch = false;
    private void CombineFalgFalse()
    {
        isCombineLaunch = false;
    }
    #endregion

    //������Ʈ ���� ����
    #region
    //������ �� ��ư ���� ����
    public void ChangeItemTapColor()
    {
        itemTapImage.color = falseColor;
        clueTapImage.color = originColor;
    }

    //���� ������ �� ��ư ���� ����
    public void ChangeCombineItemTapColor()
    {
        itemTapImage2.color = falseColor;
        clueTapImage2.color = originColor;
    }

    //�ܼ� �� ��ư ���� ����
    public void ChangeClueTapColor()
    {
        clueTapImage.color = falseColor;
        itemTapImage.color = originColor;
    }

    //���� �ܼ� �� ��ư ���� ����
    public void ChangeCombineClueTapColor()
    {
        clueTapImage2.color = falseColor;
        itemTapImage2.color = originColor;
    }
    #endregion

    //�ɼ�â ����
    #region

    //���� ���� ��ư
    public void ExitButton()
    {
        //����
        Application.Quit();
    }

    //���̺�â ����
    public void ShowSaveWindow()
    {
        //���̺�â ����
        gameObject_SaveWindow.SetActive(true);
        //�ɼ�â ����
        gameObject_Option.SetActive(false);
    }

    //�ε�â ����
    public void ShowLoadWindow()
    {
        //�ε�â ����
        gameObject_LoadWindow.SetActive(true);
        //�ɼ�â ����
        gameObject_Option.SetActive(false);
    }

    //���̺�â ����
    public void ExitSaveWindow()
    {
        //���̺�â ����
        gameObject_SaveWindow.SetActive(false);
        //�ɼ�â ����
        gameObject_Option.SetActive(true);
    }

    //�ε�â ����
    public void ExitLoadWindow()
    {
        //�ε�â ����
        gameObject_LoadWindow.SetActive(false);
        //�ɼ�â ����
        gameObject_Option.SetActive(true);
    }

    #endregion


    //UI ������ �����ϱ�
    public void Save(int _slotNum)
    {
        //Debug.Log("Save UIManagerData");

        //���� �ؽð� sprite���ϱ�
        GetCurSunClockSprite();

        //�ؽð� �̹��� ��ȣ ���ϱ�
        GetSunClockNum();

        //������ ������ �ֱ�
        curSaveUIData = new SaveUiData(gameManagerScr.GetPlaceName(),timeManagerScr.GetDay(),timeManagerScr.GetPlayTimeText(),timeManagerScr.GetPlayTimeSec(_slotNum), int_SunClockNum);

        //���̺� ������
        string jSaveData = JsonUtility.ToJson(curSaveUIData);

        //������ ���� ����
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);

        //���Կ� UI ������ �����ֱ�
        ShowUiDataToSlot();

        //���̺� ���� Ķ���� UI ����
        ChangeSlotUICalendar(_slotNum, curSaveUIData.day - 1);

        //Debug.Log("TimeManager PlayeTime : " + timeManagerScr.GetPlayTimeText());
    }

    //���Կ� UI ������ �����ֱ�
    public void ShowUiDataToSlot()
    {
        //Debug.Log("ShowUiDataToSlot");
        if(text_LoadPlaceName != null)
        {
            for (int i = 0; i < text_SavePlaceName.Length; i++)
            {
                //���� i��° ���Կ� �ش��ϴ� SaveData jsonFile�� �����Ѵٸ�
                if (File.Exists(saveFilePath + i.ToString()) == true)
                {
                    Debug.Log("���� Ui������ ����" + i.ToString());

                    //���� �о����
                    string jLoadData = File.ReadAllText(saveFilePath + i.ToString());

                    //curLoadUiData�� ������ȭ
                    curLoadUiData = JsonUtility.FromJson<LoadUiData>(jLoadData);

                    //���彽���� ��� UI Text ����
                    text_SavePlaceName[i].text = curLoadUiData.placeName;
                    //�ε彽���� ��� UI Text ����
                    text_LoadPlaceName[i].text = curLoadUiData.placeName;

                    ////���彽���� ��¥ UI Text ����
                    //text_SaveDayCount[i].text = curLoadUiData.day;
                    ////�ε彽���� ��¥ UI Text ����
                    //text_LoadDayCount[i].text = curLoadUiData.day;

                    //���彽���� �÷���Ÿ�� UI Text ����
                    text_SavePlayTime[i].text = curLoadUiData.playTimeText;
                    //�ε彽���� ��¥ UI Text ����
                    text_LoadPlayTime[i].text = curLoadUiData.playTimeText;

                    //���彽���� �ؽð� UI image ����
                    image_SaveSunClock[i].sprite = sprite_AllSunClock[curLoadUiData.sunClockNum];
                    //�ε彽���� �ؽð� Ui image ����
                    image_LoadSunClock[i].sprite = sprite_AllSunClock[curLoadUiData.sunClockNum];

                    //���彽���� Ķ���� UI image ����
                    image_SaveUICalendar[i].sprite = sprite_AllCalendar[curLoadUiData.day - 1];
                    //�ε彽���� Ķ���� UI image ����
                    image_LoadUICalendar[i].sprite = sprite_AllCalendar[curLoadUiData.day - 1];


                    ////�÷���Ÿ�� ����
                    //timeManagerScr.SetPlayTimeSec(curLoadUiData.playTimeSec);
                }
            }
        }
    }

    //UI ������ �ҷ�����(�÷�����, �ؽð� UI)
    public void Load(int _slotNum)
    {
        //���� �о����
        string jLoadData = File.ReadAllText(saveFilePath + _slotNum.ToString());

        //curLoadUiData�� ������ȭ
        curLoadUiData2 = JsonUtility.FromJson<LoadUiData>(jLoadData);

        //�ش��ϴ� ���Կ� �÷��� Ÿ�Ӱ��� �޾ƿ�
        timeManagerScr.SetPlayTimeSec(curLoadUiData2.playTimeSec);

        //�ؽð� UI �̹��� ����
        image_CurSunClock.sprite = sprite_AllSunClock[curLoadUiData2.sunClockNum];
        Debug.Log($"�ؽð� �̹������� : {curLoadUiData2.sunClockNum}");
    }

    //���� �ؽð� �̹��� ���ϱ�
    public void GetCurSunClockSprite()
    {
        //���� �ؽð� ��������Ʈ �̹��� ����
        sprite_CurSundClock = image_CurSunClock.sprite;
    }

    //�ؽð� �̹��� ��ȣ ���ϱ�
    public void GetSunClockNum()
    {
        for (int i = 0; i < sprite_AllSunClock.Length; i++)
        {
            //���� �ؽð� �̹����� ���� �̹����� �ε��� �� ���ϱ�
            if (sprite_AllSunClock[i] == sprite_CurSundClock)
            {
                int_SunClockNum = i;
            }
        }
    }

    //�ؽð� �̹��� ���� (TimeManager���� ����)
    public void ChangeSunClockImage(int _sunClockNum)
    {
        //�ؽð� ��������Ʈ �̹��� ����
        image_CurSunClock.sprite = sprite_AllSunClock[_sunClockNum];
    }

    //Slot UI Ķ���� ��������Ʈ �̹��� ����
    public void ChangeSlotUICalendar(int _slotNum, int _day)
    {
        image_SaveUICalendar[_slotNum].sprite = sprite_AllCalendar[_day];
        image_LoadUICalendar[_slotNum].sprite = sprite_AllCalendar[_day];
    }


    //UI�� ���������� Ȯ���Ҽ��ִ� �޼���
    public bool GetUiVisible()
    {
        if(gameObject_ItemWindow.activeSelf || gameObject_CombineWindow.activeSelf || gameObject_LoadWindow.activeSelf || gameObject_MapWindow.activeSelf ||
            gameObject_Option.activeSelf || gameObject_SaveWindow.activeSelf || gameManagerScr.isJoomackPuzzleStart)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }

    //Ŀ�� �̹��� �����ֱ�
    public void ShowCursor()
    {
        cursorCtrlScr.ChangeCursor(cursorCtrlScr.sprite_idle);
    }

    //Ŀ�� ����
    public void BlindCursor()
    {
        cursorCtrlScr.ChangeCursor(cursorCtrlScr.sprite_None);
    }

    //Ŀ�� �Һ� �ѱ�
    public void ShowCursorLight()
    {
        gameObjcet_CursorLights.SetActive(true);
    }

    //Ŀ�� �Һ� ����
    public void BlindCursorLight()
    {
        gameObjcet_CursorLights.SetActive(false);
    }
}
