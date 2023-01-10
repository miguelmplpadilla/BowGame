using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscensorController : MonoBehaviour
{
    
    public bool subiendoBajando = false;
    public GameObject puntoInicio;
    public GameObject puntoFinal;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startSubirBajar(int direccion)
    {
        subiendoBajando = true;
        StartCoroutine(subirBajar(direccion));
    }

    IEnumerator subirBajar(int direccion)
    {
        Vector3 direccionMover = puntoFinal.transform.position;
        if (direccion == 1)
        {
            direccionMover = puntoFinal.transform.position;
        }
        while (true)
        {
            Vector3.MoveTowards(transform.position, puntoFinal.transform.position, 2*Time.deltaTime);
        }
    }
}
