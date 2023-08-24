using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Bbang : MonoBehaviour
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

    public GameObject images_Bbang;

    public Sprite[] images_NPC_portrait;

    public Trigger_NPC trigger_npc;

    public Controller controller_scr;

    // ���� ��� ��� ����
    private int RandomNum;

    // ���� ��� ��� ���� �迭
    public string[] Random_text_array;

    [System.Serializable]
    public struct DialogData
    {
        public int speakerIndex;              // �̸��� ��縦 ����� ���� DialogSystem�� speaker �迭 ����
        public string name;                   // NPC �̸�
        [TextArea(3, 5)]
        public string dialogue;               // ���
    }

    [SerializeField]
    public int index;
    [SerializeField]
    public S_NPCdatabase_Yes dialogdb;
    [SerializeField]
    private DialogData[] dialogs;

    //�ܺ� ��ũ��Ʈ���� ����ϱ� ���� �뵵(�̱�������)
    public static Dialog_TypingWriter_Bbang instance;
    void Start()
    {
        CharacterName.text = "";
        ChatText.text = "";
    }
    private void Awake()
	{

        if (Dialog_TypingWriter_Bbang.instance == null)
        {
            Dialog_TypingWriter_Bbang.instance = this;
        }

        for (int i = 4999; i < dialogdb.NPC_01.Count; ++i)
        {
            if (dialogdb.NPC_01[i].index_num == index)
            {
                dialogs[index].name = dialogdb.NPC_01[i].npc_name;
                dialogs[index].dialogue = dialogdb.NPC_01[i].comment;
                index++;
            }
        }
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
        //StopCoroutine(TextPractice());
        dialogstart();
        /*if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(TextPractice());
        }*/
    }

    public void dialogstart()
    {
        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("zŰ ����! Bbang!!!!");

            controller_scr.TalkStart();
            //bool_isBotjim = true;
            if (bool_isNPC == false)
            {
                StartCoroutine(TextPractice());
                trigger_npc.isNPCTrigger = true;
                images_Bbang.SetActive(true);
                bool_isNPC = true;
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
            }
            else
            {
                images_Bbang.SetActive(false);
                bool_isNPC = false;
                trigger_npc.isNPCTrigger = false;

                writerText = "";
                StopAllCoroutines();
            }
        }
    }

    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        narrator = characternameText = CharacterName.text = dialogdb.NPC_01[1].npc_name;
        narration = dialogdb.NPC_01[1].comment;
        string narration_2 = dialogdb.NPC_01[399].comment;
        RandomNum = Random.Range(0, 2);
        Debug.Log(RandomNum);

        if(RandomNum == 0)
		{
            for (a = 0; a < narration.Length; a++)
            //for (a = 0; a < textSpeed; a++)
            {
                writerText += narration[a];
                ChatText.text = writerText;

                //�ؽ�Ʈ Ÿ���� �ð� ����
                //yield return null;
                yield return new WaitForSeconds(0.05f);
            }
            yield return null;
        }
        else if(RandomNum == 1)
		{
            for (a = 0; a < narration_2.Length; a++)
            //for (a = 0; a < textSpeed; a++)
            {
                writerText += narration_2[a];
                ChatText.text = writerText;

                //�ؽ�Ʈ Ÿ���� �ð� ����
                //yield return null;
                yield return new WaitForSeconds(0.05f);
            }
            yield return null;
        }
        Debug.Log(writerText);
        //writerText = "";

        /*
        //�ؽ�Ʈ Ÿ����
        for (a = 0; a < narration.Length; a++)
        //for (a = 0; a < textSpeed; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            //�ؽ�Ʈ Ÿ���� �ð� ����
            //yield return null;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
        */

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
        //yield return StartCoroutine(NormalChat("�������", "ȣȣ, ���� ���̽Ű���?"));
        //yield return StartCoroutine(NormalChat("�������", "�̹��� �鿩�� ��డ �׷��� ���ڴ���,,,"));
        yield return StartCoroutine(NormalChat(characternameText, writerText));
    }
}