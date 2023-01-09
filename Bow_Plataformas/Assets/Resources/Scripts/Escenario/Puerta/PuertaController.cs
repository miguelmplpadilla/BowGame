using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaController : MonoBehaviour
{

    private GameObject player;
    private Animator animator;

    public string scenaMover;

    public bool abrirPuertaDistancia = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }
    
    void Update()
    {
        if (abrirPuertaDistancia)
        {
            float distancia = Vector3.Distance(transform.position, player.transform.position);

            if (distancia < 2)
            {
                animator.SetBool("abrir", true);
            }
            else
            {
                animator.SetBool("abrir", false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(scenaMover);
        }
    }
}
