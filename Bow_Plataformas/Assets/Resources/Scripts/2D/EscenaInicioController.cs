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
        SceneManager.LoadScene("Escena3");
    }

    public void moverNivelesExtra()
    {
        SceneManager.LoadScene("EscenaPrincipal");
    }
}