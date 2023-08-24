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

    public string characternameText = "";

    bool isButtonClicked = false;

    public bool bool_isNPC = false;

    public GameObject images_Bbang;

    public Sprite[] images_NPC_portrait;

    public Trigger_NPC trigger_npc;

    public Controller controller_scr;

    // 랜덤 대사 출력 변수
    private int RandomNum;

    // 랜덤 대사 출력 저장 배열
    public string[] Random_text_array;

    [System.Serializable]
    public struct DialogData
    {
        public int speakerIndex;              // 이름과 대사를 출력할 현재 DialogSystem의 speaker 배열 순번
        public string name;                   // NPC 이름
        [TextArea(3, 5)]
        public string dialogue;               // 대사
    }

    [SerializeField]
    public int index;
    [SerializeField]
    public S_NPCdatabase_Yes dialogdb;
    [SerializeField]
    private DialogData[] dialogs;

    //외부 스크립트에서 사용하기 위한 용도(싱글톤패턴)
    public static Dialog_TypingWriter_Bbang instance;
    void Start()
    {
        CharacterName.text = "";
        ChatText.text = "";
    }
    private void Awake()
	{

        if (Dialog_TypingWriter_Bbang.instance == null)
        {
            Dialog_TypingWriter_Bbang.instance = this;
        }

        for (int i = 4999; i < dialogdb.NPC_01.Count; ++i)
        {
            if (dialogdb.NPC_01[i].index_num == index)
            {
                dialogs[index].name = dialogdb.NPC_01[i].npc_name;
                dialogs[index].dialogue = dialogdb.NPC_01[i].comment;
                index++;
            }
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
        //StopCoroutine(TextPractice());
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

            controller_scr.TalkStart();
            //bool_isBotjim = true;
            if (bool_isNPC == false)
            {
                StartCoroutine(TextPractice());
                trigger_npc.isNPCTrigger = true;
                images_Bbang.SetActive(true);
                bool_isNPC = true;
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
            }
            else
            {
                images_Bbang.SetActive(false);
                bool_isNPC = false;
                trigger_npc.isNPCTrigger = false;

                writerText = "";
                StopAllCoroutines();
            }
        }
    }

    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        narrator = characternameText = CharacterName.text = dialogdb.NPC_01[1].npc_name;
        narration = dialogdb.NPC_01[1].comment;
        string narration_2 = dialogdb.NPC_01[399].comment;
        RandomNum = Random.Range(0, 2);
        Debug.Log(RandomNum);

        if(RandomNum == 0)
		{
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
        }
        else if(RandomNum == 1)
		{
            for (a = 0; a < narration_2.Length; a++)
            //for (a = 0; a < textSpeed; a++)
            {
                writerText += narration_2[a];
                ChatText.text = writerText;

                //텍스트 타이핑 시간 조절
                //yield return null;
                yield return new WaitForSeconds(0.05f);
            }
            yield return null;
        }
        Debug.Log(writerText);
        //writerText = "";

        /*
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
        */

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
        //yield return StartCoroutine(NormalChat("뺑덕어멈", "호호, 무슨 일이신가요?"));
        //yield return StartCoroutine(NormalChat("뺑덕어멈", "이번에 들여온 비녀가 그렇게 예쁘던데,,,"));
        yield return StartCoroutine(NormalChat(characternameText, writerText));
    }
}