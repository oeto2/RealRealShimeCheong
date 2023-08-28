using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastMessage : MonoBehaviour
{
    //���� UI Image
    public Image Image_Slot;

    //������Ʈ UI Image
    public Image Image_Object;

    //TosastMessage Text
    public Text text_Toast;

    //Object Name Text
    public Text text_ObjectName;

    //������ sprite �̹���
    public Sprite[] sprite_Items;

    //�ܼ� Sprite �̹���
    public Sprite[] sprite_Clues;

    //�佺Ʈ �޼��� �ִϸ�����
    public Animator animator_Toast;

    //�佺Ʈ �޼����� ����������
    public bool toastMessagePractice;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //�޼��� ����
            ToastMessageStart();
        }
    }

    //ToastMessageAnimation
    IEnumerator ToastMessageAnimation(bool _isPractice)
    {
        //�̹� �������� �ƴ϶��
        if(!_isPractice)
        {
            yield return new WaitForSeconds(0.05f);
            toastMessagePractice = true;

            //�޼��� ���� ����
            ToastMessageUP();

            yield return new WaitForSeconds(3f);

            //�޼��� �Ʒ��� �̵�
            ToastMessageDown();

            yield return new WaitForSeconds(2f);

            //�佺Ʈ �޼��� ����
            ToastMessageOrigin();

            yield return new WaitForSeconds(0.1f);
            animator_Toast.SetBool("OriginStart", false);

            toastMessagePractice = false;
        }

        //�̹� �������̶��
        if(_isPractice)
        {
            //���� �ö� ���¿��� ����Ǿ������
            if(animator_Toast.GetBool("UpStart"))
            {
                //�Ʒ��� ������
                ToastMessageDown();
                yield return new WaitForSeconds(1f);

                //�佺Ʈ �޼��� ����
                ToastMessageOrigin();

                yield return new WaitForSeconds(0.1f);

                //�Ķ���� �� ����
                ToastMessageParameters_Reset();

                //���� ���� falg �ʱ�ȭ
                toastMessagePractice = false;

                yield return new WaitForSeconds(0.5f);

                //�ִϸ��̼� �����
                ToastMessageStart();
            }
        }
    }

    //ToastMessageStart()
    public void ToastMessageStart()
    {
        //�������� �ƴ϶��
        if (!toastMessagePractice)
        {
            Debug.Log("�ڷ�ƾ ����");
            //�޼��� ����
            StartCoroutine(ToastMessageAnimation(false));
        }

        //�̹� �������̶��
        if (toastMessagePractice)
        {
            Debug.Log("�ڷ�ƾ ����");
            //�ڷ�ƾ ���� (�������� �ִϸ��̼� ����)
            StopAllCoroutines();

            //�ִϸ��̼� ����
            StartCoroutine(ToastMessageAnimation(true));
        }
    }

    //ToastMessage UP
    public void ToastMessageUP()
    {
        if (!animator_Toast.GetBool("UpStart"))
        {
            animator_Toast.SetBool("UpStart", true);
        }
    }

    //ToastMessage Donw
    public void ToastMessageDown()
    {
        if (!animator_Toast.GetBool("DownStart"))
        {
            animator_Toast.SetBool("DownStart", true);
        }
    }

    //ToastMessage Origin
    public void ToastMessageOrigin()
    {
        if (!animator_Toast.GetBool("OriginStart"))
        {
            animator_Toast.SetBool("UpStart", false);
            animator_Toast.SetBool("DownStart", false);
            animator_Toast.SetBool("OriginStart", true);
        }
    }

    //Reset ToastMEssage Parameters
    public void ToastMessageParameters_Reset()
    {
        animator_Toast.SetBool("UpStart", false);
        animator_Toast.SetBool("DownStart", false);
        animator_Toast.SetBool("OriginStart", false);
    }

    //�佺Ʈ �޼��� ������ ����
    public void ToastMessageInfo_Chage(string _text, Sprite _sprite, string _name)
    {
        //�佺Ʈ �޼��� �ؽ�Ʈ ����
        text_Toast.text = _text;

        //�佺Ʈ �޼��� ������Ʈ �̹��� ����
        Image_Object.sprite = _sprite;

        //�佺Ʈ �޼��� ������Ʈ �̸� ����
        text_ObjectName.text = _name;
    }
}
