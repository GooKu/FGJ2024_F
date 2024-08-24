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
}
