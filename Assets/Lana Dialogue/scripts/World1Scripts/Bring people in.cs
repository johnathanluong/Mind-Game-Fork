using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Bringpeoplein : MonoBehaviour
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
                gameObject.SetActive(false);
                break;
            case 1:
            gameObject.SetActive(true);
                break;
                case 2:
                gameObject.SetActive(true);
                //AstarPath.active.Scan();
                break;
            default:
           gameObject.SetActive(true); //added idk what it does
           // AstarPath.active.Scan();
                break;
        }
    }
}
