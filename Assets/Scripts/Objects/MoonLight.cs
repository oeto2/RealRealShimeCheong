using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonLight : MonoBehaviour
{
    //���� ��������Ʈ����
    public SpriteRenderer spriteRen_Moon;

    //�� ���� ����
    public void BrightenMoon(byte _brightness)
    {
        //Debug.Log($"_brightness : {_brightness}");

        if(_brightness <= 255)
        {
            spriteRen_Moon.color = new Color32(255, 255, 255, _brightness);
        }
        else if (_brightness > 255)
        {
            spriteRen_Moon.color = new Color32(255, 255, 255, 255);
        }
    }
}
