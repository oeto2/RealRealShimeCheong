using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BeadDirection
{
    UP = 0,
    Down,
    Left,
    Right
}


public class BeadMove : MonoBehaviour
{
    //Bead RigidBody2D
    private Rigidbody2D rigid;

    //Bead Speed
    [Range(0, 100)] public int speed;

    //Bead RayCast
    private RaycastHit2D rayhit;

    //RayCast Length
    [Range(0, 10)] public float rayLength;

    //RayCast Space
    [Range(0, 10)] public float raySpace; 

    //구슬이 이동중인지 확인하는 Flag
    public bool isMove;

    //구슬의 이동방향
    public BeadDirection beadDir = BeadDirection.UP;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //비드 움직이기
        BeadMoveCtrl();

        //RayCast 방향설정
        RayCastDirection();
        
    }

    //구슬의 움직임 구현 메서드
    public void BeadMoveCtrl()
    {
        //구슬이 이동중인걸 알수있는 조건
        if(rigid.velocity != Vector2.zero)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }
        
        
        #region 방향키에 따른 구슬 이동 로직
        //위로 이동
        if (Input.GetKeyDown(KeyCode.W) && !isMove)
        {
            Debug.Log("구슬 위로 이동");
            rigid.velocity = Vector2.up * speed;

            //rigid.AddForce(new Vector2(0, 10 * power));

            //RayCast 방향 설정
            beadDir = BeadDirection.UP;
        }
        //아래로 이동
        else if (Input.GetKeyDown(KeyCode.S) && !isMove)
        {
            Debug.Log("구슬 아래로 이동");
            rigid.velocity = Vector2.down * speed;

            //rigid.AddForce(new Vector2(0, -10 * power));

            //RayCast 방향 설정
            beadDir = BeadDirection.Down;
        }
        //왼쪽으로 이동
        else if (Input.GetKeyDown(KeyCode.A) && !isMove)
        {
            Debug.Log("구슬 왼쪽으로 이동");
            rigid.velocity = Vector2.left * speed;


            //rigid.AddForce(new Vector2(-10 * power, 0));

            //RayCast 방향 설정
            beadDir = BeadDirection.Left;
        }
        //오른쪽 이동
        else if (Input.GetKeyDown(KeyCode.D) && !isMove)
        {
            Debug.Log("구슬 오른쪽로 이동");
            rigid.velocity = Vector2.right * speed;


            //rigid.AddForce(new Vector2(10 * power, 0));

            //RayCast 방향 설정
            beadDir = BeadDirection.Right;
        }
        #endregion

        //구슬의 이동정지 조건
        if (rayhit.collider != null)
        {
            //구슬 이동 정지
            BeadMoveStop();
            Debug.Log("구슬 이동 정지");
        }
    }

    //Raycast의 방향
    public void RayCastDirection()
    {
        switch(beadDir)
        {
            //구슬의 이동방향이 위쪽이라면
            case BeadDirection.UP:
                rayhit = Physics2D.Raycast(this.transform.position, new Vector2(0,1), rayLength, LayerMask.GetMask("Wall"));
                Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y + raySpace), new Vector2(0, rayLength), Color.red);
                break;

            //구슬의 이동방향이 아래쪽 이라면
            case BeadDirection.Down:
                rayhit = Physics2D.Raycast(this.transform.position, new Vector2(0, -1), rayLength, LayerMask.GetMask("Wall"));
                Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y - raySpace), new Vector2(0, -rayLength), Color.red);
                break;

            //구슬의 이동방향이 왼쪽이라면
            case BeadDirection.Left:
                rayhit = Physics2D.Raycast(this.transform.position, new Vector2(-1, 0), rayLength, LayerMask.GetMask("Wall"));
                Debug.DrawRay(new Vector2(this.transform.position.x - raySpace, this.transform.position.y), new Vector2(-rayLength, 0), Color.red);
                break;

            //구슬의 이동방향이 오른쪽이라면
            case BeadDirection.Right:
                rayhit = Physics2D.Raycast(this.transform.position, new Vector2(1, 0), rayLength, LayerMask.GetMask("Wall"));
                Debug.DrawRay(new Vector2(this.transform.position.x + raySpace, this.transform.position.y), new Vector2(rayLength, 0), Color.red);
                break;
        }
    }

    //구슬 이동 정지
    public void BeadMoveStop()
    {
        rigid.velocity = Vector3.zero;
    }
}
