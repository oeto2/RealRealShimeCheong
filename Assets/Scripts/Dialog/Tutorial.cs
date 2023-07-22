using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // 튜토리얼 텍스트
    public Text Tutorial_Text;

    // string형 텍스트
    string dialogue_text;

    public string[] tutorialDialogue;

    public string[] dialogues_text;

    // 다음 대사 출력을 위한 정수형 확인 변수
    public int talkNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IEnumerator Typing(string talk)
	//{

	//}

    public void StartTalk(string[] talks)
	{
        dialogues_text = talks;

        // talkNum번째 대사 출력
        //StartCoroutine(Typing(dialogues_text[talkNum]));
    }

    public void NextTalk()
	{

    }
}
