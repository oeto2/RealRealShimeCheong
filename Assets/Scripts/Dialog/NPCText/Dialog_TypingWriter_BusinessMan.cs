using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_BusinessMan : Dialogue, ITalkable
{
    //외부 스크립트 참조
    public BeadMove beadmoveScr;

    //구슬 퍼즐 시작
    IEnumerator BeadPuzzlePlay()
    {
        //다이얼로그 창 종료
        DialogueCanvas.SetActive(false);

        //구슬 퍼즐 시작
        GameManager.instance.PlayBeadPuzzle();

        //퍼즐 클리어 전까지 무한 대기
        while (true)
        {
            if (beadmoveScr.isClear)
            {
                break;
            }
            yield return null;
        }
    }

    public IEnumerator TextPractice()
    {
        //만약 먹 전달을 완료 했다면
        if (EventManager.instance.eventEndCheck.muckEvent_End == true)
        {
            //이벤트 완료 끄기
            EventManager.instance.eventEndCheck.muckEvent_End = false;
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[153].npc_name, dialogdb.NPC_01[153].comment, true));

            //짚신을 사간 청이 단서 획득
            ObjectManager.instance.GetClue(2016);
            yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[154].npc_name, dialogdb.NPC_01[154].comment));
        }

        else
        {
            #region 단서
            //2001 : 청이의 거짓말
            if (ObjectManager.instance.GetEquipObjectKey() == 2001)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[25].npc_name, dialogdb.NPC_01[25].comment));
            }

            //2002 : 청이의 행방
            else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[33].npc_name, dialogdb.NPC_01[33].comment));
            }

            //2003 : 청이와 장터
            else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
            {
                //청이가 사간것 획득
                ObjectManager.instance.GetClue(2015);

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[535].npc_name, dialogdb.NPC_01[535].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[41].npc_name, dialogdb.NPC_01[41].comment));
            }

            //2005 : 누군가의 아들
            else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[60].npc_name, dialogdb.NPC_01[60].comment));
            }

            //2006 : 송나라 상인과 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[68].npc_name, dialogdb.NPC_01[68].comment));
            }

            //2007 : 승려와 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[81].npc_name, dialogdb.NPC_01[81].comment));
            }

            //2010 : 공양미 삼백석
            else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[107].npc_name, dialogdb.NPC_01[107].comment));
            }

            //2011 : 공양미의 출처
            else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[116].npc_name, dialogdb.NPC_01[116].comment));
            }

            //2012 : 청이의 거래
            else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[124].npc_name, dialogdb.NPC_01[124].comment));
            }

            //2013 : 향리집 셋째아들
            else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
            {
                //노점의 단골 단서 획득
                ObjectManager.instance.GetClue(2018);

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[545].npc_name, dialogdb.NPC_01[545].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[132].npc_name, dialogdb.NPC_01[132].comment));
            }

            //2014 : 잠잠해져야할 물살
            else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[140].npc_name, dialogdb.NPC_01[140].comment));
            }

            //2015 : 청이가 사간 것 (이후 대사 추가?)
            else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[547].npc_name, dialogdb.NPC_01[547].comment, true));

                //아직 먹 전달을 못했으면
                if (EventManager.instance.eventProgress.deliveryMuck != true)
                {
                    //먹 전달 이벤트 시작
                    EventManager.instance.EventActive(Events.muck);

                    //먹 획득
                    ObjectManager.instance.GetItem(1007);

                }

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[148].npc_name, dialogdb.NPC_01[148].comment));
            }

            //2016 : 짚신을 사간 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[158].npc_name, dialogdb.NPC_01[158].comment));
            }

            //2017 : 배의 출항
            else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[168].npc_name, dialogdb.NPC_01[168].comment));
            }

            //2018 : 노점의 단골
            else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[185].npc_name, dialogdb.NPC_01[185].comment));
            }

            //2019 : 뜨지않는 배
            else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[193].npc_name, dialogdb.NPC_01[193].comment));
            }

            //2020 : 사공에게 있었던 일
            else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[202].npc_name, dialogdb.NPC_01[202].comment));
            }

            //2021 : 사공의 물건 (구슬 퍼즐? 대사 추가)
            else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[553].npc_name, dialogdb.NPC_01[553].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[210].npc_name, dialogdb.NPC_01[210].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[215].npc_name, dialogdb.NPC_01[215].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[216].npc_name, dialogdb.NPC_01[216].comment, true));

                //퍼즐 시작
                yield return StartCoroutine(BeadPuzzlePlay());
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[217].npc_name, dialogdb.NPC_01[217].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[218].npc_name, dialogdb.NPC_01[218].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[219].npc_name, dialogdb.NPC_01[219].comment, true));
                //먹 획득
                ObjectManager.instance.GetItem(1011);
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[220].npc_name, dialogdb.NPC_01[220].comment));

            }

            //2023 : 3월보름날
            else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[232].npc_name, dialogdb.NPC_01[232].comment));
            }
            #endregion

            #region 아이템
            //1000 : 쌀
            else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[286].npc_name, dialogdb.NPC_01[286].comment));
            }

            //1005 : 주먹밥
            else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[295].npc_name, dialogdb.NPC_01[295].comment));
            }

            //1006 : 비녀 (이벤트?)
            else if (ObjectManager.instance.GetEquipObjectKey() == 1006)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[526].npc_name, dialogdb.NPC_01[526].comment, true));

                //베드엔딩1 양상군자
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[302].npc_name, dialogdb.NPC_01[302].comment, true));

                //배경 이미지 변경
                EndingManager.instance.ShowEndingBG();
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[410].npc_name, dialogdb.NPC_01[410].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[411].npc_name, dialogdb.NPC_01[411].comment, true));

                //베드엔딩 컬러로 변경
                EndingManager.instance.ChangeToBadEndingBG();
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[412].npc_name, dialogdb.NPC_01[412].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[413].npc_name, dialogdb.NPC_01[413].comment, true));
                //타이틀 이동
                EndingManager.instance.LoadTitleScene();

            }

            //1007 : 먹
            else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[310].npc_name, dialogdb.NPC_01[310].comment));
            }

            //1009 : 꽃
            else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[325].npc_name, dialogdb.NPC_01[325].comment));
            }

            //1011 : 사공의 물건 (이후대사 추가)
            else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[341].npc_name, dialogdb.NPC_01[341].comment));
            }

            //1013 : 새끼줄1
            else if (ObjectManager.instance.GetEquipObjectKey() == 1013)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[854].npc_name, dialogdb.NPC_01[854].comment));
            }
            //1014 : 새끼줄2
            else if (ObjectManager.instance.GetEquipObjectKey() == 1014)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[864].npc_name, dialogdb.NPC_01[864].comment));
            }
            #endregion

            #region 조합 단서
            //4023 : 공양미를 구한 방법
            else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[352].npc_name, dialogdb.NPC_01[352].comment));
            }

            //8032 : 사라진 두사람
            else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[376].npc_name, dialogdb.NPC_01[376].comment));
            }

            //4033 : 무역의 중단
            else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[560].npc_name, dialogdb.NPC_01[560].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[384].npc_name, dialogdb.NPC_01[384].comment));
            }

            //4018 : 청이의 가출
            else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[394].npc_name, dialogdb.NPC_01[394].comment));
            }
            #endregion

            //기본 대사
            else
            {
                yield return StartCoroutine(DialogManager.instance.NormalChat("장사꾼"));
            }
        }
    }
}