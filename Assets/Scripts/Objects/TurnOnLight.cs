using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnLight : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public Controller playerCtrlScr;
    public TutorialManager tutorialManagerScr;

    //Spot Light Object
    public GameObject lightObject;

    //������Ʈ�� ��Ҵ���
    private bool isTouch;

    //���� ��������
    [Tooltip("���� ���� ������ True")]
    public bool isTrunOnLight;

    //���� �������� Ȯ���ϴ� ����
    public bool isLightsOn;

    // Update is called once per frame
    void Update()
    {

        //���ܺ��� �����µ� ZŰ�� ������ ���
        if (isTouch && !lightObject.activeSelf && Input.GetKeyDown(KeyCode.Z) && !isLightsOn)
        {
            Debug.Log("���ѱ�");
            lightObject.SetActive(true);
            isTrunOnLight = true;
            Invoke("isLightsOnTrue", 0.1f);
        }

        //���ܺ��� �������·� ZŰ�� ������ ���
        if (isTouch && lightObject.activeSelf && Input.GetKeyDown(KeyCode.Z) && isLightsOn && tutorialManagerScr.events != Events.TurnOnLights)
        {
            Debug.Log("�Ҳ���");
            lightObject.SetActive(false);
            isLightsOn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = false;
        }
    }
    //���ܺ� �ѱ�
    public void TurnOnLights()
    {
        lightObject.SetActive(true);
        isTrunOnLight = true;
    }


    //���ܺ� ����
    public void TurnOFFLights()
    {
        lightObject.SetActive(false);
        isTrunOnLight = false;
    }

    private void isLightsOnTrue()
    {
        isLightsOn = true;
    }
}
