using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//������ ������ Ŭ����
[System.Serializable]
public class GameSaveData
{
    //������
    public GameSaveData(Vector3 _playerPos, int _LimitCamera, int _mapPinNum, int _joomuckBabState, bool _getJangjack, 
        bool _getBagage, bool _getRice, bool _boatMan2_Show)
    {
        playerPos = _playerPos; limitCamera = _LimitCamera; mapPinNum = _mapPinNum;
        joomuckBabState = _joomuckBabState;
        getJangjack = _getJangjack;
        getBagage = _getBagage;
        getRice = _getRice;
        boatman2_Show = _boatMan2_Show;
    }

    //�÷��̾� ��ġ��
    public Vector3 playerPos;

    //ī�޶� ���� ����
    public int limitCamera;

    //������ �� ��ġ��
    public int mapPinNum;

    //�ָԹ� �̺�Ʈ �����Ȳ
    public int joomuckBabState;

    #region ������ ȹ�� ����
    //���� ȹ�� ����
    public bool getJangjack;

    //�ٰ��� ȹ�� ����
    public bool getBagage;

    //�� ȹ�� ����
    public bool getRice;

    #endregion

    //����2 �̺�Ʈ ���࿩��
    public bool boatman2_Show;
}


//�ε��� ������ Ŭ����
[System.Serializable]
public class GameLoadData
{
    //������
    public GameLoadData(Vector3 _playerPos, int _LimitCamera, int _mapPinNum, int _joomuckBabState, bool _getJangjack, bool _getBagage, bool _getRice
        , bool _boatMan2_Show)
    {
        playerPos = _playerPos; limitCamera = _LimitCamera; mapPinNum = _mapPinNum;
        joomuckBabState = _joomuckBabState;
        getJangjack = _getJangjack;
        getBagage = _getBagage;
        getRice = _getRice;
        boatman2_Show = _boatMan2_Show;
    }

    //�÷��̾� ��ġ��
    public Vector3 playerPos;

    //ī�޶� ���� ����
    public int limitCamera;

    //������ �� ��ġ��
    public int mapPinNum;

    //�ָԹ� �̺�Ʈ �����Ȳ
    public int joomuckBabState;

    #region ������ ȹ�� ����
    //���� ȹ�� ����
    public bool getJangjack;

    //�ٰ��� ȹ�� ����
    public bool getBagage;

    //�� ȹ�� ����
    public bool getRice;
    #endregion

    //����2 �̺�Ʈ ���࿩��
    public bool boatman2_Show;

}


public class GameManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ
    public CameraMove cameraMoveScr;
    public UIManager uiManagerScr;
    public JoomackPuzzle joomackScr;
    public Dialog_TypingWriter_Guiduck dialogGuiduckScr;
    public Dialog_TypingWriter_BoatMan dialogBoatManScr;
    public JoomuckBab joomuckBabScr;

    // �ϳ��� �߰�����
    public bool bool_isAction;
    public GameObject scanObject;
    public Text dialogText;
    public TalkManager talkManager;

    public int talkIndex;
    public GameObject talkPanel;

    //�ε� �̹��� ������Ʈ
    public GameObject gameObjcet_Loading;

    //Player ������Ʈ
    public GameObject gameObjcet_Player;
    //Player ReturnPos
    public Transform transform_PlayerReturn;

    //������ ������ Ŭ����
    public GameSaveData curGameSaveData;

    //�ҷ��� ������ Ŭ����
    public GameLoadData curGameLoadData;

    //������ ���� ��ġ
    private string saveFilePath;

    //���� �÷��̰� ��ġ�� ��� �̸�
    private string curPlaceName;

    #region ����

    //�÷��̾ ���� ���������� Ȯ���ϴ� flag
    public bool isBeadPuzzleStart;

    //�÷��̾ �ָ� ���������� Ȯ���ϴ� flag
    public bool isJoomackPuzzleStart;

    //Bead Puzzle Map Transform
    public Transform transform_BeadPuzzleMap;

    //Joomack Puzzle Map Transform
    public Transform transform_JoomackMap;

    //���� ����ġ ��
    public int int_PinPosNum;

    //�̱���
    public static GameManager instance = null;

    //NPC ���̾�α� �̹���
    public GameObject gameObjcet_dialogueNPC;

    //�ΰ��� UI 
    public GameObject gameObject_gameUI;

    //�ð� UI
    public GameObject gameObjcet_timeUI;

    #endregion

    //���� ȹ�� ����
    public bool getJangjack;

    //�ٰ��� ȹ�� ����
    public bool getBagage;

    //�� ȹ�� ����
    public bool getRice;

    //���� ������Ʈ
    public GameObject gameObject_Jangjack;

    //�ٰ��� ������Ʈ
    public GameObject gameObject_Bagage;

    //�� ������Ʈ
    public GameObject gameObject_Rice;

    //����2 ������Ʈ
    public GameObject gameObject_BoatMan2;

    //���� ���� �÷��̾ �������� ����������
    public bool isPlayerSelecting;

    //�÷��̾� ���� ������Ʈ
    public GameObject gameObject_Player;

    //Player �ٴ� SponPos
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

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("���� ���� ����");
            PlayBeadPuzzle();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("�ָ� ���� ����");
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

    void Dialog(int id, bool bool_isNPC)
    {

    }

    //�÷��̾� ��ȯ
    public void ReturnPlayer()
    {
        //�÷��̾� ������ �� ����
        gameObjcet_Player.transform.position = new Vector3(transform_PlayerReturn.position.x, transform_PlayerReturn.position.y, 0);

        //ī�޶� �������� �� ����
        cameraMoveScr.ChangeLimit(0);
    }

    //Save Data
    public void Save(int _slotNum)
    {
        Debug.Log("Save GameManagerData");

        //������ ����� �̸� ���ϱ�
        GetPlaceName();

        //������ ������ �ֱ�
        curGameSaveData = new GameSaveData(new Vector3(gameObjcet_Player.transform.position.x, gameObjcet_Player.transform.position.y,
                          gameObjcet_Player.transform.position.z), cameraMoveScr.int_CurLimitNum, int_PinPosNum, joomuckBabScr.GetEventState()
                          , getJangjack, getBagage, getRice, dialogBoatManScr.boatMan2_Show);

        //���̺� ������
        string jSaveData = JsonUtility.ToJson(curGameSaveData);

        //������ ���� ����
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);
    }

    //������ �ε�
    public void Load(int _slotNum)
    {
        Debug.Log("Load GameManagerData");

        if(_slotNum <= 2)
        {
            //���̺� ���� �о����
            string jLoadData = File.ReadAllText(saveFilePath + _slotNum.ToString());

            //�о�� ���� ����Ʈ�� ����
            curGameLoadData = JsonUtility.FromJson<GameLoadData>(jLoadData);

            //�÷��̾� ��ġ �缳��
            gameObjcet_Player.transform.position = curGameLoadData.playerPos;

            //ī�޶� ���ѱ��� �缳��
            cameraMoveScr.ChangeLimit(curGameLoadData.limitCamera);

            Debug.Log($"ī�޶� ���ѱ��� : {curGameLoadData.limitCamera}");

            //���� �� ��ġ �缳��
            int_PinPosNum = curGameLoadData.mapPinNum;
            //uiManagerScr.pinActionScr.PinPosChange(curGameLoadData.mapPinNum);

            //������ ����
            joomuckBabScr.EventSetting(curGameLoadData.joomuckBabState);

            //������ ����

            //��
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


            //����
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

            //�ٰ���
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

            //����2 ���̱�
            if(curGameLoadData.boatman2_Show)
            {
                //����2 ���̱�
                gameObject_BoatMan2.SetActive(true);

                //�÷��� �ʱ�ȭ
                dialogBoatManScr.boatMan2_Show = true;
            }
        }
        
    }

    //���� ��� �̸� ���ϴ� �޼���
    public string GetPlaceName()
    {
        if (cameraMoveScr.int_CurLimitNum == 0)
        {
            curPlaceName = "���: �ȹ�";
            return curPlaceName;
        }

        else if (cameraMoveScr.int_CurLimitNum == 1)
        {
            curPlaceName = "���: �ξ�";
            return curPlaceName;
        }

        else if (cameraMoveScr.int_CurLimitNum == 2)
        {
            curPlaceName = "���: ����";
            return curPlaceName;
        }

        else if (cameraMoveScr.int_CurLimitNum == 3)
        {
            curPlaceName = "���: ����";
            return curPlaceName;
        }

        else if (cameraMoveScr.int_CurLimitNum == 4)
        {
            curPlaceName = "���: ����";
            return curPlaceName;
        }

        else if (cameraMoveScr.int_CurLimitNum == 5)
        {
            curPlaceName = "���: ����";
            return curPlaceName;
        }

        else if (cameraMoveScr.int_CurLimitNum == 6)
        {
            curPlaceName = "���: �ٴ�";
            return curPlaceName;
        }

        else
        {
            return null;
        }
    }

    //���� ���� ����
    public void PlayBeadPuzzle()
    {
        //�ð�����
        TimeManager.instance.StopTime();

        //���� ����
        isBeadPuzzleStart = true;

        //Ŀ�� �Һ� ����
        uiManagerScr.BlindCursorLight();

        //�ε� �̹��� �����ֱ�
        StartCoroutine(ShowLoding());

        //ī�޶� ��ġ ����
        cameraMoveScr.CameraTransfer(transform_BeadPuzzleMap.position);

        //���� UI �����
        gameObject_gameUI.SetActive(false);

        //�ð� UI �����
        gameObjcet_timeUI.SetActive(false);
    }

    //���� ���� ��
    public void BeadPuzzleEnd()
    {
        //�ð� �帣��
        TimeManager.instance.ContinueTime();

        //���� ����
        isBeadPuzzleStart = false;

        //Ŀ�� �Һ� �ѱ�
        uiManagerScr.ShowCursorLight();

        //�ε� �̹��� �����ֱ�
        StartCoroutine(ShowLoding());

        //���� UI ���̱�
        gameObject_gameUI.SetActive(true);

        //�ð� UI ���̱�
        gameObjcet_timeUI.SetActive(true);

        //���̾�α� �����ֱ�
        gameObjcet_dialogueNPC.SetActive(true);
    }

    //�ָ� ���� ����
    public void JoomackPuzzleStart()
    {
        //�ð� ����
        TimeManager.instance.StopTime();

        isJoomackPuzzleStart = true;

        //���̾�α� ����
        gameObjcet_dialogueNPC.SetActive(false);

        //���� UI �����
        gameObject_gameUI.SetActive(false);

        //�ð� UI �����
        gameObjcet_timeUI.SetActive(false);

        //�ε� �̹��� �����ֱ�
        StartCoroutine(ShowLoding());

        //Ŀ�� �����ֱ�
        uiManagerScr.ShowCursor();

        //Ŀ�� �Һ� ����
        uiManagerScr.BlindCursorLight();

        //�ָ� ���� UI ���̱�
        joomackScr.ShowJoomackUI();

        //ī�޶� ��ġ ����
        cameraMoveScr.CameraTransfer(transform_JoomackMap.position);
    }

    //�ָ� ���� ��
    public void JoomackPuzzleClear()
    {
        //�ð� �帣��
        TimeManager.instance.ContinueTime();

        isJoomackPuzzleStart = false;

        //���� UI ���̱�
        gameObject_gameUI.SetActive(true);

        //�ð� UI ���̱�
        gameObjcet_timeUI.SetActive(true);

        //�ε� �̹��� �����ֱ�
        StartCoroutine(ShowLoding());

        ////Ŀ�� �����ֱ�
        //uiManagerScr.ShowCursor();

        //Ŀ�� �Һ� �ѱ�
        uiManagerScr.ShowCursorLight();

        //�ָ� ���� UI ����
        joomackScr.JoomackUIClose();

        //���̾�α� ���̱�
        dialogGuiduckScr.images_NPC.SetActive(true);

        //�ָ� ���� Ŭ����
        EventManager.instance.eventProgress.joomackPuzzle_Clear = true;

        //��ȭ ����
        dialogGuiduckScr.StartDialogSentence();
    }

    //�ε� �̹��� �����ֱ�
    IEnumerator ShowLoding()
    {
        //�ε� �̹��� �����ֱ�
        gameObjcet_Loading.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        //�ε� �̹��� ����
        gameObjcet_Loading.SetActive(false);
    }

    //PinPosNum�� ����
    public void ChangePinPosNum(int _pinPosNum)
    {
        int_PinPosNum = _pinPosNum;

        //�ð� �帣��
        TimeManager.instance.ContinueTime();
    }

    //ȭ�� �̹��� ��Ӱ� ����
    public void StartBilnd()
    {
        gameObjcet_Loading.SetActive(true);
    }

    //ȭ�� �̹��� ��� ����
    public void StartBright()
    {
        gameObjcet_Loading.SetActive(false);
    }

    //�÷��̾� ��ġ ����
    public void TransferPlayer(Vector3 _pos, int _mapNum)
    {
        //Player�� ��ġ���� ���� ���� ������ ����
        gameObject_Player.transform.position = _pos;

        //ī�޶��� ���� ������ �� ��ȣ�� ����
        Camera.main.GetComponent<CameraMove>().ChangeLimit(_mapNum);
    }
}
