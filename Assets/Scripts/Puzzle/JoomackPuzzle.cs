using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoomackPuzzle : MonoBehaviour
{
    //주막 퍼즐 UI
    public GameObject gameObject_JoomackUI;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isJoomackPuzzleStart)
        {
        }
    }

    //주막 UI 보여주기
    public void ShowJoomackUI()
    {
        gameObject_JoomackUI.SetActive(true);
    }

    //주막 UI 끄기
    public void JoomackUIClose()
    {
        gameObject_JoomackUI.SetActive(false);
    }
}
