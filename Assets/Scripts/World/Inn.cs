using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
public class Inn : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    private GameObject baseMenu;
    private GameObject selectionMenu;
    private TextMeshProUGUI shopText1;
    private TextMeshProUGUI shopText2;
    private TextMeshProUGUI shopText3;
    private TextMeshProUGUI moneyText;

    private GameManager gameManager;

    [SerializeField] int cost;
    [SerializeField] string shopLine1 = "Get some zzz's";
    [SerializeField] string shopLine2 = "Would you like to rest?";
    [SerializeField] string brokeLine = "Oh you're broke? Get out.";

    List<TextMeshProUGUI> selectionItems;

    PlayerStats playerStats;
    List<PartyStats> partyStats;
    Fader fader;

    int selectedItem = 0;
    bool innOpened = false;
    private void Start()
    {
        gameManager = GameManager.Instance;
        playerStats = gameManager.PlayerStats;
        partyStats = gameManager.PartyStats;
        baseMenu = canvas.transform.GetChild(11).gameObject;
        selectionMenu = baseMenu.transform.GetChild(0).gameObject;
        selectionItems = selectionMenu.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        shopText1 = baseMenu.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        shopText2 = baseMenu.transform.GetChild(1).gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        shopText3 = baseMenu.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        moneyText = baseMenu.transform.GetChild(1).gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        fader = FindObjectOfType<Fader>();
    }

    private void Update()
    {
        if (innOpened)
        {
            Selection();
        }
    }

    public void OpenShop()
    {
        gameManager.playerStopped = true;
        innOpened = true;
        InnMenu();
        baseMenu.SetActive(true);
        UpdateItemSelection();
    }
    private void CloseShop()
    {
        gameManager.playerStopped = false;
        innOpened = false;
        baseMenu.SetActive(false);
    }

    private void InnMenu()
    {
        shopText1.text = shopLine1;
        shopText2.text = shopLine2;
        moneyText.text = "$" + playerStats.Money.ToString();
        shopText3.text = "Your room will cost $" + cost;


        if (playerStats.Money >= cost)
        {
            selectionItems[0].text = "Rest";
        }
        else
        {
            selectionItems[0].text = brokeLine;
        }
    }

    private void Selection()
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
            selectedItem = Mathf.Clamp(selectedItem, 0, selectionItems.Count - 1);
            UpdateItemSelection();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selectedItem == 0)
            {
                if (playerStats.Money >= cost)
                {
                    baseMenu.SetActive(false);
                    StartCoroutine(Rest());
                    CloseShop();
                }
                else
                {
                    CloseShop();
                }
            }
            else if (selectedItem == 1)
            {
                CloseShop();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShop();
        }
    }

    private IEnumerator Rest()
    {
        fader = FindObjectOfType<Fader>();
        yield return fader.FadeIn(1f);
        playerStats.CurHP = playerStats.MaxHP;
        playerStats.CurMP = playerStats.MaxMP;
        for (int i = 0; i < partyStats.Count; ++i)
        {
            partyStats[i].CurHP = partyStats[i].MaxHP;
            partyStats[i].CurMP = partyStats[i].MaxMP;
        }
        playerStats.Money -= cost;
        yield return fader.FadeOut(1f);
    }

    private void UpdateItemSelection()      //highlights whatever is currently selected
    {
        for (int i = 0; i < selectionItems.Count; i++)
        {
            if (playerStats.Money >= cost) 
            {
                if (i != selectedItem)
                {
                    selectionItems[i].color = Color.black;
                }
                else
                {
                    selectionItems[i].color = Color.blue;
                }
            }
            else
            {
                if(i != selectedItem)
                {
                    selectionItems[i].color = Color.black;
                }
                else
                {
                    if(selectedItem == 0)
                    {
                        selectionItems[i].color = Color.red;
                    }
                    else if(selectedItem == 1)
                    {
                        selectionItems[i].color = Color.blue;
                    }
                }
            }
        }
    }
}
