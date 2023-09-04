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
            //∆©≈‰∏ÆæÛ ª±∂± flag ture
            tutorialManagerScr.TutorialSenteceEnd_Bbang();
            isFirst = true;
        }
    }

    private void SetActiveFalse()
    {
        //ø¿∫Í¡ß∆Æ ∫Ò»∞º∫»≠
        gameObject.SetActive(false);
    }
}
