using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    //������â �������̽� ������Ʈ
    public GameObject gameObject_ItemWindow;
    //���� ������Ʈ
    public GameObject gameObject_MapWindow;
    //�ɼ�â ������Ʈ
    public GameObject gameObject_Option;

    //������ â�� ���������� Ȯ���ϴ� flag
    public bool isItemWindowLaunch;
    //������ ���������� Ȯ�� �ϴ� flag
    public bool isMapWindowLaunch;
    //�ɼ�â�� ���������� Ȯ���ϴ� flag
    public bool isOptionLaunch;

    //�� ��ư�� ���� ����
    private Color originColor = new Color32(255, 255, 255, 255);

    //�� ��ư�� ��Ȱ��ȭ ����
    private Color falseColor = new Color32(170, 170, 170, 255);

    //Itme Tap Button Image
    public Image itemTapImage;
    
    //Clue Tap Button Image
    public Image clueTapImage;

    // Update is called once per frame
    void Update()
    {
        //������ â ���� �ڵ�
        #region
        //������ â ��Ȱ��ȭ ���¿��� XŰ�� ���� ���
        if (Input.GetKeyDown(KeyCode.X) && !gameObject_ItemWindow.activeSelf && !isMapWindowLaunch && !isOptionLaunch)
        {
            //������ â ����
            ItemWindowLaunch();
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
        //������ �������� �ƴѵ� MŰ�� ������ ���
        if (Input.GetKeyDown(KeyCode.M) && !gameObject_MapWindow.activeSelf && !isItemWindowLaunch && !isOptionLaunch)
        {
            //���� ������Ʈ Ȱ��ȭ
            gameObject_MapWindow.SetActive(true);
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

        //������â ,���� ,�ɼ�â�� ���������� ������ ESCŰ�� ������ ���
        if (!isOptionLaunch && !isItemWindowLaunch && !isMapWindowLaunch && Input.GetKeyDown(KeyCode.Escape))
        {
            //�ɼ�â �����ֱ�
            gameObject_Option.SetActive(true);
        }

        //�ɼ�â�� �������϶� ESC�� ������ ���
        else if (isOptionLaunch && Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject_Option.SetActive(false);
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
        #endregion

    }

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
        gameObject_Option.SetActive(false);
    }

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

    //������ �� ��ư ���� ����
    public void ChangeItemTapColor()
    {
        itemTapImage.color = falseColor;
        clueTapImage.color = originColor;
    }

    //�ܼ� �� ��ư ���� ����
    public void ChangeClueTapColor()
    {
        clueTapImage.color = falseColor;
        itemTapImage.color = originColor;
    }
}
