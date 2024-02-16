using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public string sceneToLoad;
    public Vector2 playerPosition;
    public PlayerStats stats;

    Fader fader;
    GameObject parent;
    GameManager gameManager;

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
        gameManager.clearPlayerInput();
        DontDestroyOnLoad(parent);

        fader.DontDestoryCanvas();

        yield return fader.FadeIn(0.5f);
        SceneManager.LoadScene(sceneToLoad);
        yield return fader.FadeOut(0.5f);

        fader.DestoryCanvas();
        Destroy(parent);
    }
}
