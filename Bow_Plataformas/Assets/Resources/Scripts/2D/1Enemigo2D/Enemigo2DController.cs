using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2DController : MonoBehaviour
{
    private GameObject player;

    public bool mov = true;
    private bool girando = false;
    public bool atacando = false;
    public bool muerto = false;
    public bool parring = false;
    private bool parry = false;

    public float speed = 2;
    public int numParrys = 0;

    private Vector2 escala;

    private Animator animator;
    private Enemigo2DHurtController hurtController;

    private void Awake()
    {
        escala = new Vector2(1, 1);
        transform.localScale = escala;

        animator = GetComponent<Animator>();
        hurtController = GetComponentInChildren<Enemigo2DHurtController>();
    }

    private void Start()
    {
        player = GameObject.Find("Player2D");
    }

    void Update()
    {
        if (mov && !muerto && !parring)
        {
            float distancia = Vector2.Distance(transform.position, player.transform.position);

            if (distancia < 2)
            {
                /*if (numParrys < 2)
                {
                    if (player.GetComponent<Player2DAtack>().atacando && !parry)
                    {
                        animator.SetTrigger("parry");
                        parring = true;
                        parry = true;
                    }
                }*/
                
                if (distancia > 0.3f)
                {
                    Vector2 seguirPlayer = new Vector2(player.transform.position.x, transform.position.y);
                    transform.position = Vector2.MoveTowards(transform.position, seguirPlayer, speed * Time.deltaTime);
                    
                    if (!girando)
                    {
                        if (player.transform.position.x > transform.position.x && escala.x != 1)
                        {
                            animator.SetTrigger("girar");
                            escala = new Vector2(1, 1);
                            girando = true;
                            mov = false;
                        }
                        else if (player.transform.position.x < transform.position.x && escala.x == 1)
                        {
                            animator.SetTrigger("girar");
                            escala = new Vector2(-1, 1);
                            girando = true;
                            mov = false;
                        }
                        
                        animator.SetBool("run", true);
                    }
                }
                else
                {
                    if (!atacando)
                    {
                        StartCoroutine("atacar");
                        atacando = true;
                        mov = false;
                        
                        if (!girando)
                        {
                            if (player.transform.position.x > transform.position.x && escala.x != 1)
                            {
                                StopCoroutine("atacar");
                                animator.SetTrigger("girar");
                                escala = new Vector2(1, 1);
                                girando = true;
                                mov = false;
                            }
                            else if (player.transform.position.x < transform.position.x && escala.x == 1)
                            {
                                StopCoroutine("atacar");
                                animator.SetTrigger("girar");
                                escala = new Vector2(-1, 1);
                                girando = true;
                                mov = false;
                            }
                        
                            animator.SetBool("run", true);
                        }
                    }
                    
                    animator.SetBool("run", false);
                }
            }
        }
        else
        {
            animator.SetBool("run", false);
        }
    }

    public void setParryHurtControllerTrue()
    {
        numParrys++;
        hurtController.parry = true;
    }

    public void startSetParryFalse()
    {
        parring = false;
        hurtController.parry = false;
        StartCoroutine("setParryFalse");
    }

    private IEnumerator setParryFalse()
    {
        yield return new WaitForSeconds(2f);
        parry = false;
    }

    private IEnumerator atacar()
    {
        yield return new WaitForSeconds(1f);
        
        animator.SetTrigger("ataque");

        yield return new WaitForSeconds(3f);

        atacando = false;
        mov = true;

        yield return null;
    }

    public void girarEscalaEnemigo()
    {
        transform.localScale = escala;
        girando = false;
        mov = true;
    }

    public void setMovEnemigoTrue()
    {
        mov = true;
    }

    public void destruir()
    {
        Destroy(gameObject);
    }
}
