using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class LevelManagerBase : MonoBehaviour
{
    public abstract void Merge(GameObject a, GameObject b, GameObject result);
    public abstract void Dismantle(InteractiveObject source, List<InteractiveObject> results);
    public abstract void Pass();
    public abstract void Fail();
    public abstract void Dialog(string message);
    public abstract void GetItem(GameObject item);
}
