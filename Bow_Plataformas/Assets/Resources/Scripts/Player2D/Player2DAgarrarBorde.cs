using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DAgarrarBorde : MonoBehaviour
{

    private Player2DMovement player2DMovement;
    private Player2DGroundController groundController;

    private bool isBorde = false;
    private bool enganchado = false;

    private Rigidbody2D rigidbody;
    private Animator animator;

    private GameObject borde;
    
    private void Awake()
    {
        player2DMovement = GetComponentInParent<Player2DMovement>();
        groundController = transform.parent.GetComponentInChildren<Player2DGroundController>();

        rigidbody = GetComponentInParent<Rigidbody2D>();

        animator = GetComponentInParent<Animator>();
    }

    
    void Update()
    {
        if (!groundController.isGrounded && isBorde && !enganchado)
        {
            if (rigidbody.velocity.y < 0)
            {
                if (borde != null)
                {
                    transform.parent.parent = borde.transform.parent;
                    if (transform.parent.localScale.x > 0)
                    {
                        transform.parent.localPosition = new Vector3(0.616999984f, 0.289799988f, 0);
                    }
                    else
                    {
                        transform.parent.localPosition = new Vector3(0.871699989f, 0.289799988f, 0);
                    }
                }
                animator.SetBool("enganchado", true);
                rigidbody.bodyType = RigidbodyType2D.Static;
                player2DMovement.mov = false;
                enganchado = true;
            }
        }

        if (enganchado)
        {
            if (Input.GetButtonDown("Jump"))
            {
                transform.parent.parent = null;
                animator.SetBool("enganchado", false);
                rigidbody.bodyType = RigidbodyType2D.Dynamic;
                player2DMovement.mov = true;
                enganchado = false;
                player2DMovement.saltar();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Borde"))
        {
            borde = col.gameObject;
            isBorde = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Borde"))
        {
            borde = null;
            isBorde = false;
        }
    }
}
