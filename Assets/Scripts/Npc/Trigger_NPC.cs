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
    public bool isNPCTrigger;

    public Controller controller;

    public Dialog_TypingWriter_Budhist dialog_TypingWriter_Budhist;

    //�ܺ� ��ũ��Ʈ���� ����ϱ� ���� �뵵(�̱��� ����)
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
            Debug.Log(ChatText + "NPC ��ġ�ߴ�");
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //controller.TalkStart();
                Debug.Log(ChatText + "z ������");
                //dialog_TypingWriter_Budhist.trigger_npc = test1.GetComponent<Dialog_TypingWriter_Budhist>().OnClickdown();
                //Main.GetComponent<MainScript>().NPCChatEnter();
                //Controller.instance.TalkEnd();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject test1;
        Debug.Log("������!!");
        isNPCTrigger = false;

        test1 = other.gameObject;
        if (other.CompareTag("Player"))
        {
            //Main.GetComponent<MainScript>().NPCChatExit();
        }
    }

}
