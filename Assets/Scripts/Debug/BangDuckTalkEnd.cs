using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangDuckTalkEnd : MonoBehaviour
{
    public TutorialManager tutorialManagerScr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ʃ�丮�� ���� flag ture
        tutorialManagerScr.TutorialSenteceEnd_Bbang();

        Invoke("SetActiveFalse", 1f);
    }

    private void SetActiveFalse()
    {
        //������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
