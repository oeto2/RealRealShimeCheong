using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinAction : MonoBehaviour
{
    //�� ��ġ��
    public Vector2[] pinPos;

    ////�̱������� ���
    //public static PinAction instance;

    //RectTransform
    RectTransform rectTransform;

    private void Awake()
    {
        //instance = this.GetComponent<PinAction>();
        rectTransform = this.GetComponent<RectTransform>();
    }

    private void Update()
    {
        //�� ��ġ ����
        if(Input.GetKeyDown(KeyCode.W))
        {
            PinPosChange(GameManager.instance.int_PinPosNum);
        }
    }

    public void PinPosChange(int _posNum)
    {
        Debug.Log($"_PosNum : {_posNum}");

        //Pin ������Ʈ�� ��ġ�� ����
        rectTransform.anchoredPosition = pinPos[_posNum];
    }
}
