using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWater : MonoBehaviour
{
    //콜라이더와 플레이어가 접촉했는지
    public bool isTouch;

    private void Update()
    {
        //바가지 장착후 Z키를 눌렀을 경우
        if(isTouch && ObjectManager.instance.GetEquipObjectKey() == 1003 && Input.GetKeyDown(KeyCode.Z))
        {
            //코루틴 실행
            StartCoroutine(DrawWaterStart());
        }
    }

    private IEnumerator DrawWaterStart()
    {
        //물이든 바가지 획득
        ObjectManager.instance.GetItem(1004);

        //바가지 아이템 제거
        ObjectManager.instance.RemoveItem(1003);

        //시스템 메세지 출력
        DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(521), true);

        yield return null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }
}
