using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetFightBackground : MonoBehaviour
{
    [SerializeField] GameObject fightBackgroundGO;
    [SerializeField] List<Sprite> backgrounds;

    // Start is called before the first frame update
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "CaveWorld1")
            fightBackgroundGO.GetComponent<SpriteRenderer>().sprite = backgrounds[0];
        else if (sceneName == "World 2")
            fightBackgroundGO.GetComponent<SpriteRenderer>().sprite = backgrounds[1];
        else if (sceneName == "World 2 Lab")
            fightBackgroundGO.GetComponent<SpriteRenderer>().sprite = backgrounds[2];
    }
}
