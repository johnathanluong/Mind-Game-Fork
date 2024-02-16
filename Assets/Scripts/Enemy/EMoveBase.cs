using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Enemy/Create new move")]
public class EMoveBase : ScriptableObject
{
    [TextArea]
    [SerializeField] string description;

    [SerializeField] bool aoe;
    
    [SerializeField] int power;

    public string Description
    {
        get { return description; }
    }
    public bool Aoe
    {
        get { return aoe; }
    }
    public int Power
    {
        get { return power; }
    }
}
