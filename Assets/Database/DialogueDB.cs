using Constant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDB : MonoBehaviour
{
    private Dictionary<int, DialogueData> dialgoue = new();
    public DialogueDB()
    {
        var res = Resources.Load<S_dialogue_data_table>(ResourcePath.DialogueSO);
        var dialogueSO = Instantiate(res);
        var entities = dialogueSO.Dialogue_Table;

        if (entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for (int i = 0; i < entityCount; i++)
        {
            var dialogue = entities[i];

            if (this.dialgoue.ContainsKey(dialogue.Id))
                this.dialgoue[dialogue.Id] = dialogue;
            else
                this.dialgoue.Add(dialogue.Id, dialogue);
        }
    }

    public DialogueData Get(int id_)
    {
        if (dialgoue.ContainsKey(id_))
            return dialgoue[id_];

        return null;
    }
}
