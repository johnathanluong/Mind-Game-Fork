using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/*
 * Custom Sprite Animator
 * Needed a new system because if we have NPCs moving around I AM NOT MAKING ANIMATION FRAMES FOR EVERY SINGLE ONE
 */
public class SpriteAnimator
{
    SpriteRenderer spriteRenderer;
    List<Sprite> frames;
    float frameRate;

    int currentFrame;
    float timer;

    public SpriteAnimator(SpriteRenderer spriteRenderer, List<Sprite> frames, float frameRate = 0.16f)
    {
        this.spriteRenderer = spriteRenderer;
        this.frames = frames;
        this.frameRate = frameRate;
    }

    public void Start()
    {
        currentFrame = 0;
        timer = 0;
        spriteRenderer.sprite = frames[0];
    }

    public void HandleUpdate()
    {
        timer += Time.deltaTime;        //how much time passes
        if(timer > frameRate)           //0.16 is the default framerate, 60fps
        {
            currentFrame = (currentFrame + 1) % frames.Count;   //loops the frames
            spriteRenderer.sprite = frames[currentFrame];
            timer -= frameRate;
        }
    }

    public List<Sprite> Frames
    {
        get { return frames; }
    }
}
