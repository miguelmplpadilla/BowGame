using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{

    public float fullVida = 10;
    public float vida = 10;
    public float numIncrementVida = 25;
    public bool muerto = false;
    private PlayerController playerController;

    private Animator animator;

    private RectTransform barraVidaPlayer;

    public bool hited = false;

    private void Awake()
    {
        vida = fullVida;
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    void Start()
    {
        barraVidaPlayer = GameObject.Find("BarraVidaPlayer").GetComponent<RectTransform>();
    }

    
    void Update()
    {
        barraVidaPlayer.sizeDelta = new Vector2(numIncrementVida * vida, barraVidaPlayer.rect.height);

        if (vida <= 0)
        {
            if (!muerto)
            {
                animator.SetTrigger("muerto");
                muerto = true;
            }
        }
    }

    public void takeDamage(float damage)
    {
        if (!hited && !playerController.saltando && !playerController.atacando && !playerController.dash)
        {
            vida -= damage;
            animator.SetTrigger("hit");
            playerController.mov = false;
            hited = true;
        }
    }

    public void setHitedFalse()
    {
        hited = false;
    }
}
