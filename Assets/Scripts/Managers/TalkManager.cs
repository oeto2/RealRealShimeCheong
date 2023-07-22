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
        //id = 5000 : ���� ���
        talkData.Add(5000, new string[] { "ȣȣ, ���� ���̽Ű���??", "�̹��� �鿩�� ��డ �׷��� ���ڴ���,,," });

        //id = 5001 : �ʹ� ���
        talkData.Add(5001, new string[] { "ȥ�ڼ��� �Ŷ��� ��ƾ� �Ͽ�.", "����� ������ ���غ��ÿ�." });

        //id = 5002 : ��»�� ����
        talkData.Add(5002, new string[] { "����� ��¾ ���̿�?", "(���� ��ȭ�� ������.)" });

        //id = 5003 : ����
        talkData.Add(5003, new string[] { "�� �� �� �ּ�?", "�� �� ������ �� ��Ű�ÿ�! �� �Ѱڳ�." });

        //id = 5004 : �·�
        talkData.Add(5004, new string[] { "��ó�� �������� �Ұ��帰�ٸ� �ٽ��� ��������ϴ�." });

        //id = 5005 : ����
        talkData.Add(5005, new string[] { "(������)...�������..", "���� ���ְڴ�.." });

        //id = 5006 : ����
        talkData.Add(5006, new string[] { "�������� ��� ���̿�. ���� �ͼ�." });

        //id = 5007 : �۳��� ����
        talkData.Add(5007, new string[] { "�ð��� �����̿�! ���� �����̽ʽÿ�!!(�� ����� ������ �ٻ� ���Ѵ�.)" ,
                                             "(�� ���� �Ĵٵ� �����ʴ´�.)",
                                            "(���� �ٺ� ���δ�.)"});
    }

    public string GetTalk(int id, int talkIndex) //Object�� id , string�迭�� index
    {
        return talkData[id][talkIndex]; //�ش� ���̵��� �ش�
    }
}

/*
5000	NPC		���� ���	ȣȣ, ���� ���̽Ű���??;�̹��� �鿩�� ��డ �׷��� ���ڴ���,,,	FALSE	0
5001	NPC		�ʹ� ���	ȥ�ڼ��� �Ŷ��� ��ƾ� �Ͽ�.;����� ������ ���غ��ÿ�.	FALSE	1
5002	NPC		��»�� ����	����� ��¾ ���̿�?;(���� ��ȭ�� ������.)	FALSE	2
5003	NPC		����	�� �� �� �ּ�?;�� �� ������ �� ��Ű�ÿ�! �� �Ѱڳ�.	FALSE	3
5004	NPC		�·�	��ó�� �������� �Ұ��帰�ٸ� �ٽ��� ��������ϴ�.	FALSE	4
5005	NPC		����	(������)...�������..;���� ���ְڴ�..	FALSE	5
5006	NPC		����	�������� ��� ���̿�. ���� �ͼ�.	FALSE	6
5007	NPC		�۳��� ����	�ð��� �����̿�! ���� �����̽ʽÿ�!!(�� ����� ������ �ٻ� ���Ѵ�.);(�� ���� �Ĵٵ� �����ʴ´�.);(���� �ٺ� ���δ�.)	FALSE	7
 */