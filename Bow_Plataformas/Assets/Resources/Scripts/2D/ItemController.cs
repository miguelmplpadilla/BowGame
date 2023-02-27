using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public string idItem = "";
    public int numItems = 1;

    public Animator[] puertas;

    public int numAnimacion;

    public VariablesPlayer variablesPlayer;

    private void Awake()
    {
        GetComponentInParent<Animator>().SetInteger("numAnimacion", numAnimacion);
    }

    public void setVariables(int num, Animator[] p)
    {
        numItems = num;
        puertas = p;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (idItem.Equals("punto"))
            {
                variablesPlayer.sumarPuntos(numItems);
            } else if (idItem.Equals("llave"))
            {
                for (int i = 0; i < puertas.Length; i++)
                {
                    puertas[i].SetBool("abierta", true);
                }
            } else if (idItem.Equals("bala"))
            {
                variablesPlayer.sumarBalasAlmacenadas(numItems);
            } else if (idItem.Equals("teseracto"))
            {
                variablesPlayer.sumarTeseracto(numItems);
            }

            Destroy(transform.parent.gameObject);
        }
    }
}
