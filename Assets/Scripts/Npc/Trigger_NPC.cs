using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_NPC : MonoBehaviour
{
    /*
	void OnTriggerEnter2D(Collider2D npc_collider)
	{
		
	}
    */
    public string ChatText = "";
    private GameObject Main;
    void Start()
    {
        Main = GameObject.Find("TalkManager");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touch!");
        Debug.Log(ChatText);
        if (other.CompareTag("NPC"))
        {
            Main.GetComponent<MainScript>().NPCChatEnter(ChatText);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            Main.GetComponent<MainScript>().NPCChatExit();
        }
    }

}
