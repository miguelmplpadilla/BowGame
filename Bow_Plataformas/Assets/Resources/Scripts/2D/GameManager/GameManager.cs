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
        Cursor.visible = false;
        
        Debug.Log("VidaPlayer: "+variablesPlayer.vida);
        Debug.Log("BalasPlayer: "+variablesPlayer.balas);
        Debug.Log("PuntosPlayer: "+variablesPlayer.puntos);
        
        variablesPlayer.inicializacion();
    }

    public void reiniciarEscena()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetString("EscenaCargar", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("LoadingScene");
    }

    private void LateUpdate()
    {
        if (!SceneManager.GetActiveScene().name.Equals("EscenaFinJuego"))
        {
            variablesPlayer.actualizarImagenes();
        }
    }
}
