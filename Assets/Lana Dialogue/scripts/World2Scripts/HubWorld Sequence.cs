using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldSequence : MonoBehaviour
{
    private DialogueManager dialogueManager;
    // Start is called before the first frame update
    void Start()
    {
       // dialogueManager = DialogueManager.GetInstance();
        for (int i = 0; i < transform.childCount; i++)
             {
                 transform.GetChild(i).gameObject.SetActive(false);
             }
              //transform.position = new NewVector3(103,93,.45);
    }

    // Update is called once per frame
    void Update()
    {
        int SceneUpdate = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Y")).value;
        switch(SceneUpdate)
        {
             case 0:
             transform.Find("Original Collision Objects").gameObject.SetActive(true);
             transform.Find("Original Ground Top").gameObject.SetActive(true);
             transform.Find("Original Ground").gameObject.SetActive(true);
              break;
             case 1:
             //if(!dialogueManager.dialogueIsPlaying){
             transform.Find("Mid Collision Objects").gameObject.SetActive(true);
             transform.Find("Mid Ground Top").gameObject.SetActive(true);
             transform.Find("Mid Ground").gameObject.SetActive(true);
             
              break;
             case 2:
             transform.Find("Final Collision Objects").gameObject.SetActive(true);
             transform.Find("Final Ground Top").gameObject.SetActive(true);
             transform.Find("Final Ground").gameObject.SetActive(true);
              break;
             default:
              break;
        }
        }
    }
