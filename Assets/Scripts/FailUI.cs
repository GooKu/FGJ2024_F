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

        if(legacyWords.Count > 0)
        {
            string content = "�������oLeagcy��r: ";
            foreach(var s in legacyWords)
            {
                content += $"�u{s}�v ";
            }
            getLegacyText.text = string.Empty;
        }
        else
        {
            getLegacyText.text = string.Empty;
        }

        gameObject.SetActive(true);
    }
}
