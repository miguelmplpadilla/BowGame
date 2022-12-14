using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlayerController : MonoBehaviour
{
    private JumpingPlayerController jumpingPlayerController;

    private void Awake()
    {
        jumpingPlayerController = GetComponentInParent<JumpingPlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            jumpingPlayerController.saltar = true;
        }
    }
}
