using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKeyButton : MonoBehaviour
{
    //∫∏ø©¡Ÿ KeyButton
    public GameObject gameObject_Keybutton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameObject_Keybutton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject_Keybutton.SetActive(false);
        }
    }
}
