using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_BusinessMan1 : MonoBehaviour
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

    public GameObject images_Bbang;

    public Sprite images_NPC_portrait;

    void Update()
    {

        foreach (var element in skipButton) // ��ư �˻�
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }
        StopCoroutine(TextPractice());
        /*if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(TextPractice());
        }*/
    }

    public void OnMouseDown()
    {
        //if (Input.GetMouseButtonDown(0))
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("�̰� Touch! ����!!!!");
            StartCoroutine(TextPractice());
            //bool_isBotjim = true;
            if (bool_isBbang == true)
            {
                images_Bbang.SetActive(true);
                bool_isBbang = false;

                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait;
            }
            else
            {
                images_Bbang.SetActive(false);
                bool_isBbang = true;
                StopCoroutine(TextPractice());
            }
        }
    }

    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        CharacterName.text = narrator;
        writerText = "";

        //�ؽ�Ʈ Ÿ����
        for (a = 0; a < narration.Length; a++)
        //for (a = 0; a < textSpeed; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            //�ؽ�Ʈ Ÿ���� �ð� ����
            //yield return null;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;

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
        yield return StartCoroutine(NormalChat("����", "�� �� �� �ּ�?"));
        yield return StartCoroutine(NormalChat("����", "�� �� ������ �� ��Ű�ÿ�! �� �Ѱڳ�."));
    }
}