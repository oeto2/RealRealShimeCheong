using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_JangSeong : Dialogue, ITalkable
{
    //함께 사라진 두 사람 대화를 했는지 체크 (대화시 True)
    private bool clue8032Talk;

    //대화 로직 모음.
    public IEnumerator TextPractice()
    {
        //만약 향리댁 대화 튜토리얼 중이라면
        if (TutorialManager.instance.events == TutorialEvents.TalkToHyang)
        {
            Debug.Log("향리댁 튜토리얼 대사");
            yield return StartCoroutine(DialogManager.instance.ItemClueChat("향리 댁 부인", "ⓦ여기는 어쩐 일이오?", true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat("심학규", "ⓦ청이가 향리 댁에 오지 않았다고 한다.", true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat("심학규", "ⓦ어찌 된 일인지 주변을 수소문 해 보자.", true));
            yield return StartCoroutine(DialogManager.instance.ItemClueChat("심학규", "ⓦ게임에서의 하루는 실제 시간의 5분입니다. 하루가 지나면 심학규의 집으로 귀환 됩니다."));
            TutorialManager.instance.HyangTalkEnd = true;
        }

        else
        {
            #region 단서
            //2000 : 승상댁의 수양딸
            if (ObjectManager.instance.GetEquipObjectKey() == 2000)
            {
                //단서획득
                ObjectManager.instance.GetClue(2001);
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[532].npc_name, dialogdb.NPC_01[532].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[11].npc_name, dialogdb.NPC_01[11].comment));
            }
            //2001 : 청이의 거짓말
            else if (ObjectManager.instance.GetEquipObjectKey() == 2001)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[24].npc_name, dialogdb.NPC_01[24].comment));
            }
            //2002 : 청이의 행방
            else if (ObjectManager.instance.GetEquipObjectKey() == 2002)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[32].npc_name, dialogdb.NPC_01[32].comment));
            }
            //2003 : 청이와 장터
            else if (ObjectManager.instance.GetEquipObjectKey() == 2003)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[40].npc_name, dialogdb.NPC_01[40].comment));
            }
            //2004 : 청이와 사내
            else if (ObjectManager.instance.GetEquipObjectKey() == 2004)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[48].npc_name, dialogdb.NPC_01[48].comment));
            }
            //2005 : 누군가의 아들
            else if (ObjectManager.instance.GetEquipObjectKey() == 2005)
            {
                //새끼줄 이벤트 클리어시
                if (EventManager.instance.giveStraw)
                {
                    //향리댁 셋째 아들 단서 획득
                    ObjectManager.instance.GetClue(2013);
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[537].npc_name, dialogdb.NPC_01[537].comment, true));
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[59].npc_name, dialogdb.NPC_01[59].comment));
                }

                //아직 새끼줄 이벤트를 클리어하지 못했을 경우
                else
                {
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[537].npc_name, dialogdb.NPC_01[537].comment, true));
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[836].npc_name, dialogdb.NPC_01[836].comment, true));
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[836].npc_name, dialogdb.NPC_01[837].comment));
                }
            }
            //2006 : 송나라 상인과 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2006)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[67].npc_name, dialogdb.NPC_01[67].comment));
            }
            //2007 : 승려와 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2007)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[80].npc_name, dialogdb.NPC_01[80].comment));
            }
            //2008 : 승려의 마음
            else if (ObjectManager.instance.GetEquipObjectKey() == 2008)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[88].npc_name, dialogdb.NPC_01[88].comment));
            }
            //2009 : 청이의 도움
            else if (ObjectManager.instance.GetEquipObjectKey() == 2009)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[541].npc_name, dialogdb.NPC_01[541].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[96].npc_name, dialogdb.NPC_01[96].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[102].npc_name, dialogdb.NPC_01[102].comment, true));

                //배의 출항 단서획득
                ObjectManager.instance.GetClue(2017);
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[103].npc_name, dialogdb.NPC_01[103].comment));
            }
            //2010 : 공양미 삼백 석
            else if (ObjectManager.instance.GetEquipObjectKey() == 2010)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[106].npc_name, dialogdb.NPC_01[106].comment));
            }
            //2011 : 공양미의 출처
            else if (ObjectManager.instance.GetEquipObjectKey() == 2011)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[115].npc_name, dialogdb.NPC_01[115].comment));
            }
            //2012 : 청이의 거래
            else if (ObjectManager.instance.GetEquipObjectKey() == 2012)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[123].npc_name, dialogdb.NPC_01[123].comment));
            }
            //2013 : 향리 댁 셋째 아들
            else if (ObjectManager.instance.GetEquipObjectKey() == 2013)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[131].npc_name, dialogdb.NPC_01[131].comment));
            }
            //2014 : 잠잠해져야 할 물살
            else if (ObjectManager.instance.GetEquipObjectKey() == 2014)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[139].npc_name, dialogdb.NPC_01[139].comment));
            }
            //2015 : 청이가 사간 것
            else if (ObjectManager.instance.GetEquipObjectKey() == 2015)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[147].npc_name, dialogdb.NPC_01[147].comment));
            }
            //2016 : 짚신을 사간 청이
            else if (ObjectManager.instance.GetEquipObjectKey() == 2016)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[157].npc_name, dialogdb.NPC_01[157].comment));
            }
            //2017 : 배의 출항
            else if (ObjectManager.instance.GetEquipObjectKey() == 2017)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[167].npc_name, dialogdb.NPC_01[167].comment));
            }
            //2018 : 노점의 단골
            else if (ObjectManager.instance.GetEquipObjectKey() == 2018)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[184].npc_name, dialogdb.NPC_01[184].comment));
            }
            //2019 : 뜨지 않는 배
            else if (ObjectManager.instance.GetEquipObjectKey() == 2019)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[192].npc_name, dialogdb.NPC_01[192].comment));
            }
            //2020 : 사공에게 있었던 일
            else if (ObjectManager.instance.GetEquipObjectKey() == 2020)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[201].npc_name, dialogdb.NPC_01[201].comment));
            }
            //2021 : 사공의 물건
            else if (ObjectManager.instance.GetEquipObjectKey() == 2021)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[207].npc_name, dialogdb.NPC_01[207].comment));
            }
            //2022 : 뱃길을 잠재울 방법
            else if (ObjectManager.instance.GetEquipObjectKey() == 2022)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[223].npc_name, dialogdb.NPC_01[223].comment));
            }
            //2023 : 3월 보름날
            else if (ObjectManager.instance.GetEquipObjectKey() == 2023)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[555].npc_name, dialogdb.NPC_01[555].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[231].npc_name, dialogdb.NPC_01[231].comment, true));

                //엔딩 화면 보이기
                EndingManager.instance.ShowEndingBG();
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[676].npc_name, dialogdb.NPC_01[676].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[677].npc_name, dialogdb.NPC_01[677].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[678].npc_name, dialogdb.NPC_01[678].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[679].npc_name, dialogdb.NPC_01[679].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[680].npc_name, dialogdb.NPC_01[680].comment, true));

                //타이틀 화면 이동
                EndingManager.instance.LoadTitleScene();
            }
            #endregion

            #region 아이템
            //1000 : 쌀
            else if (ObjectManager.instance.GetEquipObjectKey() == 1000)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[285].npc_name, dialogdb.NPC_01[285].comment));
            }
            //1005 : 주먹밥
            else if (ObjectManager.instance.GetEquipObjectKey() == 1005)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[294].npc_name, dialogdb.NPC_01[294].comment));
            }
            //1007 : 먹
            else if (ObjectManager.instance.GetEquipObjectKey() == 1007)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[527].npc_name, dialogdb.NPC_01[527].comment, true));

                //먹 아이템 제거
                ObjectManager.instance.RemoveItem(1007);
                //먹 전달 완료
                EventManager.instance.eventProgress.deliveryMuck = true;
                //먹 전달 이벤트 완료
                EventManager.instance.eventEndCheck.muckEvent_End = true;

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[309].npc_name, dialogdb.NPC_01[309].comment));
            }
            //1009 : 꽃
            else if (ObjectManager.instance.GetEquipObjectKey() == 1009)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[324].npc_name, dialogdb.NPC_01[324].comment));
            }
            //1011 : 사공의 물건
            else if (ObjectManager.instance.GetEquipObjectKey() == 1011)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[340].npc_name, dialogdb.NPC_01[340].comment));
            }
            //1012 : 볏짚
            else if (ObjectManager.instance.GetEquipObjectKey() == 1012)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[844].npc_name, dialogdb.NPC_01[844].comment));
            }

            //1013 : 새끼줄 1
            else if (ObjectManager.instance.GetEquipObjectKey() == 1013)
            {
                //새끼줄 아이템 제거
                ObjectManager.instance.RemoveItem(1013);

                //새끼줄 전달완료
                EventManager.instance.giveStraw = true;
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[842].npc_name, dialogdb.NPC_01[842].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[838].npc_name, dialogdb.NPC_01[838].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[840].npc_name, dialogdb.NPC_01[840].comment));
            }

            //1014 : 새끼줄 2
            else if (ObjectManager.instance.GetEquipObjectKey() == 1014)
            {
                //새끼줄 아이템 제거
                ObjectManager.instance.RemoveItem(1014);

                //새끼줄 전달완료
                EventManager.instance.giveStraw = true;
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[842].npc_name, dialogdb.NPC_01[842].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[839].npc_name, dialogdb.NPC_01[839].comment, true));
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[840].npc_name, dialogdb.NPC_01[840].comment));
            }

            #endregion

            #region 조합 단서
            //4023 : 공양미를 구한 방법
            else if (ObjectManager.instance.GetEquipObjectKey() == 4023)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[351].npc_name, dialogdb.NPC_01[351].comment));
            }
            //4015 : 청이가 사라진 날
            else if (ObjectManager.instance.GetEquipObjectKey() == 4015)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[359].npc_name, dialogdb.NPC_01[359].comment));
            }
            //4017 : 청이와 그의 관계
            else if (ObjectManager.instance.GetEquipObjectKey() == 4017)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[367].npc_name, dialogdb.NPC_01[367].comment));
            }
            //8032 : 함께 사라진 두 사람
            else if (ObjectManager.instance.GetEquipObjectKey() == 8032)
            {

                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[559].npc_name, dialogdb.NPC_01[559].comment, true));

                //첫번째 대화 시
                if (!clue8032Talk)
                {
                    clue8032Talk = true;
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[375].npc_name, dialogdb.NPC_01[375].comment));
                }
                else
                {
                    //베드엔딩2 구화지문

                    //엔딩 배경 보이기
                    EndingManager.instance.ShowEndingBG();
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[414].npc_name, dialogdb.NPC_01[414].comment, true));
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[415].npc_name, dialogdb.NPC_01[415].comment, true));

                    //베드엔딩 이미지로 변경
                    EndingManager.instance.ChangeToBadEndingBG();
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[416].npc_name, dialogdb.NPC_01[416].comment, true));
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[417].npc_name, dialogdb.NPC_01[417].comment, true));
                    yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[418].npc_name, dialogdb.NPC_01[418].comment, true));

                    //타이틀로 이동
                    EndingManager.instance.LoadTitleScene();
                }

            }
            //4033 : 무역의 중단
            else if (ObjectManager.instance.GetEquipObjectKey() == 4033)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[383].npc_name, dialogdb.NPC_01[383].comment));
            }
            //4018 : 청이의 가출
            else if (ObjectManager.instance.GetEquipObjectKey() == 4018)
            {
                yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[393].npc_name, dialogdb.NPC_01[393].comment));
            }
            #endregion

            #region 기본 대사
            else
            {
                Debug.Log("기본대사 실행");
                yield return StartCoroutine(DialogManager.instance.NormalChat("향리 댁 부인"));
            }
            #endregion
        }
    }
}