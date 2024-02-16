using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class HA : MonoBehaviour
{
    
    
    private DialogueManager dialogueManager;
   

    public void Start()
    {
        dialogueManager = DialogueManager.GetInstance();
           transform.Find("PuzzleGuy").gameObject.SetActive(false);
    }
   public void Update()
    {
        int CompleteTutorial = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Complete")).value;

        switch(CompleteTutorial)
        {
            case 0:
           // gameObject.SetActive(false);
           transform.Find("PuzzleGuy").gameObject.SetActive(false);
                break;
            case 1:
           transform.Find("PuzzleGuy").gameObject.SetActive(true);
            //if(!dialogueManager.dialogueIsPlaying)
               // Destroy(gameObject);
                break;
            case 2:
            //if(!dialogueManager.dialogueIsPlaying)
                transform.Find("PuzzleGuy").gameObject.SetActive(false);
                break;
            case 3:
            //gameObject.SetActive(true);
           // if(!DialogueManager.GetInstance().dialogueIsPlaying)
              // transform.Find("PuzzleGuy").gameObject.SetActive(false);
             // transform.Find("PuzzleGuy").gameObject.SetActive(true);
                break;
            default:
             transform.Find("PuzzleGuy").gameObject.SetActive(false);
                break;

        }
    }
    
}
