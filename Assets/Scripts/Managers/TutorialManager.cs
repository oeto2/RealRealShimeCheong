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
    private bool setence1End;

    // Start is called before the first frame update
    void Start()
    {
        //Ʃ�丮�� ���� �� ���� �۾���
        #region
        //UI Canvas OFF
        gameObject_UICanvas.SetActive(false);
        //Player �̵� ����
        playerCtrlScr.TalkStart();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        //Ʃ�丮�� Chaptor 1 End
        #region
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
                    //Player �̵����� ����
                    playerCtrlScr.TalkEnd();

                    //�޸� ����
                    gameObject_shimeNote.SetActive(false);

                    //1�� ��ȭ ����
                    playerDialogueScr.Start_Sentence1();

                    Invoke("CloseNote", 0.2f);
                }
            }

            //�̾ ��ȭ ����
            if (!setence1End&& playerDialogueScr.isTalkEnd && closeNote && Input.GetKeyDown(KeyCode.Z))
            {
                playerDialogueScr.Start_Sentence1_2();

                Invoke("Sentence1End", 0.2f);
            }
        }

        //2��° ��ȭ�� ������ ZŰ ������ ���̾�α�â ����
        if (setence1End && Input.GetKeyDown(KeyCode.Z))
        {
            gameObject_Dialogue.SetActive(false);
        }
        #endregion

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

    private void Sentence1End()
    {
        setence1End = true;
    }
}
