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

    //���̾�α� ȿ����
    public AudioClip clip_TalkText;

    //�ٶ��Ҹ� ȿ����
    public AudioClip clip_WindSound;

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
        //Debug.Log("���� ��ġ�� �Ҹ� ���");
        auidoSource.PlayOneShot(clip_MapOpen);
    }

    //Play Open Botzime Sound
    public void PlayOpenBotzimeSound()
    {
        //Debug.Log("���� ��ġ�� �Ҹ� ���");
        auidoSource.PlayOneShot(clip_BotzimeOpen);
    }

    //Play Open OptionWindow Sound
    public void PlayOnOptionSound()
    {
        //Debug.Log("�ɼ�â �Ҹ� ���");
        auidoSource.PlayOneShot(clip_OptionOn);
    }

    //Play Push Jangjack Sound
    public void PlayPushJangJackSound()
    {
        //Debug.Log("���� �ִ� �Ҹ� ���");
        auidoSource.PlayOneShot(clip_JangJackPush);
    }

    //Play Talk Sound
    public void PlayTalkTextSound()
    {
        //Debug.Log("���̾�α� �ؽ�Ʈ ���� ���");
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
