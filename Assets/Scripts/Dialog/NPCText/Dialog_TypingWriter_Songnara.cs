using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_Songnara : Dialogue,ITalkable
{
    public IEnumerator TextPractice()
    {

        #region 단서

        //2005 : 누군가의 아들
        if (ObjectManager.instance.GetEquipObjectKey() == 2005)
        {
            //잠잠해져야할 물살 단서 획득
            ObjectManager.instance.GetClue(2014);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[537].npc_name, dialogdb.NPC_01[537].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[64].npc_name, dialogdb.NPC_01[64].comment));
        }

        //2006 : 송나라 상인과 청이
        if (ObjectManager.instance.GetEquipObjectKey() == 2006)
        {
            //청이의 도움 단서 획득
            ObjectManager.instance.GetClue(2009);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[538].npc_name, dialogdb.NPC_01[538].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[72].npc_name, dialogdb.NPC_01[72].comment));
        }

        //2009 : 청이의 도움
        else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[101].npc_name, dialogdb.NPC_01[101].comment));
        }

        //2011 : 공양미의 출처
        else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[120].npc_name, dialogdb.NPC_01[120].comment));
        }

        //2012 : 청이의 거래
        else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[128].npc_name, dialogdb.NPC_01[128].comment));
        }

        //2014 : 잠잠해져야할 물살
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[144].npc_name, dialogdb.NPC_01[144].comment));
        }

        //2015 : 청이가 사간 것
        else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[152].npc_name, dialogdb.NPC_01[152].comment));
        }

        //2017 : 배의 출항
        else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[170].npc_name, dialogdb.NPC_01[170].comment));
        }

        //2018 : 노점의 단골
        else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[189].npc_name, dialogdb.NPC_01[189].comment));
        }

        //2020 : 사공에게 있었던 일
        else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[206].npc_name, dialogdb.NPC_01[206].comment));
        }

        //2021 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[214].npc_name, dialogdb.NPC_01[214].comment));
        }

        //2022 : 바쁜 상인들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[228].npc_name, dialogdb.NPC_01[228].comment));
        }

        //2022 : 바쁜 상인들
        else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[228].npc_name, dialogdb.NPC_01[228].comment));
        }

        //2023 : 3월 보름날
        else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
        {

            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[555].npc_name, dialogdb.NPC_01[555].comment, true));

            //베드엔딩4 취생몽사
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[248].npc_name, dialogdb.NPC_01[248].comment, true));

            //엔딩 배경 ON
            EndingManager.instance.ShowEndingBG();
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[425].npc_name, dialogdb.NPC_01[425].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[426].npc_name, dialogdb.NPC_01[426].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[427].npc_name, dialogdb.NPC_01[427].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[428].npc_name, dialogdb.NPC_01[428].comment, true));

            //베드엔딩 배경 보이기
            EndingManager.instance.ChangeToBadEndingBG();
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[429].npc_name, dialogdb.NPC_01[429].comment, true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[430].npc_name, dialogdb.NPC_01[430].comment, true));

            //타이틀로 이동
            EndingManager.instance.LoadTitleScene();
        }
        #endregion

        #region 아이템
        //1006 : 비녀 
        else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[306].npc_name, dialogdb.NPC_01[306].comment));
        }

        //1007 : 먹
        else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[314].npc_name, dialogdb.NPC_01[314].comment));
        }

        //1011 : 사공의 물건
        else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[347].npc_name, dialogdb.NPC_01[347].comment));
        }
        //1013 : 새끼줄1
        else if (ObjectManager.instance.GetEquipObjectKey() == 1013)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[860].npc_name, dialogdb.NPC_01[860].comment));
        }
        //1014 : 새끼줄2
        else if (ObjectManager.instance.GetEquipObjectKey() == 1014)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[868].npc_name, dialogdb.NPC_01[868].comment));
        }
        #endregion

        #region 조합 단서
        //4023 : 공양미를 구한 방법
        else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[356].npc_name, dialogdb.NPC_01[356].comment));
        }

        //8032 : 함께 사라진 두 사람
        else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[379].npc_name, dialogdb.NPC_01[379].comment));
        }

        //4033 : 무역의 중단
        else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[388].npc_name, dialogdb.NPC_01[388].comment));
        }

        //4018 : 청이의 가출
        else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
        {
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[393].npc_name, dialogdb.NPC_01[393].comment));
        }
        #endregion

        //기본 대사
        else
        {
            yield return StartCoroutine(DialogManager.instance.NormalChat("송나라 상인"));
        }
    }
}