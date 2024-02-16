using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBoss : MonoBehaviour
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
            case 0:
            break;
            case 1:
            break;
            case 2:
            break;
            case 3:
            break;
            case 4:
            gameObject.transform.position = new Vector2(103.5f,93);
            break;
            case 5:
            gameObject.transform.position = new Vector2(146,87);
            break;
            default:
            transform.position = new Vector2(146,87);
            break;
        }
        
    }
}
