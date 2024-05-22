using Constant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueDB : MonoBehaviour
{
    private Dictionary<int, ClueData> clue = new();

    public ClueDB()
    {
        var res = Resources.Load<S_clue_data_table>(ResourcePath.ClueSO);
        var clueSO = Instantiate(res);
        var entities = clueSO.Clue_Table;

        if (entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for (int i = 0; i < entityCount; i++)
        {
            var clue = entities[i];

            if (this.clue.ContainsKey(clue.Id))
                this.clue[clue.Id] = clue;
            else
                this.clue.Add(clue.Id, clue);
        }
    }

    public ClueData Get(int id_)
    {
        if (clue.ContainsKey(id_))
            return clue[id_];

        return null;
    }
}
