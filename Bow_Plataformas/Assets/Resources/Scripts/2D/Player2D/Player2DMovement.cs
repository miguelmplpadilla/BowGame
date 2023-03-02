using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    private Animator animatorPolvo;

    private Player2DGroundController groundController;
    private Player2DHurtController hurtController;
    private Interactuar2DController interactuar2DController;
    private Player2DAtack player2DAtack;

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

        animatorPolvo = transform.Find("Polvo").GetComponent<Animator>();

        player2DAtack = GetComponent<Player2DAtack>();
    }

    void Update()
    {
        if (hurtController.variablesPlayer.vida > 0)
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
    
                if (Input.GetButton("Sprint"))
                {
                    speed = 1.2f;
                }
                else
                {
                    speed = 0.8f;
                }
                
                animator.SetFloat("horizontalVelocity", speed);
    
                if (Input.GetButtonDown("Jump"))
                {
                    if (groundController.isGrounded)
                    {
                        saltar();
                    }
                }
                
                if (groundController.isGrounded && !player2DAtack.atacando && !player2DAtack.shoot && horizontalvelocity != 0)
                {
                    if (speed > 0.8f)
                    {
                        animatorPolvo.SetBool("moviendose", true);
                    }
                    else
                    {
                        animatorPolvo.SetBool("moviendose", false);
                    }
                }
                else
                {
                    animatorPolvo.SetBool("moviendose", false);
                }
            }
            else
            {
                animatorPolvo.SetBool("moviendose", false);
            }
    
            animator.SetFloat("verticalVelocity", rigidbody.velocity.y);
            
            animator.SetBool("grounded", groundController.isGrounded);
        }
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
