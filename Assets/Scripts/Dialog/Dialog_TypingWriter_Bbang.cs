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

    //대화가 전부 출력 되었는지
    public bool isSentenceEnd = false;

    //남은 대화가 더 있는지 (true일경우 다이얼로그를 종료하지않고 내용을 비워서 다음 대사를 출력함)
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

        if (Input.GetKeyDown(KeyCode.B))
        {
            ObjectManager.instance.GetItem(1006);
        }

        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("z키 누름! 송나라!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false && !remainSentence)
            {
                Debug.Log("대화 실행");
                images_Bbang.SetActive(true);
                StartCoroutine(TextPractice());
                Trigger_NPC.instance.isNPCTrigger = true;
                //초상화 변경
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
                //bool_isNPC = true;
            }

            //대화가 끝났을 경우
            else if (isSentenceEnd)
            {
                images_Bbang.SetActive(false);
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
        //dialogstart();
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
                //플레이어 이동제한 해제
                controller_scr.TalkEnd();

                images_Bbang.SetActive(false);
                bool_isNPC = false;
                trigger_npc.isNPCTrigger = false;

                writerText = "";
                StopAllCoroutines();
            }
        }
    }

    IEnumerator NormalChat()
    {
        int a = 0;
        string narrator = characternameText = CharacterName.text = dialogdb.NPC_01[1].npc_name;
        string narration = dialogdb.NPC_01[1].comment;
        string narration_2 = dialogdb.NPC_01[399].comment;
        RandomNum = Random.Range(0, 2);
        Debug.Log(RandomNum);

        if (RandomNum == 0)
        {
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
        }
        else if (RandomNum == 1)
        {
            for (a = 0; a < narration_2.Length; a++)
            //for (a = 0; a < textSpeed; a++)
            {
                writerText += narration_2[a];
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

    //IEnumerator ItemClueChat(string narrator, string narration)
    //{
    //    int a = 0;
    //    CharacterName.text = narrator;
    //    writerText = "";

    //    //텍스트 타이핑
    //    for (a = 0; a < narration.Length; a++)
    //    {
    //        writerText += narration[a];
    //        ChatText.text = writerText;

    //        //텍스트 타이핑 시간 조절
    //        //yield return null;
    //        yield return new WaitForSeconds(0.02f);
    //    }

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

    IEnumerator ItemClueChat(string narrator, string narration)
    {
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

        ////키(default : space)를 다시 누를 때까지 무한정 대기
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


    //오버로드
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

        ////키(default : space)를 다시 누를 때까지 무한정 대기
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

    IEnumerator TextPractice()
    {
        #region 단서
        //2000 : 승상댁의 수양딸
        if (ObjectManager.instance.GetEquipObjectKey() == 2000)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[9].npc_name, dialogdb.NPC_01[9].comment));
        }
        //2001 : 청이의 거짓말
        else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[22].npc_name, dialogdb.NPC_01[22].comment));
        }
        //2002 : 청이의 행방
        else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[30].npc_name, dialogdb.NPC_01[30].comment));
        }
        //2003 : 청이와 장터
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[38].npc_name, dialogdb.NPC_01[38].comment));
        }
        //2004 : 청이와 사내
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            //비녀 이벤트 활성화
            EventManager.instance.EventActive(Events.binyeo);
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[46].npc_name, dialogdb.NPC_01[46].comment));
        }
        //2005 : 누군가의 아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[57].npc_name, dialogdb.NPC_01[57].comment));
        }
        //2006 : 송나라 상인과 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[65].npc_name, dialogdb.NPC_01[65].comment));
        }
        //2007 : 승려와 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
        {
            //승려의 마음 단서 획득
            ObjectManager.instance.GetClue(2008);

            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[78].npc_name, dialogdb.NPC_01[78].comment));
        }
        //2008 : 승려의 마음
        else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[86].npc_name, dialogdb.NPC_01[86].comment));
        }
        //2009 : 청이의 도움
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[94].npc_name, dialogdb.NPC_01[94].comment));
        }
        //2010 : 공양미 삼백 석
        else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[104].npc_name, dialogdb.NPC_01[104].comment));
        }
        //2011 : 공양미의 출처
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[113].npc_name, dialogdb.NPC_01[113].comment));
        }
        //2012 : 청이의 거래
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[121].npc_name, dialogdb.NPC_01[121].comment));
        }
        //2013 : 향리 댁 셋째 아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[129].npc_name, dialogdb.NPC_01[129].comment));
        }
        /*//2014 : 잠잠해져야 할 물살, 2020 : 사공에게 있었던 일, 2022 : 뱃길을 잠재울 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            // 기본 대사와 같음, 생략 가능할지도
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[137].npc_name, dialogdb.NPC_01[137].comment));
        }*/
        //2015 : 청이가 사간 것
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[145].npc_name, dialogdb.NPC_01[145].comment));
        }
        //2016 : 짚신을 사간 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[155].npc_name, dialogdb.NPC_01[155].comment));
        }
        //2017 : 배의 출항
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[163].npc_name, dialogdb.NPC_01[163].comment));
        }
        //2018 : 노점의 단골
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[182].npc_name, dialogdb.NPC_01[182].comment));
        }
        //2019 : 뜨지 않는 배
        else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[190].npc_name, dialogdb.NPC_01[190].comment));
        }
        //2021 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[207].npc_name, dialogdb.NPC_01[207].comment));
        }
        //2023 : 3월 보름날
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[229].npc_name, dialogdb.NPC_01[229].comment));
        }

        #endregion

        #region 아이템
        //1000 : 쌀
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[283].npc_name, dialogdb.NPC_01[283].comment));
        }
        //1001 : 장작
        else if (ObjectManager.instance.GetEquipObjectKey() == 1001)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[249].npc_name, dialogdb.NPC_01[78].comment));
        }
        //1005 : 주먹밥
        else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[292].npc_name, dialogdb.NPC_01[292].comment));
        }
        //1006 : 비녀
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            //비녀 오브젝트 제거
            ObjectManager.instance.RemoveItem(1006);

            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[54].npc_name, dialogdb.NPC_01[54].comment,true));
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[55].npc_name, dialogdb.NPC_01[55].comment,true));

            //송나라 상인과 청이 단서 획득
            ObjectManager.instance.GetClue(2006);
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[56].npc_name, dialogdb.NPC_01[56].comment));
        }
        //1007 : 먹
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[307].npc_name, dialogdb.NPC_01[307].comment));
        }
        //1009 : 꽃
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[322].npc_name, dialogdb.NPC_01[322].comment));
        }
        //1011 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[338].npc_name, dialogdb.NPC_01[338].comment));
        }

        #endregion

        #region 조합 단서
        //4023 : 공양미를 구한 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[349].npc_name, dialogdb.NPC_01[349].comment));
        }
        //4015 : 청이가 사라진 날
        else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[357].npc_name, dialogdb.NPC_01[357].comment));
        }
        //4017 : 청이와 그의 관계
        else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[365].npc_name, dialogdb.NPC_01[365].comment));
        }
        //8032 : 함께 사라진 두 사람
        else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[373].npc_name, dialogdb.NPC_01[373].comment));
        }
        //4033 : 무역의 중단
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[381].npc_name, dialogdb.NPC_01[381].comment));
        }
        //4018 : 청이의 가출
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(ItemClueChat(dialogdb.NPC_01[391].npc_name, dialogdb.NPC_01[391].comment));
        }
        #endregion

        #region 기본 대사
        else
        {
            yield return StartCoroutine(NormalChat());
        }
        #endregion
    }
}