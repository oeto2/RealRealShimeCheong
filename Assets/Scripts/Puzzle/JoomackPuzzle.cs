using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoomackPuzzle : MonoBehaviour
{
    //�ָ� ���� UI
    public GameObject gameObject_JoomackUI;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isJoomackPuzzleStart)
        {
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
}
