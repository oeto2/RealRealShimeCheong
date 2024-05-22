using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constant
{
    public enum ObjectType
    {
        Item,
        Clue,
        ClueClue,
    }

    public enum SpeakerType
    {
        Npc,
        Player
    }
    
    public static class ResourcePath
    {
        public const string ItemSO = "DB/S_item_data_table";
        public const string ClueSO = "DB/S_clue_data_table";
        public const string DialogueSO = "DB/S_dialogue_data_table";
    }
}
