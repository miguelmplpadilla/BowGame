using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;

    private Player2DGroundController groundController;
    private Player2DHurtController hurtController;
    private Interactuar2DController interactuar2DController;

    private Vector2 movement;

    public float speed = 5f;
    public float jumpForce = 2f;
    public bool mov = true;
    public bool golpeado = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        groundController = GetComponentInChildren<Player2DGroundController>();
        hurtController = GetComponentInChildren<Player2DHurtController>();

        interactuar2DController = GetComponentInChildren<Interactuar2DController>();
    }

    void Update()
    {
        if (mov && !golpeado && !interactuar2DController.interactuando)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
        
            movement = new Vector2(horizontalInput, 0f);
        
            float horizontalvelocity = movement.normalized.x * speed;

            if (horizontalvelocity != 0)
            {
                animator.SetBool("run", true);

                transform.localScale = new Vector3(movement.normalized.x, 1, 1);
            }
            else
            {
                animator.SetBool("run", false);
            }

            rigidbody.velocity =
                transform.TransformDirection(new Vector3(horizontalvelocity, rigidbody.velocity.y, 0));

            if (Input.GetButtonDown("Jump"))
            {
                if (groundController.isGrounded)
                {
                    saltar();
                }
            }
        }
        
        animator.SetFloat("verticalVelocity", rigidbody.velocity.y);
        
        animator.SetBool("grounded", groundController.isGrounded);
    }

    public void saltar()
    {
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void setMovTrue()
    {
        mov = true;
    }

    public void starRutinaSetGolpeadoFalse()
    {
        hurtController.StartCoroutine(hurtController.setGolpeadoFalse());
    }

    public void setGolpeadoMovementFalse()
    {
        golpeado = false;
    }
}
