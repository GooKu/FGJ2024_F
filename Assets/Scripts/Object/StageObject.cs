using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObject : MonoBehaviour
{
    [SerializeField] protected string id;
    public string ID => id;

    protected LevelManagerBase levelManager;

    protected virtual void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManagerBase>();
    }
}
