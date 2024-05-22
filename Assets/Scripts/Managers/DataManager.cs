using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using Unity.VisualScripting;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;

    //�ܺ� ��ũ��Ʈ ����
    public UIManager uiManagerScr;
    public TimeManager timeManagerScr;
    public GameManager gameManagerScr;
    public ObjectManager objectManagerScr;
    public TutorialManager tutorialManagerScr;
    public EventManager eventManagerScr;

    //�ε� �ð�
    public int int_LodingTime;

    //�ε� �̹��� ������Ʈ
    public GameObject gameObject_Loading;

    //���̺� üũ ������Ʈ
    public GameObject gameObject_SaveCheckWindow;

    //�ε� üũ ������Ʈ
    public GameObject gameObjcet_LoadCheckWindow;

    //���̺� ���� ��ȣ
    [SerializeField] private int int_SaveSlotNum;

    //�ε� ���� ��ȣ
    [SerializeField] private int int_LoadSlotNum;

    //TitleManager
    public TitleManager titleManagerScr;

    //���� �̺�Ʈ ������Ʈ
    public GameObject gameObject_StartMessage;

    //�̺�Ʈ��
    public event Action LoadEvent;
    public event Action SaveEvent;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //Ÿ��Ʋ ȭ������ ���� ������ �ޱ�
        if(GameObject.Find("TitleManager") != null)
        {
            //TitleManager ã��
            titleManagerScr = GameObject.Find("TitleManager").GetComponent<TitleManager>();

            //TitleManager ���¿� ���� ���嵥���� �ҷ�����
            switch (titleManagerScr.loadSenceState)
            {
                case LoadSceneState.Slot1:

                    //���� ��ȣ �ٲٱ�
                    int_LoadSlotNum = titleManagerScr.int_ClickSlotNum;

                    //�ش� ������ �ҷ�����
                    LoadYesButton();

                    //���� �̺�Ʈ ȭ�� ����
                    gameObject_StartMessage.SetActive(false);

                    //�̵����� ����
                    Controller.instance.TalkEnd();

                    break;

                case LoadSceneState.Slot2:

                    //���� ��ȣ �ٲٱ�
                    int_LoadSlotNum = titleManagerScr.int_ClickSlotNum;

                    //�ش� ������ �ҷ�����
                    LoadYesButton();

                    //���� �̺�Ʈ ȭ�� ����
                    gameObject_StartMessage.SetActive(false);

                    //�̵����� ����
                    Controller.instance.TalkEnd();


                    break;

                case LoadSceneState.Slot3:

                    //���� ��ȣ �ٲٱ�
                    int_LoadSlotNum = titleManagerScr.int_ClickSlotNum;

                    //�ش� ������ �ҷ�����
                    LoadYesButton();

                    //���� �̺�Ʈ ȭ�� ����
                    gameObject_StartMessage.SetActive(false);

                    //�̵����� ����
                    Controller.instance.TalkEnd();

                    break;

                default:
                    //�÷��̾� �̵� ����
                    Controller.instance.TalkStart();
                    break;
            }
        }
    }

    //���̺� Yes Button
    public void SaveYesButton()
    {
        //TimeManager Data ����
        timeManagerScr.Save(int_SaveSlotNum);

        //GameManager Data ����
        gameManagerScr.Save(int_SaveSlotNum);

        //ObjectManager Data ����
        objectManagerScr.Save(int_SaveSlotNum);

        //UiManager Data ����
        uiManagerScr.Save(int_SaveSlotNum);

        //Tutorial Data ����
        tutorialManagerScr.Save(int_SaveSlotNum);

        eventManagerScr.Save(int_SaveSlotNum);

        //���̺� Ȯ��â ����
        gameObject_SaveCheckWindow.SetActive(false);

        //���̺� �̺�Ʈ ȣ��
        CallSaveEvent();
    }

    //���̺� No Button
    public void SaveNoButton()
    {
        gameObject_SaveCheckWindow.SetActive(false);
    }
    
    //�ε� Yes��ư
    public void LoadYesButton()
    {
        //�ε�
        StartCoroutine(Loading());

        //TimeManagerData Load
        timeManagerScr.Load(int_LoadSlotNum);

        //GameManager Data Load
        gameManagerScr.Load(int_LoadSlotNum);

        //ObjectManager Data Load
        objectManagerScr.Load(int_LoadSlotNum);

        //UiManager PlayTime Data Load
        uiManagerScr.Load(int_LoadSlotNum);

        //Tutorial Data Load
        tutorialManagerScr.Load(int_LoadSlotNum);

        //Event Data Load
        eventManagerScr.Load(int_LoadSlotNum);

        //�ε� �̺�Ʈ ȣ��
        CallLoadEvent();
    }

    //�ε� No��ư
    public void LoadNoButton()
    {
        gameObjcet_LoadCheckWindow.SetActive(false);
    }

    //�ε� ��
    IEnumerator Loading()
    {
        //�ε��� �̹��� ����
        gameObject_Loading.SetActive(true);

        //�ɼ�, �ε�â ����
        gameObjcet_LoadCheckWindow.SetActive(false);
        uiManagerScr.ExitLoadWindow();
        uiManagerScr.OptionExit();

        yield return new WaitForSeconds(int_LodingTime);

        gameObject_Loading.SetActive(false);
        
    }

    //���̺� ���� Ŭ��
    public void SaveSlotClick(int _slotNum)
    {
        //���̺� ���� ��ȣ ����
        int_SaveSlotNum = _slotNum;
        //���̺� Ȯ�� â ����
        gameObject_SaveCheckWindow.SetActive(true);
    }

    //�ε� ���� Ŭ��
    public void LoadSlotClick(int _slotNum)
    {
        //���� �ش� ������ SaveData jsonFile�� �����Ѵٸ�
        if (File.Exists(UIManager.instance.saveFilePath + _slotNum) == true)
        {
            //�ε� ���� ��ȣ ����
            int_LoadSlotNum = _slotNum;
            //�ε� Ȯ�� â ����
            gameObjcet_LoadCheckWindow.SetActive(true);
        }    
    }
    public void CallSaveEvent()
    {
        SaveEvent?.Invoke();
    }

    public void CallLoadEvent()
    {
        LoadEvent?.Invoke();
    }
}
