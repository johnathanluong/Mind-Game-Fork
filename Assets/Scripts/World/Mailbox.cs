using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mailbox : MonoBehaviour
{
    public bool isOpen;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void interactMailbox()
    {
        if(isOpen)
        {
            isOpen= false;

        }
        else
        {
            isOpen = true;
        }
        animator.SetBool("isOpen", isOpen);
    }
}
