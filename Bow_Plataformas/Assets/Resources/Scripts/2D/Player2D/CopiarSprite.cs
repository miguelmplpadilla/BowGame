using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopiarSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRendererPadre;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRendererPadre = transform.parent.GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        spriteRenderer.sprite = spriteRendererPadre.sprite;
    }
}
