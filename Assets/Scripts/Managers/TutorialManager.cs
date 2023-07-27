using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    //외부 스크립트
    public TurnOnLight turnOnLightScr;
    public ObjectManager objectManagerScr;
    public Controller playerCtrlScr;
    public Dialog_TypingWriter_ShimBongSa playerDialogueScr;

    //나레이션 오브젝트
    public GameObject gameObject_NarationBG;

    //UI Canvas
    public GameObject gameObject_UICanvas;

    //다이얼로그 UI
    public GameObject gameObject_Dialogue;

    //Input Key Text Object
    public GameObject gameObject_InputKeyText;

    //등잔 불 오브젝트
    public GameObject gameObject_DeungJanLight;

    //심청이의 메모
    public GameObject gameObject_shimeNote;

    //나레이션 BackGround Image
    public Image image_NarationBG;

    //나레이션 텍스트
    public Text text_Naration;

    //나레이션배경 애니메이터
    public Animator animator_NarationBG;

    //심청이 메모를 보여줬는지 : true, false
    private bool showNote;

    //심청이 메모를 껐는지
    [SerializeField]
    private bool closeNote;

    //첫번째 대화 끝
    private bool setence1End;

    // Start is called before the first frame update
    void Start()
    {
        //튜토리얼 시작 전 사전 작업들
        #region
        //UI Canvas OFF
        gameObject_UICanvas.SetActive(false);
        //Player 이동 제한
        playerCtrlScr.TalkStart();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        //튜토리얼 Chaptor 1 End
        #region
        if (!setence1End)
        {
            //스페이스 바를 눌러 독백 창 끄기
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
            {
                //Player 이동 제한 해제
                playerCtrlScr.TalkEnd();

                //Input key Text OFF
                gameObject_InputKeyText.SetActive(false);

                //텍스트 창 비우기
                text_Naration.text = "";

                //서서히 나레이션 배경 FadeOut 시작
                animator_NarationBG.SetBool("FadeOutStart", true);

                //나레이션 배경 끄기
                Invoke("ActiveFalse_NarationBG", 1.5f);
            }

            //불이 켜졌을경우
            if (turnOnLightScr.isTrunOnLight && !showNote)
            {
                Debug.Log("실행");
                //1초뒤에 메모 등작
                Invoke("ShowShimNote", 1f);

                //Player 이동제한
                playerCtrlScr.TalkStart();

                showNote = true;
            }

            //노트를 읽고 난 뒤 Z or Space를 누른다
            if (gameObject_shimeNote.activeSelf)
            {
                //메모 끄기
                if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space)) && !closeNote)
                {
                    //Player 이동제한 해제
                    playerCtrlScr.TalkEnd();

                    //메모 끄기
                    gameObject_shimeNote.SetActive(false);

                    //1번 대화 실행
                    playerDialogueScr.Start_Sentence1();

                    Invoke("CloseNote", 0.2f);
                }
            }

            //이어서 대화 시작
            if (!setence1End&& playerDialogueScr.isTalkEnd && closeNote && Input.GetKeyDown(KeyCode.Z))
            {
                playerDialogueScr.Start_Sentence1_2();

                Invoke("Sentence1End", 0.2f);
            }
        }

        //2번째 대화가 끝나고 Z키 누르면 다이얼로그창 끄기
        if (setence1End && Input.GetKeyDown(KeyCode.Z))
        {
            gameObject_Dialogue.SetActive(false);
        }
        #endregion

    }

    //나레이션 배경 끄기
    private void ActiveFalse_NarationBG()
    {
        gameObject_NarationBG.SetActive(false);
    }

    //심청이의 쪽지 보여주기
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
