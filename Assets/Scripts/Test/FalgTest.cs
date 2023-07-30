using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FalgTest : MonoBehaviour
{

    public bool falg = false;

    public bool DelayFlag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Z´©¸§");
        }
    }

    IEnumerator FalgTrueDelay (bool _flag, Action<bool> _flagReturn)
    {
        yield return new WaitForSeconds(0.2f);

        if(!_flag)
        {
            _flagReturn(true);
        }
    }
}
