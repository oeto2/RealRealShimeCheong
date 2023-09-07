using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //�̱���
    public static DialogManager instance = null;

    //��ȭ �����ͺ��̽�
    public S_NPCdatabase_Yes npcDatabaseScr;

    //�ý��� ���̾�α� ������Ʈ
    public GameObject Dialouge_System;

    //�ý��� ���̾�α� �ؽ�Ʈ
    public Text ChatText;

    //�ý��� �ʻ�ȭ
    public Image System_Portrait;

    //�ʻ�ȭ ��������Ʈ �̹�����
    public Sprite[] sprites;

    //������� ��� ��
    public string writerText = "";

    //�ý��� �޼��� �ڷ�ƾ�� �̹� ���������� Ȯ���ϴ� flag
    public bool isSentence_Start;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }

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

    //�ش��ϴ� �ε��� ���� ��ȭ�� ��ȯ���ִ� �޼���
    public string GetNpcSentence(int _indexNum)
    {
        return npcDatabaseScr.NPC_01[_indexNum].comment;
    }


    //�ý��� �޼��� �ڷ�ƾ
    IEnumerator SystemMessage(string _narration, bool _exit)
    {
        //�ڷ�ƾ �ߺ� ���� ����
        isSentence_Start = true;
        //Text ����
        writerText = "";

        //�ʻ�ȭ ����
        System_Portrait.sprite = sprites[0];

        //�ý��� ���̾�α� Ȱ��ȭ
        Dialouge_System.SetActive(true);

        int a = 0;

        for (a = 0; a < _narration.Length; a++)
        //for (a = 0; a < textSpeed; a++)
        {
            writerText += _narration[a];
            ChatText.text = writerText;

            //�ؽ�Ʈ Ÿ���� �ð� ����
            //yield return null;

            if (a > 2 && Input.GetKeyDown(KeyCode.Z))
            {
                //�ڷ�ƾ �ߺ� ���� ��������
                isSentence_Start = false;

                //�ý��� ���̾�α� ��Ȱ��ȭ
                //Dialouge_System.SetActive(false);
            }

            yield return new WaitForSeconds(0.02f);
        }
        yield return null;

        //ZŰ�� �ٽ� ���� ������ ������ ���
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //�ڷ�ƾ �ߺ� ���� ��������
                isSentence_Start = false;

                if(_exit)
                {
                    //�ý��� ���̾�α� ��Ȱ��ȭ
                    Dialouge_System.SetActive(false);
                }

                //Text ����
                writerText = "";
                break;
            }
            yield return null;
        }
    }

    //�ý��� �޼����� �������ִ� �޼���
    public void Start_SystemMessage(string _narration, bool _exit)
    {
        //�ڷ�ƾ �ߺ����� ����
        if (!isSentence_Start)
        {
            StartCoroutine(SystemMessage(_narration, _exit));
        }
    }
}
