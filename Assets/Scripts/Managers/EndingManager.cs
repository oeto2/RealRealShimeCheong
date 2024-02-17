using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    //싱글톤 패턴
    public static EndingManager instance = null;

    //엔딩 배경 오브젝트
    public GameObject gameObject_EndingBG;

    //진엔딩 배경 오브젝트
    public GameObject gameObject_RealEndingBG;

    //엔딩 배경 이미지
    public Image image_EndingBg;

    //기본 배경 칼라값
    public Color32 color32_Nomal;

    //베드 엔딩 칼라값
    public Color32 color32_Bad;

    //진 엔딩 칼라값
    public Color32 color32_Real;

    //엔딩 배경 애니메이터
    public Animator animator_EndingBG;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }


    //엔딩 배경 보이기
    public void ShowEndingBG()
    {
        Debug.Log("엔딩 배경 보이기");

        //진엔딩 배경 끄기
        CloseRealEndingBG();

        //엔딩 배경 이미지 칼라 변경 (기본)
        image_EndingBg.color = color32_Nomal;

        //엔딩 배경 오브젝트 ON
        gameObject_EndingBG.SetActive(true);
    }

    //배드엔딩 이미지로 변경
    public void ChangeToBadEndingBG()
    {
        //엔딩 배경 이미지 칼라 변경(배드엔딩)
        image_EndingBg.color = color32_Bad;
    }

    //진엔딩 이미지 보이기
    public void ShowRealEndingBG()
    {
        Debug.Log("진엔딩 배경 변경 보이기");

        gameObject_RealEndingBG.SetActive(true);
    }

    //진엔딩 이미지 끄기
    public void CloseRealEndingBG()
    {
        gameObject_RealEndingBG.SetActive(false);
    }


    //타이틀로 이동
    public void LoadTitleScene()
    {
        //엔딩 종료 (딜레이 버전)
        StartCoroutine(EndingExitDelay());

        //타이틀 씬 불러오기
        SceneManager.LoadScene("Title");
    }
    

    //엔딩 종료 딜레이 코루틴 (엔딩 종료 로직 모음) 
    IEnumerator EndingExitDelay()
    {
        //딜레이
        yield return new WaitForSeconds(2f);

        //엔딩 배경 종료
        gameObject_EndingBG.SetActive(true);

        //NPC 다이얼로그 종료
        DialogManager.instance.Dialouge_Canvas.SetActive(false);

    }

    //엔딩 이미지 천천히 밝게 하기
    public void BrightEndingBG()
    {
        //Fade In 애니메이션 실행
        animator_EndingBG.SetBool("StartFade_In", true);
    }

    //엔딩 이미지 리셋
    public void ResetEndingBG()
    {
        //Fade In 애니메이션 리셋
        animator_EndingBG.SetBool("StartFade_In", false);
    }
}
