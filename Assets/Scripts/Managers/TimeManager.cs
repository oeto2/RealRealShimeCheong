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
    public TimeSaveData(float _Time, int _Day, byte _curColorValue, bool _realTimeStop)
    {
        time = _Time;
        day = _Day; 
        curColorValue = _curColorValue;
        realTimeStop = _realTimeStop;
    }

    //������ �ð�
    public float time;

    //������ ��¥
    public int day;

    //���� ����� �÷���
    public byte curColorValue;

    //�ð� ���� ����
    public bool realTimeStop;
}

//�ҷ��� ������
[System.Serializable]
public class TimeLoadData
{
    //������
    public TimeLoadData(float _Time, int _Day, byte _curColorValue, bool _realTimeStop)
    {
        time = _Time;
        day = _Day;
        curColorValue = _curColorValue;
        realTimeStop = _realTimeStop;
    }

    //�ҷ��� �ð�
    public float time;

    //�ҷ��� ��¥
    public int day;

    //���� ����� �÷���
    public byte curColorValue;

    //�ð� ���� ����
    public bool realTimeStop;
}

public class TimeManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public TutorialManager tutorialManagerScr;
    public UIManager uiManagerScr;
    public MoonLight moonlightScr;
    public CameraMove cameraMoveScr;

    //Ķ������ �ִϸ�����
    public Animator animator_Celender;

    //�ð��� ǥ���� �ؽ�Ʈ
    public Text text_TimeText;

    //��¥ UI
    public GameObject gameObjcet_DayUI;

    //���� �ð�
    [SerializeField]
    private float float_RealTime;
    private int int_RealTime;

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

    //��ο������ϴ� ������Ʈ��
    public SpriteRenderer[] spriteRen_ObumbrateObj;

    //���� ������Ʈ���� RGB��
    public Color32 curObjectRGB;

    //�ּ� ���
    public int minBrightness;

    //������Ʈ���� ��ħ RGB��
    public Color32 startRGBValue;

    //������Ʈ���� �� RGB��
    public Color32 nightRGBValue;

    //������Ʈ ��� ������ �������� 
    private bool isSettingEnd;

    //1�� �ڷ�ƾ�� ����������
    private bool isOneSecStart;

    //�ָ��� ��������Ʈ ����
    public SpriteRenderer spriteRen_Joomack;

    //�ָ��� ���Ķ���Ʈ �̹�����
    public Sprite[] sprite_Joomacks;

    //�̱��� ����
    public static TimeManager instance = null;

    //�ð��� ���� �Ǿ����� Ȯ���ϴ� flag
    public bool isTimeStop;

    //�ð� ��¥ ���� (����)
    public bool realTimeStop;

    //������ ������ ���� �÷���
    public bool isEndingStart;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        //���� ���� ��ġ
        saveFilePath = Application.persistentDataPath + "/TimeDataText.txt";


        //������Ʈ�� ���� RGB��
        curObjectRGB = startRGBValue;
    }

    private void Update()
    {
        //�÷��̾ �� �ȿ� ���� ��� �ð� ����
        if (cameraMoveScr.int_CurLimitNum == 0 || cameraMoveScr.int_CurLimitNum == 1 || tutorialManagerScr.events == TutorialEvents.TurnOnLights
            || tutorialManagerScr.events == TutorialEvents.GetItems || tutorialManagerScr.events == TutorialEvents.TalkToHyang)
        {
            TimeManager.instance.StopTime();
        }
    }

    private void FixedUpdate()
    {
        //��¥�� ��� ������ ���
        if(int_DayCount == 15)
        {
            if(!isEndingStart)
            {
                isEndingStart = true;
                //��忣�� ����
                DialogManager.instance.StartBadEndingSentence2();
            }
        }


        //float RealTime�� int�� ��ȯ�� �����Ӵ� ����
        int_RealTime = (int)MathF.Truncate(float_RealTime);

        //������Ʈ�� ��� ����
        if (!isSettingEnd)
        {
            ObjectStartRGB_Value(startRGBValue.r);
        }


        //������Ʈ ��� ����
        if (!isOneSecStart)
        {
            StartCoroutine(OneSecStartCoroutine());
        }


        //�ؽð� �ִϸ��̼�
        ChageSunClock();

        //2�ʿ� �ѹ� �� ��� ����
        if ((int)MathF.Truncate(float_RealTime) % 2 == 0)
        {
            //�� ��� �ٲٱ�
            moonlightScr.BrightenMoon((byte)MathF.Truncate(float_RealTime));

            //������Ʈ ��� �ٲٱ�
            //ObumbrateObject((int)MathF.Truncate(curObjectRGB);
        }

        //�÷��� Ÿ�� = ��Ÿ Ÿ��
        float_PlayTimeSec += Time.deltaTime;

        //Debug.Log(float_PlayTimeSec);

        if (!timeStop && !realTimeStop)
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
                if(int_DayCount == 2)
                {
                    StopTime();
                }
                
                //���� D-day�� �����ʾҴٸ�
                if(int_DayCount < 15)
                {
                    //�÷��̾� ��ġ ���� �� ���
                    tutorialManagerScr.PassDay();
                }

                //���� ���ʹ��� ���� ���¶��
                if (EventManager.instance.drinkHerb)
                {
                    //���ʹ� �÷��� �ʱ�ȭ

                    EventManager.instance.drinkHerb = false;

                    //�÷��̾� ���� �ʱ�ȭ
                    EventManager.instance.PlayerStateReset();
                }
            }


            #region �ָ� �̹�������

            //0 ~ 49��
            else if(int_RealTime >= 0 && int_RealTime <50)
            {
                if(spriteRen_Joomack.sprite != sprite_Joomacks[0])
                {
                    ChageJoomack(sprite_Joomacks[0]);
                }
            }

            //50 ~ 99��
            else if (int_RealTime >= 50 && int_RealTime < 100)
            {
                if (spriteRen_Joomack.sprite != sprite_Joomacks[1])
                {
                    ChageJoomack(sprite_Joomacks[1]);
                }
            }

            //100 ~ 149��
            else if (int_RealTime >= 100 && int_RealTime < 149)
            {
                if (spriteRen_Joomack.sprite != sprite_Joomacks[2])
                {
                    ChageJoomack(sprite_Joomacks[2]);
                }
            }

            //150 ~ 199��
            else if (int_RealTime >= 150 && int_RealTime < 199)
            {
                if (spriteRen_Joomack.sprite != sprite_Joomacks[3])
                {
                    ChageJoomack(sprite_Joomacks[3]);
                }
            }

            //200 ~ 250��
            else if (int_RealTime >= 200 && int_RealTime < 250)
            {
                if (spriteRen_Joomack.sprite != sprite_Joomacks[4])
                {
                    ChageJoomack(sprite_Joomacks[4]);
                }
            }

            //250 ~ 299��
            else if (int_RealTime >= 250 && int_RealTime < 300)
            {
                if (spriteRen_Joomack.sprite != sprite_Joomacks[5])
                {
                    ChageJoomack(sprite_Joomacks[5]);
                }
            }

            #endregion


            
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
        isTimeStop = true;
        //�ð� ���߱�
        timeStop = true;
    }

    //�ð� ��Ӱ���
    public void ContinueTime()
    {
        isTimeStop = false;

        //�ð� �帣��
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
        curTimeSaveData = new TimeSaveData(float_RealTime, int_DayCount, curObjectRGB.r, realTimeStop);

        //���̺� ������
        string jSaveData = JsonUtility.ToJson(curTimeSaveData);

        //������ ���� ����
        File.WriteAllText(saveFilePath + _slotNum.ToString(), jSaveData);
    }

    //������ �ε�
    public void Load(int _SlotNum)
    {
        Debug.Log("Load TimeManagerData");
        Debug.Log(_SlotNum);

        if(_SlotNum <= 2)
        {
            //���̺� ���� �о����
            string jLoadData = File.ReadAllText(saveFilePath + _SlotNum.ToString());

            //�о�� ���� ����Ʈ�� ����
            curTimeLoadData = JsonUtility.FromJson<TimeLoadData>(jLoadData);

            //�ð� �缳��
            float_RealTime = curTimeLoadData.time;

            //��¥ �缳��
            int_DayCount = curTimeLoadData.day;

            //�ð� Text ����
            text_TimeText.text = float_RealTime.ToString("F0");

            //�ð� ���� ����
            realTimeStop = curTimeLoadData.realTimeStop;

            //�÷��� �缳��
            curObjectRGB = new Color32(curTimeLoadData.curColorValue, curTimeLoadData.curColorValue,
                curTimeLoadData.curColorValue, 255);

            for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
            {
                //���� ���� �ٲ� ������Ʈ�� �����Ѵٸ�
                if (spriteRen_ObumbrateObj[i] != null)
                {
                    //���� ����
                    spriteRen_ObumbrateObj[i].color = curObjectRGB;
                }
            }

            //Ķ���� ��¥ ����
            NextDayAnimaton(int_DayCount);
        }
        
    }

    //�Ѱ��� ���� ��¥
    public int GetDay()
    {
        return int_DayCount;
    }

    //UI ���Կ� ǥ���� ��¥
    public string GetDayCountText()
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

    //������Ʈ�� ���� RGB��
    public void ObjectStartRGB_Value(int _rgb)
    {
        //���� RGB������ ����
        for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
        {
            //���� ���� �ٲ� ������Ʈ�� �����Ѵٸ�
            if (spriteRen_ObumbrateObj[i] != null)
            {
                spriteRen_ObumbrateObj[i].color = new Color32((byte)_rgb, (byte)_rgb, (byte)_rgb, 255);
            }
        }
        isSettingEnd = true;
    }


    //������Ʈ ��Ӱ��ϱ�
    public void ObumbrateObject(Color32 _curObjectColor)
    {

        //��ħ�� �����Ǿ��ϴ� �� = (255 - ���� RGB��) / ��ħ�ð�
        int pluseValue_moring = (int)MathF.Truncate((255 - startRGBValue.r) / 35);

        //�㿡 �پ�����ϴ� �� = (255 - �� RGB��) / �� �ð�
        int minusValeue_night = (int)MathF.Truncate((255 - nightRGBValue.r) / 60);

        ////���� �������� �Ǿ������
        //if ((int)MathF.Truncate(float_RealTime) == 0)
        //{
        //    Debug.Log("0��");
        //    for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
        //    {
        //        //���� ���� �ٲ� ������Ʈ�� �����Ѵٸ�
        //        if (spriteRen_ObumbrateObj[i] != null)
        //        {
        //            //���� ���� (���� ��)
        //            spriteRen_ObumbrateObj[i].color = startRGBValue;
        //        }
        //    }
        //}

        //���� ��ħ�� ���(0 ~ 35��) = �ʴ� 3�� �����
        if ((int)MathF.Truncate(float_RealTime) > 0 && (int)MathF.Truncate(float_RealTime) <= 35)
        {
            Debug.Log("��ħ");

            for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
            {
                //���� ���� �ٲ� ������Ʈ�� �����Ѵٸ�
                if (spriteRen_ObumbrateObj[i] != null)
                {
                    //���� ����
                    spriteRen_ObumbrateObj[i].color = new Color32((byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), 255);
                }
            }

            //���� ������Ʈ RGB�� ����
            curObjectRGB = new Color32((byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), 255);
        }

        //���� ���� �Ǿ��� ��� (35 ~ 150��)
        else if ((int)MathF.Truncate(float_RealTime) >= 35 && (int)MathF.Truncate(float_RealTime) <= 150)
        {
            Debug.Log("��");

            if (curObjectRGB.r < 255)
            {
                if (curObjectRGB.r < 250)
                {
                    for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
                    {
                        //���� ���� �ٲ� ������Ʈ�� �����Ѵٸ�
                        if (spriteRen_ObumbrateObj[i] != null)
                        {
                            //���� ����
                            spriteRen_ObumbrateObj[i].color = new Color32((byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), 255);
                        }
                    }

                    //���� ������Ʈ RGB�� ����
                    curObjectRGB = new Color32((byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), 255);
                }

                else if (curObjectRGB.r > 250)
                {
                    for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
                    {
                        //���� ���� �ٲ� ������Ʈ�� �����Ѵٸ�
                        if (spriteRen_ObumbrateObj[i] != null)
                        {
                            //���� ����
                            spriteRen_ObumbrateObj[i].color = new Color32(255, 255, 255, 255);
                        }
                    }

                    //���� ������Ʈ RGB�� ����
                    curObjectRGB = new Color32((byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), (byte)(_curObjectColor.r + pluseValue_moring), 255);
                }

            }
        }


        // ���� ���� �Ǿ��� ��� (150 ~ 210)
        else if ((int)MathF.Truncate(float_RealTime) > 150 && (int)MathF.Truncate(float_RealTime) <= 210)
        {
            Debug.Log("��");

            //���� ���� ��Ⱑ ���� ��⺸�� ��ٸ�
            if (curObjectRGB.r >= nightRGBValue.r)
            {
                for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
                {
                    //���� ���� �ٲ� ������Ʈ�� �����Ѵٸ�
                    if (spriteRen_ObumbrateObj[i] != null)
                    {
                        //���� ����
                        spriteRen_ObumbrateObj[i].color = new Color32((byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), 255);
                    }
                }

                //���� ������Ʈ RGB�� ����
                curObjectRGB = new Color32((byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), 255);
            }   
        }

        //�߰� (210 ~ 300)
        else if(((int)MathF.Truncate(float_RealTime) > 210 && (int)MathF.Truncate(float_RealTime) <= 300))
        {
            Debug.Log("�ѹ���");

            //���� ���� ��Ⱑ ���� ��⺸�� ��ٸ�
            if (curObjectRGB.r >= nightRGBValue.r)
            {
                for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
                {
                    //���� ���� �ٲ� ������Ʈ�� �����Ѵٸ�
                    if(spriteRen_ObumbrateObj[i] != null )
                    {
                        //���� ����
                        spriteRen_ObumbrateObj[i].color = new Color32((byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), 255);
                    }
                }

                //���� ������Ʈ RGB�� ����
                curObjectRGB = new Color32((byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), (byte)(_curObjectColor.r - minusValeue_night), 255);
            }
        }
    }


    //1�ʿ� �ѹ� �����ϴ� �ڷ�ƾ
    IEnumerator OneSecStartCoroutine()
    {
        isOneSecStart = true;
        yield return new WaitForSeconds(1f);

        Debug.Log("��� ��� ����");

        if(!timeStop)
        {
            //������Ʈ ��� �ٲٱ�
            ObumbrateObject(curObjectRGB);
        }

        isOneSecStart = false;
    }


    //�ָ� �̹��� ����
    public void ChageJoomack(Sprite _sprite)
    {
        spriteRen_Joomack.sprite = _sprite;
    }

    //��� �÷� �ʱⰪ���� ����
    public void ResetBGColor()
    {
        for (int i = 0; i < spriteRen_ObumbrateObj.Length; i++)
        {
            //���� ���� �ٲ� ������Ʈ�� �����Ѵٸ�
            if (spriteRen_ObumbrateObj[i] != null)
            {
                //���� ���� (���� ��)
                spriteRen_ObumbrateObj[i].color = startRGBValue;
            }
        }
    }

    //�ð� ��¥ ����
    public void RealTimeStop()
    {
        realTimeStop = true;
    }
    
}
