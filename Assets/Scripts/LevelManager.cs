using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : LevelManagerBase
{
    private InventorySystem inventorySystem;
    private DialogSystem dialogSystem;

    private void Awake()
    {
        inventorySystem = GameObject.FindObjectOfType<InventorySystem>();
        dialogSystem = GameObject.FindObjectOfType<DialogSystem>();
    }

    public override void Merge(GameObject a, GameObject b, GameObject result)
    {
        checkAndRemoveObjectInInventory(a);
        checkAndRemoveObjectInInventory(b);
        Destroy(a);
        Destroy(b);
        checkAndAddObjectInInventory(result);
    }

    private void checkAndAddObjectInInventory(GameObject go)
    {
        checkAndAddObjectInInventory(go.GetComponent<InteractiveObject>());
    }

    private void checkAndAddObjectInInventory(InteractiveObject ia)
    {
        if (inventorySystem.TryFindObjInSlotById(ia.ID, out _)) { return; }
        inventorySystem.AddItem(Instantiate(ia));
    }

    private void checkAndRemoveObjectInInventory(GameObject go)
    {
        checkAndRemoveObjectInInventory(go.GetComponent<InteractiveObject>());
    }

    private void checkAndRemoveObjectInInventory(InteractiveObject ia)
    {
        if (inventorySystem.TryFindObjInSlotById(ia.ID, out _))
        {
            inventorySystem.RemoveItem(ia);
        }
    }

    public override void Dialog(string message)
    {
        dialogSystem.ShowDialog(message);
    }

    public override void Fail()
    {
        throw new System.NotImplementedException();
    }

    public override void Pass()
    {
        throw new System.NotImplementedException();
    }

    public override void GetItem(GameObject item)
    {
        var obj = Instantiate(item);
        inventorySystem.AddItem(obj.GetComponent<InteractiveObject>());
    }

    public override void Dismantle(InteractiveObject source, List<InteractiveObject> results)
    {
        foreach (InteractiveObject result in results)
        {
            checkAndAddObjectInInventory(result);
        }
        checkAndRemoveObjectInInventory(source);
        Destroy(source.gameObject);
    }
}
