using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombateController : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;

    private bool atacando;

    private int ataqueAnterior = 0;

    private Rigidbody rigidbody;

    public float fuerzaAtaqueImpulso = 2;

    private GameObject enemigoFijado;
    
    public bool fijado = false;
    
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (playerController.mov && playerController.hachaEquipada && !playerController.recogiendoHacha && !playerController.hachaLanzada)
        {
            if (!Input.GetButton("Fire2"))
            {
                if (!atacando)
                {
                    if (Input.GetButton("Fire1"))
                    {
                        if (playerController.speed < 1.5f)
                        {
                            int numAtaque = 0;
                            while (true)
                            {
                                numAtaque = Random.Range(1, 3 + 1);
                                if (numAtaque != ataqueAnterior)
                                {
                                    ataqueAnterior = numAtaque;
                                    break;
                                }
                            }
                            
                            animator.SetInteger("ataque", numAtaque);
                            if (enemigoFijado != null)
                            {
                                float distanciaPlayerEnemigo =
                                    Vector3.Distance(enemigoFijado.transform.position, transform.position);

                                Debug.Log(distanciaPlayerEnemigo);
                                
                                if (distanciaPlayerEnemigo > 5)
                                {
                                    rigidbody.AddForce(transform.forward * fuerzaAtaqueImpulso, ForceMode.Impulse);
                                }
                            }
                        }
                        else
                        {
                            animator.SetInteger("ataque", 4);
                        }

                        playerController.atacando = true;
                        playerController.mov = false;
                        animator.SetBool("run", false);
                        atacando = true;
                    }
                }
            }
        }

        if (Input.GetButtonDown("Fijar"))
        {
            enemigoFijado = enemigoVistaCercano();
        }

        if (fijado)
        {
            Vector3 posicionEnemigoMirar = new Vector3(enemigoFijado.transform.position.x, transform.position.y, enemigoFijado.transform.position.z);
            transform.LookAt(posicionEnemigoMirar);
        }
    }

    private GameObject enemigoVistaCercano()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        
        GameObject enemigoCercano = enemigos[0];
        float distanciaMasCercana = 100000;
        bool enemigoFijar = false;

        foreach (var enemigo in enemigos)
        {
            GetComponent<BoxCollider>().enabled = false;
            
            Vector3 direccionEnemigo = new Vector3(enemigo.transform.position.x - transform.position.x,
                enemigo.transform.position.y - transform.position.y, enemigo.transform.position.z - transform.position.z);

            Ray rayOrigin = new Ray(transform.position, direccionEnemigo);

            RaycastHit hitInfo;
            
            Physics.Raycast(rayOrigin, out hitInfo);

            Debug.DrawRay(transform.position, direccionEnemigo, Color.red);
            
            if (enemigo.GetComponent<RendererController>().visible && hitInfo.collider.tag.Equals("Enemigo"))
            {
                float distancia = Vector3.Distance(enemigo.transform.position, transform.position);

                if (distancia < distanciaMasCercana)
                {
                    distanciaMasCercana = distancia;
                    enemigoCercano = enemigo;
                }
                
                enemigoFijar = true;
            }
            
            GetComponent<BoxCollider>().enabled = true;
        }
        
        fijado = true;

        if (!enemigoFijar)
        {
            fijado = false;
            enemigoCercano = null;
        }

        return enemigoCercano;
    }

    public void setAtacandoFalse()
    {
        animator.SetInteger("ataque", 0);
        atacando = false;
    }

    public void setPlayerAtacandoFalse()
    {
        playerController.atacando = false;
        playerController.mov = true;
    }
}
