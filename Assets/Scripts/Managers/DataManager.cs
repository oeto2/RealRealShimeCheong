using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public TimeManager timeManagerScr;
    public GameManager gameManagerScr;

    //���̺� ��ư
    public void SaveButton()
    {
        Debug.Log("Save");

        //TimeManager Data ����
        timeManagerScr.Save();

        //GameManager Data ����
        gameManagerScr.Save();
    }

    //�ε� ��ư
    public void LoadButton()
    {
        Debug.Log("Load");

        //TimeManagerData Load
        timeManagerScr.Load();

        //GameManager Data Load
        gameManagerScr.Load();
    }
}
