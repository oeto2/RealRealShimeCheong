using UnityEngine;

public class JoomuckBab : MonoBehaviour
{

    //�����ܰ� �����ߴ��� Ȯ���ϴ� falg
    public bool isTouch;


    private void Update()
    {
        if (isTouch && Input.GetKeyDown(KeyCode.Z))
        {
            //�ָԹ� �̺�Ʈ�� Ȱ��ȭ ���̶��
            if(EventManager.instance.GetEventBool(Events.JoomuckBab))
            {
                
            }
        }
    }

    //������Ʈ ���˽�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    //������Ʈ ���˿��� ��������
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }

}
