using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuSelect : MonoBehaviour
{
    [SerializeField] List<TMP_Text> menuOptions;
    [SerializeField] GameObject options;

    private bool inOptions = false;
    private int curOption;

    void Update()
    {
        if (!inOptions)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                menuOptions[curOption].color = Color.black;
                if (curOption == 0)
                    curOption = menuOptions.Count - 1;
                else
                    curOption--;
                menuOptions[curOption].color = Color.blue;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                menuOptions[curOption].color = Color.black;
                curOption = (curOption + 1) % menuOptions.Count;
                menuOptions[curOption].color = Color.blue;
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                if (curOption == 0)
                    gameObject.GetComponent<SaveSystem>().NewGame();
                else if (curOption == 1)
                    gameObject.GetComponent<SaveSystem>().LoadGame();
                else if (curOption == 2)
                {
                    options.SetActive(true);
                    inOptions = true;
                }
                else
                    Application.Quit();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                options.SetActive(false);
                inOptions = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                options.SetActive(false);
                inOptions = false;
            }
        }
    }
}
