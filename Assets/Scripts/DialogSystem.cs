using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] Text dialogText;
    [SerializeField] Text endText;
    Tweener textTweener;
    Tweener endTextTweener;

    public void Init()
    {
        ClearDialog();
    }

    public void ShowDialog(string text, float duration = 1f)
    {
        textTweener?.Kill();
        endTextTweener?.Kill();
        ClearDialog();
        textTweener = dialogText.DOText(text, duration);
        endTextTweener = endText.DOText(text, duration);
    }

    public void ClearDialog()
    {
        dialogText.text = string.Empty;
        endText.text = string.Empty;
    }
}
