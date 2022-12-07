using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TotemController : MonoBehaviour
{
    public Color[] colores;
    public GameObject bola;

    private void Awake()
    {
        GetComponent<Renderer>().material.color = colores[Random.Range(0, colores.Length)];
    }

    private void OnMouseDown()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(bola, transform.position + (Vector3.up * 2), Quaternion.identity);
        }
    }
}
