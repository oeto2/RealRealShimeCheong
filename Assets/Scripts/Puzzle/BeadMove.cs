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

    //벽의 방향
    public bool upWall;
    public bool downWall;
    public bool leftWall;
    public bool rightWall;

    //구슬이 정지했는지 확인하는 flag
    public bool isStop;

    //구슬을 계속 움직이게 하는 bool값
    public bool upMove;
    public bool downMove;
    public bool leftMove;
    public bool rightMove;



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
        if (rigid.velocity != Vector2.zero)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }

        #region 방향키에 따른 구슬 이동 로직

        //위로 이동
        if ((Input.GetKeyDown(KeyCode.W) && !isMove && !upWall))
        {
            Debug.Log("구슬 위로 이동");

            //RayCast 방향 설정
            beadDir = BeadDirection.UP;

            upMove = true;
            downMove = false;
            leftMove = false;
            rightMove = false;

            //아무것도 감지되지 않았다면
            if (rayhit.collider == null)
            {
                //구슬 이동
                rigid.velocity = Vector2.up * speed;
            }
        }

        //아래로 이동
        else if ((Input.GetKeyDown(KeyCode.S) && !isMove && !downWall))
        {
            Debug.Log("구슬 아래로 이동");

            //RayCast 방향 설정
            beadDir = BeadDirection.Down;

            upMove = false;
            downMove = true;
            leftMove = false;
            rightMove = false;

            //아무것도 감지되지 않았다면
            if (rayhit.collider == null)
            {
                rigid.velocity = Vector2.down * speed;
            }
        }

        //왼쪽으로 이동
        else if ((Input.GetKeyDown(KeyCode.A) && !isMove && !leftWall))
        {
            Debug.Log("구슬 왼쪽으로 이동");

            //RayCast 방향 설정
            beadDir = BeadDirection.Left;

            upMove = false;
            downMove = false;
            leftMove = true;
            rightMove = false;

            //아무것도 감지되지 않았다면
            if (rayhit.collider == null)
            {
                rigid.velocity = Vector2.left * speed;
            }
        }

        //오른쪽 이동
        else if ((Input.GetKeyDown(KeyCode.D) && !isMove && !rightWall))
        {
            Debug.Log("구슬 오른쪽로 이동");

            //RayCast 방향 설정
            beadDir = BeadDirection.Right;

            upMove = false;
            downMove = false;
            leftMove = false;
            rightMove = true;

            //아무것도 감지되지 않았다면
            if (rayhit.collider == null)
            {
                rigid.velocity = Vector2.right * speed;
            }
        }

        //위로 계속 이동하는 조건
        if(upMove && rayhit.collider == null)
        {
            rigid.velocity = Vector2.up * speed;
        }

        //아래로 계속 이동하는 조건
        else if (downMove && rayhit.collider == null)
        {
            rigid.velocity = Vector2.down * speed;
        }

        //왼쪽으로 계속 이동하는 조건
        else if (leftMove && rayhit.collider == null)
        {
            rigid.velocity = Vector2.left * speed;
        }

        //오른쪽으로 계속 이동하는 조건
        else if (rightMove && rayhit.collider == null)
        {
            rigid.velocity = Vector2.right * speed;
        }

        #endregion

        //구슬의 이동정지 조건
        if (rayhit.collider != null)
        {
            //벽이 위에 있다면
            if (beadDir == BeadDirection.UP && !Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("위벽 감지");

                //구슬 이동 정지
                BeadMoveStop();

                //벽감지
                upWall = true;
                downWall = false;
                leftWall = false;
                rightWall = false;
            }

            //벽이 아래에 있다면
            else if (beadDir == BeadDirection.Down && !Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("아래벽 감지");
                //구슬 이동 정지
                BeadMoveStop();

                //벽감지
                upWall = false;
                downWall = true;
                leftWall = false;
                rightWall = false;
            }

            //벽이 왼쪽에 있다면
            else if (beadDir == BeadDirection.Left && !Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("왼쪽 감지");

                //구슬 이동 정지
                BeadMoveStop();

                //벽감지
                upWall = false;
                downWall = false;
                leftWall = true;
                rightWall = false;
            }

            //벽이 오른쪽에 있다면
            else if (beadDir == BeadDirection.Right && !Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("오른쪽 감지");

                //구슬 이동 정지
                BeadMoveStop();

                //벽감지
                upWall = false;
                downWall = false;
                leftWall = false;
                rightWall = true;
            }
        }

        //아무것도 감지되지 않았다면
        else if (rayhit.collider == null)
        {
            upWall = false;
            downWall = false;
            leftWall = false;
            rightWall = false;
        }
    }

    //Raycast의 방향
    public void RayCastDirection()
    {
        switch (beadDir)
        {
            //구슬의 이동방향이 위쪽이라면
            case BeadDirection.UP:
                rayhit = Physics2D.Raycast(this.transform.position, new Vector2(0, 1), rayLength, LayerMask.GetMask("Wall"));
                Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y + raySpace), new Vector2(0, rayLength), Color.red);
                break;

            //구슬의 이동방향이 아래쪽이라면
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
        Debug.Log("구슬 정지");

        rigid.velocity = Vector3.zero;
    }
}
