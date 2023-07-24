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
    //����â ������Ʈ
    public GameObject gameObject_CombineWindow;

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

    private void Awake()
    {
        //���콺 ������ ����
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        isMonuseOn = Cursor.visible;

        //���콺 �����͸� �Ѵ� ����
        if (gameObject_CombineWindow.activeSelf || gameObject_ItemWindow.activeSelf || gameObject_Option.activeSelf || gameObject_MapWindow.activeSelf)
        {
            //���콺 ������ �ѱ�
            Cursor.visible = true;
        }

        //���콺 �����͸� ���� ����
        if(!gameObject_CombineWindow.activeSelf && !gameObject_ItemWindow.activeSelf && !gameObject_Option.activeSelf && !gameObject_MapWindow.activeSelf)
        {
            //���콺 ������ ����
            Cursor.visible = false;
        }

        //������ â ���� �ڵ�
        #region
        //������ â ��Ȱ��ȭ ���¿��� XŰ�� ���� ���
        if (Input.GetKeyDown(KeyCode.X) && !gameObject_ItemWindow.activeSelf && !isMapWindowLaunch && !isOptionLaunch && !isCombineLaunch)
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
        if (Input.GetKeyDown(KeyCode.M) && !gameObject_MapWindow.activeSelf && !isItemWindowLaunch && !isOptionLaunch && !isCombineLaunch)
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
        if (!isOptionLaunch && !isItemWindowLaunch && !isMapWindowLaunch && !isCombineLaunch && Input.GetKeyDown(KeyCode.Escape))
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

        //����â ���� �ڵ�
        #region
        //����â�� �������̶��
        if(gameObject_CombineWindow.activeSelf)
        {
            isCombineLaunch = true;
        }

        //����â�� ���������� �ʴٸ�
        if(!gameObject_CombineWindow.activeSelf)
        {
            Invoke("CombineFalgFalse", 0.2f);
        }


        //����â�� �������̰� ESCŰ�� ���������
        if (isCombineLaunch && Input.GetKeyDown(KeyCode.Escape))
        {
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
        gameObject_Option.SetActive(false);
    }

    //����â �ѱ�
    public void CombineWindowLaunch()
    {
        if(!isOptionLaunch && !isItemWindowLaunch && !isMapWindowLaunch)
        {
            gameObject_CombineWindow.SetActive(true);
        }
    }

    //����â ����
    public void CombineWindowExit()
    {
        gameObject_CombineWindow.SetActive(false);
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

    #endregion
}
