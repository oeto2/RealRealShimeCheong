using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangDuckTalkEnd : MonoBehaviour
{
    public TutorialManager tutorialManagerScr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //∆©≈‰∏ÆæÛ ª±∂± flag ture
        tutorialManagerScr.TutorialSenteceEnd_Bbang();

        Invoke("SetActiveFalse", 1f);
    }

    private void SetActiveFalse()
    {
        //ø¿∫Í¡ß∆Æ ∫Ò»∞º∫»≠
        gameObject.SetActive(false);
    }
}
