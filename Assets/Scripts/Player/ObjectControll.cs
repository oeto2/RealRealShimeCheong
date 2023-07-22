using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControll : MonoBehaviour
{
    //������Ʈ �Ŵ��� ��ũ��Ʈ
    public ObjectManager objectManagerScr;

    //������Ʈ ������ ��ũ��Ʈ
    [SerializeField]
    private Objdata objdataScr;

    //Player�� ������Ʈ�� ���������� Ȯ���ϴ� flag
    [SerializeField]
    private bool isTriggerObject;

    //Player�� ȹ���� ������Ʈ
    [SerializeField]
    private GameObject gameobject_TargetObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //������Ʈ ������ ZŰ�� ������ ���
        if(Input.GetKeyDown(KeyCode.Z) && isTriggerObject) 
        {
            Debug.Log("����");
            Debug.Log(objdataScr.key);

            //������ ������Ʈ �߰�
            if(gameobject_TargetObject.CompareTag("Item"))
            {
                objectManagerScr.GetItem(objdataScr.key);
                //������Ʈ SetActive false
                gameobject_TargetObject.SetActive(false);
            }

            //�ܼ� ������ ȹ��
            else if (gameobject_TargetObject.CompareTag("Clue"))
            {
                objectManagerScr.GetClue(objdataScr.key);
                //������Ʈ SetActive false
                gameobject_TargetObject.SetActive(false);
            }
        }

    }

    //������Ʈ BoxCollider�� ���˽�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�������� ���
        if(collision.CompareTag("Item"))
        {
            //������ ������Ʈ
            gameobject_TargetObject = collision.gameObject;

            isTriggerObject = true;

            //������ ������ Objdata�� ������
            objdataScr = gameobject_TargetObject.GetComponent<Objdata>();
        }

        //�ܼ��� ���
        else if(collision.CompareTag("Clue"))
        {
            //������ ������Ʈ
            gameobject_TargetObject = collision.gameObject;

            isTriggerObject = true;

            //������ ������ Objdata�� ������
            objdataScr = gameobject_TargetObject.GetComponent<Objdata>();
        }
    }

    //������Ʈ BoxCollider�� �������ϰ��
    private void OnTriggerStay2D(Collider2D collision)
    { 
        //�������� ���
        if(collision.CompareTag("Item"))
        {
            isTriggerObject = true;
        }

        //�ܼ��� ���
        else if (collision.CompareTag("Clue"))
        {
            //������ ������Ʈ
            gameobject_TargetObject = collision.gameObject;

            isTriggerObject = true;

            //������ ������ Objdata�� ������
            objdataScr = gameobject_TargetObject.GetComponent<Objdata>();
        }
    }

    //������Ʈ boxCollider�� �������� ���
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggerObject = false;
    }

    
}
