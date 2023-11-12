using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrawPuzzle : MonoBehaviour
{
    //새끼줄 퍼즐 UI
    public GameObject gameObject_StrawUI;


    //왼쪽 버튼 클릭시
    public void LeftButton_Click()
    {
        //시계방향 새끼줄 아이템 획득
        ObjectManager.instance.GetItem(1013);

        //새끼줄 퍼즐 종료
        GameManager.instance.StrawPuzzleClear();
    }

    //오른쪽 버튼 클릭시
    public void RightButton_Click()
    {
        //반시계방향 새끼줄 아이템 획득
        ObjectManager.instance.GetItem(1014);

        //새끼줄 퍼즐 종료
        GameManager.instance.StrawPuzzleClear();
    }
}



