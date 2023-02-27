using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaCompletoController : MonoBehaviour
{
    private bool mapaMostrado = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GetComponent<Canvas>().enabled = !mapaMostrado;
            mapaMostrado = !mapaMostrado;
        }
    }
}
