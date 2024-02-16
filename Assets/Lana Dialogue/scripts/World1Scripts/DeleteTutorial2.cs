using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DeleteTutorialGuy2 : MonoBehaviour
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
        int CompleteTutorial = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("People")).value;
        switch(CompleteTutorial)
        {
            case 0:
           gameObject.SetActive(false);
                break;
            case 1:
            gameObject.SetActive(true);
           // CompleteTutorial = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("People")).value;
            //if(dialogueManager.dialogueIsPlaying && CompleteTutorial==2)
              //  gameObject.SetActive(false);
                break;
            case 2:
          // gameObject.SetActive(false);
          //if(dialogueManager.dialogueIsPlaying)
                Destroy(gameObject);
                break;
            default :
                break;
        }
    }

    }
