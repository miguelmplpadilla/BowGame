using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzaFlechasController : MonoBehaviour
{

    public GameObject flecha;
    public GameObject puntoLanzamiento;

    public float fuerzaFlecha = 2;
    public float tiempoLanzamientoFlechas = 5f;

    void Start()
    {
        StartCoroutine("lanzarFlecha");
    }

    IEnumerator lanzarFlecha()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoLanzamientoFlechas);
            GameObject flechaInstanciada = Instantiate(flecha);
            flechaInstanciada.transform.position = puntoLanzamiento.transform.position;
            flechaInstanciada.GetComponent<Rigidbody>().AddForce(transform.forward * fuerzaFlecha, ForceMode.Impulse);
        }
    }
}
