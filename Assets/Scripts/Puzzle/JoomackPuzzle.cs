using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoomackPuzzle : MonoBehaviour
{
    //�ָ� ���� UI
    public GameObject gameObject_JoomackUI;

    //�ָ� ���� ���� �ؽ�Ʈ 1��
    public Text text_Answer1;

    //�ָ� ���� ���� �ؽ�Ʈ 2��
    public Text text_Answer2;

    //���� 1���� ��
    public int int_Answer1Value = 1;

    //���� 2���� ��
    public int int_Answer2Value = 1;

    //������ Ŭ���� �ߴ���
    public bool isClear;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isJoomackPuzzleStart)
        {
        }
    }

    //���� 1�� ��ư Ŭ�� ��
    public void Answer1Button_Click()
    {
        if(int_Answer1Value <9)
        {
            int_Answer1Value++;
        }
        else
        {
            int_Answer1Value = 1;
        }

        switch (int_Answer1Value)
        {
            case 1:
                text_Answer1.text = "��";
                break;
            case 2:
                text_Answer1.text = "��";
                break;
            case 3:
                text_Answer1.text = "��";
                break;
            case 4:
                text_Answer1.text = "��";
                break;
            case 5:
                text_Answer1.text = "��";
                break;
            case 6:
                text_Answer1.text = "��";
                break;
            case 7:
                text_Answer1.text = "ĥ";
                break;
            case 8:
                text_Answer1.text = "��";
                break;
            case 9:
                text_Answer1.text = "��";
                break;
        }
    }

    //���� 2�� ��ư Ŭ�� ��
    public void AnswerButton2_Click()
    {
        if(int_Answer2Value == 1)
        {
            int_Answer2Value = 0;
        }
        else
        {
            int_Answer2Value = 1;
        }

        switch(int_Answer2Value)
        {
            case 0:
                text_Answer2.text = " ";
                break;
            case 1:
                text_Answer2.text = "��";
                break;
        }
    }

    //Ȯ�� ��ư Ŭ�� ��
    public void EnterButton_Click()
    {
        //���� �� �Է½�
        if(int_Answer1Value == 2 && int_Answer2Value == 1)
        {
            Debug.Log("���� Ŭ����");
            isClear = true;
            GameManager.instance.JoomackPuzzleClear();
        }
    }

    //�ָ� UI �����ֱ�
    public void ShowJoomackUI()
    {
        gameObject_JoomackUI.SetActive(true);
    }

    //�ָ� UI ����
    public void JoomackUIClose()
    {
        gameObject_JoomackUI.SetActive(false);
    }

    //������ UI �����ֱ�
    public void ShowStrawUI()
    {
        gameObject_JoomackUI.SetActive(true);
    }

}
