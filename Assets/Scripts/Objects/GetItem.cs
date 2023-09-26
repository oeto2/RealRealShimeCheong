using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    //외부 스크립트 참조
    public ObjectManager objectManagerScr;

    //아이템 키값
    public int key;

    //아이템과 닿았는지
    public bool isTouch;

    //코루틴이 실행중인지
    public bool isGetStart;

    private void Update()
    {
        //아이템은 봇짐을 얻은 이후에만 획득할 수 있음
        if(ObjectControll.instance.getBotzime)
        {
            //오브젝트와 접촉후 Z키를 눌러 해당 Key값에 아이템을 획득할 수 있다.
            if (Input.GetKeyDown(KeyCode.Z) && isTouch && !isGetStart)
            {
                StartCoroutine(GetItemStart(key));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }

    IEnumerator GetItemStart(int _key)
    {
        isGetStart = true;
        objectManagerScr.GetItem(key);

        //획득한 아이템이 장작일경우
        if(_key == 1001)
        {
            GameManager.instance.getJangjack = true;
        }

        else if(_key == 1000)
        {
            GameManager.instance.getRice = true;
        }

        else if(_key == 1003)
        {
            GameManager.instance.getBagage = true;
        }

        yield return new WaitForSeconds(0.1f);

        Destroy(this.gameObject);
    }
}
