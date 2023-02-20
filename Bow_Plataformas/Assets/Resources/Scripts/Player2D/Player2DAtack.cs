using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2DAtack : MonoBehaviour
{

    private Player2DMovement player2DMovement;
    private Player2DAgarrarBorde agarrarBorde;
    
    private Animator animator;
    private Rigidbody2D rigidbody;

    public GameObject bala;
    public GameObject shootPoint;
    private RectTransform rectTransformBalas;

    public float velocidadDeslizarDividir = 2;
    public float bulletForce = 2;

    public int numAtaque = 1;

    public bool shoot = false;
    public bool atacando = false;
    public bool recargando = false;

    public int balas = 3;

    private void Awake()
    {
        player2DMovement = GetComponent<Player2DMovement>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        agarrarBorde = GetComponentInChildren<Player2DAgarrarBorde>();
    }

    private void Start()
    {
        rectTransformBalas = GameObject.Find("Balas").GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!shoot && !agarrarBorde.enganchado && !recargando)
        {
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

                if (!atacando)
                {
                    player2DMovement.mov = true;
                    
                    if (Input.GetButtonDown("Fire1"))
                    {
                        StopCoroutine("reiniciarAtaque");
                        animator.SetInteger("ataque", numAtaque);
                        animator.SetTrigger("atacar");
                        numAtaque++;
                        atacando = true;
                    }
                }
                else
                {
                    player2DMovement.mov = false;
                }
            }

            if (Input.GetButtonDown("Recargar"))
            {
                if (balas < 3)
                {
                    recargar();
                }
            }
        }
    }

    public void disparar()
    {
        GameObject balaInstanciada = Instantiate(bala);

        balaInstanciada.transform.position = shootPoint.transform.position;
                    
        balaInstanciada.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x, 0) * bulletForce, ForceMode2D.Impulse);

        balas--;
    }

    private void LateUpdate()
    {
        rectTransformBalas.sizeDelta = new Vector2(50 * balas, rectTransformBalas.sizeDelta.y);
    }

    public void comprobarBalas()
    {
        if (balas <= 0)
        {
            recargar();
        }
    }

    public void recargar()
    {
        animator.SetBool("run", false);
        animator.SetTrigger("reload");
        animator.SetBool("reloading", true);
        player2DMovement.mov = false;
        rigidbody.velocity = Vector2.zero;
        recargando = true;
    }

    public void setReloadFalse()
    {
        recargando = false;
        animator.SetBool("reloading", false);
    }

    public void sumarBalas()
    {
        if (balas < 3)
        {
            balas++;
        }
        else
        {
            player2DMovement.mov = true;
            setReloadFalse();
            animator.SetTrigger("stopReloading");
        }
    }

    private void FixedUpdate()
    {
        if (!player2DMovement.mov)
        {
            if (Input.GetButton("Fire2") || atacando)
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

    private IEnumerator reiniciarAtaque()
    {
        yield return new WaitForSeconds(1f);
        numAtaque = 1;
    }

    public void setNumAtaqueTo1()
    {
        numAtaque = 1;
    }

    public void startRutinaReiniciarAtaque()
    {
        StartCoroutine("reiniciarAtaque");
    }

    public void setAtacandoFalse()
    {
        atacando = false;
    }

    public void setShootFalse()
    {
        shoot = false;
    }
}
