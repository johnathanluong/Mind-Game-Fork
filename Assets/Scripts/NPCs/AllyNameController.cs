using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllyNameController : MonoBehaviour
{
    [SerializeField] PartyStats allyStats;

    public PartyStats AllyStats
    {
        set { allyStats = value; }
    }
    
    // Start is called before the first frame update
    public void setup()
    {
        gameObject.GetComponent<TMP_Text>().text = allyStats.Name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
