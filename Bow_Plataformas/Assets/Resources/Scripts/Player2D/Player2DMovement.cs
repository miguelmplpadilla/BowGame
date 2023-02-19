using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;

    private Player2DGroundController groundController;

    private Vector2 movement;

    public float speed = 5f;
    public float jumpForce = 2f;
    public bool mov = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        groundController = GetComponentInChildren<Player2DGroundController>();
    }

    void Update()
    {
        if (mov)
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
}
