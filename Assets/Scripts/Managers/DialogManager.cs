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

    //�ܺ� ��ũ��Ʈ ����
    public Controller playerCtrlScr;

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

    //Npc �ʻ�ȭ
    public Image Npc_Portrait;

    //Player �ʻ�ȭ
    public Image player_Portrait;

    //��ȭ���� Ŀ�� ������Ʈ
    public GameObject obj_TalkEndCur;


    //�ʻ�ȭ ��������Ʈ �̹�����
    [Tooltip("0:Null, 1:����, 2:����, 3:�·�, 4:�ʹ�, 5:���, 6:���, 7:���2, 8:���, 9:��û, 10:�۳���, 11: ������")]
    public Sprite[] npc_Sprites;


    //Player ��������Ʈ �̹�����
    [Tooltip("0: Null, 1:�⺻")]
    public Sprite[] player_sprites;

    //������� ��� ��
    public string writerText = "";

    //�ý��� �޼��� �ڷ�ƾ�� �̹� ���������� Ȯ���ϴ� flag
    public bool isSentence_Start;

    //��ȭ�� ���� ��� �Ǿ�����
    public bool isSentenceEnd = false;

    //���� ��ȭ�� �� �ִ���
    public bool remainSentence = false;

    // ���ڻ� ���� ����
    bool t_white = false;   // �븻 ���
    bool t_red = false;     // ���� ���
    bool t_blue = false;    // ������
    bool t_violet = false;  // �߰� ����

    // ���ڻ� ���� ���ڴ� ��� ��� ����
    bool t_ignore = false;

    // ���� ��� ��� ����
    private int RandomNum;

    public Coroutine DialogueItemClue;

    //�ؽ�Ʈ ��ŵ�� �ߴ��� Ȯ���ϴ� flag
    public bool isSkip;
    //Ÿ���� �ӵ�
    public float typingSpeed = 0.02f;
    //��ŵ Ÿ���� �ӵ�
    public float skipTypingSpeed = 0.0005f;


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
    }

    //NPC �⺻���
    public IEnumerator NormalChat(string _narrator)
    {
        //Ŀ�� �����
        obj_TalkEndCur.SetActive(false);

        //�ð� ����
        TimeManager.instance.StopTime();

        //���̾�α� �ý�Ʈ �Ҹ� ���
        EffectSoundManager.instance.PlayTalkTextSound();

        //���̾�α� ����
        CleanDialogue();

        //Player, Npc �ʻ�ȭ �ʱ�ȭ
        ResetNpcPortrait();
        ResetPlayerPortrait();

        //���̾�α� Text ����
        CleanDialogue();

        //��ȭ �ߺ����� ����
        remainSentence = true;

        //Npc �ʻ�ȭ �ڵ� ����
        ChangeNpcPortrait(_narrator);

        //narratior ���� ���� �ش��ϴ� ���� ��簪 �Ѱ���
        string narration = RandomNpcSentence(_narrator);
        string narration_2 = RandomNpcSentence2(_narrator);
        string narration_3 = RandomNpcSentence3(_narrator);

        //�۳��� ������ ���
        if (_narrator == "�۳��� ����")
        {
            RandomNum = Random.Range(0, 3);
        }
        //�� ��
        else
        {
            RandomNum = Random.Range(0, 2);
        }

        //�ؽ�Ʈ Ÿ����
        if (RandomNum == 0)
        {
            for (int a = 0; a < narration.Length; a++)
            {
                writerText += narration[a];
                ChatText.text = writerText;

                //5���� �̻� ��ȭ�� ����ǰ� ZŰ�� ������ ���
                if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
                {
                    //Ÿ���� �ӵ� ����
                    typingSpeed = skipTypingSpeed;
                }

                //��簡 ���� ��µ��� �ʾ��� ���
                if (a < narration.Length)
                {
                    //��� Ÿ���� �ӵ�
                    yield return new WaitForSeconds(typingSpeed);
                }

            }
            yield return null;
        }
        else if (RandomNum == 1)
        {
            for (int a = 0; a < narration_2.Length; a++)
            //for (a = 0; a < textSpeed; a++)
            {
                writerText += narration_2[a];
                ChatText.text = writerText;

                //5���� �̻� ��ȭ�� ����ǰ� ZŰ�� ������ ���
                if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
                {
                    //Ÿ���� �ӵ� ����
                    typingSpeed = skipTypingSpeed;
                }

                //��簡 ���� ��µ��� �ʾ��� ���
                if (a < narration_2.Length)
                {
                    yield return new WaitForSeconds(typingSpeed);
                }
            }
            yield return null;
        }
        else if (RandomNum == 2)
        {
            for (int a = 0; a < narration_3.Length; a++)
            //for (a = 0; a < textSpeed; a++)
            {
                writerText += narration_3[a];
                ChatText.text = writerText;

                //5���� �̻� ��ȭ�� ����ǰ� ZŰ�� ������ ���
                if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
                {
                    //Ÿ���� �ӵ� ����
                    typingSpeed = skipTypingSpeed;
                }

                //��簡 ���� ��µ��� �ʾ��� ���
                if (a < narration_3.Length)
                {
                    yield return new WaitForSeconds(typingSpeed);
                }
            }
            yield return null;
        }
        Debug.Log(ChatText);

        //��� ����� ��� �Ϸ� �Ǿ��ٸ�
        if (ChatText.text == narration || ChatText.text == narration_2 || ChatText.text == narration_3)
        {
            //��ȭ���� Ŀ�� ���̱�
            obj_TalkEndCur.SetActive(true);

            //��ȭ ���� ���� ����
            remainSentence = true;
            isSentenceEnd = true;

            //Ÿ���� �ӵ� �ʱ�ȭ
            typingSpeed = 0.02f;
        }
    }

    public IEnumerator ItemClueChat(string narrator, string narration)
    {
        //Ŀ�� �����
        obj_TalkEndCur.SetActive(false);

        //�ð� ����
        TimeManager.instance.StopTime();

        //���̾�α� ����
        CleanDialogue();

        //���̾�α� â ����
        Dialouge_System.SetActive(true);

        //Player, Npc �ʻ�ȭ �ʱ�ȭ
        ResetNpcPortrait();
        ResetPlayerPortrait();

        Debug.Log("��ȭ���1");

        //������ȭ ����
        remainSentence = true;

        //���̾�α� Text ����
        CleanDialogue();

        //Npc �ʻ�ȭ �ڵ� ����
        ChangeNpcPortrait(narrator);
        //Player �ʻ�ȭ �ڵ� ����
        ChagePlayerPortrait(narrator);

        Debug.Log(narration);
        int a = 0;

        string t_letter = "";

        //�ؽ�Ʈ Ÿ����
        for (a = 0; a < narration.Length; a++)
        {
            //writerText += narration[a];
            //ChatText.text = writerText;
            switch (narration[a])
            {
                // red
                case '��':
                    t_white = false;
                    t_red = true;
                    t_blue = false;
                    t_violet = false;
                    t_ignore = true;
                    break;
                // write
                case '��':
                    t_white = true;
                    t_red = false;
                    t_blue = false;
                    t_violet = false;
                    t_ignore = true;
                    break;
                // blue
                case '��':
                    t_white = false;
                    t_red = false;
                    t_blue = true;
                    t_violet = false;
                    t_ignore = true;
                    break;
                // violet
                case '��':
                    t_white = false;
                    t_red = false;
                    t_blue = false;
                    t_violet = true;
                    t_ignore = true;
                    break;
            }

            if (!t_ignore)
            {
                if (t_white)
                {
                    t_letter = "<color=#ffffff>" + narration[a] + "</color>";    // HTML Tag
                    Debug.Log("0_write");
                }

                else if (t_red)
                {
                    //t_letter = "<color=#B40404>" + narration[a] + "</color>";
                    t_letter = "<color=#850000>" + "<b>" + narration[a] + "</b>" + "</color>";
                    Debug.Log("1_red");
                }

                else if (t_blue)
                {
                    t_letter = "<color=#0d4577>" + "<b>" + narration[a] + "</b>" + "</color>";
                    Debug.Log("2_blue");
                }

                else if (t_violet)
                {
                    t_letter = "<color=#6a2c7a>" + "<b>" + narration[a] + "</b>" + "</color>";
                    Debug.Log("3_violet");
                }
                //Debug.Log(writerText);
                writerText += t_letter; // Ư�����ڰ� �ƴ϶�� ��� ���
                ChatText.text = writerText;
                //writerText += narration[a];
                //ChatText.text = writerText;
            }
            t_ignore = false; // �� ���� ������� �ٽ� false

            //5���� �̻� ��ȭ�� ����ǰ� ZŰ�� ������ ���
            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
            {
                //Ÿ���� �ӵ� ����
                typingSpeed = skipTypingSpeed;
            }

            //��� ��� ���� ��쿡��
            if (ChatText.text != narration)
            {
                //�ؽ�Ʈ Ÿ���� �ð� ����
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        //Ÿ���� �ӵ� ����
        typingSpeed = 0.02f;

        //��ȭ ���� ���� ����
        remainSentence = true;
        isSentenceEnd = true;
    }

    //���̾�α� ��ȭ ���
    public IEnumerator ItemClueChat(string narrator, string narration, bool _remainSentence)
    {
        //Ŀ�� �����
        obj_TalkEndCur.SetActive(false);

        //�ð� ����
        TimeManager.instance.StopTime();

        //���̾�α� ����
        CleanDialogue();

        //���̾�α� â ����
        Dialouge_System.SetActive(true);

        //Player, Npc �ʻ�ȭ �ʱ�ȭ
        ResetNpcPortrait();
        ResetPlayerPortrait();


        //Player, Npc �ʻ�ȭ ���̱�
        ChangeNpcPortrait(narrator);
        ChagePlayerPortrait(narrator);

        Debug.Log("��ȭ���2");

        string t_letter = "";

        //���� ��ȭ�� ���� ���
        if (_remainSentence == true)
        {
            //������ȭ ����
            remainSentence = true;
            text_NpcName.text = narrator;

            // ���� - �ָԹ� ���̾�α� ��ȭ ���� ó��
            if (narration == npcDatabaseScr.NPC_01[869].comment)
            {
                Debug.Log(narration);
                Debug.Log("1005��, 18��");
                Dialouge_System.SetActive(false);
            }
            if (narration != npcDatabaseScr.NPC_01[869].comment)
            {
                Debug.Log(narration);
                Debug.Log("1005��, 19��");
                Dialouge_System.SetActive(true);
            }

            //�ؽ�Ʈ Ÿ����
            for (int a = 0; a < narration.Length; a++)
            {
                //writerText += narration[a];
                //ChatText.text = writerText;

                switch (narration[a])
                {
                    // red
                    case '��':
                        t_white = false;
                        t_red = true;
                        t_blue = false;
                        t_violet = false;
                        t_ignore = true;
                        break;
                    // write
                    case '��':
                        t_white = true;
                        t_red = false;
                        t_blue = false;
                        t_violet = false;
                        t_ignore = true;
                        break;
                    // blue
                    case '��':
                        t_white = false;
                        t_red = false;
                        t_blue = true;
                        t_violet = false;
                        t_ignore = true;
                        break;
                    // violet
                    case '��':
                        t_white = false;
                        t_red = false;
                        t_blue = false;
                        t_violet = true;
                        t_ignore = true;
                        break;
                }

                if (!t_ignore)
                {
                    if (t_white)
                    {
                        t_letter = "<color=#ffffff>" + narration[a] + "</color>";    // HTML Tag
                        Debug.Log("0_write");
                    }

                    else if (t_red)
                    {
                        //t_letter = "<color=#B40404>" + narration[a] + "</color>";
                        t_letter = "<color=#850000>" + "<b>" + narration[a] + "</b>" + "</color>";
                        //t_letter = "<color=#222222>" + "<b>" + narration[a] + "</b>" + "</color>";
                        Debug.Log("1_red");
                    }

                    else if (t_blue)
                    {
                        t_letter = "<color=#0d4577>" + "<b>" + narration[a] + "</b>" + "</color>";
                        Debug.Log("2_blue");
                    }

                    else if (t_violet)
                    {
                        t_letter = "<color=#6a2c7a>" + "<b>" + narration[a] + "</b>" + "</color>";
                        Debug.Log("3_violet");
                    }
                    //Debug.Log(writerText);
                    writerText += t_letter; // Ư�����ڰ� �ƴ϶�� ��� ���
                    //writerText += narration[a];
                    ChatText.text = writerText;
                    //ChatText.text = writerText;
                }
                t_ignore = false; // �� ���� ������� �ٽ� false

                //5���� �̻� ��ȭ�� ����ǰ� ZŰ�� ������ ���
                if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
                {
                    //Ÿ���� �ӵ� ����
                    typingSpeed = skipTypingSpeed;
                }

                //��� ��� ���� ��쿡��
                if (ChatText.text != narration)
                {
                    //�ؽ�Ʈ Ÿ���� �ð� ����
                    yield return new WaitForSeconds(typingSpeed);
                }

            }

            //��� ��� �� ��� ������
            yield return new WaitForSeconds(0.1f);

            //ZŰ�� �ٽ� ���� ������ ������ ���
            while (true)
            {
                if (ChatText.text == writerText && Input.GetKeyDown(KeyCode.Z))
                {
                    Debug.Log("Text ����");

                    //Ÿ���� �ӵ� �ʱ�ȭ
                    typingSpeed = 0.02f;

                    //Text ����
                    writerText = "";

                    break;
                }
                yield return null;
            }
        }
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

    #region �ý��� �޼��� ����
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
        Npc_Portrait.sprite = npc_Sprites[0];

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

        //��� ��� �� ��� ������
        yield return new WaitForSeconds(0.1f);

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
        Npc_Portrait.sprite = npc_Sprites[0];

        //�ý��� ���̾�α� Ȱ��ȭ
        Dialouge_System.SetActive(true);

        //�ؽ�Ʈ Ÿ����
        for (int a = 0; a < _narration.Length; a++)
        {
            writerText += _narration[a];
            ChatText.text = writerText;

            //5���� �̻� ��ȭ�� ����ǰ� ZŰ�� ������ ���
            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
            {
                ChatText.text = _narration;

                //for�� ���� ����
                a = _narration.Length;
            }

            //��� ��� ���� ��쿡��
            if (ChatText.text != _narration)
            {
                //�ؽ�Ʈ Ÿ���� �ð� ����
                yield return new WaitForSeconds(0.02f);
            }
        }

        //��� ��� �� ��� ������
        yield return new WaitForSeconds(0.1f);

        //ZŰ�� �ٽ� ���� ������ ������ ���
        while (true)
        {
            if (ChatText.text == _narration && Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("Text ����");

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
            StartCoroutine(SystemMessage(_narrator, _narration, _exit));
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
    #endregion

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

    //Player �ʻ�ȭ ����
    public void ChagePlayerPortrait(string _narrator)
    {
        switch (_narrator)
        {
            case "���б�":
                player_Portrait.sprite = player_sprites[1];
                break;
        }
    }

    //NPC �ʻ�ȭ ����
    public void ChangeNpcPortrait(string _narrator)
    {
        Debug.Log($"�ʻ�ȭ ���� ���� : {_narrator}");


        switch (_narrator)
        {
            case "���� ���":

                //�ʻ�ȭ ����
                Npc_Portrait.sprite = npc_Sprites[1];
                //���б� �̹��� ����
                ResetPlayerPortrait();

                //�̸� ����
                text_NpcName.text = "���� ���";
                break;

            case "����":

                //�ʻ�ȭ ����
                Npc_Portrait.sprite = npc_Sprites[2];
                //���б� �̹��� ����
                ResetPlayerPortrait();

                //�̸� ����
                text_NpcName.text = "�� ��";
                break;

            case "�·�":

                //�ʻ�ȭ ����
                Npc_Portrait.sprite = npc_Sprites[3];
                //���б� �̹��� ����
                ResetPlayerPortrait();

                //�̸� ����
                text_NpcName.text = "�� ��";
                break;

            case "�ʹ� ���":

                //�ʻ�ȭ ����
                Npc_Portrait.sprite = npc_Sprites[4];
                //���б� �̹��� ����
                ResetPlayerPortrait();

                //�̸� ����
                text_NpcName.text = "�ʹ� ���";
                break;

            case "����":

                //�ʻ�ȭ ����
                Npc_Portrait.sprite = npc_Sprites[5];
                //���б� �̹��� ����
                ResetPlayerPortrait();

                //�̸� ����
                text_NpcName.text = "����";
                break;

            case "�⸮ �� ����":

                //�ʻ�ȭ ����
                Npc_Portrait.sprite = npc_Sprites[6];
                //���б� �̹��� ����
                ResetPlayerPortrait();

                //�̸� ����
                text_NpcName.text = "�⸮ �� ����";
                break;

            case "����":

                //�ʻ�ȭ ����
                Npc_Portrait.sprite = npc_Sprites[8];
                //���б� �̹��� ����
                ResetPlayerPortrait();

                //�̸� ����
                text_NpcName.text = "����";
                break;

            case "��û":

                //�ʻ�ȭ ����
                Npc_Portrait.sprite = npc_Sprites[9];
                //���б� �̹��� ����
                ResetPlayerPortrait();

                //�̸� ����
                text_NpcName.text = "�� û";
                break;

            case "�۳��� ����":

                //�ʻ�ȭ ����
                Npc_Portrait.sprite = npc_Sprites[10];
                //���б� �̹��� ����
                ResetPlayerPortrait();

                //�̸� ����
                text_NpcName.text = "�۳��� ����";
                break;

            case "������":

                //�ʻ�ȭ ����
                Npc_Portrait.sprite = npc_Sprites[11];
                //���б� �̹��� ����
                ResetPlayerPortrait();

                //�̸� ����
                text_NpcName.text = "������";
                break;

        }
    }

    //NPC ���� ��簪1
    public string RandomNpcSentence(string _narrator)
    {
        Debug.Log($"���� ��� �� ��ȯ : {_narrator}");

        switch (_narrator)
        {
            case "���б�":
                return null;

            case "���� ���":
                return npcDatabaseScr.NPC_01[1].comment;

            case "����":
                return npcDatabaseScr.NPC_01[6].comment;

            case "�·�":
                return npcDatabaseScr.NPC_01[5].comment;

            case "�ʹ� ���":
                return npcDatabaseScr.NPC_01[2].comment;

            case "����":
                return npcDatabaseScr.NPC_01[4].comment;

            case "�⸮ �� ����":
                return npcDatabaseScr.NPC_01[3].comment;

            case "����":
                return npcDatabaseScr.NPC_01[7].comment;

            case "��û":
                return null;

            case "�۳��� ����":
                return npcDatabaseScr.NPC_01[8].comment;

            case "������":
                return npcDatabaseScr.NPC_01[563].comment;

            default:
                return null;
        }
    }

    //NPC ���� ��簪2
    public string RandomNpcSentence2(string _narrator)
    {
        Debug.Log($"���� ��� �� ��ȯ : {_narrator}");

        switch (_narrator)
        {
            case "���б�":
                return null;

            case "���� ���":
                return npcDatabaseScr.NPC_01[399].comment;

            case "����":
                return npcDatabaseScr.NPC_01[403].comment;

            case "�·�":
                return npcDatabaseScr.NPC_01[407].comment;

            case "�ʹ� ���":
                return npcDatabaseScr.NPC_01[400].comment;

            case "����":
                return npcDatabaseScr.NPC_01[402].comment;

            case "�⸮ �� ����":
                return npcDatabaseScr.NPC_01[401].comment;

            case "����":
                return npcDatabaseScr.NPC_01[404].comment;

            case "��û":
                return null;

            case "�۳��� ����":
                return npcDatabaseScr.NPC_01[405].comment;

            case "������":
                return npcDatabaseScr.NPC_01[603].comment;

            default:
                return null;
        }
    }

    //NPC ���� ��簪3
    public string RandomNpcSentence3(string _narrator)
    {
        Debug.Log($"���� ��� �� ��ȯ : {_narrator}");

        switch (_narrator)
        {
            case "���б�":
                return null;

            case "���� ���":
                return npcDatabaseScr.NPC_01[399].comment;

            case "����":
                return npcDatabaseScr.NPC_01[403].comment;

            case "�·�":
                return null;

            case "�ʹ� ���":
                return npcDatabaseScr.NPC_01[400].comment;

            case "����":
                return npcDatabaseScr.NPC_01[402].comment;

            case "�⸮ �� ����":
                return npcDatabaseScr.NPC_01[401].comment;

            case "����":
                return npcDatabaseScr.NPC_01[404].comment;

            case "��û":
                return null;

            case "�۳��� ����":
                return npcDatabaseScr.NPC_01[406].comment;

            default:
                return null;
        }
    }

    //���̾�α� ��� �� ����
    public void CleanDialogue()
    {
        ChatText.text = "";
        writerText = "";
    }

    //Npc�ʻ�ȭ �ʱ�ȭ
    public void ResetNpcPortrait()
    {
        Npc_Portrait.sprite = npc_Sprites[0];
    }

    //Player�ʻ�ȭ �ʱ�ȭ
    public void ResetPlayerPortrait()
    {
        player_Portrait.sprite = npc_Sprites[0];
    }

    //���̾�α� ����
    public void Dialouge_End()
    {
        Dialouge_System.SetActive(false);
        isSentenceEnd = false;
        remainSentence = false;
        playerCtrlScr.TalkEnd();
    }
}