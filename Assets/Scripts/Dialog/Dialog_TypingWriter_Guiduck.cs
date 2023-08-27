using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Guiduck : MonoBehaviour
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

    // ���� ��� ��� ����
    private int RandomNum;

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
            Debug.Log("zŰ ����! �ʹ����!!!!");

            //bool_isBotjim = true;
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
                trigger_npc.isNPCTrigger = false;

                writerText = "";
                StopAllCoroutines();
            }
        }
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
    }

    /*public void OnClickdown()
    {
        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("�̰� Touch! �ʹ����!!!!");
            StartCoroutine(TextPractice());
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
                StopCoroutine(TextPractice());
                Controller.instance.TalkEnd();
            }
        }
    }*/

    IEnumerator NormalChat()
    {
        int a = 0;
        string narrator = characternameText = CharacterName.text = dialogdb.NPC_01[2].npc_name;
        string narration = dialogdb.NPC_01[2].comment;
        string narration_2 = dialogdb.NPC_01[400].comment;
        RandomNum = Random.Range(0, 2);

        //narrator = CharacterName.text;

        //�ؽ�Ʈ Ÿ����
        if (RandomNum == 0)
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
        else if (RandomNum == 1)
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

    IEnumerator ItemClueChat(string narrator, string narration)
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
		#region �ܼ�
		//2000 : �»���� �����
		if (ObjectManager.instance.GetEquipObjectKey() == 2000)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[10].npc_name, dialogdb.NPC_01[10].comment));
		}
		//2001 : û���� ������
		else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[23].npc_name, dialogdb.NPC_01[23].comment));
		}
		//2002 : û���� ���
		else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[31].npc_name, dialogdb.NPC_01[31].comment));
		}
		//2003 : û�̿� ����
		else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[39].npc_name, dialogdb.NPC_01[39].comment));
		}
		//2004 : û�̿� �系
		else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[47].npc_name, dialogdb.NPC_01[47].comment));
		}
		//2005 : �������� �Ƶ�
		else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[58].npc_name, dialogdb.NPC_01[58].comment));
		}
		//2006 : �۳��� ���ΰ� û��
		else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[66].npc_name, dialogdb.NPC_01[66].comment));
		}
		//2007 : �·��� û��
		else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[79].npc_name, dialogdb.NPC_01[79].comment));
		}
		//2008 : �·��� ����
		else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[87].npc_name, dialogdb.NPC_01[87].comment));
		}
		//2009 : û���� ����
		else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[95].npc_name, dialogdb.NPC_01[95].comment));
		}
		//2010 : ����� ��� ��
		else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[105].npc_name, dialogdb.NPC_01[105].comment));
		}
		//2011 : ������� ��ó
		else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[114].npc_name, dialogdb.NPC_01[114].comment));
		}
		//2012 : û���� �ŷ�
		else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[122].npc_name, dialogdb.NPC_01[122].comment));
		}
		//2014 : ���������� �� ����
		else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
		{
			// �⺻ ���� ����, ���� ����������
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[138].npc_name, dialogdb.NPC_01[138].comment));
		}
		//2015 : û�̰� �簣 ��
		else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[146].npc_name, dialogdb.NPC_01[146].comment));
		}
		//2016 : ¤���� �簣 û��
		else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[156].npc_name, dialogdb.NPC_01[156].comment));
		}
		//2017 : ���� ����
		else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[164].npc_name, dialogdb.NPC_01[164].comment));
		}
		//2018 : ������ �ܰ�
		else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[183].npc_name, dialogdb.NPC_01[183].comment));
		}
		//2019 : ���� �ʴ� ��
		else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[191].npc_name, dialogdb.NPC_01[191].comment));
		}
		//2021 : ����� ����
		else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[208].npc_name, dialogdb.NPC_01[208].comment));
		}
		//2023 : 3�� ������
		else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[230].npc_name, dialogdb.NPC_01[230].comment));
		}
		#endregion

		#region ������
		//1000 : ��
		else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[284].npc_name, dialogdb.NPC_01[284].comment));
		}
		//1001 : ����
		else if (ObjectManager.instance.GetEquipObjectKey() == 1001)
		{
			yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[250].npc_name, dialogdb.NPC_01[250].comment));
		}
        //1002 : �νÿ� �ν˵�
        else if (ObjectManager.instance.GetEquipObjectKey() == 1002)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[258].npc_name, dialogdb.NPC_01[258].comment));
        }
        //1003 : �ٰ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 1003)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[267].npc_name, dialogdb.NPC_01[267].comment));
        }
        //1005 : �ָԹ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[293].npc_name, dialogdb.NPC_01[293].comment));
        }
        //1006 : ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[300].npc_name, dialogdb.NPC_01[300].comment));
        }
        //1007 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[308].npc_name, dialogdb.NPC_01[308].comment));
        }
        //1008 : ������
        else if (ObjectManager.instance.GetEquipObjectKey() == 1008)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[316].npc_name, dialogdb.NPC_01[316].comment));
        }
        //1009 : ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[323].npc_name, dialogdb.NPC_01[323].comment));
        }
        //1011 : ����� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[338].npc_name, dialogdb.NPC_01[338].comment));
        }
        #endregion

        #region ���� �ܼ�
        //4023 : ����̸� ���� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[350].npc_name, dialogdb.NPC_01[350].comment));
        }
        //4015 : û�̰� ����� ��
        else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[358].npc_name, dialogdb.NPC_01[358].comment));
        }
        //4017 : û�̿� ���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[366].npc_name, dialogdb.NPC_01[366].comment));
        }
        //8032 : �Բ� ����� �� ���
        else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[374].npc_name, dialogdb.NPC_01[374].comment));
        }
        //4033 : ������ �ߴ�
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[382].npc_name, dialogdb.NPC_01[382].comment));
        }
        //4018 : û���� ����
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[392].npc_name, dialogdb.NPC_01[392].comment));
        }
        #endregion

        #region �⺻ ���
        else
        {
            yield return StartCoroutine(NormalChat());
        }
        #endregion
    }
}