using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKeyButton : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public TutorialManager tutorialManagerScr;

    //������ KeyButton
    public GameObject gameObject_Keybutton;

    //ó������ �����ٰ��� Ȯ���ϴ� flag
    public bool showFirst;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ó������ �����ٰ���
        if (showFirst)
        {
            if (collision.CompareTag("Player"))
            {
                gameObject_Keybutton.SetActive(true);
            }
        }

        else
        {
            //Ʃ�丮�� 0�� �̺�Ʈ ���� �����ֱ�
            if (collision.CompareTag("Player") && tutorialManagerScr.events != TutorialEvents.TurnOnLights)
            {
                gameObject_Keybutton.SetActive(true);

            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //ó������ �����ٰ���
        if (showFirst)
        {
            if (collision.CompareTag("Player"))
            {
                gameObject_Keybutton.SetActive(false);
            }
        }

        else
        {
            //Ʃ�丮�� 0�� �̺�Ʈ ���� �����ֱ�
            if (collision.CompareTag("Player") && tutorialManagerScr.events != TutorialEvents.TurnOnLights)
            {
                gameObject_Keybutton.SetActive(false);

            }
        }
    }
}
