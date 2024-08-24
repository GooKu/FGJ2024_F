using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] GameObject slotPrefab;

    [SerializeField] Transform inventoryRoot;

    List<InventorySlot> slots;
    

    public void Init()
    {
        slots = new List<InventorySlot>();
    }

    private InventorySlot AddSlot()
    {
        InventorySlot slot = Instantiate(slotPrefab, inventoryRoot).GetComponent<InventorySlot>();
        slots.Add(slot);
        return slot;
    }

    private void RemoveSlot(InventorySlot slot)
    {
        slots.Remove(slot);
        Destroy(slot.gameObject);
    }

    public void AddItem(List<InteractiveObject> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            AddItem(items[i]);
        }
    }

    public void AddItem(InteractiveObject item)
    {
        InventorySlot slot = AddSlot();
        slot.AddItem(item);
    }

    public void RemoveItem(List<InteractiveObject> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            RemoveItem(items[i]);
        }
    }

    public void RemoveItem(InteractiveObject item)
    {
        bool canFindSlot = TryFindObjInSlotById(item.ID, out InventorySlot slot);
        if (!canFindSlot) return;
        RemoveSlot(slot);
    }

    public bool TryFindObjInSlotById(string objId, out InventorySlot slot)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].IsSameItem(objId)) continue;
            slot = slots[i];
            return true;
        }
        
        slot = null;
        return false;
    }
}
