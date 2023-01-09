using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public float totalSegundosInicio = 61;
    public float totalSegundos = 0;

    private TextMeshProUGUI temporizadorTMP;

    public bool iniciarContador = false;

    private void Awake()
    {
        totalSegundos = totalSegundosInicio;
    }

    private void Start()
    {
        temporizadorTMP = GameObject.Find("Temporizador").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (iniciarContador)
        {
            totalSegundos = totalSegundos - Time.deltaTime;
            actualizarTemporizador();
        }
        else
        {
            totalSegundos = totalSegundosInicio;
            temporizadorTMP.text = "";
        }
    }

    public void volverEscenaPrincipal()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("EscenaPrincipal");
    }
    
    private void actualizarTemporizador()
    {
        float minutes = Mathf.FloorToInt(totalSegundos / 60);
        float seconds = Mathf.FloorToInt(totalSegundos % 60);
        
        temporizadorTMP.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
