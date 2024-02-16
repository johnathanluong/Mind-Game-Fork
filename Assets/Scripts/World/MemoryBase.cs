using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Memory", menuName = "Memory/Create new memory")]
public class MemoryBase : ScriptableObject
{
    [SerializeField] string memoryName;
    [SerializeField] string scene;
    [SerializeField] string location;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] MoveBase moveReward;
    [SerializeField] Buyable itemReward;

    [SerializeField] bool isViewed;

    public string Name
    {
        get { return memoryName; }
    }
    public string Scene
    {
        get { return scene; }
    }

    public string Location
    {
        get { return location; }
    }

    public string Description
    {
        get { return description; }
    }
    public MoveBase MoveReward
    {
        get { return moveReward; }
    }
    public Buyable ItemReward
    {
        get { return itemReward; }
    }
    public bool IsViewed
    {
        get { return isViewed; }
    }
    public void SetViewed(bool val)
    {
        isViewed = val;
    }
}
