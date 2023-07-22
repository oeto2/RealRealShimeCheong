using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitChange : MonoBehaviour
{
    // 바꾸려는 이미지
    public Sprite image_change;

    // 지금 이미지
    Image image_now;

    // Start is called before the first frame update
    void Start()
    {
        image_now = GetComponent<Image>();
    }

    // 프로필 이미지 전환 함수
    void ChangeImg()
    {
        image_now.sprite = image_change;
    }
}
