using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    //외부스크립트 참조
    public TutorialManager tutorialManagerScr;

    int direction; // direction
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    //외부 스크립트에서 사용하기 위한 용도(싱글톤패턴)
    public static Controller instance;

    //벽이 감지되었음 WallDetect에서 관리
    public bool detectWall;

    //대화중인지 감지하는 Flag 외부 스크립트에서 관리
    public bool isTalk;

    //Player가 움직일 수 있는 상태인지 확인하는 falg(Animation 제어)
    public bool canMove;

    //다이얼로그 오브젝트가 켜져있는지 확인하는 flag
    public bool dialogueOn;

    //UI창이 켜져있는지 확인하는 flag
    public bool moveStop;

    //플레이어 다이얼로그
    public GameObject gameObject_PlayerDialogue;

    //NPC 다이얼로그
    public GameObject gameObject_NpcDialogue;

    // ray
    GameObject scanObject;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        if(Controller.instance == null)
		{
            Controller.instance = this;
		}
    }

    // Update is called once per frame
    private void Update()
    {
        //다이얼로그가 켜져있는지 감지
        if (gameObject_NpcDialogue.activeSelf || gameObject_PlayerDialogue.activeSelf)
        {
            dialogueOn = true;
        }
        else
        {
            dialogueOn = false;
        }
       

        //scan object
        if (Input.GetButtonDown("Jump") && scanObject != null) // space
        {
            Debug.Log("오 스페이스 누름! This is :" + scanObject.name);
        }
    }

    public float detect_range = 1.5f;
    public float moveSpeed = 5.0f;

    private void FixedUpdate() // move
    {
        //Direction Sprite
        if (Input.GetButton("Horizontal") )
        {
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                //spriteRenderer.flipX = true;
                direction = -1;
            }
            else
            {
                //spriteRenderer.flipX = false;
                direction = 1;

            }
        }

        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");

        //Player의 이동조건 
        if (!detectWall && !isTalk && !GameManager.instance.isBeadPuzzleStart && !dialogueOn && !moveStop)
        {
            ////플리에어 이동로직
            //transform.position += new Vector3(h, 0, v) * moveSpeed * Time.deltaTime;

            //플리에어 이동로직
            transform.position += new Vector3(h, 0, 0) * moveSpeed * Time.deltaTime;
        }

        //Debug용 Ray 그리기
        Debug.DrawRay(GetComponent<Rigidbody2D>().position, new Vector3(direction * detect_range, 0, 0), new Color(1, 0, 0));

        //RaycasDetect
        RaycastHit2D rayHit_detect = Physics2D.Raycast(GetComponent<Rigidbody2D>().position, new Vector3(direction, 0, 0), detect_range, LayerMask.GetMask("obj_NPC"));

        if (rayHit_detect.collider != null)
        {
            scanObject = rayHit_detect.collider.gameObject;
            Debug.Log(scanObject.name);
        }
        else
        {
            scanObject = null;
        }
    }

    void OnCollisionEnter(Collision npc_collider)
    {
        if (npc_collider.gameObject.name == "NPC")
            Debug.Log("Touch!");
    }

    //대화 시작
    public void TalkStart()
    {
        //시간 정지
        TimeManager.instance.StopTime();

        Debug.Log("TalkStart");
        isTalk = true;
        canMove = true;
    }

    //대화 끝
    public void TalkEnd()
    {
        CursorCtrl.instance.OnCursorLight();

        if (tutorialManagerScr.events == TutorialEvents.PassOneDay || tutorialManagerScr.events == TutorialEvents.Done)
        {
            //시간 흐르기
            TimeManager.instance.ContinueTime();
        }
        Debug.Log("TalkEnd");
        isTalk = false;
        canMove = false;
    }

    //Player 이동 제한
    public void PlayerMoveStop()
    {
        moveStop = true;
    }

    //Player 이동 제한 해제
    public void PlayerMoveStart()
    {
        moveStop = false;
    }
}