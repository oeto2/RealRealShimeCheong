using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    //�ð��� ǥ���� �ؽ�Ʈ
    public Text text_TimeText;

    //���� �ð�
    [SerializeField]
    private float float_RealTime;

    //�ð� ���
    [SerializeField]
    private int timeMultipleCation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float_RealTime += Time.deltaTime * timeMultipleCation;
        text_TimeText.text = float_RealTime.ToString("F0");
    }
}
