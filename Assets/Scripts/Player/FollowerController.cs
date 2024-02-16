using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class FollowerController : MonoBehaviour
{
    private CharacterAnimator animator;
    private PlayerControllerV3 playerController;
    private GameManager gameManager;

    private Vector3 targetPos;
    private bool isMoving;

    public GameObject sprite;
    public PlayerStats stats;
    public float moveSpeed;

    void Start()
    {
        gameManager = GameManager.Instance;
        transform.position = stats.Position.initialValue;
        animator = sprite.GetComponent<CharacterAnimator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerV3>();
        targetPos = transform.position;
    }

    void Update()
    {
        moveSpeed = playerController.MoveSpeed;

        FollowPlayer();
    }
    private void FollowPlayer()
    {
        if (!isMoving && gameManager.readyInput())
        {
            Vector2 playerInput = gameManager.popInput();

            animator.moveX = playerInput.x;
            animator.moveY = playerInput.y;

            targetPos = transform.position;
            targetPos.x += playerInput.x;
            targetPos.y += playerInput.y;

            animator.isMoving = isMoving;                                             //handles the animator
            
            StartCoroutine(Move(targetPos));
        }
        animator.isMoving = isMoving;
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }
}

