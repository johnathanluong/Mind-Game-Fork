using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;

    SpriteAnimator animator;
     
    void Start()
    {
        animator = new SpriteAnimator(GetComponent<SpriteRenderer>(), sprites);
        animator.Start();
    }

    void Update()
    {
        animator.HandleUpdate();   
    }
}
