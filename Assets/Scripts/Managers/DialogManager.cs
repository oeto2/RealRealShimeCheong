using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ŀ���� Ŭ������ �ν����� â���� �����ϱ� ���ؼ� �߰�
[System.Serializable]
public class Dialogue
{
	// ĳ���� �̸��� npc_name â�� ����
	[Tooltip("��� ġ�� ĳ���� �̸�")]
	public string name; // ĳ���� �̸�

	// ��� ������ ���� �迭 ����
	[Tooltip("��� ����")]
	public string[] context;
}

[System.Serializable]
public class DialogueEvent
{
	// ��ȭ �̺�Ʈ �̸� ����
	public string name;

	// vector2(x,y) x�ٺ��� y�ٱ����� ��縦 ��������
	public Vector2 line;

	// ��縦 ���� ���� �� �� �ֵ��� �迭�� ����
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
		DialogData.Add(5000, new string[] { "?�̰��� �׽�Ʈ��?", "�׷� �׽�Ʈ�� �׽�Ʈ�� �׽�Ʈ�� �׽�Ʈ��" });
	}

	public string GetTalk(int id, int idx_Dialog)
	{
		return DialogData[id][idx_Dialog];
	}
}
