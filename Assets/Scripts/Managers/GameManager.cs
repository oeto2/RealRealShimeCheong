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
    public GameSaveData(Vector3 _playerPos, int _LimitCamera, int _mapPinNum)
    {
        playerPos = _playerPos; limitCamera = _LimitCamera; mapPinNum = _mapPinNum;
    }

    //�÷��̾� ��ġ��
    public Vector3 playerPos;

    //ī�޶� ���� ����
    public int limitCamera;

    //������ �� ��ġ��
    public int mapPinNum;
}


//�ε��� ������ Ŭ����
[System.Serializable]
public class GameLoadData
{
    //������
    public GameLoadData(Vector3 _playerPos, int _LimitCamera, int _mapPinNum)
    {
        playerPos = _playerPos; limitCamera = _LimitCamera; mapPinNum = _mapPinNum;
    }

    //�÷��̾� ��ġ��
    public Vector3 playerPos;

    //ī�޶� ���� ����
    public int limitCamera;

    //������ �� ��ġ��
    public int mapPinNum;
}


public class GameManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ
    public CameraMove cameraMoveScr;
    public UIManager uiManagerScr;
    public JoomackPuzzle joomackScr;
    public Dialog_TypingWriter_Guiduck dialogGuiduckScr;

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

    //�÷��̾� ��ȯ
    public void ReturnPlayer()
    {
        //�÷��̾� ������ �� ����
        gameObjcet_Player.transform.position = new Vector3(transform_PlayerReturn.position.x,transform_PlayerReturn.position.y,0);

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
        curGameSaveData = new GameSaveData(new Vector3(gameObjcet_Player.transform.position.x,gameObjcet_Player.transform.position.y,
                          gameObjcet_Player.transform.position.z), cameraMoveScr.int_CurLimitNum, int_PinPosNum);

        //���̺� ������
        string jSaveData = JsonUtility.ToJson(curGameSaveData);

        //������ ���� ����
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);
    }

    //������ �ε�
    public void Load(int _slotNum)
    {
        Debug.Log("Load GameManagerData");

        //���̺� ���� �о����
        string jLoadData = File.ReadAllText(saveFilePath + _slotNum.ToString());

        //�о�� ���� ����Ʈ�� ����
        curGameLoadData = JsonUtility.FromJson<GameLoadData>(jLoadData);

        //�÷��̾� ��ġ �缳��
        gameObjcet_Player.transform.position = curGameLoadData.playerPos;

        //ī�޶� ���ѱ��� �缳��
        cameraMoveScr.ChangeLimit(curGameLoadData.limitCamera);

        //���� �� ��ġ �缳��
        int_PinPosNum = curGameLoadData.mapPinNum;
        //uiManagerScr.pinActionScr.PinPosChange(curGameLoadData.mapPinNum);
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

        else
        {
            return null;
        }
    }

    //���� ���� ����
    public void PlayBeadPuzzle()
    {
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
    }
}
