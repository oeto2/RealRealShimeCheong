using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //Ŭ���� ���� int
    public int int_ClickSlotNum;

    //���� ��
    public LoadSceneState loadSenceState = LoadSceneState.Nomal;

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
    }

    //Start Button Clik
    public void StartButton_Click()
    {
        int_ClickSlotNum = 4;

        loadSenceState = LoadSceneState.Nomal;

        LoadMainScene();
    }

    //Load Button Click
    public void LoadButton_Click()
    {
        //Load â ����
        gameObject_LoadWindow.SetActive(true);
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
        gameObject_LoadCheckWindow.SetActive(true);

        //Ŭ�� ���� ��ȣ �ʱ�ȭ
        int_ClickSlotNum = _slotNum;
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
            case 1:
                loadSenceState = LoadSceneState.Slot1;
                break;

            case 2:
                loadSenceState = LoadSceneState.Slot2;
                break;

            case 3:
                loadSenceState = LoadSceneState.Slot3;
                break;

            default:
                loadSenceState = LoadSceneState.Nomal;
                break;
        }

        //MainScene �ҷ�����
        LoadMainScene();
    }
    
}
