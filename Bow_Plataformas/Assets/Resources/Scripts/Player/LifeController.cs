using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{

    public float fullVida = 10;
    public float vida = 10;
    public float numIncrementVida = 25;

    private RectTransform barraVidaPlayer;

    private void Awake()
    {
        vida = fullVida;
    }

    void Start()
    {
        barraVidaPlayer = GameObject.Find("BarraVidaPlayer").GetComponent<RectTransform>();
    }

    
    void Update()
    {
        barraVidaPlayer.sizeDelta = new Vector2(numIncrementVida * vida, barraVidaPlayer.rect.height);
    }
}
