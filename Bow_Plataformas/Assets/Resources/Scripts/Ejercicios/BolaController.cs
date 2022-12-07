using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BolaController : MonoBehaviour
{
    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        renderer.material.color = new Color32((byte)Random.Range(1, 255),(byte)Random.Range(1, 255),(byte)Random.Range(1, 255),255);
    }
}
