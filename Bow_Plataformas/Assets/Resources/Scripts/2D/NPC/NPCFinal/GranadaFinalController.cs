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
        yield return new WaitForSeconds(1f);
        
        GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        
        yield return new WaitForSeconds(2f);

        animator.SetTrigger("explotar");
    }

    public void finJuego()
    {
        SceneManager.LoadScene("EscenaFinJuego");
    }
}
