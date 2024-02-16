using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ink.Runtime;
//using UnityEditor.Tilemaps;

public class HubtoWorld3 : MonoBehaviour
{
    Fader fader;
    GameObject parent;
    public string sceneToLoad;
    public PlayerStats stats;
    public Vector2 playerPosition;
    private GameManager gameManager;

    
    private void Start()
    {
        fader = FindObjectOfType<Fader>();
        parent = transform.parent.gameObject;
    }
    public void OnTriggerEnter2D(Collider2D collision) { 
        int Jester = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Jester")).value;
        gameManager = GameManager.Instance;
        gameManager.clearPlayerInput();

        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            gameManager.clearPlayerInput();

            if(Jester==3)
            {   
                gameManager.playerStopped = true;
                stats.Position.initialValue = playerPosition;
                stats.CurScene = sceneToLoad;
                gameManager.playerStopped = false;
                SceneManager.LoadScene(sceneToLoad);
            }
            }
        }
        IEnumerator SwitchScene()
    {
        DontDestroyOnLoad(parent);

        fader.DontDestoryCanvas();

        yield return fader.FadeIn(0.5f);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        yield return fader.FadeOut(0.5f);

        fader.DestoryCanvas();
        Destroy(parent);
    }
}

