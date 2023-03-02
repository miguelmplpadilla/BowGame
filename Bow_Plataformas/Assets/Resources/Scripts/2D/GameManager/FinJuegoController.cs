using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinJuegoController : MonoBehaviour
{
    public void volverMenuInicio()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetString("EscenaCargar", "MenuInicio2D");
        SceneManager.LoadScene("LoadingScene");
    }
}
