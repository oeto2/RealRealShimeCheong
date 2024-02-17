using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    // 대화를 빠르게 넘길 수 있는 키(default : space)
    public List<KeyCode> skipButton;

    public GameObject DialogueCanvas;

    public Sprite[] images_NPC_portrait;

    public Trigger_NPC trigger_npc;

    public Controller controller_scr;

    //대화가 전부 출력 되었는지
    public bool isSentenceEnd = false;

    //남은 대화가 더 있는지
    public bool remainSentence = false;

    // 글자색 설정 변수
    protected bool t_white = false;
    protected bool t_red = false;

    // 글자색 설정 문자는 대사 출력 무시
    protected bool t_ignore = false;

    protected S_NPCdatabase_Yes dialogdb;

    private void Start()
    {
        dialogdb = DialogManager.instance.npcDatabaseScr;
        DialogueCanvas = DialogManager.instance.Dialouge_Canvas;
    }
}
