using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_BoatMan : MonoBehaviour
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
        //TextPractice();
        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("z키 누름! 뱃사공!!!!");
            //bool_isBotjim = true;
            controller_scr.TalkStart();
            if (bool_isNPC == false)
            {
                StartCoroutine(TextPractice());
                images_NPC.SetActive(true);
                Trigger_NPC.instance.isNPCTrigger = true;
                GameObject.Find("NPC_Profile").GetComponent<Image>().sprite = images_NPC_portrait[0];
                bool_isNPC = true;
            }
            else
            {
                images_NPC.SetActive(false);
                // images_NPC_portrait.SetActive(false);
                writerText = "";
                StopAllCoroutines();
                Trigger_NPC.instance.isNPCTrigger = false;
                //Controller.instance.TalkEnd();
                controller_scr.TalkEnd();
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
        #region 단서
        //2001 : 청이의 거짓말
        if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[28].npc_name, npcDatabaseScr.NPC_01[28].comment));
        }

        //2003 : 청이와 장터
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[44].npc_name, npcDatabaseScr.NPC_01[44].comment));
        }

        //2004 : 청이와 남자
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[52].npc_name, npcDatabaseScr.NPC_01[52].comment));
        }

        //2005 : 누군가의 아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[63].npc_name, npcDatabaseScr.NPC_01[63].comment));
        }

        //2006 : 송나라 상인과 청이 (추가 대사 있음)
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[71].npc_name, npcDatabaseScr.NPC_01[71].comment));
        }

        //2007 : 승려와 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[84].npc_name, npcDatabaseScr.NPC_01[84].comment));
        }

        //2008 : 승려의 마음
        else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[92].npc_name, npcDatabaseScr.NPC_01[92].comment));
        }

        //2009 : 청이의 도움
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[100].npc_name, npcDatabaseScr.NPC_01[100].comment));
        }

        //2011 : 공양미의 출처
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[119].npc_name, npcDatabaseScr.NPC_01[119].comment));
        }

        //2012 : 청이의 거래
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[127].npc_name, npcDatabaseScr.NPC_01[127].comment));
        }

        //2013 : 향리집 셋째아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[135].npc_name, npcDatabaseScr.NPC_01[135].comment));
        }

        //2014 : 잠잠해져야할 물살
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[143].npc_name, npcDatabaseScr.NPC_01[143].comment));
        }

        //2015 : 청이가 사간 것
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[151].npc_name, npcDatabaseScr.NPC_01[151].comment));
        }

        //2017 : 배의 출항
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[169].npc_name, npcDatabaseScr.NPC_01[169].comment));
        }

        //2018 : 노점의 단골
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[188].npc_name, npcDatabaseScr.NPC_01[188].comment));
        }

        //2019 : 뜨지않는 배
        else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[196].npc_name, npcDatabaseScr.NPC_01[196].comment));
        }

        //2020 : 사공에게 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[205].npc_name, npcDatabaseScr.NPC_01[205].comment));
        }

        //2021 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[213].npc_name, npcDatabaseScr.NPC_01[213].comment));
        }

        //2022 : 바쁜 상인들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[227].npc_name, npcDatabaseScr.NPC_01[227].comment));
        }

        //2023 : 뱃길을 잠재울 방법 (이후 대사 있음)
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[236].npc_name, npcDatabaseScr.NPC_01[236].comment));
        }
        #endregion

        #region 아이템
        //1000 : 쌀
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[289].npc_name, npcDatabaseScr.NPC_01[289].comment));
        }

        //1005 : 주먹밥
        else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[297].npc_name, npcDatabaseScr.NPC_01[297].comment));
        }

        //1006 : 비녀 
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[305].npc_name, npcDatabaseScr.NPC_01[305].comment));
        }

        //1007 : 먹
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[313].npc_name, npcDatabaseScr.NPC_01[313].comment));
        }

        //1009 : 꽃
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[328].npc_name, npcDatabaseScr.NPC_01[328].comment));
        }

        //1011 : 사공의 물건 (이후 대사 추가)
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[344].npc_name, npcDatabaseScr.NPC_01[344].comment));
        }

        #endregion

        #region 조합 단서
        //3001 : 공양미를 구한 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 3001)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[355].npc_name, npcDatabaseScr.NPC_01[355].comment));
        }

        //3002 : 이틀전에 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 3002)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[363].npc_name, npcDatabaseScr.NPC_01[363].comment));
        }

        //3003 : 청이와 그의 관계
        else if (ObjectManager.instance.GetEquipObjectKey() == 3003)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[371].npc_name, npcDatabaseScr.NPC_01[371].comment));
        }

        //3005 : 무역의 중단
        else if (ObjectManager.instance.GetEquipObjectKey() == 3005)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[387].npc_name, npcDatabaseScr.NPC_01[387].comment));
        }

        //3006 : 청이의 가출
        else if (ObjectManager.instance.GetEquipObjectKey() == 3006)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[397].npc_name, npcDatabaseScr.NPC_01[397].comment));
        }
        #endregion

        //기본 대사
        else
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[7].npc_name, npcDatabaseScr.NPC_01[7].comment));
        }
        //yield return StartCoroutine(NormalChat(characternameText, writerText));
        //yield return StartCoroutine(NormalChat("나는봇짐", "?안녕하세요, 반갑습니다. 대화 전환 테스트입니다 이것은 테스트지? 그럼 테스트지 테스트야 테스트군 테스트"));
    }
}