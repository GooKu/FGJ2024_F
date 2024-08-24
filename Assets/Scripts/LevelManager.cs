using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void Merge(GameObject a, GameObject b, GameObject result)
    {
        var newObj = GameObject.Instantiate(result, a.transform.parent);
        //TODO:set position
        Destroy(a);
        Destroy(b);
    }
}
