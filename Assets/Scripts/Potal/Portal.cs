using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //이동시킬 Player 오브젝트
    public GameObject gameObject_Player;

    //이동할 목적지(이동할 맵의 포탈)
    public Transform transform_Destination;

    //Player가 포탈에 도착했는지 확인하는 flag
    [SerializeField] private bool isPlayerArrivePotal;

    //맵 번호 설정
    public int int_MapNum;

    private void Update()
    {
        //포탈 앞에서 W키 혹은 위 방향키를 눌렀을 경우
        if(isPlayerArrivePotal && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            //목적지 이동
            MoveToDestination();
        }
    }

    //Player가 포탈의 BoxCollider와 맞닿았을 경우
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("도착");
        //Collsion의 Tag명이 Player일 경우
        if(collision.CompareTag("Player"))
        {
            //포탈에 도착함
            isPlayerArrivePotal = true;
        }
    }

    //Player가 포탈의 BoxCollider에서 벗어났을경우
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Collsion의 Tage명이 Player일 경우
        if(collision.CompareTag("Player"))
        {
            //포탈에서 벗어남
            isPlayerArrivePotal = false;
        }
    }

    //목적지로 이동
    public void MoveToDestination()
    {
        //Player의 위치값을 목적지의 위치값으로 변경
        gameObject_Player.transform.position = transform_Destination.position;
        //카메라의 제한 구역을 맵 번호로 변경
        Camera.main.GetComponent<CameraMove>().ChangeLimit(int_MapNum);
    }
}
