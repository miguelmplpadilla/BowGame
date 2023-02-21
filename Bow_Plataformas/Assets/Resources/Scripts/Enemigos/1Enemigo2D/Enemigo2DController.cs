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

    public float speed = 2;

    private Vector2 escala;

    private Animator animator;

    private void Awake()
    {
        escala = new Vector2(1, 1);
        transform.localScale = escala;

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameObject.Find("Player2D");
    }

    void Update()
    {
        if (mov)
        {
            float distancia = Vector2.Distance(transform.position, player.transform.position);

            if (distancia < 2)
            {
                if (distancia > 0.4f)
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

    private IEnumerator atacar()
    {
        yield return new WaitForSeconds(1f);
        
        animator.SetTrigger("ataque");

        yield return new WaitForSeconds(3f);

        atacando = false;

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
}
