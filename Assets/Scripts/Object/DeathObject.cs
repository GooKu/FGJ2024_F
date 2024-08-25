using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DeathObject : StageObject
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private string message;

    protected override void Start()
    {
        base.Start();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        foreach (var item in items)
        {
            levelManager.GetItem(item);
        }
        levelManager.Fail(message);
    }
}
