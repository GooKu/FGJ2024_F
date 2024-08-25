using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Lv1QTE : StageObject
{
    [SerializeField] private Image timeBar;
    [SerializeField] private UnityEvent PassAction;

    private int step = 0;
    private string goal;

    protected override void Start()
    {
        base.Start();
        setStep(0);
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
        failHandle();
    }

    public override void Touch(StageObject stageObject)
    {
        base.Touch(stageObject);
        if(stageObject.ID == goal)
        {
            step++;
            setStep(step);
        }
        else
        {
            StopAllCoroutines();
            failHandle();
        }
    }

    private void failHandle()
    {
        levelManager.AddLegacy("w026");
        levelManager.AddLegacy("w014");
        levelManager.Fail("�B�c���... �ڳo�r�D�A�L�ĥi��");
        gameObject.SetActive(false);
    }

    private void setStep(int value)
    {
        step = value;
        switch (value)
        {
            case 0:
                {
                    goal = "wc002";
                    SoundManager.PlaySnakeSound();
                }
                break;
            case 1:
                {
                    levelManager.Dialog("�r�b�n����w�F�L���t�סA���ڥi�H�C�C�˷�");
                    goal = "wc001";
                    SoundManager.PlayArrowSound();
                }break;
            case 2:
                {
                    levelManager.Dialog("�A��Ǫ����D���L�ڸ̭��A�T���y�I");
                    StopAllCoroutines();
                    PassAction?.Invoke();
                    gameObject.SetActive(false);
                    SoundManager.PlayExplodeSound();
                }
                break;
        }
    }
}
