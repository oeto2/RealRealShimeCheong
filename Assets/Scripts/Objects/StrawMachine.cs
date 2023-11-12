using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawMachine : MonoBehaviour
{
    //베틀과 플레이어가 닿았는지
    public bool isTouch = false;

    private void Update()
    {
        //플레이어가 볏짚을 소지한 상태로 Z키를 눌렀을 경우 + 새끼줄을 가지고 있지 않아야함
        if(isTouch && ObjectManager.instance.GetItem_Check(1012) && !ObjectManager.instance.GetItem_Check(1013) && 
            !ObjectManager.instance.GetItem_Check(1014) && Input.GetKeyDown(KeyCode.Z))
        {
            //새끼줄 퍼즐 시작
            GameManager.instance.StrawPuzzleStart();
        }
    }

    //플레이어와 접촉했을 경우
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    //플레이어와 접촉중일 경우
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    //플레이어가 벗어났을 경우
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }
}
