using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombateController : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;
    
    public bool atacando;

    private int ataqueAnterior = 0;

    private Rigidbody rigidbody;

    public float fuerzaAtaqueImpulso = 2;

    public GameObject enemigoFijado;
    
    public bool fijado = false;
    public int numVecesFijado = 0;

    private Camera camara;

    public float velocidadAcercarsePersonaje = 2;
    
    private int numAtaque = 0;
    private Vector3 movement;
    private Quaternion rotacionPlayer;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        camara = GameObject.Find("Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (playerController.mov && playerController.hachaEquipada && !playerController.recogiendoHacha && !playerController.hachaLanzada)
        {
            if (!Input.GetButton("Fire2"))
            {
                if (!atacando)
                {
                    if (playerController.axisVertical >= 0)
                    {
                        if (Input.GetButtonDown("Fire1"))
                        {
                            if (playerController.speed < 1.5f || playerController.axisHorizontal != 0)
                            {
                                numAtaque = 0;
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
                                    
                                    if (distanciaPlayerEnemigo > 0.5f)
                                    {
                                        //rigidbody.AddForce(transform.forward * fuerzaAtaqueImpulso, ForceMode.Impulse);
                                        StartCoroutine("acercarAEnemigo");
                                    }
                                }

                                playerController.speed = 0.5f;
                                playerController.atacando = true;
                                playerController.mov = false;
                                animator.SetBool("run", false);
                                atacando = true;
                            }
                            else
                            {
                                if (playerController.axisVertical > 0)
                                {
                                    numAtaque = 4;
                                    animator.SetInteger("ataque", 4);
                                    playerController.dash = true;
                                    movement = new Vector3(0, 0,0.015f);
                                    
                                    playerController.atacando = true;
                                    playerController.mov = false;
                                    animator.SetBool("run", false);
                                    atacando = true;
                                }
                            }

                            
                        }
                    }
                }
            }
        }

        if (numAtaque == 4 && playerController.dash)
        {
            
            if (enemigoFijado != null)
            {
                float distanciaEnemigo =
                    Vector3.Distance(enemigoFijado.transform.position, transform.position);

                if (distanciaEnemigo < 0.5f)
                {
                    playerController.dash = false;
                }
            }
            
            transform.Translate(movement, Space.Self);
        }

        if (Input.GetButtonDown("Fijar"))
        {
            if (!fijado)
            {
                enemigoFijado = enemigoVistaCercano(null);
            }
            else
            {
                if (numVecesFijado == 0)
                {
                    StartCoroutine("tiempoNumVecesFijado");
                }

                numVecesFijado++;

                if (numVecesFijado > 1)
                {
                    //Enemigo Vista Cercano, ReFijar
                    GameObject enemigoReFijado = enemigoVistaCercano(enemigoFijado);

                    if (enemigoReFijado != null)
                    {
                        enemigoFijado = enemigoReFijado;
                    }
                    
                    numVecesFijado = 0;
                }
            }
        }

        if (fijado)
        {
            Vector3 posicionEnemigoMirar = new Vector3(enemigoFijado.transform.position.x, transform.position.y, enemigoFijado.transform.position.z);
            transform.LookAt(posicionEnemigoMirar);

            rotacionPlayer = transform.rotation;

            if (enemigoFijado.GetComponentInChildren<HurtEnemyController>().muerto || Input.GetKeyDown(KeyCode.R))
            {
                enemigoFijado = null;
                camara.GetComponent<PlayerCamara>().mouseX = rotacionPlayer.y;
                camara.GetComponent<Animator>().enabled = true;
                camara.GetComponent<Animator>().SetBool("fijado", false);
                fijado = false;
            }
        }
    }

    IEnumerator tiempoNumVecesFijado()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("NumVecesFijado a 0");
        numVecesFijado = 0;
    }

    IEnumerator acercarAEnemigo()
    {

        while (true)
        {
            Vector3 posicionAcercarEnemigo = new Vector3(enemigoFijado.transform.position.x, transform.position.y, enemigoFijado.transform.position.z);
            float disranciaAEnemigo = Vector3.Distance(transform.position, posicionAcercarEnemigo);
            transform.position = Vector3.MoveTowards(transform.position, posicionAcercarEnemigo, velocidadAcercarsePersonaje * Time.deltaTime);

            if (disranciaAEnemigo < 0.5f)
            {
                break;
            }
            
            yield return null;
        }
        
        yield return null;
    }

    private GameObject enemigoVistaCercano(GameObject ignorar)
    {
        List<GameObject> enemigos = GameObject.FindGameObjectsWithTag("Enemigo").ToList();

        if (ignorar != null)
        {
            enemigos.Remove(ignorar);
        }
        
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

            bool enemigoVisible = RendererController.isVisibleFrom(enemigo.GetComponentInParent<Renderer>(), camara);
            
            if (enemigoVisible && hitInfo.collider.tag.Equals("Enemigo"))
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
        else
        {
            camara.GetComponent<Animator>().SetBool("fijado", true);
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
