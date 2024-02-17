using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    // ��ȭ�� ������ �ѱ� �� �ִ� Ű(default : space)
    public List<KeyCode> skipButton;

    public GameObject DialogueCanvas;

    public Sprite[] images_NPC_portrait;

    public Trigger_NPC trigger_npc;

    public Controller controller_scr;

    //��ȭ�� ���� ��� �Ǿ�����
    public bool isSentenceEnd = false;

    //���� ��ȭ�� �� �ִ���
    public bool remainSentence = false;

    // ���ڻ� ���� ����
    protected bool t_white = false;
    protected bool t_red = false;

    // ���ڻ� ���� ���ڴ� ��� ��� ����
    protected bool t_ignore = false;

    protected S_NPCdatabase_Yes dialogdb;

    private void Start()
    {
        dialogdb = DialogManager.instance.npcDatabaseScr;
        DialogueCanvas = DialogManager.instance.Dialouge_Canvas;
    }
}
