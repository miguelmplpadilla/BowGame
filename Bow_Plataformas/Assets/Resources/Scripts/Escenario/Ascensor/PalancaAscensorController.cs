using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaAscensorController : MonoBehaviour
{
    
    public int subirBajar = -1;
    private AscensorController ascensorController;

    private void Start()
    {
        ascensorController = transform.parent.GetComponent<AscensorController>();
    }

    private void Update()
    {
        if (ascensorController.subiendoBajando)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }

    public void inter()
    {
        if (!ascensorController.subiendoBajando)
        {
            if (subirBajar > 0)
            {
                subirBajar = -1;
            }
            else
            {
                subirBajar = 1;
            }

            ascensorController.startSubirBajar(subirBajar);
        }
    }
}
