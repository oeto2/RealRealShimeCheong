using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public CameraMove cameraScr;

    //Bgm Audio Source
    public AudioSource auidoSource;

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
        if (auidoSource.clip == null)
        {
            Debug.Log("�⺻ ���� ���");

            //�������
            auidoSource.clip = clip_Nomal;
            auidoSource.Play();
        }

        else
        {
            //���� ���� ������ ��������� �ʴٸ�
            if (auidoSource.clip.name != clip_Nomal.name)
            {
                Debug.Log("�⺻ ���� ���");

                //�������
                auidoSource.clip = clip_Nomal;
                auidoSource.Play();
            }
        }
    }


    //Play MarketBgm
    public void PlayMartketBGM()
    {

        //���� ������� ������ ���ٸ�
        if (auidoSource.clip == null)
        {
            Debug.Log("���� ���� ���");

            //�������
            auidoSource.clip = clip_Market;
            auidoSource.Play();
        }
        else
        {
            //���� ���� ������ ��������� �ʴٸ�
            if (auidoSource.clip.name != clip_Market.name)
            {
                Debug.Log("���� ���� ���");
                //�������
                //�������
                auidoSource.clip = clip_Market;
                auidoSource.Play();
            }
        }
    }
}
