using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //Player의 Animator
    public Animator animator_Player;

    //Player가 이동중인지 확인하는 flag
    public bool isMove;

    //Player의 이미지 회전 조건
    public bool isFilp;

    //player의 SpriteRenderer
    public SpriteRenderer spriteRenderer_Player;

    // Start is called before the first frame update
    void Start()
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
        else if(Input.GetAxisRaw("Horizontal") == 0)
        {
            isMove = false;
            
        }

        //Player가 이동중이라면
        if(isMove)
        {
            //이동애니메이션 시작
            animator_Player.SetBool("moveStart", true);
        }
        //Player가 이동중이지 않다면
        else if(!isMove)
        {
            //이동 애니메이션 종료
            animator_Player.SetBool("moveStart", false);
        }
        #endregion

        //Player 이미지 회전 조건
        #region
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            isFilp = false;
            spriteRenderer_Player.flipX = isFilp;
        }

        else if(Input.GetAxisRaw("Horizontal") == -1)
        {
            isFilp = true;
            spriteRenderer_Player.flipX = isFilp;
        }
        #endregion
    }
}
