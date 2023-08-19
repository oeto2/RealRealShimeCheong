using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    //������ �̵��ӵ�
    [Range(0, 10f)] public float speed;

    //������ End position ��
    public Transform endPos;

    //������ Start Position ��
    public Transform startPos;

    private void Update()
    {
        //���� �̵�
        this.transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);

        //������ endPos�� �����������
        if(this.transform.position.x < endPos.position.x)
        {
            //������ StartPos�� �ű�
            this.transform.position = new Vector2(startPos.position.x, this.transform.position.y);
        }
    }

}
