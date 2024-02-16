using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutscenePicture2 : MonoBehaviour
{

[SerializeField] AudioClip soundClip;
private AudioSource audioSource;
private int play =0;
    // Start is called before the first frame update
    void Start()
    {
         transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        audioSource = this.gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       int SceneChange = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("tem")).value;
        switch(SceneChange)
        {
            case 0:
          //transform.GetChild(1).gameObject.SetActive(false);
          transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 1:
            transform.GetChild(0).gameObject.SetActive(false);
            if (play==0){
            audioSource = this.gameObject.AddComponent<AudioSource>();
            //audioSource.Clip = soundClip;
            audioSource.PlayOneShot(soundClip);
            play=1;
            }
            transform.GetChild(1).gameObject.SetActive(true);
                break;
            default :
                break;
        }
    }
}
