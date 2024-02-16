using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [Header("Dialogue UI")]
    [Header("Choices UI")]
    //smaller the value is below the faster the dialogue
    [SerializeField] private float typingSpeed = 0.04f;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject[] choices; ///reference to the button
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private GameObject continueIcon; //for the continue icon (current white blk cause lazy)

    //below are tags for the speakers and such for the ink files
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string AUDIO_TAG = "audio";

    //for sound for dialogue
    [Header("Audio")]
    [SerializeField] private DialogueAudioinfoSO defaultAudioInfo;//default if nothing is selected for an npc
    [SerializeField] DialogueAudioinfoSO[] audioInfos; //configurations that isnt default
    //made the COMMENTED STUFF BELOW THIS. into individual scripted objects to change for npcs
    //[SerializeField] private AudioClip[] dialogueTypingSoundClips;

    //[Range(1, 5)]
    //[SerializeField] private int frequencylevel = 2; //default frequency
    //[Range(-3, 3)]
    //[SerializeField] private float minPitch = 0.5f; //min pitch
    //[Range(-3, 3)]
    //[SerializeField] private float maxPitch = 3f; //max pitch
    //[SerializeField] private bool stopAudioSource; //for if u want audio to stop im just stupid and put it here
    //for hashcode predictable lines approach
    [SerializeField] private bool makePredictable;
    private AudioSource audioSource;
    private DialogueAudioinfoSO currentAudioInfo;
    private Dictionary<string, DialogueAudioinfoSO> audioInfoDictionary;
    
    //variable changing parameters
    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;
    private DialogueVariables dialogueVariables;

    private Animator layoutAnimator;
    private Coroutine displayLineCoroutine; //this is for stoping mutliple lines appearing at one


    private bool canContinueToNextLine= false; //make it to where the line must finish before the player can be big dumb and skip shit
    private TextMeshProUGUI[] choicesText;
    public bool dialogueIsPlaying {get; private set; } //wierd thing makes read only to outside scripts
    public bool dialogueCompleted { get; private set; }
    private Story currentStory;
    private static DialogueManager instance; //do wierd thing to this as well if i want to do get instance method
    // Start is called before the first frame update
    private InkExternalFunctions inkExternalFunctions;
   private void Awake()
   {
    
        if (instance !=null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        DontDestroyOnLoad(gameObject);
        audioSource = this.gameObject.AddComponent<AudioSource>();
        inkExternalFunctions = new InkExternalFunctions();
        currentAudioInfo = defaultAudioInfo;
        
        //inputManager = InputManager.GetInstance();
   }

   public static DialogueManager GetInstance()
   {
    return instance;
   }

   private void Start()
   {
    dialogueIsPlaying = false;
    dialoguePanel.SetActive(false);
    transform.SetAsLastSibling();
    layoutAnimator = dialoguePanel.GetComponent<Animator>();

    choicesText = new TextMeshProUGUI[choices.Length];
    int index = 0;
    foreach(GameObject choice in choices)
    {
        choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
        index++;
    }

    InitalizeAudioInfoDictionary();
   }

   private void SetCurrentAudioInfo(string id)
   {
    DialogueAudioinfoSO audioInfo = null;
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
    audioInfoDictionary = new Dictionary<string, DialogueAudioinfoSO>();
    audioInfoDictionary.Add(defaultAudioInfo.id, defaultAudioInfo); //id is the key and scriptable object is value
    foreach (DialogueAudioinfoSO audioInfo in audioInfos)
    {
        audioInfoDictionary.Add(audioInfo.id, audioInfo);
    }
   }
   private void Update()
   {
    //return right away if dialogue isnt playing
    if(!dialogueIsPlaying)
    {
        return;
    }
    //otherwise check for player input
    if ( canContinueToNextLine && currentStory.currentChoices.Count==0 && InputManager.GetInstance().GetSubmitPressed())
    {
        ContinueStory();
    }

   }
   public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine) 
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            // NOTE: The below two lines were added to fix a bug after the Youtube video was made
            InputManager.GetInstance().RegisterSubmitPressed(); // this is specific to my InputManager script
            ContinueStory();
        }
    }


   public void EnterDialogueMode(TextAsset inkJSON)
   {
    currentStory = new Story(inkJSON.text);
    dialogueIsPlaying = true;
    dialoguePanel.SetActive(true);
    dialogueCompleted = false;

    //for variable changes
    dialogueVariables.StartListening(currentStory);
    inkExternalFunctions.Bind(currentStory);
    //currentStory.BindExternalFunction("SceneTeleport", (string ID) => { Debug.Log(ID);});
    //reset the dialogue modes to default
    displayNameText.text = "???";
    if (portraitAnimator != null)
        portraitAnimator.Play("default");
    if (layoutAnimator != null)
        layoutAnimator.Play("Default");

    ContinueStory();
   }

   public IEnumerator ExitDialogueMode()
   {
    yield return new WaitForSeconds(0.2f);
    //stop listening to variable changes
    dialogueVariables.StopListening(currentStory);
    inkExternalFunctions.Unbind(currentStory);
    //currentStory.UnbindExternalFunction("SceneTeleport");
    dialogueCompleted = true;
    dialogueIsPlaying = false;
    dialoguePanel.SetActive(false);
    dialogueText.text = "";
    dialogueVariables.SaveVariables(); //doing this here will persist variables between scenes

    //go back to default audio config
    SetCurrentAudioInfo(defaultAudioInfo.id);
   }

    private IEnumerator DisplayLine(string line)
    {
        //clear current text
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters=0;
        HideChoices();
        continueIcon.SetActive(false);
        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;
        //loop through each character in line
        foreach(char letter in line.ToCharArray())
        {
            //for line below if the player wants to skip to the end of the line and press submit button then add line below
            if (InputManager.GetInstance().GetSubmitPressed())
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break; //this will beark out of foreach loop
            }

            //this will check for fancy text tags in ink
            if(letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                //won't wait at all from letter to next so all letters in that tag displays properly
               // dialogueText.text +=letter;
                if (letter =='>')
                {
                    isAddingRichTextTag = false;
                }
            }
            //if false then just display test normally
            else
            {
                PlayDialogueSound(dialogueText.maxVisibleCharacters, dialogueText.text[dialogueText.maxVisibleCharacters]); //for frequency and playing audio
                dialogueText.maxVisibleCharacters++;
                //this will wait a split second before going to next letter
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        continueIcon.SetActive(true);
        //if choices, then this will display the choices. Do this if u want dialogue to appear first and then buttons
        DisplayChoices();
        canContinueToNextLine= true;
    }

    private void PlayDialogueSound(int currentDisplayedCharacterCount, char currentCharacter)
    {
        //this sets the variables for configuration of noise in dialogue

        AudioClip[] dialogueTypingSoundClips = currentAudioInfo.dialogueTypingSoundClips;
        int frequencylevel = currentAudioInfo.frequencylevel;
        float minPitch = currentAudioInfo.minPitch;
        float maxPitch = currentAudioInfo.maxPitch;
        bool stopAudioSource = currentAudioInfo.stopAudioSource;

        //for frequency changes in the dialogue noise. if set %2 does every 2 cjaracters, %3 3 and so on
        if (currentDisplayedCharacterCount % frequencylevel == 0)
        {
            if (stopAudioSource)//if we set this to true then after each letter the noise stops. atm i have it off
                {
                    audioSource.Stop();
                }
                AudioClip soundClip= null;
                //make predictable audio with hashing
                if (makePredictable)
                {
                    int hashCode = currentCharacter.GetHashCode();
                    //sound clip mod division
                    int predictableIndex= hashCode % dialogueTypingSoundClips.Length;
                    soundClip = dialogueTypingSoundClips[predictableIndex];
                    //pitch
                    int minPitchInt = (int) (minPitch * 100);
                    int maxPitchInt = (int) (maxPitch * 100);
                    int pitchRangeInt = maxPitchInt - minPitchInt;
                    //cannont divide 0
                    if (pitchRangeInt != 0)
                    {
                        int predictablePitchInt = (hashCode % pitchRangeInt) + minPitchInt;
                        float predictablePitch = predictablePitchInt / 100f;
                        audioSource.pitch = predictablePitch;
                    }
                    else
                    {
                        audioSource.pitch = minPitch;
                    }
                }
                else{
                //rrandom sound clip
                int randomIndex = Random.Range(0, dialogueTypingSoundClips.Length);
                soundClip = dialogueTypingSoundClips[randomIndex];
                //now random value for pitch inclusively
                audioSource.pitch = Random.Range(minPitch, maxPitch);
                }
                //now audio for every character in a line
                audioSource.volume=.75f; //added 4.25.23 if we want to make sound quieter well here is this
                audioSource.PlayOneShot(soundClip);
        }
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }
   private void ContinueStory()
   {
    if (currentStory.canContinue)
    {
        //lines below will call the typing out of the dialogue
        if (displayLineCoroutine != null) //need this if not if its null error will incure
        {
            StopCoroutine(displayLineCoroutine);
        }
        string nextLine = currentStory.Continue();
        //handle external functions being last line
        if (nextLine.Equals("") && !currentStory.canContinue)
        {
            StartCoroutine(ExitDialogueMode());
        }
        else
        {
        //this will handle the tags
        HandleTags(currentStory.currentTags);
        displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
        //set text for current dialogue. this line sets ui text a string. we will call coroutine instead of the line below to show the typing effect instead
        //dialogueText.text = currentStory.Continue();
        //if choices, then this will display the choices. keep this is want to display while text is playing
        //DisplayChoices();
        }
    }
    else
    {
        StartCoroutine(ExitDialogueMode());
    }
   }

   private void HandleTags(List<string> currentTags)
   {
    //loop for each tag
    foreach (string tag in currentTags)
    {
        //parse tag
        string[] splitTag = tag.Split(':');
        if (splitTag.Length !=2)
        {
            Debug.LogError("tag no bueno not parsed correctly. should be 2 for this:" + tag);
        }
        string tagKey = splitTag[0].Trim();
        string tagValue = splitTag[1].Trim(); //trim for whitespace
        //now switch statement to route the tag key
    bool j= false;
        switch (tagKey)
        {
            case SPEAKER_TAG:
              
                displayNameText.text = tagValue; //Debug.Log("speaker" + tagValue);
                
                break;

            case PORTRAIT_TAG:
            
                portraitAnimator.Play(tagValue); //Debug.Log("portrait" + tagValue);
                break;

            case LAYOUT_TAG:

                layoutAnimator.Play(tagValue); //Debug.Log("Layout" + tagValue);
                break;

            case AUDIO_TAG:
            
                SetCurrentAudioInfo(tagValue);
                break;

            default:
                Debug.LogWarning("Tag came in but not being handled" + tag);
                break;
        }
    }
   }

   private void DisplayChoices()
   {
    List<Choice> currentChoices = currentStory.currentChoices;
    
    //if more choices then available this prints
    if (currentChoices.Count > choices.Length)
    {
        Debug.LogError("More choices given than the UI can support. Number of choices given" + currentChoices.Count);
    }
    //now to loop through the choices
    int index = 0;
    //enable and initalize the choice objects in the ui
    foreach(Choice choice in currentChoices)
    {
        choices[index].gameObject.SetActive(true);
        choicesText[index].text = choice.text;
        index++;
        //this loop allows ui and choices to be in sink.
    }
    //hide choices currently not in use
    for (int i= index; i< choices.Length; i++)
    {
        choices[i].gameObject.SetActive(false);
    }
        StartCoroutine(SelectFirstChoice());
   }

   private IEnumerator SelectFirstChoice()
   {
    //event system requires clear first then wait at least one frame
    EventSystem.current.SetSelectedGameObject(null);
    yield return new WaitForEndOfFrame();
    EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
   }

    public Ink.Runtime.Object GetVariableState(string variableName) 
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null) 
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

//public void SetVariableState(string variableName, Ink.Runtime.Object variableValue) 
   // {
      //  if (dialogueVariables.variables.ContainsKey(variableName)) 
      //  {
      //      dialogueVariables.variables.Remove(variableName);
      //      dialogueVariables.variables.Add(variableName, variableValue);
      //  }
      //  else 
       // {
        //    Debug.LogWarning("Tried to update variable that wasn't initialized by globals.ink: " + variableName);
       // }
 //   }
//save current state of all global variables
    //public void OnApplicationQuit()
   // {
    //    dialogueVariables.SaveVariables();
   // }
}
