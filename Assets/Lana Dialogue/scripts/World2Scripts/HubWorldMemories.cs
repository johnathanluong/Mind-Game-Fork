using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldMemories : MonoBehaviour
{
    private DialogueManager dialogueManager;
    // Start is called before the first frame update
    void Start()
    {dialogueManager= DialogueManager.GetInstance();
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
        int CoreMemory= ((Ink.Runtime.IntValue) dialogueManager.GetVariableState("CoreMemory1")).value;
        int CoreMemory2= ((Ink.Runtime.IntValue)  dialogueManager.GetVariableState("CoreMemory2")).value;
        int MachoMan2Memory= ((Ink.Runtime.IntValue)  dialogueManager.GetVariableState("MachoMan")).value;
        int MemoryFireAlarm= ((Ink.Runtime.IntValue)  dialogueManager.GetVariableState("FireAlarm")).value;
        int ArtandChaos= ((Ink.Runtime.IntValue) dialogueManager.GetVariableState("ArtandChaos")).value;
        int DogandStick= ((Ink.Runtime.IntValue)  dialogueManager.GetVariableState("DogAndStick")).value;
        int ScrapsandFailure= ((Ink.Runtime.IntValue)  dialogueManager.GetVariableState("ScrapsAndFailure")).value;
        int Smile= ((Ink.Runtime.IntValue)  dialogueManager.GetVariableState("Smile")).value;
        if(CoreMemory==1)
        {
            transform.Find("CoreMemory").gameObject.SetActive(true);
        }
        if(CoreMemory2==1)
        {
            transform.Find("CoreMemory2").gameObject.SetActive(true);
        }
        if(MachoMan2Memory==1)
        {
            transform.Find("MachoMan2Memory").gameObject.SetActive(true);
        }
        if(MemoryFireAlarm==1)
        {
            transform.Find("Memory Fire Alarm").gameObject.SetActive(true);
        }
        if(ArtandChaos==1)
        {
            transform.Find("ArtandChaos").gameObject.SetActive(true);
        }
        if(DogandStick==1)
        {
            transform.Find("Dog and Stick").gameObject.SetActive(true);
        }
        if(ScrapsandFailure==1)
        {
            transform.Find("Scraps and Failure").gameObject.SetActive(true);
        }
        if(Smile==1)
        {
            transform.Find("Smile").gameObject.SetActive(true);
        }
        }
    }
