using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MessageObject : MonoBehaviour
{
    [SerializeField] private string message;

    private LevelManagerBase levelManager;

    private void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManagerBase>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        levelManager.Dialog(message);
    }
}
