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

    //���� ��� �̹���
    public Image image_EndingBg;

    //�⺻ ��� Į��
    public Color32 color32_Nomal;

    //���� ���� Į��
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

    //���� ��� ���̱�
    public void ShowEndingBG()
    {
        //���� ��� �̹��� Į�� ���� (�⺻)
        image_EndingBg.color = color32_Nomal;

        //���� ��� ������Ʈ ON
        gameObject_EndingBG.SetActive(true);
    }

    //���忣�� �̹����� ����
    public void ChangeToBadEndingBG()
    {
        //���� ��� �̹��� Į�� ����(���忣��)
        image_EndingBg.color = color32_Bad;
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
        DialogManager.instance.Dialouge_System.SetActive(false);

    }
}
