using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Budhist : Dialogue, ITalkable
{
    //최초에만 출력되도록 하는 확인용
    public bool isNPC_Start = true;

    public IEnumerator TextPractice()
    {
        // 최초 1회 출력
        if (isNPC_Start == true)
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
}