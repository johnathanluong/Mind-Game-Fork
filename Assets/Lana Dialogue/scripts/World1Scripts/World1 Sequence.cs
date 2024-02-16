using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World1Sequence : MonoBehaviour
{
    private DialogueManager dialogueManager;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = DialogueManager.GetInstance();
        transform.Find("PuzzleGuyIngameScene Defeat").gameObject.SetActive(false);
       // for (int i = 0; i < transform.childCount; i++)
           //  {
          //       transform.GetChild(i).gameObject.SetActive(false);
          //   }
              //transform.position = new NewVector3(103,93,.45);
    }

    // Update is called once per frame
    void Update()
    {
        int SceneUpdate = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("People")).value;
        switch(SceneUpdate)
        {
             case 0:
             transform.Find("PuzzleGuyIngameScene Defeat").gameObject.SetActive(false);
              break;
             case 1:
             transform.Find("PuzzleGuyIngameScene Defeat").gameObject.SetActive(true);
              break;
             case 2:
            // if(!dialogueManager.dialogueIsPlaying)
             transform.Find("PuzzleGuyIngameScene Defeat").gameObject.SetActive(false);
              break;
             //transform.GetChild(3).gameObject.SetActive(true);
             case 3:
             transform.Find("PuzzleGuyIngameScene Defeat").gameObject.SetActive(false);
              break;

             default:
              transform.Find("PuzzleGuyIngameScene Defeat").gameObject.SetActive(false);
              break;
        }
    }
}
