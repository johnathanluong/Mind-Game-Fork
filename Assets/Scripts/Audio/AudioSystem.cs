using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using Ink.Runtime;
public class AudioSystem : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private OverWorldAudio Default; //default
    [SerializeField] OverWorldAudio[] audioInfos; 
    private OverWorldAudio currentAudioInfo;
    public AudioSource audioSource;
    AudioClip soundClip=null;
    AudioClip soundOld=null;
    public float fadeTime = 2f;
   // GameObject parent;
   //private AudioClip soundClip= null;
   int Fin; 
    private string sceneName;
    int j=0; // needs to be 1 for final scene for ultra bad ending used for nothing else
    //[SerializeField] private AudioClip[] dialogueTypingSoundClips;
    // Start is called before the first frame update
    private static AudioSystem instance = null;
    private Dictionary<string, OverWorldAudio> audioInfoDictionary;
    private void Awake()
      {
           if (instance !=null)
        {
            Debug.LogWarning ("Found more than one audio Manager in the scene");
            Destroy(gameObject);
        }
        instance = this;
       // DontDestroyOnLoad(parent);
        // DontDestroyOnLoad(gameObject);
          audioSource = this.gameObject.AddComponent<AudioSource>();
          currentAudioInfo = Default;
      }

      public static AudioSystem GetInstance()
   {
    return instance;
   }
    void Start()
    {
         Debug.Log(Fin);
        //AudioClip[] dialogueTypingSoundClips = currentAudioInfo.dialogueTypingSoundClips;
        //Scene currentScene = SceneManager.GetActiveScene();
        //string sceneName = currentScene.name;
       // AudioClip soundClip= null;
      // audioSource = this.gameObject.AddComponent<AudioSource>();
        //currentAudioInfo = Default;
        InitalizeAudioInfoDictionary();
        
        //audio.GetComponent<AudioSource>();
        //StartCoroutine(FadeIn(audioSource, fadeTime));
        Fin = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Fin")).value;
    }

    // Update is called once per frame
    void Update()
    {Fin = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Fin")).value;
        GameObject[] checkThis = GameObject.FindGameObjectsWithTag("World1Mem");
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        if(audioSource.isPlaying)
        {
        return;
        }
       else  if (sceneName == "Woods1")
        {
            j=0;
           // StartCoroutine(FadeOut(audioSource, fadeTime));
             
          //  soundClip = dialogueTypingSoundClips[0];
         //  audioSource.PlayOneShot(soundClip);
           //j=0;
           // PlayOneShot(soundClip);
           //PlayMusic(0, "OverworldAudio");
        PlayMusic(.65f,0, "World1");
          // j=1;
        }
        else if (sceneName == "MainMenu" || sceneName == "GameIntroduction")
        { //StartCoroutine(FadeOut(audioSource, fadeTime));
            j=0;
            PlayMusic(.50f,0, "Menu");

        }
    
       else  if (sceneName== "World 1 Houses" )
        {j=0;
           // StartCoroutine(FadeOut(audioSource, fadeTime));
           // soundClip = dialogueTypingSoundClips[1];
            //audioSource.PlayOneShot(soundClip);
        PlayMusic(.75f,1, "World1");
        }
        else if (sceneName =="CaveWorld1" || (sceneName =="CaveOverworld"))
        {j=0;
           // StartCoroutine(FadeOut(audioSource, fadeTime));
            PlayMusic(1f,3, "World1");
        }
        else if (sceneName == "HubWorld")
        {j=0;
           // StartCoroutine(FadeOut(audioSource, fadeTime));
            PlayMusic(.75f,0, "HubWorld");
        }
        else if (sceneName == "GaryIntroduction")
        {j=0;
            //StartCoroutine(FadeOut(audioSource, fadeTime));
             PlayMusic(.75f,4, "World1");
        }
        else if (sceneName == "DefeatTutorial")
        {j=0;
           // StartCoroutine(FadeOut(audioSource, fadeTime));
             PlayMusic(.75f,5, "World1");
        }
        else if (sceneName =="TownScene")
        {j=0;
           // StartCoroutine(FadeOut(audioSource, fadeTime));
             PlayMusic(.75f,6, "World1");
        }
        else if (sceneName == "World 2")
        {j=0;
           //  StartCoroutine(FadeOut(audioSource, fadeTime));
             PlayMusic(.75f,0, "World2");
        }
         else if (sceneName == "World 2 Lab")
        {j=0;
           //  StartCoroutine(FadeOut(audioSource, fadeTime));
             PlayMusic(.75f,7, "World2");
        }
        else if (sceneName == "World 2 Houses")
        {j=0;
           // StartCoroutine(FadeOut(audioSource, fadeTime));
             PlayMusic(.75f,3, "World2");
        }
        else if (sceneName =="AfterLBossBattle")
        {j=0;
           // StartCoroutine(FadeOut(audioSource, fadeTime));
             PlayMusic(.75f,6, "World2");
        }
        else if (sceneName == "ChurchScene")
        {j=0;
            // StartCoroutine(FadeOut(audioSource, fadeTime));
             PlayMusic(.75f,2, "World2");
        }
        else if (sceneName =="LIntroduction")
        {j=0;
           // StartCoroutine(FadeOut(audioSource, fadeTime));
             PlayMusic(.75f,1, "World2");
        }
        else if (sceneName == "OpeningTheDoor")
        {j=0;
           // StartCoroutine(FadeOut(audioSource, fadeTime));
             PlayMusic(.75f,6, "World2");
        }
        else if (sceneName == "Final World")
        {j=0;
            //StartCoroutine(FadeOut(audioSource, fadeTime));
            PlayMusic(.75f,0, "HubWorld");
        }
         else if (sceneName == "Final" && Fin ==1)
        {j=0;
           // StartCoroutine(FadeOut(audioSource, fadeTime));
            PlayMusic(.75f,0, "Final");
        }
        else if (sceneName == "Final" && Fin ==2)
        {j=0;
            //StartCoroutine(FadeOut(audioSource, fadeTime));
            PlayMusic(1f,1, "Final");
        }
        else if (sceneName == "Final" && Fin ==0)
        {
            //StartCoroutine(FadeOut(audioSource, fadeTime));
            if (j==0)
            {j=1;
            //StartCoroutine(FadeOut(audioSource, fadeTime));
            PlayMusicBad(.75f,2, "Final");
            }
        }

        else
        PlayMusic(.5f,0, "Default");
    }

    private void SetCurrentAudioInfo(string id)
   {
    OverWorldAudio audioInfo = null;
    if (audioInfoDictionary != null)
        audioInfoDictionary.TryGetValue(id, out audioInfo);
    if (audioInfo !=null)
    {
        this.currentAudioInfo = audioInfo;
    }
    else
    {
        Debug.LogWarning("failed to find audio info in dictionary. id is :" + id);
    }
   }
   private void InitalizeAudioInfoDictionary()
   {
    audioInfoDictionary = new Dictionary<string, OverWorldAudio>();
    audioInfoDictionary.Add(Default.id, Default); //id is the key and scriptable object is value
    foreach (OverWorldAudio audioInfo in audioInfos)
    {
        audioInfoDictionary.Add(audioInfo.id, audioInfo);
    }
   }
   public void PlayMusic(float volume, int index, string id)
    {
        StartCoroutine(FadeIn(audioSource, fadeTime, volume));
        SetCurrentAudioInfo(id);
        soundClip= null;
        AudioClip[] SoundClips = currentAudioInfo.OverWorldAudioClip;
        //audioSource.Stop ();
        soundClip=SoundClips[index];
        audioSource.clip=soundClip;
        audioSource.loop=true;
        audioSource.Play();
       // audioSource.PlayOneShot(soundClip);
        audioSource.volume = volume;
        //StartCoroutine(FadeOut(audioSource, fadeTime));
        
    }
    public void PlayMusicBad(float volume, int index, string id)
    {
        StartCoroutine(FadeIn(audioSource, fadeTime, volume));
        SetCurrentAudioInfo(id);
        soundClip= null;
        AudioClip[] SoundClips = currentAudioInfo.OverWorldAudioClip;
        //audioSource.Stop ();
        soundClip=SoundClips[index];
        audioSource.clip=soundClip;
        audioSource.loop=false;
        audioSource.Play();
       // audioSource.PlayOneShot(soundClip);
        audioSource.volume = volume;
        //StartCoroutine(FadeOut(audioSource, fadeTime));
        
    }
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime) {
		float startVolume = audioSource.volume;
		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
			yield return null;
		}
		audioSource.Stop();
	}

	public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime, float MaxVolume) {
			audioSource.Play();
			audioSource.volume = 0f;
			while (audioSource.volume < MaxVolume) {
				audioSource.volume += Time.deltaTime / FadeTime;
				yield return null;
		}
	}
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
  
}
