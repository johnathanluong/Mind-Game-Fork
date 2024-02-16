using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    
    [SerializeField] List<GameObject> cutsceneObjects; //memory objects that are in the current scene
    [SerializeField] CutsceneList cutsceneList;

    private Cutscene[] cutscene;
    private static CutsceneManager instance;


    public List<GameObject> CutsceneObjects
    {
        get { return cutsceneObjects; }
    }
    public List<CutsceneBase> CutsceneList
    {
        get { return cutsceneList.AllCutscenes; }
    }

    public static CutsceneManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        cutscene = FindObjectsOfType<Cutscene>();
        for(int i = 0; i < cutscene.Length; ++i) 
        {
            cutsceneObjects.Add(cutscene[i].gameObject);
        }
    }

    public void resetCutscene()
    {
        foreach(CutsceneBase cut in CutsceneList)
        {
            cut.SetViewed(false);
        }
    }
}

