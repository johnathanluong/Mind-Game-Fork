using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Memory : MonoBehaviour
{
    [SerializeField] private MemoryBase memoryInfo;
    GameObject player;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void interactMemory(PlayerStats stats)
    {
        if (!memoryInfo.IsViewed)
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.clearPlayerInput();
            memoryInfo.SetViewed(true);
            stats.Memories.Add(memoryInfo);
            stats.CurScene = SceneManager.GetActiveScene().name; //added this 4/15/23
            if (memoryInfo.MoveReward)
                stats.updateMoves(memoryInfo);
            if (memoryInfo.ItemReward)
            {
                if(memoryInfo.ItemReward is ItemBase)
                {
                    stats.Inventory.Add(memoryInfo.ItemReward as ItemBase);
                }
                else if(memoryInfo.ItemReward is Equipment) 
                {
                    stats.Equipment.Add(memoryInfo.ItemReward as  Equipment);
                }
            }
            
            stats.Position.initialValue.x = player.transform.position.x;
            stats.Position.initialValue.y = player.transform.position.y;
            stats.CurHP = stats.MaxHP;
            stats.CurMP = stats.MaxMP;

            //SceneManager.LoadSceneAsync(memoryInfo.Scene);
            //List<GameObject> allObjects = SceneManager.GetActiveScene().GetRootGameObjects().ToList<GameObject>();
            //for (int i = 0; i < allObjects.Count; ++i)
            //{
            //allObjects[i].SetActive(false);
            //}
            //SaveData();
            SceneManager.LoadScene(memoryInfo.Scene);
        }
        else
        {
            print("Already Viewed");
        }
    }
/*
    private void SaveData()
    {
        SerializedObject so = new SerializedObject(memoryInfo);
        so.ApplyModifiedProperties();
        EditorUtility.SetDirty(memoryInfo);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
*/
}
