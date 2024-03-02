using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public CameraMove cameraScr;

    //Bgm Audio Source
    public AudioSource audioSource;

    //�⺻ ����
    public AudioClip clip_Nomal;

    //���� ����
    public AudioClip clip_Market;

    //�̱���
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
            //�� ��
            case 0:
                break;

            //�ξ�
            case 1:
                break;

            //����
            case 2:
                //�⺻ ���� ���
                PlayNomalBGM();
                break;

            //����
            case 3:
                //�⺻ ���� ���
                PlayNomalBGM();
                break;

            //����
            case 4:
                //���� ���� ���
                PlayMartketBGM();
                break;

            //����
            case 5:
                //�⺻ ���� ���
                PlayNomalBGM();
                break;

            //�ٴ�
            case 6:
                //�⺻ ���� ���
                PlayNomalBGM();
                break;
        }
    }

    //Play NomalBGM
    public void PlayNomalBGM()
    {

        //���� ������� ������ ���ٸ�
        if (audioSource.clip == null)
        {
            Debug.Log("�⺻ ���� ���");

            //�������
            audioSource.clip = clip_Nomal;
            audioSource.Play();
        }

        else
        {
            //���� ���� ������ ��������� �ʴٸ�
            if (audioSource.clip.name != clip_Nomal.name)
            {
                Debug.Log("�⺻ ���� ���");

                //�������
                audioSource.clip = clip_Nomal;
                audioSource.Play();
            }
        }
    }


    //Play MarketBgm
    public void PlayMartketBGM()
    {
        //���� ������� ������ ���ٸ�
        if (audioSource.clip == null)
        {
            Debug.Log("���� ���� ���");

            //�������
            audioSource.clip = clip_Market;
            audioSource.Play();
        }
        else
        {
            //���� ���� ������ ��������� �ʴٸ�
            if (audioSource.clip.name != clip_Market.name)
            {
                Debug.Log("���� ���� ���");

                //�������
                audioSource.clip = clip_Market;
                audioSource.Play();
            }
        }
    }

    //���ǼҸ� ����
    public void StopMusic()
    {
        audioSource.mute = true;
    }

    //���ǼҸ� �ѱ�
    public void StartMusic()
    {
        audioSource.mute = false;
    }
}
