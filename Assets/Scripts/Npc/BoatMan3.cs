using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatMan3 : Dialogue, ITalkable
{
    //�۳��� ���ΰ� û�� ������ ��
    public int int_Select2006Num = 0;

    //2006�� ������ �� ��¥
    public int int_select2006Day = 0;

    //������ �Ϸ��ߴ���
    public bool isSelectDone;

    public IEnumerator TextPractice()
    {
        Debug.Log("������ ǥ���ϱ�");

        //������ ����
        EventManager.instance.SelectStart(NPCName.boatman3, 7194);
        yield return null;
    }

    IEnumerator EndingSentence()
    {
        //��� ��Ӱ� ����
        EndingManager.instance.ShowEndingBG();
        EndingManager.instance.BrightEndingBG();

        Debug.Log("���� ��� ����");

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

        //������ ���� (1.���Ϸ� �پ���, 2.������ �ִ´�)
        EventManager.instance.SelectStart(NPCName.Shimbongsa, 7287);
    }

    //���� ��� ����
    public void StartEndingSentence()
    {
        StartCoroutine(EndingSentence());
    }
}
