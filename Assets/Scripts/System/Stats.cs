using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats : ScriptableObject
{
    [SerializeField] string MemberName;
    [SerializeField] Sprite portrait;
    [SerializeField] Sprite battleSprite;
    [SerializeField] Sprite battleSprite2;
    [SerializeField] Sprite battleSprite3;
    [SerializeField] bool unlocked;

    //Stats
    [SerializeField] int level;
    [SerializeField] int xp;
    [SerializeField] int maxHP;
    [SerializeField] int curHP;
    [SerializeField] int maxMP;
    [SerializeField] int curMP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int speed;

    [SerializeField] Weapon weapon;
    [SerializeField] Armor armor;

    [SerializeField] List<LearnableMove> learnableMoves;
    [SerializeField] List<LearnableMove> moveSet;

    public string Name
    {
        get { return MemberName; }
    }
    public Sprite Portrait
    {
        get { return portrait; }
    }
    public Sprite BattleSprite
    {
        get { return battleSprite; }
    }
    public Sprite BattleSprite2
    {
        get { return battleSprite2; }
    }
    public Sprite BattleSprite3
    {
        get { return battleSprite3; }
    }
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    public int Xp
    {
        get { return xp; }
        set { xp = value; }
    }
    public int XpNeeded
    {
        get { return (int)(10 * Mathf.Pow(1.2f, level - 1) + (8 * (level-1))); }
    }
    public int MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
    }
    public int CurHP
    {
        get { return curHP; }
        set
        {
            if (value < 0)
                curHP = 0;
            else if (value > maxHP)
                curHP = maxHP;
            else
                curHP = value;
        }
    }
    public int MaxMP
    {
        get { return maxMP; }
        set { maxMP = value; }
    }
    public int CurMP
    {
        get { return curMP; }
        set
        {
            if (value < 0)
                curMP = 0;
            else if (value > maxMP)
                curMP = maxMP;
            else
                curMP = value;
        }
    }
    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }
    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public int TotalAttack
    {
        get
        {
            if (Weapon)
                return Attack + Weapon.WeaponAttack;
            else
                return Attack;
        }
    }
    public int TotalDefense
    {
        get
        {
            if (Armor)
                return Defense + Armor.ArmorDefense;
            else
                return Defense;
        }
    }

    public float DefCalc
    {
        get { return (25f / (25f + TotalDefense)); }
    }

    public Weapon Weapon
    {
        get { return weapon; }
        set { weapon = value; }
    }
    public Armor Armor
    {
        get { return armor; }
        set { armor = value; }
    }
    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
        set { learnableMoves = value; }
    }

    public List<LearnableMove> MoveSet
    {
        get { return moveSet; }
        set { moveSet = value; }
    }
}
