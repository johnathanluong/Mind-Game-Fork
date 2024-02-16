using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] List<Sprite> walkDownSprites;
    [SerializeField] List<Sprite> walkRightSprites;
    [SerializeField] List<Sprite> walkUpSprites;
    [SerializeField] List<Sprite> walkLeftSprites;

    [SerializeField] List<Sprite> walkDownSprites2;
    [SerializeField] List<Sprite> walkRightSprites2;
    [SerializeField] List<Sprite> walkUpSprites2;
    [SerializeField] List<Sprite> walkLeftSprites2;

    [SerializeField] List<Sprite> walkDownSprites3;
    [SerializeField] List<Sprite> walkRightSprites3;
    [SerializeField] List<Sprite> walkUpSprites3;
    [SerializeField] List<Sprite> walkLeftSprites3;
    //Parameters

    public float moveX { get; set; }
    public float moveY { get; set; }
    public bool isMoving { get; set; }

    //States

    SpriteAnimator walkUp;
    SpriteAnimator walkDown;
    SpriteAnimator walkRight;
    SpriteAnimator walkLeft;

    SpriteAnimator currentAnim;
    bool wasPrevMoving;

    //References
    SpriteRenderer spriteRenderer;

    public void Start() //3.19.23 made public so i can access from player controller
    { int Complete1 = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Y")).value;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(Complete1 ==0){
        walkUp = new SpriteAnimator(spriteRenderer, walkUpSprites);
        walkDown = new SpriteAnimator(spriteRenderer, walkDownSprites);
        walkLeft = new SpriteAnimator(spriteRenderer, walkLeftSprites);
        walkRight = new SpriteAnimator(spriteRenderer, walkRightSprites);

        currentAnim = walkDown;
        }
        if(Complete1 ==1){
        walkUp = new SpriteAnimator(spriteRenderer, walkUpSprites2);
        walkDown = new SpriteAnimator(spriteRenderer, walkDownSprites2);
        walkLeft = new SpriteAnimator(spriteRenderer, walkLeftSprites2);
        walkRight = new SpriteAnimator(spriteRenderer, walkRightSprites2);

        currentAnim = walkDown;
        }
        if(Complete1 ==2)
        {
        walkUp = new SpriteAnimator(spriteRenderer, walkUpSprites3);
        walkDown = new SpriteAnimator(spriteRenderer, walkDownSprites3);
        walkLeft = new SpriteAnimator(spriteRenderer, walkLeftSprites3);
        walkRight = new SpriteAnimator(spriteRenderer, walkRightSprites3);

        currentAnim = walkDown;
        }
    }

    private void Update()
    {
        var prevAnim = currentAnim;
        
        if (moveX == 1)
            currentAnim = walkRight;
        else if(moveX == -1)
            currentAnim = walkLeft;
        else if(moveY == 1)
            currentAnim = walkUp;
        else if(moveY == -1)
            currentAnim = walkDown;

        if(currentAnim != prevAnim || isMoving != wasPrevMoving)
        {
            currentAnim.Start();
        }

        if (isMoving)
            currentAnim.HandleUpdate();
        else
            spriteRenderer.sprite = currentAnim.Frames[0];
        

        wasPrevMoving = isMoving;
    }
}
