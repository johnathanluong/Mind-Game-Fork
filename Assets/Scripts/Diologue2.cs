using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Diologue2 : MonoBehaviour
{
    public GameObject dialoguePanel; //reference entire panel
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    
    public float wordSpeed;
    public bool playerIsClose;
    public int currentLine;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose ){
            if(dialoguePanel.activeInHierarchy)
            {
                if(dialogueText.text == dialogue[currentLine])
                NextLine();
            }
            else {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
                
            }
            
        }

        
    }


    public void zeroText()
    { 
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        currentLine=0;
        
    }

    IEnumerator Typing(){
        dialogueText.text = "";
        foreach(char letter in dialogue[currentLine]){
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

        public void NextLine() {
        if(currentLine < dialogue.Length-1) {
            currentLine++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else{
            zeroText();
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            playerIsClose = false;
            zeroText();
        }
    }
}