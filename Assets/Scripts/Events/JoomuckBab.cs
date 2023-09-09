using UnityEngine;

public class JoomuckBab : MonoBehaviour
{
    //�ܺν�ũ��Ʈ ����
    public ObjectManager objectManagerScr;

    //���� ������Ʈ
    public GameObject gameObject_JangJack;
    

    //�����ܰ� �����ߴ��� Ȯ���ϴ� falg
    public bool isTouch;


    private void Update()
    {
        if (isTouch && Input.GetKeyDown(KeyCode.Z))
        {
            //�ָԹ� �̺�Ʈ�� Ȱ��ȭ ���̰� ������ �������̶��
            if(EventManager.instance.GetEventBool(Events.JoomuckBab) && ObjectManager.instance.GetEquipObjectKey() == 1001)
            {
                //�Ʊ��̿� ������ �־���
                DialogManager.instance.Start_SystemMessage(DialogManager.instance.GetNpcSentence(519), true);
                //���� ������Ʈ Ȱ��ȭ
                gameObject_JangJack.SetActive(true);
                //���� ������ ����
                objectManagerScr.RemoveItem(1001);
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
