using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Cutscenes", menuName = "Create Cutscenes")]
public class CutsceneBase : ScriptableObject
{
    [SerializeField] string CutsceneName;
    [SerializeField] string scene;
    [SerializeField] bool isViewed;

    [TextArea]
    [SerializeField] string description;
    // Start is called before the first frame update
    public string Name
    {
        get { return CutsceneName; }
    }
    public string Scene
    {
        get { return scene; }
    }

    public string Description
    {
        get { return description; }
    }
    public bool IsViewed
    {
        get { return isViewed; }
    }
    public void SetViewed(bool val)
    {
        isViewed = val;
    }
}
