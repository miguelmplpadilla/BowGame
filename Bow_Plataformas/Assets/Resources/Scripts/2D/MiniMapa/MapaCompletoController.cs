using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaCompletoController : MonoBehaviour
{
    private bool mapaMostrado = false;
    void Update()
    {
        if (Input.GetButtonDown("Map"))
        {
            GetComponent<Canvas>().enabled = !mapaMostrado;
            mapaMostrado = !mapaMostrado;
        }
    }
}
