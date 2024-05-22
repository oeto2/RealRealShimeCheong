using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/DB", ExcelName = "S_dialogue_data_table")]
public class S_dialogue_data_table : ScriptableObject
{
    public List<DialogueData> Dialogue_Table; // Replace 'EntityType' to an actual type that is serializable.
}
