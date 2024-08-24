using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class LevelManager : LevelManagerBase
{
    [SerializeField] private TextAsset wordConfigSource;
    private InventorySystem inventorySystem;
    private DialogSystem dialogSystem;
    int currentLevel = 0;

    private Dictionary<string, WordConfig> wordConfigs = new();
    private const string pattern = ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))";

    [SerializeField] Transform levelRoot;
    [SerializeField] List<GameObject> levelPrefabs;

    [SerializeField] GameObject failPanel;
    [SerializeField] GameObject winPanel;

    public override void Init(InventorySystem inventorySystem, DialogSystem dialogSystem)
    {
        this.inventorySystem = inventorySystem;
        this.dialogSystem = dialogSystem;
        ReadWordConfig();
    }
    
    private void ReadWordConfig()
    {
        string[] lines = wordConfigSource.text.Split("\r\n");
        for (int i = 1; i < lines.Length; i++)
        {
            string[] result = Regex.Split(lines[i], pattern);
            WordConfig wc = new();
            wc.ID = result[0];
            wc.Text = result[1];
            wc.IsLagecy = result[2] == "T";
            wordConfigs.Add(wc.ID, wc);
        }
    }

    public override void Merge(GameObject a, GameObject b, GameObject result)
    {
        List<string> words = new();
        words.Add(getWordText(a.GetComponent<InteractiveObject>().ID));
        words.Add(getWordText(b.GetComponent<InteractiveObject>().ID));
        words.Add(getWordText(result.GetComponent<InteractiveObject>().ID));
        dialogSystem.ShowDialog($"把 {words[0]} 跟 {words[1]} 結合後得到了 {words[2]} ！");
        checkAndRemoveObjectInInventory(a);
        checkAndRemoveObjectInInventory(b);
        Destroy(a);
        Destroy(b);
        checkAndAddObjectInInventory(result);
    }

    private void checkAndAddObjectInInventory(GameObject go)
    {
        checkAndAddObjectInInventory(go.GetComponent<InteractiveObject>());
    }

    private void checkAndAddObjectInInventory(InteractiveObject ia)
    {
        if (inventorySystem.TryFindObjInSlotById(ia.ID, out _)) { return; }
        inventorySystem.AddItem(Instantiate(ia));
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
        checkAndRemoveObjectInInventory(go.GetComponent<InteractiveObject>());
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
        List<string> words = new();
        words.Add(getWordText(source.ID));
        foreach (InteractiveObject result in results)
        {
            checkAndAddObjectInInventory(result);
            words.Add(getWordText(result.ID));
        }
        checkAndRemoveObjectInInventory(source);
        Destroy(source.gameObject);
        dialogSystem.ShowDialog($"把 {words[0]} 拆開後得到了 {words[1]} 跟 {words[2]}");
    }

    private string getWordText(string key)
    {
        if(wordConfigs.TryGetValue(key, out var result))
        {
            return result.Text;
        }

        return string.Empty;
    }
}
