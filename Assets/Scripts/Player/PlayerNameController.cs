using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameController : MonoBehaviour
{

    [SerializeField] PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TMP_Text>().text = playerStats.Name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
