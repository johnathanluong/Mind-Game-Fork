using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class World2Sequence : MonoBehaviour
{
    private DialogueManager dialogueManager;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = DialogueManager.GetInstance();
        for (int i = 0; i < transform.childCount; i++)
             {
                 transform.GetChild(i).gameObject.SetActive(false);
             }
              //transform.position = new NewVector3(103,93,.45);
               
               
    }

    // Update is called once per frame
    void Update()
    {   Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        int SceneUpdate = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Sequence")).value;
        switch(SceneUpdate)
        {
             case 0:
             transform.Find("LIntroduction").gameObject.SetActive(true);
              break;
             case 1:
             if(!dialogueManager.dialogueIsPlaying){
             transform.Find("LIntroduction").gameObject.SetActive(false);
             transform.Find("Before Big Area Left").gameObject.SetActive(true);
             transform.Find("Before Big Area bottom").gameObject.SetActive(true);
             }
              break;
             case 2:
             if(!dialogueManager.dialogueIsPlaying){
             transform.Find("Before Big Area Left").gameObject.SetActive(false);
             transform.Find("Before Big Area bottom").gameObject.SetActive(false);
             }
              break;
             //transform.GetChild(3).gameObject.SetActive(true);
             case 3:
             if(!dialogueManager.dialogueIsPlaying){
             transform.Find("Before L Boss Battle").gameObject.SetActive(true);
             }
              break;
             case 4:
             //transform.GetChild(5).gameObject.SetActive(false);
             if(!dialogueManager.dialogueIsPlaying){
             //transform.Find("Before L Boss Battle").gameObject.SetActive(false);
            //transform.Find("LBossBattle").position = new Vector2(103,93);
             //transform.GetChild(5).gameObject.SetActive(false);
             }
              break;
             case 5:
             if(!dialogueManager.dialogueIsPlaying){
             //transform.Find("LBossBattle").gameObject.SetActive(false);
             //transform.Find("LBossBattle").position = new Vector2(146,87);
             if(sceneName =="World 2 Lab"){
             transform.Find("Ground Top (1)").gameObject.SetActive(true);
             }
             transform.Find("After L Boss Battle Cutscene").gameObject.SetActive(true);
             }
              break;
             case 6:
             if(!dialogueManager.dialogueIsPlaying){
             //transform.Find("After L Boss Battle Cutscene").gameObject.SetActive(false);
             if(sceneName =="World 2 Lab")
             transform.Find("Ground Top (1)").gameObject.SetActive(true);
             transform.Find("Opening the Door").gameObject.SetActive(true);
             }
              break;
             case 7:
             if(!dialogueManager.dialogueIsPlaying){
             //transform.Find("Opening the Door").gameObject.SetActive(false);
             if(sceneName =="World 2 Lab")
             transform.Find("Ground Top (1)").gameObject.SetActive(false);
             transform.Find("Before MC Boss Battle").gameObject.SetActive(true);
             }
              break;
             case 8:
             //if(!dialogueManager.dialogueIsPlaying){
             //transform.Find("Before MC Boss Battle").gameObject.SetActive(false);
            // transform.Find("MCBossBattle").position = new Vector2(173,25);
            // }
              break;
             case 9:
             if(!dialogueManager.dialogueIsPlaying){
            //transform.Find("MCBossBattle").position = new Vector2(300,300);
             transform.Find("After MC Boss Battle").gameObject.SetActive(true);
             transform.Find("Memory 4").gameObject.SetActive(true);
             }
              break;
              case 10:
              transform.Find("Scene Transition to HubWorld").gameObject.SetActive(true);
              break;
             default:
              for (int i = 0; i < transform.childCount; i++)
             {
                 transform.GetChild(i).gameObject.SetActive(false);
             }
              break;
        }
    }
}
