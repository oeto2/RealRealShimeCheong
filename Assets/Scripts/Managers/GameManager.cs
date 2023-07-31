using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //외부 스크립트
    public CameraMove cameraMoveScr;

    // 하나씩 추가하자
    public bool bool_isAction;
    public GameObject scanObject;
    public Text dialogText;
    public TalkManager talkManager;

    public int talkIndex;
    public GameObject talkPanel;


    //Player 오브젝트
    public GameObject gameObjcet_Player;
    //Player ReturnPos
    public Transform transform_PlayerReturn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
}
