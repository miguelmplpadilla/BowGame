using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2DHurtController : MonoBehaviour
{
    public float vida = 3;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Enemigo2DController enemigo2DController;

    private GameObject player;

    private void Awake()
    {
        rigidbody = GetComponentInParent<Rigidbody2D>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        animator = GetComponentInParent<Animator>();

        enemigo2DController = GetComponentInParent<Enemigo2DController>();
    }

    private void Start()
    {
        player = GameObject.Find("Player2D");
    }

    public void hit(float damage)
    {
        vida -= damage;

        enemigo2DController.mov = false;

        StartCoroutine("cambiarColor");

        if (vida <= 0)
        {
            animator.SetTrigger("morir");
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }

    IEnumerator cambiarColor()
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}