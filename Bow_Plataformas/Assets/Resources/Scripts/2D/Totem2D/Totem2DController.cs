using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Totem2DController : MonoBehaviour
{
    private int numTeseractos = 0;
    
    public VariablesPlayer variablesPlayer;

    public GameObject[] braseros;
    public bool[] braserosEncendidos;

    private GameObject player;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    
    private BotonInteractuarController botonInteractuarController;

    public Animator animatorHumoPortal;
    public GameObject posicionPlayer;
    public GameObject posicionCamara;

    private void Awake()
    {
        braserosEncendidos = new bool[braseros.Length];

        for (int i = 0; i < braserosEncendidos.Length; i++)
        {
            braserosEncendidos[i] = false;
        }
        
        botonInteractuarController = GetComponentInChildren<BotonInteractuarController>();
    }

    private void Start()
    {
        player = GameObject.Find("Player2D");
        cinemachineVirtualCamera = GameObject.Find("CM").GetComponent<CinemachineVirtualCamera>();
    }

    public void inter(GameObject intController)
    {
        if (numTeseractos < braseros.Length)
        {
            if (variablesPlayer.numTeseractos > numTeseractos)
            {
                Debug.Log("Num teseractos: " + numTeseractos);

                for (int i = 0; i < variablesPlayer.numTeseractos; i++)
                {
                    if (!braserosEncendidos[i])
                    {
                        braseros[i].GetComponent<Animator>().SetTrigger("encender");
                        braserosEncendidos[i] = true;
                        numTeseractos++;
                    }
                }
            }
        }

        if (numTeseractos == braseros.Length)
        {
            GetComponent<Animator>().SetTrigger("convertirRojo");
        } else
        {
            player.GetComponent<Player2DMovement>().mov = true;
            player.GetComponentInChildren<Interactuar2DController>().interactuando = false;
        }
    }

    public void interEnter()
    {
        botonInteractuarController.visible();
    }

    public void interExit()
    {
        botonInteractuarController.visible();
    }

    public void encenderPortal()
    {
        player.transform.position = posicionPlayer.transform.position;
        cinemachineVirtualCamera.Follow = posicionCamara.transform;
        animatorHumoPortal.SetTrigger("explotar");
    }
}
