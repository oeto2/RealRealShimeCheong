using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    //���� ������ �ؽ�Ʈ
    public Text text_ChangeColor;

    //�⺻ ����
    public Color32 color32_Origin = new Color32(255, 255, 255, 255);

    //������ ����
    public Color32 color_Change;

    private void OnMouseEnter()
    {
        Debug.Log("�� ����");
        //Text �� ����
        text_ChangeColor.color = color_Change;
    }

    private void OnMouseExit()
    {
        Debug.Log("�� ����2");
        //Text �� ����
        text_ChangeColor.color = color32_Origin;
    }
}
