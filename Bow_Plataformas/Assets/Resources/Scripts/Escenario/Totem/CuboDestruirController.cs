using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboDestruirController : MonoBehaviour
{

    private Rigidbody[] hijos;

    public bool destruido = false;

    private void Awake()
    {
        hijos = GetComponentsInChildren<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hacha") && !destruido)
        {
            if (other.GetComponent<HachaController>().lanzado)
            {
                for (int i = 0; i < hijos.Length; i++)
                {
                    hijos[i].useGravity = true;
                    hijos[i].isKinematic = false;
                }

                GetComponents<BoxCollider>()[0].enabled = false;
                GetComponents<BoxCollider>()[1].enabled = false;

                destruido = true;
                StartCoroutine("temporizadorDestruccion");
            }
        }
    }

    IEnumerator temporizadorDestruccion()
    {
        yield return new WaitForSeconds(10f);
        
        Destroy(gameObject);
    }
}
