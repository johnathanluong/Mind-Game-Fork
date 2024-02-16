using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTrigger : MonoBehaviour
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
        playerStorage.initialValue = playerPosition;
        dialogueManager = DialogueManager.GetInstance();
       // if (inputManager.GetInteractPressed()) 
         //   {
                inputManager = InputManager.GetInstance();
          //  }
    }

    // Update is called once per frame
    void Update()
    {
        if(!dialogueManager.dialogueIsPlaying && !played)
        {
             dialogueManager.EnterDialogueMode(inkJSON);
             played=true;
        }
        if(!dialogueManager.dialogueIsPlaying && played){
            stats.CurScene = sceneToLoad;
            SceneManager.LoadScene(sceneToLoad);
        } 
    }
}
