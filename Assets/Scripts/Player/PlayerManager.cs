using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject interactNotification;
    public PlayerStats stats;
    
    public void NotifyPlayer()
    {
        interactNotification.SetActive(true);
    }
    public void DenotifyPlayer()
    {
        interactNotification.SetActive(false);
    }

    public void updateMoves(MemoryBase memory)
    {
        LearnableMove newMove = new LearnableMove(memory.MoveReward, memory);
        stats.LearnableMoves.Add(newMove);

        if(stats.MoveSet.Count < 4)
        {
            stats.MoveSet.Add(newMove);
        }
        else
        {
            //choose whether to replace move in moveset or not add the new move
        }
    }
}
