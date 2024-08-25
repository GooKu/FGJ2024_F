using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailUI : MonoBehaviour
{
    [SerializeField] private Text deathText;
    [SerializeField] private Text getLegacyText;

    public void Show(string message, List<string> legacyWords)
    {
        deathText.text = message;
        gameObject.SetActive(true);
    }
}
