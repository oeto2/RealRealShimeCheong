using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrawPuzzle : MonoBehaviour
{
    //������ ���� UI
    public GameObject gameObject_StrawUI;


    //���� ��ư Ŭ����
    public void LeftButton_Click()
    {
        //�ð���� ������ ������ ȹ��
        ObjectManager.instance.GetItem(1013);

        //������ ���� ����
        GameManager.instance.StrawPuzzleClear();
    }

    //������ ��ư Ŭ����
    public void RightButton_Click()
    {
        //�ݽð���� ������ ������ ȹ��
        ObjectManager.instance.GetItem(1014);

        //������ ���� ����
        GameManager.instance.StrawPuzzleClear();
    }
}



