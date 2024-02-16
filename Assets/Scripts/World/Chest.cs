using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] Sprite open;
    [SerializeField] Sprite close;
    [SerializeField] ChestData chestData;
    [SerializeField] PlayerStats playerStats;

    [SerializeField] string openedBeforeText = "You have already opened this chest.";
    [SerializeField] string receivedText = "You received";

    GameManager gameManager;
    SpriteRenderer sr;
    GameObject canvas;
    GameObject menu;
    GameObject info;
    TextMeshProUGUI infoText;
    TextMeshProUGUI infoText2;
    bool menuOpen;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gameManager = GameManager.Instance;
        if(chestData.Opened)
        {
            sr.sprite = open;
        }
        else
        {
            sr.sprite = close;
        }
    }

    private void Update()
    {
        if(menuOpen)
        {
            ChestMenuSelect();
        }
    }

    public void interactChest()
    {
        canvas = GameObject.FindGameObjectWithTag("UI");
        menu = canvas.transform.GetChild(12).gameObject;
        info = menu.transform.GetChild(1).gameObject;
        infoText = info.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        infoText2 = info.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        ChestMenu();

        if (!chestData.Opened)
        {
            chestData.Opened = true;
            //SaveData();
            for(int i = 0; i < chestData.Items.Count; i++)
            {
                if (chestData.Items[i] is ItemBase)
                {
                    playerStats.Inventory.Add((ItemBase)chestData.Items[i]);
                }
                else if (chestData.Items[i] is Equipment)
                {
                    playerStats.Equipment.Add((Equipment)chestData.Items[i]);
                }
            }
            sr.sprite = open;
        }
    }

    private void ChestMenu()
    {
        gameManager.playerStopped = true;
        menuOpen = true;
        if (chestData.Opened)
        {
            infoText2.text = openedBeforeText;
        }
        else
        {
            infoText2.text = receivedText;
        }
        infoText.text = "Rewards: ";
        
        for(int i = 0; i < chestData.Items.Count; i++)
        {
            infoText.text += chestData.Items[i].name;
            if(i < chestData.Items.Count - 1)
            {
                infoText.text += ", ";
            }
        }
        infoText.text += ".";

        menu.SetActive(true);
    }

    private void ChestMenuSelect()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CloseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
    }

    private void CloseMenu()
    {
        gameManager.playerStopped = false;
        menu.SetActive(false);
        menuOpen = false;
    }

 /*   private void SaveData()
    {
        SerializedObject so = new SerializedObject(chestData);
        so.ApplyModifiedProperties();
        EditorUtility.SetDirty(chestData);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }*/
}
