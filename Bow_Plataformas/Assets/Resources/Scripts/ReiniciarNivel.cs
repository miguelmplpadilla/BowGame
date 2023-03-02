using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarNivel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 1;
            PlayerPrefs.SetString("EscenaCargar", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
