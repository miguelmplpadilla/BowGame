using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuenteController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Puente"))
        {
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.layer = LayerMask.GetMask("Default");
        }
    }
}
