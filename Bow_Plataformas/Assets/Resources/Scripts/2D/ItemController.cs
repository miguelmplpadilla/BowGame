using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public string idItem = "";
    public int numItems = 1;

    public Animator puerta;

    public int numAnimacion = 0;

    public VariablesPlayer variablesPlayer;

    public void setVariables(int num, Animator p)
    {
        numItems = num;
        puerta = p;
        
        GetComponentInParent<Animator>().SetInteger("numAnimacion", numAnimacion);
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
                puerta.SetBool("abierta", true);
            }
            
            Destroy(transform.parent.gameObject);
        }
    }
}
