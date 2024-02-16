using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour
{
    [SerializeField] List<GameObject> memoryObjects; //memory objects that are in the current scene
    [SerializeField] MemoryList memoryList;

    private Memory[] memory;
    private static MemoryManager instance;


    public List<GameObject> MemoryObjects
    {
        get { return memoryObjects; }
    }
    public List<MemoryBase> MemoryList
    {
        get { return memoryList.AllMemories; }
    }

    public static MemoryManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        memory = FindObjectsOfType<Memory>();
        for(int i = 0; i < memory.Length; ++i) 
        {
            memoryObjects.Add(memory[i].gameObject);
        }
    }

    public void resetMemories()
    {
        foreach(MemoryBase mem in MemoryList)
        {
            mem.SetViewed(false);
        }
    }
}
