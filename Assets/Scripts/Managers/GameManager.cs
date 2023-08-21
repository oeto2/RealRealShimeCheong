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
    public GameSaveData(Vector3 _playerPos, int _LimitCamera)
    {
        playerPos = _playerPos; limitCamera = _LimitCamera;
    }

    //�÷��̾� ��ġ��
    public Vector3 playerPos;

    //ī�޶� ���� ����
    public int limitCamera;   
}


//�ε��� ������ Ŭ����
[System.Serializable]
public class GameLoadData
{
    //������
    public GameLoadData(Vector3 _playerPos, int _LimitCamera)
    {
        playerPos = _playerPos; limitCamera = _LimitCamera;
    }

    //�÷��̾� ��ġ��
    public Vector3 playerPos;

    //ī�޶� ���� ����
    public int limitCamera;
}


public class GameManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ
    public CameraMove cameraMoveScr;
    public UIManager uiManagerScr;

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

    //�÷��̾ ���������� Ȯ���ϴ� flag
    public bool isPuzzleStart;

    //Bead Puzzle Map Transform
    public Transform transform_BeadPuzzleMap;

    //���� ����ġ ��
    public int int_PinPosNum;

    //�̱���
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
            Debug.Log("���� ���� ����");
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
                          gameObjcet_Player.transform.position.z), cameraMoveScr.int_CurLimitNum);

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
        else
        {
            return null;
        }
    }

    //���� ���� ����
    public void PlayBeadPuzzle()
    {
        //���� ����
        isPuzzleStart = true;

        //�ε� �̹��� �����ֱ�
        StartCoroutine(ShowLoding());

        //ī�޶� ��ġ ����
        cameraMoveScr.CameraTransfer(transform_BeadPuzzleMap.position);
    }
    
    //�ε� �̹��� �����ֱ�
    IEnumerator ShowLoding()
    {
        //�ε� �̹��� �����ֱ�
        gameObjcet_Loading.SetActive(true);

        yield return new WaitForSeconds(1f);

        //�ε� �̹��� ����
        gameObjcet_Loading.SetActive(false);
    }

    //PinPosNum�� ����
    public void ChangePinPosNum(int _pinPosNum)
    {
        int_PinPosNum = _pinPosNum;
    }
}
