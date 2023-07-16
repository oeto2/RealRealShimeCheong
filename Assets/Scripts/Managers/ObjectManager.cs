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
    //Item
    #region 
    //아이템 데이터 값이 담긴 txt파일
    public TextAsset itemDataBase;
    //Item 클래스 안의 아이템 데이터들을 리스트화 시킨 것
    public List<Item> allItemList;
    
    //현재 보유중인 아이템 리스트
    public List<Item> myItemList;

    //슬롯에서 보여줄 아이템 리스트
    public List<Item> curItemList;
    #endregion

    //단서 데이터 값이 담긴 txt파일
    public TextAsset clueDataBase;

    //Clue 클래스 안의 단서 데이터들을 리스트화 시킨것
    public List<Clue> allClueList;

    //현재 보유중인 단서 리스트
    public List<Clue> myClueList;

    //슬롯에서 보여줄 단서 리스트
    public List<Clue> curClueList;

    //아이템 Json이 저장될 위치
    public string itemfilePath;

    //단서 Json이 저장될 위치
    public string cluefilePath;

    //처음에 적용할 오브젝트 타입
    public string curType = "Item";

    //슬롯
    public GameObject[] slot;

    //사용중일때 이미지
    public GameObject[] usingImage;

    //오브젝트 이미지
    public Image[] objectImage;

    //두개의 이미지의 배열의 인덱스는 오브젝트들의 인덱스 값과 일치해야한다.
    //아이템 이미지
    public Sprite[] itemSprite;
    //단서 이미지
    public Sprite[] clueSprite;

    //현재 장착중인 오브젝트의 이미지
    public Image equitObjectSprite;

    //오브젝트 설명이 표시되는 텍스트
    public Text contentText;

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
        for(int i = 0; i< itemline.Length; i++)
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

        for(int i = 0; i<clueline.Length; i++)
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
    }

    //오브젝트 슬롯 클릭시
    public void SlotClick(int slotNum)
    {
        //오브젝트 타입이 아이템일 경우
        if (curType == "Item")
        {
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
            if(usingClue != null)
            {
                usingClue.isUsing = false;
            }

        }

        //오브젝트 타입이 단서일 경우
        else if (curType == "Clue")
        {
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
            if(usingItem != null)
            {
                usingItem.isUsing = false;
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
            //Text보이기
            for (int i = 0; i < slot.Length; i++)
            {
                //슬롯이 존재하는지 확인
                bool isExist = i < curItemList.Count;
                slot[i].SetActive(isExist);
                slot[i].GetComponentInChildren<Text>().text = isExist ? curItemList[i].name : "";

                //슬롯이 존재한다면
                if (isExist)
                {
                    //이미지교체
                    objectImage[i].sprite = itemSprite[allItemList.FindIndex(x => x.name == curItemList[i].name)];
                    usingImage[i].SetActive(curItemList[i].isUsing);
                }
            }
        }
        
        else if (curType == "Clue")
        {
            //Text보이기
            for (int i = 0; i < slot.Length; i++)
            {
                //슬롯이 존재하는지 확인
                bool isExist = i < curClueList.Count;
                slot[i].SetActive(isExist);
                slot[i].GetComponentInChildren<Text>().text = isExist ? curClueList[i].name : "";

                //슬롯이 존재한다면
                if(isExist)
                {
                    //이미지교체
                    objectImage[i].sprite = clueSprite[allClueList.FindIndex(x => x.name == curClueList[i].name)];
                    usingImage[i].SetActive(curClueList[i].isUsing);
                }
            }
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

        //단서 Json 데이터 정의
        string jCluedata = File.ReadAllText(cluefilePath);
        //단서 Json파일로부터 데이터 역직렬화(Load)
        myClueList = JsonUtility.FromJson<Serialization<Clue>>(jCluedata).target;
    }

    //Key를 통해서 아이템 얻기
    public void GetItem(int _key)
    {
        curItemList.Add(myItemList.Find(x => x.key == _key));
        TabClick(curType);
    }

    //Key를 통해서 아이템 삭제
    public void RemoveItem(int _key)
    {
        curItemList.Remove(myItemList.Find(x => x.key == _key));
        TabClick(curType);
    }

    //Key를 통해서 단서 얻기
    public void GetClue(int _key)
    {
        curClueList.Add(myClueList.Find(x => x.key == _key));
        TabClick(curType);
    }

    //Key를 통해서 단서 삭제
    public void RemoveClue(int _key)
    {
        curClueList.Remove(myClueList.Find(x => x.key == _key));
        TabClick(curType);
    }
}
