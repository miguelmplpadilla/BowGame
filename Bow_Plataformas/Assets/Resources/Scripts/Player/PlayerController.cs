using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;
    
    private float speed = 1;
    public float fuerzaLanzamientoHacha = 1;

    private Animator animator;
    private Animator camaraAnimator;

    public bool mov = true;
    public bool saltando = false;
    public bool hachaLanzada = false;

    private GameObject hacha;
    private GameObject padreHachaMano;
    public GameObject padreHachaCintura;
    private JumpingPlayerController jumpingPlayerController;

    private GameObject camara;
    private GameObject mirilla;

    private bool recogiendoHacha = false;
    private bool puedeRecoger = false;
    private bool hachaEquipada = true;

    public int interpolationFramesCount = 60;

    private void Awake()
    {
        jumpingPlayerController = GetComponentInChildren<JumpingPlayerController>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        camaraAnimator = transform.Find("Camera").GetComponent<Animator>();
        hacha = GameObject.Find("Hacha");
        padreHachaMano = hacha.transform.parent.gameObject;
        camara = transform.Find("Camera").gameObject;
        mirilla = GameObject.Find("Mirilla");
        mirilla.SetActive(false);
    }

    
    void Update()
    {
        if (!saltando)
        {
            if (mov)
            {
                movimiento();
            }

            lanzarHacha();
        
            guardarHacha();   
        }
    }

    private void movimiento()
    {
        float axisVertical = Input.GetAxis("Vertical");
        float axisHorizontal = Input.GetAxis("Horizontal");
        
        if (axisVertical != 0 || axisHorizontal != 0)
        {
            if (axisVertical > 0)
            {
                animator.SetInteger("direccion", 0);
            }
            else
            {
                animator.SetInteger("direccion", 1);
            }

            if (axisVertical == 0)
            {
                if (axisHorizontal > 0)
                {
                    animator.SetInteger("direccion", 2);
                }
                else
                {
                    animator.SetInteger("direccion", 3);
                }
            }
            
            speed = 0.5f;

            if (axisVertical > 0)
            {
                if (Input.GetButton("Sprint"))
                {
                    speed = 1.5f;
                }
            }
            
            animator.SetBool("run", true);
            animator.SetFloat("velocidad", speed);
        }
        else
        {
            animator.SetBool("run", false);
        }

        movement = new Vector3(axisHorizontal, 0,axisVertical) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);
    }

    private void lanzarHacha()
    {
        if (hachaEquipada)
        {
            if (!recogiendoHacha)
            {
                if (!hachaLanzada)
                {
                    if (Input.GetButton("Fire2"))
                    {
                        mirilla.SetActive(true);
                        mov = false;
                        animator.SetBool("run", false);
                        animator.SetBool("pointing", true);
                        camaraAnimator.SetBool("acercar", true);
                        if (Input.GetButtonDown("Fire1"))
                        {
                            hachaLanzada = true;
                            animator.SetBool("throw", true);
                            animator.SetBool("pointing", false);
                        }
                    }
                    else
                    {
                        mirilla.SetActive(false);
                        mov = true;
                        animator.SetBool("pointing", false);
                        camaraAnimator.SetBool("acercar", false);
                    }
                }
                else
                {
                    mirilla.SetActive(false);
                    camaraAnimator.SetBool("acercar", false);
                    animator.SetBool("pointing", false);
                    if (Input.GetButtonDown("Fire1"))
                    {
                        puedeRecoger = false;
                        recogiendoHacha = true;
                        recogerHacha();
                    }
                }
            }
        }
    }

    private void guardarHacha()
    {
        if (!recogiendoHacha && !hachaLanzada)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                mov = false;
                
                camaraAnimator.SetBool("acercar", false);
                animator.SetBool("pointing", false);
                animator.SetBool("run", false);

                if (hachaEquipada)
                {
                    hachaEquipada = false;
                    animator.SetTrigger("desequipar");
                }
                else
                {
                    animator.SetTrigger("equipar");
                }
            }
        }
    }

    IEnumerator traerHacha()
    {
        Vector3 puntoInicio = hacha.transform.position;
        int elapsedFrames = 0;
        float contadorPosicionFinal = Vector3.Distance(puntoInicio, padreHachaMano.transform.position);
        float numRestar = 0;

        numRestar = (contadorPosicionFinal * 0.1f) / 5;
        
        while (true)
        {
            if (puedeRecoger)
            {
                hacha.GetComponent<HachaController>().lanzado = true;
                
                float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

                if (contadorPosicionFinal > 0)
                {
                    contadorPosicionFinal -= numRestar;
                }

                if (contadorPosicionFinal < 0)
                {
                    contadorPosicionFinal = 0;
                }

                Vector3 posicionFinal = new Vector3(padreHachaMano.transform.position.x + contadorPosicionFinal,
                    padreHachaMano.transform.position.y, padreHachaMano.transform.position.z);
            
                hacha.transform.position = Vector3.Slerp(puntoInicio, posicionFinal, interpolationRatio);
            
                elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);

                float distancia = Vector3.Distance(hacha.transform.position, padreHachaMano.transform.position);

                if (distancia < 1)
                {
                    break;
                }
            }
            yield return null;
        }

        hacha.GetComponent<Rigidbody>().velocity = Vector3.zero;

        hacha.transform.SetParent(padreHachaMano.transform);
        
        hacha.GetComponent<HachaController>().hachaMano = true;
        hacha.GetComponent<HachaController>().lanzado = false;

        //hacha.transform.localPosition = posicionInicialHacha;
        //hacha.transform.localRotation = Quaternion.Euler(rotacionInicialHacha.x, rotacionInicialHacha.y, rotacionInicialHacha.z);
        
        hachaLanzada = false;
        recogiendoHacha = false;
        
        mov = true;
        animator.SetBool("run", true);
        animator.SetBool("pointing", false);
        
        yield return null;
    }

    public void soltarHacha()
    {
        Rigidbody rigidbodyHacha = hacha.GetComponent<Rigidbody>();
        hacha.transform.SetParent(null);
        //rigidbodyHacha.useGravity = true;
        rigidbodyHacha.isKinematic = false;

        hacha.transform.rotation = Quaternion.Euler(0,0,0);
        hacha.GetComponent<HachaController>().hachaMano = false;
        rigidbodyHacha.AddForce(camara.transform.forward * fuerzaLanzamientoHacha * Time.fixedDeltaTime, ForceMode.Impulse);
        HachaController hachaController = hacha.GetComponent<HachaController>();
        hachaController.lanzado = true;

    }

    public void recogerHacha()
    {
        mov = false;
        animator.SetBool("run", false);
        animator.SetBool("pointing", true);
        StartCoroutine("traerHacha");
    }

    public void setMovTrue()
    {
        mov = true;
    }

    public void setPuedeRecogerTrue()
    {
        puedeRecoger = true;
    }

    public void setThrowFalse()
    {
        animator.SetBool("throw", false);
    }

    public void setHachaEquipadaTrue()
    {
        hachaEquipada = true;
    }

    public void setPadreHachaCintura()
    {
        hacha.GetComponent<HachaController>().hachaMano = false;
        hacha.GetComponent<HachaController>().hachaCintura = true;
        hacha.transform.SetParent(padreHachaCintura.transform);
    }
    
    public void setPadreHachaMano()
    {
        hacha.GetComponent<HachaController>().hachaMano = true;
        hacha.GetComponent<HachaController>().hachaCintura = false;
        hacha.transform.SetParent(padreHachaMano.transform);
    }

    public void startSalntando()
    {
        jumpingPlayerController.startSaltando();
    }

    public void setDejarSaltarTrue()
    {
        jumpingPlayerController.setDejarSaltarTrue();
    }

    public void setJumpBlockFalse()
    {
        animator.SetBool("jumpBlock", false);
    }
}
