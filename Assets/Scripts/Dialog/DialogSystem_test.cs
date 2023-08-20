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
		public Sprite spriteRenderer; // 대사중인 NPC의 초상화
		public Image imageDialog;             // 대화나올 Image UI
		public Text textName;      // 현재 대사중인 NPC 이름 출력 Text
		public Text textDialogue;  // 대사 text
		public GameObject objectArrow;		  // 대사 완료되었을 경우 출력되는 임시 커서
	}

	[System.Serializable]
	public struct DialogData
	{
		public int speakerIndex;              // 이름과 대사를 출력할 현재 DialogSystem의 speaker 배열 순번
		public string name;                   // NPC 이름
		[TextArea(3, 5)]
		public string dialogue;				  // 대사
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
	private bool isAutoStart = false;		// 자동시작여부
	private bool isFirst = true;			// 최초 1회만 호출하기 위한 변수
	private int currentDialogIndex = -1;    // 현재 대사중인 번호
	private int currentSpeakerIndex = 0;    // 현재 대화하는 화자의 speakers 배열 대사중 번호
	private float typingSpeed = 0.1f;       // text typing 재생 속도
	private bool isTypingEffect = false;    // 현재 text typing 중인지 판별

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
		// 모든 대화 관련 오브젝트 비활성화
		for (int i = 0; i < speakers.Length; ++i)
		{
			SetActiveObjects(speakers[i], false);

			// 초상화는 보이도록 설정
			//speakers[i].spriteRenderer.SetActive(true);
			GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = speakers[i].spriteRenderer;
		}
	}

	public bool UpdateDialog()
	{
		// 대사 분기가 시작될 때 1회만 호출
		if(isFirst == true)
		{
			Setup();

			if (isAutoStart)
				SetNextDialog();

			isFirst = false;
		}
		if(Input.GetKeyDown(KeyCode.Z))
		{
			// text typing 중일 때 z키 클릭하면 타이핑 효과 종료
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
		// 이전 대화 오브젝트 비활성화
		SetActiveObjects(speakers[currentSpeakerIndex], false);

		// 다음 대사 진행
		currentDialogIndex++;

		// 현재 대사 순번 결정
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

		// 커서는 대사가 종료되었을 경우 활성화시키기 위해 항상 false
		speaker.objectArrow.SetActive(false);

		// 초상화 투명화
		//Color color = speaker.spriteRenderer.color;
		//color.a = visible == true ? 1 : 0.2f;
		//speaker.spriteRenderer.color = color;
	}
}
