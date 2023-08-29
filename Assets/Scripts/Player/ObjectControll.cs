using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControll : MonoBehaviour
{
    //외부 스크립트 참조
    public ObjectManager objectManagerScr;
    public Dialog_TypingWriter_ShimBongSa playerDialogueScr;
    public TutorialManager tutorialManagerScr;
    public Controller playerCtrlScr;

    //오브젝트 데이터 스크립트
    [SerializeField]
    private Objdata objdataScr;

    //Player가 오브젝트와 접촉중인지 확인하는 flag
    [SerializeField]
    private bool isTriggerObject;

    //Player가 획득할 오브젝트
    [SerializeField]
    private GameObject gameobject_TargetObject;

    //UI 봇짐 이미지
    public GameObject gameObjcet_BotzimeImage;

    //UI 지도 이미지
    public GameObject gameObject_MapImage;

    //봇짐을 획득 했는지
    public bool getBotzime;

    //맵을 획득 했는지
    public bool getMap;

    //봇짐 오브젝트
    public GameObject botzime;

    //지도 오브젝트
    public GameObject map;

    //싱글톤
    public static ObjectControll instance = null;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        //오브젝트 접촉후 Z키를 눌렀을 경우
        if (Input.GetKeyDown(KeyCode.Z) && isTriggerObject && tutorialManagerScr.setence1End)
        {
            //아이템 오브젝트 추가
            if (gameobject_TargetObject.CompareTag("Item"))
            {
                objectManagerScr.GetItem(objdataScr.key);
                //오브젝트 SetActive false
                gameobject_TargetObject.SetActive(false);
            }

            //단서 아이템 획득
            else if (gameobject_TargetObject.CompareTag("Clue"))
            {
                objectManagerScr.GetClue(objdataScr.key);
                //오브젝트 SetActive false
                gameobject_TargetObject.SetActive(false);
            }

            //봇짐일 경우
            else if (gameobject_TargetObject.CompareTag("Object") && gameobject_TargetObject.name == "Botzime")
            {
                //봇짐 이미지 보여주기
                gameObjcet_BotzimeImage.SetActive(true);
                GetBotzime(gameobject_TargetObject);
            }

            //맵일 경우
            else if (gameobject_TargetObject.CompareTag("Object") && gameobject_TargetObject.name == "Map")
            {
                //지도 이미지 보여주기
                gameObject_MapImage.SetActive(true);
                GetMap(gameobject_TargetObject);
            }
        }
    }

    //오브젝트 BoxCollider와 접촉시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string objName = collision.name;

        // 아이템일 경우
        if (collision.CompareTag("Item"))
        {
            //접촉한 오브젝트
            gameobject_TargetObject = collision.gameObject;

            isTriggerObject = true;

            //아이템 내부의 Objdata를 가져옴
            objdataScr = gameobject_TargetObject.GetComponent<Objdata>();
        }

        //단서일 경우
        else if(collision.CompareTag("Clue"))
        {
            //접촉한 오브젝트
            gameobject_TargetObject = collision.gameObject;

            isTriggerObject = true;

            //아이템 내부의 Objdata를 가져옴
            objdataScr = gameobject_TargetObject.GetComponent<Objdata>();
        }

        //봇짐과 닿았을 경우
        else if(collision.CompareTag("Object") && objName == "Botzime")
        {
            gameobject_TargetObject = collision.gameObject;

            isTriggerObject = true;
        }

        //맵이랑 닿았을 경우
        else if (collision.CompareTag("Object") && objName == "Map")
        {
            gameobject_TargetObject = collision.gameObject;

            isTriggerObject = true;
        }
    }

    //오브젝트 BoxCollider와 접촉중일경우
    private void OnTriggerStay2D(Collider2D collision)
    {
        string objName = collision.name;


        //봇짐일 경우
        if (collision.CompareTag("Item"))
        {
            isTriggerObject = true;
        }

        //단서일 경우
        else if (collision.CompareTag("Clue"))
        {
            //접촉한 오브젝트
            gameobject_TargetObject = collision.gameObject;

            isTriggerObject = true;
        }

        //봇짐과 닿았을 경우
        else if (collision.CompareTag("Object") && objName == "Botzime")
        {
            isTriggerObject = true;
        }

        //맵이랑 닿았을 경우
        else if (collision.CompareTag("Object") && objName == "Map")
        {
            isTriggerObject = true;
        }
    }

    //오브젝트 boxCollider와 떨어졌을 경우
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggerObject = false;
    }

    //봇짐 획득
    public void GetBotzime(GameObject _obj)
    {
        getBotzime = true;

        //봇짐 획득 대화 실행
        playerDialogueScr.Start_Sentence_GetBotzime();

        //플레이어 이동 제한
        playerCtrlScr.TalkStart();

        //오브젝트 비활성화
        _obj.SetActive(false);
    }

    //지도 획득
    #region
    public void GetMap(GameObject _obj)
    {
        getMap = true;

        //지도 획득 대화 실행
        playerDialogueScr.Start_Sentence_GetMap();

        //플레이어 이동제한
        playerCtrlScr.TalkStart();

        //오브젝트 비활성화
        _obj.SetActive(false);
    }
    public void GetMap()
    {
        getMap = true;

        //지도 획득 대화 실행
        playerDialogueScr.Start_Sentence_GetMap();

        //플레이어 이동제한
        playerCtrlScr.TalkStart();
    }
    #endregion

    //봇짐 리셋
    public void ResetBotzime()
    {
        getBotzime = false;
        botzime.SetActive(true);
    }

    //지도 리셋
    public void ResetMap()
    {
        getMap = false;
        //map.SetActive(true);
    }

    //봇짐 획득 후 로드
    public void LoadBotzime()
    {
        getBotzime = true;
        botzime.SetActive(false);
    }

    //지도 획득 후 로드
    public void LoadMap()
    {
        getMap = true;
        //map.SetActive(false);
    }
}
