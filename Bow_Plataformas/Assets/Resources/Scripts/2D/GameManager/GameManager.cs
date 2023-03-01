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
        if (variablesPlayer.vida <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void LateUpdate()
    {
        variablesPlayer.actualizarImagenes();
    }
}
