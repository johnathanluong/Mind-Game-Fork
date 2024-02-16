using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class DialogueScriptObject : ScriptableObject
{
    [SerializeField] bool played;

    [Header("Ink JSON")]
    [SerializeField] TextAsset inkJSON;

    public bool Played
    {
        get { return played; }
        set { played = value; }
    }
    public TextAsset InkJSON
    {
        get { return  inkJSON; }
    }
}
