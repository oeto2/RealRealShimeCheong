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
        #region 단서
        //2006 : 송나라 상인과 청이
        if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[72].npc_name, npcDatabaseScr.NPC_01[72].comment));
        }

        //2009 : 청이의 도움
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[101].npc_name, npcDatabaseScr.NPC_01[101].comment));
        }

        //2011 : 공양미의 출처
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[120].npc_name, npcDatabaseScr.NPC_01[120].comment));
        }

        //2012 : 청이의 거래
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[128].npc_name, npcDatabaseScr.NPC_01[128].comment));
        }

        //2014 : 잠잠해져야할 물살
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[144].npc_name, npcDatabaseScr.NPC_01[144].comment));
        }

        //2015 : 청이가 사간 것
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[152].npc_name, npcDatabaseScr.NPC_01[152].comment));
        }

        //2017 : 배의 출항
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[170].npc_name, npcDatabaseScr.NPC_01[170].comment));
        }

        //2018 : 노점의 단골
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[189].npc_name, npcDatabaseScr.NPC_01[189].comment));
        }

        //2020 : 사공에게 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[206].npc_name, npcDatabaseScr.NPC_01[206].comment));
        }

        //2021 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[214].npc_name, npcDatabaseScr.NPC_01[214].comment));
        }

        //2022 : 바쁜 상인들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[228].npc_name, npcDatabaseScr.NPC_01[228].comment));
        }

        //2022 : 바쁜 상인들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[228].npc_name, npcDatabaseScr.NPC_01[228].comment));
        }

        //2023 : 뱃길을 잠재울 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[248].npc_name, npcDatabaseScr.NPC_01[248].comment));
        }
        #endregion

        #region 아이템
        //1006 : 비녀 
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[306].npc_name, npcDatabaseScr.NPC_01[306].comment));
        }

        //1007 : 먹
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[314].npc_name, npcDatabaseScr.NPC_01[314].comment));
        }

        //1011 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[347].npc_name, npcDatabaseScr.NPC_01[347].comment));
        }

        #endregion

        #region 조합 단서
        //3001 : 공양미를 구한 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 3001)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[356].npc_name, npcDatabaseScr.NPC_01[356].comment));
        }

        //3004 : 함께 사라진 두 사람
        else if (ObjectManager.instance.GetEquipObjectKey() == 3004)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[379].npc_name, npcDatabaseScr.NPC_01[379].comment));
        }

        //3005 : 무역의 중단
        else if (ObjectManager.instance.GetEquipObjectKey() == 3005)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[388].npc_name, npcDatabaseScr.NPC_01[388].comment));
        }

        //3006 : 청이의 가출
        else if (ObjectManager.instance.GetEquipObjectKey() == 3006)
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[393].npc_name, npcDatabaseScr.NPC_01[393].comment));
        }
        #endregion

        //기본 대사
        else
        {
            yield return StartCoroutine(NormalChat(npcDatabaseScr.NPC_01[8].npc_name, npcDatabaseScr.NPC_01[8].comment));
        }
    }
}