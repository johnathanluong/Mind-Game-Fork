using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Member Stats", menuName = "NPC/Create party member stats")]
public class PartyStats : Stats
{
    [SerializeField] float attackGrowth = 1f;
    [SerializeField] float defenseGrowth = 1f;
    [SerializeField] float speedGrowth = 1f;

    [SerializeField] List<AllyMoves> leveledMoves;

    public float AttackGrowth
    {
        get { return attackGrowth; }
    }
    public float DefenseGrowth
    {
        get { return defenseGrowth; }
    }
    public float SpeedGrowth
    {
        get { return speedGrowth; }
    }
    public List<AllyMoves> LeveledMoves
    {
        get { return leveledMoves;}
    }

    
}

[System.Serializable]
public class AllyMoves
{
    [SerializeField] MoveBase move;
    [SerializeField] int level;

    public AllyMoves(MoveBase move, int level)
    {
        this.move = move;
        this.level = level;
    }

    public MoveBase Move
    {
        get { return move; }
    }
    public int Level
    {
        get { return level; }   
    }
}
