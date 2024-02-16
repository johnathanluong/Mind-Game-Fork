using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CutsceneList", menuName = "Create CutsceneList")]

public class CutsceneList : ScriptableObject
{
    [SerializeField] List<CutsceneBase> allCutscenes;

    public List<CutsceneBase> AllCutscenes
    {
        get { return allCutscenes; }
    }
}
