using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_BusinessMan : MonoBehaviour
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
        CharacterName.text = "";
        ChatText.text = "";
    }


    void Update()
    {
        //Debug.Log(npcDatabaseScr.NPC_01[4].comment);

        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Z) && trigger_npc.isNPCTrigger)
        {
            Debug.Log("z키 누름! 장사꾼!!!!");
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
                controller_scr.TalkEnd();
                bool_isNPC = false;
                //Controller.instance.TalkEnd();
            }
        }
    }

    IEnumerator NormalChat()
    {
        int a = 0;
        string narrator = characternameText = CharacterName.text = npcDatabaseScr.NPC_01[4].npc_name;
        string narration = npcDatabaseScr.NPC_01[4].comment;
        string narration_2 = npcDatabaseScr.NPC_01[402].comment;

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
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[25].npc_name, npcDatabaseScr.NPC_01[25].comment));
        }

        //2002 : 청이의 행방
        else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[33].npc_name, npcDatabaseScr.NPC_01[33].comment));
        }

        //2003 : 청이와 장터
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[41].npc_name, npcDatabaseScr.NPC_01[41].comment));
        }

        //2005 : 누군가의 아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[60].npc_name, npcDatabaseScr.NPC_01[60].comment));
        }

        //2006 : 송나라 상인과 청이 (추가 대사 있음)
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[68].npc_name, npcDatabaseScr.NPC_01[68].comment));
        }

        //2007 : 승려와 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[81].npc_name, npcDatabaseScr.NPC_01[81].comment));
        }

        //2010 : 공양미 삼백석
        else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[107].npc_name, npcDatabaseScr.NPC_01[107].comment));
        }

        //2011 : 공양미의 출처
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[116].npc_name, npcDatabaseScr.NPC_01[116].comment));
        }

        //2012 : 청이의 거래
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[124].npc_name, npcDatabaseScr.NPC_01[124].comment));
        }

        //2013 : 향리집 셋째아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[132].npc_name, npcDatabaseScr.NPC_01[132].comment));
        }

        //2014 : 잠잠해져야할 물살
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[140].npc_name, npcDatabaseScr.NPC_01[140].comment));
        }

        //2015 : 청이가 사간 것 (이후 대사 추가?)
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[148].npc_name, npcDatabaseScr.NPC_01[148].comment));
        }

        //2016 : 짚신을 사간 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[158].npc_name, npcDatabaseScr.NPC_01[158].comment));
        }

        //2017 : 배의 출항
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[168].npc_name, npcDatabaseScr.NPC_01[168].comment));
        }

        //2018 : 노점의 단골
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[185].npc_name, npcDatabaseScr.NPC_01[185].comment));
        }

        //2019 : 뜨지않는 배
        else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[193].npc_name, npcDatabaseScr.NPC_01[193].comment));
        }

        //2020 : 사공에게 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[202].npc_name, npcDatabaseScr.NPC_01[202].comment));
        }

        //2021 : 사공의 물건 (구슬 퍼즐? 대사 추가)
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[210].npc_name, npcDatabaseScr.NPC_01[210].comment));
        }

        //2023 : 뱃길을 잠재울 방법 (이후 대사 있음)
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[232].npc_name, npcDatabaseScr.NPC_01[232].comment));
        }
        #endregion

        #region 아이템
        //1000 : 쌀
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[286].npc_name, npcDatabaseScr.NPC_01[286].comment));
        }

        //1005 : 주먹밥
        else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[295].npc_name, npcDatabaseScr.NPC_01[295].comment));
        }

        //1006 : 비녀 (이벤트?)
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[302].npc_name, npcDatabaseScr.NPC_01[302].comment));
        }

        //1007 : 먹
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[310].npc_name, npcDatabaseScr.NPC_01[310].comment));
        }

        //1009 : 꽃
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[325].npc_name, npcDatabaseScr.NPC_01[325].comment));
        }

        //1011 : 사공의 물건 (이후 대사 추가)
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[341].npc_name, npcDatabaseScr.NPC_01[341].comment));
        }

        #endregion

        #region 조합 단서
        //3001 : 공양미를 구한 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 3001)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[352].npc_name, npcDatabaseScr.NPC_01[352].comment));
        }

        //3004 : 사라진 두사람
        else if (ObjectManager.instance.GetEquipObjectKey() == 3004)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[376].npc_name, npcDatabaseScr.NPC_01[376].comment));
        }

        //3005 : 무역의 중단
        else if (ObjectManager.instance.GetEquipObjectKey() == 3005)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[384].npc_name, npcDatabaseScr.NPC_01[384].comment));
        }

        //3006 : 청이의 가출
        else if (ObjectManager.instance.GetEquipObjectKey() == 3006)
        {
            yield return StartCoroutine(ItemClueChat(npcDatabaseScr.NPC_01[394].npc_name, npcDatabaseScr.NPC_01[394].comment));
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