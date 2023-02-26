using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPistolaHurtController : MonoBehaviour
{
    public float vida = 3;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private EnemigoPistolaController enemigoPistolaController;

    private GameObject player;
    public GameObject itemDrop;
    
    public GameObject salpicaduraSangre;


    public int numPuntos = 1;
    public Animator puerta;

    public float fuerzaEmpuje = 2;
    
    private Vector2 posicionInstanciar;

    public RectTransform rectTransformBarraVida;
    public RectTransform rectTransformBarraVidaHijo;
    private Animator animatorBarraVida;
    public float numTamano;

    private void Awake()
    {
        rigidbody = GetComponentInParent<Rigidbody2D>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        animator = GetComponentInParent<Animator>();

        enemigoPistolaController = GetComponentInParent<EnemigoPistolaController>();
    }

    private void Start()
    {
        player = GameObject.Find("Player2D");

        animatorBarraVida = rectTransformBarraVida.GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        rectTransformBarraVida.sizeDelta = new Vector2(numTamano * vida, rectTransformBarraVida.sizeDelta.y);
        rectTransformBarraVidaHijo.sizeDelta = new Vector2(numTamano * vida, rectTransformBarraVidaHijo.sizeDelta.y);
    }

    public void hit(float damage)
    {
        if (!enemigoPistolaController.muerto)
        {
            posicionInstanciar = transform.position + new Vector3(0,0.2f,0);
            animatorBarraVida.SetTrigger("aparecer");
            vida -= damage;

            enemigoPistolaController.mov = false;
            
            GameObject particulaSalpicaduraSangre = Instantiate(salpicaduraSangre);
            particulaSalpicaduraSangre.transform.position = posicionInstanciar;

            StartCoroutine("cambiarColor");

            if (vida <= 0)
            {
                GameObject itemInstanciado = Instantiate(itemDrop);

                itemInstanciado.transform.position = posicionInstanciar;

                itemInstanciado.GetComponentInChildren<ItemController>().setVariables(numPuntos, puerta);
                
                
                enemigoPistolaController.muerto = true;
                enemigoPistolaController.mov = false;
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
