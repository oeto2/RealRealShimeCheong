using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButton : MonoBehaviour
{
    //Load â ������Ʈ
    public GameObject gameObject_LoadWindow;

    public void LoadButton_Click()
    {
        //Load â ����
        gameObject_LoadWindow.SetActive(true);
    }
}
