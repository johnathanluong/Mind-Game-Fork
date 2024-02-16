using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DeleteRock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string rock2 = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("rock")).value;

        switch(rock2)
        {
            case "":
                break;
            case "Not Pushed":
            //Destroy(gameObject);
                break;
            case "Pushed":
                Destroy(gameObject);
                AstarPath.active.Scan();
                break;
            default:
                break;
        }
    }
}
