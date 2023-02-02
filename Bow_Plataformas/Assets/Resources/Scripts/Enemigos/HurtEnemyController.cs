using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemyController : MonoBehaviour
{
    private int vida = 3;
    private DesmontarEnemigo desmontarEnemigo;

    public bool muerto = false;

    private void Awake()
    {
        desmontarEnemigo = transform.parent.GetComponent<DesmontarEnemigo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HitBoxPlayer"))
        {
            vida--;

            if (vida <= 0)
            {
                if (!muerto)
                {
                    gameObject.tag = "Untagged";
                    transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);
                    desmontarEnemigo.desmontar();
                    muerto = true;
                }
            }
            
            transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(other.transform.forward * 2, ForceMode.Impulse);
        }
    }
}
