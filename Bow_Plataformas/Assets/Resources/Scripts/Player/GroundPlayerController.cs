using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlayerController : MonoBehaviour
{
    private JumpingPlayerController jumpingPlayerController;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        jumpingPlayerController = GetComponentInParent<JumpingPlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            animator.SetBool("jump", false);
            jumpingPlayerController.saltar = true;
        }
    }
}
