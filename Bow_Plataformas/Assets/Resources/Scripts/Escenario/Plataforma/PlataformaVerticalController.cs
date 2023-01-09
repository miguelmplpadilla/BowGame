using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaVerticalController : MonoBehaviour
{
    public float velocidadPlataforma = 2;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(new Vector3(rigidbody.velocity.x,velocidadPlataforma * Time.deltaTime, rigidbody.velocity.z), Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LimitePlataforma"))
        {
            rigidbody.velocity = Vector3.zero;
            if (velocidadPlataforma < 0)
            {
                velocidadPlataforma = 2;
            }
            else
            {
                velocidadPlataforma = -2;
            }
        }
        
        if (other.CompareTag("Player"))
        {
            other.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
