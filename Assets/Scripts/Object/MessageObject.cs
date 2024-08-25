using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MessageObject : StageObject
{
    [SerializeField] private string message;

    protected override void Start()
    {
        base.Start();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        levelManager.Dialog(message);
    }
}
