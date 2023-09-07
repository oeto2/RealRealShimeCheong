using UnityEngine;

public class JoomuckBab : MonoBehaviour
{

    //가마솥과 접촉했는지 확인하는 falg
    public bool isTouch;


    private void Update()
    {
        if (isTouch && Input.GetKeyDown(KeyCode.Z))
        {
            //주먹밥 이벤트가 활성화 중이라면
            if(EventManager.instance.GetEventBool(Events.JoomuckBab))
            {
                
            }
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
