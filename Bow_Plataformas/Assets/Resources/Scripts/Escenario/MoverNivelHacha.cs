using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoverNivelHacha : MonoBehaviour
{
    
    [SerializeField]
    private string nivelMover;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hacha"))
        {
            SceneManager.LoadScene(nivelMover);
        }
    }
}
