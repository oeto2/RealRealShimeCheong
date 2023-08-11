using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //Player�� Animator
    public Animator animator_Player;

    //�ܺ� ��ũ��Ʈ
    public Controller playerCtrlScr;

    //Player�� �̵������� Ȯ���ϴ� flag
    public bool isMove;

    //Player�� �̹��� ȸ�� ����
    public bool isFilp;

    //player�� SpriteRenderer
    public SpriteRenderer spriteRenderer_Player;

    // Update is called once per frame
    void Update()
    {
        //Player Move Animation
        #region
        //Player�� �̵������� Ȯ���ϴ� ����
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Debug.Log("Player �̵���");
            isMove = true;
        }

        //Player�� �̵��� �������� Ȯ���ϴ� ����
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            isMove = false;

        }

        //Player�� �̵����̶��
        if (isMove)
        {
            //�̵��ִϸ��̼� ����
            animator_Player.SetBool("moveStart", true);
        }
        //Player�� �̵������� �ʴٸ�
        else if (!isMove)
        {
            //�̵� �ִϸ��̼� ����
            animator_Player.SetBool("moveStart", false);
        }
        #endregion

        //Player �̹��� ȸ�� ����
        #region

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            isFilp = false;
            spriteRenderer_Player.flipX = isFilp;
        }

        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            isFilp = true;
            spriteRenderer_Player.flipX = isFilp;
        }
        #endregion

        //Player �̵� �ִϸ��̼� ����
        if (playerCtrlScr.canMove)
        {
            //Debug.Log("�̵� �ִϸ��̼� ����");
            animator_Player.SetBool("moveStart", false);
        }
    }
}
