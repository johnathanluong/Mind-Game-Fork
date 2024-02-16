using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyLvlController : MonoBehaviour
{
    [SerializeField] int level;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TMP_Text>().text = "LVL " + level;
    }

}
