using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_TypingWriter_BoatMan2 : Dialogue,ITalkable
{
    //�۳��� ���ΰ� û�� ������ ��
    public int int_Select2006Num = 0;

    //2006�� ������ �� ��¥
    public int int_select2006Day = 0;

    //���� Ŭ��
    public IEnumerator TextPractice()
    {
        //�⺻ ��� ����
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[765].npc_name, dialogdb.NPC_01[765].comment, true));

        //������ ����
        EventManager.instance.SelectStart(NPCName.boatman2, 7355);
    }

    //������� ��忣�� �ڷ�ƾ
    IEnumerator BoatManEnding()
    {
        //ȭ�� ��Ӱ� �ϱ�
        EndingManager.instance.ShowEndingBG();

        //��� ����
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

        //Ÿ��Ʋ ȭ�� �̵�
        EndingManager.instance.LoadTitleScene();
    }

    //��� ���� ��忣�� ����
    public void StartBoatManEnding_1()
    {
        StartCoroutine(BoatManEnding());
    }


    //�� ���� ���� �ڷ�ƾ
    IEnumerator GoodEndingRoot()
    {
        //�ð��� �� �̻� �帣�� ����
        TimeManager.instance.RealTimeStop();

        //ȭ�� ��Ӱ� �ϱ�
        EndingManager.instance.ShowEndingBG();

        //��� ����
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[240].npc_name, dialogdb.NPC_01[240].comment, true));

        //�ɺ���, ��ġ ����
        GameManager.instance.TransferPlayer(GameManager.instance.oceanSponPos.position, 6);

        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[241].npc_name, dialogdb.NPC_01[241].comment, true));

        //��� õõ�� ����ϱ�
        EndingManager.instance.BrightEndingBG();

        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[242].npc_name, dialogdb.NPC_01[242].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[243].npc_name, dialogdb.NPC_01[243].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[244].npc_name, dialogdb.NPC_01[244].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[245].npc_name, dialogdb.NPC_01[245].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[246].npc_name, dialogdb.NPC_01[246].comment, true));
        yield return StartCoroutine(DialogManager.instance.ItemClueChat(dialogdb.NPC_01[247].npc_name, dialogdb.NPC_01[247].comment, true));

        //���̾�α�â ����
        DialogManager.instance.Dialouge_Canvas.SetActive(false);
        remainSentence = true;
        isSentenceEnd = true;
        controller_scr.TalkEnd();
    }

    //��� ���� ��忣�� ����
    public void StartGoodEndingRoot()
    {
        StartCoroutine(GoodEndingRoot());
    }
}