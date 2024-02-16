using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Create new item")]
public class ItemBase : Buyable
{
    [SerializeField] AffectedStat affectedStat;
    [SerializeField] int effect;
    [SerializeField] bool revive;

    public AffectedStat Stat
    {
        get { return affectedStat; }
    }
    public int Effect
    {
        get { return effect; }
    }
    public bool Revive
    {
        get { return revive; }
    }

    public enum AffectedStat
    {
        HP,
        MP
    }
}