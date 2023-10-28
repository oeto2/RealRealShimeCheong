using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    //Effect Audio Source
    public AudioSource auidoSource;

    //���� ��ġ�� �Ҹ�
    public AudioClip clip_MapOpen;

    //���� ��ġ�� �Ҹ�
    public AudioClip clip_BotzimeOpen;

    //���� �ִ� �Ҹ�
    public AudioClip clip_JangJackPush;

    //�ɼ�â �Ѵ¼Ҹ�
    public AudioClip clip_OptionOn;

    //�̱���
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
        Debug.Log("���� ��ġ�� �Ҹ� ���");
        auidoSource.PlayOneShot(clip_MapOpen);
    }

    //Play Open Botzime Sound
    public void PlayOpenBotzimeSound()
    {
        Debug.Log("���� ��ġ�� �Ҹ� ���");
        auidoSource.PlayOneShot(clip_BotzimeOpen);
    }

    //Play Open OptionWindow Sound
    public void PlayOnOptionSound()
    {
        Debug.Log("�ɼ�â �Ҹ� ���");
        auidoSource.PlayOneShot(clip_OptionOn);
    }

    //Play Push Jangjack Sound
    public void PlayPushJangJackSound()
    {
        Debug.Log("���� �ִ� �Ҹ� ���");
        auidoSource.PlayOneShot(clip_JangJackPush);
    }
}
