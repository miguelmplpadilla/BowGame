using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DHurtController : MonoBehaviour
{
    public float life = 10f;

    private Player2DMovement player2DMovement;
    private Player2DAtack player2DAtack;
    private Animator animator;
    private Rigidbody2D rigidbody;

    public bool golpeado = false;

    private void Awake()
    {
        player2DMovement = GetComponentInParent<Player2DMovement>();
        player2DAtack = GetComponentInParent<Player2DAtack>();
        animator = GetComponentInParent<Animator>();
        rigidbody = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("HitBoxEnemigo"))
        {
            if (!golpeado)
            {
                rigidbody.velocity = Vector2.zero;
                player2DMovement.mov = true;
                player2DMovement.golpeado = true;
                player2DAtack.atacando = false;
                player2DAtack.shoot = false;
                golpeado = true;
                
                animator.SetTrigger("hit");
                
                life -= col.GetComponent<DamageController>().damage;
            }
        }
    }

    public IEnumerator setGolpeadoFalse()
    {
        yield return new WaitForSeconds(1f);

        golpeado = false;
    }
}
