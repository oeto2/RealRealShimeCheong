using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Bbang : MonoBehaviour
{
    // 실제 채팅이 나오는 텍스트
    public Text ChatText;

    // 캐릭터 이름이 나오는 텍스트
    public Text CharacterName;

    // 대화를 빠르게 넘길 수 있는 키(default : space)
    public List<KeyCode> skipButton;

    public string writerText = "";

    bool isButtonClicked = false;

    public bool bool_isBbang = false;

    public GameObject images_Bbang;

    public Sprite[] images_NPC_portrait;

    public Trigger_NPC trigger_npc;

    public Controller controller_scr;

    //외부 스크립트에서 사용하기 위한 용도(싱글톤패턴)
    public static Dialog_TypingWriter_Bbang instance;

    private void Awake()
	{

        if (Dialog_TypingWriter_Bbang.instance == null)
        {
            Dialog_TypingWriter_Bbang.instance = this;
        }
    }

    void Update()
    {

        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
            
        }
        StopCoroutine(TextPractice());
        dialogstart();
        /*if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(TextPractice());
        }*/
    }

    public void dialogstart()
    {
        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("z키 누름! Bbang!!!!");
            StartCoroutine(TextPractice());

            controller_scr.TalkStart();
            //bool_isBotjim = true;
            if (bool_isBbang == true)
            {
                trigger_npc.isNPCTrigger = true;
                images_Bbang.SetActive(true);
                bool_isBbang = false;
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
            }
            else
            {
                images_Bbang.SetActive(false);
                bool_isBbang = true;
                trigger_npc.isNPCTrigger = false;
                StopCoroutine(TextPractice());
            }
        }
    }

    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        CharacterName.text = narrator;
        writerText = "";

        //텍스트 타이핑
        for (a = 0; a < narration.Length; a++)
        //for (a = 0; a < textSpeed; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            //텍스트 타이핑 시간 조절
            //yield return null;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;

        //키(default : space)를 다시 누를 때까지 무한정 대기
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
        yield return StartCoroutine(NormalChat("뺑덕어멈", "호호, 무슨 일이신가요?"));
        yield return StartCoroutine(NormalChat("뺑덕어멈", "이번에 들여온 비녀가 그렇게 예쁘던데,,,"));
    }
}