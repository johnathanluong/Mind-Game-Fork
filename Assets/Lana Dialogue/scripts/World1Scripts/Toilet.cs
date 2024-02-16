using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour
{
    private DialogueManager dialogueManager;

    public void Start()
    {
            dialogueManager = DialogueManager.GetInstance();
           // inputManager = InputManager.GetInstance();
            //played=false;
            //InRange=false;
    }
   public void Update()
    {
        int M = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Y")).value;
        //int Dragon = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Dragon")).value;
         switch(M)
        {
            case 0:
            //if(Dragon==3)
              // transform.Find("Dragon").gameObject.SetActive(false);
            
              break;
            case 1:
             transform.Find("Scene Transition to HubWorld").gameObject.SetActive(true);
             break;
             default:
             transform.Find("Scene Transition to HubWorld").gameObject.SetActive(true);
             break;

        }
        
    }
    
}
