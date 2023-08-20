using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem_test : MonoBehaviour
{
	[System.Serializable]
	public struct Speaker
	{
		public Sprite spriteRenderer; // ������� NPC�� �ʻ�ȭ
		public Image imageDialog;             // ��ȭ���� Image UI
		public Text textName;      // ���� ������� NPC �̸� ��� Text
		public Text textDialogue;  // ��� text
		public GameObject objectArrow;		  // ��� �Ϸ�Ǿ��� ��� ��µǴ� �ӽ� Ŀ��
	}

	[System.Serializable]
	public struct DialogData
	{
		public int speakerIndex;              // �̸��� ��縦 ����� ���� DialogSystem�� speaker �迭 ����
		public string name;                   // NPC �̸�
		[TextArea(3, 5)]
		public string dialogue;				  // ���
	}

	[SerializeField]
	private int index;
	[SerializeField]
	private S_NPCdatabase_Yes dialogdb;
	[SerializeField]
	private Speaker[] speakers;
	[SerializeField]
	private DialogData[] dialogs;
	[SerializeField]
	private bool isAutoStart = false;		// �ڵ����ۿ���
	private bool isFirst = true;			// ���� 1ȸ�� ȣ���ϱ� ���� ����
	private int currentDialogIndex = -1;    // ���� ������� ��ȣ
	private int currentSpeakerIndex = 0;    // ���� ��ȭ�ϴ� ȭ���� speakers �迭 ����� ��ȣ
	private float typingSpeed = 0.1f;       // text typing ��� �ӵ�
	private bool isTypingEffect = false;    // ���� text typing ������ �Ǻ�

	/*
	private void Awake()
	{
		int index = 0;

		for(int i = 0; i < dialogdb.NPC_01.Count; ++i)
		{
			if(dialogdb.NPC_01[i].index_num == index)
			{
				dialogs[index].name = dialogdb.NPC_01[i].npc_name;
				dialogs[index].dialogue = dialogdb.NPC_01[i].comment;
				index++;
			}
		}
		Setup();
	}
	*/
	private void Setup()
	{
		// ��� ��ȭ ���� ������Ʈ ��Ȱ��ȭ
		for (int i = 0; i < speakers.Length; ++i)
		{
			SetActiveObjects(speakers[i], false);

			// �ʻ�ȭ�� ���̵��� ����
			//speakers[i].spriteRenderer.SetActive(true);
			GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = speakers[i].spriteRenderer;
		}
	}

	public bool UpdateDialog()
	{
		// ��� �бⰡ ���۵� �� 1ȸ�� ȣ��
		if(isFirst == true)
		{
			Setup();

			if (isAutoStart)
				SetNextDialog();

			isFirst = false;
		}
		if(Input.GetKeyDown(KeyCode.Z))
		{
			// text typing ���� �� zŰ Ŭ���ϸ� Ÿ���� ȿ�� ����
			if(dialogs.Length > currentDialogIndex+1)
			{
				SetNextDialog();
			}
			else
			{
				for (int i = 0; i<speakers.Length; ++i)
				{
					SetActiveObjects(speakers[i], false);

					//speakers[i].spriteRenderer.gameObject.SetActive(false);
				}
				return true;
			}
		}
		return false;
	}

	private void SetNextDialog()
	{
		// ���� ��ȭ ������Ʈ ��Ȱ��ȭ
		SetActiveObjects(speakers[currentSpeakerIndex], false);

		// ���� ��� ����
		currentDialogIndex++;

		// ���� ��� ���� ����
		currentSpeakerIndex = dialogs[currentDialogIndex].speakerIndex;

		SetActiveObjects(speakers[currentSpeakerIndex], true);

		speakers[currentSpeakerIndex].textName.text = dialogs[currentDialogIndex].name;

		speakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue;
	}

	private void SetActiveObjects(Speaker speaker, bool visible)
	{
		speaker.imageDialog.gameObject.SetActive(visible);
		speaker.textName.gameObject.SetActive(visible);
		speaker.textDialogue.gameObject.SetActive(visible);

		// Ŀ���� ��簡 ����Ǿ��� ��� Ȱ��ȭ��Ű�� ���� �׻� false
		speaker.objectArrow.SetActive(false);

		// �ʻ�ȭ ����ȭ
		//Color color = speaker.spriteRenderer.color;
		//color.a = visible == true ? 1 : 0.2f;
		//speaker.spriteRenderer.color = color;
	}
}
