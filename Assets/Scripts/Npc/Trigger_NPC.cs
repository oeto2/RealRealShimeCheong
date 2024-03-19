using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Trigger_NPC : MonoBehaviour
{
    //NPC와 접촉했는지
    [SerializeField] private bool isTouchNPC;
    [SerializeField] private Dialogue NPCDalogue;
    private ITalkable _ITalkable;

    [Header("Dialogue State")]
    //대화를 할 수 있는지
    [SerializeField] private float reTalkDelayTime = 0.5f;
    [SerializeField] private bool enableTalk = true;

    private void Awake()
    {
        NPCDalogue = GetComponent<Dialogue>();
        _ITalkable = GetComponent<ITalkable>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isTouchNPC  && UIManager.instance.SentenceCondition())
        {
            if (isTouchNPC && !DialogManager.instance.remainSentence && enableTalk)
            {
                Debug.Log("NPC 대사 실행");

                //다이얼로그 UI
                NPCDalogue.DialogueCanvas.SetActive(true);

                //대사 출력
                StartCoroutine(_ITalkable.TextPractice());
            }

            //대화 종료
            else if (DialogManager.instance.isSentenceEnd)
            {
                Debug.Log("NPC 대화 종료");
                //플레이어 이동제한 해제
                NPCDalogue.controller_scr.TalkEnd();

                NPCDalogue.DialogueCanvas.SetActive(false);
                StopAllCoroutines();
                StartCoroutine(ReTalkDealay());

                //남은대화 없음
                DialogManager.instance.remainSentence = false;
                //대화 끝
                DialogManager.instance.isSentenceEnd = false;
                //텍스트 비우기
                DialogManager.instance.writerText = "";
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouchNPC = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchNPC = false;
        }
    }

    IEnumerator ReTalkDealay()
    {
        enableTalk = false;
        yield return new WaitForSeconds(reTalkDelayTime);
        enableTalk = true;
    }
}
