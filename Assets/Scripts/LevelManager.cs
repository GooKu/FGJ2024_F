using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;

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
        Destroy(a);
        Destroy(b);
        var obj = Instantiate(result);
        inventorySystem.AddItem(obj.GetComponent<InteractiveObject>());
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
}
