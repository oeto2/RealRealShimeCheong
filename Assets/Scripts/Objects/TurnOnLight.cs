using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnLight : MonoBehaviour
{
    //Spot Light Object
    public GameObject lightObject;

    //오브젝트랑 닿았는지
    private bool isTouch;

    //불이 켜졌는지
    [Tooltip("등잔 불이 켜지면 True")]
    public bool isTrunOnLight;

    // Update is called once per frame
    void Update()
    {
        if(isTouch && Input.GetKeyDown(KeyCode.Z))
        {
            lightObject.SetActive(true);
            isTrunOnLight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTouch = false;
        }
    }
}
