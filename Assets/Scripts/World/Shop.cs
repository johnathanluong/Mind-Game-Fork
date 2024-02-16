using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    private GameObject baseMenu;
    private GameObject menu;
    private GameObject selectionMenu;
    private GameObject itemPriceObj;
    private GameObject itemPropsObj;
    private TextMeshProUGUI shopText;
    private TextMeshProUGUI moneyText;

    private GameObject downArrow;
    private GameObject upArrow;

    private GameManager gameManager;

    [SerializeField] string shopLine1 = "Buy something damn it";
    [SerializeField] string shopLine2 = "What would you like to buy?";
    [SerializeField] string shopLine3 = "What are you pawning off?";
    [SerializeField] List<Buyable> buyables;

    List<TextMeshProUGUI> selectionItems;
    List<TextMeshProUGUI> prices;
    List<TextMeshProUGUI> itemProperties;

    PlayerStats playerStats;

    int selectedItem = 0;
    int prevSelectedItem = 0;
    int layer = 0;
    int itemCount;
    int onScreenCount;
    int quantity = 1;
    int maxQuantity = 0;
    int invenLevels;
    int curLevel;
    int menuNum;
    int sellIndex;

    bool shopOpened = false;
    bool fullLevel = false;

    Buyable item;

    private void Start()
    {
        gameManager = GameManager.Instance;
        playerStats = gameManager.PlayerStats;
        baseMenu = canvas.transform.GetChild(9).gameObject;
        selectionMenu = baseMenu.transform.GetChild(0).gameObject;
        selectionItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        shopText = baseMenu.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        moneyText = baseMenu.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        downArrow = baseMenu.transform.GetChild(2).gameObject.transform.GetChild(3).gameObject;
        upArrow = baseMenu.transform.GetChild(2).gameObject.transform.GetChild(4).gameObject;
    }

    private void Update()
    {
        if (shopOpened)
        {
            SelectorHelper();
        }
    }

    public void OpenShop()
    {
        gameManager.playerStopped = true;
        shopOpened = true;
        shopText.text = shopLine1;
        LayerHelper();
        menu.SetActive(true);
        UpdateItemSelection();
    }
    private void CloseShop()
    {
        gameManager.playerStopped = false;
        shopOpened = false;
        quantity = 1;
        menu.SetActive(false);
    }
    private void NextLayer()
    {
        ++layer;
        LayerHelper();
        selectedItem = 0;
        menu.SetActive(true);
        UpdateItemSelection();
    }
    private void BackLayer()
    {
        --layer;
        menu.SetActive(false);
        LayerHelper();
        selectedItem = 0;
        menu.SetActive(true);
        UpdateItemSelection();
        moneyText.text = "$" + playerStats.Money.ToString();
    }

    private void LayerHelper()
    {
        switch (layer)
        {
            case 0:
                Layer1();
                break;
            case 1:
                Layer2();
                break;
            case 2:
                Layer3();
                break;
            case 4:
                ConfirmLayer();
                break;
            case 5:
                SellLayer1();
                break;
            case 6:
                SellLayer2();
                break;
            case 7:
                SellLayer3();
                break;
            case 8:
                SellConfirmationLayer();
                break;
        }
    }
    private void Layer1()
    {
        menu = baseMenu;
        selectionMenu = menu.transform.GetChild(0).gameObject;
        selectionItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();

        moneyText.text = "$" + playerStats.Money.ToString();
        shopText.text = shopLine1;

        downArrow.SetActive(false);
        upArrow.SetActive(false);
    }
    private void Layer2()
    {
        menu = baseMenu.transform.GetChild(1).gameObject;
        selectionMenu = menu.transform.GetChild(0).gameObject;
        itemPriceObj = menu.transform.GetChild(1).gameObject;

        for(int i = 0; i < 8; ++i)
        {
            selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
            itemPriceObj.transform.GetChild(i).gameObject.SetActive(true);
        }

        selectionItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        prices = itemPriceObj.GetComponentsInChildren<TextMeshProUGUI>().ToList();

        shopText.text = shopLine2;

        itemCount = buyables.Count;
        invenLevels = itemCount / 8;
        
        if(itemCount % 8 == 0)
        {
            fullLevel = true;
        }

        if(curLevel > 0)
        {
            onScreenCount = itemCount - curLevel * 8;
        }
        else if (itemCount > 8)
        {
            onScreenCount = 8;
        }
        else
        {
            onScreenCount = itemCount;
        }

        for (int i = 0; i < onScreenCount; ++i)
        {
            selectionItems[i].text = buyables[i + curLevel * 8].name;
            prices[i].text = "$" + buyables[i + curLevel * 8].CostValue;
        }

        if (onScreenCount <= 8)
        {
            for (int i = onScreenCount; i < selectionItems.Count; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(false);
                itemPriceObj.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    
    private void Layer3()
    {
        menu = baseMenu.transform.GetChild(3).gameObject;
        selectionMenu = menu.transform.GetChild(0).gameObject;
        itemPropsObj = menu.transform.GetChild(1).gameObject;

        selectionItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        itemProperties = itemPropsObj.GetComponentsInChildren<TextMeshProUGUI>().ToList();

        itemProperties[0].text = item.name;
        itemProperties[1].text = "$" + item.CostValue;

        maxQuantity = playerStats.Money / item.CostValue;
        maxQuantity = Mathf.Clamp(maxQuantity, 1, 9);


        if (item is Weapon)
        {
            Weapon weapon = (Weapon)item;
            itemProperties[2].text = "Provides +" + weapon.WeaponAttack + " more attack";
        }
            
        else if(item is Armor)
        {
            Armor armor = (Armor)item;
            itemProperties[2].text = "Provides +" + armor.ArmorDefense + " more defense";
        }
        else if(item is ItemBase)
        {
            ItemBase itemBase = (ItemBase)item;
            itemProperties[2].text = "Restores " + itemBase.Effect + " " + itemBase.Stat;
        }

        selectionItems[0].text = "Quantity: " + quantity;
    }

    private void ConfirmLayer()
    {
        TextMeshProUGUI confirmText;
        menu.SetActive(true);

        menu = baseMenu.transform.GetChild(4).gameObject;
        selectionMenu = menu.transform.GetChild(0).gameObject;

        selectionItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        confirmText = menu.transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>();

        confirmText.text = quantity.ToString() +"X " + item.name + " for $" + (item.CostValue*quantity) + "?";
    }

    private void SellLayer1()
    {
        menu = baseMenu;
        selectionMenu = menu.transform.GetChild(0).gameObject;
        selectionItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();

        moneyText.text = "$" + playerStats.Money.ToString();
        shopText.text = shopLine3;

        downArrow.SetActive(false);
        upArrow.SetActive(false);

        selectionItems[0].text = "Items";
        selectionItems[1].text = "Equipment";
    }

    private void SellLayer2()
    {
        menu = baseMenu.transform.GetChild(1).gameObject;
        selectionMenu = menu.transform.GetChild(0).gameObject;
        itemPriceObj = menu.transform.GetChild(1).gameObject;

        for (int i = 0; i < 8; ++i)
        {
            selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
            itemPriceObj.transform.GetChild(i).gameObject.SetActive(true);
        }

        selectionItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        prices = itemPriceObj.GetComponentsInChildren<TextMeshProUGUI>().ToList();

        if(menuNum == 1)
        {
            itemCount = playerStats.Inventory.Count;
        }
        else if(menuNum == 2)
        {
            itemCount = playerStats.Equipment.Count;
        }
        invenLevels = itemCount / 8;

        if (itemCount % 8 == 0)
        {
            fullLevel = true;
        }

        if (curLevel > 0)
        {
            onScreenCount = itemCount - curLevel * 8;
        }
        else if (itemCount > 8)
        {
            onScreenCount = 8;
        }
        else
        {
            onScreenCount = itemCount;
        }


        for (int i = 0; i < onScreenCount; ++i)
        {
            if (menuNum == 1)
            {
                selectionItems[i].text = playerStats.Inventory[i].name;
                prices[i].text = "$" + playerStats.Inventory[i].SellValue;
            }
            else if (menuNum == 2)
            {
                selectionItems[i].text = playerStats.Equipment[i].name;
                prices[i].text = "$" + playerStats.Equipment[i].SellValue;
            }
        }

        if (onScreenCount <= 8)
        {
            for (int i = onScreenCount; i < selectionItems.Count; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(false);
                itemPriceObj.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    private void SellLayer3()
    {
        menu = baseMenu.transform.GetChild(5).gameObject;
        selectionMenu = menu.transform.GetChild(0).gameObject;
        itemPropsObj = menu.transform.GetChild(1).gameObject;

        selectionItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        itemProperties = itemPropsObj.GetComponentsInChildren<TextMeshProUGUI>().ToList();

        itemProperties[0].text = item.name;
        itemProperties[1].text = "$" + item.SellValue;

        if (item is Weapon)
        {
            Weapon weapon = (Weapon)item;
            itemProperties[2].text = "Provides +" + weapon.WeaponAttack + " more attack";
        }
        else if (item is Armor)
        {
            Armor armor = (Armor)item;
            itemProperties[2].text = "Provides +" + armor.ArmorDefense + " more defense";
        }
        else if (item is ItemBase)
        {
            ItemBase itemBase = (ItemBase)item;
            itemProperties[2].text = "Restores " + itemBase.Effect + " " + itemBase.Stat;
        }
    }

    private void SellConfirmationLayer()
    {
        TextMeshProUGUI confirmText;
        menu.SetActive(true);

        menu = baseMenu.transform.GetChild(6).gameObject;
        selectionMenu = menu.transform.GetChild(0).gameObject;

        selectionItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        confirmText = menu.transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>();

        confirmText.text = item.name + " for $" + (item.SellValue * quantity) + "?";
    }

    private void SelectorHelper()
    {
        switch (layer)
        {
            case 0:
                Selector1();
                break;
            case 1:
                Selector2();
                break;
            case 2:
                Selector3();
                break;
            case 3:
                QuantitySelect();
                break;
            case 4:
                ConfirmSelect();
                break;
            case 5:
                SellSelector1();
                break;
            case 6:
                SellSelector2();
                break;
            case 7:
                SellSelector3();
                break;
            case 8:
                SellConfirmSelect();
                break;
        }
    }

    private void Selector1()
    {
        Selection(selectionItems.Count);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selectedItem == 0)
            {
                NextLayer();
            }
            else if(selectedItem == 1)
            {
                layer = 4;
                NextLayer();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShop();
        }
    }
    private void Selector2()
    {
        InvenSelection();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (buyables[selectedItem + (curLevel * 8)].CostValue <= playerStats.Money)
            {
                item = buyables[selectedItem + (curLevel * 8)];
                prevSelectedItem = selectedItem;
                NextLayer();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            curLevel = 0;
            fullLevel = false;
            for (int i = 0; i < selectionItems.Count; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
                itemPriceObj.transform.GetChild(i).gameObject.SetActive(true);
            }
            BackLayer();
        }
    }
    private void Selector3()
    {
        Selection(selectionItems.Count);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(selectedItem == 0)
            {
                layer = 3;
                selectionItems[0].color = Color.red;
            }
            else if(selectedItem == 1)
            {
                layer = 3;
                NextLayer();
            }
            else if(selectedItem == 2)
            {
                BackLayer();
                selectedItem = prevSelectedItem;
                quantity = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackLayer();
            selectedItem = prevSelectedItem;
            quantity = 1;
        }
    }

    private void QuantitySelect()
    {
        int prevQuantity = quantity;

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && quantity > 1)         //move around menu
        {
            --quantity;
        }
        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && quantity < maxQuantity)
        {
            ++quantity;
        }

        if (prevQuantity != quantity)                                              //updates which menu item is highlighted
        {
            selectionItems[0].text = "Quantity: " + quantity;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            BackLayer();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackLayer();
        }
    }

    private void ConfirmSelect()
    {
        Selection(selectionItems.Count);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(selectedItem == 0)
            {
                Buy();
                layer = 3;
                BackLayer();
                BackLayer();
            }
            else if(selectedItem == 1)
            {
                layer = 3;
                BackLayer();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            layer = 3;
            BackLayer();
        }
    }

    private void SellSelector1()
    {
        Selection(selectionItems.Count);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            menuNum = selectedItem + 1;
            NextLayer();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            layer = 1;
            selectionItems[0].text = "Buy";
            selectionItems[1].text = "Sell";
            BackLayer();
        }
    }
    private void SellSelector2()
    {
        ArrowHelper();
        UpdateItemSelection();
        int prevSelection = selectedItem;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))         //move around menu
        {
            if (selectedItem == 7 && curLevel < invenLevels)
            {
                if (fullLevel && curLevel == invenLevels - 1)
                {
                    //do nothing because i'm too stupid to do discrete math
                }
                else
                    SellDownLevel();
            }
            else if (selectedItem + 1 < onScreenCount)
            {
                ++selectedItem;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectedItem == 0 && curLevel > 0)
            {
                SellUpLevel();
            }
            else if (selectedItem > 0)
            {
                --selectedItem;
            }
        }

        if (prevSelection != selectedItem)                                              //updates which menu item is highlighted
        {
            selectedItem = Mathf.Clamp(selectedItem, 0, selectionItems.Count - 1);
            UpdateItemSelection();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(menuNum == 1)
                item = playerStats.Inventory[selectedItem + (curLevel * 8)];
            else if(menuNum == 2)
                item = playerStats.Equipment[selectedItem + (curLevel * 8)];

            sellIndex = selectedItem + (curLevel * 8);
            prevSelectedItem = selectedItem;
            NextLayer();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            curLevel = 0;
            fullLevel = false;
            for (int i = 0; i < selectionItems.Count; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
                itemPriceObj.transform.GetChild(i).gameObject.SetActive(true);
            }
            BackLayer();
        }
    }

    private void SellSelector3()
    {
        Selection(selectionItems.Count);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selectedItem == 0)
            {
                NextLayer();
            }
            else if (selectedItem == 1)
            {
                BackLayer();
                selectedItem = prevSelectedItem;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackLayer();
            selectedItem = prevSelectedItem;
        }
    }

    private void SellConfirmSelect()
    {
        Selection(selectionItems.Count);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selectedItem == 0)
            {
                Sell();
                BackLayer();
                BackLayer();
            }
            else if (selectedItem == 1)
            {
                BackLayer();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackLayer();
        }
    }

    private void Buy()
    {
        for(int i = 0; i < quantity; ++i)
        {
            if (item is Weapon)
            {
                Weapon weapon = (Weapon)item;
                playerStats.Equipment.Add(weapon);
            }

            else if (item is Armor)
            {
                Armor armor = (Armor)item;
                playerStats.Equipment.Add(armor);
            }
            else if (item is ItemBase)
            {
                ItemBase itemBase = (ItemBase)item;
                playerStats.Inventory.Add(itemBase);
            }
            playerStats.Money -= item.CostValue;
        }
    }
    
    private void Sell()
    {
        if(item is Equipment)
        {
            Equipment equip = (Equipment)item;
            playerStats.Equipment.RemoveAt(sellIndex);
        }
        else if(item is ItemBase)
        {
            ItemBase itemBase = (ItemBase)item;
            playerStats.Inventory.RemoveAt(sellIndex);
        }

        playerStats.Money += item.SellValue;
        curLevel = 0;
        selectedItem = 0;
    }

    private void Selection(int limiter)
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

        if (prevSelection != selectedItem)                                              //updates which menu item is highlighted
        {
            selectedItem = Mathf.Clamp(selectedItem, 0, limiter - 1);
            UpdateItemSelection();
        }
    }

    private void InvenSelection()
    {
        BuyMenuUpdateItemSelection();
        int prevSelection = selectedItem;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))         //move around menu
        {
            if (selectedItem == 7 && curLevel < invenLevels)
            {
                if(fullLevel && curLevel == invenLevels - 1)
                {
                    //do nothing because i'm too stupid to do discrete math
                }
                else
                    DownLevel();
            }
            else if (selectedItem + 1 < onScreenCount)
            {
                ++selectedItem;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectedItem == 0 && curLevel > 0)
            {
                UpLevel();
            }
            else if (selectedItem > 0)
            {
                --selectedItem;
            }
        }

        if (prevSelection != selectedItem)                                              //updates which menu item is highlighted
        {
            selectedItem = Mathf.Clamp(selectedItem, 0, selectionItems.Count - 1);
            BuyMenuUpdateItemSelection();
        }
    }
    void DownLevel()
    {
        ++curLevel;
        selectedItem -= 7;

        int below = itemCount - curLevel * 8;

        if (below >= 8)
            onScreenCount = 8;
        else
            onScreenCount = itemCount % 8;



        for (int i = 0; i < onScreenCount; ++i)
        {
                selectionItems[i].text = buyables[i + (curLevel * 8)].name;
                prices[i].text = "$" + buyables[i + (curLevel * 8)].CostValue;
        }

        if (below <= 8)
        {
            for (int i = onScreenCount; i < selectionItems.Count; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(false);
                itemPriceObj.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (onScreenCount == 1)
            selectedItem = 0;
    }

    void UpLevel()
    {
        --curLevel;
        selectedItem += 7;
        onScreenCount = 8;

        for (int i = 0; i < onScreenCount; ++i)
        {
            selectionItems[i].text = buyables[i + (curLevel * 8)].name;
            prices[i].text = "$" + buyables[i + (curLevel * 8)].CostValue;
        }

        for (int i = 0; i < selectionItems.Count; ++i)
        {
            selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
            itemPriceObj.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    void SellDownLevel()
    {
        ++curLevel;
        selectedItem -= 7;

        int below = itemCount - curLevel * 8;

        if (below >= 8)
            onScreenCount = 8;
        else
            onScreenCount = itemCount % 8;

        if(menuNum == 1)
        {
            for (int i = 0; i < onScreenCount; ++i)
            {
                selectionItems[i].text = playerStats.Inventory[i + (curLevel * 8)].name;
                prices[i].text = "$" + playerStats.Inventory[i + (curLevel * 8)].SellValue;
            }

            if (below <= 8)
            {
                for (int i = onScreenCount; i < selectionItems.Count; ++i)
                {
                    selectionMenu.transform.GetChild(i).gameObject.SetActive(false);
                    itemPriceObj.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        else if(menuNum == 2)
        {
            for (int i = 0; i < onScreenCount; ++i)
            {
                selectionItems[i].text = playerStats.Equipment[i + (curLevel * 8)].name;
                prices[i].text = "$" + playerStats.Equipment[i + (curLevel * 8)].SellValue;
            }

            if (below <= 8)
            {
                for (int i = onScreenCount; i < selectionItems.Count; ++i)
                {
                    selectionMenu.transform.GetChild(i).gameObject.SetActive(false);
                    itemPriceObj.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        if (onScreenCount == 1)
            selectedItem = 0;
    }

    void SellUpLevel()
    {
        --curLevel;
        selectedItem += 7;
        onScreenCount = 8;

        if(menuNum == 1)
        {
            for (int i = 0; i < onScreenCount; ++i)
            {
                selectionItems[i].text = playerStats.Inventory[i + (curLevel * 8)].name;
                prices[i].text = "$" + playerStats.Inventory[i + (curLevel * 8)].SellValue;
            }

            for (int i = 0; i < selectionItems.Count; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
                itemPriceObj.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else if(menuNum == 2)
        {
            for (int i = 0; i < onScreenCount; ++i)
            {
                selectionItems[i].text = playerStats.Equipment[i + (curLevel * 8)].name;
                prices[i].text = "$" + playerStats.Equipment[i + (curLevel * 8)].SellValue;
            }

            for (int i = 0; i < selectionItems.Count; ++i)
            {
                selectionMenu.transform.GetChild(i).gameObject.SetActive(true);
                itemPriceObj.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        
    }

    private void UpdateItemSelection()      //highlights whatever is currently selected
    {
        for (int i = 0; i < selectionItems.Count; i++)
        {
            if (i != selectedItem)
            {
                selectionItems[i].color = Color.black;
                if(layer == 1 || layer == 6)
                    prices[i].color = Color.black;
            }
            else
            {
                selectionItems[i].color = Color.blue;
                if(layer == 1 || layer == 6)
                    prices[i].color = Color.blue;
            }
        }
    }

    private void BuyMenuUpdateItemSelection()
    {
        ArrowHelper();
        for (int i = 0; i < onScreenCount; i++)
        {
            if (i != selectedItem)
            {
                selectionItems[i].color = Color.black;
                prices[i].color = Color.black;

                if (buyables[i + (curLevel * 8)].CostValue > playerStats.Money)
                {
                    selectionItems[i].color = Color.gray;
                    prices[i].color = Color.gray;
                }
            }
            else
            {
                selectionItems[i].color = Color.blue;
                prices[i].color = Color.blue;

                if (buyables[i + (curLevel * 8)].CostValue > playerStats.Money)
                {
                    selectionItems[i].color = Color.red;
                    prices[i].color = Color.red;
                }
            }
        }
    }

    void ArrowHelper()
    {
        if (curLevel < invenLevels)
        {
            downArrow.SetActive(true);
        }
        else
        {
            downArrow.SetActive(false);
        }

        if (curLevel > 0)
        {
            upArrow.SetActive(true);
        }
        else
        {
            upArrow.SetActive(false);
        }

        if(fullLevel && curLevel == invenLevels - 1)
        {
            downArrow.SetActive(false);
        }  
    }
}


