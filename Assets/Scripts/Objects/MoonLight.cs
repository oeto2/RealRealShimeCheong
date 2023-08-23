using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonLight : MonoBehaviour
{
    //달의 스프라이트렌더
    public SpriteRenderer spriteRen_Moon;

    //달 투명도 변경
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
