using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Budhist : MonoBehaviour
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

    public Sprite[] images_NPC_portrait;
    
    public Trigger_NPC trigger_npc;

    public bool isNPCTrigger;

    public Controller controller_scr;

    // 랜덤 대사 출력 변수
    private int RandomNum;

    //최초에만 출력되도록 하는 확인용
    public bool isNPC_Start = true;

    //대화가 전부 출력 되었는지
    public bool isSentenceEnd = false;

    //남은 대화가 더 있는지
    public bool remainSentence = false;

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

    //최초 클릭
    void Start()
    {
        CharacterName.text = "";
        ChatText.text = "";
    }

    void Awake()
	{
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

	public void Update()
    {
        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger && UIManager.instance.SentenceCondition()
             && TutorialManager.instance.SentenceCondition())
        {
            Debug.Log("z키 누름! 승려!!!!");

            //controller_scr.TalkStart();
            if (bool_isNPC == false && !DialogManager.instance.remainSentence)
            {
                StartCoroutine(TextPractice());
                images_NPC.SetActive(true);
                bool_isNPC = true;
                Trigger_NPC.instance.isNPCTrigger = true;
            }
            else if(DialogManager.instance.isSentenceEnd)
            {
                images_NPC.SetActive(false);
                // images_NPC_portrait.SetActive(false);
                //대사 비우기
                StopAllCoroutines();
                Trigger_NPC.instance.isNPCTrigger = false;
                bool_isNPC = false;
                //Controller.instance.TalkEnd();
                controller_scr.TalkEnd();

                //남은대화 없음
                DialogManager.instance.remainSentence = false;
                //대화 끝
                DialogManager.instance.isSentenceEnd = false;
                //텍스트 비우기
                DialogManager.instance.writerText = "";
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        isNPCTrigger = true;
        if (other.CompareTag("Player"))
        {
            OnClickdown();
        }
    }
        public void OnClickdown()
        {
        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("이건 Touch! 승려!!!!");
            //StartCoroutine(TextPractice());
            //bool_isBotjim = true;
            if (bool_isNPC == true)
            {
                Controller.instance.TalkStart();
                images_NPC.SetActive(true);
                bool_isNPC = false;

                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
            }
            else
            {
                images_NPC.SetActive(false);
               // images_NPC_portrait.SetActive(false);
                bool_isNPC = true;
                //StopCoroutine(TextPractice());
                Controller.instance.TalkEnd();
            }
        }
    }



    IEnumerator TextPractice()
    {
        // 최초 1회 출력
        if (isNPC_Start==true)
        {
            Debug.Log("승려 1회 대사 출력");
            //공양미 삼백석 단서 획득
            ObjectManager.instance.GetClue(2010);
            isNPC_Start = false;
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[0].npc_name, dialogdb.NPC_01[0].comment));
        }

        #region 단서
        //2001 : 청이의 거짓말
        else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[26].npc_name, dialogdb.NPC_01[26].comment));
        }
        //2002 : 청이의 행방
        else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[34].npc_name, dialogdb.NPC_01[34].comment));
        }
        //2003 : 청이와 장터
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[42].npc_name, dialogdb.NPC_01[42].comment));
        }
        //2004 : 청이와 사내
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[50].npc_name, dialogdb.NPC_01[50].comment));
        }
        //2005 : 누군가의 아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[61].npc_name, dialogdb.NPC_01[61].comment));
        }
        //2006 : 송나라 상인과 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[69].npc_name, dialogdb.NPC_01[69].comment));
        }
        //2007 : 승려와 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[82].npc_name, dialogdb.NPC_01[82].comment));
        }
        //2008 : 승려의 마음
        else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[90].npc_name, dialogdb.NPC_01[90].comment));
        }
        //2009 : 청이의 도움
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[98].npc_name, dialogdb.NPC_01[98].comment));
        }
        //2010 : 공양미 삼백 석
        else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[108].npc_name, dialogdb.NPC_01[108].comment));
        }
        //2011 : 공양미의 출처
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[117].npc_name, dialogdb.NPC_01[117].comment));
        }
        //2014 : 잠잠해져야 할 물살
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            // 기본 대사와 같음, 생략 가능할지도
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[141].npc_name, dialogdb.NPC_01[141].comment));
        }
        //2015 : 청이가 사간 것
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[149].npc_name, dialogdb.NPC_01[149].comment));
        }
        //2017 : 배의 출항
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[165].npc_name, dialogdb.NPC_01[165].comment));
        }
        //2021 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[211].npc_name, dialogdb.NPC_01[211].comment));
        }
        //2023 : 3월 보름날
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[233].npc_name, dialogdb.NPC_01[233].comment));
        }

        #endregion

        #region 아이템
        //1000 : 쌀
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[253].npc_name, dialogdb.NPC_01[253].comment));
        }
        //1005 : 주먹밥
        else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[296].npc_name, dialogdb.NPC_01[296].comment));
        }
        //1007 : 먹
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[311].npc_name, dialogdb.NPC_01[311].comment));
        }
        //1009 : 꽃
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[326].npc_name, dialogdb.NPC_01[326].comment));
        }
        //1011 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[342].npc_name, dialogdb.NPC_01[342].comment));
        }
        //1014 : 새끼줄2
        else if (ObjectManager.instance.GetEquipObjectKey() == 1014)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[865].npc_name, dialogdb.NPC_01[865].comment));
        }
        #endregion

        #region 조합 단서
        //4023 : 공양미를 구한 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[353].npc_name, dialogdb.NPC_01[353].comment));
        }
        //4033 : 무역의 중단
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[386].npc_name, dialogdb.NPC_01[386].comment));
        }
        //4018 : 청이의 가출
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[396].npc_name, dialogdb.NPC_01[396].comment));
        }
        #endregion

        #region 기본 대사
        else
        {
            yield return StartCoroutine(DialogManager.instance.NormalChat("승려"));
        }
        #endregion
    }

    #region PreviousCode
    //IEnumerator NormalChat_4999(string narrator, string narration)
    //{
    //    int a = 0;
    //    /*
    //    // 글자색 설정 변수
    //    bool t_white = false;
    //    bool t_red = false;

    //    // 글자색 설정 문자는 대사 출력 무시
    //    bool t_ignore = false;
    //    */
    //    //CharacterName.text = narrator;
    //    narrator = characternameText = dialogdb.NPC_01[0].npc_name;
    //    //CharacterName.text = narrator;
    //    writerText = dialogdb.NPC_01[0].comment;
    //    Debug.Log(characternameText);
    //    //writerText = "";

    //    //narrator = CharacterName.text;
    //    //yield return null;
    //    //텍스트 타이핑
    //    for (a = 0; a < narration.Length; a++)
    //    //for (a = 0; a < 62; a++)
    //    {
    //        /*string t_letter = narration[a].ToString();
    //        //string t_letter;
    //        switch (narration[a])
    //        {
    //            case 'ⓡ':
    //                t_white = false;
    //                t_red = true;
    //                t_ignore = true;
    //                break;
    //            //case 'ⓦ':
    //                //t_white = true;
    //                //t_red = false;
    //                //t_ignore = true;
    //                break;
    //        }
    //        if (t_ignore==true)
    //        {
    //            if (t_white)
    //            {
    //                t_letter = "<color=#ffffff>" + narration[a] + "</color>";    // HTML Tag
    //	Debug.Log(t_letter);
    //                Debug.Log('1');

    //}

    //else if (t_red)
    //            {
    //                t_letter = "<color=#B40404>" + narration[a] + "</color>";
    //                Debug.Log(t_letter);
    //                Debug.Log('2');
    //            }
    //            Debug.Log(writerText);
    //            //ChatText.text = writerText;
    //            //writerText += t_letter; // 특수문자가 아니라면 대사 출력
    //            //writerText += narration[a];
    //            //ChatText.text = writerText;
    //            //t_ignore = false; // 한 글자 찍었으면 다시 false
    //        }*/

    //        writerText += narration[a];
    //        ChatText.text = writerText;
    //        //t_ignore = false; // 한 글자 찍었으면 다시 false
    //        //ChatText.text = t_letter;
    //        //writerText += t_letter; // 특수문자가 아니라면 대사 출력
    //        //ChatText.text = writerText;
    //        //텍스트 타이핑 시간 조절
    //        yield return new WaitForSeconds(0.07f);
    //    }
    //    yield return null;
    //    //키(default : space)를 다시 누를 때까지 무한정 대기
    //    while (true)
    //    {
    //        if (isButtonClicked)
    //        {
    //            isButtonClicked = false;
    //            break;
    //        }
    //        yield return null;
    //    }
    //}

    //IEnumerator NormalChat()
    //{
    //    //대화 중복실행 방지
    //    remainSentence = true;

    //    string narrator = characternameText = CharacterName.text = dialogdb.NPC_01[5].npc_name;
    //    string narration = dialogdb.NPC_01[5].comment;
    //    string narration_2 = dialogdb.NPC_01[407].comment;
    //    RandomNum = Random.Range(0, 2);
    //    isNPC_Start = false;
    //    //narrator = CharacterName.text;

    //    //텍스트 타이핑
    //    if (RandomNum == 0)
    //    {
    //        for (int a = 0; a < narration.Length; a++)
    //        {
    //            writerText += narration[a];
    //            ChatText.text = writerText;

    //            //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
    //            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
    //            {
    //                ChatText.text = narration;

    //                //남은대화 없음
    //                remainSentence = true;
    //                //대화 끝
    //                isSentenceEnd = true;

    //                //for문 조건 충족
    //                a = narration.Length;
    //                ////대화 끝
    //                //isSentenceEnd = true;
    //            }

    //            //대사가 전부 출력되지 않았을 경우
    //            if (a < narration.Length)
    //            {
    //                //대사 타이핑 속도
    //                yield return new WaitForSeconds(0.02f);
    //            }

    //        }
    //        yield return null;
    //    }
    //    else if (RandomNum == 1)
    //    {
    //        for (int a = 0; a < narration_2.Length; a++)
    //        //for (a = 0; a < textSpeed; a++)
    //        {
    //            writerText += narration_2[a];
    //            ChatText.text = writerText;

    //            //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
    //            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
    //            {
    //                ChatText.text = narration_2;

    //                //남은대화 없음
    //                remainSentence = true;
    //                //대화 끝
    //                isSentenceEnd = true;

    //                //for문 조건 충족
    //                a = narration_2.Length;
    //                ////대화 끝
    //                //isSentenceEnd = true;
    //            }

    //            //대사가 전부 출력되지 않았을 경우
    //            if (a < narration_2.Length)
    //            {
    //                yield return new WaitForSeconds(0.02f);
    //            }
    //        }
    //        yield return null;
    //    }
    //    Debug.Log(writerText);

    //    //대사 출력이 모두 완료 되었다면
    //    if (ChatText.text == narration || ChatText.text == narration_2)
    //    {
    //        //대화 종료 조건 충족
    //        remainSentence = true;
    //        isSentenceEnd = true;
    //    }
    //}

    ////오버로드
    //IEnumerator ItemClueChat(string narrator, string narration)
    //{
    //    //다이얼로그창 띄우기
    //    images_NPC.SetActive(true);

    //    //첫대사 플래그 false
    //    isNPC_Start = false;

    //    //남은대화 있음
    //    remainSentence = true;

    //    Debug.Log(narration);
    //    CharacterName.text = narrator;
    //    //characternameText = narrator;
    //    //narrator = CharacterName.text;

    //    //심학규의 대사일경우
    //    if (narrator == "심학규")
    //    {
    //        //초상화 변경
    //        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[1];
    //    }
    //    else
    //    {
    //        //초상화 변경
    //        GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
    //    }

    //    //텍스트 타이핑
    //    for (int a = 0; a < narration.Length; a++)
    //    {
    //        writerText += narration[a];
    //        ChatText.text = writerText;

    //        //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
    //        if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
    //        {
    //            ChatText.text = narration;

    //            //남은대화 없음
    //            remainSentence = true;
    //            //대화 끝
    //            isSentenceEnd = true;

    //            //for문 조건 충족
    //            a = narration.Length;
    //        }

    //        //대사 출력 중일 경우에만
    //        if (ChatText.text != narration)
    //        {
    //            //텍스트 타이핑 시간 조절
    //            yield return new WaitForSeconds(0.02f);
    //        }
    //    }

    //    //대사 출력이 모두 완료 되었다면
    //    if (ChatText.text == narration)
    //    {
    //        //대화 종료 조건 충족
    //        remainSentence = true;
    //        isSentenceEnd = true;
    //    }
    //}

    //IEnumerator ItemClueChat(string narrator, string narration, bool _remainSentence)
    //{
    //        //심학규의 대사일경우
    //        if (narrator == "심학규")
    //        {
    //            //초상화 변경
    //            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[1];
    //        }
    //        else
    //        {
    //            //초상화 변경
    //            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
    //        }

    //    //남은 대화가 있을경우
    //    if (_remainSentence == true)
    //    {
    //        //남은대화 있음
    //        remainSentence = true;

    //        CharacterName.text = narrator;

    //        //텍스트 타이핑
    //        for (int a = 0; a < narration.Length; a++)
    //        {
    //            writerText += narration[a];
    //            ChatText.text = writerText;

    //            //5글자 이상 대화가 진행되고 Z키를 눌렀을 경우
    //            if (a > 5 && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z)))
    //            {
    //                writerText = narration;
    //                ChatText.text = narration;

    //                //남은대화 없음
    //                remainSentence = true;
    //                ////대화 끝
    //                //isSentenceEnd = true;

    //                //for문 조건 충족
    //                a = narration.Length;
    //            }

    //            //대사 출력 중일 경우에만
    //            if (ChatText.text != narration)
    //            {
    //                //텍스트 타이핑 시간 조절
    //                yield return new WaitForSeconds(0.02f);
    //            }

    //        }

    //        //대사 출력 후 잠깐 딜레이
    //        yield return new WaitForSeconds(0.1f);

    //        //Z키를 다시 누를 때까지 무한정 대기
    //        while (true)
    //        {
    //            if (ChatText.text == narration && Input.GetKeyDown(KeyCode.Z))
    //            {
    //                Debug.Log("Text 비우기");

    //                //Text 비우기
    //                writerText = "";

    //                break;
    //            }
    //            yield return null;
    //        }
    //    }
    //}
    #endregion
}