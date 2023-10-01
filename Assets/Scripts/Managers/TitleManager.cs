using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    //�̱��� ����
    public static TitleManager instance = null;

    //Load â ������Ʈ
    public GameObject gameObject_LoadWindow;

    //Load Ȯ��â ������Ʈ
    public GameObject gameObject_LoadCheckWindow;

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

    //Scene�� �ҷ����ִ� �޼���(Start Button UI�� ����)
    public void LoadMainScene()
    {
        //TextScnene �ҷ�����
        SceneManager.LoadScene("TestScene");
    }

    //Load Ȯ�� â ����
    public void ShowLoadCheckWIndow()
    {
        gameObject_LoadCheckWindow.SetActive(true);
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
    }
    
}
