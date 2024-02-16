using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarneyBoss : MonoBehaviour
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
        int SceneUpdate = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Dragon")).value;
        switch(SceneUpdate)
        {
            
            case 1:
            if(!dialogueManager.dialogueIsPlaying)
            gameObject.transform.position = new Vector2(-3,112);
            break;
            //case 5:
           // gameObject.transform.position = new Vector2(150,87);
           // break;
            default:
            //AstarPath.active.Scan();
            gameObject.transform.position = new Vector2(-51,100);
            break;
        }
        
    }
}
