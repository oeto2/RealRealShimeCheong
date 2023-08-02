using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ
    public TurnOnLight turnOnLightScr;
    public ObjectManager objectManagerScr;
    public Controller playerCtrlScr;
    public Dialog_TypingWriter_ShimBongSa playerDialogueScr;
    public ObjectControll objCtrlScr;
    public TimeManager timeManagerScr;
    public GameManager gameManagerScr;

    //�����̼� ������Ʈ
    public GameObject gameObject_NarationBG;

    //UI Canvas
    public GameObject gameObject_UICanvas;

    //���̾�α� UI
    public GameObject gameObject_Dialogue;

    //Input Key Text Object
    public GameObject gameObject_InputKeyText;

    //���� �� ������Ʈ
    public GameObject gameObject_DeungJanLight;

    //��û���� �޸�
    public GameObject gameObject_shimeNote;

    //�����̼� BackGround Image
    public Image image_NarationBG;

    //�����̼� �ؽ�Ʈ
    public Text text_Naration;

    //�����̼ǹ�� �ִϸ�����
    public Animator animator_NarationBG;

    //��û�� �޸� ��������� : true, false
    private bool showNote;

    //��û�� �޸� ������
    [SerializeField]
    private bool closeNote;

    //ù��° ��ȭ ��
    public bool setence1End;

    //������Ʈ �Ѵ� ȹ��
    public bool getObjects;

    //�Ϸ簡 �������� Ȯ���ϴ� falg
    public bool passDay;

    //���� ��ȭ ��
    public bool SentenceEnd_Bbang;
    //�⸮�� ��ȭ ��
    public bool SentenceEnd_Hyang;
    //�⸮�� ��ȭ ������ �ɺ��� ��ȭ ��
    private bool SentenceEnd_HyangShim;

    //�帧 ����� Flags
    private bool BangtalkEnd;
    private bool HyangTalkEnd1;
    private bool HyangTalkEnd2;
    private bool HyangTalkEnd3;
    private bool PassDayTalkEnd1;
    private bool PassDayTalkEnd2;
    private bool PassDayTalkEnd3;

    public enum TutorialProgress
    {
    } 

    // Start is called before the first frame update
    void Start()
    {
        //Ʃ�丮�� ���� �� ���� �۾���
        #region
        ////UI Canvas OFF
        //gameObject_UICanvas.SetActive(false);
        //Player �̵� ����
        playerCtrlScr.TalkStart();

        //�ð� ���߱�
        timeManagerScr.StopTime();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Ʃ�丮�� Chaptor 1 End

        if (!setence1End)
        {
            //�����̽� �ٸ� ���� ���� â ����
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
            {
                //Player �̵� ���� ����
                playerCtrlScr.TalkEnd();

                //Input key Text OFF
                gameObject_InputKeyText.SetActive(false);

                //�ؽ�Ʈ â ����
                text_Naration.text = "";

                //������ �����̼� ��� FadeOut ����
                animator_NarationBG.SetBool("FadeOutStart", true);

                //�����̼� ��� ����
                Invoke("ActiveFalse_NarationBG", 1.5f);
            }

            //���� ���������
            if (turnOnLightScr.isTrunOnLight && !showNote)
            {
                Debug.Log("����");
                //1�ʵڿ� �޸� ����
                Invoke("ShowShimNote", 1f);

                //Player �̵�����
                playerCtrlScr.TalkStart();

                showNote = true;
            }

            //��Ʈ�� �а� �� �� Z or Space�� ������
            if (gameObject_shimeNote.activeSelf)
            {
                //�޸� ����
                if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space)) && !closeNote)
                {
                    //Player �̵�����
                    playerCtrlScr.TalkStart();

                    //�޸� ����
                    gameObject_shimeNote.SetActive(false);

                    //1�� ��ȭ ����
                    playerDialogueScr.Start_Sentence1();

                    Invoke("CloseNote", 0.2f);
                }
            }

            //�̾ ��ȭ ����
            if (!setence1End && playerDialogueScr.isTalkEnd && closeNote && Input.GetKeyDown(KeyCode.Z))
            {
                //Player �̵�����
                playerCtrlScr.TalkStart();

                playerDialogueScr.Start_Sentence1_2();

                Invoke("Sentence1End", 0.2f);
            }
        }

        //2��° ��ȭ�� ������ ZŰ ������ ���̾�α�â ����
        if (setence1End && Input.GetKeyDown(KeyCode.Z) && !objCtrlScr.getBotzime && !objCtrlScr.getMap && playerDialogueScr.isTalkEnd)
        {
            //Player �̵����� ����
            playerCtrlScr.TalkEnd();

            //UI ĵ���� ���̱�
            gameObject_UICanvas.SetActive(true);

            gameObject_Dialogue.SetActive(false);
        }
        #endregion

        #region ������Ʈ ȹ�� Ʃ�丮��

        if (setence1End && !getObjects)
        {
            //���� ȹ�� �� â����
            if (setence1End && Input.GetKeyDown(KeyCode.Z) && objCtrlScr.getBotzime && playerDialogueScr.isTalkEnd)
            {
                //Player �̵����� ����
                playerCtrlScr.TalkEnd();

                gameObject_Dialogue.SetActive(false);
            }

            //�� ȹ�� �� â����
            if (setence1End && Input.GetKeyDown(KeyCode.Z) && objCtrlScr.getMap && playerDialogueScr.isTalkEnd)
            {
                //Player �̵����� ����
                playerCtrlScr.TalkEnd();

                gameObject_Dialogue.SetActive(false);
            }

            if (!gameObject_Dialogue.activeSelf && objCtrlScr.getMap && objCtrlScr.getBotzime)
            {
                //�Ѵ� ȹ�� ��ȭ ����
                playerDialogueScr.Start_Sentence_GetObjcets();

                //Player �̵�����
                playerCtrlScr.TalkStart();

                getObjects = true;
            }
        }
        #endregion

        if (setence1End && getObjects && Input.GetKeyDown(KeyCode.Z) && playerDialogueScr.isTalkEnd)
        {
            //Player �̵����� ����
            playerCtrlScr.TalkEnd();

            gameObject_Dialogue.SetActive(false);
        }

        //���� ����� ���� ������ ���
        if (setence1End && getObjects && SentenceEnd_Bbang && !BangtalkEnd)
        {
            Debug.Log("�����̾߱� �� ��ȭ ����");
            playerCtrlScr.TalkStart();
            playerDialogueScr.Start_Sentence_BbangEnd();

            BangtalkEnd = true;
        }

        //������ ���� ����� �Ǿ��ٸ� ZŰ�� ���� ���̾�α� ����
        if (playerDialogueScr.isTalkEnd && Input.GetKeyDown(KeyCode.Z) && BangtalkEnd && !SentenceEnd_Hyang)
        {
            playerCtrlScr.TalkEnd();
            gameObject_Dialogue.SetActive(false);
        }

        //�⸮�� ��ȭ�� ��� ������ ���
        if (getObjects && SentenceEnd_Bbang && SentenceEnd_Hyang && !HyangTalkEnd1 && !HyangTalkEnd2)
        {
            playerCtrlScr.TalkStart();
            //�⸮�� 1����ȭ
            playerDialogueScr.Start_Sentence_HyangEnd1();

            HyangTalkEnd1 = true;
        }

        //1����ȭ�� ��� ������ ZŰ �������
        if (SentenceEnd_Hyang && HyangTalkEnd1 && playerDialogueScr.isTalkEnd && Input.GetKeyDown(KeyCode.Z) && !HyangTalkEnd2)
        {
            //�⸮�� 2�� ��ȭ
            playerDialogueScr.Start_Sentence_HyangEnd2();

            HyangTalkEnd2 = true;
        }

        //2����ȭ�� ��� ������ ZŰ�� �������
        if (playerDialogueScr.isTalkEnd && HyangTalkEnd2 && Input.GetKeyDown(KeyCode.Z) && !HyangTalkEnd3)
        {
            //�⸮�� 3�� ��ȭ
            playerDialogueScr.Start_Sentence_HyangEnd3();

            HyangTalkEnd3 = true;
        }

        //3����ȭ�� ��� ������ Z Ű�� �������
        if (playerDialogueScr.isTalkEnd && HyangTalkEnd1 && HyangTalkEnd3 && Input.GetKeyDown(KeyCode.Z) && !SentenceEnd_HyangShim)
        {
            //��ȭ ��
            playerCtrlScr.TalkEnd();

            //���̾�α� ����
            gameObject_Dialogue.SetActive(false);

            //�ð� ����
            timeManagerScr.ResetTime();

            //��¥ UI �����ֱ�
            timeManagerScr.ShowDayUI();

            //�ð� �帣��
            timeManagerScr.ContinueTime();

            SentenceEnd_HyangShim = true;
        }


        //�Ϸ簡 ������ ���
        if (passDay && !PassDayTalkEnd1)
        {
            //��ȭ ����
            playerCtrlScr.TalkStart();



            //�Ϸ� ������ ��ȭ 1
            playerDialogueScr.Start_Sentence_PassDay();

            PassDayTalkEnd1 = true;
        }

        if (playerDialogueScr.isTalkEnd && PassDayTalkEnd1 && !PassDayTalkEnd2 && Input.GetKeyDown(KeyCode.Z) && passDay)
        {
            //�Ϸ� ������ ��ȭ 2
            playerDialogueScr.Start_Sentence_PassDay2();

            PassDayTalkEnd2 = true;
        }

        if (playerDialogueScr.isTalkEnd && PassDayTalkEnd2 && !PassDayTalkEnd3 && Input.GetKeyDown(KeyCode.Z) && passDay)
        {
            //�Ϸ� ������ ��ȭ 3
            playerDialogueScr.Start_Sentence_PassDay3();

            PassDayTalkEnd3 = true;
        }

        if (playerDialogueScr.isTalkEnd && PassDayTalkEnd3 && Input.GetKeyDown(KeyCode.Z) && passDay)
        {
            //��ȭ ��
            playerCtrlScr.TalkEnd();

            //���̾�α� â ����
            gameObject_Dialogue.SetActive(false);

            //�÷��� �ʱ�ȭ
            passDay = false;

            //�ð� �帣��
            timeManagerScr.PassDaySentenceEnd();
        }
    }

    //�Ϸ簡 ��������(TimeManager���� ����)
    public void PassDay()
    {
        //Player ��ġ �ʱ�ȭ
        gameManagerScr.ReturnPlayer();

        //Player ������ ����
        playerCtrlScr.TalkStart();

        //�ɺ��� �̵��� ���ʵڿ� ���̾�α� ������
        Invoke("PassDayTrue", 1.5f);
    }

    //PassDay Flag Dealy��
    private void PassDayTrue()
    {
        passDay = true;
    }


    //�����̼� ��� ����
    private void ActiveFalse_NarationBG()
    {
        gameObject_NarationBG.SetActive(false);
    }

    //��û���� ���� �����ֱ�
    private void ShowShimNote()
    {
        gameObject_shimeNote.SetActive(true);
    }

    private void CloseNote()
    {
        closeNote = true;
    }

    //1�� ��ȭ�� ��������
    private void Sentence1End()
    {
        setence1End = true;
    }

    //Ʃ�丮�� ���� ��ȭ�� ��� ������ �Ǿ�����
    public void TutorialSenteceEnd_Bbang()
    {
        SentenceEnd_Bbang = true;
    }

    //Ʃ�丮�� �⸮���� ���ϴ°� ��� ��������
    public void TutorialSentenceEnd_Hyang()
    {
        SentenceEnd_Hyang = true;
    }
}
