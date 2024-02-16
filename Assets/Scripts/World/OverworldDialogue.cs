using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class OverworldDialogue : MonoBehaviour
{
    [SerializeField] DialogueScriptObject dialogue;

    private DialogueManager dialogueManager;
    public void Start()
    {
        if (dialogue.Played)
        {
            Destroy(gameObject);
        }
        dialogueManager = DialogueManager.GetInstance();
    }
    public void Update()
    {
        if (dialogue.Played)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !dialogueManager.dialogueIsPlaying)
        {
            StartDialogue();
        }
    }
    private void StartDialogue()
    {
        dialogue.Played = true;
        //SaveData();
        dialogueManager.EnterDialogueMode(dialogue.InkJSON);
    }

    /*private void SaveData()
    {
        SerializedObject so = new SerializedObject(dialogue);
        so.ApplyModifiedProperties();
        EditorUtility.SetDirty(dialogue);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }*/
}
