using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Budhist : MonoBehaviour
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

    public GameObject images_NPC;

    public Sprite[] images_NPC_portrait;
    
    public Trigger_NPC trigger_npc;

    public bool isNPCTrigger;

    public Controller controller_scr;

    // �ڷ�ƾ ���� ����
    private bool m_isBreak;
    Coroutine co_NormalChat_4999;
    Coroutine co_end;

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

    //���� Ŭ��
    void Start()
    {
        //StartCoroutine(TextPractice());
        //TextPractice();
        //StopCoroutine(TextPractice());
        CharacterName.text = "";
        ChatText.text = "";
    }

	private void Awake()
	{
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

	public void Update()
    {
        foreach (var element in skipButton) // ��ư �˻�
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }
        //OnClickdown();
        //TextPractice();
        //StopCoroutine(TextPractice());

        /* if (Input.GetMouseButtonDown(0))
         {
             StartCoroutine(TextPractice());
         }*/

        /*if (Input.GetMouseButtonDown(1))
                {
                    StopCoroutine(TextPractice());
                }*/
        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("zŰ ����! �·�!!!!");
            //Debug.Log(dialogs[index].dialogue);
            //StartCoroutine(TextPractice());
            //bool_isBotjim = true;
            //InvokeRepeating("Invoke_4999_test", 0f, 0.05f);
			controller_scr.TalkStart();
            if (bool_isNPC == false)
            {
                StartCoroutine(TextPractice());
                images_NPC.SetActive(true);
                bool_isNPC = true;
                Trigger_NPC.instance.isNPCTrigger = true;
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
            }
            else
            {
                images_NPC.SetActive(false);
                // images_NPC_portrait.SetActive(false);
                bool_isNPC = false;
                Trigger_NPC.instance.isNPCTrigger = false;
                //Controller.instance.TalkEnd();
                //StopCoroutine("TextPractice");
                writerText = "";
                StopAllCoroutines();
            }
            //co_NormalChat_4999 = StartCoroutine(TextPractice());
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        isNPCTrigger = true;
        if (other.CompareTag("Player"))
        {
            OnClickdown();
        }
    }
        public void OnClickdown()
        {
        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("�̰� Touch! �·�!!!!");
            //StartCoroutine(TextPractice());
            //bool_isBotjim = true;
            if (bool_isNPC == true)
            {
                Controller.instance.TalkStart();
                images_NPC.SetActive(true);
                bool_isNPC = false;

                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
            }
            else
            {
                images_NPC.SetActive(false);
               // images_NPC_portrait.SetActive(false);
                bool_isNPC = true;
                //StopCoroutine(TextPractice());
                Controller.instance.TalkEnd();
            }
        }
    }

    IEnumerator NormalChat_4999(string narrator, string narration)
    {
        int a = 0;
        /*
        // ���ڻ� ���� ����
        bool t_white = false;
        bool t_red = false;

        // ���ڻ� ���� ���ڴ� ��� ��� ����
        bool t_ignore = false;
        */
        //CharacterName.text = narrator;
        narrator = characternameText = dialogdb.NPC_01[0].npc_name;
        //CharacterName.text = narrator;
        writerText = dialogdb.NPC_01[0].comment;
        Debug.Log(characternameText);
        //writerText = "";

        //narrator = CharacterName.text;
        //yield return null;
        //�ؽ�Ʈ Ÿ����
        for (a = 0; a < narration.Length; a++)
        //for (a = 0; a < 62; a++)
        {
            /*string t_letter = narration[a].ToString();
            //string t_letter;
            switch (narration[a])
            {
                case '��':
                    t_white = false;
                    t_red = true;
                    t_ignore = true;
                    break;
                //case '��':
                    //t_white = true;
                    //t_red = false;
                    //t_ignore = true;
                    break;
            }
            if (t_ignore==true)
            {
                if (t_white)
                {
                    t_letter = "<color=#ffffff>" + narration[a] + "</color>";    // HTML Tag
					Debug.Log(t_letter);
                    Debug.Log('1');

				}

				else if (t_red)
                {
                    t_letter = "<color=#B40404>" + narration[a] + "</color>";
                    Debug.Log(t_letter);
                    Debug.Log('2');
                }
                Debug.Log(writerText);
                //ChatText.text = writerText;
                //writerText += t_letter; // Ư�����ڰ� �ƴ϶�� ��� ���
                //writerText += narration[a];
                //ChatText.text = writerText;
                //t_ignore = false; // �� ���� ������� �ٽ� false
            }*/

            writerText += narration[a];
            ChatText.text = writerText;
            //t_ignore = false; // �� ���� ������� �ٽ� false
            //ChatText.text = t_letter;
            //writerText += t_letter; // Ư�����ڰ� �ƴ϶�� ��� ���
            //ChatText.text = writerText;
            //�ؽ�Ʈ Ÿ���� �ð� ����
            yield return new WaitForSeconds(0.07f);
        }
        yield return null;
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

    IEnumerator NormalChat_4999_test(string narrator, string narration)
    {
        int a = 0;
        //CharacterName.text = narrator;
        narrator = characternameText  = CharacterName.text = dialogdb.NPC_01[0].npc_name;
        narration = dialogdb.NPC_01[0].comment;
        Debug.Log(writerText);
        Debug.Log(narration);
        writerText = "";

        //narrator = CharacterName.text;

        //�ؽ�Ʈ Ÿ����
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            //�ؽ�Ʈ Ÿ���� �ð� ����
            //yield return null;
            yield return new WaitForSeconds(0.01f);
        }
        //yield break;

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

    IEnumerator NormalChat_2(string narrator, string narration)
    {
        int a = 0;
        //CharacterName.text = narrator;
        narrator = characternameText = dialogdb.NPC_01[0].npc_name;
        writerText = dialogdb.NPC_01[0].comment;
        Debug.Log(writerText);
        //writerText = "";

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
        //yield return StartCoroutine(NormalChat(characternameText, writerText));
        //if(index == 4999)
        //{
        yield return StartCoroutine(NormalChat_4999_test(characternameText, writerText)); 
        //yield return StartCoroutine(NormalChat_2(characternameText, writerText));
        //}
        //yield return StartCoroutine(NormalChat(characternameText, writerText));
        //yield return StartCoroutine(NormalChat("���º���", "?�ȳ��ϼ���, �ݰ����ϴ�. ��ȭ ��ȯ �׽�Ʈ�Դϴ� �̰��� �׽�Ʈ��? �׷� �׽�Ʈ�� �׽�Ʈ�� �׽�Ʈ�� �׽�Ʈ"));
    }
}