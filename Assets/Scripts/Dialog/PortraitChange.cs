using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitChange : MonoBehaviour
{
    // �ٲٷ��� �̹���
    public Sprite image_change;

    // ���� �̹���
    Image image_now;

    // Start is called before the first frame update
    void Start()
    {
        image_now = GetComponent<Image>();
    }

    // ������ �̹��� ��ȯ �Լ�
    void ChangeImg()
    {
        image_now.sprite = image_change;
    }
}
