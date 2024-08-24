using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DisposableBox : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private string stroy;

    private Button button;
    private LevelManagerBase levelManager;

    private void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManagerBase>();
        button = GetComponent<Button>();
    }

    // setup at button
    public void OnClick()
    {
        levelManager.GetItem(item);
        levelManager.Dialog(stroy);
        button.interactable = false;
    }
}
