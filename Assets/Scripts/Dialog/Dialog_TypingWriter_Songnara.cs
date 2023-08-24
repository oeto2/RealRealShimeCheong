using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Songnara : MonoBehaviour
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

    //다이얼로그 UI
    public GameObject images_NPC;

    public Sprite[] images_NPC_portrait;

    public Trigger_NPC trigger_npc;

    public bool isNPCTrigger;

    public Controller controller_scr;

    public S_NPCdatabase_Yes npcDatabaseScr;

    //최초 클릭
    void Start()
    {
        //StartCoroutine(TextPractice());
        //TextPractice();
        //StopCoroutine(TextPractice());
        CharacterName.text = "";
        ChatText.text = "";
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
   
        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            
            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false)
            {
                //다이얼로그 보이기
                images_NPC.SetActive(true);
                //대사 비우기
                writerText = "";
                //대사 출력
                StartCoroutine(TextPractice());
                //초상화 변경
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
                bool_isNPC = true;
            }
            else
            {
                //다이얼로그 종료
                images_NPC.SetActive(false);
                //대사 비우기
                writerText = "";
                //코루틴 중지
                StopAllCoroutines();
                bool_isNPC = false;
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

        //텍스트 타이핑
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
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
        yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[8].npc_name, npcDatabaseScr.NPC_01[8].comment));
    }
}