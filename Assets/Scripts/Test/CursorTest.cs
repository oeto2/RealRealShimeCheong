using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        
        Debug.Log(Input.mousePosition.x);
    }
}
