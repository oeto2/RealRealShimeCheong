using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    //�̱��� ����
    public static EndingManager instance = null;

    //���� ��� ������Ʈ
    public GameObject gameObject_EndingBG;

    //������ ��� ������Ʈ
    public GameObject gameObject_RealEndingBG;

    //���� ��� �̹���
    public Image image_EndingBg;

    //�⺻ ��� Į��
    public Color32 color32_Nomal;

    //���� ���� Į��
    public Color32 color32_Bad;

    //�� ���� Į��
    public Color32 color32_Real;

    //���� ��� �ִϸ�����
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


    //���� ��� ���̱�
    public void ShowEndingBG()
    {
        Debug.Log("���� ��� ���̱�");

        //������ ��� ����
        CloseRealEndingBG();

        //���� ��� �̹��� Į�� ���� (�⺻)
        image_EndingBg.color = color32_Nomal;

        //���� ��� ������Ʈ ON
        gameObject_EndingBG.SetActive(true);
    }

    //��忣�� �̹����� ����
    public void ChangeToBadEndingBG()
    {
        //���� ��� �̹��� Į�� ����(��忣��)
        image_EndingBg.color = color32_Bad;
    }

    //������ �̹��� ���̱�
    public void ShowRealEndingBG()
    {
        Debug.Log("������ ��� ���� ���̱�");

        gameObject_RealEndingBG.SetActive(true);
    }

    //������ �̹��� ����
    public void CloseRealEndingBG()
    {
        gameObject_RealEndingBG.SetActive(false);
    }


    //Ÿ��Ʋ�� �̵�
    public void LoadTitleScene()
    {
        //���� ���� (������ ����)
        StartCoroutine(EndingExitDelay());

        //Ÿ��Ʋ �� �ҷ�����
        SceneManager.LoadScene("Title");
    }
    

    //���� ���� ������ �ڷ�ƾ (���� ���� ���� ����) 
    IEnumerator EndingExitDelay()
    {
        //������
        yield return new WaitForSeconds(2f);

        //���� ��� ����
        gameObject_EndingBG.SetActive(true);

        //NPC ���̾�α� ����
        DialogManager.instance.Dialouge_Canvas.SetActive(false);

    }

    //���� �̹��� õõ�� ��� �ϱ�
    public void BrightEndingBG()
    {
        //Fade In �ִϸ��̼� ����
        animator_EndingBG.SetBool("StartFade_In", true);
    }

    //���� �̹��� ����
    public void ResetEndingBG()
    {
        //Fade In �ִϸ��̼� ����
        animator_EndingBG.SetBool("StartFade_In", false);
    }
}
