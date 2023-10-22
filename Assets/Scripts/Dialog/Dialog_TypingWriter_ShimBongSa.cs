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
            tutorial_SentenceList.Add(new TutorialSentence("��û�̸� �Ⱥ��� ��Ʋ�� ������, ������ �����̴� ���� ä�� �غ���."));//0
            tutorial_SentenceList.Add(new TutorialSentence(" Z�� ������ �ֺ��� ���ǰ� ��ȣ�ۿ��� �� �ִ�."));
            tutorial_SentenceList.Add(new TutorialSentence("������ ì���. X�� ���� ������ ��� �� �ִ�."));
            tutorial_SentenceList.Add(new TutorialSentence("�� ���� ���� �Ǿ��ִ� ������ ì���."));
            tutorial_SentenceList.Add(new TutorialSentence("ä�� �������� ��û�̸� ������ �⸮ ������ ����.")); 
            tutorial_SentenceList.Add(new TutorialSentence("�������̳� �ܼ��� ���� �� Z�� ������ �׿� ���� ��ȣ�ۿ��� �Ͼ�ϴ�.")); //5
            tutorial_SentenceList.Add(new TutorialSentence("û�̰� �⸮ �쿡 ���� �ʾҴٰ� �Ѵ�."));
            tutorial_SentenceList.Add(new TutorialSentence(" ���� �� ������ �ֺ��� ���ҹ� �� ����."));
            tutorial_SentenceList.Add(new TutorialSentence(" ���ӿ����� �Ϸ�� ���� �ð��� 5���Դϴ�. �Ϸ簡 ������ ���б��� ������ ��ȯ �˴ϴ�."));
            tutorial_SentenceList.Add(new TutorialSentence("������� �̾߱⸦ ���Ƶ�, �ƹ����� û�̴� �������� ���ӿ� �Ѿ���� Ʋ������."));
            tutorial_SentenceList.Add(new TutorialSentence(" û�̸� ������ ������ �˾Ƴ��� �Ѵ�.")); //10
            tutorial_SentenceList.Add(new TutorialSentence(" �������� �˾Ƴ� ������ ������ ���ο� �ܼ��� ���� �� �ִ�."));
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
    IEnumerator StartChat(string narrator, string _narration, bool _clear)
    {
        Debug.Log("���̾�α� ����");

        isTalkEnd = false;

        //UI���� ĳ���� �̸� 
        CharacterName.text = narrator;

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

    //Ʃ�丮�� ��ȭ ��� �޼��� ����
    #region
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

        if (isTalkEnd)
        {
            //��ȭ ����� ���̾�α�â ����
            gameObject_Dialougue.SetActive(true);

            StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[1].sentence, false));
        }
    }

    //���� ȹ�� ���
    public void Start_Sentence_GetBotzime()
    {
        Debug.Log("���� ȹ�� ��ȭ");

        //��ȭ ����� ���̾�α�â ����
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[2].sentence, true));
    }

    //���� ȹ�� ���
    public void Start_Sentence_GetMap()
    {
        Debug.Log("���� ȹ�� ��ȭ");

        //��ȭ ����� ���̾�α�â ����
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[3].sentence, true));
    }

    //����, ���� �Ѵ� ȹ�� ���
    public void Start_Sentence_GetObjcets()
    {
        Debug.Log("�Ѵ� ȹ�� ��ȭ");

        //��ȭ ����� ���̾�α�â ����
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[4].sentence, true));
    }

    //���� ��� ��ȭ ������ ��ȭ
    public void Start_Sentence_BbangEnd()
    {
        Debug.Log("���� ��ȭ ��");

        //��ȭ ����� ���̾�α�â ����
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[5].sentence, true));
    }

    //�⸮�� ��ȭ ������ ��ȭ
    public void Start_Sentence_HyangEnd1()
    {
        Debug.Log("�⸮�� ��ȭ ��");

        //��ȭ ����� ���̾�α�â ����
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[6].sentence, true));
    }

    //�⸮�� ��ȭ ������ ��ȭ2
    public void Start_Sentence_HyangEnd2()
    {
        Debug.Log("�⸮�� ��ȭ ��2");

        //��ȭ ����� ���̾�α�â ����
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[7].sentence, false));
    }

    //�⸮�� ��ȭ ������ ��ȭ3
    public void Start_Sentence_HyangEnd3()
    {
        Debug.Log("�⸮�� ��ȭ ��3");

        //��ȭ ����� ���̾�α�â ����
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[8].sentence, true));
    }

    //�Ϸ簡 ������ ��ȭ
    public void Start_Sentence_PassDay()
    {
        Debug.Log("�Ϸ� ������ ��ȭ");

        //��ȭ ����� ���̾�α�â ����
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[9].sentence, true));
    }

    //�Ϸ簡 ������ ��ȭ
    public void Start_Sentence_PassDay2()
    {
        Debug.Log("�Ϸ� ������ ��ȭ2");

        //��ȭ ����� ���̾�α�â ����
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[10].sentence, true));
    }

    //�Ϸ簡 ������ ��ȭ
    public void Start_Sentence_PassDay3()
    {
        Debug.Log("�Ϸ� ������ ��ȭ3");

        //��ȭ ����� ���̾�α�â ����
        gameObject_Dialougue.SetActive(true);

        StartCoroutine(StartChat(narator_Name, tutorial_SentenceList[11].sentence, false));
    }
    #endregion
}