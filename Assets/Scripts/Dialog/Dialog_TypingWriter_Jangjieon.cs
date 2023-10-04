using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Jangjieon : MonoBehaviour
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
            Debug.Log("z키 누름! 장지언!!!!");

            controller_scr.TalkStart();
            if (bool_isNPC == false && !remainSentence)
            {
                StartCoroutine(TextPractice());
                images_NPC.SetActive(true);
                bool_isNPC = true;
                Trigger_NPC.instance.isNPCTrigger = true;
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
            }
            else if (isSentenceEnd)
            {
                images_NPC.SetActive(false);
                // images_NPC_portrait.SetActive(false);
                //대사 비우기
                writerText = "";
                StopAllCoroutines();
                Trigger_NPC.instance.isNPCTrigger = false;
                bool_isNPC = false;
                //Controller.instance.TalkEnd();
                controller_scr.TalkEnd();
                //남은대화 없음
                remainSentence = false;
                //대화 끝
                isSentenceEnd = false;
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
            Debug.Log("이건 Touch! 장지언!!!!");
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

    IEnumerator NormalChat_4999(string narrator, string narration)
    {
        int a = 0;
        /*
        // 글자색 설정 변수
        bool t_white = false;
        bool t_red = false;

        // 글자색 설정 문자는 대사 출력 무시
        bool t_ignore = false;
        */
        //CharacterName.text = narrator;
        narrator = characternameText = dialogdb.NPC_01[563].npc_name;
        //CharacterName.text = narrator;
        writerText = dialogdb.NPC_01[0].comment;
        Debug.Log(characternameText);
        //writerText = "";

        //narrator = CharacterName.text;
        //yield return null;
        //텍스트 타이핑
        for (a = 0; a < narration.Length; a++)
        //for (a = 0; a < 62; a++)
        {
            /*string t_letter = narration[a].ToString();
            //string t_letter;
            switch (narration[a])
            {
                case 'ⓡ':
                    t_white = false;
                    t_red = true;
                    t_ignore = true;
                    break;
                //case 'ⓦ':
                    //t_white = true;
                    //t_red = false;
                    //t_ignore = true;
                    break;
            }
            if (t_ignore==true)
            {
                if (t_white)
                {
                    t_letter = "<color=#ffffff>" + narration[a] + "</color>";    // HTML Tag
					Debug.Log(t_letter);
                    Debug.Log('1');

				}

				else if (t_red)
                {
                    t_letter = "<color=#B40404>" + narration[a] + "</color>";
                    Debug.Log(t_letter);
                    Debug.Log('2');
                }
                Debug.Log(writerText);
                //ChatText.text = writerText;
                //writerText += t_letter; // 특수문자가 아니라면 대사 출력
                //writerText += narration[a];
                //ChatText.text = writerText;
                //t_ignore = false; // 한 글자 찍었으면 다시 false
            }*/

            writerText += narration[a];
            ChatText.text = writerText;
            //t_ignore = false; // 한 글자 찍었으면 다시 false
            //ChatText.text = t_letter;
            //writerText += t_letter; // 특수문자가 아니라면 대사 출력
            //ChatText.text = writerText;
            //텍스트 타이핑 시간 조절
            yield return new WaitForSeconds(0.07f);
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

    IEnumerator NormalChat()
    {
        int a = 0;
        string narrator = characternameText = CharacterName.text = dialogdb.NPC_01[563].npc_name;
        string narration = dialogdb.NPC_01[603].comment;
        isNPC_Start = false;
        //narrator = CharacterName.text;


        for (a = 0; a < narration.Length; a++)
        //for (a = 0; a < textSpeed; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            //텍스트 타이핑 시간 조절
            //yield return null;

            if (Input.GetKeyDown(KeyCode.Z))
            {
                //남은대화 없음
                remainSentence = true;
                //대화 끝
                isSentenceEnd = true;
            }

            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
        Debug.Log(writerText);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //대화 끝
            isSentenceEnd = true;
        }
        Debug.Log(writerText);

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

    //오버로드
    IEnumerator ItemClueChat(string narrator, string narration)
    {
        //다이얼로그창 띄우기
        images_NPC.SetActive(true);

        //첫대사 플래그 false
        isNPC_Start = false;

        //남은대화 있음
        remainSentence = true;

        Debug.Log(narration);
        int a = 0;
        CharacterName.text = narrator;
        //characternameText = narrator;
        //narrator = CharacterName.text;

        //심학규의 대사일경우
        if (narrator == "심학규")
        {
            //초상화 변경
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[1];
        }
        else
        {
            //초상화 변경
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
        }

        //텍스트 타이핑
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;

            if (Input.GetKeyDown(KeyCode.Z))
            {
                //남은대화 없음
                remainSentence = true;
                //대화 끝
                isSentenceEnd = true;
            }

            //텍스트 타이핑 시간 조절
            //yield return null;

            yield return new WaitForSeconds(0.02f);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //대화 끝
            isSentenceEnd = true;
        }

        //Z키를 다시 누를 때까지 무한정 대기
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //남은대화 없음
                remainSentence = true;
                //대화 끝
                isSentenceEnd = true;
                break;
            }
            yield return null;
        }
    }

    IEnumerator ItemClueChat(string narrator, string narration, bool _remainSentence)
    {
        //심학규의 대사일경우
        if (narrator == "심학규")
        {
            //초상화 변경
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[1];
        }
        else
        {
            //초상화 변경
            GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
        }

        //남은 대화가 있을경우
        if (_remainSentence == true)
        {
            //남은대화 있음
            remainSentence = true;

            Debug.Log(narration);
            int a = 0;
            CharacterName.text = narrator;
            //characternameText = narrator;

            //narrator = CharacterName.text;

            //텍스트 타이핑
            for (a = 0; a < narration.Length; a++)
            {
                writerText += narration[a];
                ChatText.text = writerText;

                //텍스트 타이핑 시간 조절
                //yield return null;
                yield return new WaitForSeconds(0.02f);

                //중간에 Z키를 누르면
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    break;
                }
            }

            //Z키를 다시 누를 때까지 무한정 대기
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    //Text 비우기
                    writerText = "";
                    break;
                }
                yield return null;
            }
        }
    }

    IEnumerator TextPractice()
    {
        #region 단서
        //2001 : 향리댁 수양 딸 
        if (ObjectManager.instance.GetEquipObjectKey() == 2000)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[563].npc_name, dialogdb.NPC_01[563].comment,true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[564].npc_name, dialogdb.NPC_01[564].comment));
        }
        //2001 : 청이의 거짓말
        else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[565].npc_name, dialogdb.NPC_01[565].comment));
        }
        //2002 : 청이의 행방
        else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[566].npc_name, dialogdb.NPC_01[566].comment));
        }
        //2004 : 청이와 사내
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[567].npc_name, dialogdb.NPC_01[567].comment));
        }
        //2005 : 누군가의 아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[568].npc_name, dialogdb.NPC_01[568].comment));
        }
        //2006 : 송나라 상인과 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[569].npc_name, dialogdb.NPC_01[569].comment));
        }
        //2007 : 승려와 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[570].npc_name, dialogdb.NPC_01[570].comment));
        }
        //2008 : 승려의 마음
        else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[571].npc_name, dialogdb.NPC_01[571].comment));
        }
        //2009 : 청이의 도움
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[572].npc_name, dialogdb.NPC_01[572].comment));
        }
        //2010 : 공양미 삼백 석
        else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[573].npc_name, dialogdb.NPC_01[573].comment));
        }
        //2011 : 공양미의 출처
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[574].npc_name, dialogdb.NPC_01[574].comment,true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[574].npc_name, dialogdb.NPC_01[575].comment));
        }
        //2012 : 청이의 거래
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[576].npc_name, dialogdb.NPC_01[576].comment));
        }
        //2013 : 향리댁 셋째 아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[577].npc_name, dialogdb.NPC_01[577].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[578].npc_name, dialogdb.NPC_01[578].comment));
        }
        //2014 : 잠잠해져야 할 물살
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            // 기본 대사와 같음, 생략 가능할지도
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[579].npc_name, dialogdb.NPC_01[579].comment));
        }
        //2015 : 청이가 사간 것
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[580].npc_name, dialogdb.NPC_01[580].comment));
        }
        //2016 : 짚신을 사간 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[581].npc_name, dialogdb.NPC_01[581].comment,true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[582].npc_name, dialogdb.NPC_01[582].comment));
        }
        //2017 : 배의 출항
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[583].npc_name, dialogdb.NPC_01[583].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[584].npc_name, dialogdb.NPC_01[584].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[585].npc_name, dialogdb.NPC_01[585].comment));

        }
        //2018 : 노점의 단골
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[586].npc_name, dialogdb.NPC_01[586].comment));
        }
        //2019 : 뜨지 않는 배
        else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[587].npc_name, dialogdb.NPC_01[587].comment));
        }
        //2020 : 사공에게 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[588].npc_name, dialogdb.NPC_01[588].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[589].npc_name, dialogdb.NPC_01[589].comment));
        }
        //2021 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[590].npc_name, dialogdb.NPC_01[590].comment));
        }
        //2022 : 뱃길을 잠재울 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[591].npc_name, dialogdb.NPC_01[591].comment));
        }
        //2023 : 3월 보름날
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[592].npc_name, dialogdb.NPC_01[592].comment));
        }
        #endregion

        #region 아이템
        
        #endregion

        #region 조합 단서
        //4023 : 공양미를 구한 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[593].npc_name, dialogdb.NPC_01[593].comment));
        }
        //4015 : 청이가 사라진 날
        else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[594].npc_name, dialogdb.NPC_01[594].comment));
        }
        //4017 : 청이와 그의 관계
        else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[595].npc_name, dialogdb.NPC_01[595].comment));
        }
        //8032 : 함께 사라진 두 사람
        else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[596].npc_name, dialogdb.NPC_01[596].comment));
        }
        //4033 : 무역의 중단
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[597].npc_name, dialogdb.NPC_01[597].comment));
        }
        //4018 : 청이의 가출
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[598].npc_name, dialogdb.NPC_01[598].comment));
        }
        //6045 : 바다의 바쳐질 제물
        else if (ObjectManager.instance.GetEquipObjectKey() == 6045)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[599].npc_name, dialogdb.NPC_01[599].comment,true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[600].npc_name, dialogdb.NPC_01[600].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[601].npc_name, dialogdb.NPC_01[601].comment, true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[602].npc_name, dialogdb.NPC_01[602].comment));
        }
        #endregion

        #region 기본 대사
        else
        {
            yield return StartCoroutine(NormalChat());
        }
        #endregion
        //yield return StartCoroutine(NormalChat(characternameText, writerText));
        //if(index == 4999)
        //{
        //yield return StartCoroutine(NormalChat_4999(characternameText, writerText)); 
        //yield return StartCoroutine(NormalChat_2(characternameText, writerText));
        //}
        //yield return StartCoroutine(NormalChat(characternameText, writerText));
        //yield return StartCoroutine(NormalChat("나는봇짐", "?안녕하세요, 반갑습니다. 대화 전환 테스트입니다 이것은 테스트지? 그럼 테스트지 테스트야 테스트군 테스트"));
    }
}