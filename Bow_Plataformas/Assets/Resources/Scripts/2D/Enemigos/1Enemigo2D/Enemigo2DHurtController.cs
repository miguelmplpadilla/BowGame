using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemigo2DHurtController : MonoBehaviour
{
    public float vida = 3;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Enemigo2DController enemigo2DController;

    private GameObject player;
    public GameObject itemDrop;
    
    public GameObject salpicaduraSangre;
    public GameObject manchaSangre;


    public int numPuntos = 1;
    public Animator puerta;

    public bool parry = false;

    public float fuerzaEmpuje = 2;
    
    private Vector2 posicionInstanciar;

    private void Awake()
    {
        rigidbody = GetComponentInParent<Rigidbody2D>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        animator = GetComponentInParent<Animator>();

        enemigo2DController = GetComponentInParent<Enemigo2DController>();
    }

    private void Start()
    {
        player = GameObject.Find("Player2D");
    }

    public void hit(float damage)
    {
        if (!enemigo2DController.muerto && !parry)
        {
            posicionInstanciar = transform.position + new Vector3(0,0.2f,0);
            
            vida -= damage;

            enemigo2DController.mov = false;
            enemigo2DController.parring = false;
            parry = false;
            
            GameObject particulaSalpicaduraSangre = Instantiate(salpicaduraSangre);
            particulaSalpicaduraSangre.transform.position = posicionInstanciar;

            StartCoroutine("cambiarColor");

            if (vida <= 0)
            {
                GameObject itemInstanciado = Instantiate(itemDrop);
                GameObject manchaSangreInstanciada = Instantiate(manchaSangre);
                
                itemInstanciado.transform.position = posicionInstanciar;
                manchaSangreInstanciada.transform.position = posicionInstanciar;

                itemInstanciado.GetComponentInChildren<ItemController>().setVariables(numPuntos, puerta);
                
                
                enemigo2DController.muerto = true;
                enemigo2DController.mov = false;
                animator.SetTrigger("morir");
            }
            else
            {
                animator.SetTrigger("hit");
            }
        }
        else
        {
            Vector2 direccionFuerza = Vector2.zero;
            if (player.transform.position.x > transform.position.x)
            {
                direccionFuerza = Vector2.left;
            }
            else
            {
                direccionFuerza = Vector2.right;
            }
            
            rigidbody.AddForce(direccionFuerza * fuerzaEmpuje * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    IEnumerator cambiarColor()
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}