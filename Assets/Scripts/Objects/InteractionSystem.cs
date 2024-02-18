using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    [Header("Interaction UI")]
    //상호작용 UI 켄버스
    [SerializeField] private GameObject InteractionCavas;
    [SerializeField] private Text Interaction_Text;
    [SerializeField] private string showTextContent;

    [Header("Postion Setting")]
    [SerializeField][Range(-2f, 2f)] private float xPos;
    [SerializeField][Range(-2f, 2f)] private float YPos;


    private void Start()
    {
        //켄버스 프리팹 할당 후 자식으로 설정
        InteractionCavas = Instantiate(Resources.Load<GameObject>("Prefabs/InteractionCanvas"), transform);
        Interaction_Text = InteractionCavas.GetComponentInChildren<Text>();
        SettingInteractionText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            InteractionCavas.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            InteractionCavas.SetActive(false);
    }

    //상호작용 텍스트 설정
    void SettingInteractionText()
    {
        Interaction_Text.text = showTextContent;
        InteractionCavas.transform.position += new Vector3(xPos, YPos, 0);
    }
}
