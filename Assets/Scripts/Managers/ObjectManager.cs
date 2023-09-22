using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//List�� Jason���� �����Ҽ� �ְԵ����ִ� Class
[System.Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}

//Item ������ ���̽�
[System.Serializable]
public class Item
{
    //������ ����
    public Item(int _key, string _type, string _name, string _content, bool _isUsing, int _indexNum)
    {
        key = _key; type = _type; name = _name; content = _content; isUsing = _isUsing; indexNum = _indexNum;
    }

    public int key, indexNum;
    public string type, name, content;
    public bool isUsing;
}

//Clue ������ ���̽�
[System.Serializable]
public class Clue
{
    //������ ����
    public Clue(int _key, string _type, string _name, string _content, bool _isUsing, int _indexNum)
    {
        key = _key; type = _type; name = _name; content = _content; isUsing = _isUsing; indexNum = _indexNum;
    }

    public int key, indexNum;
    public string type, name, content;
    public bool isUsing;
}

public class ObjectManager : MonoBehaviour
{
    //�ܺν�ũ��Ʈ ����
    public UIManager uiManagerScr;
    public ToastMessage toastMessageScr;

    //�̱���
    public static ObjectManager instance = null;

    //Item
    #region 
    //������ ������ ���� ��� txt����
    public TextAsset itemDataBase;

    //Item Ŭ���� ���� ������ �����͵��� ����Ʈȭ ��Ų ��
    [Tooltip("��� ������ ����Ʈ")]
    public List<Item> allItemList;

    //���ӳ����� ����� ������ ����Ʈ (������â ����)
    [Tooltip("���ӳ����� ����� ������ ����Ʈ")]
    public List<Item> myItemList;

    //���Կ��� ������ ������ ����Ʈ (���� ������)
    [Tooltip("���ӳ����� ȹ���� ������ ����Ʈ(�κ��丮)")]
    public List<Item> curItemList;

    //����â���� ������ ������ ����Ʈ (���� ������)
    [Tooltip("���ӳ����� ȹ���� ������ ����Ʈ(����â)")]
    public List<Item> curItemList2;
    #endregion

    //�ܼ� ������ ���� ��� txt����
    public TextAsset clueDataBase;

    //Clue Ŭ���� ���� �ܼ� �����͵��� ����Ʈȭ ��Ų��
    [Tooltip("��� �ܼ� ����Ʈ")]
    public List<Clue> allClueList;

    //���ӳ����� ����� �ܼ� ����Ʈ
    [Tooltip("���ӳ����� ����� �ܼ� ����Ʈ")]
    public List<Clue> myClueList;

    //���Կ��� ������ �ܼ� ����Ʈ(���� ������)
    [Tooltip("���ӳ����� ȹ���� �ܼ� ����Ʈ(�κ��丮)")]
    public List<Clue> curClueList;

    //����â �������� �ܼ� ����Ʈ(���� ������)
    [Tooltip("���ӳ����� ȹ���� �ܼ� ����Ʈ(����â)")]
    public List<Clue> curClueList2;

    //������ Json�� ����� ��ġ
    public string itemfilePath;
    //�������� ������ Json�� ����� ��ġ
    public string curItemfilePath;

    //�ܼ� Json�� ����� ��ġ
    public string cluefilePath;
    //�������� �ܼ� Json�� ����� ��ġ
    public string curCluefilePath;

    //���̾�α� Json�� ����� ��ġ
    public string dialogfilePath;
    //���̾�α� ������ ��� Json�� ����� ��ġ
    public string curDialogfilePath;

    //ó���� ������ ������Ʈ Ÿ��
    public string curType = "Item";

    //����
    public GameObject[] slot;
    //����â ����
    public GameObject[] slot2;

    //������϶� �̹���
    public GameObject[] usingImage;
    //����â ������϶� �̹���
    public GameObject[] usingImage2;

    //������Ʈ �̹���
    public Image[] objectImage;
    //����â ������Ʈ �̹���
    public Image[] objectImage2;

    //�ΰ��� �̹����� �迭�� �ε����� ������Ʈ���� �ε��� ���� ��ġ�ؾ��Ѵ�.
    //������ �̹���
    public Sprite[] itemSprite;
    //�ܼ� �̹���
    public Sprite[] clueSprite;

    //���� �������� ������Ʈ�� �̹���
    public Image equitObjectSprite;

    //������Ʈ ������ ǥ�õǴ� �ؽ�Ʈ
    public Text contentText;
    //����â ������Ʈ ������ ǥ�õǴ� �ؽ�Ʈ
    public Text contentText2;

    //����â�� ����1�� �̹���
    public Image image_CombineSlot1;
    //����â�� ����2�� �̹���
    public Image image_CombineSlot2;

    //����â Sprite�̹���
    public Sprite sprite_Slot;
    //����â Ȱ��ȭ Sprite�̹���
    public Sprite sprite_UsingSlot;
    //NoneImage
    public Sprite sprite_NoneImage;

    //����â 1�� ���������
    public bool combineSlot1_Using;
    //����â 2�� ���������
    public bool combineSlot2_Using;

    //����â ����1 ������ �̹���
    public Image image_CombineSlot1Item;
    //����â ����2 ������ �̹���
    public Image image_CombineSlot2Item;

    //����â ����1 ������ �̸�
    public Text combineSlot1Name;
    //����â ����2 ������ �̸�
    public Text combineSlot2Name;

    //����â ����1�� ���õ� �������� ����
    public Item curCombineItem1Info;
    //����â ����2�� ���õ� �������� ����
    public Item curCombineItem2Info;

    //����â ����1�� ���õ� �ܼ��� ����
    public Clue curCombineClue1Info;
    //����â ����2�� ���õ� �ܼ��� ����
    public Clue curCombineClue2Info;

    // GetClue Ȯ�� ����
    public bool isGetClue = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //��ü ������ ����Ʈ �ҷ�����
        #region
        //String �迭 line�ȿ� itemDataBase ���� �������� 0���� itemDataBase.text.Length���� �޾ƿµ� ���͸� �������� �迭�� ������ ����
        //ex) line.length = ItemDataBase ���� �ڵ��� �� ����
        string[] itemline = itemDataBase.text.Substring(0, itemDataBase.text.Length).Split('\n');

        // ItmeDataBasse ���� �������� Tab�� �������� ������ ����
        // ex) row[0] = key, row[1] = ObjectType, row[2] = Name, row[3] = Content, row[4] = isUsing, row[5] = IndexNum
        for (int i = 0; i < itemline.Length; i++)
        {
            string[] row = itemline[i].Split('\t');

            //allItemList�� ���� �߰�
            allItemList.Add(new Item(int.Parse(row[0]), row[1], row[2], row[3], row[4] == "TRUE", int.Parse(row[5])));
        }
        #endregion

        //��ü �ܼ� ����Ʈ �ҷ�����
        #region
        //�ܼ� �����ͺ��̽� �ؽ�Ʈ ���Ͼ��� �� ������ŭ�� ũ�⸦ ���� �迭 clueline ����
        string[] clueline = clueDataBase.text.Substring(0, clueDataBase.text.Length).Split('\n');

        for (int i = 0; i < clueline.Length; i++)
        {
            //TabŰ�� ���� �������� �����͵��� ��� �迭�� ����
            string[] row = clueline[i].Split('\t');

            allClueList.Add(new Clue(int.Parse(row[0]), row[1], row[2], row[3], row[4] == "TRUE", int.Parse(row[5])));
        }
        #endregion

        //������ Json ������ ����� ��ġ
        itemfilePath = Application.persistentDataPath + "/AllItemText.txt";

        //�������� ������ Json ������ ����� ��ġ
        curItemfilePath = Application.persistentDataPath + "/CurItemText.txt";

        //�ܼ� Json ������ ����� ��ġ
        cluefilePath = Application.persistentDataPath + "/AllClueText.txt";

        //�������� �ܼ� Json ������ ����� ��ġ
        curCluefilePath = Application.persistentDataPath + "/CurClueText.txt";

        Save(100);
        Load(100);

        //������ �� �⺻���� �����ֱ�
        TabClick(curType);
        //����â ������ �� �⺻���� �����ֱ�
        TabClick2(curType);

        ////��� ������ ȹ��
        //GetAllItem();

        ////��� �ܼ� ȹ��
        //GetAllClue();

        //�ν˵� ȹ��
        GetItem(1002);

        //��� �ܼ� ȹ��
        //GetClue(2021);
        //GetClue(2011);
        //GetClue(2012);
    }


    private void Update()
    {
        //����â�� ������ �ʾ��� ���
        if (!uiManagerScr.gameObject_CombineWindow.activeSelf)
        {
            //���� ���� ����
            EmptyCombineSlot();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(GetEquipObjectKey());
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("1002 ȹ��");
            GetItem(1002);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("1003 ȹ��");
            GetItem(1003);
        }
    }

    //������Ʈ ���� Ŭ���� (������â ����)
    public void SlotClick(int slotNum)
    {
        //������Ʈ Ÿ���� �������� ���
        if (curType == "Item")
        {
            //������ â�� ���
            #region
            Item curItem = curItemList[slotNum];
            Item usingItem = curItemList.Find(x => x.isUsing == true);



            //������� �������� ������ �����۵��� ����� false�� �ٲ�
            if (usingItem != null) usingItem.isUsing = false;
            {
                curItem.isUsing = true;

                //���õ� �����۾��� ���� ǥ���ϱ�
                contentText.text = curItem.content;

                //���õ� �̹��� �Ű��ֱ�
                equitObjectSprite.sprite = itemSprite[curItem.indexNum];
            }

            //������� �ܼ��� �ִٸ� usingClue�� ��ڴ�.
            Clue usingClue = curClueList.Find(x => x.isUsing == true);

            //���� ������� �ܼ��� �־��ٸ� �� ���� false�� �ٲٰڴ�.
            if (usingClue != null)
            {
                usingClue.isUsing = false;
            }

            //������� �������� �ѹ� �� Ŭ���ϸ� false�� �ٲٴ� �ڵ�
            if (usingItem != null)
            {
                if (usingItem.key == curItemList[slotNum].key)
                {
                    curItemList[slotNum].isUsing = false;

                    //���� ������Ʈ �̹��� ���ֱ�
                    equitObjectSprite.sprite = sprite_NoneImage;
                }
            }

            #endregion
        }

        //������Ʈ Ÿ���� �ܼ��� ���
        else if (curType == "Clue")
        {
            //�ܼ� â�� ���
            #region
            Clue curClue = curClueList[slotNum];
            Clue usingClue = curClueList.Find(x => x.isUsing == true);

            if (usingClue != null) usingClue.isUsing = false;
            {
                curClue.isUsing = true;

                //���õ� �ܼ����� ���� ǥ���ϱ�
                contentText.text = curClue.content;

                //����������� ���� ���õ� �ܼ� �̹����� �ٲ��ֱ�
                equitObjectSprite.sprite = clueSprite[curClue.indexNum];
            }

            //������� �������� �ִٸ� usingItem�� ��ڴ�.
            Item usingItem = curItemList.Find(x => x.isUsing == true);

            //���� ������� �������� �־��ٸ� �� �������� isUsing�� false�� �ٲٰڴ�.
            if (usingItem != null)
            {
                usingItem.isUsing = false;
            }

            //������� �ܼ��� �ѹ� �� Ŭ���ϸ� false�� �ٲٴ� �ڵ�
            if (usingClue != null)
            {
                if (usingClue.key == curClueList[slotNum].key)
                {
                    curClueList[slotNum].isUsing = false;

                    //���� ������Ʈ �̹��� ���ֱ�
                    equitObjectSprite.sprite = sprite_NoneImage;
                }
            }
            #endregion
        }

        TabClick(curType);
        //Save();
    }

    //������Ʈ ���� Ŭ����(����â ����)
    public void SlotClick2(int slotNum)
    {
        //������Ʈ Ÿ���� �������� ���
        if (curType == "Item")
        {
            //����â�� �������� ���
            #region
            Item curItem2 = curItemList2[slotNum];
            Item usingItem2 = curItemList2.Find(x => x.isUsing == true);

            //������� �������� ������ �����۵��� ����� false�� �ٲ�
            if (curItem2 != null)
            {
                //���õ� �����۾��� ���� ǥ���ϱ�
                contentText2.text = curItem2.content;
            }

            //������ �ǿ��� ����â 1�� ������ϰ��
            if (combineSlot1_Using)
            {
                //����â ���� 1�� �̹����� ������� �������� �̹����� ����
                image_CombineSlot1Item.sprite = itemSprite[curItem2.indexNum];
                //����â ���� 1�� ������ �̸� ����
                combineSlot1Name.text = curItem2.name;

                //curCombineClue1Info���� Key���� ���
                curCombineClue1Info = null;

                //curCombineItem1Info�� ������� �������� ������ �ѱ�
                curCombineItem1Info = curItem2;
            }

            //������ �ǿ��� ����â 2�� ������ϰ��
            else if (combineSlot2_Using)
            {
                //����â ���� 2�� �̹����� ������� �������� �̹����� ����
                image_CombineSlot2Item.sprite = itemSprite[curItem2.indexNum];
                //����â ���� 2�� ������ �̸� ����
                combineSlot2Name.text = curItem2.name;

                //curCombineClue2Info���� Key���� ���
                curCombineClue2Info = null;

                //curCombineItem2Info�� ������� �������� ������ �ѱ�
                curCombineItem2Info = curItem2;
            }

            #endregion
        }

        //������Ʈ Ÿ���� �ܼ��� ���
        else if (curType == "Clue")
        {
            //����â�� �ܼ� â�� ���
            #region
            Clue curClue2 = curClueList2[slotNum];
            Clue usingClue2 = curClueList2.Find(x => x.isUsing == true);

            if (curClueList2 != null)
            {
                //���õ� �ܼ����� ���� ǥ���ϱ�
                contentText2.text = curClue2.content;
            }

            //�ܼ� �ǿ��� ����â 1�� ������ϰ��
            if (combineSlot1_Using)
            {
                //����â ���� 1�� �̹����� ������� �ܼ��� �̹����� ����
                image_CombineSlot1Item.sprite = clueSprite[curClue2.indexNum];

                //����â ���� 1�� �ܼ� �̸�����
                combineSlot1Name.text = curClue2.name;

                //curCombineItem1Info���� key���� ���
                curCombineItem1Info = null;

                //curCombineClue1Info�� ������� �ܼ��� ������ �ѱ�
                curCombineClue1Info = curClue2;
            }

            //�ܼ� �ǿ��� ����â 2�� ������ϰ��
            else if (combineSlot2_Using)
            {
                //����â ���� 1�� �̹����� ������� �ܼ��� �̹����� ����
                image_CombineSlot2Item.sprite = clueSprite[curClue2.indexNum];
                //����â ���� 2�� �ܼ� �̸�����
                combineSlot2Name.text = curClue2.name;

                //curCombineItem2Info���� key���� ���
                curCombineItem2Info = null;

                //curCombineClue2Info�� ������� �ܼ��� ������ �ѱ�
                curCombineClue2Info = curClue2;
            }
            #endregion
        }

        //���� ����1�� ����2 ���� �����۵��� �����ϰ�.
        if (curCombineItem1Info != null && curCombineItem2Info != null)
        {
            //1�� ���԰� 2�������� �������� ���� 2�������� ������̶��
            if ((curCombineItem1Info.key == curCombineItem2Info.key) && combineSlot2_Using)
            {
                Debug.Log("1������ ����");
                //���� ���� ������ 1���� ����.
                EmptyCombineSlot1Item();
            }
        }

        //���� ����1�� ����2 ���� �����۵��� �����ϰ�.
        if (curCombineItem1Info != null && curCombineItem2Info != null)
        {
            //1�� ���԰� 2�������� �������� ���� 1�������� ������̶��
            if ((curCombineItem1Info.key == curCombineItem2Info.key) && combineSlot1_Using)
            {
                Debug.Log("2������ ����");

                //���� ���� ������ 2���� ����.
                EmptyCombineSlot2Item();
            }
        }

        //���� ����1�� ����2 ���� �ܼ����� �����ϰ�.
        if (curCombineClue1Info != null && curCombineClue2Info != null)
        {
            //1�� ���԰� 2�������� �ܼ��� ���� 1�������� ������̶��
            if ((curCombineClue1Info.key == curCombineClue2Info.key) && combineSlot2_Using)
            {
                Debug.Log("1������ ����");

                //���� ���� �ܼ� 2���� ����.
                EmptyCombineSlot1Clue();
            }
        }

        //���� ����1�� ����2 ���� �ܼ����� �����ϰ�.
        if (curCombineClue1Info != null && curCombineClue2Info != null)
        {
            //1�� ���԰� 2�������� �ܼ��� ���� 1�������� ������̶��
            if ((curCombineClue1Info.key == curCombineClue2Info.key) && combineSlot1_Using)
            {
                Debug.Log("2������ ����");

                //���� ���� �ܼ� 2���� ����.
                EmptyCombineSlot2Clue();
            }
        }

        TabClick2(curType);
        //Save();
    }

    //������Ʈ â������ Tab Ŭ��
    public void TabClick(string tabName)
    {
        //Ŭ���� Ÿ�Կ� ���缭 ������Ʈ ����Ʈ �ҷ�����
        curType = tabName;

        if (curType == "Item")
        {
            //������ â ����
            for (int i = 0; i < slot.Length; i++)
            {
                //������ �����ϴ��� Ȯ��
                bool isExist = i < curItemList.Count;
                //���� ���� ��Ȱ��ȭ
                slot[i].SetActive(isExist);
                //Text���̱�
                slot[i].GetComponentInChildren<Text>().text = isExist ? curItemList[i].name : "";

                //������ �����Ѵٸ�
                if (isExist)
                {
                    //�̹�����ü
                    objectImage[i].sprite = itemSprite[allItemList.FindIndex(x => x.name == curItemList[i].name)];
                    usingImage[i].SetActive(curItemList[i].isUsing);
                }
            }
            //�ܼ� ��ư �̹��� ��Ӱ�
            uiManagerScr.ChangeClueTapColor();
        }

        else if (curType == "Clue")
        {
            //�ܼ� ����
            for (int i = 0; i < slot.Length; i++)
            {
                //������ �����ϴ��� Ȯ��
                bool isExist = i < curClueList.Count;
                //���� ���� ��Ȱ��ȭ
                slot[i].SetActive(isExist);
                //Text���̱�
                slot[i].GetComponentInChildren<Text>().text = isExist ? curClueList[i].name : "";

                //������ �����Ѵٸ�
                if (isExist)
                {
                    //�̹�����ü
                    objectImage[i].sprite = clueSprite[allClueList.FindIndex(x => x.name == curClueList[i].name)];
                    usingImage[i].SetActive(curClueList[i].isUsing);
                }
            }
            //������ ��ư �̹��� ��Ӱ�
            uiManagerScr.ChangeItemTapColor();
        }
    }

    //����â������ Tab Ŭ��
    public void TabClick2(string tabName)
    {
        //Ŭ���� Ÿ�Կ� ���缭 ������Ʈ ����Ʈ �ҷ�����
        curType = tabName;

        if (curType == "Item")
        {
            //����â�� ������ ����
            for (int i = 0; i < slot2.Length; i++)
            {
                //������ �����ϴ��� Ȯ��
                bool isExist = i < curItemList2.Count;
                //���� ���� ��Ȱ��ȭ
                slot2[i].SetActive(isExist);
                //Text���̱�
                slot2[i].GetComponentInChildren<Text>().text = isExist ? curItemList2[i].name : "";

                //������ �����Ѵٸ�
                if (isExist)
                {
                    //�̹�����ü
                    objectImage2[i].sprite = itemSprite[allItemList.FindIndex(x => x.name == curItemList2[i].name)];
                    //usingImage2[i].SetActive(curItemList2[i].isUsing);
                }
            }

            //�ܼ� ��ư �̹��� ��Ӱ�
            uiManagerScr.ChangeCombineClueTapColor();
        }

        else if (curType == "Clue")
        {
            //����â �ܼ� ����
            for (int i = 0; i < slot2.Length; i++)
            {
                //������ �����ϴ��� Ȯ��
                bool isExist = i < curClueList2.Count;
                //���� ���� ��Ȱ��ȭ
                slot2[i].SetActive(isExist);
                //Text���̱�
                slot2[i].GetComponentInChildren<Text>().text = isExist ? curClueList2[i].name : "";

                //������ �����Ѵٸ�
                if (isExist)
                {
                    //�̹�����ü
                    objectImage2[i].sprite = clueSprite[allClueList.FindIndex(x => x.name == curClueList2[i].name)];
                    //usingImage2[i].SetActive(curClueList2[i].isUsing);
                }
            }
            //������ ��ư �̹��� ��Ӱ�
            uiManagerScr.ChangeCombineItemTapColor();
        }
    }

    //Data ����
    public void Save(int _slotNum)
    {
        //Json ������ ������ ����
        string jItemdata = JsonUtility.ToJson(new Serialization<Item>(allItemList));

        //json ������ ���� ����
        File.WriteAllText(itemfilePath + _slotNum.ToString(), jItemdata);

        //Json �ܼ� ������ ����
        string jCluedata = JsonUtility.ToJson(new Serialization<Clue>(allClueList));

        //json �ܼ� ���� ����
        File.WriteAllText(cluefilePath + _slotNum.ToString(), jCluedata);

        //���� �������� ������ ����Ʈ Json
        string jcurItmeData = JsonUtility.ToJson(new Serialization<Item>(curItemList));

        //���� �������� ������ JsonData ���� ����
        File.WriteAllText(curItemfilePath + _slotNum.ToString(), jcurItmeData);

        //���� �������� �ܼ� ����Ʈ Json
        string jcurClueData = JsonUtility.ToJson(new Serialization<Clue>(curClueList));

        //���� �������� �ܼ� JsonData ���� ����
        File.WriteAllText(curCluefilePath + _slotNum.ToString(), jcurClueData);

        //���� �������� ������ ����Ʈ �ҷ�����
        TabClick(curType);
        //����â���� ���� �������� ������ ����Ʈ �ҷ�����
        TabClick2(curType);
    }

    //Data �ҷ�����
    public void Load(int _slotNum)
    {
        //������ Json ������ ����
        string jItemdata = File.ReadAllText(itemfilePath + _slotNum.ToString());

        //������ Json���Ϸκ��� ������ ������ȭ(Load)
        myItemList = JsonUtility.FromJson<Serialization<Item>>(jItemdata).target;

        //�ܼ� Json ������ ����
        string jCluedata = File.ReadAllText(cluefilePath + _slotNum.ToString());
        //�ܼ� Json���Ϸκ��� ������ ������ȭ(Load)
        myClueList = JsonUtility.FromJson<Serialization<Clue>>(jCluedata).target;

        //�������� ������ Json �о����
        string jcurItemData = File.ReadAllText(curItemfilePath + _slotNum.ToString());

        //curItemList�� jcurItemData ����
        curItemList = JsonUtility.FromJson<Serialization<Item>>(jcurItemData).target;
        curItemList2 = JsonUtility.FromJson<Serialization<Item>>(jcurItemData).target;

        //�������� �ܼ� Json �о����
        string jcurClueData = File.ReadAllText(curCluefilePath + _slotNum.ToString());

        //curClueList�� jcurClueData ����
        curClueList = JsonUtility.FromJson<Serialization<Clue>>(jcurClueData).target;
        curClueList2 = JsonUtility.FromJson<Serialization<Clue>>(jcurClueData).target;


        //���� �������� ������ ����Ʈ �ҷ�����
        TabClick(curType);
        //����â���� ���� �������� ������ ����Ʈ �ҷ�����
        TabClick2(curType);
    }

    //Key�� ���ؼ� ������ ���
    public void GetItem(int _key)
    {
        //�ش� Key�� ���� ������Ʈ�� �����ϴ� ���
        if (myItemList.Find(x => x.key == _key) != null)
        {
            Item GetItem = myItemList.Find(x => x.key == _key);
            Item CheckItem = curItemList.Find(x => x.key == _key);

            //�ش� �������� ���������� �ʴٸ�
            if(CheckItem == null)
            {
                Debug.Log($"{_key}�� ������ ȹ��");
                curItemList.Add(myItemList.Find(x => x.key == _key));
                curItemList2.Add(myItemList.Find(x => x.key == _key));
                TabClick(curType);
                TabClick2(curType);


                //������ ȹ���� ���¶��
                if (ObjectControll.instance.getBotzime)
                {
                    Debug.Log("�佺Ʈ�޼��� ����");
                    //�佺Ʈ �޼��� ����
                    toastMessageScr.ToastMessageStart();

                    //�佺Ʈ �޼��� ������ �Ѱ��ֱ�
                    toastMessageScr.ToastMessageInfo_Chage($"{GetItem.name} ȹ��", itemSprite[GetItem.indexNum], GetItem.name);
                }
            }
        }
            
    }

    //Key�� ���ؼ� ������ ����
    public void RemoveItem(int _key)
    {
        //�ش� Key�� ���� ������Ʈ�� �����ϴ� ���
        if (curItemList.Find(x => x.key == _key) != null)
        {
            //������ ������
            Item Item = curItemList.Find(x => x.key == _key);

            Debug.Log($"{_key} ������ ����");
            curItemList.Remove(curItemList.Find(x => x.key == _key));
            curItemList2.Remove(curItemList2.Find(x => x.key == _key));
            TabClick(curType);
            TabClick2(curType);

            //�������� �������� �����������
            if(equitObjectSprite.sprite == itemSprite[Item.indexNum])
            {
                //�������� ������ �̹��� ����
                equitObjectSprite.sprite = sprite_NoneImage;
            }
        }
    }

    //Key�� ���ؼ� �ܼ� ���
    public void GetClue(int _key)
    {
        //�ش� Key�� ���� �ܼ��� �����ϴ� ���
        if (myClueList.Find(x => x.key == _key) != null)
        {
            Clue GetClue = myClueList.Find(x => x.key == _key);
            Clue CheckClue = curClueList.Find(x => x.key == _key);

            //���� �ش� �ܼ��� ���������� �ʴٸ�
            if (CheckClue == null)
            {
                curClueList.Add(myClueList.Find(x => x.key == _key));
                curClueList2.Add(myClueList.Find(x => x.key == _key));
                TabClick(curType);
                TabClick2(curType);

                isGetClue = true;

                //�佺Ʈ �޼��� ����
                toastMessageScr.ToastMessageStart();

                //�佺Ʈ �޼��� ������ �Ѱ��ֱ�
                toastMessageScr.ToastMessageInfo_Chage($"{GetClue.name} ȹ��", clueSprite[GetClue.indexNum], GetClue.name);
            }
          
        }
    }

    //Key�� ���ؼ� �ܼ� ����
    public void RemoveClue(int _key)
    {
        //�ش� Key�� ���� �ܼ��� �����ϴ� ���
        if (curClueList.Find(x => x.key == _key) != null)
        {
            //������ �ܼ�
            Clue clue = curClueList.Find(x => x.key == _key);

            curClueList.Remove(curClueList.Find(x => x.key == _key));
            curClueList2.Remove(curClueList2.Find(x => x.key == _key));
            TabClick(curType);
            TabClick2(curType);

            //������ �ܼ��� �������� ���
            if(equitObjectSprite.sprite == clueSprite[clue.indexNum])
            {
                //�������� �ܼ� �̹��� ����
                equitObjectSprite.sprite = sprite_NoneImage;
            }
        }
    }

    //����â ���� Ŭ��
    public void CombineSlot(int slotNum)
    {
        //1�������� ��������� �ʰ� ������ ���
        if (slotNum == 1 && !combineSlot1_Using)
        {
            //���� 1�� Sprite�� UsingSprite�� ��ü
            image_CombineSlot1.sprite = sprite_UsingSlot;
            //���� 2�� Sprite�� ���� �̹����� ����
            image_CombineSlot2.sprite = sprite_Slot;

            ////1�� ����â �����
            Invoke("CombineSlot1True", 0.1f);
            //2�� ����â ��� ���
            combineSlot2_Using = false;
        }

        //2�� ������ ��������� �ʰ� ������ ���
        else if (slotNum == 2 && !combineSlot2_Using)
        {
            //���� 2�� Sprite�� UsingSprite�� ��ü
            image_CombineSlot2.sprite = sprite_UsingSlot;
            //���� 1�� Sprite�� ���� �̹����� ����
            image_CombineSlot1.sprite = sprite_Slot;

            //2�� ����â �����
            Invoke("CombineSlot2True", 0.1f);
            //1�� ����â ��� ���
            combineSlot1_Using = false;
        }

        //����1�� ������ε� �ѹ� �� ������ ���
        if (slotNum == 1 && combineSlot1_Using)
        {
            //���� 1�� ��� ���
            combineSlot1_Using = false;
            //���� 1�� Sprite�� ���� �̹����� ����
            image_CombineSlot1.sprite = sprite_Slot;
        }

        //����2�� ������ε� �ѹ� �� ������ ���
        else if (slotNum == 2 && combineSlot2_Using)
        {
            //���� 2�� ��� ���
            combineSlot2_Using = false;
            //���� 2�� Sprite�� ���� �̹����� ����
            image_CombineSlot2.sprite = sprite_Slot;
        }
    }

    //���� ��ư�� ������ ���
    public void CombineButton()
    {

        //���� 1: Item, ���� 2: Item
        #region
        if (curCombineItem1Info != null && curCombineItem2Info != null)
        {
            if (curCombineItem1Info.key != 0 && curCombineItem2Info.key != 0)
            {
                Debug.Log("�����۳����� ����");
                //����1,����2�� Key�� ��
                int sumKeyValue = curCombineItem1Info.key + curCombineItem2Info.key;

                //����1,����2�� Type�� ��
                string slotSumType = curCombineItem1Info.type + curCombineItem2Info.type;

                //������ ������Ʈ�� Type
                string sumObjectType = "";

                //myItemList���� �ش� SumKey���� ���� �������� �������
                if (myItemList.Find(x => x.key == sumKeyValue) != null)
                {
                    Item sumItem = myItemList.Find(x => x.key == sumKeyValue);
                    //������ �������� Type
                    sumObjectType = sumItem.type;
                }

                //Sumkey�� ã�� ������Ʈ�� Ÿ���� ����â�� �� ������Ʈ�� Ÿ���� ���� ���� �������.
                if (sumObjectType == slotSumType)
                {
                    //�ش� Key���� ���� ������ ȹ��
                    GetItem(sumKeyValue);

                    //���տ� ���� ������Ʈ ����
                    RemoveItem(curCombineItem1Info.key);
                    RemoveItem(curCombineItem2Info.key);

                    //���� ����
                    EmptyCombineSlot();

                    //����â Textǥ��
                    contentText2.text = "���� ����!";
                }

                //���յ� �������� �������� �ʴٸ�
                else if (sumObjectType == "")
                {
                    Debug.Log("�ؽ�Ʈ �־���");

                    //���� ���� ����
                    EmptyCombineSlot();

                    contentText2.text = "���� ����!";
                }
            }
        }
        #endregion

        //���� 1: Clue, ���� 2: Clue
        #region
        if (curCombineClue1Info != null && curCombineClue2Info != null)
        {
            if (curCombineClue1Info.key != 0 && curCombineClue2Info.key != 0)
            {
                Debug.Log("�ܼ������� ����");

                //����1,����2�� Key�� ��
                int sumKeyValue = curCombineClue1Info.key + curCombineClue2Info.key;

                //����1,����2�� Type�� ��
                string slotSumType = "";

                #region ����ó��
                //���� ���ս��� 1��,2���� Ű���� 2022,4023�̶��
                if((curCombineClue1Info.key == 2022 || curCombineClue1Info.key == 4023) && 
                    (curCombineClue2Info.key == 2022 || curCombineClue2Info.key == 4023))
                {
                    Debug.Log("�ٴٿ� ���� û��");
                    //����1, ����2�� Type�� ��
                    slotSumType = "ClueClue";
                }

                //���� ���ս��� 1��,2���� �ܼ��� Type�� ClueClue ���
                else if (curCombineClue1Info.type == "ClueClue" && curCombineClue2Info.type == "ClueClue")
                {
                    //����1,����2�� Type�� ��
                    slotSumType = "ClueClue";
                }

                //�׿��� ���
                else
                {
                    //����1,����2�� Type�� ��
                    slotSumType = curCombineClue1Info.type + curCombineClue2Info.type;
                }

                //���� ���ս��� 1����,2���� �ܼ��� 2001 �Ǵ� 2016�� ���
                if((curCombineClue1Info.key == 2001 || curCombineClue1Info.key == 2016) && 
                    (curCombineClue2Info.key == 2001 || curCombineClue2Info.key == 2016))
                {
                    //���� Ű ���� 9000
                    sumKeyValue = 9000;
                }
                #endregion

                //������ ������Ʈ�� Type
                string sumObjectType = "";

                Debug.Log($"���յ� �ܼ��� Ű�� {sumKeyValue}");
                Debug.Log($"���յ� �ܼ��� Ÿ�԰� {slotSumType}");

                //myItemList���� �ش� SumKey���� ���� �ܼ� �������
                if (myClueList.Find(x => x.key == sumKeyValue) != null)
                {
                    Debug.Log("�ش� �ܼ��� �����Ϳ� ����");
                    Clue sumClue = myClueList.Find(x => x.key == sumKeyValue);
                    //������ �ܼ��� Type
                    sumObjectType = sumClue.type;
                    Debug.Log($"������ �ܼ��� Type: {sumObjectType}");
                }

                //Sumkey�� ã�� ������Ʈ�� Ÿ���� ����â�� �� ������Ʈ�� Ÿ���� ���� ���� �������.
                if (sumObjectType == slotSumType)
                {
                    //�ش� Key���� ���� �ܼ� ȹ��
                    GetClue(sumKeyValue);

                    ////���տ� ���� �ܼ� ����
                    //RemoveClue(curCombineClue1Info.key);
                    //RemoveClue(curCombineClue2Info.key);

                    //���� ����
                    EmptyCombineSlot();

                    //����â Textǥ��
                    contentText2.text = "���� ����!";
                }

                //���յ� �ܼ��� �������� �ʴٸ�
                else if (sumObjectType == "")
                {
                    Debug.Log("�ؽ�Ʈ �־���");

                    //���� ���� ����
                    EmptyCombineSlot();

                    contentText2.text = "���� ����!";
                }
            }
        }
        #endregion

        //���� 1: Item, ���� 2: Clue
        #region
        if (curCombineItem1Info != null && curCombineClue2Info != null)
        {
            if (curCombineItem1Info.key != 0 && curCombineClue2Info.key != 0)
            {
                Debug.Log("�����۰� �ܼ��� ����");

                //����1,����2�� Key�� ��
                int sumKeyValue = curCombineItem1Info.key + curCombineClue2Info.key;

                //����1,����2�� Type�� ��
                string slotSumType = curCombineItem1Info.type + curCombineClue2Info.type;

                //������ ������Ʈ�� Type
                string sumObjectType = "";

                //myItemList���� �ش� SumKey���� ���� ������ �������
                if (myItemList.Find(x => x.key == sumKeyValue) != null)
                {
                    Item sumItem = myItemList.Find(x => x.key == sumKeyValue);
                    //������ �ܼ��� Type
                    sumObjectType = sumItem.type;

                    //Sumkey�� ã�� ������Ʈ�� Ÿ���� ����â�� �� ������Ʈ�� Ÿ���� ���� ���� �������.
                    if (sumObjectType == slotSumType && sumItem.key != 0)
                    {
                        Debug.Log("���������� ������");
                        //�ش� Key���� ���� �ܼ� ȹ��
                        GetItem(sumKeyValue);

                        //���տ� ���� ������Ʈ ����
                        RemoveItem(curCombineItem1Info.key);
                        RemoveClue(curCombineClue2Info.key);

                        //���� ����
                        EmptyCombineSlot();

                        //����â Textǥ��
                        contentText2.text = "���� ����!";
                    }
                }

                //myClueList���� �ش� SumKey���� ���� �ܼ��� �������
                else if (myClueList.Find(x => x.key == sumKeyValue) != null)
                {
                    Clue sumClue = myClueList.Find(x => x.key == sumKeyValue);
                    //������ �ܼ��� Type
                    sumObjectType = sumClue.type;

                    //Sumkey�� ã�� ������Ʈ�� Ÿ���� ����â�� �� ������Ʈ�� Ÿ���� ���� ���� �������.
                    if (sumObjectType == slotSumType && sumClue.key != 0)
                    {
                        Debug.Log("�ܼ��� ������");

                        //�ش� Key���� ���� �ܼ� ȹ��
                        GetClue(sumKeyValue);

                        //���տ� ���� ������Ʈ ����
                        RemoveItem(curCombineItem1Info.key);
                        RemoveClue(curCombineClue2Info.key);

                        //���� ����
                        EmptyCombineSlot();

                        //����â Textǥ��
                        contentText2.text = "���� ����!";
                    }
                }

                //���յ� �ܼ��� �������� �ʴٸ�
                if (sumObjectType == "")
                {
                    Debug.Log("�ؽ�Ʈ �־���");

                    //���� ���� ����
                    EmptyCombineSlot();

                    contentText2.text = "���� ����!";
                }
            }
        }
        #endregion

        //���� 1: Clue, ���� 2: Item
        #region
        if (curCombineClue1Info != null && curCombineItem2Info != null)
        {
            if (curCombineClue1Info.key != 0 && curCombineItem2Info.key != 0)
            {
                Debug.Log("�ܼ��� �������� ����");

                //����1,����2�� Key�� ��
                int sumKeyValue = curCombineClue1Info.key + curCombineItem2Info.key;

                //����1,����2�� Type�� ��
                string slotSumType = curCombineItem2Info.type + curCombineClue1Info.type;

                //������ ������Ʈ�� Type
                string sumObjectType = "";

                //myItemList���� �ش� SumKey���� ���� ������ �������
                if (myItemList.Find(x => x.key == sumKeyValue) != null)
                {
                    Item sumItem = myItemList.Find(x => x.key == sumKeyValue);
                    //������ �ܼ��� Type
                    sumObjectType = sumItem.type;

                    //Sumkey�� ã�� ������Ʈ�� Ÿ���� ����â�� �� ������Ʈ�� Ÿ���� ���� ���� �������.
                    if (sumObjectType == slotSumType && sumItem.key != 0)
                    {
                        Debug.Log("���������� ������");
                        //�ش� Key���� ���� �ܼ� ȹ��
                        GetItem(sumKeyValue);

                        //���տ� ���� ������Ʈ ����
                        RemoveClue(curCombineClue1Info.key);
                        RemoveItem(curCombineItem2Info.key);

                        //���� ����
                        EmptyCombineSlot();

                        //����â Textǥ��
                        contentText2.text = "���� ����!";
                    }
                }

                //myClueList���� �ش� SumKey���� ���� �ܼ��� �������
                else if (myClueList.Find(x => x.key == sumKeyValue) != null)
                {
                    Clue sumClue = myClueList.Find(x => x.key == sumKeyValue);
                    //������ �ܼ��� Type
                    sumObjectType = sumClue.type;

                    //Sumkey�� ã�� ������Ʈ�� Ÿ���� ����â�� �� ������Ʈ�� Ÿ���� ���� ���� �������.
                    if (sumObjectType == slotSumType && sumClue.key != 0)
                    {
                        Debug.Log("�ܼ��� ������");

                        //�ش� Key���� ���� �ܼ� ȹ��
                        GetClue(sumKeyValue);

                        //���տ� ���� ������Ʈ ����
                        RemoveClue(curCombineClue1Info.key);
                        RemoveItem(curCombineItem2Info.key);

                        //���� ����
                        EmptyCombineSlot();

                        //����â Textǥ��
                        contentText2.text = "���� ����!";
                    }
                }

                //���յ� �ܼ��� �������� �ʴٸ�
                if (sumObjectType == "")
                {
                    Debug.Log("�ؽ�Ʈ �־���");

                    //���� ���� ����
                    EmptyCombineSlot();

                    contentText2.text = "���� ����!";
                }
            }
        }

        #endregion
    }

    //���� ���� ����
    private void EmptyCombineSlot()
    {
        //����â 2���� ����� ����
        combineSlot1_Using = false;
        combineSlot2_Using = false;

        //����â ���� �������� Key���� �����
        curCombineItem1Info = null;
        curCombineItem2Info = null;
        curCombineClue1Info = null;
        curCombineClue2Info = null;

        //����â ������ ������Ʈ �̹��� ����
        image_CombineSlot1Item.sprite = sprite_NoneImage;
        image_CombineSlot2Item.sprite = sprite_NoneImage;

        //����â ������ Text ����
        combineSlot1Name.text = "";
        combineSlot2Name.text = "";

        //����â ������ ����̹��� �ʱ�ȭ
        image_CombineSlot1.sprite = sprite_Slot;
        image_CombineSlot2.sprite = sprite_Slot;

        //Textâ ����
        contentText2.text = "�̰��� ����â�Դϴ�.";
    }


    //���� ���� 1���� True�� �ٲ��ִ� �޼���
    private void CombineSlot1True()
    {
        combineSlot1_Using = true;
    }

    //���� ���� 2���� True�� �ٲ��ִ� �޼���
    private void CombineSlot2True()
    {
        combineSlot2_Using = true;
    }


    //���ս��� 1���� �������� ���� �޼���
    private void EmptyCombineSlot1Item()
    {
        //1�� �������� �����
        curCombineItem1Info = null;
        //1�� ������ ������ �̹����� �����.
        image_CombineSlot1Item.sprite = sprite_NoneImage;
        //1�� ������ ������ �̸��� �����
        combineSlot1Name.text = "";
    }

    //���ս��� 2���� �������� ���� �޼���
    private void EmptyCombineSlot2Item()
    {
        //2�� �������� �����
        curCombineItem2Info = null;
        //2�� ������ ������ �̹����� �����.
        image_CombineSlot2Item.sprite = sprite_NoneImage;
        //2�� ������ ������ �̸��� �����
        combineSlot2Name.text = "";
    }

    //���ս��� 1���� �ܼ��� ���� �޼���
    private void EmptyCombineSlot1Clue()
    {
        //1�� �������� �����
        curCombineClue1Info = null;
        //1�� ������ �ܼ� �̹����� �����.
        image_CombineSlot1Item.sprite = sprite_NoneImage;
        //1�� ������ �ܼ� �̸��� �����
        combineSlot1Name.text = "";
    }

    //���ս��� 2���� �ܼ��� ���� �޼���
    private void EmptyCombineSlot2Clue()
    {
        //2�� �������� �����
        curCombineClue2Info = null;
        //2�� ������ �ܼ� �̹����� �����.
        image_CombineSlot2Item.sprite = sprite_NoneImage;
        //2�� ������ �ܼ� �̸��� �����
        combineSlot2Name.text = "";
    }

    //���� ���� 1���� ������Ʈ �ֱ�
    public void InputCombinSlot1(int _slotNum)
    {
        //���� Item Tap�� ���
        if (curType == "Item")
        {
            //���� ���� 1���� ������ �̹��� ����
            image_CombineSlot1Item.sprite = itemSprite[curItemList2[_slotNum].indexNum];

            //���� ���� 1�� �̸� �ٲٱ�
            combineSlot1Name.text = curItemList2[_slotNum].name;

            //���� ���� 1���� ���� �ֱ�
            curCombineItem1Info = curItemList2[_slotNum];

            //���� �ܼ� 1�� ���� ����
            curCombineClue1Info = null;


            if (curCombineItem1Info != null && curCombineItem2Info != null)
            {
                if (curCombineItem1Info.key == curCombineItem2Info.key)
                {
                    //2�� ���� ������ ����
                    EmptyCombineSlot2Item();
                }
            }
        }

        //���� Clue Tap�� ���
        else if (curType == "Clue")
        {
            //���� ���� 1���� �ܼ� �̹��� ����
            image_CombineSlot1Item.sprite = clueSprite[curClueList2[_slotNum].indexNum];

            //���� ���� 1�� �̸� �ٲٱ�
            combineSlot1Name.text = curClueList2[_slotNum].name;

            //���� ���� 1���� ���� �ֱ�
            curCombineClue1Info = curClueList2[_slotNum];

            //���� ������ 1�� ���� ����
            curCombineItem1Info = null;

            if (curCombineClue1Info != null && curCombineClue2Info != null)
            {
                //���� ���� 1��,2���� �ܼ��� ���� ���
                if (curCombineClue1Info.key == curCombineClue2Info.key)
                {
                    //2�� ���� �ܼ� ����
                    EmptyCombineSlot2Clue();
                }
            }
        }
    }

    //���� ���� 2���� ������Ʈ �ֱ�
    public void InputCombinSlot2(int _slotNum)
    {
        if (curType == "Item")
        {
            //���� ���� 2���� ������ �̹��� ����
            image_CombineSlot2Item.sprite = itemSprite[curItemList2[_slotNum].indexNum];

            //���� ���� 2�� �̸� �ٲٱ�
            combineSlot2Name.text = curItemList2[_slotNum].name;

            //���� ���� 2���� ���� �ֱ�
            curCombineItem2Info = curItemList2[_slotNum];

            //���� �ܼ� 2�� ���� ����
            curCombineClue2Info = null;

            if (curCombineItem1Info != null && curCombineItem2Info != null)
            {
                //���� ���� 1��,2���� ���� �������� ����������
                if (curCombineItem1Info.key == curCombineItem2Info.key)
                {
                    //1�� ���� ������ ����
                    EmptyCombineSlot1Item();
                }
            }
        }

        //���� Clue Tap�� ���
        else if (curType == "Clue")
        {
            //���� ���� 2���� �ܼ� �̹��� ����
            image_CombineSlot2Item.sprite = clueSprite[curClueList2[_slotNum].indexNum];

            //���� ���� 2�� �̸� �ٲٱ�
            combineSlot2Name.text = curClueList2[_slotNum].name;

            //���� ���� 2���� ���� �ֱ�
            curCombineClue2Info = curClueList2[_slotNum];

            //���� ������ 2�� ���� ����
            curCombineItem2Info = null;

            if (curCombineClue1Info != null && curCombineClue2Info != null)
            {
                //���� ���� 1��,2���� �ܼ��� ���� ���
                if (curCombineClue1Info.key == curCombineClue2Info.key)
                {
                    //1�� ���� �ܼ� ����
                    EmptyCombineSlot1Clue();
                }
            }
        }
    }


    //������ ������Ʈ�� Key ���� ��ȯ���ִ� �޼��� (���� ������ key��, ������ 0)
    public int GetEquipObjectKey()
    {
        //�������� ������ ����Ʈ�� ������� �ʴٸ�
        if (curItemList != null)
        {
            //������� ������ Ŭ���� �ֱ�
            Item usingItem = curItemList.Find(x => x.isUsing == true);

            //������� �������� �־��ٸ�
            if (usingItem != null)
            {
                //������� ������ Ű�� ��ȯ
                return usingItem.key;
            }
        }

        //�������� �ܼ� ����Ʈ�� ������� �ʴٸ�
        if (curClueList != null)
        {
            //������� �ܼ� Ŭ���� �ֱ�
            Clue usingclue = curClueList.Find(x => x.isUsing == true);

            //������� �ܼ��� �־��ٸ�
            if (usingclue != null)
            {
                //������� �ܼ� Ű�� ��ȯ
                return usingclue.key;
            }
        }

        //������� ������Ʈ�� ���� ���
        return 0;
    }

    //��� ������ ȹ���ϱ�
    public void GetAllItem()
    {
        for (int i = 0; i < allItemList.Count; i++)
        {
            GetItem(allItemList[i].key);
        }
    }

    //��� �ܼ� ȹ���ϱ�
    public void GetAllClue()
    {
        for (int i = 0; i < allClueList.Count; i++)
        {
            GetClue(allClueList[i].key);
        }
    }
}
