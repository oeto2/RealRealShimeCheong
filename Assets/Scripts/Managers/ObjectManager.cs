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

    //Item
    #region 
    //������ ������ ���� ��� txt����
    public TextAsset itemDataBase;
    //Item Ŭ���� ���� ������ �����͵��� ����Ʈȭ ��Ų ��
    public List<Item> allItemList;

    //���ӳ����� ����� ������ ����Ʈ (������â ����)
    public List<Item> myItemList;

    //���ӳ����� ����� ������ ����Ʈ (����â ����)
    public List<Item> combinItemList;

    //���Կ��� ������ ������ ����Ʈ (���� ������)
    public List<Item> curItemList;
    //����â���� ������ ������ ����Ʈ (���� ������)
    public List<Item> curItemList2;
    #endregion

    //�ܼ� ������ ���� ��� txt����
    public TextAsset clueDataBase;

    //Clue Ŭ���� ���� �ܼ� �����͵��� ����Ʈȭ ��Ų��
    public List<Clue> allClueList;

    //���ӳ����� ����� �ܼ� ����Ʈ
    public List<Clue> myClueList;

    //���ӳ����� ����� �ܼ� ����Ʈ(����â ����)
    public List<Clue> combineCluList;


    //���Կ��� ������ �ܼ� ����Ʈ(���� ������)
    public List<Clue> curClueList;
    //����â �������� �ܼ� ����Ʈ(���� ������)
    public List<Clue> curClueList2;

    //������ Json�� ����� ��ġ
    public string itemfilePath;

    //�ܼ� Json�� ����� ��ġ
    public string cluefilePath;

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
        itemfilePath = Application.persistentDataPath + "/MyItemText.txt";

        //�ܼ� Json ������ ����� ��ġ
        cluefilePath = Application.persistentDataPath + "/MyClueText.txt";

        Save();
        Load();

        ////�ش� Ű���� ���� ������Ʈ ���
        //GetItem(1000);
        //GetItem(1001);
        //GetItem(1002);

        //GetClue(2003);
        //GetClue(2002);
        //GetClue(2001);
        //GetClue(2000);

        ////�ش� Ű ���� ���� ������Ʈ ����
        //RemoveClue(2000);
        //RemoveItem(1000);

        //������ �� �⺻���� �����ֱ�
        TabClick(curType);
        //����â ������ �� �⺻���� �����ֱ�
        TabClick2(curType);
    }


    private void Update()
    {
        //����â�� ������ �ʾ��� ���
        if (!uiManagerScr.gameObject_CombineWindow.activeSelf)
        {
            //���� ���� ����
            EmptyCombineSlot();
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
            #endregion
        }
        Save();
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
            if (usingItem2 != null) usingItem2.isUsing = false;
            {
                curItem2.isUsing = true;

                //���õ� �����۾��� ���� ǥ���ϱ�
                contentText2.text = curItem2.content;

                //���õ� �̹��� �Ű��ֱ�
                equitObjectSprite.sprite = itemSprite[curItem2.indexNum];
            }

            //������� �ܼ��� �ִٸ� usingClue�� ��ڴ�.
            Clue usingClue2 = curClueList2.Find(x => x.isUsing == true);

            //���� ������� �ܼ��� �־��ٸ� �� ���� false�� �ٲٰڴ�.
            if (usingClue2 != null)
            {
                usingClue2.isUsing = false;
            }

            //������ �ǿ��� ����â 1�� ������ϰ��
            if (combineSlot1_Using)
            {
                //����â ���� 1�� �̹����� ������� �������� �̹����� ����
                image_CombineSlot1Item.sprite = itemSprite[curItem2.indexNum];
                //����â ���� 1�� ������ �̸� ����
                combineSlot1Name.text = curItem2.name;

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

            if (usingClue2 != null) usingClue2.isUsing = false;
            {
                curClue2.isUsing = true;

                //���õ� �ܼ����� ���� ǥ���ϱ�
                contentText2.text = curClue2.content;

                //����������� ���� ���õ� �ܼ� �̹����� �ٲ��ֱ�
                equitObjectSprite.sprite = clueSprite[curClue2.indexNum];
            }

            //������� �������� �ִٸ� usingItem�� ��ڴ�.
            Item usingItem2 = curItemList2.Find(x => x.isUsing == true);

            //���� ������� �������� �־��ٸ� �� �������� isUsing�� false�� �ٲٰڴ�.
            if (usingItem2 != null)
            {
                usingItem2.isUsing = false;
            }

            //�ܼ� �ǿ��� ����â 1�� ������ϰ��
            if (combineSlot1_Using)
            {
                //����â ���� 1�� �̹����� ������� �������� �̹����� ����
                image_CombineSlot1Item.sprite = itemSprite[curClue2.indexNum];
                //����â ���� 1�� �ܼ� �̸�����
                combineSlot1Name.text = curClue2.name;
            }

            //�ܼ� �ǿ��� ����â 2�� ������ϰ��
            else if (combineSlot2_Using)
            {
                //����â ���� 1�� �̹����� ������� �������� �̹����� ����
                image_CombineSlot2Item.sprite = itemSprite[curClue2.indexNum];
                //����â ���� 2�� �ܼ� �̸�����
                combineSlot2Name.text = curClue2.name;
            }

            #endregion
        }

        Save();
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
                    usingImage2[i].SetActive(curClueList2[i].isUsing);
                }
            }
            //������ ��ư �̹��� ��Ӱ�
            uiManagerScr.ChangeCombineItemTapColor();
        }
    }

    //Data ����
    void Save()
    {
        //Json ������ ������ ����
        string jItemdata = JsonUtility.ToJson(new Serialization<Item>(allItemList));

        //json ������ ���� ����
        File.WriteAllText(itemfilePath, jItemdata);

        //Json �ܼ� ������ ����
        string jCluedata = JsonUtility.ToJson(new Serialization<Clue>(allClueList));

        //json �ܼ� ���� ����
        File.WriteAllText(cluefilePath, jCluedata);

        //���� �������� ������ ����Ʈ �ҷ�����
        TabClick(curType);
        //����â���� ���� �������� ������ ����Ʈ �ҷ�����
        TabClick2(curType);
    }

    //Data �ҷ�����
    void Load()
    {
        //������ Json ������ ����
        string jItemdata = File.ReadAllText(itemfilePath);

        //������ Json���Ϸκ��� ������ ������ȭ(Load)
        myItemList = JsonUtility.FromJson<Serialization<Item>>(jItemdata).target;

        //Json���Ϸκ��� �����۸���Ʈ �������� (����â ����)
        combinItemList = JsonUtility.FromJson<Serialization<Item>>(jItemdata).target;

        //���� �������� ������ ����Ʈ �ҷ�����
        TabClick(curType);
        //����â���� ���� �������� ������ ����Ʈ �ҷ�����
        TabClick2(curType);

        //�ܼ� Json ������ ����
        string jCluedata = File.ReadAllText(cluefilePath);
        //�ܼ� Json���Ϸκ��� ������ ������ȭ(Load)
        myClueList = JsonUtility.FromJson<Serialization<Clue>>(jCluedata).target;
        //�ܼ� Json���Ϸκ��� �ܼ�����Ʈ �������� (����â ����)
        combineCluList = JsonUtility.FromJson<Serialization<Clue>>(jCluedata).target;
    }

    //Key�� ���ؼ� ������ ���
    public void GetItem(int _key)
    {
        curItemList.Add(myItemList.Find(x => x.key == _key));
        curItemList2.Add(myItemList.Find(x => x.key == _key));
        TabClick(curType);
        TabClick2(curType);
    }

    //Key�� ���ؼ� ������ ����
    public void RemoveItem(int _key)
    {
        curItemList.Remove(myItemList.Find(x => x.key == _key));
        curItemList2.Remove(myItemList.Find(x => x.key == _key));
        TabClick(curType);
        TabClick2(curType);
    }

    //Key�� ���ؼ� �ܼ� ���
    public void GetClue(int _key)
    {
        curClueList.Add(myClueList.Find(x => x.key == _key));
        curClueList2.Add(myClueList.Find(x => x.key == _key));
        TabClick(curType);
        TabClick2(curType);
    }

    //Key�� ���ؼ� �ܼ� ����
    public void RemoveClue(int _key)
    {
        curClueList.Remove(myClueList.Find(x => x.key == _key));
        curClueList2.Remove(myClueList.Find(x => x.key == _key));
        TabClick(curType);
        TabClick2(curType);
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

            //1�� ����â �����
            combineSlot1_Using = true;
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
            combineSlot2_Using = true;
            //1�� ����â ��� ���
            combineSlot1_Using = false;
        }

        ////1�� ������ ������ε� 1�������� ������ ���
        //if (slotNum == 1 && combineSlot1_Using)
        //{
        //    //���� 1�� Sprite�� �⺻ ���� Sprite�� ��ü
        //    image_CombineSlot1.sprite = sprite_Slot;

        //    //1�� ����â �����
        //    combineSlot1_Using = false;
        //}

        ////2�� ������ ������ε� 2�������� ������ ���
        //if (slotNum == 2 && combineSlot2_Using)
        //{
        //    //���� 2�� Sprite�� �⺻ ���� Sprite�� ��ü
        //    image_CombineSlot2.sprite = sprite_Slot;

        //    //1�� ����â �����
        //    combineSlot2_Using = false;
        //}
    }

    //���� ��ư�� ������ ���
    public void CombineButton()
    {
        //����â 2���� ������� ��� (�������� �������)
        if ((curCombineItem1Info.key != 0 && curCombineItem2Info.key != 0))
        {
            Debug.Log("���� ����");
            //����1,����2�� Key�� ��
            int sumKeyValue = curCombineItem1Info.key + curCombineItem2Info.key;
            //����1,����2�� Type�� ��
            string sumType = "";

            //���� 1������ ������Ʈ Ÿ���� �������̶��
            if (curCombineItem1Info.type == "Item")
            {
                sumType = curCombineItem1Info.type + curCombineItem2Info.type;
            }


            //���� 1������ ������Ʈ Ÿ���� �ܼ����
            else if (curCombineItem1Info.type == "Clue")
            {
                //�������� ����
                sumType = curCombineItem2Info.name + curCombineItem1Info.name;
            }

            Debug.Log(sumType);


            //������ �������� Type
            string sumItemType = "";

            //������ �������� Key���� ���� �������� �־��ٸ�
            if (myItemList.Find(x => x.key == sumKeyValue) != null)
            {
                //��ģ Key���� ���� �������� ������ sumItem�� ��ڴ�.
                Item sumItem = myItemList.Find(x => x.key == sumKeyValue);
                //������ �������� Type
                sumItemType = sumItem.type;
            }

            Debug.Log("������ �������� Key��:" + sumKeyValue);
            Debug.Log("������ �������� Ÿ��:" + sumItemType);

            //���� ���յ� �������� Type�� ���ٸ�
            if (sumItemType == sumType)
            {
                //���տ� ���� �����۵��� ������̿����ٸ�
                if(curCombineItem1Info.isUsing || curCombineItem2Info.isUsing)
                {
                    //������ â�� Text ����ֱ�
                    contentText.text = "";
                }

                //�ش� Key���� ���� ������ ȹ��
                GetItem(sumKeyValue);
                //���տ� ���� ������ ����
                RemoveItem(curCombineItem1Info.key);
                RemoveItem(curCombineItem2Info.key);
                //���� ����
                EmptyCombineSlot();

                contentText2.text = "���� ����!";
            }

            //���յ� �������� �������� �ʴٸ�
            else if (sumItemType == "")
            {
                Debug.Log("�ؽ�Ʈ �־���");

                //���� ���� ����
                EmptyCombineSlot();

                contentText2.text = "���� ����!";
            }
        }
    }

    //���� ���� ����
    private void EmptyCombineSlot()
    {
        //����â 2���� ����� ����
        combineSlot1_Using = false;
        combineSlot2_Using = false;

        //����â ���� �������� �����
        curCombineItem1Info = null;
        curCombineItem2Info = null;

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
}
