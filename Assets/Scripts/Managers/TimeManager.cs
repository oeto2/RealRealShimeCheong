using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    //Ķ������ �ִϸ�����
    public Animator animator_Celender;

    //�ð��� ǥ���� �ؽ�Ʈ
    public Text text_TimeText;

    //���� �ð�
    [SerializeField]
    private float float_RealTime;

    //�ð� ���
    [SerializeField]
    private int timeMultipleCation;

    //�Ϸ簡 ���������� �ɸ��½ð�
    public int int_DayMinute;

    //���� °���� Ȯ���ϴ� ����
    [SerializeField]
    private int int_DayCount = 1;

    public float Timetiem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //���� �ð� = �ð� + ���
        float_RealTime += Time.deltaTime * timeMultipleCation;
        text_TimeText.text = float_RealTime.ToString("F0");

        //���� int_DayMinute ��ŭ �ð��� ����Ǿ������
        if (int_DayMinute <= float_RealTime)
        {
            //��¥ ����
            int_DayCount++;

            //�ð� �ʱ�ȭ
            float_RealTime = 0;

            //Ķ���� �ִϸ��̼� ����
            NextDayAnimaton(int_DayCount);
        }
    }

    //��¥�� �������� ������ �ִϸ��̼�
    public void NextDayAnimaton(int _day)
    {
        //Ķ���� ��¥ �ٲٱ�
        animator_Celender.SetInteger("DayNum", _day);
    }
}
