using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
        {
            if (col.CompareTag("Enemigo"))
            {
                col.SendMessage("hit");
            }

            Destroy(gameObject);
        }
    }
}
