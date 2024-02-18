using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_BoatMan2 : Dialogue,ITalkable
{
    //송나라 상인과 청이 선택지 값
    public int int_Select2006Num = 0;

    //2006번 선택지 고른 날짜
    public int int_select2006Day = 0;

    //최초 클릭
    public IEnumerator TextPractice()
    {
        //기본 대사 진행
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[765].npc_name, dialogdb.NPC_01[765].comment, true));

        //선택지 시작
        EventManager.instance.SelectStart(NPCName.boatman2, 7355);
    }

    //계란유골 배드엔딩 코루틴
    IEnumerator BoatManEnding()
    {
        //화면 어둡게 하기
        EndingManager.instance.ShowEndingBG();

        //대사 진행
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[766].npc_name, dialogdb.NPC_01[766].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[767].npc_name, dialogdb.NPC_01[767].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[768].npc_name, dialogdb.NPC_01[768].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[769].npc_name, dialogdb.NPC_01[769].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[770].npc_name, dialogdb.NPC_01[770].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[771].npc_name, dialogdb.NPC_01[771].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[772].npc_name, dialogdb.NPC_01[772].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[773].npc_name, dialogdb.NPC_01[773].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[774].npc_name, dialogdb.NPC_01[774].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[775].npc_name, dialogdb.NPC_01[775].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[776].npc_name, dialogdb.NPC_01[776].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[778].npc_name, dialogdb.NPC_01[778].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[779].npc_name, dialogdb.NPC_01[779].comment));

        //타이틀 화면 이동
        EndingManager.instance.LoadTitleScene();
    }

    //계란 유골 배드엔딩 시작
    public void StartBoatManEnding_1()
    {
        StartCoroutine(BoatManEnding());
    }


    //굿 엔딩 진입 코루틴
    IEnumerator GoodEndingRoot()
    {
        //시간이 더 이상 흐르지 않음
        TimeManager.instance.RealTimeStop();

        //화면 어둡게 하기
        EndingManager.instance.ShowEndingBG();

        //대사 진행
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[240].npc_name, dialogdb.NPC_01[240].comment, true));

        //심봉사, 위치 변경
        GameManager.instance.TransferPlayer(GameManager.instance.oceanSponPos.position, 6);

        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[241].npc_name, dialogdb.NPC_01[241].comment, true));

        //배경 천천히 밝게하기
        EndingManager.instance.BrightEndingBG();

        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[242].npc_name, dialogdb.NPC_01[242].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[243].npc_name, dialogdb.NPC_01[243].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[244].npc_name, dialogdb.NPC_01[244].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[245].npc_name, dialogdb.NPC_01[245].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[246].npc_name, dialogdb.NPC_01[246].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[247].npc_name, dialogdb.NPC_01[247].comment, true));

        //다이얼로그창 끄기
        DialogManager.instance.Dialouge_Canvas.SetActive(false);
        remainSentence = true;
        isSentenceEnd = true;
        controller_scr.TalkEnd();
    }

    //계란 유골 배드엔딩 시작
    public void StartGoodEndingRoot()
    {
        StartCoroutine(GoodEndingRoot());
    }
}