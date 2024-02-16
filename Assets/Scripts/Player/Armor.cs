using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Equipment/Create new armor")]

public class Armor : Equipment
{
    [SerializeField] int addedDefense;
    public int ArmorDefense
    {
        get { return addedDefense; }
    }

}
