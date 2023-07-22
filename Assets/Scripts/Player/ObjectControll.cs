using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControll : MonoBehaviour
{
    //오브젝트 매니저 스크립트
    public ObjectManager objectManagerScr;

    //오브젝트 데이터 스크립트
    [SerializeField]
    private Objdata objdataScr;

    //Player가 오브젝트와 접촉중인지 확인하는 flag
    [SerializeField]
    private bool isTriggerObject;

    //Player가 획득할 오브젝트
    [SerializeField]
    private GameObject gameobject_TargetObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //오브젝트 접촉후 Z키를 눌렀을 경우
        if(Input.GetKeyDown(KeyCode.Z) && isTriggerObject) 
        {
            Debug.Log("실행");
            Debug.Log(objdataScr.key);

            //아이템 오브젝트 추가
            if(gameobject_TargetObject.CompareTag("Item"))
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
        }

    }

    //오브젝트 BoxCollider와 접촉시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //아이템일 경우
        if(collision.CompareTag("Item"))
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
    }

    //오브젝트 BoxCollider와 접촉중일경우
    private void OnTriggerStay2D(Collider2D collision)
    { 
        //아이템일 경우
        if(collision.CompareTag("Item"))
        {
            isTriggerObject = true;
        }

        //단서일 경우
        else if (collision.CompareTag("Clue"))
        {
            //접촉한 오브젝트
            gameobject_TargetObject = collision.gameObject;

            isTriggerObject = true;

            //아이템 내부의 Objdata를 가져옴
            objdataScr = gameobject_TargetObject.GetComponent<Objdata>();
        }
    }

    //오브젝트 boxCollider와 떨어졌을 경우
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggerObject = false;
    }

    
}
