using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private GameObject startStage;
    [SerializeField] private GameObject[] stages;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;

    private int index;

    private void Awake()
    {
        if (startStage == null) 
        { 
            index = 0;
        }
        else
        {
            for (int i = 0; i < stages.Length; i++)
            {
                if (stages[i] == startStage) { index = i; break; }
            }
        }
        UpdateStage(index);
    }

    // + is right, - if left 
    public void UpdateStage(int delta)
    {
        updateStage(index + delta);
    }

    public void updateStage(int result)
    {
        if (result < 0 || result >= stages.Length) { return; }

        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].SetActive(i == result);
        }

        leftButton.SetActive(result > 0);
        rightButton.SetActive(result < stages.Length - 1);
        index = result;
    }
}
