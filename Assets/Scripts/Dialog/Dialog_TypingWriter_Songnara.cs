using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Songnara : MonoBehaviour
{
    // ���� ä���� ������ �ؽ�Ʈ
    public Text ChatText;

    // ĳ���� �̸��� ������ �ؽ�Ʈ
    public Text CharacterName;

    // ��ȭ�� ������ �ѱ� �� �ִ� Ű(default : space)
    public List<KeyCode> skipButton;

    public string writerText = "";

    public string characternameText = "";

    bool isButtonClicked = false;

    public bool bool_isNPC = false;

    //���̾�α� UI
    public GameObject images_NPC;

    public Sprite[] images_NPC_portrait;

    public Trigger_NPC trigger_npc;

    public bool isNPCTrigger;

    public Controller controller_scr;

    public S_NPCdatabase_Yes npcDatabaseScr;

    //���� Ŭ��
    void Start()
    {
        //StartCoroutine(TextPractice());
        //TextPractice();
        //StopCoroutine(TextPractice());
        CharacterName.text = "";
        ChatText.text = "";
    }


    void Update()
    {
        foreach (var element in skipButton) // ��ư �˻�
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }
   
        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            
            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false)
            {
                //���̾�α� ���̱�
                images_NPC.SetActive(true);
                //��� ����
                writerText = "";
                //��� ���
                StartCoroutine(TextPractice());
                //�ʻ�ȭ ����
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
                bool_isNPC = true;
            }
            else
            {
                //���̾�α� ����
                images_NPC.SetActive(false);
                //��� ����
                writerText = "";
                //�ڷ�ƾ ����
                StopAllCoroutines();
                bool_isNPC = false;
            }
        }
    }
   

    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        CharacterName.text = narrator;
        //characternameText = narrator;
        writerText = "";

        //narrator = CharacterName.text;

        //�ؽ�Ʈ Ÿ����
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            //�ؽ�Ʈ Ÿ���� �ð� ����
            //yield return null;
            yield return new WaitForSeconds(0.02f);
        }

        //Ű(default : space)�� �ٽ� ���� ������ ������ ���
        while (true)
        {
            if (isButtonClicked)
            {
                isButtonClicked = false;
                break;
            }
            yield return null;
        }
    }
    

    IEnumerator TextPractice()
    {
        yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[8].npc_name, npcDatabaseScr.NPC_01[8].comment));
    }
}