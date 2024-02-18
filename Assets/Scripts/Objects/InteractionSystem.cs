using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    [Header("Interaction UI")]
    //��ȣ�ۿ� UI �˹���
    [SerializeField] private GameObject InteractionCavas;
    [SerializeField] private Text Interaction_Text;
    [SerializeField] private string showTextContent;

    [Header("Postion Setting")]
    [SerializeField][Range(-2f, 2f)] private float xPos;
    [SerializeField][Range(-2f, 2f)] private float YPos;


    private void Start()
    {
        //�˹��� ������ �Ҵ� �� �ڽ����� ����
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

    //��ȣ�ۿ� �ؽ�Ʈ ����
    void SettingInteractionText()
    {
        Interaction_Text.text = showTextContent;
        InteractionCavas.transform.position += new Vector3(xPos, YPos, 0);
    }
}
