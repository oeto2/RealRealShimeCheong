using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetect : MonoBehaviour
{
    //외부 스크립트
    public Controller playerControlScr;

    //벽을 감지할 RayCast
    private RaycastHit2D rayHit2D;

    public SpriteRenderer spriteRender;

    private int direction;

    //얼마만큼 레이를 띄워서 할건지
    [SerializeField]
    [Range(-3, 3f)] private float raySpace;

    //ray의 길이
    [SerializeField]
    [Range(0, 3f)] private float rayLength;

    private void Update()
    {
        if(!spriteRender.flipX)
        {
            direction = 1;
        }

        else if (spriteRender.flipX)
        {
            direction = -1;
        }

        //벽 감지용 디버그 레이
        Debug.DrawRay(new Vector2(transform.position.x + (raySpace * direction), transform.position.y), new Vector2(rayLength * direction, 0), new Color(0, 1, 0));

        //벽 감지용 레이
        rayHit2D = Physics2D.Raycast(new Vector2(transform.position.x + (raySpace * direction), transform.position.y), new Vector2(1 * direction, 0), rayLength, LayerMask.GetMask("Wall"));

        //벽이 감지 되었다면
        if(rayHit2D.collider != null)
        {
            //이동 제한
            playerControlScr.detectWall = true;
        }

        //아무것도 감지되지 않았다면
        else if(rayHit2D.collider == null)
        {
            //이동제한 해제
            playerControlScr.detectWall = false;
        }

    }
}
