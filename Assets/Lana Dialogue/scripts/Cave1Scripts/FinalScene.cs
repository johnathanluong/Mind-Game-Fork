using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class FinalScene : MonoBehaviour
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
        int Complete = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Fin")).value;
       if(!dialogueManager.dialogueIsPlaying && InRange) 
        {
             dialogueManager.EnterDialogueMode(inkJSON);
             played=true;
        }
        if(played){
            
             Destroy(gameObject);
        } 
        switch(Complete)
        {
            case 0:
                break;
           case 1:
               Destroy(gameObject);
                break;
            default:
        Destroy(gameObject);
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
