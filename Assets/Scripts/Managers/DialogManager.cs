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

    //NPC �̸� Text
    public Text text_NpcName;

    //�ý��� �ʻ�ȭ
    public Image System_Portrait;

    //�ʻ�ȭ ��������Ʈ �̹�����
    [Tooltip("0:����, 1:����, 2:����, 3:����, 4:�ʹ�, 5:���, 6:���, 7:���2, 8:���, 9:��û, 10:�۳���")]
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

    //�ش��ϴ� �ε��� ���� NPC �̸��� ��ȯ���ִ� �޼���
    public string GetNpcName(int _indexNum)
    {
        return npcDatabaseScr.NPC_01[_indexNum].npc_name;
    }


    //�ý��� �޼��� �ڷ�ƾ
    IEnumerator SystemMessage(string _narration, bool _exit)
    {
        //���̾�α� ����
        CleanDialogue();

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

                if (_exit)
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

    //�ý��� �޼��� �ڷ�ƾ
    IEnumerator SystemMessage(string _narrator, string _narration, bool _exit)
    {
        //ȭ�ڿ� ���� �ʻ�ȭ, �̸� ����
        ChangeNpcPortrait(_narrator);

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

                if (_exit)
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

    //�ý��� �޼����� �������ִ� �޼���(�̸� ����)
    public void Start_SystemMessage(string _narrator, string _narration, bool _exit)
    {
        //�ڷ�ƾ �ߺ����� ����
        if (!isSentence_Start)
        {
            StartCoroutine(SystemMessage(_narrator,_narration, _exit));
        }
    }

    //�ý��� �޼����� ������ true, �ƴϸ� false ��ȯ)
    public bool IsSystemMessageEnd()
    {
        //���̾�α� â�� ���� �Ǿ��ٸ�
        if (Dialouge_System.activeSelf == false)
        {
            return true;
        }

        //�ƴϸ�
        else
        {
            return false;
        }
    }

    #region ���� �̺�Ʈ ���
    //���� �ֱ� ��� ���
    IEnumerator SystemMessage_HerbSentence()
    {
        //���� �ֱ� �ý��� �޼��� ���
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(530), true));

        //���Ŀ� 7398�� �����ϱ�
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(531), true));

        //���̾�α� ����
        Dialouge_System.SetActive(false);
    }

    //���� �ֱ� ��� ����
    public void StartPushHerbSentence()
    {
        StartCoroutine(SystemMessage_HerbSentence());
    }

    #endregion


    #region ������ �̺�Ʈ ���

    //���ٰ��� ���
    IEnumerator SystemMessage_WaterBagage()
    {
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(522), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(282), true));
    }

    //���ٰ��� ���2
    IEnumerator SystemMessage_WaterBagage_2()
    {
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(522), true));
    }

    //�� �ٰ��� ��� ���
    public void Start_WaterBageSentence()
    {
        StartCoroutine(SystemMessage_WaterBagage());
    }

    //�� �ٰ��� ��� ���2
    public void Start_WaterBageSentence_2()
    {
        StartCoroutine(SystemMessage_WaterBagage_2());
    }


    //�ܿ� �Ҹ� �־��� ��� ���
    IEnumerator SystemMessage_RiceSentence()
    {
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(523), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(291), true));
    }

    //�ܿ� �Ҹ� �־��� ��� ��� ���
    public void Start_RiceSentence()
    {
        StartCoroutine(SystemMessage_RiceSentence());
    }

    #endregion

    //���� 3 ���
    IEnumerator BoatMan3_Sentence()
    {
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(604), true));
    }

    //����3 ������
    public void Start_BoatMan3_Sentence()
    {
        StartCoroutine(BoatMan3_Sentence());
    }

    //�����ұ� ������ȭ
    IEnumerator BadEndingSentence()
    {
        //��� ��Ӱ� ����
        EndingManager.instance.ShowEndingBG();

        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(419), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(420), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(421), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(422), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(423), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcSentence(424), true));

        //Ÿ��Ʋ �̵�
        EndingManager.instance.LoadTitleScene();
    }

    //�����ұ� ���� ����
    public void StartBadEndingSentence()
    {
        StartCoroutine(BadEndingSentence());
    }

    //������ ������ȭ
    IEnumerator BadEndingSentence2()
    {
        //��� ��Ӱ� ����
        EndingManager.instance.ShowEndingBG();

        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(795), DialogManager.instance.GetNpcSentence(795), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(796), DialogManager.instance.GetNpcSentence(796), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(797), DialogManager.instance.GetNpcSentence(797), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(798), DialogManager.instance.GetNpcSentence(798), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(799), DialogManager.instance.GetNpcSentence(799), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(800), DialogManager.instance.GetNpcSentence(800), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(801), DialogManager.instance.GetNpcSentence(801), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(802), DialogManager.instance.GetNpcSentence(802), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(803), DialogManager.instance.GetNpcSentence(803), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(804), DialogManager.instance.GetNpcSentence(804), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(805), DialogManager.instance.GetNpcSentence(805), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(806), DialogManager.instance.GetNpcSentence(806), true));
        yield return StartCoroutine(SystemMessage(DialogManager.instance.GetNpcName(807), DialogManager.instance.GetNpcSentence(807), true));

        //Ÿ��Ʋ �̵�
        EndingManager.instance.LoadTitleScene();
    }

    //������ ���� ����
    public void StartBadEndingSentence2()
    {
        //������ ���� ����
        StartCoroutine(BadEndingSentence2());
    }

    //NPC �ʻ�ȭ ����
    public void ChangeNpcPortrait(string _narrator)
    {
        Debug.Log($"�ʻ�ȭ ���� ���� : {_narrator}");
        

        switch (_narrator)
        {
            case "���б�":

                //�ʻ�ȭ ����
                System_Portrait.sprite = sprites[0];

                //�̸� ����
                text_NpcName.text = "���б�";
                break;

            case "�������":

                //�ʻ�ȭ ����
                System_Portrait.sprite = sprites[1];

                //�̸� ����
                text_NpcName.text = "�������";
                break;

            case "����":

                //�ʻ�ȭ ����
                System_Portrait.sprite = sprites[2];

                //�̸� ����
                text_NpcName.text = "����";
                break;

            case "����":

                //�ʻ�ȭ ����
                System_Portrait.sprite = sprites[3];

                //�̸� ����
                text_NpcName.text = "����";
                break;

            case "�ʹ����":

                //�ʻ�ȭ ����
                System_Portrait.sprite = sprites[4];

                //�̸� ����
                text_NpcName.text = "�ʹ����";
                break;

            case "����":

                //�ʻ�ȭ ����
                System_Portrait.sprite = sprites[5];

                //�̸� ����
                text_NpcName.text = "����";
                break;

            case "�⸮ �� ����":

                //�ʻ�ȭ ����
                System_Portrait.sprite = sprites[6];

                //�̸� ����
                text_NpcName.text = "�⸮ �� ����";
                break;

            case "����":

                //�ʻ�ȭ ����
                System_Portrait.sprite = sprites[8];

                //�̸� ����
                text_NpcName.text = "����";
                break;

            case "��û":

                //�ʻ�ȭ ����
                System_Portrait.sprite = sprites[9];

                //�̸� ����
                text_NpcName.text = "��û";
                break;

            case "�۳��� ����":

                //�ʻ�ȭ ����
                System_Portrait.sprite = sprites[10];

                //�̸� ����
                text_NpcName.text = "�۳��� ����";
                break;

        }
    }

    //���̾�α� ����
    public void CleanDialogue()
    {
        //NPC �̸� ����
        text_NpcName.text = "";
    }
}
