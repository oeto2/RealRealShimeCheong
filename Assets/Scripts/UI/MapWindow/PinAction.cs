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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PinPosChange(int _posNum)
    {
        Debug.Log($"_PosNum : {_posNum}");
        //Pin ������Ʈ�� ��ġ�� ����
        rectTransform.anchoredPosition = pinPos[_posNum];

    }
}
