using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 커스텀 클래스를 인스펙터 창에서 수정하기 위해서 추가
[System.Serializable]
public class Dialogue
{
	// 캐릭터 이름을 npc_name 창에 띄우기
	[Tooltip("대사 치는 캐릭터 이름")]
	public string name; // 캐릭터 이름

	// 대사 내용을 담을 배열 정의
	[Tooltip("대사 내용")]
	public string[] context;
}

[System.Serializable]
public class DialogueEvent
{
	// 대화 이벤트 이름 정의
	public string name;

	// vector2(x,y) x줄부터 y줄까지의 대사를 가져오기
	public Vector2 line;

	// 대사를 여러 명이 할 수 있도록 배열로 생성
	public Dialogue[] dialogues;
}

public class DialogManager : MonoBehaviour
{
	Dictionary<int, string[]> DialogData;

	void Awake()
	{
		DialogData = new Dictionary<int, string[]>();
		GenerateData();
	}

	void GenerateData()
	{
		DialogData.Add(5000, new string[] { "?이것은 테스트지?", "그럼 테스트지 테스트야 테스트군 테스트똻" });
	}

	public string GetTalk(int id, int idx_Dialog)
	{
		return DialogData[id][idx_Dialog];
	}
}
