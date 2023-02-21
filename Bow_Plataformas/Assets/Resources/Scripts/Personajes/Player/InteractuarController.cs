using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuarController : MonoBehaviour
{

    public bool interactuable = false;
    
    [SerializeField]
    private GameObject objetoInteractuable;

    private void Update()
    {
        if (interactuable)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                objetoInteractuable.SendMessage("inter");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Inter"))
        {
            objetoInteractuable = other.gameObject;
            interactuable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Inter"))
        {
            objetoInteractuable = null;
            interactuable = false;
        }
    }
}
