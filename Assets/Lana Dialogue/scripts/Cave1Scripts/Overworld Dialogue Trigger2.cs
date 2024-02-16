using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class OverworldDialogueTrigger2 : MonoBehaviour
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
           // inputManager = InputManager.GetInstance();
            //played=false;
            //InRange=false;
    }
   public void Update()
    {
        int CompleteTutorial = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Complete")).value;
        int CompleteTutorial2 = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Complete2")).value;
        int People = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("People")).value;

       //if(!dialogueManager.dialogueIsPlaying && InRange) 
       // {
         //    dialogueManager.EnterDialogueMode(inkJSON);
         //    played=true;
      //  }
        //if(played){
            
        //     Destroy(gameObject);
       // } 
        switch(CompleteTutorial)
        {
            case 0:
                break;
            case 1:
               if(!dialogueManager.dialogueIsPlaying && InRange) 
        {
             dialogueManager.EnterDialogueMode(inkJSON);
             played=true;
        }
        if(played){
            
             Destroy(gameObject);
        } 
                break;
            default:
             if(!dialogueManager.dialogueIsPlaying && InRange) 
        {
             dialogueManager.EnterDialogueMode(inkJSON);
             played=true;
        }
        if(played){
            
             Destroy(gameObject);
        } 
                break;
        }
        switch(CompleteTutorial2)
        {
            case 0:
                break;
            case 1:
             Destroy(gameObject);
             break;
             default:
             break;
        }
        switch(People)
        {
            case 0:
            break;
            case 1:
            Destroy(gameObject);
            break;
            case 2:
            Destroy(gameObject);
            break;
            default:
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
