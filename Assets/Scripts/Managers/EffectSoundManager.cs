using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    //Effect Audio Source
    public AudioSource auidoSource;

    //지도 펼치는 소리
    public AudioClip clip_MapOpen;


    //Play Open Map Sound
    public void PlayOpenMapSound()
    {
        Debug.Log("지도 펼치는 소리 재생");
        auidoSource.PlayOneShot(clip_MapOpen);
    }
}
