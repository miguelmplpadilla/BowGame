using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public float damage = 1;
    public bool destruir = false;

    public bool bala;

    public GameObject particulasColisionBala;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (bala)
        {
            Instantiate(particulasColisionBala, transform.position, Quaternion.identity);
        }
        
        if (col.CompareTag("HitBoxPlayer") && destruir)
        {
            Destroy(gameObject);
        }
    }
}
