using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chest", menuName = "Create new chest")]
public class ChestData : ScriptableObject
{
    [SerializeField] List<Buyable> items;

    [SerializeField] bool opened;

    public List<Buyable> Items
    {
        get { return items; }
    }
    public bool Opened
    {
        get { return opened; }
        set { opened = value; }
    }
}
