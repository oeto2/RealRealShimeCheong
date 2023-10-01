using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButton : MonoBehaviour
{
    //Load 창 오브젝트
    public GameObject gameObject_LoadWindow;

    public void LoadButton_Click()
    {
        //Load 창 띄우기
        gameObject_LoadWindow.SetActive(true);
    }
}
