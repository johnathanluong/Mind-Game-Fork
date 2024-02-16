using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{

    //Parameters
    [SerializeField] List<Sprite> walkRightSprites;
    [SerializeField] List<Sprite> walkLeftSprites;

    public float moveX;
    public bool isMoving;

    //States
    SpriteAnimator walkRight;
    SpriteAnimator walkLeft;

    SpriteAnimator currentAnim;
    bool wasPrevMoving;

    //References
    SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        walkLeft = new SpriteAnimator(spriteRenderer, walkLeftSprites);
        walkRight = new SpriteAnimator(spriteRenderer, walkRightSprites);

        currentAnim = walkLeft;
    }

    private void Update()
    {
        var prevAnim = currentAnim;

        if (moveX == 1)
            currentAnim = walkRight;
        else if (moveX == -1)
            currentAnim = walkLeft;

        if (currentAnim != prevAnim || isMoving != wasPrevMoving)
        {
            currentAnim.Start();
        }

        if (isMoving)
            currentAnim.HandleUpdate();
        else
            spriteRenderer.sprite = currentAnim.Frames[0];


        wasPrevMoving = isMoving;
    }

    public SpriteRenderer SpriteRenderer
    {
        get { return spriteRenderer; }
    }
}
