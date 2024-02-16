using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DeleteDragon : MonoBehaviour
{private DialogueManager dialogueManager;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = DialogueManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        int People = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Dragon")).value;

        switch(People)
        {
            case 0:
                break;
            case 1:
            //AstarPath.active.Scan();
            if(!dialogueManager.dialogueIsPlaying)
                gameObject.SetActive(false);
                break;
            case 2:
               // gameObject.SetActive(true);
                break;
                case 3:
           // gameObject.transform.position = new Vector2(-2,150);
          // AstarPath.active.Scan();
           gameObject.SetActive(false);
                break;
            default:
            //AstarPath.active.Scan();
            Destroy(gameObject);
                break;
        }
    }
}
