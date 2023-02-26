using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    public float numDamage = 1;
    public GameObject particulasColision;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("HurtBoxEnemigo"))
        {
            col.SendMessage("hit", numDamage);
        }
        
        if (!col.CompareTag("Player") && !col.CompareTag("Enemigo") && !col.CompareTag("Inter") && !col.CompareTag("ItemDrop"))
        {
            Instantiate(particulasColision, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
