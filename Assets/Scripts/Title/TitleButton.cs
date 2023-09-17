using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    //색을 변경할 텍스트
    public Text text_ChangeColor;

    //기본 색깔
    public Color32 color32_Origin = new Color32(255, 255, 255, 255);

    //변경할 색깔
    public Color32 color_Change;

    private void OnMouseEnter()
    {
        Debug.Log("색 변경");
        //Text 색 변경
        text_ChangeColor.color = color_Change;
    }

    private void OnMouseExit()
    {
        Debug.Log("색 변경2");
        //Text 색 변경
        text_ChangeColor.color = color32_Origin;
    }
}
