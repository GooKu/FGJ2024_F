using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lv1QTE : StageObject
{
    [SerializeField] private Image timeBar;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(countDown(10));
    }

    private IEnumerator countDown(float time)
    {
        float remainTime = time;
        timeBar.fillAmount = 1;
        levelManager.Dialog("�A�ӫ��");

        do
        {
            yield return null;
            remainTime -= Time.deltaTime;
            timeBar.fillAmount = remainTime/time;
        } while (remainTime > 0);
        levelManager.Fail("�B�c���... �ڳo�r�D�A�L�ĥi��");
    }
}
