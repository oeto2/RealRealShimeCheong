using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

//LoadScene�Ҷ� �Ѱ��� ���°�
public enum LoadSceneState
{
    Slot1, //1�� ����
    Slot2, //2�� ����
    Slot3, //3�� ����
    Nomal, //ó������
}

public class TitleManager : MonoBehaviour
{
    //�̱��� ����
    public static TitleManager instance = null;

    //Load â ������Ʈ
    public GameObject gameObject_LoadWindow;

    //Load Ȯ��â ������Ʈ
    public GameObject gameObject_LoadCheckWindow;

    //Load Slot Place Name Text
    public Text[] text_LoadPlaceName;

    //Load Slot DayCount Text
    public Text[] text_LoadDayCount;

    //Load Slot PlayTime text
    public Text[] text_LoadPlayTime;

    //Load Slot SunClock Image
    public Image[] image_LoadSunClock;

    //Load Slot UI Ķ���� �̹���
    public Image[] image_LoadUICalendar;

    //Ķ���� ��������Ʈ ����
    public Sprite[] sprite_AllCalendar;

    //�ε��� �����͸� �޾ƿ� Ŭ����
    public LoadUiData curLoadUiData;

    //�ؽð� ��������Ʈ ��� �̹�����
    public Sprite[] sprite_AllSunClock;

    //Ŭ���� ���� int
    public int int_ClickSlotNum;

    //���� ��
    public LoadSceneState loadSenceState = LoadSceneState.Nomal;

    //���� ���� ��ġ
    public string saveFilePath;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        //���� ���� ��ġ
        saveFilePath = Application.persistentDataPath + "/UiDataText.txt";
    }

    //������ �������ִ� �޼���
    public void ExitGame()
    {
        if (gameObject_LoadWindow.activeSelf == false)
        {
            //���� ����
            Application.Quit();
        }
    }

    //Start Button Clik
    public void StartButton_Click()
    {
        if (gameObject_LoadWindow.activeSelf == false)
        {
            int_ClickSlotNum = 4;

            loadSenceState = LoadSceneState.Nomal;

            LoadMainScene();
        }
    }

    //Load Button Click
    public void LoadButton_Click()
    {
        //Load â ����
        gameObject_LoadWindow.SetActive(true);

        //Loadâ UI ������ �ҷ�����
        ShowUiDataToSlot();
    }

    //Scene�� �ҷ����ִ� �޼���(Start Button UI�� ����)
    public void LoadMainScene()
    {
        //TextScnene �ҷ�����
        SceneManager.LoadScene("TestScene");
    }

    //Load Ȯ�� â ����
    public void ShowLoadCheckWIndow(int _slotNum)
    {
        //���� i��° ���Կ� �ش��ϴ� SaveData jsonFile�� �����Ѵٸ�
        if (File.Exists(saveFilePath + _slotNum) == true)
        {
            //Ŭ�� ���� ��ȣ �ʱ�ȭ
            int_ClickSlotNum = _slotNum;

            gameObject_LoadCheckWindow.SetActive(true);
        }    
    }

    //Load â ����
    public void CloseLoadWindow()
    {
        gameObject_LoadWindow.SetActive(false);
    }

    //�ƴϿ� ��ư Ŭ��
    public void NoButton_Click()
    {
        gameObject_LoadCheckWindow.SetActive(false);
    }

    //�� ��ư Ŭ��
    public void YesButton_Click()
    {
        gameObject_LoadCheckWindow.SetActive(false);

        //Load State ����
        switch(int_ClickSlotNum)
        {
            case 0:
                loadSenceState = LoadSceneState.Slot1;
                break;

            case 1:
                loadSenceState = LoadSceneState.Slot2;
                break;

            case 2:
                loadSenceState = LoadSceneState.Slot3;
                break;

            default:
                loadSenceState = LoadSceneState.Nomal;
                break;
        }

        //MainScene �ҷ�����
        LoadMainScene();
    }

    //���Կ� UI ������ �����ֱ�
    public void ShowUiDataToSlot()
    {
        //Debug.Log("ShowUiDataToSlot");
        if (text_LoadPlaceName != null)
        {
            for (int i = 0; i < text_LoadPlaceName.Length; i++)
            {
                //���� i��° ���Կ� �ش��ϴ� SaveData jsonFile�� �����Ѵٸ�
                if (File.Exists(saveFilePath + i.ToString()) == true)
                {
                    Debug.Log("���� Ui������ ����" + i.ToString());

                    //���� �о����
                    string jLoadData = File.ReadAllText(saveFilePath + i.ToString());

                    //curLoadUiData�� ������ȭ
                    curLoadUiData = JsonUtility.FromJson<LoadUiData>(jLoadData);
                   
                    //�ε彽���� ��� UI Text ����
                    text_LoadPlaceName[i].text = curLoadUiData.placeName;

                    //�ε彽���� ��¥ UI Text ����
                    text_LoadPlayTime[i].text = curLoadUiData.playTimeText;
                  
                    //�ε彽���� �ؽð� Ui image ����
                    image_LoadSunClock[i].sprite = sprite_AllSunClock[curLoadUiData.sunClockNum];
                 
                    //�ε彽���� Ķ���� UI image ����
                    image_LoadUICalendar[i].sprite = sprite_AllCalendar[curLoadUiData.day - 1];

                }
            }
        }
    }
}
