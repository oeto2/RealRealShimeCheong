using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnLight : MonoBehaviour
{
    //외부 스크립트 참조
    public Controller playerCtrlScr;
    public TutorialManager tutorialManagerScr;

    public Animator animator_DeungJan;

    //Spot Light Object
    public GameObject lightObject;

    //오브젝트랑 닿았는지
    private bool isTouch;

    //불이 켜졌는지
    [Tooltip("등잔 불이 켜지면 True")]
    public bool isTrunOnLight;

    //불이 켜졌는지 확인하는 변수
    public bool isLightsOn;

    // Update is called once per frame
    void Update()
    {

        //등잔불이 꺼졌는데 Z키를 눌렀을 경우
        if (isTouch && !lightObject.activeSelf && Input.GetKeyDown(KeyCode.Z) && !isLightsOn)
        {
            Debug.Log("불켜기");
            lightObject.SetActive(true);
            isTrunOnLight = true;
            Invoke("isLightsOnTrue", 0.1f);

            //애니메이션 켜기
            animator_DeungJan.SetBool("LightOn", true);
        }

        //등잔불이 켜진상태로 Z키를 눌렀을 경우
        if (isTouch && lightObject.activeSelf && Input.GetKeyDown(KeyCode.Z) && isLightsOn && tutorialManagerScr.events != TutorialEvents.TurnOnLights)
        {
            Debug.Log("불끄기");
            lightObject.SetActive(false);
            isLightsOn = false;

            //애니메이션 끄기
            animator_DeungJan.SetBool("LightOn", false);
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
    //등잔불 켜기
    public void TurnOnLights()
    {
        lightObject.SetActive(true);
        isTrunOnLight = true;
        isLightsOn = true;

        //애니메이션 켜기
        animator_DeungJan.SetBool("LightOn", true);
    }


    //등잔불 끄기
    public void TurnOFFLights()
    {
        lightObject.SetActive(false);
        isTrunOnLight = false;
        isLightsOn = false;


        //애니메이션 끄기
        animator_DeungJan.SetBool("LightOn", false);
    }

    private void isLightsOnTrue()
    {
        isLightsOn = true;
    }
}
