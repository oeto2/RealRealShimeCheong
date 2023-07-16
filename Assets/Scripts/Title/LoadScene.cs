using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //Scene을 불러와주는 메서드(Start Button UI로 실행)
    public void LoadMainScene()
    {
        //TextScnene 불러오기
        SceneManager.LoadScene("TestScene");
    }
}
