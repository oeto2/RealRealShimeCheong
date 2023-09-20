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

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Player Move Animation
        #region
        //Player�� �̵������� Ȯ���ϴ� ����
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
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
            if (!playerCtrlScr.isTalk &&
            !GameManager.instance.isBeadPuzzleStart && !playerCtrlScr.dialogueOn && !playerCtrlScr.moveStop)
            {
                //�̵� �ִϸ��̼� ����
                animator_Player.SetBool("moveStart", true);
            }
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

        if (Input.GetAxisRaw("Horizontal") == 1 && !playerCtrlScr.isTalk &&
            !GameManager.instance.isBeadPuzzleStart && !playerCtrlScr.dialogueOn && !playerCtrlScr.moveStop)
        {
            isFilp = false;
            spriteRenderer_Player.flipX = isFilp;
        }

        else if (Input.GetAxisRaw("Horizontal") == -1 && !playerCtrlScr.isTalk &&
            !GameManager.instance.isBeadPuzzleStart && !playerCtrlScr.dialogueOn && !playerCtrlScr.moveStop)
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

    //���� �ִϸ��̼����� ����
    public void ChangeAnimationBotzime()
    {
        animator_Player.SetBool("getBotzime", true);
    }

    //�⺻ �ִϸ��̼����� ����
    public void ChangeAnimationNomal()
    {
        animator_Player.SetBool("getBotzime", false);
    }
}
