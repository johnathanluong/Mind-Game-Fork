using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

/*

public class MenuControllerTest : MonoBehaviour
{
    private static MenuController _instance;
    public event Action<int> onMenuSelected;
    public event Action onBack;

    [SerializeField] GameObject canvas;
    private GameObject menu;
    private GameObject selectionMenu;
    private GameObject mcStats;
    private GameObject memberStats;


    List<TextMeshProUGUI> menuItems;
    List<TextMeshProUGUI> mcItems;
    List<TextMeshProUGUI> memberItems;

    [SerializeField] PlayerStats playerStats;
    [SerializeField] List<PartyStats> partyStats;

    int selectedItem = 0;
    int currentMember = 0;
    bool menuOpened = false;
    Menu curMenu = Menu.Base;

    public enum Menu
    {
        Base,
        Party,
        Inventory,
        Memories,
        Options,
        Save,
        Load
    }

    public static MenuController GetInstance()
    {
        if (_instance == null)
        {
            Debug.LogError("MenuController is null");
        }
        return _instance;
    }
    public bool MenuOpened
    {
        get { return menuOpened; }
        set { menuOpened = value; }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("More than one MenuController");
        }
        _instance = this;

        menu = canvas.transform.GetChild(1).gameObject;

        selectionMenu = menu.transform.GetChild(0).gameObject;
        mcStats = menu.transform.GetChild(1).gameObject;
        memberStats = menu.transform.GetChild(2).gameObject;

        menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        mcItems = mcStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        memberItems = memberStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();
    }

    public void Update()
    {
        SelectMenu();
    }

    public void OpenMenu()
    {
        menuOpened = true;
        menu.SetActive(true);
        UpdateItemSelection(menuItems);
    }
    public void CloseMenu()
    {
        menuOpened = false;
        menu.SetActive(false);
    }
    void SelectMenu()
    {
        if (menuOpened)
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
                    break;
                case Menu.Memories:
                    break;
                case Menu.Options:
                    break;
                case Menu.Save:
                    break;
                case Menu.Load:
                    break;
            }
        }
    }
    void BaseMenu()
    {
        if (menuOpened && curMenu == Menu.Base)
        {
            menu = canvas.transform.GetChild(1).gameObject;

            selectionMenu = menu.transform.GetChild(0).gameObject;
            mcStats = menu.transform.GetChild(1).gameObject;
            memberStats = menu.transform.GetChild(2).gameObject;

            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            mcItems = mcStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            memberItems = memberStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            mcItems[0].text = playerStats.Name;
            mcItems[1].text = "HP: " + playerStats.CurHP + "/" + playerStats.MaxHP;
            mcItems[2].text = "MP: " + playerStats.CurMP + "/" + playerStats.MaxMP;
            mcItems[3].text = "Level: " + playerStats.Level;

            memberItems[0].text = partyStats[currentMember].Name;
            memberItems[1].text = "HP: " + partyStats[currentMember].CurHP + "/" + partyStats[currentMember].MaxHP;
            memberItems[2].text = "MP: " + partyStats[currentMember].CurMP + "/" + partyStats[currentMember].MaxMP;
            memberItems[3].text = "Level: " + partyStats[currentMember].Level;

            MenuSelect(menuItems);
        }
    }
    public void PartyMenu()
    {
        Debug.Log("Party" + curMenu);
        if (menuOpened && curMenu == Menu.Party)
        {
            menu = canvas.transform.GetChild(2).gameObject;

            selectionMenu = menu.transform.GetChild(0).gameObject;
            mcStats = menu.transform.GetChild(1).gameObject;
            memberStats = menu.transform.GetChild(2).gameObject;

            menuItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            mcItems = mcStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            memberItems = memberStats.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            mcItems[0].text = playerStats.Name;
            mcItems[1].text = "HP: " + playerStats.CurHP + "/" + playerStats.MaxHP;
            mcItems[2].text = "MP: " + playerStats.CurMP + "/" + playerStats.MaxMP;
            mcItems[3].text = "Level: " + playerStats.Level;

            memberItems[0].text = partyStats[currentMember].Name;
            memberItems[1].text = "HP: " + partyStats[currentMember].CurHP + "/" + partyStats[currentMember].MaxHP;
            memberItems[2].text = "MP: " + partyStats[currentMember].CurMP + "/" + partyStats[currentMember].MaxMP;
            memberItems[3].text = "Level: " + partyStats[currentMember].Level;

            MenuSelect(menuItems);
        }
    }
    void MenuSelect(List<TextMeshProUGUI> menuItems)
    {
        int prevSelection = selectedItem;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))         //move around menu
        {
            ++selectedItem;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
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
            UpdateItemSelection(menuItems);
        }

        if (Input.GetKeyDown(KeyCode.Return))                                           //event based on either do something in menu or exit
        {
            onMenuSelected?.Invoke(selectedItem);
            curMenu = (Menu)selectedItem;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            onBack?.Invoke();
            CloseMenu();
        }
    }

    void UpdateItemSelection(List<TextMeshProUGUI> menuItems)
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            if (i == selectedItem)
            {
                menuItems[i].color = Color.blue;
            }
            else
            {
                menuItems[i].color = Color.black;
            }

        }
    }

}
*/