using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMan3 : MonoBehaviour
{
    //뱃사공과 닿았는지
    public bool isTouch;

    private void Update()
    {
        if(isTouch && Input.GetKeyDown(KeyCode.Z) && !EventManager.instance.gameObject_SelectUI.activeSelf)
        {
            //선택지 시작
            EventManager.instance.SelectStart(NPCName.boatman3, 7194);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }
}
