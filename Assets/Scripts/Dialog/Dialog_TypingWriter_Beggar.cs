using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Beggar : MonoBehaviour
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

    public S_NPCdatabase_Yes npcDatabaseScr;

    // 랜덤 대사 출력 변수
    private int RandomNum;

    //최초 클릭
    void Start()
    {
        //StartCoroutine(TextPractice());
        //TextPractice();
        //StopCoroutine(TextPractice());
        CharacterName.text="";
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
            Debug.Log("z키 누름! 상거지다!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();

            if (bool_isNPC == false)
            {
                images_NPC.SetActive(true);
                StartCoroutine(TextPractice());
                Trigger_NPC.instance.isNPCTrigger = true;
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
                bool_isNPC = true;
            }
            else
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
            }
        }
    }


    IEnumerator NormalChat()
    {
        int a = 0;
        string narrator = characternameText = CharacterName.text = npcDatabaseScr.NPC_01[6].npc_name;
        string narration = npcDatabaseScr.NPC_01[6].comment;
        string narration_2 = npcDatabaseScr.NPC_01[403].comment;

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
                yield return new WaitForSeconds(0.02f);
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

    IEnumerator ItemClueChat(string narrator, string narration)
    {
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
        #region 단서

        //2000 : 승상댁의 수양딸
        if (ObjectManager.instance.GetEquipObjectKey() == 2000)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[14].npc_name, npcDatabaseScr.NPC_01[14].comment));
        }

        //2001 : 청이의 거짓말
        else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[27].npc_name, npcDatabaseScr.NPC_01[27].comment));
        }

        //2002 : 청이의 행방
        else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[35].npc_name, npcDatabaseScr.NPC_01[35].comment));
        }

        //2003 : 청이와 장터
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[43].npc_name, npcDatabaseScr.NPC_01[43].comment));
        }

        //2004 : 청이와 남자
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[51].npc_name, npcDatabaseScr.NPC_01[51].comment));
        }

        //2004 : 청이와 남자
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[51].npc_name, npcDatabaseScr.NPC_01[51].comment));
        }

        //2005 : 누군가의 아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[62].npc_name, npcDatabaseScr.NPC_01[62].comment));
        }

        //2006 : 송나라 상인과 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[70].npc_name, npcDatabaseScr.NPC_01[70].comment));
        }

        //2008 : 승려의 마음
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[70].npc_name, npcDatabaseScr.NPC_01[70].comment));
        }

        //2009 : 청이의 도움
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[99].npc_name, npcDatabaseScr.NPC_01[99].comment));
        }

        //2010 : 공양미 삼백석
        else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[109].npc_name, npcDatabaseScr.NPC_01[109].comment));
        }

        //2010 : 공양미의 출처
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[118].npc_name, npcDatabaseScr.NPC_01[118].comment));
        }

        //2012 : 청이의 거래
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[126].npc_name, npcDatabaseScr.NPC_01[126].comment));
        }

        //2013 : 향리댁 셋째아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[134].npc_name, npcDatabaseScr.NPC_01[134].comment));
        }

        //2014 : 잠잠해져야할 물살
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[142].npc_name, npcDatabaseScr.NPC_01[142].comment));
        }

        //2015 : 청이가 사간 것
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[150].npc_name, npcDatabaseScr.NPC_01[150].comment));
        }

        //2016 : 짚신을 사간 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[160].npc_name, npcDatabaseScr.NPC_01[160].comment));
        }

        //2017 : 배의 출항
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[166].npc_name, npcDatabaseScr.NPC_01[166].comment));
        }

        //2020 : 사공에게 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[204].npc_name, npcDatabaseScr.NPC_01[204].comment));
        }

        //2021 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[212].npc_name, npcDatabaseScr.NPC_01[212].comment));
        }

        //2023 : 뱃길을 잠재울 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[234].npc_name, npcDatabaseScr.NPC_01[234].comment));
        }
        #endregion

        #region 아이템
        //1000 : 쌀
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[288].npc_name, npcDatabaseScr.NPC_01[288].comment));
        }

        //1006 : 비녀
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[304].npc_name, npcDatabaseScr.NPC_01[304].comment));
        }

        //1008 : 보리떡
        else if (ObjectManager.instance.GetEquipObjectKey() == 1008)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[312].npc_name, npcDatabaseScr.NPC_01[312].comment));
        }

        //1009 : 꽃
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[327].npc_name, npcDatabaseScr.NPC_01[327].comment));
        }

        //1011 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[343].npc_name, npcDatabaseScr.NPC_01[343].comment));
        }
        #endregion

        #region 조합 단서

        //3001 : 공양미를 구한 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 3001)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[354].npc_name, npcDatabaseScr.NPC_01[354].comment));
        }

        //3002 : 이틀전에 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 3002)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[362].npc_name, npcDatabaseScr.NPC_01[362].comment));
        }

        //3003 : 청이와 그의 관계
        else if (ObjectManager.instance.GetEquipObjectKey() == 3002)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[370].npc_name, npcDatabaseScr.NPC_01[370].comment));
        }

        //3004 : 함께 사라진 두사람
        else if (ObjectManager.instance.GetEquipObjectKey() == 3004)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[378].npc_name, npcDatabaseScr.NPC_01[378].comment));
        }

        //3005 : 무역의 중단
        else if (ObjectManager.instance.GetEquipObjectKey() == 3005)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[385].npc_name, npcDatabaseScr.NPC_01[385].comment));
        }

        //3006 : 청이의 가출
        else if (ObjectManager.instance.GetEquipObjectKey() == 3006)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[395].npc_name, npcDatabaseScr.NPC_01[395].comment));
        }
        #endregion

        //기본 대사
        else
        {
            yield return StartCoroutine(NormalChat());
        }

        //yield return StartCoroutine(NormalChat(characternameText, writerText));
        //yield return StartCoroutine(NormalChat("나는봇짐", "?안녕하세요, 반갑습니다. 대화 전환 테스트입니다 이것은 테스트지? 그럼 테스트지 테스트야 테스트군 테스트"));
    }
}