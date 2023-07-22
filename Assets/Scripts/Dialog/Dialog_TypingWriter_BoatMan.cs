using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_BoatMan : MonoBehaviour
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

    //public Sprite[] images_NPC_portrait;

    public Sprite images_NPC_portrait;


    //���� Ŭ��
    void Start()
    {
        StartCoroutine(TextPractice());
        //TextPractice();
        //StopCoroutine(TextPractice());
        CharacterName.text="";
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
        TextPractice();
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


    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("�̰� Touch! ����!!!!");
            StartCoroutine(TextPractice());
            //bool_isBotjim = true;
            if (bool_isNPC == true)
            {
                images_NPC.SetActive(true);
                bool_isNPC = false;

                //GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[1];
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait;
            }
            else
            {
                images_NPC.SetActive(false);
               // images_NPC_portrait.SetActive(false);
                bool_isNPC = true;
                StopCoroutine(TextPractice());
            }
        }
    }

    IEnumerator NormalChat(string narrator, string narration)
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
        yield return StartCoroutine(NormalChat(characternameText, writerText));
        //yield return StartCoroutine(NormalChat(characternameText, writerText));
        //yield return StartCoroutine(NormalChat("���º���", "?�ȳ��ϼ���, �ݰ����ϴ�. ��ȭ ��ȯ �׽�Ʈ�Դϴ� �̰��� �׽�Ʈ��? �׷� �׽�Ʈ�� �׽�Ʈ�� �׽�Ʈ�� �׽�Ʈ"));
    }
}