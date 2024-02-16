using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.SceneManagement;
public class InkExternalFunctions : MonoBehaviour
{
    public Vector2 playerPosition;
    public Vector2 playerPosition2;
    public VectorValue playerStorage;
    public GameManager gameManager;
    private DialogueManager dialogueManager;
    private InputManager inputManager;
    public bool InRange=false;
    public bool played=false;
    string IDt;
    void Start()
    {
        
        gameManager = GameManager.Instance;
        dialogueManager = DialogueManager.GetInstance();
        
    }

    void Update()
    {
       if(dialogueManager.dialogueIsPlaying && InRange) 
        {
            if(IDt=="Woods1")
            {
              playerStorage.initialValue = playerPosition;
             gameManager.World=0;
            }
            else if(IDt=="World 2")
            {
            playerStorage.initialValue = playerPosition2;
             gameManager.World=1;
            }
        }
    }
    public void Bind (Story story)
    {
        story.BindExternalFunction("SceneTeleport", (string ID) => SceneTransition(ID));
    
    }
    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("SceneTeleport");
    }
    public void SceneTransition(string ID)
    {
        if (ID != null)
    {
        //gameManager.World=0;
        if(ID=="Woods1")
        {IDt="Woods1";
        SceneManager.LoadScene(ID);
        }
        if(ID=="World 2")
        {IDt="World 2";
        SceneManager.LoadScene(ID);
        }
    }
    else{
        Debug.LogWarning("tried to teleport but not a valid teleport");
    }
    }
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            InRange = true;
        }
    }
    

}
