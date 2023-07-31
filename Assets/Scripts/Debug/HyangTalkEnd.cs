using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyangTalkEnd : MonoBehaviour
{
    public TutorialManager tutorialManagerScr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tutorialManagerScr.TutorialSentenceEnd_Hyang();

        Invoke("SetActiveObjcet", 1f);
    }

    private void SetActiveObjcet()
    {
        gameObject.SetActive(false);
    }
}
