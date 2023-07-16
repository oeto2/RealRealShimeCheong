using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineObject : MonoBehaviour
{
    //원래 오브젝트
    public GameObject gameObject_Origin;

    //빛날 오브젝트
    public GameObject gameObject_Shine;

    //오브젝트에 마우스를 올리고 있을경우
    private void OnMouseEnter()
    {
        if (gameObject.CompareTag("Object"))
        {
            gameObject_Origin.SetActive(false);
            gameObject_Shine.SetActive(true);
        }

    }

    //오브젝트에 마우스가 벗어날 경우
    private void OnMouseExit()
    {
        Debug.Log(gameObject.name + "벗어남");
        gameObject_Origin.SetActive(true);
        gameObject_Shine.SetActive(false);
    }
}
