using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private CutsceneBase CutsceneInfo;
   // public string sceneToLoad;
    public PlayerStats stats;
    GameManager gameManager;
    // public Vector2 playerPosition;
    // public VectorValue playerStorage;
    //[SerializeField] public bool Cplayed=false;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger && !CutsceneInfo.IsViewed)
        {
            //playerStorage.initialValue = playerPosition;
            gameManager.clearPlayerInput();
            CutsceneInfo.SetViewed(true);
            //SaveData();
            stats.Position.initialValue.x = Mathf.RoundToInt(this.transform.position.x) + 0.5f;
            stats.Position.initialValue.y = Mathf.RoundToInt(this.transform.position.y);
            stats.CurScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(CutsceneInfo.Scene);
        }
        else
        {
            print("Already Viewed cutscene");
        }
    }
    /*
    private void SaveData()
    {
        SerializedObject so = new SerializedObject(CutsceneInfo);
        so.ApplyModifiedProperties();
        EditorUtility.SetDirty(CutsceneInfo);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    */
}
