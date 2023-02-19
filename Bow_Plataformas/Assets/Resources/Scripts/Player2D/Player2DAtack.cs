using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DAtack : MonoBehaviour
{

    private Player2DMovement player2DMovement;
    private Animator animator;
    private Rigidbody2D rigidbody;

    public float velocidadDeslizarDividir = 2;

    public bool shoot = false;

    private void Awake()
    {
        player2DMovement = GetComponent<Player2DMovement>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!shoot)
        {
            Debug.Log(rigidbody.velocity);

            if (Input.GetButton("Fire2"))
            {
                animator.SetBool("point", true);
                player2DMovement.mov = false;

                if (Input.GetButtonDown("Fire1"))
                {
                    animator.SetTrigger("shoot");
                    shoot = true;
                }
            }
            else
            {
                animator.SetBool("point", false);
                player2DMovement.mov = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!player2DMovement.mov)
        {
            if (Input.GetButton("Fire2"))
            {
                if (rigidbody.velocity.x != 0)
                {
                    if (rigidbody.velocity.x > 0)
                    {
                        rigidbody.velocity = new Vector2(rigidbody.velocity.x - velocidadDeslizarDividir, rigidbody.velocity.y);
                        if (rigidbody.velocity.x < 0)
                        {
                            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                        }
                    }
                    else
                    {
                        rigidbody.velocity = new Vector2(rigidbody.velocity.x + velocidadDeslizarDividir, rigidbody.velocity.y);
                        if (rigidbody.velocity.x > 0)
                        {
                            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                        }
                    }
                }
            }
        }
    }

    public void setShootFalse()
    {
        shoot = false;
    }
}
