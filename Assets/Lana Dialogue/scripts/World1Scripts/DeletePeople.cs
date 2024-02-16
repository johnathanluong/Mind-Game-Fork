using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DeletePeople : MonoBehaviour
{private DialogueManager dialogueManager;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = DialogueManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        int People = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("People")).value;

        switch(People)
        {
            case 0:
                break;
            case 1:
            if(!dialogueManager.dialogueIsPlaying){
                Destroy(gameObject);
                AstarPath.active.Scan();
            }
                
                break;
                case 2:
                Destroy(gameObject);
                
                break;
            default:
            Destroy(gameObject); //added idk what it does
           // AstarPath.active.Scan();
                break;
        }
    }
}
