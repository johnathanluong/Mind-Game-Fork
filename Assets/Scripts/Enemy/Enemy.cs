using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy
{
    [SerializeField] EnemyBase eBase;
    [SerializeField] int level;
    [SerializeField] int curHP;
    public Enemy(EnemyBase eBase, int level)
    {
        this.eBase = eBase;
        this.level = level;
    }


    public string Name
    {
        get { return eBase.name; }
    }
    public string Description
    {
        get { return eBase.Description; }
    }
    public int Level
    {
        get { return level; }
    }
    public Sprite BattleSprite
    {
        get { return eBase.BattleSprite; }
    }
    public Types Weaknesss
    {
        get { return eBase.Weaknesss; }
    }
    public Types Resist
    {
        get { return eBase.Resist; }
    }
    public int MaxHP
    {
        get { return Mathf.RoundToInt(eBase.HP + (4f * (level - 1))); }
    }
    public int CurHP
    {
        get { return curHP; }
        set { curHP = value; }
    }
    public int Attack
    {
        get { return (eBase.Attack * (10 + (level-1)))/10; }
        //get { return (eBase.Attack + (1 * (level-1))); }
    }
    public int Defense
    {
        get { return (eBase.Defense * (10 + (level-1)))/10; }
        //get {return (eBase.Defense + (1 * (level - 1))); }
    }
    public float DefCalc
    {
        get { return (40f / (40f + Defense)); }
    }
    public int Speed
    {
        get { return (eBase.Speed * (10 + (level-1)))/10; }
        //get { return (eBase.Speed + (1 * (level - 1))); }
    }
    public int XP
    {
        get { return (int)(eBase.XpGiven + (0.15f * (level-1) * eBase.XpGiven)); }
    }
    public int Money
    {
        get { return (int)(eBase.MoneyGiven + (0.15f * (level-1) * eBase.MoneyGiven)); }
    }
    public List<EMoveBase> Moves
    {
        get { return eBase.Moves; }
    }
    public EnemyBase BaseStats
    {
        get { return eBase; }
    }
}
