using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconoMiniMapaController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Color color;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.color = color;
    }
}
