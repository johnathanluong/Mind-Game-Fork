using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCBoss : MonoBehaviour
{
    private DialogueManager dialogueManager;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = DialogueManager.GetInstance();
        //parent = transform.gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        int SceneUpdate = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Sequence")).value;
        switch(SceneUpdate)
        {
            
            case 8:
            //if(!dialogueManager.dialogueIsPlaying)
            gameObject.transform.position = new Vector2(174.5f,25);
            break;
            case 5:
            gameObject.transform.position = new Vector2(174.5f,25);
            break;
            default:
            gameObject.transform.position = new Vector2(150,87);
            break;
        }
        
    }
}
