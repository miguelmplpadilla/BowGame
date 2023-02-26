using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFinalController : MonoBehaviour
{

    private GameObject player;
    private Animator animator;

    public GameObject granada;
    public GameObject throwPoint;
    public float fuerzaGranada;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        player = GameObject.Find("Player2D");

        StartCoroutine("empezarAtacar");
    }

    void Update()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    IEnumerator empezarAtacar()
    {
        yield return new WaitForSeconds(2f);
        
        animator.SetTrigger("sacarGranada");

        yield return new WaitForSeconds(2f);
        
        animator.SetTrigger("lanzarGranada");
        
        yield return new WaitForSeconds(2f);
        
        GameObject.Find("HumoPortal").GetComponent<Animator>().SetTrigger("explotar");
    }

    public void lanzarGranada()
    {
        Vector2 direccionLanzarGranada = transform.localScale;

        GameObject granadaInstanciada = Instantiate(granada, throwPoint.transform.position, Quaternion.identity);
        
        granadaInstanciada.GetComponent<Rigidbody2D>().AddForce(direccionLanzarGranada * fuerzaGranada, ForceMode2D.Impulse);
    }
}
