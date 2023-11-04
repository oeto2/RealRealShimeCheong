using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    //오디오 스프라이트 이미지들
    public Sprite[] sprite_AudioButtons;

    //배경 사운드 음소거 이미지
    public Image image_BG_Audio;

    //효과음 사운드 음소거 이미지
    public Image image_EF_Audio;

    //현재 배경 음악 음소거 중인지
    public bool isMuteBG_Sound;

    //현재 효과음 음악 음소거 중인지
    public bool isMuteEF_Sound;

    //배경음악 볼륨 텍스트
    public Text text_BGValume;

    //배경음악 조절바
    public Slider slider_BGValume;

    //효과음 볼륨 텍스트
    public Text text_EFValume;

    //효과음악 조절바
    public Slider slider_EFValume;

    private void Update()
    {
        //배경 볼륨 텍스트 = 슬라이더 value값
        text_BGValume.text = (slider_BGValume.value * 100).ToString("0");
        //배경 볼륨 값 = 슬라이더 value값
        BgmManager.instance.audioSource.volume = slider_BGValume.value;

        //효과음 볼륨 텍스트 = 슬라이더 value값
        text_EFValume.text = (slider_EFValume.value * 100).ToString("0");
        //효과음 볼륨 값 = 슬라이더 value값
        EffectSoundManager.instance.auidoSource.volume = slider_EFValume.value; ;
    }


    //배경 음소거 버튼 클릭 시
    public void BG_ButtonClick()
    {
        //음소거 중이지 않다면
        if(!isMuteBG_Sound)
        {
            //이미지 변경 (음소거 이미지)
            image_BG_Audio.sprite = sprite_AudioButtons[1];

            //배경음악 끄기
            BgmManager.instance.StopMusic();

            isMuteBG_Sound = true;
        }

        //음소거 중이라면
        else
        {
            //이미지 변경 (기본 이미지)
            image_BG_Audio.sprite = sprite_AudioButtons[0];

            //배경음악 켜기
            BgmManager.instance.StartMusic();

            isMuteBG_Sound = false;
        }
    }

    //이펙트 음소거 버튼 클릭 시
    public void EF_ButtonClick()
    {
        //음소거 중이지 않다면
        if (!isMuteEF_Sound)
        {
            //이미지 변경 (음소거 이미지)
            image_EF_Audio.sprite = sprite_AudioButtons[1];

            //효과음 음악 끄기
            EffectSoundManager.instance.StopMusic();

            isMuteEF_Sound = true;
        }
        //음소거 중이라면
        else
        {
            //이미지 변경 (기본 이미지)
            image_EF_Audio.sprite = sprite_AudioButtons[0];

            //효과음 켜기
            EffectSoundManager.instance.StartMusic();

            isMuteEF_Sound = false;
        }
    }
}
