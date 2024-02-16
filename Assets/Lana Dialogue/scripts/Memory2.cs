using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Memory2 : MonoBehaviour
{
    [SerializeField] private MemoryBase memoryInfo;
    GameObject player;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void interactMemory2(PlayerStats stats)
    {
        
            stats.CurScene = SceneManager.GetActiveScene().name; //added this 4/15/23
            stats.Position.initialValue.x = player.transform.position.x;
            stats.Position.initialValue.y = player.transform.position.y;
            //SceneManager.LoadScene(memoryInfo.Scene);
            List<GameObject> allObjects = SceneManager.GetActiveScene().GetRootGameObjects().ToList<GameObject>();
            for (int i = 0; i < allObjects.Count; ++i)
            {
                allObjects[i].SetActive(false);
            }

            SceneManager.LoadScene(memoryInfo.Scene);
        }
    }

