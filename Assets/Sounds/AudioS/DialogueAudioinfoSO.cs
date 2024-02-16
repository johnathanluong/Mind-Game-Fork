using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueAudioInfo", menuName = "ScriptableObjects/DialogueAudioInfoSO", order = 1)]
public class DialogueAudioinfoSO : ScriptableObject
{
    public string id;

    [SerializeField] public AudioClip[] dialogueTypingSoundClips;
    [Range(1, 5)]
    [SerializeField] public int frequencylevel = 2; //default frequency
    [Range(-3, 3)]
    [SerializeField] public float minPitch = 0.5f; //min pitch
    [Range(-3, 3)]
    [SerializeField] public float maxPitch = 3f; //max pitch
    [SerializeField] public bool stopAudioSource; //for if u want audio to stop im just stupid and put it here
}
