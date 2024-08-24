using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;

public class LevelManager : LevelManagerBase
{
    private InventorySystem inventorySystem;

    private void Awake()
    {
        inventorySystem = GameObject.FindObjectOfType<InventorySystem>();
    }

    public override void Merge(GameObject a, GameObject b, GameObject result)
    {
        Destroy(a);
        Destroy(b);
        inventorySystem.AddItem(result.GetComponent<InteractiveObject>());
    }

    public override void Dialog(string message)
    {
        throw new System.NotImplementedException();
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
        inventorySystem.AddItem(item.GetComponent<InteractiveObject>());
    }
}
