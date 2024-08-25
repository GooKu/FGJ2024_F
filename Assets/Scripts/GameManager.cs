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
    [SerializeField] InventorySystem inventorySystem;
    [SerializeField] LevelManager levelManager;

    void Start()
    {
        // init system
        dialogSystem.Init();
        inventorySystem.Init();
        levelManager.Init(inventorySystem, dialogSystem);

        titlePage.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            levelManager.Pass();
        }
        if (Input.GetKey(KeyCode.V))
        {
            levelManager.Fail();
        }
    }

    public void StartNewGame(int level)
    {
        // setup main scene
        titlePage.SetActive(false);
        mainScenePage.SetActive(true);
        levelManager.NewGameInit();
        levelManager.StartLevel(level);
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
