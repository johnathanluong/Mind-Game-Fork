using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
public class World1DialogueMiniBossComplete : MonoBehaviour
{
     [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public bool played=false;
    public bool InRange=false;
    private int K=0;
    private DialogueManager dialogueManager;
    private InputManager inputManager;
[SerializeField] private CutsceneBase CutsceneInfo;
    public PlayerStats stats;
    public void Start()
    {
            dialogueManager = DialogueManager.GetInstance();
           // inputManager = InputManager.GetInstance();
            //played=false;
            //InRange=false;
    }
   public void Update()
    {
         int People = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("People")).value;

       //if(!dialogueManager.dialogueIsPlaying && InRange) 
       // {
         //    dialogueManager.EnterDialogueMode(inkJSON);
         //    played=true;
      //  }
        //if(played){
            
        //     Destroy(gameObject);
       // } 
        switch(People)
        {//egg
            case 0:
                break;
            case 1:
            
               //if(!dialogueManager.dialogueIsPlaying && InRange) 
       // {
          //  dialogueManager.EnterDialogueMode(inkJSON);
         //   played=true;
      //  }
      //  if(played){
            
       //      Destroy(gameObject);
      //  } 
      K=1;
               break;
                case 2:
                Destroy(gameObject);
                break;
            default:
                break;
        }
        
    }
   // private void OnTriggerEnter2D(Collider2D collider) 
   // {
     //   if (collider.gameObject.tag == "Player")
     //   {
     //       InRange = true;
      //  }
  //  }
   public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger && !CutsceneInfo.IsViewed &&K==1)
        {
            //playerStorage.initialValue = playerPosition;
            CutsceneInfo.SetViewed(true);
            stats.Position.initialValue.x = Mathf.RoundToInt(this.transform.position.x) + 0.5f;
            stats.Position.initialValue.y = Mathf.RoundToInt(this.transform.position.y);
            //stats.LastScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(CutsceneInfo.Scene);
        }
        else
        {
            print("Already Viewed cutscene");
        }
    }
}
