using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    // Start is called before the first frame update
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        //id = 5000 : 뺑덕 어멈
        talkData.Add(5000, new string[] { "호호, 무슨 일이신가요??", "이번에 들여온 비녀가 그렇게 예쁘던데,,," });

        //id = 5001 : 귀덕 어멈
        talkData.Add(5001, new string[] { "혼자서라도 거뜬히 살아야 하오.", "어려움 있으면 말해보시오." });

        //id = 5002 : 장승상댁 부인
        talkData.Add(5002, new string[] { "여기는 어쩐 일이오?", "(노비와 대화를 나눈다.)" });

        //id = 5003 : 장사꾼
        talkData.Add(5003, new string[] { "뭐 살 것 있소?", "살 것 없으면 좀 비키시오! 객 쫓겠네." });

        //id = 5004 : 승려
        talkData.Add(5004, new string[] { "부처께 지성으로 불공드린다면 근심이 없어진답니다." });

        //id = 5005 : 거지
        talkData.Add(5005, new string[] { "(꼬르륵)...배고프다..", "저거 맛있겠다.." });

        //id = 5006 : 뱃사공
        talkData.Add(5006, new string[] { "오랜만에 밟는 땅이요. 쉬고 싶소." });

        //id = 5007 : 송나라 상인
        talkData.Add(5007, new string[] { "시간이 명줄이오! 빨리 움직이십시오!!(퍽 겸손한 말씨로 바삐 일한다.)" ,
                                             "(이 쪽은 쳐다도 보지않는다.)",
                                            "(몹시 바빠 보인다.)"});
    }

    public string GetTalk(int id, int talkIndex) //Object의 id , string배열의 index
    {
        return talkData[id][talkIndex]; //해당 아이디의 해당
    }
}

/*
5000	NPC		뺑덕 어멈	호호, 무슨 일이신가요??;이번에 들여온 비녀가 그렇게 예쁘던데,,,	FALSE	0
5001	NPC		귀덕 어멈	혼자서라도 거뜬히 살아야 하오.;어려움 있으면 말해보시오.	FALSE	1
5002	NPC		장승상댁 부인	여기는 어쩐 일이오?;(노비와 대화를 나눈다.)	FALSE	2
5003	NPC		장사꾼	뭐 살 것 있소?;살 것 없으면 좀 비키시오! 객 쫓겠네.	FALSE	3
5004	NPC		승려	부처께 지성으로 불공드린다면 근심이 없어진답니다.	FALSE	4
5005	NPC		거지	(꼬르륵)...배고프다..;저거 맛있겠다..	FALSE	5
5006	NPC		뱃사공	오랜만에 밟는 땅이요. 쉬고 싶소.	FALSE	6
5007	NPC		송나라 상인	시간이 명줄이오! 빨리 움직이십시오!!(퍽 겸손한 말씨로 바삐 일한다.);(이 쪽은 쳐다도 보지않는다.);(몹시 바빠 보인다.)	FALSE	7
 */