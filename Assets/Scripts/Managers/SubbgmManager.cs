using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubbgmManager : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public CameraMove cameraScr;

    //Bgm Audio Source
    public AudioSource auidoSource;

    //���� ����
    public AudioClip clip_Brook;

    //�̱���
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
            //�� ��
            case 0:
                StopBGM();
                break;

            //�ξ�
            case 1:
                StopBGM();
                break;

            //����
            case 2:
                StopBGM();
                break;

            //����
            case 3:
                StopBGM();
                break;

            //����
            case 4:
                StopBGM();
                break;

            //����
            case 5:
                PlayBrookBGM();
                break;

            //�ٴ�
            case 6:
                StopBGM();
                break;
        }
    }

    //Play BrookBGM
    public void PlayBrookBGM()
    {
        //���� ������� ������ ���ٸ�
        if (auidoSource.clip == null)
        {
            Debug.Log("���� ���� ���");

            //�������
            auidoSource.clip = clip_Brook;
            auidoSource.Play();
        }

        else
        {
            //���� ���� ������ ��������� �ʴٸ�
            if (auidoSource.clip.name != clip_Brook.name)
            {
                Debug.Log("���� ���� ���");

                //�������
                auidoSource.clip = clip_Brook;
                auidoSource.Play();
            }
        }
    }

    //Stop Play
    public void StopBGM()
    {
        //Ŭ�� ����
        auidoSource.clip = null;

        //���� ����
        auidoSource.Stop();
    }
  
}
