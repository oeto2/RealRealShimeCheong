using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class PlayerUICanvas : MonoBehaviour
{
    //�ٶ� �ִϸ��̼� ������Ʈ
    [SerializeField] private GameObject WindImage_Object;

    //�ٶ��� �δ� �ð�
    [SerializeField] float windStartTime = 0;

    //�ٶ��� �Ҿ����� üũ
    [SerializeField] bool isStartWind = false;

    private void Awake()
    {
        WindSetting();
    }

    private void Start()
    {
        TimeManager.instance.NextDayEvent += ResetIsStartWind;
    }

    private void Update()
    {
        WindTrigger();
    }

    //�ٶ��� ���� ���� ����
    public void WindSetting()
    {
        windStartTime = Random.Range(0, 300);
    }

    public void WindTrigger()
    {
        //���� �ð��� �ٶ� �߻� �ð��̶��
        if (!isStartWind && (TimeManager.instance.Float_RealTime >= windStartTime))
        {
            isStartWind = true;

            //�ٶ� �ִϸ��̼� ����
            PlayWindAnimation();

            WindSetting();

            Debug.Log("�ٶ� �δ� Ʈ���� ����");
        }
    }

    public void PlayWindAnimation()
    {
        WindImage_Object.SetActive(true);
    }

    public void ResetIsStartWind()
    {
        isStartWind = false;
    }
}
