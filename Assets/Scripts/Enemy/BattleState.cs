using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
//using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.SceneManagement.SceneHierarchyHooks;

[CreateAssetMenu(fileName = "BattleState")]
public class BattleState : ScriptableObject
{
    [TextArea]
    [SerializeField] string useDescription;
    [SerializeField] PartyStats ally;
    [SerializeField] CutsceneBase townScene;
    [SerializeField] DialogueScriptObject townScene2;

    [SerializeField] List<Enemy> enemyList = new List<Enemy>();

    GameManager gameManager;
    public GameObject battleCanvas;

    public PartyStats Ally
    {
        get { return ally; }
        set { ally = value; }
    }
    public List<Enemy> EnemyList
    {
        get { return enemyList; }
    }
    public void EndBattle()
    {
        EnemyList.Clear();
        List<GameObject> allObjects = SceneManager.GetActiveScene().GetRootGameObjects().ToList<GameObject>();
        for (int i = 0; i < allObjects.Count; i++)
        {
            allObjects[i].SetActive(true);
        }
    }

    public void LostBattle()
    {
        EndBattle();
        gameManager = GameManager.Instance;
        PlayerStats playerStats = gameManager.PlayerStats;
        if (gameManager.World == 0)
        {
            playerStats.CurHP = playerStats.MaxHP;
            playerStats.CurMP = playerStats.MaxMP;
            if (ally != null)
            {
                ally.CurHP = ally.MaxHP;
                ally.CurMP = ally.MaxMP;
            }

            for (int i = 0; i < gameManager.PartyStats.Count; ++i)
            {
                gameManager.PartyStats[i].CurHP = gameManager.PartyStats[i].MaxHP;
                gameManager.PartyStats[i].CurMP = gameManager.PartyStats[i].MaxMP;
            }
            
            //SaveData(playerStats);

            if (townScene.IsViewed)
            {
                if (playerStats.Money >= 5)
                {
                    playerStats.Money -= 5;
                }
                else
                {
                    playerStats.Money = 0;
                }

                playerStats.Position.initialValue.x = 91.5f;
                playerStats.Position.initialValue.y = 44f;
                //SaveData(playerStats.Position);
                SceneManager.LoadScene("World 1 Houses");
            }
            else
            {
                playerStats.Position.initialValue.x = -7.5f;
                playerStats.Position.initialValue.y = -2f;
                //SaveData(playerStats.Position);
                SceneManager.LoadScene("Woods1");
            }
        }
        else if(gameManager.World == 1)
        {
            playerStats.CurHP = playerStats.MaxHP;
            playerStats.CurMP = playerStats.MaxMP;
            if (ally != null)
            {
                ally.CurHP = ally.MaxHP;
                ally.CurMP = ally.MaxMP;
            }
            
            for (int i = 0; i < gameManager.PartyStats.Count; ++i)
            {
                gameManager.PartyStats[i].CurHP = gameManager.PartyStats[i].MaxHP;
                gameManager.PartyStats[i].CurMP = gameManager.PartyStats[i].MaxMP;
            }

            if (townScene2.Played)
            {
                if (playerStats.Money >= 15)
                {
                    playerStats.Money -= 15;
                }
                else
                {
                    playerStats.Money = 0;
                }

                //SaveData(playerStats);

                playerStats.Position.initialValue.x = 91.5f;
                playerStats.Position.initialValue.y = 42f;
                //SaveData(playerStats.Position);
                SceneManager.LoadScene("World 2 Houses");
            }
            else
            {
                if (playerStats.Money >= 10)
                {
                    playerStats.Money -= 10;
                }
                else
                {
                    playerStats.Money = 0;
                }

                playerStats.Position.initialValue.x = 39.5f;
                playerStats.Position.initialValue.y = 15f;
                SceneManager.LoadScene("World 2");
            }

            
            
        }
    }
    /*
    private void SaveData(ScriptableObject thing)
    {
        SerializedObject so = new SerializedObject(thing);
        so.ApplyModifiedProperties();
        EditorUtility.SetDirty(thing);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    */
}
