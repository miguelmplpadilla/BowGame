using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GranadaFinalController : MonoBehaviour
{

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine("explosionFinal");
    }

    IEnumerator explosionFinal()
    {
        yield return new WaitForSeconds(3f);
        
        GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
        
        animator.SetTrigger("explotar");
    }

    public void finJuego()
    {
        SceneManager.LoadScene("EscenaFinJuego");
    }
}
