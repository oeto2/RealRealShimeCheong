using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    //¿ÜºÎ ½ºÅ©¸³Æ® ÂüÁ¶
    public CameraMove cameraScr;

    //Bgm Audio Source
    public AudioSource audioSource;

    //±âº» À½¾Ç
    public AudioClip clip_Nomal;

    //ÀåÅÍ À½¾Ç
    public AudioClip clip_Market;

    //½Ì±ÛÅæ
    public static BgmManager instance = null;


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
                Destroy(this.gameObject);
            }
        }
    }

    private void Start()
    {
        DataManager.instance.LoadEvent += ChangeBGM;
    }

    //Change BGM
    public void ChangeBGM()
    {
        switch (cameraScr.int_CurLimitNum)
        {
            //Áý ¾È
            case 0:
                break;

            //ºÎ¾ý
            case 1:
                break;

            //¸¶´ç
            case 2:
                //±âº» À½¾Ç Àç»ý
                PlayNomalBGM();
                break;

            //¸¶À»
            case 3:
                //±âº» À½¾Ç Àç»ý
                PlayNomalBGM();
                break;

            //½ÃÀå
            case 4:
                //ÀåÅÍ À½¾Ç Àç»ý
                PlayMartketBGM();
                break;

            //°³¿ï
            case 5:
                //±âº» À½¾Ç Àç»ý
                PlayNomalBGM();
                break;

            //¹Ù´Ù
            case 6:
                //±âº» À½¾Ç Àç»ý
                PlayNomalBGM();
                break;
        }
    }

    //Play NomalBGM
    public void PlayNomalBGM()
    {

        //ÇöÀç Àç»ýÁßÀÎ À½¾ÇÀÌ ¾ø´Ù¸é
        if (audioSource.clip == null)
        {
            Debug.Log("±âº» À½¾Ç Àç»ý");

            //À½¾ÇÀç»ý
            audioSource.clip = clip_Nomal;
            audioSource.Play();
        }

        else
        {
            //ÇöÀç °°Àº À½¾ÇÀÌ Àç»ýÁßÀÌÁö ¾Ê´Ù¸é
            if (audioSource.clip.name != clip_Nomal.name)
            {
                Debug.Log("±âº» À½¾Ç Àç»ý");

                //À½¾ÇÀç»ý
                audioSource.clip = clip_Nomal;
                audioSource.Play();
            }
        }
    }


    //Play MarketBgm
    public void PlayMartketBGM()
    {
        //ÇöÀç Àç»ýÁßÀÎ À½¾ÇÀÌ ¾ø´Ù¸é
        if (audioSource.clip == null)
        {
            Debug.Log("¸¶ÄÏ À½¾Ç Àç»ý");

            //À½¾ÇÀç»ý
            audioSource.clip = clip_Market;
            audioSource.Play();
        }
        else
        {
            //ÇöÀç °°Àº À½¾ÇÀÌ Àç»ýÁßÀÌÁö ¾Ê´Ù¸é
            if (audioSource.clip.name != clip_Market.name)
            {
                Debug.Log("¸¶ÄÏ À½¾Ç Àç»ý");

                //À½¾ÇÀç»ý
                audioSource.clip = clip_Market;
                audioSource.Play();
            }
        }
    }

    //À½¾Ç¼Ò¸® ²ô±â
    public void StopMusic()
    {
        audioSource.mute = true;
    }

    //À½¾Ç¼Ò¸® ÄÑ±â
    public void StartMusic()
    {
        audioSource.mute = false;
    }
}
