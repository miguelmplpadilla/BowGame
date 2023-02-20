using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    public float numDamage = 1;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("HurtBoxEnemigo"))
        {
            col.SendMessage("hit", numDamage);
        }
        
        if (!col.CompareTag("Player") && !col.CompareTag("Enemigo"))
        {
            Destroy(gameObject);
        }
    }
}
