using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class LevelManagerBase : MonoBehaviour
{
    public abstract void Merge(GameObject a, GameObject b, GameObject result);
}
