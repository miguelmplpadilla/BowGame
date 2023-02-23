using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public VariablesPlayer variablesPlayer;

    private void Start()
    {
        Debug.Log("VidaPlayer: "+variablesPlayer.vida);
        Debug.Log("BalasPlayer: "+variablesPlayer.balas);
        Debug.Log("PuntosPlayer: "+variablesPlayer.puntos);
        
        variablesPlayer.inicializacion();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("ScenePruebaScriptableObject");
        }
    }

    private void LateUpdate()
    {
        variablesPlayer.actualizarBalas();
        variablesPlayer.actualizarVida();
        variablesPlayer.actualizarNumBalas();
    }
}
