using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoomuckBab : MonoBehaviour
{

    //외부스크립트 참조
    public ObjectManager objectManagerScr;

    //장작 오브젝트
    public GameObject gameObject_JangJack;

    //불 오브젝트
    public GameObject gameobjcet_Fire;

    //가마솥과 접촉했는지 확인하는 falg
    public bool isTouch;

    //주먹밥 제작 순서
    public enum MakeJoomuckBab
    {
        //장작 넣기
        PushJangJack = 0,
        //부싯돌 사용
        UseFireStone,
        //물 넣기
        FillWater,
        //쌀 넣기
        PushRice,
        //주먹밥 만들기
        MakeJoomuckBab
    }

    //주먹밥 만들기 이벤트 순서
    public MakeJoomuckBab makeJoomuckBab = MakeJoomuckBab.PushJangJack;

    private void Update()
    {
        if (isTouch && Input.GetKeyDown(KeyCode.Z))
        {
            //주먹밥 이벤트가 활성화 중이고 장작을 착용중이라면
            if(EventManager.instance.GetEventBool(Events.JoomuckBab))
            {
                switch(makeJoomuckBab)
                {
                    //장작 만들기
                    case MakeJoomuckBab.PushJangJack:
                        StartCoroutine(PushJangJack());
                        break;

                    //부싯돌 사용
                    case MakeJoomuckBab.UseFireStone:
                        StartCoroutine(UseFireStone());
                        break;
                }
            }
        }
    }   

    //장작 넣기
    private IEnumerator PushJangJack()
    {
        //장작 아이템을 착용중이라면
        if (ObjectManager.instance.GetEquipObjectKey() == 1001)
        {
            //시스템 메세지 출력
            DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(519), true);

            //장작 오브젝트 활성화
            gameObject_JangJack.SetActive(true);

            //장작 아이템 제거
            objectManagerScr.RemoveItem(1001);

            //다음 이벤트로 이동
            makeJoomuckBab = MakeJoomuckBab.UseFireStone;
        }
        yield return null;
    }
    
    //부싯돌 사용
    private IEnumerator UseFireStone()
    {
        //부싯돌을 착용중이라면
        if (ObjectManager.instance.GetEquipObjectKey() == 1002)
        {
            //불 오브젝트 활성화
            gameobjcet_Fire.SetActive(true);

            //시스템 메세지 출력
            DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(520), true);

            //다음 이벤트로 이동
            makeJoomuckBab = MakeJoomuckBab.FillWater;
        }
        yield return null;
    }


    //오브젝트 접촉시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    //오브젝트 접촉에서 벗어났을경우
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }
}
