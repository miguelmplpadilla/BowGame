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
    private CameraController cameraController;

    public float velocidadDeslizarDividir = 2;
    public float bulletForce = 2;

    public int numAtaque = 1;

    public bool shoot = false;
    public bool atacando = false;
    public bool recargando = false;

    public VariablesPlayer variablesPlayer;

    public GameObject casquilloBala;
    public GameObject posicionCasquilloBala;

    private Interactuar2DController interactuar2DController;

    private void Awake()
    {
        player2DMovement = GetComponent<Player2DMovement>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        agarrarBorde = GetComponentInChildren<Player2DAgarrarBorde>();
        interactuar2DController = GetComponentInChildren<Interactuar2DController>();
    }

    private void Start()
    {
        cameraController = GameObject.Find("CM").GetComponent<CameraController>();
    }

    void Update()
    {
        if (!shoot && !agarrarBorde.enganchado && !recargando && !interactuar2DController.interactuando)
        {
            if (Input.GetAxis("Fire2") > 0)
            {
                animator.SetBool("point", true);
                player2DMovement.mov = false;

                if (Input.GetAxis("Fire1") > 0)
                {
                    if (variablesPlayer.balas > 0)
                    {
                        animator.SetTrigger("shoot");
                        shoot = true;
                    }
                }
            }
            else
            {
                animator.SetBool("point", false);

                if (!atacando)
                {
                    player2DMovement.mov = true;
                    
                    if (Input.GetAxis("Fire1") > 0)
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
                if (variablesPlayer.balas < 3 && variablesPlayer.balasAlmacenadas > 0)
                {
                    recargar();
                }
            }
        }
    }

    public void shakeCamera(float amountShake)
    {
        cameraController.shakeCamera(0.1f, amountShake);
    }

    public void disparar()
    {
        GameObject balaInstanciada = Instantiate(bala);

        balaInstanciada.transform.position = shootPoint.transform.position;
                    
        balaInstanciada.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x, 0) * bulletForce, ForceMode2D.Impulse);

        variablesPlayer.restarBalas(1);
    }

    public void comprobarBalas()
    {
        if (variablesPlayer.balas <= 0 && variablesPlayer.balasAlmacenadas > 0)
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
        
        player2DMovement.mov = true;
    }

    public void sumarBalas()
    {
        if (variablesPlayer.balas < 3 && variablesPlayer.balasAlmacenadas > 0)
        {
            variablesPlayer.sumarBalas(1);
            variablesPlayer.restarBalasAlmacenadas(1);
        }
        else
        {
            setReloadFalse();
            animator.SetTrigger("stopReloading");
        }
    }

    private void FixedUpdate()
    {
        if (!player2DMovement.mov)
        {
            if (Input.GetAxis("Fire2") > 0 || atacando)
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

    public void resetTriggerDisparar()
    {
        animator.ResetTrigger("shoot");
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

    public void crearCasquilloBala()
    {
        Instantiate(casquilloBala, posicionCasquilloBala.transform.position, Quaternion.identity);
    }
}
