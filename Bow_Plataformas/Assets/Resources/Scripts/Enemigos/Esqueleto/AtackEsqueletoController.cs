using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackEsqueletoController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<LifeController>().takeDamage(1);
        }
    }
}
