using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buyable : ScriptableObject
{
    [TextArea]
    [SerializeField] string description;

    [SerializeField] int costValue;
    [SerializeField] int sellValue;
    public string Description
    {
        get { return description; }
    }
    public int CostValue
    {
        get { return costValue; }
    }
    public int SellValue
    {
        get { return sellValue; }
    }
}
