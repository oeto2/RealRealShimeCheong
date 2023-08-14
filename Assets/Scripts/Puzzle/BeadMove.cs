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

    //������ �̵������� Ȯ���ϴ� Flag
    public bool isMove;

    //������ �̵�����
    public BeadDirection beadDir = BeadDirection.UP;

    //���� ����
    public bool upWall;
    public bool downWall;
    public bool leftWall;
    public bool rightWall;

    //������ �����ߴ��� Ȯ���ϴ� flag
    public bool isStop;

    //������ ��� �����̰� �ϴ� bool��
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
        //��� �����̱�
        BeadMoveCtrl();

        //RayCast ���⼳��
        RayCastDirection();
    }

    //������ ������ ���� �޼���
    public void BeadMoveCtrl()
    {
        //������ �̵����ΰ� �˼��ִ� ����
        if (rigid.velocity != Vector2.zero)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }

        #region ����Ű�� ���� ���� �̵� ����

        //���� �̵�
        if ((Input.GetKeyDown(KeyCode.W) && !isMove && !upWall))
        {
            Debug.Log("���� ���� �̵�");

            //RayCast ���� ����
            beadDir = BeadDirection.UP;

            upMove = true;
            downMove = false;
            leftMove = false;
            rightMove = false;

            //�ƹ��͵� �������� �ʾҴٸ�
            if (rayhit.collider == null)
            {
                //���� �̵�
                rigid.velocity = Vector2.up * speed;
            }
        }

        //�Ʒ��� �̵�
        else if ((Input.GetKeyDown(KeyCode.S) && !isMove && !downWall))
        {
            Debug.Log("���� �Ʒ��� �̵�");

            //RayCast ���� ����
            beadDir = BeadDirection.Down;

            upMove = false;
            downMove = true;
            leftMove = false;
            rightMove = false;

            //�ƹ��͵� �������� �ʾҴٸ�
            if (rayhit.collider == null)
            {
                rigid.velocity = Vector2.down * speed;
            }
        }

        //�������� �̵�
        else if ((Input.GetKeyDown(KeyCode.A) && !isMove && !leftWall))
        {
            Debug.Log("���� �������� �̵�");

            //RayCast ���� ����
            beadDir = BeadDirection.Left;

            upMove = false;
            downMove = false;
            leftMove = true;
            rightMove = false;

            //�ƹ��͵� �������� �ʾҴٸ�
            if (rayhit.collider == null)
            {
                rigid.velocity = Vector2.left * speed;
            }
        }

        //������ �̵�
        else if ((Input.GetKeyDown(KeyCode.D) && !isMove && !rightWall))
        {
            Debug.Log("���� �����ʷ� �̵�");

            //RayCast ���� ����
            beadDir = BeadDirection.Right;

            upMove = false;
            downMove = false;
            leftMove = false;
            rightMove = true;

            //�ƹ��͵� �������� �ʾҴٸ�
            if (rayhit.collider == null)
            {
                rigid.velocity = Vector2.right * speed;
            }
        }

        //���� ��� �̵��ϴ� ����
        if(upMove && rayhit.collider == null)
        {
            rigid.velocity = Vector2.up * speed;
        }

        //�Ʒ��� ��� �̵��ϴ� ����
        else if (downMove && rayhit.collider == null)
        {
            rigid.velocity = Vector2.down * speed;
        }

        //�������� ��� �̵��ϴ� ����
        else if (leftMove && rayhit.collider == null)
        {
            rigid.velocity = Vector2.left * speed;
        }

        //���������� ��� �̵��ϴ� ����
        else if (rightMove && rayhit.collider == null)
        {
            rigid.velocity = Vector2.right * speed;
        }

        #endregion

        //������ �̵����� ����
        if (rayhit.collider != null)
        {
            //���� ���� �ִٸ�
            if (beadDir == BeadDirection.UP && !Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("���� ����");

                //���� �̵� ����
                BeadMoveStop();

                //������
                upWall = true;
                downWall = false;
                leftWall = false;
                rightWall = false;
            }

            //���� �Ʒ��� �ִٸ�
            else if (beadDir == BeadDirection.Down && !Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("�Ʒ��� ����");
                //���� �̵� ����
                BeadMoveStop();

                //������
                upWall = false;
                downWall = true;
                leftWall = false;
                rightWall = false;
            }

            //���� ���ʿ� �ִٸ�
            else if (beadDir == BeadDirection.Left && !Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("���� ����");

                //���� �̵� ����
                BeadMoveStop();

                //������
                upWall = false;
                downWall = false;
                leftWall = true;
                rightWall = false;
            }

            //���� �����ʿ� �ִٸ�
            else if (beadDir == BeadDirection.Right && !Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("������ ����");

                //���� �̵� ����
                BeadMoveStop();

                //������
                upWall = false;
                downWall = false;
                leftWall = false;
                rightWall = true;
            }
        }

        //�ƹ��͵� �������� �ʾҴٸ�
        else if (rayhit.collider == null)
        {
            upWall = false;
            downWall = false;
            leftWall = false;
            rightWall = false;
        }
    }

    //Raycast�� ����
    public void RayCastDirection()
    {
        switch (beadDir)
        {
            //������ �̵������� �����̶��
            case BeadDirection.UP:
                rayhit = Physics2D.Raycast(this.transform.position, new Vector2(0, 1), rayLength, LayerMask.GetMask("Wall"));
                Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y + raySpace), new Vector2(0, rayLength), Color.red);
                break;

            //������ �̵������� �Ʒ����̶��
            case BeadDirection.Down:
                rayhit = Physics2D.Raycast(this.transform.position, new Vector2(0, -1), rayLength, LayerMask.GetMask("Wall"));
                Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y - raySpace), new Vector2(0, -rayLength), Color.red);
                break;

            //������ �̵������� �����̶��
            case BeadDirection.Left:
                rayhit = Physics2D.Raycast(this.transform.position, new Vector2(-1, 0), rayLength, LayerMask.GetMask("Wall"));
                Debug.DrawRay(new Vector2(this.transform.position.x - raySpace, this.transform.position.y), new Vector2(-rayLength, 0), Color.red);
                break;

            //������ �̵������� �������̶��
            case BeadDirection.Right:
                rayhit = Physics2D.Raycast(this.transform.position, new Vector2(1, 0), rayLength, LayerMask.GetMask("Wall"));
                Debug.DrawRay(new Vector2(this.transform.position.x + raySpace, this.transform.position.y), new Vector2(rayLength, 0), Color.red);
                break;
        }
    }

    //���� �̵� ����
    public void BeadMoveStop()
    {
        Debug.Log("���� ����");

        rigid.velocity = Vector3.zero;
    }
}
