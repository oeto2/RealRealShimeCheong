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
        if(rigid.velocity != Vector2.zero)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }
        
        
        #region ����Ű�� ���� ���� �̵� ����
        //���� �̵�
        if (Input.GetKeyDown(KeyCode.W) && !isMove)
        {
            Debug.Log("���� ���� �̵�");
            rigid.velocity = Vector2.up * speed;

            //rigid.AddForce(new Vector2(0, 10 * power));

            //RayCast ���� ����
            beadDir = BeadDirection.UP;
        }
        //�Ʒ��� �̵�
        else if (Input.GetKeyDown(KeyCode.S) && !isMove)
        {
            Debug.Log("���� �Ʒ��� �̵�");
            rigid.velocity = Vector2.down * speed;

            //rigid.AddForce(new Vector2(0, -10 * power));

            //RayCast ���� ����
            beadDir = BeadDirection.Down;
        }
        //�������� �̵�
        else if (Input.GetKeyDown(KeyCode.A) && !isMove)
        {
            Debug.Log("���� �������� �̵�");
            rigid.velocity = Vector2.left * speed;


            //rigid.AddForce(new Vector2(-10 * power, 0));

            //RayCast ���� ����
            beadDir = BeadDirection.Left;
        }
        //������ �̵�
        else if (Input.GetKeyDown(KeyCode.D) && !isMove)
        {
            Debug.Log("���� �����ʷ� �̵�");
            rigid.velocity = Vector2.right * speed;


            //rigid.AddForce(new Vector2(10 * power, 0));

            //RayCast ���� ����
            beadDir = BeadDirection.Right;
        }
        #endregion

        //������ �̵����� ����
        if (rayhit.collider != null)
        {
            //���� �̵� ����
            BeadMoveStop();
            Debug.Log("���� �̵� ����");
        }
    }

    //Raycast�� ����
    public void RayCastDirection()
    {
        switch(beadDir)
        {
            //������ �̵������� �����̶��
            case BeadDirection.UP:
                rayhit = Physics2D.Raycast(this.transform.position, new Vector2(0,1), rayLength, LayerMask.GetMask("Wall"));
                Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y + raySpace), new Vector2(0, rayLength), Color.red);
                break;

            //������ �̵������� �Ʒ��� �̶��
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
        rigid.velocity = Vector3.zero;
    }
}
