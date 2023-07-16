using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pointer_soban : MonoBehaviour
{
    // 소반 UI control용
    public bool bool_isSoban = false;
    public bool bool_isSoban_exit = false;

    public GameObject images;
    public GameObject images_exit;

    /*
    public void OnPointerDown(PointerEventData eventData)
    {
        //Down Event
        Debug.Log("Touch! Soban!!!!");
    }
    */
    public void Update()
    {


    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("이건 Touch! Soban!!!!");
            //bool_isSoban = true;
            //bool_isSoban_exit = true;

            if (bool_isSoban == true)
            {
                images.SetActive(true);
                bool_isSoban = false;

                //images.SetActive(false);
            }

            else if (bool_isSoban_exit == true)
            {
                //images.SetActive(true);
                images.SetActive(false);
                bool_isSoban = true;
            }

        }
    }
}
