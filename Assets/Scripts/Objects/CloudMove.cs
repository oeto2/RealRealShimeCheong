using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    //구름의 이동속도
    [Range(0, 10f)] public float speed;

    //구름의 End position 값
    public Transform endPos;

    //구름의 Start Position 값
    public Transform startPos;

    private void Update()
    {
        //구름 이동
        this.transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);

        //구름이 endPos에 도달했을경우
        if(this.transform.position.x < endPos.position.x)
        {
            //구름을 StartPos로 옮김
            this.transform.position = new Vector2(startPos.position.x, this.transform.position.y);
        }
    }

}
