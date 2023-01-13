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

    private bool pausado = false;

    private GameObject panelPausa;
    private RectTransform rectTransformPanelPausa;

    public bool temporizadorPausado = false;

    private void Awake()
    {
        totalSegundos = totalSegundosInicio;
    }

    private void Start()
    {
        temporizadorTMP = GameObject.Find("Temporizador").GetComponent<TextMeshProUGUI>();
        panelPausa = GameObject.Find("PanelPausa");
        rectTransformPanelPausa = panelPausa.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (iniciarContador)
        {
            if (!temporizadorPausado)
            {
                totalSegundos = totalSegundos - Time.deltaTime;
                actualizarTemporizador();
            }
        }
        else
        {
            totalSegundos = totalSegundosInicio;
            temporizadorTMP.text = "";
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausarDespausarJuego();
        }
    }

    public void pausarDespausarJuego()
    {
        if (!pausado)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            rectTransformPanelPausa.localScale = new Vector3(1,1,1);
            Time.timeScale = 0;
            pausado = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            rectTransformPanelPausa.localScale = new Vector3(0,0,0);
            Time.timeScale = 1;
            pausado = false;
        }
    }

    public void pausarTemporizador()
    {
        temporizadorPausado = !temporizadorPausado;
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
