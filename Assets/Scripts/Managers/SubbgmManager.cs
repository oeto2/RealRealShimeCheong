using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubbgmManager : MonoBehaviour
{
    //외부 스크립트 참조
    public CameraMove cameraScr;

    //Bgm Audio Source
    public AudioSource auidoSource;

    //개울 음악
    public AudioClip clip_Brook;

    //싱글톤
    public static SubbgmManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    //Change BGM
    public void ChangeBGM()
    {
        switch (cameraScr.int_CurLimitNum)
        {
            //집 안
            case 0:
                StopBGM();
                break;

            //부엌
            case 1:
                StopBGM();
                break;

            //마당
            case 2:
                StopBGM();
                break;

            //마을
            case 3:
                StopBGM();
                break;

            //시장
            case 4:
                StopBGM();
                break;

            //개울
            case 5:
                PlayBrookBGM();
                break;

            //바다
            case 6:
                StopBGM();
                break;
        }
    }

    //Play BrookBGM
    public void PlayBrookBGM()
    {
        //현재 재생중인 음악이 없다면
        if (auidoSource.clip == null)
        {
            Debug.Log("개울 음악 재생");

            //음악재생
            auidoSource.clip = clip_Brook;
            auidoSource.Play();
        }

        else
        {
            //현재 같은 음악이 재생중이지 않다면
            if (auidoSource.clip.name != clip_Brook.name)
            {
                Debug.Log("개울 음악 재생");

                //음악재생
                auidoSource.clip = clip_Brook;
                auidoSource.Play();
            }
        }
    }

    //Stop Play
    public void StopBGM()
    {
        //클립 비우기
        auidoSource.clip = null;

        //음악 정지
        auidoSource.Stop();
    }
  
}
