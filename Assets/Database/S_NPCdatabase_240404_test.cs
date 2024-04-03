using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class S_NPCdatabase_240404_test : ScriptableObject
{
	public List<DialogDBEntity> NPCDialogue; // Replace 'EntityType' to an actual type that is serializable.
	public List<DialogDBEntity> FrogDialogue; // 두꺼비 다이얼로그
	public List<DialogDBEntity> FrogSelect; // 두꺼비 선택지
}
