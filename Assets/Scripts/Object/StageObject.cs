using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObject : MonoBehaviour
{
    [SerializeField] protected string id;
    public string ID => id;

    protected LevelManager levelManager;

    protected virtual void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    public virtual void Touch(StageObject stageObject) { }
}
