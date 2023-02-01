using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesmontarEnemigo : MonoBehaviour
{

    public Rigidbody[] rigidbodysRaghdoll;

    private void Awake()
    {
        foreach (var rigidbody in rigidbodysRaghdoll)
        {
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
