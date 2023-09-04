using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_NPC : MonoBehaviour
{
    public string ChatText = "";
    private GameObject Main;
    public bool isNPCTrigger;

    public Controller controller;

    public Dialog_TypingWriter_Budhist dialog_TypingWriter_Budhist;

    //외부 스크립트에서 사용하기 위한 용도(싱글톤 패턴)
    public static Trigger_NPC instance;

    void Awake()
    {
        if(Trigger_NPC.instance == null)
        {
            Trigger_NPC.instance = this;
		}
    }

    void Start()
    {
        Main = GameObject.Find("TalkManager");
    }

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
            Debug.Log(ChatText + "NPC 터치했다");
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log(ChatText + "z 눌렀다");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject test1;
        Debug.Log("나갔지!!");
        isNPCTrigger = false;

        test1 = other.gameObject;
        if (other.CompareTag("Player"))
        {
        }
    }

}
