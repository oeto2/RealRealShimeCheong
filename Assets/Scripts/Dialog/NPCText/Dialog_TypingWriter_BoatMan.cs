using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_BoatMan : Dialogue, ITalkable
{
    //송나라 상인과 청이 선택지 값
    public int int_Select2006Num = 0;

    //2006번 선택지 고른 날짜
    public int int_select2006Day = 0;

    //뱃사공2 오브젝트
    public GameObject gameObject_BoatMan2;

    //뱃사공2 이벤트 진행여부
    public bool boatMan2_Show;

    //사공의 물건을 전달 했는지
    public bool boatManObject;
   

    public IEnumerator TextPractice()
    {
        #region 단서
        //2001 : 청이의 거짓말
        if (ObjectManager.instance.GetEquipObjectKey() == 2001)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[28].npc_name, dialogdb.NPC_01[28].comment));
        }

        //2003 : 청이와 장터
        else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[44].npc_name, dialogdb.NPC_01[44].comment));
        }

        //2004 : 청이와 남자
        else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[52].npc_name, dialogdb.NPC_01[52].comment));
        }

        //2005 : 누군가의 아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[63].npc_name, dialogdb.NPC_01[63].comment));
        }

        //2006 : 송나라 상인과 청이 (추가 대사 있음)
        else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[538].npc_name, dialogdb.NPC_01[538].comment, true));

            //선택지를 고르지 않았다면
            if (!EventManager.instance.selectEndCheck.select2006_End)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[71].npc_name, dialogdb.NPC_01[71].comment, true));
                EventManager.instance.SelectStart(NPCName.boatman, 2006);
            }
            else
            {
                //선택지 1번을 골랐을 경우
                if (int_Select2006Num == 1)
                {
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[73].npc_name, dialogdb.NPC_01[73].comment, true));
                    //청이의 거래 단서 획득
                    ObjectManager.instance.GetClue(2012);
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[74].npc_name, dialogdb.NPC_01[74].comment));
                }
                else
                {
                    //만약 하루가 지났다면
                    if (int_select2006Day < TimeManager.instance.int_DayCount)
                    {
                        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[71].npc_name, dialogdb.NPC_01[71].comment, true));
                        EventManager.instance.SelectStart(NPCName.boatman, 2006);
                    }
                    else
                    {
                        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[77].npc_name, dialogdb.NPC_01[77].comment));
                    }
                }
            }
        }

        //2007 : 승려와 청이
        else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[84].npc_name, dialogdb.NPC_01[84].comment));
        }

        //2008 : 승려의 마음
        else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[92].npc_name, dialogdb.NPC_01[92].comment));
        }

        //2009 : 청이의 도움
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[100].npc_name, dialogdb.NPC_01[100].comment));
        }

        //2011 : 공양미의 출처
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[119].npc_name, dialogdb.NPC_01[119].comment));
        }

        //2012 : 청이의 거래
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[127].npc_name, dialogdb.NPC_01[127].comment));
        }

        //2013 : 향리집 셋째아들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[135].npc_name, dialogdb.NPC_01[135].comment));
        }

        //2014 : 잠잠해져야할 물살
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            //뜨지않는 배 단서 획득
            ObjectManager.instance.GetClue(2019);

            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[546].npc_name, dialogdb.NPC_01[546].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[143].npc_name, dialogdb.NPC_01[143].comment));
        }

        //2015 : 청이가 사간 것
        else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[151].npc_name, dialogdb.NPC_01[151].comment));
        }

        //2017 : 배의 출항
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[169].npc_name, dialogdb.NPC_01[169].comment));
        }

        //2018 : 노점의 단골
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[188].npc_name, dialogdb.NPC_01[188].comment));
        }

        //2019 : 뜨지않는 배
        else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[196].npc_name, dialogdb.NPC_01[196].comment));
        }

        //2020 : 사공에게 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            //사공의 물건 단서 획득
            ObjectManager.instance.GetClue(2021);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[552].npc_name, dialogdb.NPC_01[552].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[205].npc_name, dialogdb.NPC_01[205].comment));
        }

        //2021 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[213].npc_name, dialogdb.NPC_01[213].comment));
        }

        //2022 : 뱃길을 잠재울 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[227].npc_name, dialogdb.NPC_01[227].comment));
        }

        //2023 : 3월 보름날
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[555].npc_name, dialogdb.NPC_01[555].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[236].npc_name, dialogdb.NPC_01[236].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[237].npc_name, dialogdb.NPC_01[237].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[238].npc_name, dialogdb.NPC_01[238].comment, true));

            //개울가 뱃사공 보이기
            gameObject_BoatMan2.SetActive(true);
            boatMan2_Show = true;

            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[239].npc_name, dialogdb.NPC_01[239].comment));
        }
        #endregion

        #region 아이템
        //1000 : 쌀
        else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[289].npc_name, dialogdb.NPC_01[289].comment));
        }

        //1005 : 주먹밥
        else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[297].npc_name, dialogdb.NPC_01[297].comment));
        }

        //1006 : 비녀 
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[305].npc_name, dialogdb.NPC_01[305].comment));
        }

        //1007 : 먹
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[313].npc_name, dialogdb.NPC_01[313].comment));
        }

        //1009 : 꽃
        else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[328].npc_name, dialogdb.NPC_01[328].comment));
        }

        //1011 : 사공의 물건 (이후 대사 추가)
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[531].npc_name, dialogdb.NPC_01[531].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[344].npc_name, dialogdb.NPC_01[344].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[345].npc_name, dialogdb.NPC_01[345].comment, true));
            boatManObject = true;
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[346].npc_name, dialogdb.NPC_01[346].comment));
        }

        //1013 : 새끼줄1
        else if (ObjectManager.instance.GetEquipObjectKey() == 1013)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[859].npc_name, dialogdb.NPC_01[859].comment));
        }
        //1014 : 새끼줄2
        else if (ObjectManager.instance.GetEquipObjectKey() == 1014)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[867].npc_name, dialogdb.NPC_01[867].comment));
        }
        #endregion

        #region 조합 단서
        //4023 : 공양미를 구한 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[355].npc_name, dialogdb.NPC_01[355].comment));
        }

        //4015 : 이틀전에 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[363].npc_name, dialogdb.NPC_01[363].comment));
        }

        //4017 : 청이와 그의 관계
        else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[371].npc_name, dialogdb.NPC_01[371].comment));
        }

        //4033 : 무역의 중단
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[560].npc_name, dialogdb.NPC_01[560].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[387].npc_name, dialogdb.NPC_01[387].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[389].npc_name, dialogdb.NPC_01[389].comment, true));

            //뱃길을 잠재울 방법 단서획득
            ObjectManager.instance.GetClue(2022);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[390].npc_name, dialogdb.NPC_01[390].comment));
        }

        //4018 : 청이의 가출
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[397].npc_name, dialogdb.NPC_01[397].comment));
        }
        #endregion

        //기본 대사
        else
        {
            yield return StartCoroutine(DialogManager.instance.NormalChat("뱃사공"));
        }
    }

    //송나라 상인과 청이 1번 대사
    IEnumerator Select2006_Sentence1()
    {
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[73].npc_name, dialogdb.NPC_01[73].comment, true));
        //청이의 거래 단서 획득
        ObjectManager.instance.GetClue(2012);
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[74].npc_name, dialogdb.NPC_01[74].comment));
    }

    //송나라 상인과 청이 2번 대사
    IEnumerator Select2006_Sentence2()
    {
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[75].npc_name, dialogdb.NPC_01[75].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[76].npc_name, dialogdb.NPC_01[76].comment));

    }

    //송나라 상인과 청이 선택지 1번 대사 출력
    public void PrintSelect2006_Sentence1()
    {
        StartCoroutine(Select2006_Sentence1());
    }

    //송나라 상인과 청이 선택지 2번 대사 출력
    public void PrintSelect2006_Sentence2()
    {
        StartCoroutine(Select2006_Sentence2());
    }
}