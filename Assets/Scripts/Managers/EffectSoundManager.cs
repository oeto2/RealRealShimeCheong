using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    //Effect Audio Source
    public AudioSource auidoSource;

    //���� ��ġ�� �Ҹ�
    public AudioClip clip_MapOpen;


    //Play Open Map Sound
    public void PlayOpenMapSound()
    {
        Debug.Log("���� ��ġ�� �Ҹ� ���");
        auidoSource.PlayOneShot(clip_MapOpen);
    }
}
