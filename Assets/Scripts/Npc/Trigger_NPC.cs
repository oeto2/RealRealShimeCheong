using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_NPC : MonoBehaviour
{
    //NPC�� �����ߴ���
    [SerializeField] private bool isTouchNPC;
    [SerializeField] private Dialogue NPCDalogue;
    private ITalkable _ITalkable;

    //��ȭ�� �� �� �ִ���
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
                Debug.Log("NPC ��� ����");

                //���̾�α� UI
                NPCDalogue.DialogueCanvas.SetActive(true);

                //��� ���
                StartCoroutine(_ITalkable.TextPractice());
            }

            //��ȭ ����
            else if (DialogManager.instance.isSentenceEnd)
            {
                Debug.Log("NPC ��ȭ ����");
                //�÷��̾� �̵����� ����
                NPCDalogue.controller_scr.TalkEnd();

                NPCDalogue.DialogueCanvas.SetActive(false);
                StopAllCoroutines();
                StartCoroutine(ReTalkDealay());

                //������ȭ ����
                DialogManager.instance.remainSentence = false;
                //��ȭ ��
                DialogManager.instance.isSentenceEnd = false;
                //�ؽ�Ʈ ����
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
