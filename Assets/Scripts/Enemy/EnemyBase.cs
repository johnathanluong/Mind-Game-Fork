using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Create new enemy")]
public class EnemyBase : ScriptableObject
{
    [SerializeField] string EnemyName;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite battleSprite;

    [SerializeField] Types weaknesss;
    [SerializeField] Types resist;

    //Stats
    [SerializeField] int baseHP;

    [SerializeField] int curHP; //just cause i don't want to rewrite marc's stuff :D

    [SerializeField] int baseAttack = 10;
    [SerializeField] int baseDefense = 10;
    [SerializeField] int baseSpeed = 10;

    [SerializeField] int xpGiven = 1;
    [SerializeField] int moneyGiven = 1;

    [SerializeField] List<EMoveBase> moves;


    public string Name
    {
        get { return EnemyName; }
    }
    public string Description
    {
        get { return description; }
    }
    public Sprite BattleSprite
    {
        get { return battleSprite; }
    }
    public Types Weaknesss
    {
        get { return weaknesss; }
    }
    public Types Resist
    {
        get { return resist; }
    }
    public int HP
    {
        get { return baseHP; }
    }

    public int CurHP
    {
        get { return curHP; }
        set { curHP = value; }
    }

    public int Attack
    {
        get { return baseAttack; }
    }
    public int Defense
    {
        get { return baseDefense; }
    }
    public int Speed
    {
        get { return baseSpeed; }
    }
    public int XpGiven
    {
        get { return xpGiven; }
    }
    public int MoneyGiven
    {
        get { return moneyGiven; }
    }
    public List<EMoveBase> Moves
    {
        get { return moves; }
    }
}

public enum Types
{
    None,
    Physical,
    Magical,
    Psychological
}
