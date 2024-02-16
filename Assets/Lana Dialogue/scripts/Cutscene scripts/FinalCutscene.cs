using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ink.Runtime;
public class FinalCutscene : MonoBehaviour
{
    [SerializeField] private CutsceneBase CutsceneInfo;
   // public string sceneToLoad;
    public PlayerStats stats;
   // public Vector2 playerPosition;
   // public VectorValue playerStorage;
    //[SerializeField] public bool Cplayed=false;
    int Fin = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Fin")).value;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger && !CutsceneInfo.IsViewed &&Fin!=0 )
        {
            //playerStorage.initialValue = playerPosition;
            CutsceneInfo.SetViewed(true);
            stats.Position.initialValue.x = Mathf.RoundToInt(this.transform.position.x) + 0.5f;
            stats.Position.initialValue.y = Mathf.RoundToInt(this.transform.position.y);
            stats.CurScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(CutsceneInfo.Scene);
        }
        else
        {
            print("Already Viewed cutscene");
        }
    }
}
