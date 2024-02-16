using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DeleteSpriteAfterDialogue : MonoBehaviour
{
    
   // public Animation anim;
    public Animator Ex;
    public bool played=false;
    public bool InRange=false;
    
   // private DialogueManager dialogueManager;
    private InputManager inputManager;
    private AudioSource Audio;
    public AudioClip soundClip;
    public AudioClip soundClip2;
    int CompleteTutorial;
    public void Start()
    {
            //dialogueManager = DialogueManager.GetInstance();
           // anim = this.gameObject.AddComponent<Animation>();
           Audio =this.gameObject.AddComponent<AudioSource>();
          // anime.animation
           
           Ex = gameObject.GetComponent<Animator>();
            Audio.clip = soundClip;
           //  Ex.SetBool("Explosion",true);
           //Ex.SetBool("Explosion",false);
           //Ex.SetBool("Die",false);
           // inputManager = InputManager.GetInstance();
            //played=false;
            //InRange=false;
    }
   public void Update()
    {
        CompleteTutorial = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Complete")).value;

        switch(CompleteTutorial)
        {
            case 0:
          //  gameObject.SetActive(false);
                break;
            case 1:
          // gameObject.SetActive(true);
         // Ex.SetBool("Explosion",true);
          Ex.SetBool("Explosion",true);
       // Debug.Log("Here"+CompleteTutorial);
        Audio.clip = soundClip;
        if(!played){
        Audio.PlayOneShot(soundClip);
        // Audio.clip = soundClip2;
       // Audio.PlayOneShot(soundClip2);
      //  Audio.PlayOneShot(soundClip2);
      // Audio.PlayOneShot(soundClip2);
      // Audio.PlayOneShot(soundClip2);
      // Audio.PlayOneShot(soundClip2);
      // Audio.PlayOneShot(soundClip2);
        played=true;
        }
        //gameObject.SetActive(false);
        //egg
            //if(!dialogueManager.dialogueIsPlaying)
               // Destroy(gameObject);
              // Ex.SetBool("Explosion",false);
                break;
            case 2:
           // if(!dialogueManager.dialogueIsPlaying)
            //    Destroy(gameObject);
                break;
            case 3:
            //gameObject.SetActive(true);
            
          //  if(!dialogueManager.dialogueIsPlaying)
            
          //  if(dialogueManager.dialogueIsPlaying)
        Ex.SetBool("Explosion",true);
       // Debug.Log("Here"+CompleteTutorial);
        Audio.clip = soundClip;
        if(!played){
        Audio.PlayOneShot(soundClip);
       // Audio.clip = soundClip2;
       // Audio.PlayOneShot(soundClip2);
       // Audio.PlayOneShot(soundClip2);
       // Audio.PlayOneShot(soundClip2);
       // Audio.PlayOneShot(soundClip2);
       // Audio.PlayOneShot(soundClip2);
        //Audio.PlayOneShot(soundClip2);
        played=true;
        }
                
           // if(!dialogueManager.dialogueIsPlaying)
              //  Destroy(gameObject);
                break;
            default:
          //  if(!dialogueManager.dialogueIsPlaying)
              //  Destroy(gameObject);
                break;

        }
    }
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            InRange = true;
        }
    }
    private void explode()
    {
       // anim.Play();
       //Ex = gameObject.GetComponent<Animator>();
       //Audio =this.gameObject.AddComponent<AudioSource>();
        Ex.SetBool("Explosion",true);
       // Debug.Log("Here"+CompleteTutorial);
        Audio.clip = soundClip;
        if(!played){
        Audio.PlayOneShot(soundClip);
       // Audio.clip = soundClip2;
        //Audio.PlayOneShot(soundClip2);
        //Audio.PlayOneShot(soundClip2);
        //Audio.PlayOneShot(soundClip2);
        //Audio.PlayOneShot(soundClip2);
       // Audio.PlayOneShot(soundClip2);
        //Audio.PlayOneShot(soundClip2);
        played=true;
        }
       // gameObject.SetActive(false);
       
    }
}
