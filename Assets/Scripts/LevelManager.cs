using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] FailUI failPanel;
    [SerializeField] Button restartBtn;
    [SerializeField] GameObject winPanel;
    [SerializeField] LegacyBagUI legacyBagUI;

    private Dictionary<string, WordConfig> lagecyBag = new();

    private List<string> getLagecyThisTime = new();

    [Header("Debug")]
    [SerializeField] List<GameObject> testWordPrefabs;
    [SerializeField] Vector2Int addWordRange;

    public override void Init(InventorySystem inventorySystem, DialogSystem dialogSystem)
    {
        this.inventorySystem = inventorySystem;
        this.dialogSystem = dialogSystem;
        ReadWordConfig();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            List<InteractiveObject> iobjs = new();
            Debug.Log($"add {addWordRange.x}-{addWordRange.y}");
            for (int i = addWordRange.x; i < addWordRange.y + 1; i++)
            {
                iobjs.Add(Instantiate(testWordPrefabs[i]).GetComponent<InteractiveObject>());
            }
            inventorySystem.AddItem(iobjs);
        }
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

    private void RegisterRestartBtnEvent(int level)
    {
        restartBtn.onClick.RemoveAllListeners();
        restartBtn.onClick.AddListener(() => RestartLevel(level));
    }

    public override void Merge(GameObject a, GameObject b, GameObject result)
    {
        List<string> words = new();
        words.Add(getWordText(a.GetComponent<InteractiveObject>().ID));
        words.Add(getWordText(b.GetComponent<InteractiveObject>().ID));
        words.Add(getWordText(result.GetComponent<InteractiveObject>().ID));
        dialogSystem.ShowDialog($"把 {words[0]} 跟 {words[1]} 結合後得到了 {words[2]} ！");
        checkAndRemoveObject(a);
        checkAndRemoveObject(b);
        checkAndAddObjectInInventory(result);
    }

    private void checkAndAddObjectInInventory(GameObject go)
    {
        checkAndAddObjectInInventory(go.GetComponent<InteractiveObject>());
    }

    private void checkAndAddObjectInInventory(InteractiveObject ia)
    {
        if (checkLagecy(ia.ID) && !lagecyBag.ContainsKey(ia.ID))
        {
            var config = wordConfigs[ia.ID];
            config.Image = ia.GetComponent<Image>().sprite;
            lagecyBag.Add(ia.ID, config);
            getLagecyThisTime.Add(config.Text);
        }

        if (inventorySystem.TryFindObjInSlotById(ia.ID, out _)) { return; }
        inventorySystem.AddItem(Instantiate(ia));
    }

    public override void StartLevel(int level)
    {
        failPanel.gameObject.SetActive(false);
        winPanel.SetActive(false);

        dialogSystem.ClearDialog();

        currentLevel = level;
        Destroy(levelRoot.Find($"Level_{level}").gameObject);
        Instantiate(levelPrefabs[level], levelRoot).name = $"Level_{level}";
        RegisterRestartBtnEvent(level);
    }

    private void RestartLevel(int level)
    {
        StartLevel(level);
        inventorySystem.ClearAllItems();
    }

    private void checkAndRemoveObject(GameObject go)
    {
        checkAndRemoveObject(go.GetComponent<InteractiveObject>());
    }

    private void checkAndRemoveObject(InteractiveObject ia)
    {
        if (checkLagecy(ia.ID)) { return; }

        if (inventorySystem.TryFindObjInSlotById(ia.ID, out _))
        {
            inventorySystem.RemoveItem(ia);
        }

        Destroy(ia.gameObject);
    }

    public override void Dialog(string message)
    {
        dialogSystem.ShowDialog(message);
    }

    public override void Fail(string message = "")
    {
        failPanel.Show(message, getLagecyThisTime);
        getLagecyThisTime.Clear();
    }

    public override void Pass(InteractiveObject interactiveObject = null)
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

        if(interactiveObject != null)
        {
            checkAndRemoveObject(interactiveObject);
        }
    }

    public override void GetItem(GameObject item)
    {
        checkAndAddObjectInInventory(item);
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
        checkAndRemoveObject(source);
        dialogSystem.ShowDialog($"把 {words[0]} 拆開後得到了 {words[1]} 跟 {words[2]}");
    }

    //setup at button
    public void ShowLegacyBag()
    {
        legacyBagUI.Show(lagecyBag);
    }
    //setup at button
    public void AddLegacyFromBag(LegacyButton button)
    {
        var obj = Resources.Load<InteractiveObject>($"Words/{button.ID}");
        checkAndAddObjectInInventory(obj);
    }

    private string getWordText(string id)
    {
        if(wordConfigs.TryGetValue(id, out var result))
        {
            return result.Text;
        }

        return string.Empty;
    }

    private bool checkLagecy(string id)
    {
        if (wordConfigs.TryGetValue(id, out var result))
        {
            return result.IsLagecy;
        }

        return false;
    }
}
