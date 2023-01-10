using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscensorController : MonoBehaviour
{
    
    public bool subiendoBajando = false;
    
    public GameObject puntoInicio;
    public GameObject puntoFinal;

    public GameObject puerta1;
    public GameObject puerta2;
    
    public void startSubirBajar(int direccion)
    {
        subiendoBajando = true;
        StartCoroutine(subirBajar(direccion));
    }

    IEnumerator subirBajar(int direccion)
    {
        Vector3 direccionMover;
        if (direccion == 1)
        {
            direccionMover = puntoFinal.transform.position;
            puerta2.SetActive(true);
        }
        else
        {
            direccionMover = puntoInicio.transform.position;
            puerta1.SetActive(true);
        }
        
        while (true)
        {
            float distancia = Vector3.Distance(transform.position, direccionMover);
            Debug.Log("Distancia: "+distancia);
            if (distancia > 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, direccionMover, 2*Time.deltaTime);
            }
            else
            {
                transform.position = direccionMover;
                if (direccion == 1)
                {
                    puerta1.SetActive(false);
                }
                else
                {
                    puerta2.SetActive(false);
                }

                subiendoBajando = false;
                break;
            }
            yield return null;
        }

        yield return null;
    }
}
