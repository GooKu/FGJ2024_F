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

    private Dictionary<string, WordConfig> wordConfigs = new();
    private const string pattern = ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))";

    private void Awake()
    {
        inventorySystem = GameObject.FindObjectOfType<InventorySystem>();
        dialogSystem = GameObject.FindObjectOfType<DialogSystem>();

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
        throw new System.NotImplementedException();
    }

    public override void Pass()
    {
        throw new System.NotImplementedException();
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
