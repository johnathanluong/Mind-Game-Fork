using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ink.Runtime;

public class ScenesTransDialogue : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    public bool played=false;
    public bool InRange=false;
    
    private DialogueManager dialogueManager;
    private InputManager inputManager;

    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    // Start is called before the first frame update
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
       if(!dialogueManager.dialogueIsPlaying && InRange &&!played) 
        {
             dialogueManager.EnterDialogueMode(inkJSON);
             played=true;
             
        }
        if(!dialogueManager.dialogueIsPlaying && played){
           // SceneManager.LoadScene(sceneToLoad);
            Destroy(gameObject);
            SceneManager.LoadScene(sceneToLoad);
        } 
        switch(CompleteTutorial)
        {
           case 0:
                break;
           case 1:
          // yield return new WaitForSeconds(0.2f);
           if(!dialogueManager.dialogueIsPlaying)
            Destroy(gameObject);
                break;
            default:
             if(!dialogueManager.dialogueIsPlaying)
            Destroy(gameObject);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            playerStorage.initialValue = playerPosition;
            InRange = true;
        }
    }
}