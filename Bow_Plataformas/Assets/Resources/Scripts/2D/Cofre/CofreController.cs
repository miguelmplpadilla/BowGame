using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreController : MonoBehaviour
{
    private BotonInteractuarController botonInteractuarController;
    private Animator animator;
    
    private GameObject player;

    public GameObject item;
    public GameObject itemPoint;

    public int numPuntos;
    public Animator puerta;

    public GameObject itemMostrar;
    public Sprite spriteItemMostrar;

    private bool cofreAbierto = false;

    private void Awake()
    {
        botonInteractuarController = GetComponentInChildren<BotonInteractuarController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameObject.Find("Player2D");

        itemMostrar.GetComponent<SpriteRenderer>().sprite = spriteItemMostrar;
    }

    public void inter(GameObject intController)
    {
        if (!cofreAbierto)
        {
            botonInteractuarController.visible();
            
            animator.SetTrigger("abrir");
            
            cofreAbierto = true;
        }
        
        player.GetComponent<Player2DMovement>().mov = true;
        player.GetComponentInChildren<Interactuar2DController>().interactuando = false;
    }

    public void throwItem()
    {
        Instantiate(item, itemPoint.transform.position, Quaternion.identity).GetComponentInChildren<ItemController>().setVariables(numPuntos, puerta);
    }

    public void interEnter()
    {
        if (!cofreAbierto)
        {
            botonInteractuarController.visible();
        }
    }

    public void interExit()
    {
        if (!cofreAbierto)
        {
            botonInteractuarController.visible();
        }
    }
}
