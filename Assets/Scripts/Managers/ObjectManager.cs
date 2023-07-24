using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//List를 Jason으로 저장할수 있게도와주는 Class
[System.Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}

//Item 데이터 베이스
[System.Serializable]
public class Item
{
    //생성자 정의
    public Item(int _key, string _type, string _name, string _content, bool _isUsing, int _indexNum)
    {
        key = _key; type = _type; name = _name; content = _content; isUsing = _isUsing; indexNum = _indexNum;
    }

    public int key, indexNum;
    public string type, name, content;
    public bool isUsing;
}

//Clue 데이터 베이스
[System.Serializable]
public class Clue
{
    //생성자 정의
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
    //외부스크립트 참조
    public UIManager uiManagerScr;

    //Item
    #region 
    //아이템 데이터 값이 담긴 txt파일
    public TextAsset itemDataBase;
    //Item 클래스 안의 아이템 데이터들을 리스트화 시킨 것
    public List<Item> allItemList;

    //게임내에서 사용할 아이템 리스트 (아이템창 한정)
    public List<Item> myItemList;

    //슬롯에서 보여줄 아이템 리스트 (현재 보유중)
    public List<Item> curItemList;
    //조합창에서 보여줄 아이템 리스트 (현재 보유중)
    public List<Item> curItemList2;
    #endregion

    //단서 데이터 값이 담긴 txt파일
    public TextAsset clueDataBase;

    //Clue 클래스 안의 단서 데이터들을 리스트화 시킨것
    public List<Clue> allClueList;

    //게임내에서 사용할 단서 리스트
    public List<Clue> myClueList;

    //슬롯에서 보여줄 단서 리스트(현재 보유중)
    public List<Clue> curClueList;
    //조합창 에보여줄 단서 리스트(현재 보유중)
    public List<Clue> curClueList2;

    //아이템 Json이 저장될 위치
    public string itemfilePath;

    //단서 Json이 저장될 위치
    public string cluefilePath;

    //처음에 적용할 오브젝트 타입
    public string curType = "Item";

    //슬롯
    public GameObject[] slot;
    //조합창 슬롯
    public GameObject[] slot2;

    //사용중일때 이미지
    public GameObject[] usingImage;
    //조합창 사용중일때 이미지
    public GameObject[] usingImage2;

    //오브젝트 이미지
    public Image[] objectImage;
    //조합창 오브젝트 이미지
    public Image[] objectImage2;

    //두개의 이미지의 배열의 인덱스는 오브젝트들의 인덱스 값과 일치해야한다.
    //아이템 이미지
    public Sprite[] itemSprite;
    //단서 이미지
    public Sprite[] clueSprite;

    //현재 장착중인 오브젝트의 이미지
    public Image equitObjectSprite;

    //오브젝트 설명이 표시되는 텍스트
    public Text contentText;
    //조합창 오브젝트 설명이 표시되는 텍스트
    public Text contentText2;

    //조합창의 슬롯1의 이미지
    public Image image_CombineSlot1;
    //조합창의 슬롯2의 이미지
    public Image image_CombineSlot2;

    //슬롯창 Sprite이미지
    public Sprite sprite_Slot;
    //슬롯창 활성화 Sprite이미지
    public Sprite sprite_UsingSlot;
    //NoneImage
    public Sprite sprite_NoneImage;

    //조합창 1이 사용중인지
    public bool combineSlot1_Using;
    //조합창 2가 사용중인지
    public bool combineSlot2_Using;

    //조합창 슬롯1 아이템 이미지
    public Image image_CombineSlot1Item;
    //조합창 슬롯2 아이템 이미지
    public Image image_CombineSlot2Item;

    //조합창 슬롯1 아이템 이름
    public Text combineSlot1Name;
    //조합창 슬롯2 아이템 이름
    public Text combineSlot2Name;

    //조합창 슬롯1에 선택된 아이템의 정보
    public Item curCombineItem1Info;
    //조합창 슬롯2에 선택된 아이템의 정보
    public Item curCombineItem2Info;

    //조합창 슬롯1에 선택된 단서의 정보
    public Clue curCombineClue1Info;
    //조합창 슬롯2에 선택된 단서의 정보
    public Clue curCombineClue2Info;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        //전체 아이템 리스트 불러오기
        #region
        //String 배열 line안에 itemDataBase 안의 정보들을 0부터 itemDataBase.text.Length까지 받아온뒤 엔터를 기준으로 배열을 나누어 저장
        //ex) line.length = ItemDataBase 안의 코드의 줄 갯수
        string[] itemline = itemDataBase.text.Substring(0, itemDataBase.text.Length).Split('\n');

        // ItmeDataBasse 안의 정보들을 Tab을 기준으로 나누어 저장
        // ex) row[0] = key, row[1] = ObjectType, row[2] = Name, row[3] = Content, row[4] = isUsing, row[5] = IndexNum
        for (int i = 0; i < itemline.Length; i++)
        {
            string[] row = itemline[i].Split('\t');

            //allItemList에 값들 추가
            allItemList.Add(new Item(int.Parse(row[0]), row[1], row[2], row[3], row[4] == "TRUE", int.Parse(row[5])));
        }
        #endregion

        //전체 단서 리스트 불러오기
        #region
        //단서 데이터베이스 텍스트 파일안의 줄 갯수만큼의 크기를 가진 배열 clueline 선언
        string[] clueline = clueDataBase.text.Substring(0, clueDataBase.text.Length).Split('\n');

        for (int i = 0; i < clueline.Length; i++)
        {
            //Tab키를 눌린 기준으로 데이터들을 떼어서 배열에 저장
            string[] row = clueline[i].Split('\t');

            allClueList.Add(new Clue(int.Parse(row[0]), row[1], row[2], row[3], row[4] == "TRUE", int.Parse(row[5])));
        }
        #endregion

        //아이템 Json 파일이 저장될 위치
        itemfilePath = Application.persistentDataPath + "/MyItemText.txt";

        //단서 Json 파일이 저장될 위치
        cluefilePath = Application.persistentDataPath + "/MyClueText.txt";

        Save();
        Load();

        //아이템 탭 기본으로 보여주기
        TabClick(curType);
        //조합창 아이템 탭 기본으로 보여주기
        TabClick2(curType);

        GetItem(1001);
        GetItem(1002);
        GetClue(2001);
        GetClue(2002);

    }


    private void Update()
    {
        //조합창이 열리지 않았을 경우
        if (!uiManagerScr.gameObject_CombineWindow.activeSelf)
        {
            //조합 슬롯 비우기
            EmptyCombineSlot();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(GetEquipObjectKey());
        }
    }

    //오브젝트 슬롯 클릭시 (아이템창 한정)
    public void SlotClick(int slotNum)
    {
        //오브젝트 타입이 아이템일 경우
        if (curType == "Item")
        {
            //아이템 창의 경우
            #region
            Item curItem = curItemList[slotNum];
            Item usingItem = curItemList.Find(x => x.isUsing == true);

            

            //사용중인 아이템을 제외한 아이템들의 사용을 false로 바꿈
            if (usingItem != null) usingItem.isUsing = false;
            {
                curItem.isUsing = true;

                //선택된 아이템안의 내용 표시하기
                contentText.text = curItem.content;

                //선택된 이미지 옮겨주기
                equitObjectSprite.sprite = itemSprite[curItem.indexNum];
            }

            //사용중인 단서가 있다면 usingClue에 담겠다.
            Clue usingClue = curClueList.Find(x => x.isUsing == true);

            //만약 사용중인 단서가 있었다면 그 값을 false로 바꾸겠다.
            if (usingClue != null)
            {
                usingClue.isUsing = false;
            }
            
            //사용중인 아이템을 한번 더 클릭하면 false로 바꾸는 코드
            if(usingItem != null)
            {
                if (usingItem.key == curItemList[slotNum].key)
                {
                    curItemList[slotNum].isUsing = false;
                    
                    //장착 오브젝트 이미지 없애기
                    equitObjectSprite.sprite = sprite_NoneImage;
                }
            }

            #endregion
        }

        //오브젝트 타입이 단서일 경우
        else if (curType == "Clue")
        {
            //단서 창의 경우
            #region
            Clue curClue = curClueList[slotNum];
            Clue usingClue = curClueList.Find(x => x.isUsing == true);

            if (usingClue != null) usingClue.isUsing = false;
            {
                curClue.isUsing = true;

                //선택된 단서안의 내용 표시하기
                contentText.text = curClue.content;

                //착용아이템을 현재 선택된 단서 이미지로 바꿔주기
                equitObjectSprite.sprite = clueSprite[curClue.indexNum];
            }

            //사용중인 아이템이 있다면 usingItem에 담겠다.
            Item usingItem = curItemList.Find(x => x.isUsing == true);

            //만약 사용중인 아이템이 있었다면 그 아이템의 isUsing을 false로 바꾸겠다.
            if (usingItem != null)
            {
                usingItem.isUsing = false;
            }

            //사용중인 단서를 한번 더 클릭하면 false로 바꾸는 코드
            if (usingClue != null)
            {
                if (usingClue.key == curClueList[slotNum].key)
                {
                    curClueList[slotNum].isUsing = false;

                    //장착 오브젝트 이미지 없애기
                    equitObjectSprite.sprite = sprite_NoneImage;
                }
            }
            #endregion
        }
        Save();
    }

    //오브젝트 슬롯 클릭시(조합창 한정)
    public void SlotClick2(int slotNum)
    {
        //오브젝트 타입이 아이템일 경우
        if (curType == "Item")
        {
            //조합창의 아이템의 경우
            #region
            Item curItem2 = curItemList2[slotNum];
            Item usingItem2 = curItemList2.Find(x => x.isUsing == true);

            //사용중인 아이템을 제외한 아이템들의 사용을 false로 바꿈
            if (curItem2 != null)
            {
                //선택된 아이템안의 내용 표시하기
                contentText2.text = curItem2.content;
            }

            //아이템 탭에서 조합창 1이 사용중일경우
            if (combineSlot1_Using)
            {
                //조합창 슬롯 1의 이미지를 사용중인 아이템의 이미지로 변경
                image_CombineSlot1Item.sprite = itemSprite[curItem2.indexNum];
                //조합창 슬롯 1의 아이템 이름 변경
                combineSlot1Name.text = curItem2.name;

                //curCombineClue1Info안의 Key값을 비움
                curCombineClue1Info = null;

                //curCombineItem1Info에 사용중인 아이템의 정보를 넘김
                curCombineItem1Info = curItem2;
            }

            //아이템 탭에서 조합창 2가 사용중일경우
            else if (combineSlot2_Using)
            {
                //조합창 슬롯 2의 이미지를 사용중인 아이템의 이미지로 변경
                image_CombineSlot2Item.sprite = itemSprite[curItem2.indexNum];
                //조합창 슬롯 2의 아이템 이름 변경
                combineSlot2Name.text = curItem2.name;

                //curCombineClue2Info안의 Key값을 비움
                curCombineClue2Info = null;

                //curCombineItem2Info에 사용중인 아이템의 정보를 넘김
                curCombineItem2Info = curItem2;
            }

            #endregion
        }

        //오브젝트 타입이 단서일 경우
        else if (curType == "Clue")
        {
            //조합창의 단서 창의 경우
            #region
            Clue curClue2 = curClueList2[slotNum];
            Clue usingClue2 = curClueList2.Find(x => x.isUsing == true);

            if (curClueList2 != null)
            {
                //선택된 단서안의 내용 표시하기
                contentText2.text = curClue2.content;
            }

            //단서 탭에서 조합창 1이 사용중일경우
            if (combineSlot1_Using)
            {
                //조합창 슬롯 1의 이미지를 사용중인 단서의 이미지로 변경
                image_CombineSlot1Item.sprite = clueSprite[curClue2.indexNum];

                //조합창 슬롯 1의 단서 이름변경
                combineSlot1Name.text = curClue2.name;

                //curCombineItem1Info안의 key값을 비움
                curCombineItem1Info = null;

                //curCombineClue1Info에 사용중인 단서의 정보를 넘김
                curCombineClue1Info = curClue2;
            }

            //단서 탭에서 조합창 2가 사용중일경우
            else if (combineSlot2_Using)
            {
                //조합창 슬롯 1의 이미지를 사용중인 단서의 이미지로 변경
                image_CombineSlot2Item.sprite = clueSprite[curClue2.indexNum];
                //조합창 슬롯 2의 단서 이름변경
                combineSlot2Name.text = curClue2.name;

                //curCombineItem2Info안의 key값을 비움
                curCombineItem2Info = null;

                //curCombineClue2Info에 사용중인 단서의 정보를 넘김
                curCombineClue2Info = curClue2;
            }
            #endregion
        }

        //조합 슬롯1과 슬롯2 안의 아이템들이 존재하고.
        if (curCombineItem1Info != null && curCombineItem2Info != null)
        {
            //1번 슬롯과 2번슬롯의 아이템이 같고 2번슬롯을 사용중이라면
            if ((curCombineItem1Info.key == curCombineItem2Info.key) && combineSlot2_Using)
            {
                Debug.Log("1번슬롯 비우기");
                //조합 슬롯 아이템 1번을 비운다.
                EmptyCombineSlot1Item();
            }
        }

        //조합 슬롯1과 슬롯2 안의 아이템들이 존재하고.
        if (curCombineItem1Info != null && curCombineItem2Info != null)
        {
            //1번 슬롯과 2번슬롯의 아이템이 같고 1번슬롯을 사용중이라면
            if ((curCombineItem1Info.key == curCombineItem2Info.key) && combineSlot1_Using)
            {
                Debug.Log("2번슬롯 비우기");

                //조합 슬롯 아이템 2번을 비운다.
                EmptyCombineSlot2Item();
            }
        }

        //조합 슬롯1과 슬롯2 안의 단서들이 존재하고.
        if (curCombineClue1Info != null && curCombineClue2Info != null)
        {
            //1번 슬롯과 2번슬롯의 단서가 같고 1번슬롯을 사용중이라면
            if ((curCombineClue1Info.key == curCombineClue2Info.key) && combineSlot2_Using)
            {
                Debug.Log("1번슬롯 비우기");

                //조합 슬롯 단서 2번을 비운다.
                EmptyCombineSlot1Clue();
            }
        }

        //조합 슬롯1과 슬롯2 안의 단서들이 존재하고.
        if (curCombineClue1Info != null && curCombineClue2Info != null)
        {
            //1번 슬롯과 2번슬롯의 단서가 같고 1번슬롯을 사용중이라면
            if ((curCombineClue1Info.key == curCombineClue2Info.key) && combineSlot1_Using)
            {
                Debug.Log("2번슬롯 비우기");

                //조합 슬롯 단서 2번을 비운다.
                EmptyCombineSlot2Clue();
            }
        }

        Save();
    }

    //오브젝트 창에서의 Tab 클릭
    public void TabClick(string tabName)
    {
        //클릭한 타입에 맞춰서 오브젝트 리스트 불러오기
        curType = tabName;

        if (curType == "Item")
        {
            //아이템 창 슬롯
            for (int i = 0; i < slot.Length; i++)
            {
                //슬롯이 존재하는지 확인
                bool isExist = i < curItemList.Count;
                //없는 슬롯 비활성화
                slot[i].SetActive(isExist);
                //Text보이기
                slot[i].GetComponentInChildren<Text>().text = isExist ? curItemList[i].name : "";

                //슬롯이 존재한다면
                if (isExist)
                {
                    //이미지교체
                    objectImage[i].sprite = itemSprite[allItemList.FindIndex(x => x.name == curItemList[i].name)];
                    usingImage[i].SetActive(curItemList[i].isUsing);
                }
            }
            //단서 버튼 이미지 어둡게
            uiManagerScr.ChangeClueTapColor();
        }

        else if (curType == "Clue")
        {
            //단서 슬롯
            for (int i = 0; i < slot.Length; i++)
            {
                //슬롯이 존재하는지 확인
                bool isExist = i < curClueList.Count;
                //없는 슬롯 비활성화
                slot[i].SetActive(isExist);
                //Text보이기
                slot[i].GetComponentInChildren<Text>().text = isExist ? curClueList[i].name : "";

                //슬롯이 존재한다면
                if (isExist)
                {
                    //이미지교체
                    objectImage[i].sprite = clueSprite[allClueList.FindIndex(x => x.name == curClueList[i].name)];
                    usingImage[i].SetActive(curClueList[i].isUsing);
                }
            }
            //아이템 버튼 이미지 어둡게
            uiManagerScr.ChangeItemTapColor();
        }
    }

    //조합창에서의 Tab 클릭
    public void TabClick2(string tabName)
    {
        //클릭한 타입에 맞춰서 오브젝트 리스트 불러오기
        curType = tabName;

        if (curType == "Item")
        {
            //조합창의 아이템 슬롯
            for (int i = 0; i < slot2.Length; i++)
            {
                //슬롯이 존재하는지 확인
                bool isExist = i < curItemList2.Count;
                //없는 슬롯 비활성화
                slot2[i].SetActive(isExist);
                //Text보이기
                slot2[i].GetComponentInChildren<Text>().text = isExist ? curItemList2[i].name : "";

                //슬롯이 존재한다면
                if (isExist)
                {
                    //이미지교체
                    objectImage2[i].sprite = itemSprite[allItemList.FindIndex(x => x.name == curItemList2[i].name)];
                    //usingImage2[i].SetActive(curItemList2[i].isUsing);
                }
            }

            //단서 버튼 이미지 어둡게
            uiManagerScr.ChangeCombineClueTapColor();
        }

        else if (curType == "Clue")
        {
            //조합창 단서 슬롯
            for (int i = 0; i < slot2.Length; i++)
            {
                //슬롯이 존재하는지 확인
                bool isExist = i < curClueList2.Count;
                //없는 슬롯 비활성화
                slot2[i].SetActive(isExist);
                //Text보이기
                slot2[i].GetComponentInChildren<Text>().text = isExist ? curClueList2[i].name : "";

                //슬롯이 존재한다면
                if (isExist)
                {
                    //이미지교체
                    objectImage2[i].sprite = clueSprite[allClueList.FindIndex(x => x.name == curClueList2[i].name)];
                    //usingImage2[i].SetActive(curClueList2[i].isUsing);
                }
            }
            //아이템 버튼 이미지 어둡게
            uiManagerScr.ChangeCombineItemTapColor();
        }
    }

    //Data 저장
    void Save()
    {
        //Json 아이템 데이터 정의
        string jItemdata = JsonUtility.ToJson(new Serialization<Item>(allItemList));

        //json 아이템 파일 저장
        File.WriteAllText(itemfilePath, jItemdata);

        //Json 단서 데이터 정의
        string jCluedata = JsonUtility.ToJson(new Serialization<Clue>(allClueList));

        //json 단서 파일 저장
        File.WriteAllText(cluefilePath, jCluedata);

        //현재 보유중인 아이템 리스트 불러오기
        TabClick(curType);
        //조합창에서 현재 보유중인 아이템 리스트 불러오기
        TabClick2(curType);
    }

    //Data 불러오기
    void Load()
    {
        //아이템 Json 데이터 정의
        string jItemdata = File.ReadAllText(itemfilePath);

        //아이템 Json파일로부터 데이터 역직렬화(Load)
        myItemList = JsonUtility.FromJson<Serialization<Item>>(jItemdata).target;

        //현재 보유중인 아이템 리스트 불러오기
        TabClick(curType);
        //조합창에서 현재 보유중인 아이템 리스트 불러오기
        TabClick2(curType);

        //단서 Json 데이터 정의
        string jCluedata = File.ReadAllText(cluefilePath);
        //단서 Json파일로부터 데이터 역직렬화(Load)
        myClueList = JsonUtility.FromJson<Serialization<Clue>>(jCluedata).target;
    }

    //Key를 통해서 아이템 얻기
    public void GetItem(int _key)
    {
        //해당 Key를 가진 오브젝트가 존재하는 경우
        if (myItemList.Find(x => x.key == _key) != null)
        {
            curItemList.Add(myItemList.Find(x => x.key == _key));
            curItemList2.Add(myItemList.Find(x => x.key == _key));
            TabClick(curType);
            TabClick2(curType);
        }
    }

    //Key를 통해서 아이템 삭제
    public void RemoveItem(int _key)
    {
        //해당 Key를 가진 오브젝트가 존재하는 경우
        if (myItemList.Find(x => x.key == _key) != null)
        {
            curItemList.Remove(myItemList.Find(x => x.key == _key));
            curItemList2.Remove(myItemList.Find(x => x.key == _key));
            TabClick(curType);
            TabClick2(curType);
        }
    }

    //Key를 통해서 단서 얻기
    public void GetClue(int _key)
    {
        //해당 Key를 가진 단서가 존재하는 경우
        if (myClueList.Find(x => x.key == _key) != null)
        {
            curClueList.Add(myClueList.Find(x => x.key == _key));
            curClueList2.Add(myClueList.Find(x => x.key == _key));
            TabClick(curType);
            TabClick2(curType);
        }
    }

    //Key를 통해서 단서 삭제
    public void RemoveClue(int _key)
    {
        //해당 Key를 가진 단서가 존재하는 경우
        if (myClueList.Find(x => x.key == _key) != null)
        {
            curClueList.Remove(myClueList.Find(x => x.key == _key));
            curClueList2.Remove(myClueList.Find(x => x.key == _key));
            TabClick(curType);
            TabClick2(curType);
        }
    }

    //조합창 슬롯 클릭
    public void CombineSlot(int slotNum)
    {
        //1번슬롯이 사용중이지 않고 눌렀을 경우
        if (slotNum == 1 && !combineSlot1_Using)
        {
            //슬롯 1의 Sprite를 UsingSprite로 교체
            image_CombineSlot1.sprite = sprite_UsingSlot;
            //슬롯 2의 Sprite를 원래 이미지로 변경
            image_CombineSlot2.sprite = sprite_Slot;

            ////1번 조합창 사용중
            Invoke("CombineSlot1True", 0.1f);
            //2번 조합창 사용 취소
            combineSlot2_Using = false;
        }

        //2번 슬롯이 사용중이지 않고 눌렀을 경우
        else if (slotNum == 2 && !combineSlot2_Using)
        {
            //슬롯 2의 Sprite를 UsingSprite로 교체
            image_CombineSlot2.sprite = sprite_UsingSlot;
            //슬롯 1의 Sprite를 원래 이미지로 변경
            image_CombineSlot1.sprite = sprite_Slot;

            //2번 조합창 사용중
            Invoke("CombineSlot2True", 0.1f);
            //1번 조합창 사용 취소
            combineSlot1_Using = false;
        }

        //슬롯1이 사용중인데 한번 더 눌렀을 경우
        if (slotNum == 1 && combineSlot1_Using)
        {
            //슬롯 1번 사용 취소
            combineSlot1_Using = false;
            //슬롯 1의 Sprite를 원래 이미지로 변경
            image_CombineSlot1.sprite = sprite_Slot;
        }

        //슬롯2가 사용중인데 한번 더 눌렀을 경우
        else if (slotNum == 2 && combineSlot2_Using)
        {
            //슬롯 2번 사용 취소
            combineSlot2_Using = false;
            //슬롯 2의 Sprite를 원래 이미지로 변경
            image_CombineSlot2.sprite = sprite_Slot;
        }


    }

    //조합 버튼을 눌렀을 경우
    public void CombineButton()
    {

        //슬롯 1: Item, 슬롯 2: Item
        #region
        if (curCombineItem1Info != null && curCombineItem2Info != null)
        {
            if (curCombineItem1Info.key != 0 && curCombineItem2Info.key != 0)
            {
                Debug.Log("아이템끼리의 조합");
                //슬롯1,슬롯2의 Key의 합
                int sumKeyValue = curCombineItem1Info.key + curCombineItem2Info.key;

                //슬롯1,슬롯2의 Type의 합
                string slotSumType = curCombineItem1Info.type + curCombineItem2Info.type;

                //합쳐진 오브젝트의 Type
                string sumObjectType = "";

                //myItemList에서 해당 SumKey값을 가진 아이템이 있을경우
                if (myItemList.Find(x => x.key == sumKeyValue) != null)
                {
                    Item sumItem = myItemList.Find(x => x.key == sumKeyValue);
                    //합쳐진 아이템의 Type
                    sumObjectType = sumItem.type;
                }

                //Sumkey로 찾은 오브젝트의 타입이 슬롯창의 두 오브젝트의 타입을 더한 값과 같을경우.
                if (sumObjectType == slotSumType)
                {
                    //해당 Key값을 가진 아이템 획득
                    GetItem(sumKeyValue);

                    //조합에 사용된 오브젝트 제거
                    RemoveItem(curCombineItem1Info.key);
                    RemoveItem(curCombineItem2Info.key);

                    //슬롯 비우기
                    EmptyCombineSlot();

                    //조합창 Text표시
                    contentText2.text = "조합 성공!";
                }

                //조합된 아이템이 존재하지 않다면
                else if (sumObjectType == "")
                {
                    Debug.Log("텍스트 넣었음");

                    //조합 슬롯 비우기
                    EmptyCombineSlot();

                    contentText2.text = "조합 실패!";
                }
            }
        }
        #endregion

        //슬롯 1: Clue, 슬롯 2: Clue
        #region
        if (curCombineClue1Info != null && curCombineClue2Info != null)
        {
            if (curCombineClue1Info.key != 0 && curCombineClue2Info.key != 0)
            {
                Debug.Log("단서끼리의 조합");

                //슬롯1,슬롯2의 Key의 합
                int sumKeyValue = curCombineClue1Info.key + curCombineClue2Info.key;

                //슬롯1,슬롯2의 Type의 합
                string slotSumType = curCombineClue1Info.type + curCombineClue2Info.type;

                //합쳐진 오브젝트의 Type
                string sumObjectType = "";

                //myItemList에서 해당 SumKey값을 가진 단서 있을경우
                if (myClueList.Find(x => x.key == sumKeyValue) != null)
                {
                    Clue sumItem = myClueList.Find(x => x.key == sumKeyValue);
                    //합쳐진 단서의 Type
                    sumObjectType = sumItem.type;
                }

                //Sumkey로 찾은 오브젝트의 타입이 슬롯창의 두 오브젝트의 타입을 더한 값과 같을경우.
                if (sumObjectType == slotSumType)
                {
                    //해당 Key값을 가진 단서 획득
                    GetClue(sumKeyValue);

                    //조합에 사용된 단서 제거
                    RemoveClue(curCombineClue1Info.key);
                    RemoveClue(curCombineClue2Info.key);

                    //슬롯 비우기
                    EmptyCombineSlot();

                    //조합창 Text표시
                    contentText2.text = "조합 성공!";
                }

                //조합된 단서가 존재하지 않다면
                else if (sumObjectType == "")
                {
                    Debug.Log("텍스트 넣었음");

                    //조합 슬롯 비우기
                    EmptyCombineSlot();

                    contentText2.text = "조합 실패!";
                }
            }
        }
        #endregion

        //슬롯 1: Item, 슬롯 2: Clue
        #region
        if (curCombineItem1Info != null && curCombineClue2Info != null)
        {
            if (curCombineItem1Info.key != 0 && curCombineClue2Info.key != 0)
            {
                Debug.Log("아이템과 단서의 조합");

                //슬롯1,슬롯2의 Key의 합
                int sumKeyValue = curCombineItem1Info.key + curCombineClue2Info.key;

                //슬롯1,슬롯2의 Type의 합
                string slotSumType = curCombineItem1Info.type + curCombineClue2Info.type;

                //합쳐진 오브젝트의 Type
                string sumObjectType = "";

                //myItemList에서 해당 SumKey값을 가진 아이템 있을경우
                if (myItemList.Find(x => x.key == sumKeyValue) != null)
                {
                    Item sumItem = myItemList.Find(x => x.key == sumKeyValue);
                    //합쳐진 단서의 Type
                    sumObjectType = sumItem.type;

                    //Sumkey로 찾은 오브젝트의 타입이 슬롯창의 두 오브젝트의 타입을 더한 값과 같을경우.
                    if (sumObjectType == slotSumType && sumItem.key != 0)
                    {
                        Debug.Log("아이템으로 생성됨");
                        //해당 Key값을 가진 단서 획득
                        GetItem(sumKeyValue);

                        //조합에 사용된 오브젝트 제거
                        RemoveItem(curCombineItem1Info.key);
                        RemoveClue(curCombineClue2Info.key);

                        //슬롯 비우기
                        EmptyCombineSlot();

                        //조합창 Text표시
                        contentText2.text = "조합 성공!";
                    }
                }

                //myClueList에서 해당 SumKey값을 가진 단서가 있을경우
                else if (myClueList.Find(x => x.key == sumKeyValue) != null)
                {
                    Clue sumClue = myClueList.Find(x => x.key == sumKeyValue);
                    //합쳐진 단서의 Type
                    sumObjectType = sumClue.type;

                    //Sumkey로 찾은 오브젝트의 타입이 슬롯창의 두 오브젝트의 타입을 더한 값과 같을경우.
                    if (sumObjectType == slotSumType && sumClue.key != 0)
                    {
                        Debug.Log("단서로 생성됨");

                        //해당 Key값을 가진 단서 획득
                        GetClue(sumKeyValue);

                        //조합에 사용된 오브젝트 제거
                        RemoveItem(curCombineItem1Info.key);
                        RemoveClue(curCombineClue2Info.key);

                        //슬롯 비우기
                        EmptyCombineSlot();

                        //조합창 Text표시
                        contentText2.text = "조합 성공!";
                    }
                }

                //조합된 단서가 존재하지 않다면
                if (sumObjectType == "")
                {
                    Debug.Log("텍스트 넣었음");

                    //조합 슬롯 비우기
                    EmptyCombineSlot();

                    contentText2.text = "조합 실패!";
                }
            }
        }
        #endregion

        //슬롯 1: Clue, 슬롯 2: Item
        #region
        if (curCombineClue1Info != null && curCombineItem2Info != null)
        {
            if (curCombineClue1Info.key != 0 && curCombineItem2Info.key != 0)
            {
                Debug.Log("단서와 아이템의 조합");

                //슬롯1,슬롯2의 Key의 합
                int sumKeyValue = curCombineClue1Info.key + curCombineItem2Info.key;

                //슬롯1,슬롯2의 Type의 합
                string slotSumType = curCombineItem2Info.type + curCombineClue1Info.type;

                //합쳐진 오브젝트의 Type
                string sumObjectType = "";

                //myItemList에서 해당 SumKey값을 가진 아이템 있을경우
                if (myItemList.Find(x => x.key == sumKeyValue) != null)
                {
                    Item sumItem = myItemList.Find(x => x.key == sumKeyValue);
                    //합쳐진 단서의 Type
                    sumObjectType = sumItem.type;

                    //Sumkey로 찾은 오브젝트의 타입이 슬롯창의 두 오브젝트의 타입을 더한 값과 같을경우.
                    if (sumObjectType == slotSumType && sumItem.key != 0)
                    {
                        Debug.Log("아이템으로 생성됨");
                        //해당 Key값을 가진 단서 획득
                        GetItem(sumKeyValue);

                        //조합에 사용된 오브젝트 제거
                        RemoveClue(curCombineClue1Info.key);
                        RemoveItem(curCombineItem2Info.key);

                        //슬롯 비우기
                        EmptyCombineSlot();

                        //조합창 Text표시
                        contentText2.text = "조합 성공!";
                    }
                }

                //myClueList에서 해당 SumKey값을 가진 단서가 있을경우
                else if (myClueList.Find(x => x.key == sumKeyValue) != null)
                {
                    Clue sumClue = myClueList.Find(x => x.key == sumKeyValue);
                    //합쳐진 단서의 Type
                    sumObjectType = sumClue.type;

                    //Sumkey로 찾은 오브젝트의 타입이 슬롯창의 두 오브젝트의 타입을 더한 값과 같을경우.
                    if (sumObjectType == slotSumType && sumClue.key != 0)
                    {
                        Debug.Log("단서로 생성됨");

                        //해당 Key값을 가진 단서 획득
                        GetClue(sumKeyValue);

                        //조합에 사용된 오브젝트 제거
                        RemoveClue(curCombineClue1Info.key);
                        RemoveItem(curCombineItem2Info.key);

                        //슬롯 비우기
                        EmptyCombineSlot();

                        //조합창 Text표시
                        contentText2.text = "조합 성공!";
                    }
                }

                //조합된 단서가 존재하지 않다면
                if (sumObjectType == "")
                {
                    Debug.Log("텍스트 넣었음");

                    //조합 슬롯 비우기
                    EmptyCombineSlot();

                    contentText2.text = "조합 실패!";
                }
            }
        }

        #endregion
    }

    //조합 슬롯 비우기
    private void EmptyCombineSlot()
    {
        //조합창 2개의 사용을 중지
        combineSlot1_Using = false;
        combineSlot2_Using = false;

        //조합창 안의 정보들의 Key값을 비워줌
        curCombineItem1Info = null;
        curCombineItem2Info = null;
        curCombineClue1Info = null;
        curCombineClue2Info = null;

        //조합창 슬롯의 오브젝트 이미지 제거
        image_CombineSlot1Item.sprite = sprite_NoneImage;
        image_CombineSlot2Item.sprite = sprite_NoneImage;

        //조합창 슬롯의 Text 제거
        combineSlot1Name.text = "";
        combineSlot2Name.text = "";

        //조합창 슬롯의 사용이미지 초기화
        image_CombineSlot1.sprite = sprite_Slot;
        image_CombineSlot2.sprite = sprite_Slot;

        //Text창 리셋
        contentText2.text = "이곳은 조합창입니다.";
    }


    //조합 슬롯 1번을 True로 바꿔주는 메서드
    private void CombineSlot1True()
    {
        combineSlot1_Using = true;
    }

    //조합 슬롯 2번을 True로 바꿔주는 메서드
    private void CombineSlot2True()
    {
        combineSlot2_Using = true;
    }


    //조합슬롯 1번의 아이템을 비우는 메서드
    private void EmptyCombineSlot1Item()
    {
        //1번 정보값을 지운다
        curCombineItem1Info = null;
        //1번 슬롯의 아이템 이미지를 지운다.
        image_CombineSlot1Item.sprite = sprite_NoneImage;
        //1번 슬롯의 아이템 이름을 지운다
        combineSlot1Name.text = "";
    }

    //조합슬롯 2번의 아이템을 비우는 메서드
    private void EmptyCombineSlot2Item()
    {
        //2번 정보값을 지운다
        curCombineItem2Info = null;
        //2번 슬롯의 아이템 이미지를 지운다.
        image_CombineSlot2Item.sprite = sprite_NoneImage;
        //2번 슬롯의 아이템 이름을 지운다
        combineSlot2Name.text = "";
    }

    //조합슬롯 1번의 단서를 비우는 메서드
    private void EmptyCombineSlot1Clue()
    {
        //1번 정보값을 지운다
        curCombineClue1Info = null;
        //1번 슬롯의 단서 이미지를 지운다.
        image_CombineSlot1Item.sprite = sprite_NoneImage;
        //1번 슬롯의 단서 이름을 지운다
        combineSlot1Name.text = "";
    }

    //조합슬롯 2번의 단서를 비우는 메서드
    private void EmptyCombineSlot2Clue()
    {
        //2번 정보값을 지운다
        curCombineClue2Info = null;
        //2번 슬롯의 단서 이미지를 지운다.
        image_CombineSlot2Item.sprite = sprite_NoneImage;
        //2번 슬롯의 단서 이름을 지운다
        combineSlot2Name.text = "";
    }

    //조합 슬롯 1번에 오브젝트 넣기
    public void InputCombinSlot1(int _slotNum)
    {
        //현재 Item Tap일 경우
        if (curType == "Item")
        {
            //조합 슬롯 1번에 아이템 이미지 변경
            image_CombineSlot1Item.sprite = itemSprite[curItemList2[_slotNum].indexNum];

            //조합 슬롯 1번 이름 바꾸기
            combineSlot1Name.text = curItemList2[_slotNum].name;

            //조합 슬롯 1번에 정보 넣기
            curCombineItem1Info = curItemList2[_slotNum];

            //조합 단서 1번 정보 비우기
            curCombineClue1Info = null;


            if (curCombineItem1Info != null && curCombineItem2Info != null)
            {
                if (curCombineItem1Info.key == curCombineItem2Info.key)
                {
                    //2번 슬롯 아이템 비우기
                    EmptyCombineSlot2Item();
                }
            }
        }

        //현재 Clue Tap일 경우
        else if (curType == "Clue")
        {
            //조합 슬롯 1번에 단서 이미지 변경
            image_CombineSlot1Item.sprite = clueSprite[curClueList2[_slotNum].indexNum];

            //조합 슬롯 1번 이름 바꾸기
            combineSlot1Name.text = curClueList2[_slotNum].name;

            //조합 슬롯 1번에 정보 넣기
            curCombineClue1Info = curClueList2[_slotNum];

            //조합 아이템 1번 정보 비우기
            curCombineItem1Info = null;

            if (curCombineClue1Info != null && curCombineClue2Info != null)
            {
                //조합 슬롯 1번,2번의 단서가 같을 경우
                if (curCombineClue1Info.key == curCombineClue2Info.key)
                {
                    //2번 슬롯 단서 비우기
                    EmptyCombineSlot2Clue();
                }
            }
        }
    }

    //조합 슬롯 2번에 오브젝트 넣기
    public void InputCombinSlot2(int _slotNum)
    {
        if (curType == "Item")
        {
            //조합 슬롯 2번에 아이템 이미지 변경
            image_CombineSlot2Item.sprite = itemSprite[curItemList2[_slotNum].indexNum];

            //조합 슬롯 2번 이름 바꾸기
            combineSlot2Name.text = curItemList2[_slotNum].name;

            //조합 슬롯 2번에 정보 넣기
            curCombineItem2Info = curItemList2[_slotNum];

            //조합 단서 2번 정보 비우기
            curCombineClue2Info = null;

            if (curCombineItem1Info != null && curCombineItem2Info != null)
            {
                //조합 슬롯 1번,2번에 같은 아이템이 들어있을경우
                if (curCombineItem1Info.key == curCombineItem2Info.key)
                {
                    //1번 슬롯 아이템 비우기
                    EmptyCombineSlot1Item();
                }
            }
        }

        //현재 Clue Tap일 경우
        else if (curType == "Clue")
        {
            //조합 슬롯 2번에 단서 이미지 변경
            image_CombineSlot2Item.sprite = clueSprite[curClueList2[_slotNum].indexNum];

            //조합 슬롯 2번 이름 바꾸기
            combineSlot2Name.text = curClueList2[_slotNum].name;

            //조합 슬롯 2번에 정보 넣기
            curCombineClue2Info = curClueList2[_slotNum];

            //조합 아이템 2번 정보 비우기
            curCombineItem2Info = null;

            if (curCombineClue1Info != null && curCombineClue2Info != null)
            {
                //조합 슬롯 1번,2번의 단서가 같을 경우
                if (curCombineClue1Info.key == curCombineClue2Info.key)
                {
                    //1번 슬롯 단서 비우기
                    EmptyCombineSlot1Clue();
                }
            }
        }
    }
   

    //장착한 오브젝트의 Key 값을 반환해주는 메서드 (값이 있으면 key값, 없으면 0)
    public int GetEquipObjectKey()
    {
        //보유중인 아이템 리스트가 비어있지 않다면
        if(curItemList != null)
        {
            //사용중인 아이템 클래스 넣기
            Item usingItem = curItemList.Find(x => x.isUsing == true);

            //사용중인 아이템이 있었다면
            if(usingItem != null)
            {
                //사용중인 아이템 키값 반환
                return usingItem.key;
            }
        }

        //보유중인 단서 리스트가 비어있지 않다면
        if (curClueList != null)
        {
            //사용중인 단서 클래스 넣기
            Clue usingclue = curClueList.Find(x => x.isUsing == true);

            //사용중인 단서가 있었다면
            if (usingclue != null)
            {
                //사용중인 단서 키값 반환
                return usingclue.key;
            }
        }

        //사용중인 오브젝트가 없을 경우
        return 0;
    }
}
