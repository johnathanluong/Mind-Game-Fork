using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class HubDialogue1 : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public bool played=false;
    public bool InRange=false;
    
    private DialogueManager dialogueManager;
    private InputManager inputManager;
    

    public void Start()
    {
            dialogueManager = DialogueManager.GetInstance();
           int CompleteTutorial = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Complete")).value;
    }
   public void Update()
    {
        int CompleteTutorial = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Complete")).value;
       if(!dialogueManager.dialogueIsPlaying && InRange) 
        {
             dialogueManager.EnterDialogueMode(inkJSON);
             played=true;
        }
        if(played){
            
             Destroy(gameObject);
        } 
        switch(CompleteTutorial)
        {
            case 0:
                break;
           // case 1:
          //      Destroy(gameObject);
          //      break;
            default:
            //Destroy(gameObject);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            InRange = true;
        }
    }
}

