using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinAction : MonoBehaviour
{
    //핀 위치들
    public Vector2[] pinPos;

    ////싱글톤패턴 기법
    //public static PinAction instance;

    //RectTransform
    RectTransform rectTransform;

    private void Awake()
    {
        //instance = this.GetComponent<PinAction>();
        rectTransform = this.GetComponent<RectTransform>();
    }

    private void Update()
    {
        //핀 위치 변경
        if(Input.GetKeyDown(KeyCode.W))
        {
            PinPosChange(GameManager.instance.int_PinPosNum);
        }
    }

    public void PinPosChange(int _posNum)
    {
        Debug.Log($"_PosNum : {_posNum}");

        //Pin 오브젝트의 위치값 변경
        rectTransform.anchoredPosition = pinPos[_posNum];
    }
}
