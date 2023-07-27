using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


//Tutorial Sentence�� �����ص� Class
[System.Serializable]
public class TutorialSentence
{
    public TutorialSentence(string _sentence)
    {
        sentence = _sentence;
    }

    public string sentence;
}

public class Dialog_TypingWriter_ShimBongSa : MonoBehaviour
{
    // ���� ä���� ������ �ؽ�Ʈ
    public Text ChatText;

    // ĳ���� �̸��� ������ �ؽ�Ʈ
    public Text CharacterName;

    // ��ȭ�� ������ �ѱ� �� �ִ� Ű(default : space)
    public List<KeyCode> skipButton;

    public string writerText = "";

    bool isButtonClicked = false;

    public bool bool_isBbang = false;

    //Dialogue UI Objcet
    public GameObject gameObject_Dialougue;

    //Ʃ�丮�� ��ȭ ����Ʈ
    public List<TutorialSentence> tutorial_SentenceList;

    //��ȭ �������� �̸�
    [Tooltip("String : �ɺ���")]
    private string narator_Name = "�ɺ���";

    //��ȭ �������� Ȯ���ϴ� falg
    [Tooltip("��ȭ���� : False, ��ȭ �� : true")]
    public bool isTalkEnd;

    private void Start()
    {
        //Ʃ�丮�� ��ȭ ����
        {
            tutorial_SentenceList.Add(new TutorialSentence("��û�̸� �Ⱥ��� ��Ʋ�� ������, ������ �����̴� ���� ä�� �غ���."));
            tutorial_SentenceList.Add(new TutorialSentence(" Z�� ������ �ֺ��� ���ǰ� ��ȣ�ۿ��� �� �ִ�."));
            tutorial_SentenceList.Add(new TutorialSentence("������ ì���. X�� ���� ������ ��� �� �ִ�."));
            tutorial_SentenceList.Add(new TutorialSentence("�� ���� ���� �Ǿ��ִ� ������ ì�ܾ� �Ѵ�."));
            tutorial_SentenceList.Add(new TutorialSentence("ä�� �������� ��û�̸� ������ �⸮ ������ ����."));
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
    }
  

    //��ȭ ����
    IEnumerator StartChat(string narrator, string narration, bool _clear)
    {
        Debug.Log("���̾�α� ����");

        isTalkEnd = false;

        int a = 0;

        //UI���� ĳ���� �̸� 
        CharacterName.text = narrator;

        //True : Textâ ���� ��ȭ ����, False : ���� ��ȭ �̾ ��ȭ ����
        if(_clear)
        {
            writerText = "";
        }

        //�ؽ�Ʈ Ÿ����(���� string�� ���̸�ŭ)
        for (a = 0; a < narration.Length; a++)
        //for (a = 0; a < textSpeed; a++)
        {
            //�����뿡 1���ھ� ���ϱ�
            writerText += narration[a];
            ChatText.text = writerText;

            //�ؽ�Ʈ Ÿ���� �ð� ����
            //yield return null;
            yield return new WaitForSeconds(0.05f);
        }

        isTalkEnd = true;
        //yield return null;

        ////Ű(default : space)�� �ٽ� ���� ������ ������ ���
        //while (true)
        //{
        //    if (isButtonClicked)
        //    {
        //        isButtonClicked = false;
        //        break;
        //    }
        //    yield return null;
        //}
    }

    //IEnumerator TextPractice()
    //{
    //    //yield return StartCoroutine(NormalChat("�������", "ȣȣ, ���� ���̽Ű���?"));
    //    //yield return StartCoroutine(NormalChat("�������", "�̹��� �鿩�� ��డ �׷��� ���ڴ���,,,"));
    //}

    //1�� ��ȭ ���� (Tutorial Manager���� ����)
    public void Start_Sentence1()
    {
        Debug.Log("���̾�α� ��ȭ1");

        //��ȭ ����� ���̾�α�â ����
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[0].sentence, true));
    }

    public void Start_Sentence1_2()
    {
        Debug.Log("���̾�α� ��ȭ1_2");

        if(isTalkEnd)
        {
            //��ȭ ����� ���̾�α�â ����
            gameObject_Dialougue.SetActive(true);

            StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[1].sentence, false));
        }
        
    }
}