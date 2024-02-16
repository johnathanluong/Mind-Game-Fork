using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Image image;
    GameObject canvas;
    GameManager gameManager;

    private void Start()
    {
        image = GetComponent<Image>();
        gameManager = GameManager.Instance;
        canvas = transform.parent.gameObject;
    }

    public IEnumerator FadeIn(float time)
    {
        canvas = transform.parent.gameObject;
        gameManager.playerStopped = true;
        yield return image.DOFade(1f, time).WaitForCompletion();
    }

    public IEnumerator FadeOut(float time)
    {
        yield return image.DOFade(0f, time).WaitForCompletion();
        gameManager.playerStopped = false;
    }


    public void DontDestoryCanvas()
    {
        DontDestroyOnLoad(canvas);
    }
    public void DestoryCanvas()
    {
        Destroy(canvas);
    }
}
