using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] Text dialogText;

    public void Init()
    {
        ClearDialog();
    }

    public void ShowDialog(string text, float duration = 1f)
    {
        ClearDialog();
        dialogText.DOText(text, duration);
    }

    public void ClearDialog()
    {
        dialogText.text = string.Empty;
    }
}
