using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;
using Newtonsoft.Json.Linq;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] List<VectorValue> playerPositions;
    [SerializeField] PartyStats Gary;
    [SerializeField] PartyStats L;
    [SerializeField] List<MemoryBase> memories;
    [SerializeField] List<CutsceneBase> cutscenes;
    [SerializeField] List<ChestData> chests;
    [SerializeField] List<DialogueScriptObject> dialogueObjects;
    [SerializeField] List<Equipment> LEquip;
    [SerializeField] List<Equipment> GaryEquip;
    [SerializeField] List<LearnableMove> MCDefaultMoves;
    [SerializeField] List<LearnableMove> LDefaultMoves;
    [SerializeField] List<LearnableMove> GaryDefaultMoves;
    private DialogueManager dialogueManager;

    public class ListObject<T>
    {
        public List<T> objects;

        public ListObject(List<T> objects)
        {
            this.objects = objects;
        }
    }

    public class SaveData
    {
        public string playerName;
        public int level; 
        public int money;
        public string world;

        public SaveData(string playerName, int level, int money, string world)
        {
            this.playerName = playerName;
            this.level = level;
            this.money = money;
            this.world = world;
        }
    }
    // (0.5, 0) for new game player position. Load intro scene.

    void Start()
    {
        dialogueManager = DialogueManager.GetInstance();
    }
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.BackQuote))
        //     SaveGame();
    }
    public void NewGame()
    {
        // Set all to default presets
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < chests.Count; i++)
            chests[i].Opened = false;
        for (int i = 0; i < cutscenes.Count; i++)
            cutscenes[i].SetViewed(false);
        for (int i = 0; i < memories.Count; i++)
            memories[i].SetViewed(false);
        for (int i = 0; i < dialogueObjects.Count; i++)
            dialogueObjects[i].Played = false;
        playerPositions[0].initialValue = new Vector2(12.5f, 28f);
        playerPositions[1].initialValue = new Vector2(0.5f, 0f);
        DefaultMC();
        DefaultGary();
        DefaultL();
        SceneManager.LoadScene(playerStats.CurScene);
    }
    public void SaveGame()
    {
        // Player stats
        string saveData = JsonUtility.ToJson(playerStats) + ";";

        // Player position
        for (int i = 0; i < playerPositions.Count; i++)
            saveData += JsonUtility.ToJson(playerPositions[i]) + ";";
        
        // Gary
        saveData += JsonUtility.ToJson(Gary) + ";";

        // L
        saveData += JsonUtility.ToJson(L) + ";";

        // Memories
        for (int i = 0; i < memories.Count; i++)
            saveData += memories[i].IsViewed + ";";

        // Cutscenes
        for (int i = 0; i < cutscenes.Count; i++)
            saveData += cutscenes[i].IsViewed + ";";

        // Chests
        for (int i = 0; i < chests.Count; i++)
            saveData += chests[i].Opened + ";";

        // Dialogue Objects
        for (int i = 0; i < dialogueObjects.Count; i++)
            saveData += dialogueObjects[i].Played + ";";

        string inkVars = "";
        if (PlayerPrefs.HasKey("INK_VARIABLES"))
            inkVars = PlayerPrefs.GetString("INK_VARIABLES");
        saveData += inkVars;

        //string loadGlobals = loadGlobalsJson.text;
        File.WriteAllText(Application.persistentDataPath + "/saveData.txt", saveData);
        Debug.Log("Saved");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/saveData.txt"))
        {
            // Load everything from file
            string saveData = File.ReadAllText(Application.persistentDataPath + "/saveData.txt");
            string[] dataCategories = saveData.Split(';');

            //Player Stats
            PlayerStats tempPlayer = ScriptableObject.CreateInstance<PlayerStats>();
            JsonUtility.FromJsonOverwrite(dataCategories[0], tempPlayer);
            OverwriteMC(tempPlayer);

            // Position
            for (int i = 0; i < playerPositions.Count; i++)
                JsonUtility.FromJsonOverwrite(dataCategories[1 + i], playerPositions[i]);

            // Gary
            PartyStats tempGary = ScriptableObject.CreateInstance<PartyStats>();
            JsonUtility.FromJsonOverwrite(dataCategories[4], tempGary);
            OverwriteGary(tempGary);

            // L
            PartyStats tempL = ScriptableObject.CreateInstance<PartyStats>();
            JsonUtility.FromJsonOverwrite(dataCategories[5], tempL);
            OverwriteL(tempL);

            // Memories
            for (int i = 0; i < memories.Count; i++)
                memories[i].SetViewed(bool.Parse(dataCategories[6 + i]));

            // Cutscenes
            for (int i = 0; i < cutscenes.Count; i++)
                cutscenes[i].SetViewed(bool.Parse(dataCategories[18 + i]));

            // Chests
            for (int i = 0; i < chests.Count; i++)
                chests[i].Opened = bool.Parse(dataCategories[27 + i]);

            // Dialogue Objects
            for (int i = 0; i < dialogueObjects.Count; i++)
                dialogueObjects[i].Played = bool.Parse(dataCategories[43 + i]);

            PlayerPrefs.SetString("INK_VARIABLES", dataCategories[48]);
            SceneManager.LoadScene(playerStats.CurScene);
        }
        else if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            NewGame();
        }
    }
    public SaveData PreviewSaveData()
    {
        if (File.Exists(Application.persistentDataPath + "/saveData.txt"))
        {
            string saveData = File.ReadAllText(Application.persistentDataPath + "/saveData.txt");
            string[] dataCategories = saveData.Split(';');
            JObject obj = JObject.Parse(dataCategories[0]);
            return new SaveData((string) obj["MemberName"], (int) obj["level"], (int) obj["money"], (string) obj["curScene"]);
        }
        else
            return null;
    }

    private void OverwriteL(PartyStats tempAlly)
    {
        L.Level = tempAlly.Level;
        L.Xp = tempAlly.Xp;
        L.MaxHP = tempAlly.MaxHP;
        L.CurHP = tempAlly.CurHP;
        L.MaxMP = tempAlly.MaxMP;
        L.CurMP = tempAlly.CurMP;
        L.Attack = tempAlly.Attack;
        L.Defense = tempAlly.Defense;
        L.Speed = tempAlly.Speed;
        L.Weapon = tempAlly.Weapon;
        L.Armor = tempAlly.Armor;
        L.LearnableMoves = tempAlly.LearnableMoves;
        L.MoveSet = tempAlly.MoveSet;
    }
    private void OverwriteGary(PartyStats tempAlly)
    {
        Gary.Level = tempAlly.Level;
        Gary.Xp = tempAlly.Xp;
        Gary.MaxHP = tempAlly.MaxHP;
        Gary.CurHP = tempAlly.CurHP;
        Gary.MaxMP = tempAlly.MaxMP;
        Gary.CurMP = tempAlly.CurMP;
        Gary.Attack = tempAlly.Attack;
        Gary.Defense = tempAlly.Defense;
        Gary.Speed = tempAlly.Speed;
        Gary.Weapon = tempAlly.Weapon;
        Gary.Armor = tempAlly.Armor;
        Gary.LearnableMoves = tempAlly.LearnableMoves;
        Gary.MoveSet = tempAlly.MoveSet;
    }

    private void OverwriteMC(PlayerStats tempMC)
    {
        playerStats.Level = tempMC.Level;
        playerStats.Xp = tempMC.Xp;
        playerStats.MaxHP = tempMC.MaxHP;
        playerStats.CurHP = tempMC.CurHP;
        playerStats.MaxMP = tempMC.MaxMP;
        playerStats.CurMP = tempMC.CurMP;
        playerStats.Attack = tempMC.Attack;
        playerStats.Defense = tempMC.Defense;
        playerStats.Speed = tempMC.Speed;
        playerStats.Weapon = tempMC.Weapon;
        playerStats.Armor = tempMC.Armor;
        playerStats.LearnableMoves = tempMC.LearnableMoves;
        playerStats.MoveSet = tempMC.MoveSet;
        playerStats.CurScene = tempMC.CurScene;
        playerStats.Money = tempMC.Money;
        playerStats.Points = tempMC.Points;
        playerStats.CutScenes = tempMC.CutScenes;
        playerStats.Memories = tempMC.Memories;
        playerStats.Inventory = tempMC.Inventory;
        playerStats.Equipment = tempMC.Equipment;
    }

    private void DefaultGary()
    {
        Gary.Level = 1;
        Gary.Xp = 0;
        Gary.MaxHP = 75;
        Gary.CurHP = 75;
        Gary.MaxMP = 25;
        Gary.CurMP = 25;
        Gary.Attack = 11;
        Gary.Defense = 12;
        Gary.Speed = 10;
        Gary.Weapon = (Weapon) GaryEquip[0];
        Gary.Armor = (Armor) GaryEquip[1];
        Gary.LearnableMoves.Clear();
        Gary.MoveSet = GaryDefaultMoves;
    }

    private void DefaultL()
    {
        L.Level = 10;
        L.Xp = 0;
        L.MaxHP = 90;
        L.CurHP = 90;
        L.MaxMP = 60;
        L.CurMP = 60;
        L.Attack = 50;
        L.Defense = 10;
        L.Speed = 25;
        L.Weapon = (Weapon) LEquip[0];
        L.Armor = (Armor) LEquip[1];
        L.LearnableMoves.Clear();
        L.MoveSet = LDefaultMoves;
    }

    private void DefaultMC()
    {
        playerStats.Level = 1;
        playerStats.Xp = 1;
        playerStats.MaxHP = 50;
        playerStats.CurHP = 50;
        playerStats.MaxMP = 25;
        playerStats.CurMP = 25;
        playerStats.Attack = 10;
        playerStats.Defense = 10;
        playerStats.Speed = 10;
        playerStats.Weapon = null;
        playerStats.Armor = null;
        playerStats.LearnableMoves.Clear();
        playerStats.MoveSet = MCDefaultMoves;
        playerStats.CurScene = "GameIntroduction";
        playerStats.Money = 0;
        playerStats.Points = 0;
        playerStats.CutScenes.Clear();
        playerStats.Memories.Clear();
        playerStats.Inventory.Clear();
        playerStats.Equipment.Clear();
    }
}
