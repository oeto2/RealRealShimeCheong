using Constant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    private Dictionary<int, ItemData> items = new();

    public ItemDB()
    {
        var res = Resources.Load<S_item_data_table>(ResourcePath.ItemSO);
        var itemSO = Instantiate(res);
        var entities = itemSO.Item_Table;

        if (entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for (int i = 0; i < entityCount; i++)
        {
            var item = entities[i];

            if (items.ContainsKey(item.Id))
                items[item.Id] = item;
            else
                items.Add(item.Id, item);
        }
    }

    public ItemData Get(int id_)
    {
        if (items.ContainsKey(id_))
            return items[id_];

        return null;
    }
}
