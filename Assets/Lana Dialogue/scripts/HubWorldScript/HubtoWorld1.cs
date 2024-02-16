using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ink.Runtime;
public class HubtoWorld1 : MonoBehaviour
{

    //public string sceneToLoad;
    //public string sceneToLoad2;
    //public Vector2 playerPosition;
    //public Vector2 playerPosition2;
    public PlayerStats playerStats;
    private GameManager gameManager;
    void Start() 
    {
         gameManager = GameManager.Instance;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    { int CompleteTutorial = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Complete")).value;
     int Jester = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Jester")).value;
        if(collision.CompareTag("Player") && !collision.isTrigger)
        { 
            if(Jester!=0)
            {
                gameManager.playerStopped = true;
                gameManager.World=0;
                gameManager.clearPlayerInput();
                if (string.IsNullOrEmpty(playerStats.SceneWorld1))
                {
                    playerStats.Position.initialValue.x = 12.5f;
                    playerStats.Position.initialValue.y = 28f;
                    gameManager.playerStopped = false;
                    playerStats.CurScene = "CaveWorld1";
                    SceneManager.LoadScene("CaveWorld1");
                }
                else
                {
                    playerStats.Position.initialValue = playerStats.PositionWorld1.initialValue;
                    gameManager.playerStopped = false;
                    playerStats.CurScene = playerStats.SceneWorld1;
                    SceneManager.LoadScene(playerStats.SceneWorld1);
                }    
            }
        }
    }
}