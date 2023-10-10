using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    //Effect Audio Source
    public AudioSource auidoSource;

    //Áöµµ ÆîÄ¡´Â ¼Ò¸®
    public AudioClip clip_MapOpen;

    //º¿Áü ÆîÄ¡´Â ¼Ò¸®
    public AudioClip clip_BotzimeOpen;

    //Play Open Map Sound
    public void PlayOpenMapSound()
    {
        Debug.Log("Áöµµ ÆîÄ¡´Â ¼Ò¸® Àç»ý");
        auidoSource.PlayOneShot(clip_MapOpen);
    }

    //Play Open Botzime Sound
    public void PlayOpenBotzimeSound()
    {
        Debug.Log("º¿Áü ÆîÄ¡´Â ¼Ò¸® Àç»ý");
        auidoSource.PlayOneShot(clip_BotzimeOpen);
    }
}
