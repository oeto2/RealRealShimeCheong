using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/DB", ExcelName = "S_clue_data_table")]
public class S_clue_data_table : ScriptableObject
{
	public List<ClueData> Clue_Table; // Replace 'EntityType' to an actual type that is serializable.
}
