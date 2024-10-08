using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DisposableBox : StageObject
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private string stroy;

    private Button button;

    protected override void Start()
    {
        base.Start();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        foreach (var item in items)
        {
            levelManager.GetItem(item);
        }
        levelManager.Dialog(stroy);
        button.interactable = false;
    }
}
