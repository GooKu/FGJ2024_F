using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : LevelManagerBase
{
    private InventorySystem inventorySystem;
    private DialogSystem dialogSystem;
    int currentLevel = 0;

    [SerializeField] Transform levelRoot;
    [SerializeField] List<GameObject> levelPrefabs;

    [SerializeField] GameObject failPanel;
    [SerializeField] GameObject winPanel;

    public override void Init(InventorySystem inventorySystem, DialogSystem dialogSystem)
    {
        this.inventorySystem = inventorySystem;
        this.dialogSystem = dialogSystem;
    }

    public override void Merge(GameObject a, GameObject b, GameObject result)
    {
        checkAndRemoveObjectInInventory(a);
        checkAndRemoveObjectInInventory(b);
        Destroy(a);
        Destroy(b);
        var obj = Instantiate(result);
        inventorySystem.AddItem(obj.GetComponent<InteractiveObject>());
    }

    public override void StartLevel(int level)
    {
        failPanel.SetActive(false);
        winPanel.SetActive(false);

        currentLevel = level;
        Destroy(levelRoot.Find($"Level_{level}").gameObject);
        Instantiate(levelPrefabs[level], levelRoot);
    }

    private void checkAndRemoveObjectInInventory(GameObject go)
    {
        InteractiveObject ia = go.GetComponent<InteractiveObject>();
        checkAndRemoveObjectInInventory(ia);
    }

    private void checkAndRemoveObjectInInventory(InteractiveObject ia)
    {
        if (inventorySystem.TryFindObjInSlotById(ia.ID, out _))
        {
            inventorySystem.RemoveItem(ia);
        }
    }

    public override void Dialog(string message)
    {
        dialogSystem.ShowDialog(message);
    }

    public override void Fail()
    {
        failPanel.SetActive(true);
    }

    public override void Pass()
    {
        int totalLevelCount = levelPrefabs.Count;
        bool isLastLevel = currentLevel == totalLevelCount - 1;

        if (isLastLevel)
        {
            winPanel.SetActive(true);
        }
        else
        {
            StartLevel(currentLevel + 1);
        }
    }

    public override void GetItem(GameObject item)
    {
        var obj = Instantiate(item);
        inventorySystem.AddItem(obj.GetComponent<InteractiveObject>());
    }

    public override void Dismantle(InteractiveObject source, List<InteractiveObject> results)
    {
        List<InteractiveObject> objs = new();
        foreach (InteractiveObject result in results)
        {
            objs.Add(Instantiate(result));
        }
        inventorySystem.AddItem(objs);
        checkAndRemoveObjectInInventory(source);
        Destroy(source.gameObject);
    }
}
