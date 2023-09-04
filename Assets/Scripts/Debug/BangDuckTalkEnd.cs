using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangDuckTalkEnd : MonoBehaviour
{
    public TutorialManager tutorialManagerScr;

    private bool isFirst;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isFirst)
        {
            //Ʃ�丮�� ���� flag ture
            tutorialManagerScr.TutorialSenteceEnd_Bbang();
            isFirst = true;
        }
    }

    private void SetActiveFalse()
    {
        //������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
