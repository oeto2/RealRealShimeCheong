using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class PlayerUICanvas : MonoBehaviour
{
    //바람 애니메이션 오브젝트
    [SerializeField] private GameObject WindImage_Object;

    //바람이 부는 시간
    [SerializeField] float windStartTime = 0;

    //바람이 불었는지 체크
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

    //바람이 언제 불지 세팅
    public void WindSetting()
    {
        windStartTime = Random.Range(0, 300);
    }

    public void WindTrigger()
    {
        //현재 시간이 바람 발생 시간이라면
        if (!isStartWind && (TimeManager.instance.Float_RealTime >= windStartTime))
        {
            isStartWind = true;

            //바람 애니메이션 시작
            PlayWindAnimation();

            WindSetting();

            Debug.Log("바람 부는 트리거 시작");
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
