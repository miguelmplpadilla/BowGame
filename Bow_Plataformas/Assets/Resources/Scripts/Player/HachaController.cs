using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HachaController : MonoBehaviour
{
    public bool lanzado = false;
    public bool hachaMano = true;
    public float rotateSpeed = 1;
    private GameObject player;

    private Rigidbody rigidbody;
    
    private Vector3 rotacionInicialHacha;
    private Vector3 posicionInicialHacha;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        
        rotacionInicialHacha = new Vector3(-29.175f, 90, 0);
        posicionInicialHacha = new Vector3(0.108f, 0.09f, -0.005f);
    }

    void Update()
    {
        if (lanzado)
        {
            transform.Rotate(new Vector3(0,rotateSpeed, 0) * Time.fixedDeltaTime, Space.Self);
        }

        if (hachaMano)
        {
            transform.localPosition = posicionInicialHacha;
            transform.localRotation = Quaternion.Euler(rotacionInicialHacha.x, rotacionInicialHacha.y, rotacionInicialHacha.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            
            //transform.rotation = new Quaternion(player.transform.forward.x, player.transform.forward.y, player.transform.forward.z, 1);
            rigidbody.isKinematic = true;
            lanzado = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.tag.Equals("Player"))
        {
            rigidbody.isKinematic = true;
            lanzado = false;
        }
    }
}
