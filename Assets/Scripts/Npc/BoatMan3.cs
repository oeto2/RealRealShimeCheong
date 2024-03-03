using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatMan3 : Dialogue, ITalkable
{
    //송나라 상인과 청이 선택지 값
    public int int_Select2006Num = 0;

    //2006번 선택지 고른 날짜
    public int int_select2006Day = 0;

    //선택을 완료했는지
    public bool isSelectDone;

    public IEnumerator TextPractice()
    {
        Debug.Log("선택지 표시하기");

        //선택지 시작
        EventManager.instance.SelectStart(NPCName.boatman3, 7194);
        yield return null;
    }

    IEnumerator EndingSentence()
    {
        //배경 어둡게 변경
        EndingManager.instance.ShowEndingBG();
        EndingManager.instance.BrightEndingBG();

        Debug.Log("엔딩 대사 시작");

        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[681].npc_name, dialogdb.NPC_01[681].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[682].npc_name, dialogdb.NPC_01[682].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[683].npc_name, dialogdb.NPC_01[683].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[684].npc_name, dialogdb.NPC_01[684].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[685].npc_name, dialogdb.NPC_01[685].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[686].npc_name, dialogdb.NPC_01[686].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[687].npc_name, dialogdb.NPC_01[687].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[688].npc_name, dialogdb.NPC_01[688].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[689].npc_name, dialogdb.NPC_01[689].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[690].npc_name, dialogdb.NPC_01[690].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[691].npc_name, dialogdb.NPC_01[691].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[692].npc_name, dialogdb.NPC_01[692].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[693].npc_name, dialogdb.NPC_01[693].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[694].npc_name, dialogdb.NPC_01[694].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[695].npc_name, dialogdb.NPC_01[695].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[696].npc_name, dialogdb.NPC_01[696].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[697].npc_name, dialogdb.NPC_01[697].comment, true));

        //선택지 진행 (1.구하러 뛰어든다, 2.가만히 있는다)
        EventManager.instance.SelectStart(NPCName.Shimbongsa, 7287);
    }

    //엔딩 대사 시작
    public void StartEndingSentence()
    {
        StartCoroutine(EndingSentence());
    }
}
