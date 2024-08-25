using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegacyButton : MonoBehaviour
{
    [SerializeField] private Image image;

    public string ID {  get; private set; }

    public void Init(string id, Sprite sprite)
    {
        ID = id;
        image.sprite = sprite;
    }
}
