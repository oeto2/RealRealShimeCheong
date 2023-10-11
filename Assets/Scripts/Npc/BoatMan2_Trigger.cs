using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMan2_Trigger : MonoBehaviour
{
    //����2�� �����ߴ���
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
