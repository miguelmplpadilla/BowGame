using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlayerController : MonoBehaviour
{
    public bool saltar = true;
    private GameObject jumpingBlock;
    private GameObject player;
    
    private Animator animator;
    private PlayerController playerController;
    private CombateController combateController;
    private Rigidbody rigidbody;

    private bool dejarSaltar = false;
    private bool empezarMoverse = false;

    public float jumpingForce = 1f;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        combateController = GetComponentInParent<CombateController>();
        animator = GetComponentInParent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (saltar && !combateController.fijado)
        {
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("jump", true);
                rigidbody.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
                saltar = false;
            }
        }
        
        animator.SetBool("jump", !saltar);
    }
}
