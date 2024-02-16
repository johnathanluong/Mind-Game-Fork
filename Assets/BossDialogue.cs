using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class BossDialogue : MonoBehaviour
{

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private DialogueManager dialogueManager;
    private InputManager inputManager;
    private bool played=false;
    public TextAsset InkJSON
    {
        get { return inkJSON; }
        set { inkJSON = value; }
    }

    private void Update() 
    {
        if(!dialogueManager.dialogueIsPlaying && played == false)
        {
            played = true;
            dialogueManager.EnterDialogueMode(inkJSON);
        }
    }

    public void beginDialogue()
    {
        
        dialogueManager = DialogueManager.GetInstance();
        inputManager = InputManager.GetInstance();
        dialogueManager.EnterDialogueMode(inkJSON); //emoteAnimator);
        //played = false;
    }

    public bool isFinished()
    {   
        return dialogueManager.dialogueCompleted;
    }
}