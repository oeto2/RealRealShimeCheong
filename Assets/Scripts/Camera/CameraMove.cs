using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //외부 스크립트
    public WallDetect wallDetectScr;

    //카메라가 추적할 오브젝트의 Transform값
    public Transform transform_TrargetObject;

    //카메라 추적 속도
    [SerializeField] [Range(1, 30)] private float speed;

    //카메라 제한 값
    private Transform transform_CameraLimit;

    //제한 영역들의 Transform값
    public Transform[] transform_Limits;

    //카메라의 화면의 세로 절반 값
    private float hight;
    //화면의 가로 값
    private float width;

    //현재 카메라 제한 구역번호
    public int int_CurLimitNum;

    private void Start()
    {
        //hight = 카메라 화면의 세로크기 절반 값
        hight = Camera.main.orthographicSize;
        //width = 카메라 화면의 가로크기 절반 값
        width = hight * Screen.width / Screen.height;
        //기본 제한 구역 맵 번호
        ChangeLimit(0);
        int_CurLimitNum = 0;
    }
    
    //제한 영역 바꾸기
    public void ChangeLimit(int x)
    {
        //카메라의 제한 범위 값 변경
        transform_CameraLimit = transform_Limits[x];

        int_CurLimitNum = x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //카메라 오브젝트의 포지션 값을 추적오브젝트의 포지션값 과의 사이값으로 적용 (Y축,Z축은 고정)
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 1, 0), new Vector3( transform_TrargetObject.position.x, 1,0), Time.deltaTime * speed);

        //lx = 제한영역의 가로 절반 값 - 카메라 가로 절반값
        float lx = transform_CameraLimit.localScale.x * 0.5f - width;
        //clampX = X축 제한 값
        float clampX = Mathf.Clamp(transform.position.x, -lx + transform_CameraLimit.position.x, lx + transform_CameraLimit.position.x);

        //ly = 제한 영역의 세로 절반 값 - 카메라 세로 절반값
        float ly = transform_CameraLimit.localScale.y * 0.5f - hight;
        //clampY = Y축 제한 값
        float clampY = Mathf.Clamp(transform.position.y, -ly + transform_CameraLimit.position.y, ly + transform_CameraLimit.position.y);

        //카메라 제한 값 설정
        transform.position = new Vector3(clampX, clampY, -10f);
    }
  
}
