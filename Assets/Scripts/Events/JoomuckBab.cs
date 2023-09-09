using UnityEngine;

public class JoomuckBab : MonoBehaviour
{
    //외부스크립트 참조
    public ObjectManager objectManagerScr;

    //장작 오브젝트
    public GameObject gameObject_JangJack;
    

    //가마솥과 접촉했는지 확인하는 falg
    public bool isTouch;


    private void Update()
    {
        if (isTouch && Input.GetKeyDown(KeyCode.Z))
        {
            //주먹밥 이벤트가 활성화 중이고 장작을 착용중이라면
            if(EventManager.instance.GetEventBool(Events.JoomuckBab) && ObjectManager.instance.GetEquipObjectKey() == 1001)
            {
                //아궁이에 장작을 넣었다
                DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(519), true);
                
                //장작 오브젝트 활성화
                gameObject_JangJack.SetActive(true);

                //장작 아이템 제거
                objectManagerScr.RemoveItem(1001);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            objectManagerScr.GetClue(2001);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            objectManagerScr.RemoveClue(2001);
        }
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
