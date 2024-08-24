using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public InteractiveObject Item { get; private set; }

    public bool IsSameItem(string id)
    {
        if (Item == null) return false;
        return Item.ID.Equals(id);
    }

    public void AddItem(InteractiveObject item)
    {
        Item = item;
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
    }
}
