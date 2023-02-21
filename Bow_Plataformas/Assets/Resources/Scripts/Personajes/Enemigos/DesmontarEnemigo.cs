using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesmontarEnemigo : MonoBehaviour
{

    public Rigidbody[] rigidbodysRaghdoll;

    public float porcentageMasaRigidbodysRaghdoll = 100;

    private void Awake()
    {
        foreach (var rigidbody in rigidbodysRaghdoll)
        {
            float masaActual = rigidbody.mass;

            float masaFinal = (masaActual * porcentageMasaRigidbodysRaghdoll) / 100;

            rigidbody.mass = masaFinal;
            
            rigidbody.GetComponent<Collider>().enabled = false;
            rigidbody.isKinematic = true;
        }
    }

    public void desmontar()
    {
        GetComponent<Animator>().enabled = false;
        foreach (var rigidbody in rigidbodysRaghdoll)
        {
            rigidbody.GetComponent<Collider>().enabled = true;
            rigidbody.isKinematic = false;
        }
    }
    
}
