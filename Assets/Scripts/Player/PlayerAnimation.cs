using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //Player의 Animator
    public Animator animator_Player;

    //외부 스크립트
    public Controller playerCtrlScr;

    //Player가 이동중인지 확인하는 flag
    public bool isMove;

    //Player의 이미지 회전 조건
    public bool isFilp;

    //player의 SpriteRenderer
    public SpriteRenderer spriteRenderer_Player;

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Player Move Animation
        #region
        //Player가 이동중인지 확인하는 조건
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            isMove = true;
        }

        //Player가 이동이 끝났는지 확인하는 조건
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            isMove = false;

        }

        //Player가 이동중이라면
        if (isMove)
        {
            if (!playerCtrlScr.isTalk &&
            !GameManager.instance.isBeadPuzzleStart && !playerCtrlScr.dialogueOn && !playerCtrlScr.moveStop)
            {
                //이동 애니메이션 시작
                animator_Player.SetBool("moveStart", true);
            }
        }
        //Player가 이동중이지 않다면
        else if (!isMove)
        {
            //이동 애니메이션 종료
            animator_Player.SetBool("moveStart", false);
        }
        #endregion

        //Player 이미지 회전 조건
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

        //Player 이동 애니메이션 끄기
        if (playerCtrlScr.canMove)
        {
            //Debug.Log("이동 애니메이션 정지");
            animator_Player.SetBool("moveStart", false);
        }
    }

    //봇짐 애니메이션으로 변경
    public void ChangeAnimationBotzime()
    {
        animator_Player.SetBool("getBotzime", true);
    }

    //기본 애니메이션으로 변경
    public void ChangeAnimationNomal()
    {
        animator_Player.SetBool("getBotzime", false);
    }
}
