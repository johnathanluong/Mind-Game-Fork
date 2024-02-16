using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
public class Ldelete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { int CompleteTutorial = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Sequence")).value;
       switch(CompleteTutorial)
        {
            case 3:
                Destroy(gameObject);
                break;
                case 4:
                Destroy(gameObject);
                break;
                case 5:
                Destroy(gameObject);
                break;
             default:
             break;
        } 
    }
}
