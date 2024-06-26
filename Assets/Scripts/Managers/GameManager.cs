using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//저장할 데이터 클래스
[System.Serializable]
public class GameSaveData
{
    //생성자
    public GameSaveData(Vector3 _playerPos, int _LimitCamera, int _mapPinNum, int _joomuckBabState, bool _getJangjack,
        bool _getBagage, bool _getRice, bool _boatMan2_Show, int _herbEventState)
    {
        playerPos = _playerPos; limitCamera = _LimitCamera; mapPinNum = _mapPinNum;
        joomuckBabState = _joomuckBabState;
        getJangjack = _getJangjack;
        getBagage = _getBagage;
        getRice = _getRice;
        boatman2_Show = _boatMan2_Show;
        herbEventState = _herbEventState;
    }

    //플레이어 위치값
    public Vector3 playerPos;

    //카메라 제한 영역
    public int limitCamera;

    //지도의 핀 위치값
    public int mapPinNum;

    //주먹밥 이벤트 진행상황
    public int joomuckBabState;

    #region 아이템 획득 여부
    //장작 획득 여부
    public bool getJangjack;

    //바가지 획득 여부
    public bool getBagage;

    //쌀 획득 여부
    public bool getRice;

    #endregion

    //뱃사공2 이벤트 진행여부
    public bool boatman2_Show;

    //약초물 제작 이벤트 진행상황
    public int herbEventState;
}


//로드할 데이터 클래스
[System.Serializable]
public class GameLoadData
{
    //생성자
    public GameLoadData(Vector3 _playerPos, int _LimitCamera, int _mapPinNum, int _joomuckBabState, bool _getJangjack, bool _getBagage, bool _getRice
        , bool _boatMan2_Show, int _herbEventState)
    {
        playerPos = _playerPos; limitCamera = _LimitCamera; mapPinNum = _mapPinNum;
        joomuckBabState = _joomuckBabState;
        getJangjack = _getJangjack;
        getBagage = _getBagage;
        getRice = _getRice;
        boatman2_Show = _boatMan2_Show;
        herbEventState = _herbEventState;
    }

    //플레이어 위치값
    public Vector3 playerPos;

    //카메라 제한 영역
    public int limitCamera;

    //지도의 핀 위치값
    public int mapPinNum;

    //주먹밥 이벤트 진행상황
    public int joomuckBabState;

    #region 아이템 획득 여부
    //장작 획득 여부
    public bool getJangjack;

    //바가지 획득 여부
    public bool getBagage;

    //쌀 획득 여부
    public bool getRice;
    #endregion

    //뱃사공2 이벤트 진행여부
    public bool boatman2_Show;

    //약초물 제작 이벤트 진행상황
    public int herbEventState;
}

public class GameManager : MonoBehaviour
{
    //외부 스크립트
    public CameraMove cameraMoveScr;
    public UIManager uiManagerScr;
    public JoomackPuzzle joomackScr;
    public Dialog_TypingWriter_Guiduck dialogGuiduckScr;
    public Dialog_TypingWriter_BoatMan dialogBoatManScr;
    public JoomuckBab joomuckBabScr;
    public StrawPuzzle strawPuzzleScr;
    public Dialog_TypingWriter_ShimBongSa dialogSimbongScr;

    // 하나씩 추가하자
    public bool bool_isAction;
    public GameObject scanObject;
    public Text dialogText;
    public TalkManager talkManager;

    public int talkIndex;
    public GameObject talkPanel;

    //로딩 이미지 오브젝트
    public GameObject gameObjcet_Loading;

    //Player 오브젝트
    public GameObject gameObjcet_Player;
    //Player ReturnPos
    public Transform transform_PlayerReturn;

    //저장할 데이터 클래스
    public GameSaveData curGameSaveData;

    //불러올 데이터 클래스
    public GameLoadData curGameLoadData;

    //저장할 파일 위치
    private string saveFilePath;

    //현재 플레이가 위치한 장소 이름
    private string curPlaceName;

    #region 퍼즐

    //플레이어가 구슬 퍼즐중인지 확인하는 flag
    public bool isBeadPuzzleStart;

    //플레이어가 주막 퍼즐중인지 확인하는 flag
    public bool isJoomackPuzzleStart;

    //플레이어가 새끼줄 퍼즐중인지 확인하는 flag
    public bool isStrawPuzzleStart;

    //Bead Puzzle Map Transform
    public Transform transform_BeadPuzzleMap;

    //Joomack Puzzle Map Transform
    public Transform transform_JoomackMap;

    //straw Puzzle Map Transform
    public Transform transform_StrawMap;

    //맵의 핀위치 값
    public int int_PinPosNum;

    //싱글톤
    public static GameManager instance = null;

    //NPC 다이얼로그 이미지
    public GameObject gameObjcet_dialogueNPC;

    //인게임 UI 
    public GameObject gameObject_gameUI;

    //시간 UI
    public GameObject gameObjcet_timeUI;

    #endregion

    //장작 획득 여부
    public bool getJangjack;

    //바가지 획득 여부
    public bool getBagage;

    //쌀 획득 여부
    public bool getRice;

    //장작 오브젝트
    public GameObject gameObject_Jangjack;

    //바가지 오브젝트
    public GameObject gameObject_Bagage;

    //쌀 오브젝트
    public GameObject gameObject_Rice;

    //뱃사공2 오브젝트
    public GameObject gameObject_BoatMan2;

    //지금 현재 플레이어가 선택지를 진행중인지
    public bool isPlayerSelecting;

    //플레이어 게임 오브젝트
    public GameObject gameObject_Player;

    //Player 바다 SponPos
    public Transform oceanSponPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        saveFilePath = Application.persistentDataPath + "/GameManagerDataText.txt";
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            JoomackPuzzleStart();
        }
    }

    public void Action(GameObject scan_obj)
    {
        if (bool_isAction) // exit action
        {
            bool_isAction = false;
        }
        else
        {
            bool_isAction = true;
            scanObject = scan_obj;
            //objdata obj_Data = GameObject.Find("Stage").GetComponent<objdata>();
            Objdata obj_Data = scanObject.GetComponent<Objdata>();
            Talk(obj_Data.key, obj_Data.bool_isNPC);

            talkPanel.SetActive(bool_isAction);
        }
    }

    void Talk(int id, bool bool_isNPC)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            bool_isAction = false;
            talkIndex = 0;
            return;
        }
        if (bool_isNPC)
        {
            dialogText.text = talkData;

        }

        else
        {
            dialogText.text = talkData;
        }
        bool_isAction = true;
        talkIndex++;
    }

    //플레이어 귀환
    public void ReturnPlayer()
    {
        //플레이어 포지션 값 변경
        gameObjcet_Player.transform.position = new Vector3(transform_PlayerReturn.position.x, transform_PlayerReturn.position.y, 0);

        //카메라 영역제한 값 변경
        cameraMoveScr.ChangeLimit(0);
    }

    //Save Data
    public void Save(int _slotNum)
    {
        //Debug.Log("Save GameManagerData");

        //저장할 장소의 이름 구하기
        GetPlaceName();

        //저장할 데이터 넣기
        curGameSaveData = new GameSaveData(new Vector3(gameObjcet_Player.transform.position.x, gameObjcet_Player.transform.position.y,
                          gameObjcet_Player.transform.position.z), cameraMoveScr.int_CurLimitNum, int_PinPosNum, joomuckBabScr.GetEventState()
                          , getJangjack, getBagage, getRice, dialogBoatManScr.boatMan2_Show, joomuckBabScr.GetHerbEventState());

        //세이브 데이터
        string jSaveData = JsonUtility.ToJson(curGameSaveData);

        //데이터 파일 생성
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);
    }

    //데이터 로드
    public void Load(int _slotNum)
    {
        //Debug.Log("Load GameManagerData");

        if (_slotNum <= 2)
        {
            //세이브 파일 읽어오기
            string jLoadData = File.ReadAllText(saveFilePath + _slotNum.ToString());

            //읽어온 파일 리스트에 저장
            curGameLoadData = JsonUtility.FromJson<GameLoadData>(jLoadData);

            //플레이어 위치 재설정
            gameObjcet_Player.transform.position = curGameLoadData.playerPos;

            //카메라 제한구역 재설정
            cameraMoveScr.ChangeLimit(curGameLoadData.limitCamera);

            Debug.Log($"카메라 제한구역 : {curGameLoadData.limitCamera}");

            //지도 핀 위치 재설정
            int_PinPosNum = curGameLoadData.mapPinNum;
            //uiManagerScr.pinActionScr.PinPosChange(curGameLoadData.mapPinNum);

            //가마솥 세팅
            joomuckBabScr.EventSetting(curGameLoadData.joomuckBabState);

            //약초 이벤트 세팅
            joomuckBabScr.HerbEventSetting(curGameLoadData.herbEventState);

            //아이템 세팅

            //쌀
            if (curGameLoadData.getRice)
            {
                if (gameObject_Rice != null)
                {
                    gameObject_Rice.SetActive(false);
                }
            }
            else
            {
                if (gameObject_Rice != null)
                {
                    gameObject_Rice.SetActive(true);
                }
            }

            //장작
            if (curGameLoadData.getJangjack)
            {
                if (gameObject_Jangjack != null)
                {
                    gameObject_Jangjack.SetActive(false);
                }
            }
            else
            {
                if (gameObject_Jangjack != null)
                {
                    gameObject_Jangjack.SetActive(true);
                }
            }

            //바가지
            if (curGameLoadData.getBagage)
            {
                if (gameObject_Bagage != null)
                {
                    gameObject_Bagage.SetActive(false);
                }
            }
            else
            {
                if (gameObject_Bagage != null)
                {
                    gameObject_Bagage.SetActive(true);
                }
            }

            //뱃사공2 보이기
            if (curGameLoadData.boatman2_Show)
            {
                //뱃사공2 보이기
                gameObject_BoatMan2.SetActive(true);

                //플래그 초기화
                dialogBoatManScr.boatMan2_Show = true;
            }
        }
    }

    //현재 장소 이름 구하는 메서드
    public string GetPlaceName()
    {
        switch(cameraMoveScr.int_CurLimitNum)
        {
            case 0:
                curPlaceName = "장소: 안방";
                return curPlaceName;
            case 1:
                curPlaceName = "장소: 부엌";
                return curPlaceName;
            case 2:
                curPlaceName = "장소: 마당";
                return curPlaceName;
            case 3:
                curPlaceName = "장소: 마을";
                return curPlaceName;
            case 4:
                curPlaceName = "장소: 장터";
                return curPlaceName;
            case 5:
                curPlaceName = "장소: 개울";
                return curPlaceName;
            case 6:
                curPlaceName = "장소: 바다";
                return curPlaceName;
        }
        return "";
    }

    //구슬 퍼즐 시작
    public void PlayBeadPuzzle()
    {
        //시간정지
        TimeManager.instance.StopTime();

        //퍼즐 시작
        isBeadPuzzleStart = true;

        //커서 불빛 끄기
        uiManagerScr.BlindCursorLight();

        //로딩 이미지 보여주기
        StartCoroutine(ShowLoding());

        //카메라 위치 변경
        cameraMoveScr.CameraTransfer(transform_BeadPuzzleMap.position);

        //게임 UI 숨기기
        gameObject_gameUI.SetActive(false);

        //시간 UI 숨기기
        gameObjcet_timeUI.SetActive(false);
    }

    //구슬 퍼즐 끝
    public void BeadPuzzleEnd()
    {
        //시간 흐르기
        TimeManager.instance.ContinueTime();

        //퍼즐 시작
        isBeadPuzzleStart = false;

        //커서 불빛 켜기
        uiManagerScr.ShowCursorLight();

        //로딩 이미지 보여주기
        StartCoroutine(ShowLoding());

        //게임 UI 보이기
        gameObject_gameUI.SetActive(true);

        //시간 UI 보이기
        gameObjcet_timeUI.SetActive(true);

        //다이얼로그 보여주기
        gameObjcet_dialogueNPC.SetActive(true);
    }

    //주막 퍼즐 시작
    public void JoomackPuzzleStart()
    {
        //시간 정지
        TimeManager.instance.StopTime();

        isJoomackPuzzleStart = true;

        //다이얼로그 끄기
        gameObjcet_dialogueNPC.SetActive(false);

        //게임 UI 숨기기
        gameObject_gameUI.SetActive(false);

        //시간 UI 숨기기
        gameObjcet_timeUI.SetActive(false);

        //로딩 이미지 보여주기
        StartCoroutine(ShowLoding());

        //커서 보여주기
        uiManagerScr.ShowCursor();

        //커서 불빛 끄기
        uiManagerScr.BlindCursorLight();

        //주막 퍼즐 UI 보이기
        joomackScr.ShowJoomackUI();

        //카메라 위치 변경
        cameraMoveScr.CameraTransfer(transform_JoomackMap.position);
    }

    //주막 퍼즐 끝
    public void JoomackPuzzleClear()
    {
        //시간 흐르기
        TimeManager.instance.ContinueTime();

        isJoomackPuzzleStart = false;

        //게임 UI 보이기
        gameObject_gameUI.SetActive(true);

        //시간 UI 보이기
        gameObjcet_timeUI.SetActive(true);

        //로딩 이미지 보여주기
        StartCoroutine(ShowLoding());

        ////커서 보여주기
        //uiManagerScr.ShowCursor();

        //커서 불빛 켜기
        uiManagerScr.ShowCursorLight();

        //주막 퍼즐 UI 끄기
        joomackScr.JoomackUIClose();

        //다이얼로그 보이기
        dialogGuiduckScr.DialogueCanvas.SetActive(true);

        //주막 퍼즐 클리어
        EventManager.instance.eventProgress.joomackPuzzle_Clear = true;

        //대화 실행
        dialogGuiduckScr.StartDialogSentence();
    }

    //새끼줄 퍼즐 시작
    public void StrawPuzzleStart()
    {
        //시간 정지
        TimeManager.instance.StopTime();

        //새끼줄 퍼즐 falg on
        isStrawPuzzleStart = true;

        //다이얼로그 끄기
        gameObjcet_dialogueNPC.SetActive(false);

        //게임 UI 숨기기
        gameObject_gameUI.SetActive(false);

        //시간 UI 숨기기
        gameObjcet_timeUI.SetActive(false);

        //로딩 이미지 보여주기
        StartCoroutine(ShowLoding());

        //커서 보여주기
        uiManagerScr.ShowCursor();

        //커서 불빛 끄기
        uiManagerScr.BlindCursorLight();

        //새끼줄 퍼즐 UI 보이기
        strawPuzzleScr.gameObject_StrawUI.SetActive(true);

        //카메라 위치 변경
        cameraMoveScr.CameraTransfer(new Vector2(transform_StrawMap.position.x, 0.7f));
    }

    //새끼줄 퍼즐 끝
    public void StrawPuzzleClear()
    {
        //볏짚 아이템 제거
        ObjectManager.instance.RemoveItem(1012);

        //시간 흐르기
        TimeManager.instance.ContinueTime();

        isStrawPuzzleStart = false;

        //게임 UI 보이기
        gameObject_gameUI.SetActive(true);

        //시간 UI 보이기
        gameObjcet_timeUI.SetActive(true);

        //로딩 이미지 보여주기
        StartCoroutine(ShowLoding());

        //커서 불빛 켜기
        uiManagerScr.ShowCursorLight();

        //새끼줄 퍼즐 UI 끄기
        strawPuzzleScr.gameObject_StrawUI.SetActive(false);

        //다이얼로그 실행
        StartCoroutine(dialogSimbongScr.StrawPuzzleEnd_Sentence());
    }

    //로딩 이미지 보여주기
    IEnumerator ShowLoding()
    {
        //로딩 이미지 보여주기
        gameObjcet_Loading.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        //로딩 이미지 끄기
        gameObjcet_Loading.SetActive(false);
    }

    //PinPosNum값 변경
    public void ChangePinPosNum(int _pinPosNum)
    {
        int_PinPosNum = _pinPosNum;

        //시간 흐르기
        TimeManager.instance.ContinueTime();
    }

    //화면 이미지 어둡게 변경
    public void StartBilnd()
    {
        gameObjcet_Loading.SetActive(true);
    }

    //화면 이미지 밝게 변경
    public void StartBright()
    {
        gameObjcet_Loading.SetActive(false);
    }

    //플레이어 위치 변경
    public void TransferPlayer(Vector3 _pos, int _mapNum)
    {
        //Player의 위치값을 목적 설정 값으로 변경
        gameObject_Player.transform.position = _pos;

        //카메라의 제한 구역을 맵 번호로 변경
        Camera.main.GetComponent<CameraMove>().ChangeLimit(_mapNum);
    }
}
