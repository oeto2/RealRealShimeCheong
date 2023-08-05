using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public GameObject Dialog_Text_Name;
    public GameObject Dialog_Text_NPC;
    public GameObject NPCText;

    void Start()
    {
        Dialog_Text_Name = GameObject.Find("Dialog_Text_Name");
        Dialog_Text_NPC = GameObject.Find("Dialog_Text_NPC");
        Dialog_Text_Name.SetActive(false);
        Dialog_Text_NPC.SetActive(false);
    }

    public void NPCChatEnter()
    {
        //text = Dialog_Text_NPC.GetComponent<Text>();
        //Dialog_Text_Name.text = text;
        Dialog_Text_Name.SetActive(true);
        Dialog_Text_NPC.SetActive(true);
    }

    public void NPCChatExit()
    {
        //Dialog_Text_NPC = "";
        //Dialog_Text_Name= "";
        Dialog_Text_Name.SetActive(false);
        Dialog_Text_NPC.SetActive(false);
    }
}