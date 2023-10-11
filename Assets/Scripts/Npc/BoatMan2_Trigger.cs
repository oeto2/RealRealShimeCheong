using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMan2_Trigger : MonoBehaviour
{
    //¹î»ç°ø2¿Í Á¢ÃËÇß´ÂÁö
    public bool isTouch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isTouch = false;
    }
}
