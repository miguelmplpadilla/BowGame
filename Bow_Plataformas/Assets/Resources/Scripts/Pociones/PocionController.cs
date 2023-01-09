using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PocionController : MonoBehaviour
{
    public int tipoPocion = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tipoPocion == 1)
            {
                other.transform.localScale = other.transform.localScale*2;
            } else if (tipoPocion == 2)
            {
                other.transform.localScale = other.transform.localScale/2;
            } else if (tipoPocion == 3)
            {
                other.GetComponent<LifeController>().vida = other.GetComponent<LifeController>().fullVida;
            } else if (tipoPocion == 4)
            {
                SceneController sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
                sceneController.totalSegundos = sceneController.totalSegundos + 20;
            }
            
            Destroy(gameObject);
        }
    }
}