using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    //�ð��� ǥ���� �ؽ�Ʈ
    public Text text_TimeText;

    //���� �ð�
    private float RealTimeSec;

    //�ð� ���
    [SerializeField]
    private int timeMultipleCation;

    //��
    private int minute = 0;

    //��
    private int day = 0;

    //�Ϸ縦 ������� �Ұ���
    [SerializeField]
    private int dayMinute = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RealTimeSec += Time.deltaTime * timeMultipleCation;
        text_TimeText.text = day + "��      " +  minute + "�� " + RealTimeSec.ToString("F0") + "��";

        //1���� 60��
        if(RealTimeSec >= 60)
        {
            minute++;
            RealTimeSec = 0;
        }

        //���� ������ dayminute ������ Ŀ���ų� ���� ���
        if(minute >= dayMinute)
        {
            day++;
            minute = 0;
        }
    }
}
