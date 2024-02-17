using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_NPC : MonoBehaviour
{
    public string ChatText = "";
    public bool isNPCTrigger;

    void Update()
	{

	}

    private void OnTriggerStay2D(Collider2D other)
    {
        GameObject test1;

        Debug.Log(ChatText);
        isNPCTrigger = true;
        test1 = other.gameObject;
        Debug.Log(test1);
        //Controller.instance.TalkStart();
        if (other.CompareTag("Player"))
        {
            Debug.Log(ChatText + "NPC ÅÍÄ¡Çß´Ù");
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log(ChatText + "z ´­·¶´Ù");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject test1;
        Debug.Log("³ª°¬Áö!!");
        isNPCTrigger = false;

        test1 = other.gameObject;
        if (other.CompareTag("Player"))
        {
        }
    }

}
