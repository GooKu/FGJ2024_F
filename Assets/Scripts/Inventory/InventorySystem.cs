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
            InventorySlot slot = AddSlot();
            slot.AddItem(items[i]);
        }
    }

    public void RemoveItem(List<InteractiveObject> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            bool canFindSlot = TryFindObjInSlotById(items[i].ID, out InventorySlot slot);
            if (!canFindSlot) continue;
            RemoveSlot(slot);
        }
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
