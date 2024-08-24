using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject titlePage;
    [SerializeField] GameObject mainScenePage;

    [Header("System")]
    [SerializeField] DialogSystem dialogSystem;

    void Start()
    {
        // init system
        dialogSystem.Init();
    }

    void Update()
    {

    }

    public void StartNewGame()
    {
        // setup main scene
        titlePage.SetActive(false);
        mainScenePage.SetActive(true);
    }

    public void LeaveCurrentGame()
    {
        // close main scene, return to title
        titlePage.SetActive(true);
        mainScenePage.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
