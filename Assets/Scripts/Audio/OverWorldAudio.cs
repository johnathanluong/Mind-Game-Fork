using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OverWorldAudioInfo", menuName = "ScriptableObjects/OverWorldAudio", order = 1)]
public class OverWorldAudio : ScriptableObject
{
     public string id;

    [SerializeField] public AudioClip[] OverWorldAudioClip;
}