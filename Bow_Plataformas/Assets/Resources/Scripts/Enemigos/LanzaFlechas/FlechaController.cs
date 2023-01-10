using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FlechaController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("destruirFlecha");
    }

    IEnumerator destruirFlecha()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<LifeController>().vida--;
        }
        
        if (!other.CompareTag("LanzaFlechas") && !other.name.Equals("InteractuarController"))
        {
            Destroy(gameObject);
        }
    }
}
