using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemyController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HitBoxPlayer"))
        {
            transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(other.transform.forward * 2, ForceMode.Impulse); 
        }
    }
}
