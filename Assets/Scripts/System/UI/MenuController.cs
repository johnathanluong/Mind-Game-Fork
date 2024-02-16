using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;
using static Pathfinding.RaycastModifier;
using Debug = UnityEngine.Debug;
using Input = UnityEngine.Input;

public class MenuController : MonoBehaviour
{
    private static MenuController _instance;
    private AudioSource Audio;
    public AudioClip soundClip1=null;
    public AudioClip soundClip2=null;
    public AudioClip soundClip3=null;
    private GameObject canvas;
    private GameObject baseMenu;
    private GameObject menu;
    private GameObject selectionMenu;
    private GameObject mcStats;
    private GameObject memberStats;
    private GameObject otherMoves;
    private GameObject interactBox;
    private GameObject noItems;
    private List<GameObject> partyMemberScreen = new List<GameObject>();

    private GameObject downArrow;
    private GameObject upArrow;

    List<TextMeshProUGUI> menuItems;
    List<TextMeshProUGUI> mcItems;
    List<TextMeshProUGUI> memberItems;
    List<TextMeshProUGUI> menuItems2;

    List<List<TextMeshProUGUI>> partyMemberItems = new List<List<TextMeshProUGUI>>();

    GameManager gameManager;

    PlayerStats playerStats;
    List<PartyStats> partyStats;
    MemoryList memoryList;

    int selectedItem = 0;
    int currentMember = 0;
    int layer = 0;
    int maxLayer = 1;

    int menuNum = 0;
    int invenCount = 0;
    int onScreenCount = 0;
    int invenLevels = 0;
    int curLevel = 0;
    bool fullLevel = false;
    Buyable item;
    int itemIndex;
    int itemIndex2;
    string curMemberName;
    int memberSelected;
    MemoryBase curMem;
    LearnableMove prevMove;
    LearnableMove curMove;
    Stats charStats;

    bool menuOpened = false;
    bool gotInfo = true;
    Menu curMenu = Menu.Base;

    int points;
    int allocated;
    int initPower;
    int initDefense;
    int initSpeed;
    int newPower;
    int newDefense;
    int newSpeed;
    public bool opened=false;

    public enum Menu
    {
        Party,
        Inventory,
        Moves,
        Memories,
        Options,
        Save,
        Load,
        Base
    }
    public static MenuController Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("MenuController is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    public bool MenuOpened
    {
        get { return menuOpened; }
        set { menuOpened = value; }
    }
    private void Start()
    {
        Audio = this.gameObject.AddComponent<AudioSource>();
        gameManager = GameManager.Instance;
        playerStats = gameManager.PlayerStats;
        partyStats = gameManager.PartyStats;
        memoryList = gameManager.MemoryList;

        canvas = GameObject.FindGameObjectWithTag("UI");

        curMemberName = playerStats.Name;

        menu = canvas.transform.GetChild(1).gameObject;

        selectionMenu = menu.transform.GetChild(0).gameObject;
        mcStats = menu.transform.GetChild(1).gameObject;
        memberStats = menu.transform.GetChild(2).gameObject;

        menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        mcItems = mcStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        memberItems = memberStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();


        partyMemberScreen.Clear();

        for (int i = 0; i < 4; ++i)
        {
            partyMemberScreen.Add(canvas.transform.GetChild(2).transform.GetChild(i + 1).gameObject);       //gets the party member screen sheets
        }
    }
    void Update()
    {
        if (!canvas)
        {   
            StartCoroutine(FindCanvas());

            gameManager = GameManager.Instance;
            playerStats = gameManager.PlayerStats;
            partyStats = gameManager.PartyStats;
            memoryList = gameManager.MemoryList;

            menu = canvas.transform.GetChild(1).gameObject;

            selectionMenu = menu.transform.GetChild(0).gameObject;
            mcStats = menu.transform.GetChild(1).gameObject;
            memberStats = menu.transform.GetChild(2).gameObject;

            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            mcItems = mcStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            memberItems = memberStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        }
        if (menuOpened)
        {    
            if(opened==false)
            {
            Audio.clip=soundClip1;
            Audio.PlayOneShot(soundClip1);
            opened=true;
            }
            Selection();
            //Debug.Log("Layer: " + layer);
            //Debug.Log("Menu: " + curMenu);
            //Debug.Log("CurMenuName: " + menu.gameObject.name);
        }
    }

    public void OpenMenu()  //opens the menu
    {
        StartCoroutine(FindCanvas());

        gameManager = GameManager.Instance;
        playerStats = gameManager.PlayerStats;
        partyStats = gameManager.PartyStats;
        memoryList = gameManager.MemoryList;

        menu = canvas.transform.GetChild(1).gameObject;

        if (gameManager.World != -1)
        {
            memberStats = menu.transform.GetChild(2).gameObject;
            memberItems = memberStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            memberStats.SetActive(true);
        }
        else
            menu.transform.GetChild(2).gameObject.SetActive(false);

        selectionMenu = menu.transform.GetChild(0).gameObject;
        mcStats = menu.transform.GetChild(1).gameObject;

        menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        mcItems = mcStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        

        partyMemberScreen.Clear();

        for (int i = 0; i < 4; ++i)
        {
            partyMemberScreen.Add(canvas.transform.GetChild(2).transform.GetChild(i + 1).gameObject);       //gets the party member screen sheets
        }

        menuOpened = true;
        menu.SetActive(true);
        SelectMenu();
        UpdateItemSelection();
    }

    IEnumerator FindCanvas()
    {
        yield return canvas = GameObject.FindGameObjectWithTag("UI");
    }
    void NextMenu()     //go to the next menu
    {
        if(layer < maxLayer)
        {
            menu.SetActive(false);
            gotInfo = false;
            ++layer;
            selectedItem = 0;
            SelectMenu();
            menu.SetActive(true);
            UpdateItemSelection();
        }
    }
    void BackMenu()     //go back a menu
    {
        menu.SetActive(false);
        --layer;
        selectedItem = 0;
        gotInfo = false;
        if (layer == 0) //back to base
        {
            curMenu = Menu.Base;
            partyMemberItems.Clear();
            curMemberName = playerStats.Name;
        }
        SelectMenu();
        menu.SetActive(true);
        UpdateItemSelection();
    }
    void CloseMenu()        //resets the controller to default
    {
        Audio.PlayOneShot(soundClip1);
        opened=false;
        menuOpened = false;
        gotInfo = false;
        menu.SetActive(false);
        curMenu = Menu.Base;
        layer = 0;
        partyMemberItems.Clear();
    }
    void SelectMenu()           //handles the data for each menu
    {
        switch (curMenu)
        {
            case Menu.Base:
                BaseMenu();
                break;
            case Menu.Party:
                PartyMenu();
                break;
            case Menu.Inventory:
                InventoryMenu();
                break;
            case Menu.Moves:
                MovesMenu();
                break;
            case Menu.Memories:
                MemoryMenu();
                break;
            case Menu.Options:
                OptionMenu();
                break;
            case Menu.Save:
                SaveMenu();
                break;
            case Menu.Load:
                LoadMenu();
                break;
        }
    }
    void Selection()        //handles the actual controlling the menu, switches based on which menu it's on
    {
        switch (curMenu)
        {
            case Menu.Base:
                BaseMenuSelect();
                break;
            case Menu.Party:
                PartyMenuSelect();
                break;
            case Menu.Inventory:
                InventoryMenuSelect();
                break;
            case Menu.Moves:
                MovesMenuSelect();
                break;
            case Menu.Memories:
                MemoryMenuSelect();
                break;
            case Menu.Options:
                OptionMenuSelect();
                break;
            case Menu.Save:
                SaveMenuSelect();
                break;
            case Menu.Load:
                LoadMenuSelect();
                break;
        }
    }
    void BaseMenu()
    {
        currentMember = gameManager.World;
        if (!gotInfo)
        {
            menu = canvas.transform.GetChild(1).gameObject;
            selectionMenu = menu.transform.GetChild(0).gameObject;
            mcStats = menu.transform.GetChild(1).gameObject;

            if (gameManager.World != -1)
            {
                memberStats = menu.transform.GetChild(2).gameObject;
                memberItems = memberStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();
                memberStats.SetActive(true);
            }
            else
                menu.transform.GetChild(2).gameObject.SetActive(false);


            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            mcItems = mcStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            maxLayer = 1;

            gotInfo = true;
        }
        if (menuOpened && curMenu == Menu.Base)
        {
            Image mcPortrait;
            Image allyPortrait;

            mcPortrait = mcStats.transform.GetChild(0).GetComponent<Image>();
            mcPortrait.sprite = playerStats.Portrait;
            if (gameManager.World != -1)
            {
                allyPortrait = memberStats.transform.GetChild(0).GetComponent<Image>();
                allyPortrait.sprite = partyStats[currentMember].Portrait;
            }
            
                

            mcItems[0].text = playerStats.Name;
            mcItems[1].text = "HP: " + playerStats.CurHP + "/" + playerStats.MaxHP;
            mcItems[2].text = "MP: " + playerStats.CurMP + "/" + playerStats.MaxMP;
            mcItems[3].text = "Level: " + playerStats.Level;

            if (gameManager.World != -1)
            {
                memberItems[0].text = partyStats[currentMember].Name;
                memberItems[1].text = "HP: " + partyStats[currentMember].CurHP + "/" + partyStats[currentMember].MaxHP;
                memberItems[2].text = "MP: " + partyStats[currentMember].CurMP + "/" + partyStats[currentMember].MaxMP;
                memberItems[3].text = "Level: " + partyStats[currentMember].Level;
            }

            TextMeshProUGUI money = menu.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            money.text = "$" + playerStats.Money;
        }
    }
    void PartyMenu()
    {
        switch (layer)
        {
            case 1:
                PartyLayer1();
                break;
            case 2:
                PartyLayer2();
                break;
        }
    }
    void PartyLayer1()
    {
        if (!gotInfo)
        {
            menu = canvas.transform.GetChild(2).gameObject;

            selectionMenu = menu.transform.GetChild(0).gameObject;
            mcStats = menu.transform.GetChild(1).gameObject;

            for(int i = 1; i < 4; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
            }

            points = playerStats.Points;
            for (int i = 0; i < partyStats.Count; ++i)
            {
                memberStats = menu.transform.GetChild(2 + i).gameObject;
                memberItems = memberStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();
                partyMemberItems.Add(memberItems);
            }

            for(int i = partyStats.Count; i < 3; ++i)
            {
                selectionMenu.transform.GetChild(i + 1).gameObject.SetActive(false);
            }

            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            mcItems = mcStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            maxLayer = 2;

            gotInfo = true;
        }

        mcItems[0].text = playerStats.Name;
        mcItems[1].text = "HP: " + playerStats.CurHP + "/" + playerStats.MaxHP;
        mcItems[2].text = "MP: " + playerStats.CurMP + "/" + playerStats.MaxMP;
        mcItems[3].text = "Level: " + playerStats.Level;
        mcItems[4].text = "Power: " + playerStats.Attack;
        mcItems[5].text = "Defense: " + playerStats.Defense;
        mcItems[6].text = "Speed: " + playerStats.Speed;

        for(int i = 0; i < playerStats.MoveSet.Count; ++i)
        {
            mcItems[8 + i].text = playerStats.MoveSet[i].Base.name;
        }

        if (!playerStats.Weapon)
        {
            mcItems[13].text = "Weapon: None";
        }
        else
        {
            mcItems[13].text = "Weapon: " + playerStats.Weapon.name + " (+" + playerStats.Weapon.WeaponAttack + ")";
        }
        if (!playerStats.Armor)
        {
            mcItems[14].text = "Armor: None";
        }
        else
        {
            mcItems[14].text = "Armor: " + playerStats.Armor.name + " (+" + playerStats.Armor.ArmorDefense + ")";
        }

        mcItems[15].text = "Points Available: " + points;
        mcItems[16].text = "";
        mcItems[17].text = "XP: " + playerStats.Xp + "/" + playerStats.XpNeeded;

        for (int i = 0; i < partyStats.Count; ++i)
        {
            partyMemberItems[i][0].text = partyStats[i].Name;
            partyMemberItems[i][1].text = "HP: " + partyStats[i].CurHP + "/" + partyStats[i].MaxHP;
            partyMemberItems[i][2].text = "MP: " + partyStats[i].CurMP + "/" + partyStats[i].MaxMP;
            partyMemberItems[i][3].text = "Level: " + partyStats[i].Level;
            partyMemberItems[i][4].text = "Power: " + partyStats[i].Attack;
            partyMemberItems[i][5].text = "Defense: " + partyStats[i].Defense;
            partyMemberItems[i][6].text = "Speed: " + partyStats[i].Speed;
            partyMemberItems[i][15].text = "XP: " + partyStats[i].Xp + "/" + partyStats[i].XpNeeded;

            for (int j = 0; j < partyStats[i].MoveSet.Count; ++j)
            {
                partyMemberItems[i][8 + j].text = partyStats[i].MoveSet[j].Base.name;
            }

            if (!partyStats[i].Weapon)
            {
                partyMemberItems[i][13].text = "Weapon: None";
            }
            else
            {
                partyMemberItems[i][13].text = "Weapon: " + partyStats[i].Weapon.name + " (+" + partyStats[i].Weapon.WeaponAttack + ")";
            }
            if (!partyStats[i].Armor)
            {
                partyMemberItems[i][14].text = "Armor: None";
            }
            else
            {
                partyMemberItems[i][14].text = "Armor: " + partyStats[i].Armor.name + " (+" + partyStats[i].Armor.ArmorDefense + ")";
            }
        }
    }
    void PartyLayer2()
    {
        if (!gotInfo)
        {
            selectionMenu = selectionMenu.transform.GetChild(selectionMenu.transform.childCount - 1).gameObject;

            menuItems.Clear();
            for(int i = 0; i < selectionMenu.transform.childCount; ++i)
            {
                menuItems.Add(selectionMenu.transform.GetChild(i).GetComponent<TextMeshProUGUI>());
            }

            initPower = playerStats.Attack;
            initDefense = playerStats.Defense;
            initSpeed = playerStats.Speed;

            newPower = initPower;
            newDefense = initDefense;
            newSpeed = initSpeed;

            if (points > 0)
                selectedItem = 3;
            else
                selectedItem = 8;

            gotInfo = true;
        }

        mcItems[4].text = "Power: " + newPower;
        mcItems[5].text = "Defense: " + newDefense;
        mcItems[6].text = "Speed: " + newSpeed;

        mcItems[15].text = "Points Available: " + points;

        if (points > 0)
        {
            menuItems[3].gameObject.SetActive(true);
            menuItems[4].gameObject.SetActive(true);
            menuItems[5].gameObject.SetActive(true);
        }
        else
        {
            menuItems[3].gameObject.SetActive(false);
            menuItems[4].gameObject.SetActive(false);
            menuItems[5].gameObject.SetActive(false);
        }


        if(allocated > 0)
        {
            menuItems[0].gameObject.SetActive(true);
            menuItems[1].gameObject.SetActive(true);
            menuItems[2].gameObject.SetActive(true);
            menuItems[6].gameObject.SetActive(true);
            menuItems[7].gameObject.SetActive(true);

            mcItems[16].text = "Used: " + allocated;
        }
        else
        {
            menuItems[0].gameObject.SetActive(false);
            menuItems[1].gameObject.SetActive(false);
            menuItems[2].gameObject.SetActive(false);
            menuItems[6].gameObject.SetActive(false);
            menuItems[7].gameObject.SetActive(false);

            mcItems[16].text = "";
        }

    }

    void InventoryMenu()
    {
        switch (layer)
        {
            case 1:
                InventoryLayer1();
                break;
            case 2:
                if (menuNum == 1)
                    ItemsInventory();
                else
                    EquipmentInventory();
                break;
            case 3:
                InventoryConfirm();
                break;
        }
    }

    void InventoryLayer1()
    {
        if (!gotInfo)
        {
            baseMenu = canvas.transform.GetChild(3).gameObject;
            menu = baseMenu;

            selectionMenu = menu.transform.GetChild(0).gameObject;
            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            downArrow = baseMenu.transform.GetChild(3).gameObject;
            upArrow = baseMenu.transform.GetChild(4).gameObject;
            interactBox = baseMenu.transform.GetChild(5).gameObject;
            noItems = baseMenu.transform.GetChild(6).gameObject;

            maxLayer = 3;

            gotInfo = true;
        }

        noItems.SetActive(false);
        downArrow.SetActive(false); 
        upArrow.SetActive(false);
    }

    void ItemsInventory()
    {
        if(!gotInfo)
        {
            menu.SetActive(true);                   
            menu = baseMenu.transform.GetChild(1).gameObject;   
            selectionMenu = menu.transform.GetChild(0).gameObject;

            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            gotInfo = true;
        }

        invenCount = playerStats.Inventory.Count;
        invenLevels = invenCount / 10;

        if(invenCount == 0)
        {
            noItems.SetActive(true);
        }
        else
        {
            noItems.SetActive(false);
        }

        if (invenCount % 10 == 0)
        {
            fullLevel = true;
        }

        if (invenCount > 10)
            onScreenCount = 10;
        else
            onScreenCount = invenCount;

        for (int i = 0; i < onScreenCount; ++i)
        {
            selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            menuItems[i].text = playerStats.Inventory[i].name;
        }

        if(invenCount <= 10)
        {
            for (int i = invenCount; i < menuItems.Count; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        menu.SetActive(true);
    }

    void EquipmentInventory()
    {
        if (!gotInfo)
        {
            menu.SetActive(true);
            menu = baseMenu.transform.GetChild(2).gameObject;
            selectionMenu = menu.transform.GetChild(0).gameObject;

            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            gotInfo = true;
        }

        invenCount = playerStats.Equipment.Count;
        invenLevels = invenCount / 10;

        if (invenCount == 0)
        {
            noItems.SetActive(true);
        }
        else
        {
            noItems.SetActive(false);
        }

        if (invenCount % 10 == 0)
        {
            fullLevel = true;
        }

        if (invenCount > 10)
            onScreenCount = 10;
        else
            onScreenCount = invenCount;

        for (int i = 0; i < onScreenCount; ++i)
        {
            selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            menuItems[i].text = playerStats.Equipment[i].name;
        }

        if (invenCount <= 10)
        {
            for (int i = invenCount; i < menuItems.Count; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        menu.SetActive(true);
    }

    void InventoryConfirm()
    {
        if (!gotInfo)
        {
            GameObject info;
            List<TextMeshProUGUI> infoItems;

            interactBox.gameObject.SetActive(true);
            selectionMenu = interactBox.transform.GetChild(0).gameObject;
            info = interactBox.transform.GetChild(1).gameObject;

            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            infoItems = info.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            if (menuNum == 1)
            {
                ItemBase itemBase = (ItemBase)item;
                infoItems[0].text = itemBase.name;
                infoItems[1].text = "Restores " + itemBase.Effect + " " + itemBase.Stat;
                infoItems[2].text = itemBase.Description;
                menuItems[0].text = "Use on: " + curMemberName;
                menuItems[1].text = "Use";
            }
            else if (menuNum == 2)
            {
                infoItems[0].text = item.name;
                infoItems[2].text = item.Description;
                if(item is Weapon)
                {
                    Weapon weapon = (Weapon)item;
                    infoItems[1].text = "Provides +" + weapon.WeaponAttack + " more attack";
                }
                else if(item is Armor)
                {
                    Armor armor = (Armor)item;
                    infoItems[1].text = "Provides +" + armor.ArmorDefense + " more defense";
                }
                menuItems[0].text = "Equip on: " + curMemberName;
                menuItems[1].text = "Equip";
            }

            gotInfo = true;
        }
    }

    void MovesMenu()
    {
        switch (layer)
        {
            case 1:
                MovesLayer1();
                break;
            case 2:
                MovesLayer2();
                break;
            case 3:
                MovesLayer3();
                break;
            case 4:
                MovesLayer4(); 
                break;
            case 5:
                MovesLayer5();
                break;
            case 6:
                MovesLayer6();
                break;
            case 7:
                MovesLayer7();
                break;

        }
    }

    void MovesLayer1()
    {
        if (!gotInfo)
        {
            baseMenu = canvas.transform.GetChild(4).gameObject;
            menu = baseMenu;

            selectionMenu = menu.transform.GetChild(0).gameObject;

            interactBox = baseMenu.transform.GetChild(2).gameObject;
            downArrow = baseMenu.transform.GetChild(3).gameObject;
            upArrow = baseMenu.transform.GetChild(4).gameObject;

            maxLayer = 7;

            for (int i = 0; i < 3; ++i)
            {
                selectionMenu.transform.GetChild(i + 1).gameObject.SetActive(true);
            }

            for (int i = partyStats.Count; i < 3; ++i)
            {
                selectionMenu.transform.GetChild(i + 1).gameObject.SetActive(false);
            }

            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            gotInfo = true;
        }

        downArrow.SetActive(false);
        upArrow.SetActive(false);
    }

    void MovesLayer2()
    {
        int learned;

        if (memberSelected == 0)
            charStats = playerStats;
        else
            charStats = partyStats[memberSelected - 1];

        if (charStats.LearnableMoves.Count > 4)
        {
            learned = 4;
            downArrow.SetActive(true);
        }
        else
            learned = charStats.LearnableMoves.Count;

        if (!gotInfo)
        {
            menu.SetActive(true);
            menu = baseMenu.transform.GetChild(1).gameObject;

            selectionMenu = menu.transform.GetChild(0).gameObject;
            otherMoves = menu.transform.GetChild(1).gameObject;

            for (int i = 0; i < 4; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
                otherMoves.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = charStats.MoveSet.Count; i < 4; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(false);
            }

            for(int i = learned; i < 4; ++i)
            {
                otherMoves.transform.GetChild(i).gameObject.SetActive(false);
            }

            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            menuItems2 = otherMoves.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            gotInfo = true;
        }

        for(int i = 0; i < charStats.MoveSet.Count; ++i)
        {
            menuItems[i].text = charStats.MoveSet[i].Base.name;
        }
        for(int i = 0; i < learned; ++i)
        {
            menuItems2[i].text = charStats.LearnableMoves[i].Base.name;
        }
    }

    void MovesLayer3()
    {
        if (!gotInfo)
        {
            GameObject info;
            List<TextMeshProUGUI> infoItems;

            downArrow.gameObject.SetActive(false);
            upArrow.gameObject.SetActive(false);
            interactBox.gameObject.SetActive(true);
            selectionMenu = interactBox.transform.GetChild(0).gameObject;
            info = interactBox.transform.GetChild(1).gameObject;

            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            infoItems = info.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            infoItems[0].text = prevMove.Base.name;
            infoItems[1].text = prevMove.Base.Type.ToString();
            infoItems[2].text = "Power: " + prevMove.Base.Power + " / Mana Cost: " + prevMove.Base.ManaCost;
            infoItems[3].text = prevMove.Base.Description;

            menuItems[0].text = "Choose";

            gotInfo = true;
        }
    }

    void MovesLayer4()
    {
        if (!gotInfo)
        {
            menu.SetActive(true);

            selectionMenu = otherMoves;

            for (int i = 0; i < 4; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
            }

            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            invenCount = charStats.LearnableMoves.Count;
            invenLevels = invenCount / 4;

            if (invenCount % 4 == 0)
            {
                fullLevel = true;
            }

            if (invenCount > 4)
                onScreenCount = 4;
            else
                onScreenCount = invenCount;


            for (int i = 0; i < onScreenCount; ++i)
            {
                menuItems[i].text = charStats.LearnableMoves[i].Base.name;
            }

            if (invenCount <= 4)
            {
                for (int i = invenCount; i < menuItems.Count; ++i)
                {
                    selectionMenu.transform.GetChild(i).gameObject.SetActive(false);
                }
            }

            gotInfo = true;
        }
    }

    void MovesLayer5()
    {
        if (!gotInfo)
        {
            GameObject info;
            List<TextMeshProUGUI> infoItems;

            downArrow.gameObject.SetActive(false);
            upArrow.gameObject.SetActive(false);
            interactBox.gameObject.SetActive(true);
            selectionMenu = interactBox.transform.GetChild(0).gameObject;
            info = interactBox.transform.GetChild(1).gameObject;

            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            infoItems = info.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            infoItems[0].text = curMove.Base.name;
            infoItems[1].text = curMove.Base.Type.ToString();
            infoItems[2].text = "Power: " + curMove.Base.Power + " / Mana Cost: " + curMove.Base.ManaCost;
            infoItems[3].text = curMove.Base.Description;

            menuItems[0].text = "Replace";

            gotInfo = true;
        }
    }

    void MovesLayer6()
    {
        if (!gotInfo)
        {
            menu.SetActive(true);

            selectionMenu = otherMoves;

            for (int i = 0; i < 4; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
            }

            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            invenCount = charStats.LearnableMoves.Count;
            invenLevels = invenCount / 4;

            if (invenCount % 4 == 0)
            {
                fullLevel = true;
            }

            if (invenCount > 4)
                onScreenCount = 4;
            else
                onScreenCount = invenCount;


            for (int i = 0; i < onScreenCount; ++i)
            {
                menuItems[i].text = charStats.LearnableMoves[i].Base.name;
            }

            if (invenCount <= 4)
            {
                for (int i = invenCount; i < menuItems.Count; ++i)
                {
                    selectionMenu.transform.GetChild(i).gameObject.SetActive(false);
                }
            }

            gotInfo = true;
        }
    }

    void MovesLayer7()
    {
        if (!gotInfo)
        {
            GameObject info;
            List<TextMeshProUGUI> infoItems;

            downArrow.gameObject.SetActive(false);
            upArrow.gameObject.SetActive(false);
            interactBox.gameObject.SetActive(true);
            selectionMenu = interactBox.transform.GetChild(0).gameObject;
            info = interactBox.transform.GetChild(1).gameObject;

            selectionMenu.transform.GetChild(1).gameObject.SetActive(false);

            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            infoItems = info.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            infoItems[0].text = curMove.Base.name;
            infoItems[1].text = curMove.Base.Type.ToString();
            infoItems[2].text = "Power: " + curMove.Base.Power + " / Mana Cost: " + curMove.Base.ManaCost;
            infoItems[3].text = curMove.Base.Description;

            menuItems[0].text = "Back";


            gotInfo = true;
        }
    }

    void MemoryMenu()
    {
        MemoryLayer1();
    }
    void MemoryLayer1()
    {
        if (!gotInfo)
        {
            baseMenu = canvas.transform.GetChild(5).gameObject;
            menu = baseMenu;

            selectionMenu = menu.transform.GetChild(0).gameObject;
            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            maxLayer = 1;

            gotInfo = true;
        }
    }

    void OptionMenu()
    {
        if (!gotInfo)
        {
            baseMenu = canvas.transform.GetChild(6).gameObject;
            menu = baseMenu;

            gotInfo = true;
        }
    }

    void SaveMenu()
    {
        baseMenu = canvas.transform.GetChild(7).gameObject;
        menu = baseMenu;

        List<TextMeshProUGUI> infoItems;
        GameObject info;

        info = menu.transform.GetChild(2).gameObject;
        infoItems = info.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        selectionMenu = menu.transform.GetChild(0).gameObject;
        menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();

        gotInfo = true;
        
        SaveSystem saveSystem = gameManager.GetComponent<SaveSystem>();
        SaveSystem.SaveData prevSaveData = saveSystem.PreviewSaveData();

        if (prevSaveData != null)
        {
            infoItems[0].text = "Player Name: " + prevSaveData.playerName;
            infoItems[1].text = "Level: " + prevSaveData.level;
            infoItems[2].text = "Money: $" + prevSaveData.money;
            infoItems[3].text = "Current Scene: " + prevSaveData.world;
        }
        else
        {
            infoItems[0].text = "There is no save data.";
        }
    }

    void LoadMenu()
    {
        baseMenu = canvas.transform.GetChild(8).gameObject;
        menu = baseMenu;

        List<TextMeshProUGUI> infoItems;
        GameObject info;

        info = menu.transform.GetChild(2).gameObject;
        infoItems = info.GetComponentsInChildren<TextMeshProUGUI>().ToList();

        selectionMenu = menu.transform.GetChild(0).gameObject;
        menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();


        gotInfo = true;

        SaveSystem saveSystem = gameManager.GetComponent<SaveSystem>();
        SaveSystem.SaveData prevSaveData = saveSystem.PreviewSaveData();

        if (prevSaveData != null)
        {
            infoItems[0].text = "Player Name: " + prevSaveData.playerName;
            infoItems[1].text = "Level: " + prevSaveData.level;
            infoItems[2].text = "Money: " + prevSaveData.money;
            infoItems[3].text = "Current Scene: " + prevSaveData.world;
        }
        else
        {
            infoItems[0].text = "There is no save data.";
        }
    }


    void BaseMenuSelect()
    {
        SelectHelper();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selectedItem == 7)
            {
                Application.Quit();
            }
            else if(curMenu == Menu.Base)
            {
                curMenu = (Menu)selectedItem;
                NextMenu();
            }                       
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(layer == 0)
                CloseMenu();
            else
                BackMenu();
        }
    }

    void PartyMenuSelect()
    {
        switch (layer)
        {
            case 1:
                PartySelect1();
                break;
            case 2:
                PartySelect2();
                break;
        }
    }

    void PartySelect1()
    {
        SelectHelper();
        int cur = 0;

        for (int i = 0; i < menuItems.Count; ++i)            //shows the current party member's stats when hovered over
        {
            if (i != selectedItem)
            {
                partyMemberScreen[i].SetActive(false);
            }
            else
            {
                partyMemberScreen[i].SetActive(true);
                cur = i;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(selectedItem == 0 && playerStats.Points > 0)
            {
                selectionMenu = partyMemberScreen[cur];
                NextMenu();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackMenu();
        }
    }
    void PartySelect2()
    {
        if (selectedItem >= 0 && selectedItem <= 2)      //left power, def, speed
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))         //move around menu
            {Audio.PlayOneShot(soundClip2);
                if (selectedItem == 2)
                {
                    selectedItem = 6;
                }
                else
                {
                    ++selectedItem;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {Audio.PlayOneShot(soundClip2);
                if(selectedItem == 1 || selectedItem == 2) 
                {
                    --selectedItem;
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {Audio.PlayOneShot(soundClip2);
                if(points > 0)
                    selectedItem += 3;
            }
        }
        else if (selectedItem >= 3 && selectedItem <= 5)      //left power, def, speed
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))         //move around menu
            {
                if (selectedItem == 5)
                {
                    if(allocated > 0)
                        selectedItem = 6;
                }
                else
                {
                    ++selectedItem;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {Audio.PlayOneShot(soundClip2);
                if (selectedItem == 4 || selectedItem == 5)
                {
                    --selectedItem;
                }
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {Audio.PlayOneShot(soundClip2);
                if(allocated > 0)
                    selectedItem -= 3;
            }
        }
        else if(selectedItem == 6 || selectedItem == 7)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {Audio.PlayOneShot(soundClip2);
                selectedItem = 2;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && selectedItem == 7)
            {Audio.PlayOneShot(soundClip2);
                selectedItem = 6;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && selectedItem == 6)
            {Audio.PlayOneShot(soundClip2);
                selectedItem = 7;
            }
        }        

        UpdateItemSelection();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            PointInteraction();
            PartyLayer2();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuItems[0].gameObject.SetActive(false);
            menuItems[1].gameObject.SetActive(false);
            menuItems[2].gameObject.SetActive(false);
            menuItems[6].gameObject.SetActive(false);
            menuItems[7].gameObject.SetActive(false);

            menuItems[selectedItem].color = Color.black;
            BackMenu();
        }
    }

    void InventoryMenuSelect()
    {
        switch (layer)
        {
            case 1:
                InventorySelect1();
                break;
            case 2:
                InventorySelect2();
                break;
            case 3:
                InventoryConfirmSelect();
                break;
            case 4:
                CharSelect();
                break;
        }
    }

    void InventorySelect1()
    {
        SelectHelper();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            menuNum = selectedItem + 1;
            NextMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackMenu();
        }
    }

    void InventorySelect2()
    {
        ArrowHelper();

        int prevSelection = selectedItem;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))         //move around menu
        {
            if (selectedItem > 7 && curLevel < invenLevels)
            {
                if (fullLevel && curLevel == invenLevels - 1)
                {
                    //do nothing because i'm too stupid to do discrete math
                }
                else
                    DownLevel();
            }
            else if (selectedItem + 2 < onScreenCount)
            {
                selectedItem += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {Audio.PlayOneShot(soundClip2);
            if (selectedItem < 2 && curLevel > 0)
            {
                UpLevel();
            }
            else if (selectedItem - 1 > 0) 
            {
                selectedItem -= 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {Audio.PlayOneShot(soundClip2);
            if(selectedItem > 0 && selectedItem % 2 != 0)
                --selectedItem;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {Audio.PlayOneShot(soundClip2);
            if (selectedItem + 1 < onScreenCount && selectedItem % 2 != 1)
                ++selectedItem;
        }

        if (prevSelection != selectedItem)                                              //updates which menu item is highlighted
        {
            selectedItem = Mathf.Clamp(selectedItem, 0, menuItems.Count - 1);
            UpdateItemSelection();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(menuNum == 1)
            {
                item = playerStats.Inventory[selectedItem + curLevel * 10];
            }
            else if(menuNum == 2)
            {
                item = playerStats.Equipment[selectedItem + curLevel * 10];
            }
            itemIndex = selectedItem + curLevel* 10;
            NextMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            curLevel = 0;
            fullLevel= false;
            for (int i = 0; i < menuItems.Count; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
            }
            BackMenu();
        }
    }

    void InventoryConfirmSelect()
    {
        SelectHelper();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(selectedItem == 0)
            {
                layer = 4;
                menuItems[0].color = Color.red;
            }
            else if(selectedItem == 1)
            {
                if(menuNum == 1)
                {
                    UseItem();
                }
                else if(menuNum == 2)
                {
                    EquipItem();
                }
                BackMenu();
                interactBox.SetActive(false);
                memberSelected = 0;
                curMemberName = playerStats.Name;
            }
            else if(selectedItem == 2)
            {
                BackMenu();
                interactBox.SetActive(false);
                memberSelected = 0;
                curMemberName = playerStats.Name;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackMenu();
            interactBox.SetActive(false);
            memberSelected = 0;
            curMemberName = playerStats.Name;
        }
    }

    private void CharSelect()
    {
        string prevMemberName = curMemberName;

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))         //move around menu
        {Audio.PlayOneShot(soundClip2);
            --memberSelected;
        }
        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {Audio.PlayOneShot(soundClip2);
            ++memberSelected;
        }

        if(memberSelected == -1)
        {
            memberSelected = partyStats.Count;
        }
        if(memberSelected == partyStats.Count + 1)
        {
            memberSelected = 0;
        }

        switch (memberSelected)
        {
            case 0:
                curMemberName = playerStats.Name;
                break;
            case 1:
                curMemberName = partyStats[0].Name;
                break;
            case 2:
                curMemberName = partyStats[1].Name;
                break;
            case 3:
                curMemberName = partyStats[2].Name;
                break;
        }

        if (prevMemberName != curMemberName)                                              //updates which menu item is highlighted
        {
            if (menuNum == 1)
            {
                menuItems[0].text = "Use on: " + curMemberName;
            }
            else if (menuNum == 2)
            {
                menuItems[0].text = "Equip on: " + curMemberName;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            BackMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackMenu();
        }
    }

    void MovesMenuSelect()
    {
        switch (layer)
        {
            case 1:
                MovesSelect1();
                break;
            case 2:
                MovesSelect2();
                break;
            case 3:
                MovesSelect3();
                break;
            case 4:
                MovesSelect4();
                break;
            case 5:
                MovesSelect5();
                break;
            case 6:
                MovesSelect6();
                break;
            case 7:
                MovesSelect7();
                break;

        }
    }
    void MovesSelect1()
    {
        SelectHelper();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            memberSelected = selectedItem; 
            NextMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackMenu();
        }
    }

    void MovesSelect2()
    {
        SelectHelper();

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            layer = 5;
            menuItems[selectedItem].color = Color.black;
            itemIndex = selectedItem;
            NextMenu();

            selectedItem = itemIndex;
            int maxMenu = menuItems.Count;
            selectedItem = Mathf.Clamp(selectedItem, 0, maxMenu-1);
            UpdateItemSelection();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            prevMove = charStats.MoveSet[selectedItem];
            menuItems[selectedItem].color = Color.red;
            itemIndex = selectedItem;
            NextMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackMenu();
        }
    }

    void MovesSelect3()
    {
        SelectHelper();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(selectedItem == 0)
            {
                if(charStats.LearnableMoves.Count > 0)
                {
                    NextMenu();
                    interactBox.SetActive(false);
                }
                else
                {
                    menuItems[selectedItem].color = Color.red;
                }
                
            }
            else if(selectedItem == 1)
            {
                itemIndex = 0;
                interactBox.SetActive(false);
                BackMenu();
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            itemIndex = 0;
            interactBox.SetActive(false);
            BackMenu();
        }
    }

    void MovesSelect4()
    {
        LearnedMoveSelection();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            curMove = charStats.LearnableMoves[selectedItem];
            itemIndex2 = selectedItem;
            menuItems[selectedItem].color = Color.black;
            NextMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            curLevel = 0;
            fullLevel = false;
            ArrowHelper();
            layer = 3;
            menuItems[selectedItem].color = Color.black;
            BackMenu();
            selectedItem = itemIndex;
            UpdateItemSelection();
        }
    }

    void MovesSelect5()
    {
        SelectHelper();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selectedItem == 0)
            {
                ReplaceMove();
                layer = 3;
                BackMenu();
            }
            else if (selectedItem == 1)
            {
                itemIndex = 0;
                BackMenu();
                selectedItem = itemIndex2;
                UpdateItemSelection();
            }
            interactBox.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            itemIndex = 0;
            interactBox.SetActive(false);
            BackMenu();
            selectedItem = itemIndex2;
            UpdateItemSelection();
        }
    }

    void MovesSelect6()
    {
        LearnedMoveSelection();

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            curLevel = 0;
            fullLevel = false;
            ArrowHelper();
            layer = 3;
            menuItems[selectedItem].color = Color.black;
            itemIndex = selectedItem;
            BackMenu();

            selectedItem = itemIndex;
            int maxMenu = menuItems.Count;
            selectedItem = Mathf.Clamp(selectedItem, 0, maxMenu-1);
            UpdateItemSelection();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            menuItems[selectedItem].color = Color.black;
            curMove = charStats.LearnableMoves[selectedItem];
            NextMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            curLevel = 0;
            fullLevel = false;
            ArrowHelper();
            layer = 3;
            menuItems[selectedItem].color = Color.black;
            BackMenu();
            selectedItem = itemIndex;
            UpdateItemSelection();
        }
    }

    void MovesSelect7()
    {
        UpdateItemSelection();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            selectionMenu.transform.GetChild(1).gameObject.SetActive(true);
            interactBox.SetActive(false);
            BackMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            selectionMenu.transform.GetChild(1).gameObject.SetActive(true);
            interactBox.SetActive(false);
            BackMenu();
        }
    }

    void MemoryMenuSelect()
    {
        switch (layer)
        {
            case 1:
                MemorySelect1();
                break;
        }
    }

    void MemorySelect1()
    {
        SelectHelper();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selectedItem == 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if(gameManager.World == 0)
                {
                    playerStats.PositionWorld1.initialValue.x = player.transform.position.x;
                    playerStats.PositionWorld1.initialValue.y = player.transform.position.y;
                    playerStats.SceneWorld1 = SceneManager.GetActiveScene().name;
                    
                }
                else if(gameManager.World == 1)
                {
                    playerStats.PositionWorld2.initialValue.x = player.transform.position.x;
                    playerStats.PositionWorld2.initialValue.y = player.transform.position.y;
                    playerStats.SceneWorld2 = SceneManager.GetActiveScene().name;
                }
                playerStats.Position.initialValue.x = 0.5f;
                playerStats.Position.initialValue.y = 3f;
                playerStats.CurScene = "HubWorld";
                gameManager.clearPlayerInput();
                BackMenu();
                CloseMenu();

                gameManager.World =-1; 
                SceneManager.LoadScene("HubWorld");
            }
            else if(selectedItem == 1)
            {
                BackMenu();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackMenu();
        }
    }

    void OptionMenuSelect()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            BackMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackMenu();
        }
    }

    void SaveMenuSelect()
    {
        SelectHelper();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameManager.GetComponent<SaveSystem>().SaveGame();
            BackMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackMenu();
        }
    }

    void LoadMenuSelect()
    {
        SelectHelper();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SaveSystem.SaveData prevSaveData = gameManager.GetComponent<SaveSystem>().PreviewSaveData();
            if(prevSaveData != null )
            {
                BackMenu();
                CloseMenu();
                gameManager.GetComponent<SaveSystem>().LoadGame();
            }
            else
            {
                menuItems[0].color = Color.red;
                menuItems[0].text = "Stop";
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackMenu();
        }
    }

    void UseItem()
    {
        ItemBase itemBase = (ItemBase)item;

        if (memberSelected == 0)
        {
            if (itemBase.Stat == ItemBase.AffectedStat.HP)
            {
                playerStats.CurHP += itemBase.Effect;
            }
            else if (itemBase.Stat == ItemBase.AffectedStat.MP)
            {
                playerStats.CurMP += itemBase.Effect;
            }
        }
        else
        {
            if (itemBase.Stat == ItemBase.AffectedStat.HP)
            {
                partyStats[memberSelected - 1].CurHP += itemBase.Effect;
            }
            else if (itemBase.Stat == ItemBase.AffectedStat.MP)
            {
                partyStats[memberSelected - 1].CurMP += itemBase.Effect;
            }
        }

        playerStats.Inventory.RemoveAt(itemIndex);
    }

    void ReplaceMove()
    {
        charStats.MoveSet.RemoveAt(itemIndex);
        charStats.LearnableMoves.RemoveAt(itemIndex2);
        charStats.MoveSet.Insert(itemIndex, curMove);
        charStats.LearnableMoves.Insert(itemIndex2, prevMove);
    }

    void EquipItem()
    {
        Equipment prevEquip;

        if(item is Weapon)
        {
            if (memberSelected == 0)
            {
                if (!playerStats.Weapon)
                {
                    playerStats.Weapon = (Weapon)item;
                    playerStats.Equipment.RemoveAt(itemIndex);
                }
                else
                {
                    prevEquip = playerStats.Weapon;
                    playerStats.Equipment.Add(prevEquip);
                    playerStats.Weapon = (Weapon)item;
                    playerStats.Equipment.RemoveAt(itemIndex);
                }
            }
            else
            {
                if (!partyStats[memberSelected - 1].Weapon)
                {
                    partyStats[memberSelected - 1].Weapon = (Weapon)item;
                    playerStats.Equipment.RemoveAt(itemIndex);
                }
                else
                {
                    prevEquip = partyStats[memberSelected - 1].Weapon;
                    playerStats.Equipment.Add(prevEquip);
                    partyStats[memberSelected - 1].Weapon = (Weapon)item;
                    playerStats.Equipment.RemoveAt(itemIndex);
                }
            }
        }
        else if(item is Armor)
        {
            if (memberSelected == 0)
            {
                if (!playerStats.Armor)
                {
                    playerStats.Armor = (Armor)item;
                    playerStats.Equipment.RemoveAt(itemIndex);
                }
                else
                {
                    prevEquip = playerStats.Armor;
                    playerStats.Equipment.Add(prevEquip);
                    playerStats.Armor = (Armor)item;
                    playerStats.Equipment.RemoveAt(itemIndex);
                }
            }
            else
            {
                if (!partyStats[memberSelected - 1].Armor)
                {
                    partyStats[memberSelected - 1].Armor = (Armor)item;
                    playerStats.Equipment.RemoveAt(itemIndex);
                }
                else
                {
                    prevEquip = partyStats[memberSelected - 1].Armor;
                    playerStats.Equipment.Add(prevEquip);
                    partyStats[memberSelected - 1].Armor = (Armor)item;
                    playerStats.Equipment.RemoveAt(itemIndex);
                }
            }
        }
    }

    private void LearnedMoveSelection()
    {
        ArrowHelper();
        UpdateItemSelection();
        int prevSelection = selectedItem;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))         //move around menu
        {
            if (selectedItem == 3 && curLevel < invenLevels)
            {
                if (fullLevel && curLevel == invenLevels - 1)
                {
                    //do nothing because i'm too stupid to do discrete math
                }
                else
                    DownMoveLevel();
            }
            else if (selectedItem + 1 < onScreenCount)
            {
                ++selectedItem;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {Audio.PlayOneShot(soundClip2);
            if (selectedItem == 0 && curLevel > 0)
            {
                UpMoveLevel();
            }
            else if (selectedItem > 0)
            {
                --selectedItem;
            }
        }

        if (prevSelection != selectedItem)                                              //updates which menu item is highlighted
        {
            selectedItem = Mathf.Clamp(selectedItem, 0, menuItems.Count - 1);
            UpdateItemSelection();
        }
    }

    void ArrowHelper()
    {
        if (curLevel < invenLevels)
        {Audio.PlayOneShot(soundClip2);
            downArrow.SetActive(true);
        }
        else
        {
            downArrow.SetActive(false);
        }

        if(curLevel > 0)
        {Audio.PlayOneShot(soundClip2);
            upArrow.SetActive(true);
        }
        else
        {
            upArrow.SetActive(false);
        }

        if (fullLevel && curLevel == invenLevels - 1)
        {
            downArrow.SetActive(false);
        }
    }

    void DownMoveLevel()
    {
        ++curLevel;
        selectedItem -= 3;

        int below = invenCount - curLevel * 4;

        if (below >= 4)
            onScreenCount = 4;
        else
            onScreenCount = invenCount % 4;

        for (int i = 0; i < onScreenCount; ++i)
        {
            menuItems[i].text = charStats.LearnableMoves[i + (curLevel * 4)].Base.name;
        }

        if (below <= 8)
        {
            for (int i = onScreenCount; i < menuItems.Count; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (onScreenCount == 1)
            selectedItem = 0;
    }

    void UpMoveLevel()
    {
        --curLevel;
        selectedItem += 3;
        onScreenCount = 4;

        for (int i = 0; i < onScreenCount; ++i)
        {
            menuItems[i].text = charStats.LearnableMoves[i + (curLevel * 4)].Base.name;
        }

        for (int i = 0; i < menuItems.Count; ++i)
        {
            selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    void DownLevel()
    {
        ++curLevel;
        selectedItem -= 8;

        int below = invenCount - curLevel * 10;

        if (below >= 10)
            onScreenCount = 10;
        else
            onScreenCount = invenCount % 10;


        for (int i = 0; i < onScreenCount; ++i)
        {
            if (menuNum == 1)
                menuItems[i].text = playerStats.Inventory[i + (curLevel * 10)].name;
            else
                menuItems[i].text = playerStats.Equipment[i + (curLevel * 10)].name;
        }

        if (below <= 10)
        {
            for (int i = onScreenCount; i < menuItems.Count; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (onScreenCount == 1)
            selectedItem = 0;
    }

    void UpLevel()
    {
        --curLevel;
        selectedItem += 8;
        onScreenCount = 10;

        for (int i = 0; i < onScreenCount; ++i)
        {
            if (menuNum == 1)
                menuItems[i].text = playerStats.Inventory[i + (curLevel * 10)].name;
            else
                menuItems[i].text = playerStats.Equipment[i + (curLevel * 10)].name;
        }

        for (int i = 0; i < menuItems.Count; ++i)
        {
            selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    void PointInteraction()
    {
        if (selectedItem >= 0 && selectedItem <= 2 && allocated > 0)
        {
            if (selectedItem == 0 && newPower > initPower)
            {
                --newPower;
                ++points;
                --allocated;
            }
            else if (selectedItem == 1 && newDefense > initDefense)
            {
                --newDefense;
                ++points;
                --allocated;
            }
            else if (selectedItem == 2 && newSpeed > initSpeed)
            {
                --newSpeed;
                ++points;
                --allocated;
            }

            if (allocated == 0)
                selectedItem += 3;
        }
        else if (selectedItem >= 3 && selectedItem <= 5 && points > 0)
        {
            if (selectedItem == 3)
            {
                ++newPower;
            }
            else if (selectedItem == 4)
            {
                ++newDefense;
            }
            else 
            {
                ++newSpeed;
            }

            --points;
            ++allocated;

            if (points == 0)
                selectedItem -= 3;
        }
        else if (selectedItem == 6 && allocated > 0)
        {
            playerStats.Attack = newPower;
            playerStats.Defense = newDefense;
            playerStats.Speed = newSpeed;


            playerStats.Points = points;
            allocated = 0;
            menuItems[selectedItem].color = Color.black;

            PartyLayer2();
            BackMenu();
        }
        else if (selectedItem == 7 && allocated > 0)
        {
            newPower = initPower;
            newDefense = initDefense;
            newSpeed = initSpeed;

            points += allocated;
            playerStats.Points = points;
            allocated = 0;
            menuItems[selectedItem].color = Color.black;

            PartyLayer2();
            BackMenu();
        }
    }

    void SelectHelper()
    {
        int prevSelection = selectedItem;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))         //move around menu
        {Audio.PlayOneShot(soundClip2);
            ++selectedItem;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {Audio.PlayOneShot(soundClip2);
            --selectedItem;
        }

        if (selectedItem == -1)                                                          //rotates through if at bottom/top
        {
            selectedItem = menuItems.Count - 1;
        }
        else if (selectedItem == menuItems.Count)
        {
            selectedItem = 0;
        }

        if (prevSelection != selectedItem)                                              //updates which menu item is highlighted
        {
            selectedItem = Mathf.Clamp(selectedItem, 0, menuItems.Count - 1);
            UpdateItemSelection();
        }
    }

    void UpdateItemSelection()      //highlights whatever is currently selected
    {
        for(int i = 0; i < menuItems.Count; i++)
        {
            if (i != selectedItem)
            {
                menuItems[i].color = Color.black;
            }
            else
            {
                menuItems[i].color = Color.blue;
            }
        }
    }

    void UpdateMemorySelection()
    {
        for(int i = 0; i < menuItems.Count; ++i)
        {
            if(i != selectedItem)
            {
                menuItems[i].color = Color.black;
            }
            else
            {
                menuItems[i].color = Color.blue;
                if (selectedItem == 0 && !curMem.IsViewed)
                {
                    menuItems[i].color = Color.red;
                }
            }
        }
    }
}
