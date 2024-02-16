using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAllyToBattle : MonoBehaviour
{
    [SerializeField] BattleState battleState;
    [SerializeField] PartyStats ally;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        if (!gameManager.PartyStats.Contains(ally))
        {
            gameManager.PartyStats.Add(ally);
        }
        for(int i = 0; i < gameManager.PartyStats.Count; i++)
        {
            if (!gameManager.PartyStats[i])
            {
                gameManager.PartyStats.RemoveAt(i);
            }
        }
        battleState.Ally = ally;

        if(ally.Name == "Gary")
        {
            gameManager.World = 0;
        }
        else if(ally.Name == "Lily")
        {
            gameManager.World = 1;
        }
        else if(ally.Name == "Olivia")
        {
            gameManager.World = 2;
        }
        //Debug.Log("Ally added to party");
    }
}
