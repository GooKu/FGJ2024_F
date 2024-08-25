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
        levelManager.Dialog("你該怎麼做");

        do
        {
            yield return null;
            remainTime -= Time.deltaTime;
            timeBar.fillAmount = remainTime/time;
        } while (remainTime > 0);
        levelManager.Fail("冰箱怎麼有... 啊這毒蛇，無藥可醫");
    }
}
