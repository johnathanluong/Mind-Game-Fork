using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Presets;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] PartyStats gary;
    [SerializeField] PartyStats lily;
   // [SerializeField] Preset playerPreset;
   // [SerializeField] Preset garyPreset;
   // [SerializeField] Preset lilyPreset;
    [SerializeField] List<CutsceneBase> cutsceneBases;
    [SerializeField] List<MemoryBase> memoryBases;
    [SerializeField] List<ChestData> chestDatas;
    [SerializeField] List<DialogueScriptObject> dialogueScriptObjects;
    public void resetStats()
    {
       // playerPreset.ApplyTo(stats);
       // garyPreset.ApplyTo(gary);
       // lilyPreset.ApplyTo(lily);

        for (int i = 0; i < cutsceneBases.Count; i++)
        {
            cutsceneBases[i].SetViewed(false);
        }
        for(int i = 0; i < memoryBases.Count; ++i)
        {
            memoryBases[i].SetViewed(false);
        }
        for(int i = 0; i < chestDatas.Count; ++i)
        {
            chestDatas[i].Opened = false;
        }
        for(int i = 0; i < dialogueScriptObjects.Count; ++i)
        {
            dialogueScriptObjects[i].Played = false;
        }
    }
}
