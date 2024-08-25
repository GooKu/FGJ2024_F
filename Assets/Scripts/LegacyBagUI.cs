using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegacyBagUI : MonoBehaviour
{
    [SerializeField] private LegacyButton buttonSample;

    private Stack<LegacyButton> activeButtons = new();
    private Stack<LegacyButton> poolButtons = new();

    public void Show(Dictionary<string, WordConfig> datas)
    {
        foreach(var l in datas)
        {
            LegacyButton button;

            if (poolButtons.Count > 0)
            {
                button = poolButtons.Pop();
            }
            else
            {
                button = Instantiate(buttonSample, buttonSample.transform.parent);
            }
            button.Init(l.Key, l.Value.Image);
            button.gameObject.SetActive(true);
            activeButtons.Push(button);
        }
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        int count = activeButtons.Count;
        for(int i = 0; i < count; i++)
        {
            var button = activeButtons.Pop();
            button.gameObject.SetActive(false);
            poolButtons.Push(button);
        }
        gameObject.SetActive(false);
    }
}
