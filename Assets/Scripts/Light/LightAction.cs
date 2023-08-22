using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightAction : MonoBehaviour
{
    //Light 스크립트
    public Light2D light2d;

    //광원의 최대 크기
    [Range(0, 10f)] public float maxRadius;

    //광원의 최소 크기
    [Range(0, 10f)] public float minRadius;

    //광원 크기 변경 속도
    [Range(0, 100)] public float speed;

    //광원의 크기가 최대값에 도달했는지
    public bool isMax;

    private void Awake()
    {
        light2d = GetComponent<Light2D>();
    }

    private void Update()
    {
        //광원 크기 변경 실행
        StartChangeLightRadius();
    }

    public void StartChangeLightRadius()
    {
        //Debug.Log(light2d.pointLightOuterRadius);

        //광원의 크기가 최대 값이 아니라면
        if(light2d.pointLightOuterRadius < maxRadius && !isMax)
        {
            //광원 크기 키우기
            light2d.pointLightOuterRadius = light2d.pointLightOuterRadius + Time.deltaTime * speed;
        }

        //광원의 크기가 최대크기에 도달 했을경우
        if(light2d.pointLightOuterRadius > maxRadius)
        {
            isMax = true;
        }

        //광원의 크기가 최소크기에 도달 했을경우
        if(light2d.pointLightOuterRadius < minRadius)
        {
            isMax = false;
        }
        
        //광원의 크기가 최대로 커졌다면
        if(isMax)
        {
            //광원의 크기 줄이기
            light2d.pointLightOuterRadius = light2d.pointLightOuterRadius - Time.deltaTime * speed;
        }
    }
}
