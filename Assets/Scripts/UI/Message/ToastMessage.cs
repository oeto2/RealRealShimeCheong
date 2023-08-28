using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastMessage : MonoBehaviour
{
    //슬롯 UI Image
    public Image Image_Slot;

    //오브젝트 UI Image
    public Image Image_Object;

    //TosastMessage Text
    public Text text_Toast;

    //Object Name Text
    public Text text_ObjectName;

    //아이템 sprite 이미지
    public Sprite[] sprite_Items;

    //단서 Sprite 이미지
    public Sprite[] sprite_Clues;

    //토스트 메세지 애니메이터
    public Animator animator_Toast;

    //토스트 메세지가 실행중인지
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
            //메세지 실행
            ToastMessageStart();
        }
    }

    //ToastMessageAnimation
    IEnumerator ToastMessageAnimation(bool _isPractice)
    {
        //이미 실행중이 아니라면
        if(!_isPractice)
        {
            yield return new WaitForSeconds(0.05f);
            toastMessagePractice = true;

            //메세지 위로 띄우기
            ToastMessageUP();

            yield return new WaitForSeconds(3f);

            //메세지 아래로 이동
            ToastMessageDown();

            yield return new WaitForSeconds(2f);

            //토스트 메세지 원점
            ToastMessageOrigin();

            yield return new WaitForSeconds(0.1f);
            animator_Toast.SetBool("OriginStart", false);

            toastMessagePractice = false;
        }

        //이미 실행중이라면
        if(_isPractice)
        {
            //위로 올라간 상태에서 실행되었을경우
            if(animator_Toast.GetBool("UpStart"))
            {
                //아래로 내리기
                ToastMessageDown();
                yield return new WaitForSeconds(1f);

                //토스트 메세지 원점
                ToastMessageOrigin();

                yield return new WaitForSeconds(0.1f);

                //파라미터 값 리셋
                ToastMessageParameters_Reset();

                //시작 조건 falg 초기화
                toastMessagePractice = false;

                yield return new WaitForSeconds(0.5f);

                //애니메이션 재시작
                ToastMessageStart();
            }
        }
    }

    //ToastMessageStart()
    public void ToastMessageStart()
    {
        //실행중이 아니라면
        if (!toastMessagePractice)
        {
            Debug.Log("코루틴 실행");
            //메세지 실행
            StartCoroutine(ToastMessageAnimation(false));
        }

        //이미 실행중이라면
        if (toastMessagePractice)
        {
            Debug.Log("코루틴 종료");
            //코루틴 종료 (실행중인 애니메이션 종료)
            StopAllCoroutines();

            //애니메이션 실행
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

    //토스트 메세지 정보값 변경
    public void ToastMessageInfo_Chage(string _text, Sprite _sprite, string _name)
    {
        //토스트 메세지 텍스트 변경
        text_Toast.text = _text;

        //토스트 메세지 오브젝트 이미지 변경
        Image_Object.sprite = _sprite;

        //토스트 메세지 오브젝트 이름 변경
        text_ObjectName.text = _name;
    }
}
