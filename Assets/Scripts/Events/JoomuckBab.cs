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

    //가마솥 기본 오브젝트
    public GameObject gameObjcet_GamasotNomal;

    //가마솥 끓는 오브젝트
    public GameObject gameObjcet_GamasotUsing;

    //가마솥과 접촉했는지 확인하는 falg
    public bool isTouch;

    //주먹밥 제작 순서
    public enum MakeJoomuckBab
    {
        //장작 넣기
        PushJangJack = 0,
        //부싯돌 사용
        UseFireStone,
        //물과 쌀 모두 없음
        FillWater_OR_PushRice,
        //물은 넣었는데 쌀은 안넣음
        FillWaterDone,
        //쌀은 넣었는데 물은 안넣음
        PushRiceDone,
        //주먹밥 만들기
        MakeJoomuckBab,
        //이벤트 종료
        Done
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
                    //장작 넣기
                    case MakeJoomuckBab.PushJangJack:
                        StartCoroutine(PushJangJack());
                        break;

                    //부싯돌 사용
                    case MakeJoomuckBab.UseFireStone:
                        StartCoroutine(UseFireStone());
                        break;

                    //물 넣기 혹은 쌀 넣기
                    case MakeJoomuckBab.FillWater_OR_PushRice:
                        StartCoroutine(FillWater_OR_PushRice());
                        break;

                    //물은 넣었는데 쌀은 안넣음
                    case MakeJoomuckBab.FillWaterDone:
                        StartCoroutine(FillWaterDone());
                        break;

                    //쌀은 넣었는데 물을 안넣음
                    case MakeJoomuckBab.PushRiceDone:
                        StartCoroutine(PushRiceDone());
                        break;

                    //주먹밥 만들기
                    case MakeJoomuckBab.MakeJoomuckBab:
                        StartCoroutine(TakeJoomuckBab());
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
            makeJoomuckBab = MakeJoomuckBab.FillWater_OR_PushRice;
        }
        yield return null;
    }

    //물 혹은 쌀 넣기
    private IEnumerator FillWater_OR_PushRice()
    {
        //물든 바가지를 착용중이라면
        if(ObjectManager.instance.GetEquipObjectKey() == 1004)
        {
            //시스템 메세지 출력
            DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(522), true);

            //물든 바가지 아이템 제거
            ObjectManager.instance.RemoveItem(1004);

            //바가지 아이템 획득
            ObjectManager.instance.GetItem(1003);


            //다음 이벤트로 이동
            makeJoomuckBab = MakeJoomuckBab.FillWaterDone;
        }

        //쌀을 착용중이라면
        if(objectManagerScr.GetEquipObjectKey() == 1000)
        {
            //시스템 메세지 출력
            DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(523), true);

            //쌀 아이템 제거
            ObjectManager.instance.RemoveItem(1000);

            //다음 이벤트로 이동
            makeJoomuckBab = MakeJoomuckBab.PushRiceDone;
        }

        yield return null;
    }
    
    //물은 넣었는데 쌀은 안 넣음
    private IEnumerator FillWaterDone()
    {
        //쌀을 착용 중이라면
        if (ObjectManager.instance.GetEquipObjectKey() == 1000)
        {
            //시스템 메세지 출력
            DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(523), true);

            //쌀 아이템 제거
            ObjectManager.instance.RemoveItem(1000);

            //가마솥 사용 시작
            UsingGamasot();

            //시스템 메세지가 끝날때까지 무한대기
            while (true)
            {
                if (DialogManager.instance.IsSystemMessageEnd() == true)
                {
                    yield return new WaitForSeconds(0.2f);
                    //다음 이벤트로 이동
                    makeJoomuckBab = MakeJoomuckBab.MakeJoomuckBab;
                    break;
                }

                yield return null;
            }
        }
        yield return null;
    }

    //쌀은 넣었는데 물을 안넣음
    private IEnumerator PushRiceDone()
    {
        //물이든 바가지를 착용 중이라면
        if (objectManagerScr.GetEquipObjectKey() == 1004)
        {
            //시스템 메세지 출력
            DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(522), true);

            //물이든 바가지 아이템 제거
            ObjectManager.instance.RemoveItem(1004);

            //바가지 아이템 획득
            ObjectManager.instance.GetItem(1003);

            //가마솥 사용 시작
            UsingGamasot();

            //시스템 메세지가 끝날때까지 무한대기
            while(true)
            {
                if(DialogManager.instance.IsSystemMessageEnd() == true)
                {
                    Debug.Log("다음 이벤트 이동");
                    yield return new WaitForSeconds(0.2f);
                    //다음 이벤트로 이동
                    makeJoomuckBab = MakeJoomuckBab.MakeJoomuckBab;
                    break;
                }

                yield return null;
            }
        }
        yield return null;
    }

    //주먹밥 챙기기
    private IEnumerator TakeJoomuckBab()
    {
        if(DialogManager.instance.IsSystemMessageEnd() == true)
        {
            //시스템 메세지 출력
            DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(524), true);

            //주먹밥 획득
            ObjectManager.instance.GetItem(1005);

            //가마솥 사용 종료
            UsingGamasotEnd();

            //다음 이벤트 이동
            makeJoomuckBab = MakeJoomuckBab.Done;
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

    //가마솥 사용 시작
    public void UsingGamasot()
    {
        //기본 가마솥 오브젝트 비활성화
        gameObjcet_GamasotNomal.SetActive(false);

        //끓는 가마솥 오브젝트 활성화
        gameObjcet_GamasotUsing.SetActive(true);
    }

    //가마솥 사용 끝
    public void UsingGamasotEnd()
    {
        //기본 가마솥 오브젝트 비활성화
        gameObjcet_GamasotNomal.SetActive(true);

        //불 오브젝트 비활성화
        gameobjcet_Fire.SetActive(false);

        //끓는 가마솥 오브젝트 활성화
        gameObjcet_GamasotUsing.SetActive(false);
    }
}
