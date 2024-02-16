using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Player/Create new move")]
public class MoveBase : ScriptableObject
{
    [TextArea] 
    [SerializeField] string description;

    [SerializeField] Types type;
    [SerializeField] int power;
    [SerializeField] int manaCost;
    [SerializeField] bool aoe;

    [SerializeField] List<KeyCode> keys1;
    [SerializeField] List<KeyCode> keys2;
    [SerializeField] List<KeyCode> keys3;

    public string Description
    {
        get { return description; }
    }
    public Types Type
    {
        get { return type; }
    }
    public int Power
    {
        get { return power; }
    }
    public int ManaCost
    {
        get { return manaCost; }
    }
    public bool Aoe
    {
        get { return aoe; }
    }
    public List<KeyCode> Keys1
    {
        get { return keys1; }
    }
    public List<KeyCode> Keys2
    {
        get { return keys2; }
    }
    public List<KeyCode> Keys3
    {
        get { return keys3; }
    }
}
