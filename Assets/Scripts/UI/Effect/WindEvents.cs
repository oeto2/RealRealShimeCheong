using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEvents : MonoBehaviour
{
    public void PlayeWindSound()
    {
        EffectSoundManager.instance.PlayWindSound();
    }
    public void EndWindAnimation()
    {
        gameObject.SetActive(false);
    }
}
