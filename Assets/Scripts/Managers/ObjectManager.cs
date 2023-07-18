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

    //게임내에서 사용할 아이템 리스트 (조합창 한정)
    public List<Item> combinItemList;

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

    //게임내에서 사용할 단서 리스트(조합창 한정)
    public List<Clue> combineCluList;


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

        ////해당 키값을 가진 오브젝트 얻기
        //GetItem(1000);
        //GetItem(1001);
        //GetItem(1002);

        //GetClue(2003);
        //GetClue(2002);
        //GetClue(2001);
        //GetClue(2000);

        ////해당 키 값을 가진 오브젝트 제거
        //RemoveClue(2000);
        //RemoveItem(1000);

        //아이템 탭 기본으로 보여주기
        TabClick(curType);
        //조합창 아이템 탭 기본으로 보여주기
        TabClick2(curType);
    }


    private void Update()
    {
        //조합창이 열리지 않았을 경우
        if (!uiManagerScr.gameObject_CombineWindow.activeSelf)
        {
            //조합 슬롯 비우기
            EmptyCombineSlot();
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
            if (usingItem2 != null) usingItem2.isUsing = false;
            {
                curItem2.isUsing = true;

                //선택된 아이템안의 내용 표시하기
                contentText2.text = curItem2.content;

                //선택된 이미지 옮겨주기
                equitObjectSprite.sprite = itemSprite[curItem2.indexNum];
            }

            //사용중인 단서가 있다면 usingClue에 담겠다.
            Clue usingClue2 = curClueList2.Find(x => x.isUsing == true);

            //만약 사용중인 단서가 있었다면 그 값을 false로 바꾸겠다.
            if (usingClue2 != null)
            {
                usingClue2.isUsing = false;
            }

            //아이템 탭에서 조합창 1이 사용중일경우
            if (combineSlot1_Using)
            {
                //조합창 슬롯 1의 이미지를 사용중인 아이템의 이미지로 변경
                image_CombineSlot1Item.sprite = itemSprite[curItem2.indexNum];
                //조합창 슬롯 1의 아이템 이름 변경
                combineSlot1Name.text = curItem2.name;

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

            if (usingClue2 != null) usingClue2.isUsing = false;
            {
                curClue2.isUsing = true;

                //선택된 단서안의 내용 표시하기
                contentText2.text = curClue2.content;

                //착용아이템을 현재 선택된 단서 이미지로 바꿔주기
                equitObjectSprite.sprite = clueSprite[curClue2.indexNum];
            }

            //사용중인 아이템이 있다면 usingItem에 담겠다.
            Item usingItem2 = curItemList2.Find(x => x.isUsing == true);

            //만약 사용중인 아이템이 있었다면 그 아이템의 isUsing을 false로 바꾸겠다.
            if (usingItem2 != null)
            {
                usingItem2.isUsing = false;
            }

            //단서 탭에서 조합창 1이 사용중일경우
            if (combineSlot1_Using)
            {
                //조합창 슬롯 1의 이미지를 사용중인 아이템의 이미지로 변경
                image_CombineSlot1Item.sprite = itemSprite[curClue2.indexNum];
                //조합창 슬롯 1의 단서 이름변경
                combineSlot1Name.text = curClue2.name;
            }

            //단서 탭에서 조합창 2가 사용중일경우
            else if (combineSlot2_Using)
            {
                //조합창 슬롯 1의 이미지를 사용중인 아이템의 이미지로 변경
                image_CombineSlot2Item.sprite = itemSprite[curClue2.indexNum];
                //조합창 슬롯 2의 단서 이름변경
                combineSlot2Name.text = curClue2.name;
            }

            #endregion
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
                    usingImage2[i].SetActive(curClueList2[i].isUsing);
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

        //Json파일로부터 아이템리스트 가져오기 (조합창 한정)
        combinItemList = JsonUtility.FromJson<Serialization<Item>>(jItemdata).target;

        //현재 보유중인 아이템 리스트 불러오기
        TabClick(curType);
        //조합창에서 현재 보유중인 아이템 리스트 불러오기
        TabClick2(curType);

        //단서 Json 데이터 정의
        string jCluedata = File.ReadAllText(cluefilePath);
        //단서 Json파일로부터 데이터 역직렬화(Load)
        myClueList = JsonUtility.FromJson<Serialization<Clue>>(jCluedata).target;
        //단서 Json파일로부터 단서리스트 가져오기 (조합창 한정)
        combineCluList = JsonUtility.FromJson<Serialization<Clue>>(jCluedata).target;
    }

    //Key를 통해서 아이템 얻기
    public void GetItem(int _key)
    {
        curItemList.Add(myItemList.Find(x => x.key == _key));
        curItemList2.Add(myItemList.Find(x => x.key == _key));
        TabClick(curType);
        TabClick2(curType);
    }

    //Key를 통해서 아이템 삭제
    public void RemoveItem(int _key)
    {
        curItemList.Remove(myItemList.Find(x => x.key == _key));
        curItemList2.Remove(myItemList.Find(x => x.key == _key));
        TabClick(curType);
        TabClick2(curType);
    }

    //Key를 통해서 단서 얻기
    public void GetClue(int _key)
    {
        curClueList.Add(myClueList.Find(x => x.key == _key));
        curClueList2.Add(myClueList.Find(x => x.key == _key));
        TabClick(curType);
        TabClick2(curType);
    }

    //Key를 통해서 단서 삭제
    public void RemoveClue(int _key)
    {
        curClueList.Remove(myClueList.Find(x => x.key == _key));
        curClueList2.Remove(myClueList.Find(x => x.key == _key));
        TabClick(curType);
        TabClick2(curType);
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

            //1번 조합창 사용중
            combineSlot1_Using = true;
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
            combineSlot2_Using = true;
            //1번 조합창 사용 취소
            combineSlot1_Using = false;
        }

        ////1번 슬롯이 사용중인데 1번슬롯을 눌렀을 경우
        //if (slotNum == 1 && combineSlot1_Using)
        //{
        //    //슬롯 1의 Sprite를 기본 슬롯 Sprite로 교체
        //    image_CombineSlot1.sprite = sprite_Slot;

        //    //1번 조합창 사용중
        //    combineSlot1_Using = false;
        //}

        ////2번 슬롯이 사용중인데 2번슬롯을 눌렀을 경우
        //if (slotNum == 2 && combineSlot2_Using)
        //{
        //    //슬롯 2의 Sprite를 기본 슬롯 Sprite로 교체
        //    image_CombineSlot2.sprite = sprite_Slot;

        //    //1번 조합창 사용중
        //    combineSlot2_Using = false;
        //}
    }

    //조합 버튼을 눌렀을 경우
    public void CombineButton()
    {
        //조합창 2개가 사용중일 경우 (정보값이 있을경우)
        if ((curCombineItem1Info.key != 0 && curCombineItem2Info.key != 0))
        {
            Debug.Log("조합 실행");
            //슬롯1,슬롯2의 Key의 합
            int sumKeyValue = curCombineItem1Info.key + curCombineItem2Info.key;
            //슬롯1,슬롯2의 Type의 합
            string sumType = "";

            //만약 1번슬롯 오브젝트 타입이 아이템이라면
            if (curCombineItem1Info.type == "Item")
            {
                sumType = curCombineItem1Info.type + curCombineItem2Info.type;
            }


            //만약 1번슬롯 오브젝트 타입이 단서라면
            else if (curCombineItem1Info.type == "Clue")
            {
                //역순으로 저장
                sumType = curCombineItem2Info.name + curCombineItem1Info.name;
            }

            Debug.Log(sumType);


            //합쳐진 아이템의 Type
            string sumItemType = "";

            //합쳐진 아이템의 Key값을 가진 아이템이 있었다면
            if (myItemList.Find(x => x.key == sumKeyValue) != null)
            {
                //합친 Key값을 가진 아이템이 있으면 sumItem에 담겠다.
                Item sumItem = myItemList.Find(x => x.key == sumKeyValue);
                //합쳐진 아이템의 Type
                sumItemType = sumItem.type;
            }

            Debug.Log("합쳐진 아이템의 Key값:" + sumKeyValue);
            Debug.Log("합쳐진 아이템의 타입:" + sumItemType);

            //만약 조합된 아이템의 Type이 같다면
            if (sumItemType == sumType)
            {
                //조합에 사용된 아이템들이 사용중이였었다면
                if(curCombineItem1Info.isUsing || curCombineItem2Info.isUsing)
                {
                    //아이템 창의 Text 비워주기
                    contentText.text = "";
                }

                //해당 Key값을 가진 아이템 획득
                GetItem(sumKeyValue);
                //조합에 사용된 아이템 제거
                RemoveItem(curCombineItem1Info.key);
                RemoveItem(curCombineItem2Info.key);
                //슬롯 비우기
                EmptyCombineSlot();

                contentText2.text = "조합 성공!";
            }

            //조합된 아이템이 존재하지 않다면
            else if (sumItemType == "")
            {
                Debug.Log("텍스트 넣었음");

                //조합 슬롯 비우기
                EmptyCombineSlot();

                contentText2.text = "조합 실패!";
            }
        }
    }

    //조합 슬롯 비우기
    private void EmptyCombineSlot()
    {
        //조합창 2개의 사용을 중지
        combineSlot1_Using = false;
        combineSlot2_Using = false;

        //조합창 안의 정보들을 비워줌
        curCombineItem1Info = null;
        curCombineItem2Info = null;

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
}
