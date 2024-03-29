using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    //외부 스크립트 참조
    public CameraMove cameraScr;

    //Bgm Audio Source
    public AudioSource audioSource;

    //기본 음악
    public AudioClip clip_Nomal;

    //장터 음악
    public AudioClip clip_Market;

    //싱글톤
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
            //집 안
            case 0:
                break;

            //부엌
            case 1:
                break;

            //마당
            case 2:
                //기본 음악 재생
                PlayNomalBGM();
                break;

            //마을
            case 3:
                //기본 음악 재생
                PlayNomalBGM();
                break;

            //시장
            case 4:
                //장터 음악 재생
                PlayMartketBGM();
                break;

            //개울
            case 5:
                //기본 음악 재생
                PlayNomalBGM();
                break;

            //바다
            case 6:
                //기본 음악 재생
                PlayNomalBGM();
                break;
        }
    }

    //Play NomalBGM
    public void PlayNomalBGM()
    {

        //현재 재생중인 음악이 없다면
        if (audioSource.clip == null)
        {
            Debug.Log("기본 음악 재생");

            //음악재생
            audioSource.clip = clip_Nomal;
            audioSource.Play();
        }

        else
        {
            //현재 같은 음악이 재생중이지 않다면
            if (audioSource.clip.name != clip_Nomal.name)
            {
                Debug.Log("기본 음악 재생");

                //음악재생
                audioSource.clip = clip_Nomal;
                audioSource.Play();
            }
        }
    }


    //Play MarketBgm
    public void PlayMartketBGM()
    {
        //현재 재생중인 음악이 없다면
        if (audioSource.clip == null)
        {
            Debug.Log("마켓 음악 재생");

            //음악재생
            audioSource.clip = clip_Market;
            audioSource.Play();
        }
        else
        {
            //현재 같은 음악이 재생중이지 않다면
            if (audioSource.clip.name != clip_Market.name)
            {
                Debug.Log("마켓 음악 재생");

                //음악재생
                audioSource.clip = clip_Market;
                audioSource.Play();
            }
        }
    }

    //음악소리 끄기
    public void StopMusic()
    {
        audioSource.mute = true;
    }

    //음악소리 켜기
    public void StartMusic()
    {
        audioSource.mute = false;
    }
}
