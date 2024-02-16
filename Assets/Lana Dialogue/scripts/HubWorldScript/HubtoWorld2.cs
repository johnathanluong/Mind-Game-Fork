using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ink.Runtime;
public class HubtoWorld2 : MonoBehaviour
{
    public PlayerStats playerStats;
    private GameManager gameManager;
    public void OnTriggerEnter2D(Collider2D collision) { 
        int Jester = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Jester")).value;
        gameManager = GameManager.Instance;
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            gameManager.clearPlayerInput();
            if (Jester==2){
                gameManager.playerStopped = true;
                gameManager.World = 1;
                if (string.IsNullOrEmpty(playerStats.SceneWorld2))
                {
                    playerStats.Position.initialValue.x = 54.5f;
                    playerStats.Position.initialValue.y = 19f;
                    gameManager.playerStopped = false;
                    SceneManager.LoadScene("LIntroduction");
                }
                else
                {
                    playerStats.Position.initialValue = playerStats.PositionWorld2.initialValue;
                    gameManager.playerStopped = false;
                    playerStats.CurScene = playerStats.SceneWorld2;
                    SceneManager.LoadScene(playerStats.SceneWorld2);
                }
            }
        }
    }
}
