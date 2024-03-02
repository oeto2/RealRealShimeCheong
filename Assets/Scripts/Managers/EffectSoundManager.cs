using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    //Effect Audio Source
    public AudioSource auidoSource;

    //지도 펼치는 소리
    public AudioClip clip_MapOpen;

    //봇짐 펼치는 소리
    public AudioClip clip_BotzimeOpen;

    //장작 넣는 소리
    public AudioClip clip_JangJackPush;

    //옵션창 켜는소리
    public AudioClip clip_OptionOn;

    //다이얼로그 효과음
    public AudioClip clip_TalkText;

    //바람소리 효과음
    public AudioClip clip_WindSound;

    //싱글톤
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
        //Debug.Log("지도 펼치는 소리 재생");
        auidoSource.PlayOneShot(clip_MapOpen);
    }

    //Play Open Botzime Sound
    public void PlayOpenBotzimeSound()
    {
        //Debug.Log("봇짐 펼치는 소리 재생");
        auidoSource.PlayOneShot(clip_BotzimeOpen);
    }

    //Play Open OptionWindow Sound
    public void PlayOnOptionSound()
    {
        //Debug.Log("옵션창 소리 재생");
        auidoSource.PlayOneShot(clip_OptionOn);
    }

    //Play Push Jangjack Sound
    public void PlayPushJangJackSound()
    {
        //Debug.Log("장작 넣는 소리 재생");
        auidoSource.PlayOneShot(clip_JangJackPush);
    }

    //Play Talk Sound
    public void PlayTalkTextSound()
    {
        //Debug.Log("다이얼로그 텍스트 사운드 재생");
        auidoSource.PlayOneShot(clip_TalkText);
    }

    //Play Wind Sound
    public void PlayWindSound()
    {
        auidoSource.PlayOneShot(clip_WindSound);
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
