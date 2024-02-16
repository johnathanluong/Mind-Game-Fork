using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
public class GarySpawnIn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { int CompleteTutorial = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Complete")).value;
       switch(CompleteTutorial)
        {
            case 0:
                Destroy(gameObject);
                break;
             default:
             break;
        } 
    }
}
