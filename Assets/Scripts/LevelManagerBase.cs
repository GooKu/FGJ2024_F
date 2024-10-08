using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class LevelManagerBase : MonoBehaviour
{
    public abstract void Init(InventorySystem inventorySystem, DialogSystem dialogSystem);
    public abstract void StartLevel(int level);
    public abstract void Merge(GameObject a, GameObject b, GameObject result);
    public abstract void Dismantle(InteractiveObject source, List<InteractiveObject> results);
    public abstract void Pass(InteractiveObject interactiveObject = null);
    public abstract void Fail(string message = "");
    public abstract void Dialog(string message);
    public abstract void GetItem(GameObject item);
}
