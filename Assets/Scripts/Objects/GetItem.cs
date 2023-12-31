using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public ObjectManager objectManagerScr;

    //������ Ű��
    public int key;

    //�����۰� ��Ҵ���
    public bool isTouch;

    //�ڷ�ƾ�� ����������
    public bool isGetStart;

    private void Update()
    {
        //�������� ������ ���� ���Ŀ��� ȹ���� �� ����
        if (ObjectControll.instance.getBotzime)
        {
            //������Ʈ�� ������ ZŰ�� ���� �ش� Key���� �������� ȹ���� �� �ִ�.
            if (Input.GetKeyDown(KeyCode.Z) && isTouch && !isGetStart)
            {
                StartCoroutine(GetItemStart(key));
            }
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
        isTouch = false;
    }

    IEnumerator GetItemStart(int _key)
    {
        Debug.Log("������ ȹ�� ����");

        isGetStart = true;

        //���� ȹ���� ������Ʈ�� ��¤�� ���
        if (_key == 1012)
        {
            Debug.Log("��¤ ȹ�� ����");

            //���� ��¤ ������Ʈ�� ���������� �ʴٸ�
            if (objectManagerScr.GetItem_Check(1012) == false)
            {
                //��¤ ȹ��
                objectManagerScr.GetItem(key);
            }
        }
        
        //��¤�� ������ ������Ʈ�� ���
        else
        {
            objectManagerScr.GetItem(key);
        }

        //ȹ���� �������� �����ϰ��
        if (_key == 1001)
        {
            GameManager.instance.getJangjack = true;
        }

        else if (_key == 1000)
        {
            GameManager.instance.getRice = true;
        }

        else if (_key == 1003)
        {
            GameManager.instance.getBagage = true;
        }

        yield return new WaitForSeconds(0.1f);

        //��¤�� �ƴҰ�� ������Ʈ ����
        if (_key != 1012)
        {
            Destroy(this.gameObject);
        }
    }
}
