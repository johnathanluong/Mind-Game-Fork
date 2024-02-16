using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionHub : MonoBehaviour
{

    public string sceneToLoad;
    public Vector2 playerPosition;
    public PlayerStats stats;
    private GameManager gameManager;
    Fader fader;
    GameObject parent;

    private void Start()
    {
        gameManager = GameManager.Instance;
        fader = FindObjectOfType<Fader>();
        parent = transform.parent.gameObject;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        fader = FindObjectOfType<Fader>();
        parent = transform.parent.gameObject;
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            stats.Position.initialValue = playerPosition;
            stats.CurScene = sceneToLoad;
            StartCoroutine(SwitchScene());
        }
    }

    IEnumerator SwitchScene()
    {
        DontDestroyOnLoad(parent);
        gameManager.clearPlayerInput();
        fader.DontDestoryCanvas();
        gameManager.World =-1; 
        yield return fader.FadeIn(0.5f);
        SceneManager.LoadScene(sceneToLoad);
        yield return fader.FadeOut(0.5f);

        fader.DestoryCanvas();
        Destroy(parent);
    }
}
