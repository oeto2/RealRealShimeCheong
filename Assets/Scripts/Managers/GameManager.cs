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
    public GameSaveData(Vector3 _playerPos, int _LimitCamera)
    {
        playerPos = _playerPos; limitCamera = _LimitCamera;
    }

    //플레이어 위치값
    public Vector3 playerPos;

    //카메라 제한 영역
    public int limitCamera;   
}


//로드할 데이터 클래스
[System.Serializable]
public class GameLoadData
{
    //생성자
    public GameLoadData(Vector3 _playerPos, int _LimitCamera)
    {
        playerPos = _playerPos; limitCamera = _LimitCamera;
    }

    //플레이어 위치값
    public Vector3 playerPos;

    //카메라 제한 영역
    public int limitCamera;
}


public class GameManager : MonoBehaviour
{
    //외부 스크립트
    public CameraMove cameraMoveScr;
    public UIManager uiManagerScr;

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

    //플레이어가 퍼즐중인지 확인하는 flag
    public bool isPuzzleStart;

    //Bead Puzzle Map Transform
    public Transform transform_BeadPuzzleMap;

    //맵의 핀위치 값
    public int int_PinPosNum;

    //싱글톤
    public static GameManager instance = null;

    #endregion

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/GameManagerDataText.txt";
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("구슬 퍼즐 시작");
            PlayBeadPuzzle();
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

        if(talkData == null)
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

    void Dialog(int id, bool bool_isNPC)
	{

    }

    //플레이어 귀환
    public void ReturnPlayer()
    {
        //플레이어 포지션 값 변경
        gameObjcet_Player.transform.position = new Vector3(transform_PlayerReturn.position.x,transform_PlayerReturn.position.y,0);

        //카메라 영역제한 값 변경
        cameraMoveScr.ChangeLimit(0);
    }

    //Save Data
    public void Save(int _slotNum)
    {
        Debug.Log("Save GameManagerData");

        //저장할 장소의 이름 구하기
        GetPlaceName();

        //저장할 데이터 넣기
        curGameSaveData = new GameSaveData(new Vector3(gameObjcet_Player.transform.position.x,gameObjcet_Player.transform.position.y,
                          gameObjcet_Player.transform.position.z), cameraMoveScr.int_CurLimitNum);

        //세이브 데이터
        string jSaveData = JsonUtility.ToJson(curGameSaveData);

        //데이터 파일 생성
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);
    }

    //데이터 로드
    public void Load(int _slotNum)
    {
        Debug.Log("Load GameManagerData");

        //세이브 파일 읽어오기
        string jLoadData = File.ReadAllText(saveFilePath + _slotNum.ToString());

        //읽어온 파일 리스트에 저장
        curGameLoadData = JsonUtility.FromJson<GameLoadData>(jLoadData);

        //플레이어 위치 재설정
        gameObjcet_Player.transform.position = curGameLoadData.playerPos;

        //카메라 제한구역 재설정
        cameraMoveScr.ChangeLimit(curGameLoadData.limitCamera);
    }

    //현재 장소 이름 구하는 메서드
    public string GetPlaceName()
    {
        if (cameraMoveScr.int_CurLimitNum == 0)
        {
            curPlaceName = "장소: 안방";
            return curPlaceName;
        }

        else if (cameraMoveScr.int_CurLimitNum == 1)
        {
            curPlaceName = "장소: 부엌";
            return curPlaceName;
        }

        else if (cameraMoveScr.int_CurLimitNum == 2)
        {
            curPlaceName = "장소: 마당";
            return curPlaceName;
        }
        else
        {
            return null;
        }
    }

    //구슬 퍼즐 시작
    public void PlayBeadPuzzle()
    {
        //퍼즐 시작
        isPuzzleStart = true;

        //로딩 이미지 보여주기
        StartCoroutine(ShowLoding());

        //카메라 위치 변경
        cameraMoveScr.CameraTransfer(transform_BeadPuzzleMap.position);
    }
    
    //로딩 이미지 보여주기
    IEnumerator ShowLoding()
    {
        //로딩 이미지 보여주기
        gameObjcet_Loading.SetActive(true);

        yield return new WaitForSeconds(1f);

        //로딩 이미지 끄기
        gameObjcet_Loading.SetActive(false);
    }

    //PinPosNum값 변경
    public void ChangePinPosNum(int _pinPosNum)
    {
        int_PinPosNum = _pinPosNum;
    }
}
