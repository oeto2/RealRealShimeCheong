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
    public ObjectControll objCtrlScr;

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
    public bool setence1End;

    //오브젝트 둘다 획득
    public bool getObjects;

    //뺑덕 대화 끝
    public bool SentenceEnd_Bbang;
    //향리댁 대화 끝
    public bool SentenceEnd_Hyang;

    //흐름 제어용 Flags
    private bool BangtalkEnd;
    private bool HyangTalkEnd1;
    private bool HyangTalkEnd2;

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
                    //Player 이동제한
                    playerCtrlScr.TalkStart();

                    //메모 끄기
                    gameObject_shimeNote.SetActive(false);

                    //1번 대화 실행
                    playerDialogueScr.Start_Sentence1();

                    Invoke("CloseNote", 0.2f);
                }
            }

            //이어서 대화 시작
            if (!setence1End && playerDialogueScr.isTalkEnd && closeNote && Input.GetKeyDown(KeyCode.Z))
            {
                //Player 이동제한
                playerCtrlScr.TalkStart();

                playerDialogueScr.Start_Sentence1_2();

                Invoke("Sentence1End", 0.2f);
            }
        }

        //2번째 대화가 끝나고 Z키 누르면 다이얼로그창 끄기
        if (setence1End && Input.GetKeyDown(KeyCode.Z) && !objCtrlScr.getBotzime && !objCtrlScr.getMap && playerDialogueScr.isTalkEnd)
        {
            //Player 이동제한 해제
            playerCtrlScr.TalkEnd();

            //UI 캔버스 보이기
            gameObject_UICanvas.SetActive(true);

            gameObject_Dialogue.SetActive(false);
        }
        #endregion

        //오브젝트 획득 하기 튜토리얼
        #region
        if (setence1End && !getObjects)
        {
            //봇짐 획득 후 창끄기
            if (setence1End && Input.GetKeyDown(KeyCode.Z) && objCtrlScr.getBotzime && playerDialogueScr.isTalkEnd)
            {
                //Player 이동제한 해제
                playerCtrlScr.TalkEnd();

                gameObject_Dialogue.SetActive(false);
            }

            //맵 획득 후 창끄기
            if (setence1End && Input.GetKeyDown(KeyCode.Z) && objCtrlScr.getMap && playerDialogueScr.isTalkEnd)
            {
                //Player 이동제한 해제
                playerCtrlScr.TalkEnd();

                gameObject_Dialogue.SetActive(false);
            }

            if (!gameObject_Dialogue.activeSelf && objCtrlScr.getMap && objCtrlScr.getBotzime)
            {
                //둘다 획득 대화 실행
                playerDialogueScr.Start_Sentence_GetObjcets();

                //Player 이동제한
                playerCtrlScr.TalkStart();

                getObjects = true;
            }
        }
        #endregion
        {
            if (setence1End && getObjects && Input.GetKeyDown(KeyCode.Z) && playerDialogueScr.isTalkEnd)
            {
                //Player 이동제한 해제
                playerCtrlScr.TalkEnd();

                gameObject_Dialogue.SetActive(false);
            }

            //뺑떡 어멈의 말이 끝났을 경우
            if (setence1End && getObjects && SentenceEnd_Bbang && !BangtalkEnd)
            {
                Debug.Log("뺑떡이야기 후 대화 실행");
                playerCtrlScr.TalkStart();
                playerDialogueScr.Start_Sentence_BbangEnd();

                BangtalkEnd = true;
            }

            //문장이 전부 출력이 되었다면 Z키를 눌러 다이얼로그 끄기
            if (playerDialogueScr.isTalkEnd && Input.GetKeyDown(KeyCode.Z) && BangtalkEnd && !SentenceEnd_Hyang)
            {
                playerCtrlScr.TalkEnd();
                gameObject_Dialogue.SetActive(false);
            }

            //향리댁 대화가 모두 끝났을 경우
            if(getObjects && SentenceEnd_Bbang && SentenceEnd_Hyang && !HyangTalkEnd1 && !HyangTalkEnd2)
            {
                playerCtrlScr.TalkStart();
                //향리댁 1번대화
                playerDialogueScr.Start_Sentence_HyangEnd1();

                HyangTalkEnd1 = true;
            }

            //1번대화가 모두 끝나고 Z키 누를경우
            if(SentenceEnd_Hyang && HyangTalkEnd1 && playerDialogueScr.isTalkEnd && Input.GetKeyDown(KeyCode.Z) && !HyangTalkEnd2)
            {
                //향리댁 2번 대화
                playerDialogueScr.Start_Sentence_HyangEnd2();

                HyangTalkEnd2 = true;
            }

            //2번대화가 모두 끝나고 Z키를 누를경우
            if(playerDialogueScr.isTalkEnd && HyangTalkEnd2 && Input.GetKeyDown(KeyCode.Z))
            {
                playerCtrlScr.TalkEnd();

                //다이얼로그 종료
                gameObject_Dialogue.SetActive(false);
            }
        }
        
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

    //튜토리얼 뺑덕 대화가 모두 마무리 되었는지
    public void TutorialSenteceEnd_Bbang()
    {
        SentenceEnd_Bbang = true;
    }

    //튜토리얼 향리댁 대화가 모두 마무리 되었는지
    public void TutorialSentenceEnd_Hyang()
    {
        SentenceEnd_Hyang = true;
    }
}
