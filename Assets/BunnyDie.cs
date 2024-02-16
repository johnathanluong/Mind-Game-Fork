using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
public class BunnyDie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int Art = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Art")).value;
        switch(Art)
        {
            case 0:
            break;
            case 1:
            gameObject.transform.position = new Vector2(-2,-50);
            break;
            default:
            gameObject.transform.position = new Vector2(-2,-50);
            break;
        }
    }
}
