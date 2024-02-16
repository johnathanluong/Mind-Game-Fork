using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MemoryList", menuName = "Memory/Create memory list")]

public class MemoryList : ScriptableObject
{
    [SerializeField] List<MemoryBase> allMemories;

    public List<MemoryBase> AllMemories
    {
        get { return allMemories; }
    }
}
