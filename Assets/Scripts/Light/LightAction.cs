using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightAction : MonoBehaviour
{
    //Light ��ũ��Ʈ
    public Light2D light2d;

    //������ �ִ� ũ��
    [Range(0, 10f)] public float maxRadius;

    //������ �ּ� ũ��
    [Range(0, 10f)] public float minRadius;

    //���� ũ�� ���� �ӵ�
    [Range(0, 100)] public float speed;

    //������ ũ�Ⱑ �ִ밪�� �����ߴ���
    public bool isMax;

    private void Awake()
    {
        light2d = GetComponent<Light2D>();
    }

    private void Update()
    {
        //���� ũ�� ���� ����
        StartChangeLightRadius();
    }

    public void StartChangeLightRadius()
    {
        //Debug.Log(light2d.pointLightOuterRadius);

        //������ ũ�Ⱑ �ִ� ���� �ƴ϶��
        if(light2d.pointLightOuterRadius < maxRadius && !isMax)
        {
            //���� ũ�� Ű���
            light2d.pointLightOuterRadius = light2d.pointLightOuterRadius + Time.deltaTime * speed;
        }

        //������ ũ�Ⱑ �ִ�ũ�⿡ ���� �������
        if(light2d.pointLightOuterRadius > maxRadius)
        {
            isMax = true;
        }

        //������ ũ�Ⱑ �ּ�ũ�⿡ ���� �������
        if(light2d.pointLightOuterRadius < minRadius)
        {
            isMax = false;
        }
        
        //������ ũ�Ⱑ �ִ�� Ŀ���ٸ�
        if(isMax)
        {
            //������ ũ�� ���̱�
            light2d.pointLightOuterRadius = light2d.pointLightOuterRadius - Time.deltaTime * speed;
        }
    }
}
