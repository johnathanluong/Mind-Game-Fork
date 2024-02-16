using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;

    public static EnemyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("EnemyManager is null");
            }
            return _instance;
        }
    }

    public void StartCombat()
    {
        Debug.Log("hi?");
        //List<GameObject> enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        //Debug.Log(enemies.Count);
    }
}
