using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyNameController : MonoBehaviour
{

    [SerializeField] EnemyBase enemyStats;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TMP_Text>().text = enemyStats.Name;
    }

    public EnemyBase EnemyStats
    {
        get { return enemyStats; }
        set { enemyStats = value; }
    }
}
