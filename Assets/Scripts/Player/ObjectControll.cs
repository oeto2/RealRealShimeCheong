using UnityEngine;

public class ObjectControll : MonoBehaviour
{
    //�ܺ� ��ũ��Ʈ ����
    public ObjectManager objectManagerScr;
    public Dialog_TypingWriter_ShimBongSa playerDialogueScr;
    public TutorialManager tutorialManagerScr;
    public Controller playerCtrlScr;
    public PlayerAnimation playerAnimationScr;

    //������Ʈ ������ ��ũ��Ʈ
    [SerializeField]
    private Objdata objdataScr;

    //Player�� ������Ʈ�� ���������� Ȯ���ϴ� flag
    [SerializeField]
    private bool isTriggerObject;

    //Player�� ȹ���� ������Ʈ
    [SerializeField]
    private GameObject gameobject_TargetObject;

    //UI ���� �̹���
    public GameObject gameObjcet_BotzimeImage;

    //UI ���� �̹���
    public GameObject gameObject_MapImage;

    //������ ȹ�� �ߴ���
    public bool getBotzime;

    //���� ȹ�� �ߴ���
    public bool getMap;

    //���� ������Ʈ
    public GameObject botzime;

    //���� ������Ʈ
    public GameObject map;

    //�̱���
    public static ObjectControll instance = null;

    //���� UI ������Ʈ
    public GameObject gameObject_BotzimeUI;

    //���� UI ������Ʈ
    public GameObject gameObject_MapUI;


    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        //������Ʈ ������ ZŰ�� ������ ���
        if (Input.GetKeyDown(KeyCode.Z) && isTriggerObject && tutorialManagerScr.setence1End)
        {
            //������ ������Ʈ �߰�
            if (gameobject_TargetObject.CompareTag("Item"))
            {
                objectManagerScr.GetItem(objdataScr.key);
                //������Ʈ SetActive false
                gameobject_TargetObject.SetActive(false);
            }

            //�ܼ� ������ ȹ��
            else if (gameobject_TargetObject.CompareTag("Clue"))
            {
                objectManagerScr.GetClue(objdataScr.key);
                //������Ʈ SetActive false
                gameobject_TargetObject.SetActive(false);
            }

            //������ ���
            else if (gameobject_TargetObject.CompareTag("Object") && gameobject_TargetObject.name == "Botzime")
            {
                //���� �̹��� �����ֱ�
                gameObjcet_BotzimeImage.SetActive(true);
                GetBotzime(gameobject_TargetObject);
            }

            //���� ���
            else if (gameobject_TargetObject.CompareTag("Object") && gameobject_TargetObject.name == "Map")
            {
                //���� �̹��� �����ֱ�
                gameObject_MapImage.SetActive(true);
                GetMap(gameobject_TargetObject);
            }
        }
    }

    //������Ʈ BoxCollider�� ���˽�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string objName = collision.name;

        // �������� ���
        if (collision.CompareTag("Item"))
        {
            //������ ������Ʈ
            gameobject_TargetObject = collision.gameObject;

            isTriggerObject = true;

            //������ ������ Objdata�� ������
            objdataScr = gameobject_TargetObject.GetComponent<Objdata>();
        }

        //�ܼ��� ���
        else if(collision.CompareTag("Clue"))
        {
            //������ ������Ʈ
            gameobject_TargetObject = collision.gameObject;

            isTriggerObject = true;

            //������ ������ Objdata�� ������
            objdataScr = gameobject_TargetObject.GetComponent<Objdata>();
        }

        //������ ����� ���
        else if(collision.CompareTag("Object") && objName == "Botzime")
        {
            gameobject_TargetObject = collision.gameObject;

            isTriggerObject = true;
        }

        //���̶� ����� ���
        else if (collision.CompareTag("Object") && objName == "Map")
        {
            gameobject_TargetObject = collision.gameObject;

            isTriggerObject = true;
        }
    }

    //������Ʈ BoxCollider�� �������ϰ��
    private void OnTriggerStay2D(Collider2D collision)
    {
        string objName = collision.name;


        //������ ���
        if (collision.CompareTag("Item"))
        {
            isTriggerObject = true;
        }

        //�ܼ��� ���
        else if (collision.CompareTag("Clue"))
        {
            //������ ������Ʈ
            gameobject_TargetObject = collision.gameObject;

            isTriggerObject = true;
        }

        //������ ����� ���
        else if (collision.CompareTag("Object") && objName == "Botzime")
        {
            isTriggerObject = true;
        }

        //���̶� ����� ���
        else if (collision.CompareTag("Object") && objName == "Map")
        {
            isTriggerObject = true;
        }
    }

    //������Ʈ boxCollider�� �������� ���
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggerObject = false;
    }

    //���� ȹ��
    public void GetBotzime(GameObject _obj)
    {
        getBotzime = true;

        //������Ʈ UI ���̱�
        UIManager.instance.gameObject_UIBackGround.SetActive(true);

        ////���� UI �̹��� ���̱�
        //gameObject_BotzimeUI.SetActive(true);

        ////���� �ִϸ��̼� ����
        //playerAnimationScr.ChangeAnimationBotzime();

        //���� ȹ�� ��ȭ ����
        playerDialogueScr.Start_Sentence_GetBotzime();

        //������Ʈ ��Ȱ��ȭ
        _obj.SetActive(false);
    }

    //���� ȹ��
    #region
    public void GetMap(GameObject _obj)
    {
        getMap = true;

        //���� ȹ�� ��ȭ ����
        playerDialogueScr.Start_Sentence_GetMap();

        ////�� ������Ʈ UI ���̱�
        //gameObject_MapUI.SetActive(true);

        ////���� �̹��� ���̱�
        //gameObject_MapImage.SetActive(true);
            
        //������Ʈ ��Ȱ��ȭ
        _obj.SetActive(false);
    }
    public void GetMap()
    {
        getMap = true;

        //���� ȹ�� ��ȭ ����
        playerDialogueScr.Start_Sentence_GetMap();

        //  //�� ������Ʈ UI ���̱�
        //gameObject_MapUI.SetActive(true);

        ////���� �̹��� ���̱�
        //gameObject_MapImage.SetActive(true);
        
    }
    #endregion

    //���� ����
    public void ResetBotzime()
    {
        getBotzime = false;
        botzime.SetActive(true);
    }

    //���� ����
    public void ResetMap()
    {
        getMap = false;
        //map.SetActive(true);
    }

    //���� ȹ�� �� �ε�
    public void LoadBotzime()
    {
        getBotzime = true;

        //���� UI �̹��� ���̱�
        gameObject_BotzimeUI.SetActive(true);

        //���� �ִϸ��̼� ����
        playerAnimationScr.ChangeAnimationBotzime();
        botzime.SetActive(false);
    }

    //���� ȹ�� �� �ε�
    public void LoadMap()
    {
        getMap = true;
        //map.SetActive(false);
    }
}
