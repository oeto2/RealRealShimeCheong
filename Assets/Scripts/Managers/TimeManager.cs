using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

//������ ������
[System.Serializable]
public class TimeSaveData
{
    //������
    public TimeSaveData(float _Time, int _Day)
    {
        time = _Time; day = _Day;
    }

    //������ �ð�
    public float time;

    //������ ��¥
    public int day;
}

//�ҷ��� ������
[System.Serializable]
public class TimeLoadData
{
    //������
    public TimeLoadData(float _Time, int _Day)
    {
        time = _Time; day = _Day;
    }

    //�ҷ��� �ð�
    public float time;

    //�ҷ��� ��¥
    public int day;
}

public class TimeManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public TutorialManager tutorialManagerScr;
    public UIManager uiManagerScr;

    //Ķ������ �ִϸ�����
    public Animator animator_Celender;

    //�ð��� ǥ���� �ؽ�Ʈ
    public Text text_TimeText;

    //��¥ UI
    public GameObject gameObjcet_DayUI;

    //���� �ð�
    [SerializeField]
    private float float_RealTime;

    //�ð� ���
    [SerializeField]
    private int timeMultipleCation;

    //�Ϸ簡 ���������� �ɸ��½ð�
    public int int_DayMinute;

    //���� °���� Ȯ���ϴ� ����
    public int int_DayCount = 1;

    //�ð� ���߱�
    private bool timeStop;

    //�÷���Ÿ�� �ð�
    private float float_PlayTimeHour = 0;
    //�÷���Ÿ�� ��
    private float float_PlayTimeMinute = 0;
    //�÷���Ÿ�� ��
    public float float_PlayTimeSec = 0;

    //���� �÷��� �ð�
    public float float_SavePlayTime = 0;

    //������ ������ Ŭ����
    public TimeSaveData curTimeSaveData;

    //���� ���� ��ġ
    private string saveFilePath;

    //�ҷ��� ������ Ŭ����
    public TimeLoadData curTimeLoadData;

    //�÷���Ÿ��
    private string playTime;

    //�ؽð� �̹��� ��ȣ
    public int sunClcokImageNum;

    private void Start()
    {
        //���� ���� ��ġ
        saveFilePath = Application.persistentDataPath + "/TimeDataText.txt";
    }

    private void FixedUpdate()
    {
        //�ؽð� �ִϸ��̼�
        ChageSunClock();

        //�÷��� Ÿ�� = ��Ÿ Ÿ��
        float_PlayTimeSec += Time.deltaTime;

        //Debug.Log(float_PlayTimeSec);

        if (!timeStop)
        {
            //���� �ð� = �ð� + ���
            float_RealTime += Time.deltaTime * timeMultipleCation;
            text_TimeText.text = float_RealTime.ToString("F0");

            //���� �Ϸ簡 ������ ���
            if (int_DayMinute <= float_RealTime)
            {
                //��¥ ����
                int_DayCount++;

                //�ð� ����
                ResetTime();

                //Ķ���� �ִϸ��̼� ����
                NextDayAnimaton(int_DayCount);

                //�ð� ����
                StopTime();

                //�Ϸ簡 ������
                tutorialManagerScr.PassDay();
            }
        }
    }

    //��¥�� �������� ������ �ִϸ��̼�
    public void NextDayAnimaton(int _day)
    {
        //Ķ���� ��¥ �ٲٱ�
        animator_Celender.SetInteger("DayNum", _day);
    }

    //��¥ UI �����ֱ�
    public void ShowDayUI()
    {
        gameObjcet_DayUI.SetActive(true);
    }

    //��¥ UI ����
    public void CloseDayUI()
    {
        gameObjcet_DayUI.SetActive(false);
    }

    //�ð� ����
    public void ResetTime()
    {
        float_RealTime = 0f;
        text_TimeText.text = float_RealTime.ToString("F0");
    }

    //�ð� ���߱�
    public void StopTime()
    {
        timeStop = true;
    }

    //�ð� ��Ӱ���
    public void ContinueTime()
    {
        timeStop = false;
    }

    //�Ϸ� ������ ��ȭ �Ϸ�
    public void PassDaySentenceEnd()
    {
        //�ð� ��� �帣��
        ContinueTime();
    }

    //�Ϸ� ������
    public void PassOneDay()
    {
        //�Ϸ� ������
        int_DayCount++;
        //�ð� 0���� �ʱ�ȭ
        float_RealTime = 0;
        //Ķ���� ��¥ ����
        NextDayAnimaton(int_DayCount);
    }


    //������ ����
    public void Save(int _slotNum)
    {
        Debug.Log("Save TimeManagerData");
        

        //������ ������ �ֱ�
        curTimeSaveData = new TimeSaveData(float_RealTime, int_DayCount);

        //���̺� ������
        string jSaveData = JsonUtility.ToJson(curTimeSaveData);

        //������ ���� ����
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);
    }

    //������ �ε�
    public void Load(int _SlotNum)
    {
        Debug.Log("Load TimeManagerData");

        //���̺� ���� �о����
        string jLoadData = File.ReadAllText(saveFilePath + _SlotNum.ToString());

        //�о�� ���� ����Ʈ�� ����
        curTimeLoadData = JsonUtility.FromJson<TimeLoadData>(jLoadData);

        //�ð� �缳��
        float_RealTime = curTimeLoadData.time;

        //��¥ �缳��
        int_DayCount = curTimeLoadData.day;

        //Ķ���� ��¥ ����
        NextDayAnimaton(int_DayCount);
    }

    //UI ���Կ� ǥ���� ��¥
    public string GetDayCount()
    {
        return int_DayCount.ToString() + "��°";
    }

    //�÷��� Ÿ���� ���ϴ� �޼���
    public string GetPlayTimeText()
    {
        //������ 1�� �̻� �ߴٸ�
        if (float_PlayTimeSec / 60 >= 1)
        {
            float_PlayTimeMinute = float_PlayTimeSec / 60;
        }
        //�ƴϸ� 0
        else
        {
            float_PlayTimeMinute = 0;
        }

        //������ 60�� �̻� �ߴٸ�
        if (float_PlayTimeSec / 3600 >= 1)
        {
            float_PlayTimeHour = float_PlayTimeSec / 3600;
        }
        //�ƴϸ� 0
        else
        {
            float_PlayTimeHour = 0;
        }

        playTime = "���� �ð� : " + MathF.Truncate(float_PlayTimeHour) + "�ð� " + MathF.Truncate(float_PlayTimeMinute) + "��";

        Debug.Log("playTimetext : " + playTime);

        return playTime;
    }

    //�Ѱ��� �ð� ��
    public float GetPlayTimeSec(int _slotNum)
    {
        Debug.Log("�Ѱ��� �÷��� Ÿ�� : " + float_PlayTimeSec);

        //���� �÷��� Ÿ�� ���� �Ѱ���
        return float_PlayTimeSec;
    }

    //�޾ƿ� ���� �÷���Ÿ�� ��
    public void SetPlayTimeSec(float _playTime)
    {
        Debug.Log("����� �÷��� Ÿ�� : " + _playTime.ToString());

        //���� �÷��� �ð� �޾ƿ���
        float_SavePlayTime = _playTime;
        //���� �÷��� Ÿ���� ���� �÷��� Ÿ�� ������ ����
        float_PlayTimeSec = float_SavePlayTime;
    }

    //�ؽð� UI �̹��� ����
    public void ChageSunClock()
    {
        sunClcokImageNum = ((int)MathF.Truncate(float_RealTime) / 5);
        uiManagerScr.ChangeSunClockImage(sunClcokImageNum);
    }
}
