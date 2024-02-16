using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DeleteTutorialGuy : MonoBehaviour
{
   // public bool played=false;
   // public bool InRange=false;
    
    private DialogueManager dialogueManager;
    private InputManager inputManager;
    void Start()
    {
        dialogueManager = DialogueManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        int CompleteTutorial = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Complete")).value;
        switch(CompleteTutorial)
        {
            case 0:
                break;
            case 1:
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }

    }
