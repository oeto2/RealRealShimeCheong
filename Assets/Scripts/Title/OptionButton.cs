using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    //옵션 인터페이스 오브젝트
    public GameObject gameObject_optionInterface;


    //옵션 설정 창을 띄워주는 메서드
    public void OptionStart()
    {
        //옵션 인터페이스 오브젝트 활성화
        gameObject_optionInterface.SetActive(true);
    }
}
