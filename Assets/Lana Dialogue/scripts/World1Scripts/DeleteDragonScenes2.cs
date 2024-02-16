using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using TMPro;
public class DeleteDragonScenes2 : MonoBehaviour
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
        int People = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Dragon")).value;

       //if(!dialogueManager.dialogueIsPlaying && InRange) 
       // {
         //    dialogueManager.EnterDialogueMode(inkJSON);
         //    played=true;
      //  }
        //if(played){
            
        //     Destroy(gameObject);
       // } 
        switch(People)
        {
            case 0:
                break;
                case 1:
                break;
            case 2:
            
               if(!dialogueManager.dialogueIsPlaying && InRange) 
        {
             dialogueManager.EnterDialogueMode(inkJSON);
             played=true;
        }
        if(played){
            
             Destroy(gameObject);
        } 
                break;
                case 3:
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
