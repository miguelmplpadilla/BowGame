using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaInicioController : MonoBehaviour
{
    public void cerrarJuego()
    {
        Application.Quit();
    }

    public void moverEscenaJuego()
    {
        PlayerPrefs.SetString("EscenaCargar", "Escena3");
        SceneManager.LoadScene("LoadingScene");
    }

    public void moverNivelesExtra()
    {
        PlayerPrefs.SetString("EscenaCargar", "EscenaPrincipal");
        SceneManager.LoadScene("LoadingScene");
    }
}