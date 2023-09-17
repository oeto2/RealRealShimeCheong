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

    //엔딩 배경 이미지
    public Image image_EndingBg;

    //기본 배경 칼라값
    public Color32 color32_Nomal;

    //베드 엔딩 칼라값
    public Color32 color32_Bad;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //엔딩 배경 보이기
    public void ShowEndingBG()
    {
        //엔딩 배경 이미지 칼라 변경 (기본)
        image_EndingBg.color = color32_Nomal;

        //엔딩 배경 오브젝트 ON
        gameObject_EndingBG.SetActive(true);
    }

    //베드엔딩 이미지로 변경
    public void ChangeToBadEndingBG()
    {
        //엔딩 배경 이미지 칼라 변경(베드엔딩)
        image_EndingBg.color = color32_Bad;
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
        DialogManager.instance.Dialouge_System.SetActive(false);

    }
}
