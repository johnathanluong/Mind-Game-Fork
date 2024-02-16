using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Stats", menuName = "Player/Create player stats")]
public class PlayerStats : Stats
{
    [SerializeField] string curScene;

    [SerializeField] VectorValue position;
    [SerializeField] VectorValue positionWorld1;
    [SerializeField] VectorValue positionWorld2;
    [SerializeField] string sceneWorld1;
    [SerializeField] string sceneWorld2;
    [SerializeField] bool isMoving; 

    //Stats
    [SerializeField] int points;
    [SerializeField] int money;

    [SerializeField] List<ItemBase> inventory;
    [SerializeField] List<Equipment> equipment;
    [SerializeField] List<MemoryBase> memoriesViewed; 
    [SerializeField] List<CutsceneBase> cutscenesViewed;

    public string CurScene
    {
        get { return curScene; }
        set { curScene = value; }
    }

    public VectorValue Position
    {
        get { return position; }
    }
    public VectorValue PositionWorld1
    {
        get { return positionWorld1; }
    }
    public VectorValue PositionWorld2
    {
        get { return positionWorld2; }
    }
    public string SceneWorld1
    {
        get { return sceneWorld1; }
        set { sceneWorld1 = value; }
    }
    public string SceneWorld2
    {
        get { return sceneWorld2; }
        set { sceneWorld2 = value; }
    }
    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }
    public int Money
    {
        get { return money; }
        set { money = value; }
    }
    public int Points
    {
        get { return points; }
        set { points = value; }
    }
    public List<ItemBase> Inventory
    {
        get { return inventory; }
        set { inventory = value; }
    }
    public List<Equipment> Equipment
    {
        get { return equipment; }
        set { equipment = value; }
    }
    public List<MemoryBase> Memories
    {
        get { return memoriesViewed; }
        set { memoriesViewed = value; }
    }
    public List<CutsceneBase> CutScenes
    {
        get { return cutscenesViewed; }
        set { cutscenesViewed = value; }
    }



    public void updateMoves(MemoryBase memory)
    {
        LearnableMove newMove = new LearnableMove(memory.MoveReward, memory);

        if (MoveSet.Count < 4)
        {
            MoveSet.Add(newMove);
        }
        else
        {
            LearnableMoves.Add(newMove);    
        }
    }
}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] MemoryBase memory;

    public LearnableMove(MoveBase move, MemoryBase mem){
        moveBase = move;
        memory = mem;
    }

    public MoveBase Base
    {
        get { return moveBase; }
    }
    public MemoryBase Memory
    {
        get { return memory; }
    }
}
