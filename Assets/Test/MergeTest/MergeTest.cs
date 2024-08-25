using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeTest : LevelManagerBase
{
    public override void Merge(GameObject a, GameObject b, GameObject result)
    {
        var newObj = GameObject.Instantiate(result, a.transform.parent);
        Destroy(a);
        Destroy(b);
    }

    public override void Pass(InteractiveObject interactiveObject = null)
    {
        throw new System.NotImplementedException();
    }
    public override void Fail(string message = "")
    {
        throw new System.NotImplementedException();
    }

    public override void Dialog(string message)
    {
        throw new System.NotImplementedException();
    }

    public override void GetItem(GameObject item)
    {
        throw new System.NotImplementedException();
    }

    public override void Dismantle(InteractiveObject source, List<InteractiveObject> results)
    {
        throw new System.NotImplementedException();
    }

    public override void Init(InventorySystem inventorySystem, DialogSystem dialogSystem)
    {
        throw new System.NotImplementedException();
    }

    public override void StartLevel(int level)
    {
        throw new System.NotImplementedException();
    }
}
