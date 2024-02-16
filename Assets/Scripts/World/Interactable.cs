using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
        
    private InputManager inputManager;
    private GameManager gameManager;

    private void Start()
    {
        inputManager = InputManager.GetInstance();
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && !gameManager.PlayerStats.IsMoving)
        {
            if (inputManager.GetInteractPressed())
            {
                interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange= true;
            collision.gameObject.GetComponent<PlayerManager>().NotifyPlayer();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            collision.gameObject.GetComponent<PlayerManager>().DenotifyPlayer();
        }
    }
}
