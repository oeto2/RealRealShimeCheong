using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(Database.Item.Get(1000).Name);
        Debug.Log(Database.Clue.Get(2000).Name);
        Debug.Log(Database.Dialogue.Get(4999).Comment);
    }
}
