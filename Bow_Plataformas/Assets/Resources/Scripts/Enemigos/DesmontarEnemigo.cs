using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesmontarEnemigo : MonoBehaviour
{

    public GameObject padeDesmontar;
    public GameObject padreSecundario;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            desmontar(padeDesmontar);
        }
    }

    private void desmontar(GameObject objetoDesmontar)
    {
        foreach (var objeto in padeDesmontar.GetComponentsInChildren<Rigidbody>())
        {
            objeto.isKinematic = false;
            objeto.useGravity = true;
            objeto.transform.parent = padreSecundario.transform;
            desmontar(objeto.gameObject);
        }
    }
    
}
