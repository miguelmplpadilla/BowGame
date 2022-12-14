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
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerController.mov && playerController.hachaEquipada && !playerController.recogiendoHacha && !playerController.hachaLanzada)
        {
            if (!Input.GetButton("Fire2"))
            {
                if (!atacando)
                {
                    if (Input.GetButtonDown("Fire1"))
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
                        }
                        else
                        {
                            animator.SetInteger("ataque", 4);
                        }

                        playerController.mov = false;
                        animator.SetBool("run", false);
                        atacando = true;
                    }
                }
            }
        }
    }

    public void setAtacandoFalse()
    {
        playerController.mov = true;
        animator.SetInteger("ataque", 0);
        atacando = false;
    }
}
