using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private MemoryBase memoryInfo;
    public Vector2 playerPosition;
    public PlayerStats stats;

    private DialogueManager dialogueManager;
    private InputManager inputManager;
     private AudioSource Audio;
    private bool played=false; //if the scene played
    public AudioClip soundClip=null;
    // Start is called before the first frame update
    void Start()
    {
       Audio = this.gameObject.AddComponent<AudioSource>();
        dialogueManager = DialogueManager.GetInstance();
       // if (inputManager.GetInteractPressed()) 
          //  {
                inputManager = InputManager.GetInstance();
           // }
    }

    // Update is called once per frame
    void Update()
    {
        AudioS();
        if(!dialogueManager.dialogueIsPlaying && !played)
        {
             dialogueManager.EnterDialogueMode(inkJSON);
             played=true;
        }
        if(!dialogueManager.dialogueIsPlaying && played){
            List<GameObject> allObjects = SceneManager.GetActiveScene().GetRootGameObjects().ToList<GameObject>();
            for (int i = 0; i < allObjects.Count; i++)
            {
                allObjects[i].SetActive(true);
            }
            SceneManager.LoadScene(stats.CurScene);
        }
    }
    private void AudioS()
    {
        if(Audio.isPlaying)
        return;
        else{
        //Audio.audioSource = this.gameObject.AddComponent<AudioSource>();
       //StartCoroutine(AudioSystem.FadeOut(Audio.audioSource, Audio.fadeTime));
       Audio.clip=soundClip;
        Audio.loop=true;
        Audio.PlayOneShot(soundClip);
        
        }
    }
}
