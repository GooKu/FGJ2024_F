using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DisposableBox : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private string stroy;

    private Button button;
    private LevelManagerBase levelManager;

    private void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManagerBase>();
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
