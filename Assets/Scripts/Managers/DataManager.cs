using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public UIManager uiManagerScr;
    public TimeManager timeManagerScr;
    public GameManager gameManagerScr;
    public ObjectManager objectManagerScr;

    //�ε� �ð�
    public int int_LodingTime;

    //�ε� �̹��� ������Ʈ
    public GameObject gameObject_Loading;

    //���̺� üũ ������Ʈ
    public GameObject gameObject_SaveCheckWindow;

    //�ε� üũ ������Ʈ
    public GameObject gameObjcet_LoadCheckWindow;

    //���̺� ���� ��ȣ
    private int int_SaveSlotNum;

    //�ε� ���� ��ȣ
    private int int_LoadSlotNum;

    //���̺� Yes Button
    public void SaveYesButton()
    {
        Debug.Log("Save");

        //TimeManager Data ����
        timeManagerScr.Save(int_SaveSlotNum);

        //GameManager Data ����
        gameManagerScr.Save(int_SaveSlotNum);

        //ObjectManager Data ����
        objectManagerScr.Save(int_SaveSlotNum);

        //���̺� Ȯ��â ����
        gameObject_SaveCheckWindow.SetActive(false);
    }

    //���̺� No Button
    public void SaveNoButton()
    {
        gameObject_SaveCheckWindow.SetActive(false);
    }
    
    //�ε� Yes��ư
    public void LoadYesButton()
    {
        Debug.Log("Load");

        //�ε�
        StartCoroutine(Loading());

        //TimeManagerData Load
        timeManagerScr.Load(int_LoadSlotNum);

        //GameManager Data Load
        gameManagerScr.Load(int_LoadSlotNum);

        //ObjectManager Data Load
        objectManagerScr.Load(int_LoadSlotNum);
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
        //�ε� ���� ��ȣ ����
        int_LoadSlotNum = _slotNum;
        //�ε� Ȯ�� â ����
        gameObjcet_LoadCheckWindow.SetActive(true);
    }
}
