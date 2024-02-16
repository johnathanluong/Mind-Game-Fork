using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAllyFromBattle : MonoBehaviour
{
    [SerializeField] BattleState battleState;
    [SerializeField] PartyStats ally;
    GameManager gameManager;
    void Start()
    {
        ally = null;
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        gameManager = GameManager.Instance;

        if (gameManager.PartyStats.Contains(ally))
        {
            gameManager.PartyStats.Remove(ally);
        }
        battleState.Ally = null;
    }

    void OnDestroy()
    {
        gameManager = GameManager.Instance;
        if (!gameManager.PartyStats.Contains(ally))
        {
            gameManager.PartyStats.Add(ally);
        }
        battleState.Ally = ally;
    }
}
