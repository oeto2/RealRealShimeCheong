using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    //Effect Audio Source
    public AudioSource auidoSource;

    //瘤档 祁摹绰 家府
    public AudioClip clip_MapOpen;

    //嚎咙 祁摹绰 家府
    public AudioClip clip_BotzimeOpen;

    //厘累 持绰 家府
    public AudioClip clip_JangJackPush;

    //可记芒 难绰家府
    public AudioClip clip_OptionOn;

    //教臂沛
    public static EffectSoundManager instance = null;
    
    private void Awake()
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
                Destroy(gameObject);
            }
        }
    }

    //Play Open Map Sound
    public void PlayOpenMapSound()
    {
        Debug.Log("瘤档 祁摹绰 家府 犁积");
        auidoSource.PlayOneShot(clip_MapOpen);
    }

    //Play Open Botzime Sound
    public void PlayOpenBotzimeSound()
    {
        Debug.Log("嚎咙 祁摹绰 家府 犁积");
        auidoSource.PlayOneShot(clip_BotzimeOpen);
    }

    //Play Open OptionWindow Sound
    public void PlayOnOptionSound()
    {
        Debug.Log("可记芒 家府 犁积");
        auidoSource.PlayOneShot(clip_OptionOn);
    }

    //Play Push Jangjack Sound
    public void PlayPushJangJackSound()
    {
        Debug.Log("厘累 持绰 家府 犁积");
        auidoSource.PlayOneShot(clip_JangJackPush);
    }

    //Stop Music
    public void StopMusic()
    {
        auidoSource.mute = true;
    }

    //Start Music
    public void StartMusic()
    {
        auidoSource.mute = false;
    }
}
