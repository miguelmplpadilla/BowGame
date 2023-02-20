using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2DHurtController : MonoBehaviour
{

    public float vida = 3;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    private GameObject player;

    private void Awake()
    {
        rigidbody = GetComponentInParent<Rigidbody2D>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    private void Start()
    {
        player = GameObject.Find("Player2D");
    }

    public void hit(float damage)
    {
        vida -= damage;
        
        spriteRenderer.color = Color.red;
            
        StartCoroutine("cambiarColor");
            
        if (vida <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    IEnumerator cambiarColor()
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
