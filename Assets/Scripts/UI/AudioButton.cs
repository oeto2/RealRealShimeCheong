using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    //����� ��������Ʈ �̹�����
    public Sprite[] sprite_AudioButtons;

    //��� ���� ���Ұ� �̹���
    public Image image_BG_Audio;

    //ȿ���� ���� ���Ұ� �̹���
    public Image image_EF_Audio;

    //���� ��� ���� ���Ұ� ������
    public bool isMuteBG_Sound;

    //���� ȿ���� ���� ���Ұ� ������
    public bool isMuteEF_Sound;

    //������� ���� �ؽ�Ʈ
    public Text text_BGValume;

    //������� ������
    public Slider slider_BGValume;

    //ȿ���� ���� �ؽ�Ʈ
    public Text text_EFValume;

    //ȿ������ ������
    public Slider slider_EFValume;

    private void Update()
    {
        //��� ���� �ؽ�Ʈ = �����̴� value��
        text_BGValume.text = (slider_BGValume.value * 100).ToString("0");
        //��� ���� �� = �����̴� value��
        BgmManager.instance.audioSource.volume = slider_BGValume.value;

        //ȿ���� ���� �ؽ�Ʈ = �����̴� value��
        text_EFValume.text = (slider_EFValume.value * 100).ToString("0");
        //ȿ���� ���� �� = �����̴� value��
        EffectSoundManager.instance.auidoSource.volume = slider_EFValume.value; ;
    }


    //��� ���Ұ� ��ư Ŭ�� ��
    public void BG_ButtonClick()
    {
        //���Ұ� ������ �ʴٸ�
        if(!isMuteBG_Sound)
        {
            //�̹��� ���� (���Ұ� �̹���)
            image_BG_Audio.sprite = sprite_AudioButtons[1];

            //������� ����
            BgmManager.instance.StopMusic();

            isMuteBG_Sound = true;
        }

        //���Ұ� ���̶��
        else
        {
            //�̹��� ���� (�⺻ �̹���)
            image_BG_Audio.sprite = sprite_AudioButtons[0];

            //������� �ѱ�
            BgmManager.instance.StartMusic();

            isMuteBG_Sound = false;
        }
    }

    //����Ʈ ���Ұ� ��ư Ŭ�� ��
    public void EF_ButtonClick()
    {
        //���Ұ� ������ �ʴٸ�
        if (!isMuteEF_Sound)
        {
            //�̹��� ���� (���Ұ� �̹���)
            image_EF_Audio.sprite = sprite_AudioButtons[1];

            //ȿ���� ���� ����
            EffectSoundManager.instance.StopMusic();

            isMuteEF_Sound = true;
        }
        //���Ұ� ���̶��
        else
        {
            //�̹��� ���� (�⺻ �̹���)
            image_EF_Audio.sprite = sprite_AudioButtons[0];

            //ȿ���� �ѱ�
            EffectSoundManager.instance.StartMusic();

            isMuteEF_Sound = false;
        }
    }
}
