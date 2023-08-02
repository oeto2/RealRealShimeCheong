using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public TimeManager timeManagerScr;
    public GameManager gameManagerScr;

    //���̺� ��ư
    public void SaveButton(int _slotNum)
    {
        Debug.Log("Save");

        //TimeManager Data ����
        timeManagerScr.Save(_slotNum);

        //GameManager Data ����
        gameManagerScr.Save(_slotNum);
    }

    //�ε� ��ư
    public void LoadButton(int _slotNum)
    {
        Debug.Log("Load");

        //TimeManagerData Load
        timeManagerScr.Load(_slotNum);

        //GameManager Data Load
        gameManagerScr.Load(_slotNum);
    }
}
