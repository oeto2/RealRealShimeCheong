using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Ʃ�丮�� �ؽ�Ʈ
    public Text Tutorial_Text;

    // string�� �ؽ�Ʈ
    string dialogue_text;

    public string[] tutorialDialogue;

    public string[] dialogues_text;

    // ���� ��� ����� ���� ������ Ȯ�� ����
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

        // talkNum��° ��� ���
        //StartCoroutine(Typing(dialogues_text[talkNum]));
    }

    public void NextTalk()
	{

    }
}
