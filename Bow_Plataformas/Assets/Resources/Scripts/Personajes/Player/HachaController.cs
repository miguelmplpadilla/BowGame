using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HachaController : MonoBehaviour
{
    public bool lanzado = false;
    public bool hachaMano = true;
    public bool hachaCintura = false;
    public float rotateSpeed = 1;
    private GameObject player;

    private Rigidbody rigidbody;
    
    private Vector3 rotacionInicialHachaMano;
    private Vector3 posicionInicialHachaMano;
    
    private Vector3 rotacionInicialHachaCintura;
    private Vector3 posicionInicialHachaCintura;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        
        rotacionInicialHachaMano = new Vector3(-29.175f, 90, 0);
        posicionInicialHachaMano = new Vector3(0.108f, 0.09f, -0.005f);
        
        rotacionInicialHachaCintura = new Vector3(-19.983f,184.593f,-50.423f);
        posicionInicialHachaCintura = new Vector3(-0.033f,-0.053f,0.011f);
    }

    void Update()
    {
        if (lanzado)
        {
            transform.Rotate(new Vector3(0,rotateSpeed, 0) * Time.fixedDeltaTime);
        }

        if (hachaMano)
        {
            transform.localPosition = posicionInicialHachaMano;
            transform.localRotation = Quaternion.Euler(rotacionInicialHachaMano.x, rotacionInicialHachaMano.y, rotacionInicialHachaMano.z);
        }

        if (hachaCintura)
        {
            transform.localPosition = posicionInicialHachaCintura;
            transform.localRotation = Quaternion.Euler(rotacionInicialHachaCintura.x, rotacionInicialHachaCintura.y, rotacionInicialHachaCintura.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("CuboDestruir") && !other.CompareTag("CachitosCuboDestruir"))
        {
            //transform.rotation = new Quaternion(player.transform.forward.x, player.transform.forward.y, player.transform.forward.z, 1);
            rigidbody.isKinematic = true;
            lanzado = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.tag.Equals("Player") && !collision.gameObject.tag.Equals("CuboDestruir") && !collision.gameObject.tag.Equals("CachitosCuboDestruir"))
        {
            rigidbody.isKinematic = true;
            lanzado = false;
        }
    }
}
