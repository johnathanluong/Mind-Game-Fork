using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutscenePicture3 : MonoBehaviour
{

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public Vector2 playerPosition;
    // Start is called before the first frame update
    public string sceneToLoad;
    private bool played=false; //if the scene played

     private DialogueManager dialogueManager;
     private InputManager inputManager;
     //public Vector2 playerPosition;
    public PlayerStats stats;
    public VectorValue playerStorage;
    // Start is called before the first frame update
    void Start()
    {
     transform.GetChild(0).gameObject.SetActive(false);
     transform.GetChild(1).gameObject.SetActive(false);
         //transform.GetChild(0).gameObject.SetActive(false);
        //transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
      //  transform.GetChild(3).gameObject.SetActive(false);
        playerStorage.initialValue = playerPosition;
        dialogueManager = DialogueManager.GetInstance();
        inputManager = InputManager.GetInstance();

    }

    // Update is called once per frame
    void Update()
    {
       int SceneChange = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("tem")).value;
        switch(SceneChange)
        {
            case 0: //ultra bad
            transform.GetChild(2).gameObject.SetActive(false);
          transform.GetChild(1).gameObject.SetActive(false);
          transform.GetChild(0).gameObject.SetActive(true);
          transform.GetChild(2).gameObject.SetActive(false);
          transform.GetChild(1).gameObject.SetActive(false);
         //transform.GetChild(0).gameObject.SetActive(false);
        //transform.GetChild(1).gameObject.SetActive(false);
       // transform.GetChild(2).gameObject.SetActive(false);
         // transform.GetChild(1).gameObject.SetActive(false);
         // transform.GetChild(0).gameObject.SetActive(true);
          if(!dialogueManager.dialogueIsPlaying && !played)
        {
             dialogueManager.EnterDialogueMode(inkJSON);
             played=true;
        }
        if(!dialogueManager.dialogueIsPlaying && played){
            
             SceneManager.LoadScene(sceneToLoad);
        } 
                break;
            case 1: //ultra good
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
            //transform.GetChild(3).gameObject.SetActive(false);
           // transform.GetChild(1).gameObject.SetActive(true);
            if(!dialogueManager.dialogueIsPlaying && !played)
        {
             dialogueManager.EnterDialogueMode(inkJSON);
             played=true;
        }
        if(!dialogueManager.dialogueIsPlaying && played){
            
             SceneManager.LoadScene(sceneToLoad);
        } 
                break;
                case 2: //Normal
            //transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
            if(!dialogueManager.dialogueIsPlaying && !played)
        {
             dialogueManager.EnterDialogueMode(inkJSON);
             played=true;
        }
        if(!dialogueManager.dialogueIsPlaying && played){
            
             SceneManager.LoadScene(sceneToLoad);
        } 
            break;
            default :
                break;
        }
    }
}
