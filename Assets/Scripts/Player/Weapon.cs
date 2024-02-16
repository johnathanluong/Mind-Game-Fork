using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Equipment/Create new weapon")]
public class Weapon : Equipment
{
    [SerializeField] int addedAttack;
    public int WeaponAttack
    {
        get { return addedAttack; }
    }
}
