using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter : MonoBehaviour
{
    // 실제 채팅이 나오는 텍스트
    public Text ChatText;

    // 캐릭터 이름이 나오는 텍스트
    public Text CharacterName;

    // 대화를 빠르게 넘길 수 있는 키(default : space)
    public List<KeyCode> skipButton;

    public string writerText = "";

    public string characternameText = "";

    bool isButtonClicked = false;

    public bool bool_isNPC = false;

    public GameObject images_NPC;

    //public Sprite[] images_NPC_portrait;
    public Sprite images_NPC_portrait;

    public TalkManager talkManager;

    public bool bool_isAction;

    public int talkIndex;

    //최초 클릭
    void Start()
    {
        StartCoroutine(TextPractice());
        //TextPractice();
        //StopCoroutine(TextPractice());
        CharacterName.text="";
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

    void Dialog(int id, bool bool_isNPC)
    {
        /*
        string dialogData = TalkManager.GetTalk(id, talkIndex);

        if (bool_isNPC)
        {
            ChatText.text = dialogData;

        }

        else
        {
            ChatText.text = dialogData;
        }
        */

    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("이건 Touch! 스님!!!!");
            StartCoroutine(TextPractice());
            //bool_isBotjim = true;
            if (bool_isNPC == true)
            {
                images_NPC.SetActive(true);
                bool_isNPC = false;

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

        Objdata obj_Data = GameObject.Find("NPC").GetComponent<Objdata>();
        //Dialog(obj_Data.key, obj_Data.bool_isNPC);

        //narrator = CharacterName.text;

        //텍스트 타이핑
        for (a = 0; a < narration.Length+1; a++)
		{
            writerText += narration[a];
            //Dialog(obj_Data.key, obj_Data.bool_isNPC) += narration[a];
            ChatText.text = writerText;

            //텍스트 타이핑 시간 조절
            //yield return null;
            yield return new WaitForSeconds(0.02f);
        }

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
        yield return StartCoroutine(NormalChat(characternameText, writerText));
        //yield return StartCoroutine(NormalChat(characternameText, writerText));
        //yield return StartCoroutine(NormalChat("나는봇짐", "?안녕하세요, 반갑습니다. 대화 전환 테스트입니다 이것은 테스트지? 그럼 테스트지 테스트야 테스트군 테스트"));
    }
}